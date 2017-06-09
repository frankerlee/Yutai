using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Commands.MapLegend;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.UI.Controls;

namespace Yutai.Controls
{
    public partial class MapLegendDockPanel : DockPanelControlBase, IMenuProvider, IMapLegendView
    {
        private readonly IAppContext _context;
        private esriTOCControlItem pTocItem = esriTOCControlItem.esriTOCControlItemNone;
        private IBasicMap pMap = null;
        private ILayer pLayer = null;
        private object pother = null;
        private object pindex = null;
        private ITOCBuddy2 _tocBuddyControl;
        private IBasicMap _selectedMap;
        private ILayer _selectedLayer;
        private List<YutaiCommand> _commands;
        private esriTOCControlItem _selectedItemType;
        private string _caption;

        public static string DefaultDockName = "Dock_Main_MapLegend";
        public MapLegendDockPanel(IAppContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            InitializeComponent();
            InitContextMenu();
            TabPosition = 0;
            /* legendControl1.LayerMouseUp += LegendLayerMouseUp;
             legendControl1.GroupMouseUp += LegendGroupMouseUp;*/
            axTOCControl1.OnMouseDown += AxTocControl1OnOnMouseDown;
            _caption = "二维图例";
            Image = Properties.Resources.icon_maplegend;
        }


        public override string DockName
        {
            get { return "Dock_Main_MapLegend"; }
        }

        private void InitContextMenu()
        {
            CreateCommands();
            foreach (YutaiCommand command in _commands)
            {
                AddCommand(command);
            }
        }

        private void AddMenuCommand(YutaiCommand command)
        {
            ToolStripDropDownButton dropDown = new ToolStripDropDownButton();
            dropDown.Name = command.Name;
            dropDown.Text = command.Caption;
            contextMenuLayer.Items.Add(dropDown);
        }

        private void AddCommand(YutaiCommand command)
        {
            if (command is YutaiMenuCommand)
            {
                AddMenuCommand(command);
                return;
            }
            string[] names = command.Name.Split('.');
            if (names.Length == 1)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem
                {
                    Text = command.Caption,
                    Name = command.Name,
                    ToolTipText = command.Tooltip,
                    Image = command.Image,
                };
                menu.Click += command.OnClick;
                contextMenuLayer.Items.Add(menu);
            }
            else if (names.Length == 2)
            {
                ToolStripDropDownButton dropDown = contextMenuLayer.Items[names[0]] as ToolStripDropDownButton;
                ToolStripMenuItem menu = new ToolStripMenuItem
                {
                    Text = command.Caption,
                    Name = command.Name,
                    ToolTipText = command.Tooltip,
                    Image = command.Image,
                };
                menu.Click += command.OnClick;
                dropDown.DropDownItems.Add(menu);
            }
        }

        private void CreateCommands()
        {
            if (_commands == null)
            {
                _commands = new List<YutaiCommand>
                {
                    new CmdLegendAddGroupLayer(_context,this),
                    new CmdLegendAddData(_context,this),
                    new CmdOpenAttributeTable(_context, this),
                    new CmdZoomToLayer(_context, this),
                    new YutaiMenuCommand("dropVisibleScaleRange","dropVisibleScaleRange","dropVisibleScaleRange", "可见比例范围", ""),
                    new CmdSetMinimumScale(_context, this),
                    new CmdSetMaximumScale(_context, this),
                    new CmdClearScaleRange(_context,this),
                    new YutaiMenuCommand("dropSelection", "dropSelection","dropSelection", "选择",""),
                    new CmdZoomToSelectFeatures(_context, this),
                    new CmdPanToSelectedFeature(_context, this),
                    new CmdSelectAllFeatures(_context, this),
                    new CmdSwitchSelectedFeature(_context, this),
                    new CmdCreateLayerBySelection(_context,this),
                    new CmdExpandAllLayer(_context,this),
                    new CmdCollapseAllLayer(_context,this),
                    new CmdShowAllLayer(_context, this),
                    new CmdHideAllLayer(_context, this),
                    new CmdExportData(_context,this),
                    new CmdDeleteAllLayer(_context,this),
                    new CmdDeleteLayer(_context,this),
                };
            }
        }

        private void AxTocControl1OnOnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent itocControlEventsOnMouseDownEvent)
        {
            if (itocControlEventsOnMouseDownEvent.button == 2)
            {
                axTOCControl1.HitTest(itocControlEventsOnMouseDownEvent.x, itocControlEventsOnMouseDownEvent.y, ref pTocItem, ref pMap, ref pLayer, ref pother, ref pindex);
                if (pTocItem == esriTOCControlItem.esriTOCControlItemMap)
                    axTOCControl1.SelectItem(pMap, null);
                else
                    axTOCControl1.SelectItem(pLayer, null);

                var pnt = PointToClient(Cursor.Position);
                contextMenuLayer.Show(this, pnt);
            }
        }


        /* public event KeyEventHandler LegendKeyDown
        {
          add { axTOCControl1.KeyDown += value; }
            remove { axTOCControl1.KeyDown -= value; }
        }*/

        public int SelectedGroupHandle { get; private set; }

        public ITOCControl Legend
        {
            get { return (ITOCControl)axTOCControl1.Object; }
        }

        public AxTOCControl LegendControl
        {
            get { return (AxTOCControl)axTOCControl1; }
        }

        /*private void LegendGroupMouseUp(object sender, GroupMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SelectedGroupHandle = e.GroupHandle;
                var pnt = PointToClient(Cursor.Position);
                contextMenuGroup.Show(this, pnt);
            }
        }*/

        /* private void LegendLayerMouseUp(object sender, LayerMouseEventArgs e)
         {
             if (e.Button == MouseButtons.Right)
             {
                 Legend.SelectedLayerHandle = e.LayerHandle;

                 var group = Legend.Groups.GroupByLayerHandle(e.LayerHandle);
                 if (group != null)
                 {
                     SelectedGroupHandle = group.Handle;
                 }

                 var pnt = PointToClient(Cursor.Position);
                 contextMenuLayer.Show(this, pnt);
             }
         }*/

        /* private void OnLegendClick(object sender, LegendClickEventArgs e)
         {
             if (e.Button == MouseButtons.Right)
             {
                 var pnt = PointToClient(Cursor.Position);
                 contextMenuGroup.Show(this, pnt);
             }
         }*/

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get
            {
                //因为用Command的方式开发功能，因此在这儿不将ToolStrip注入，避免多次触发菜单的点击事件
                yield break;
                //yield return contextMenuLayer.Items;
                //yield return contextMenuGroup.Items;
            }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        private void contextMenuLayer_Opening(object sender, CancelEventArgs e)
        {

        }

        public ITOCControl2 TocControl
        {
            get { return axTOCControl1.Object as ITOCControl2; }
        }

        public ITOCBuddy2 TocBuddyControl
        {
            get { return ((ITOCControl2)axTOCControl1.Object).Buddy as ITOCBuddy2; }
        }

        public IBasicMap SelectedMap
        {
            get { return pMap; }
        }

        public ILayer SelectedLayer
        {
            get { return pLayer; }
        }

        public esriTOCControlItem SelectedItemType
        {
            get { return pTocItem; }
        }

        public override Bitmap Image { get; }

        public override string Caption { get { return _caption; } set { _caption = value; } }

        public override DockPanelState DefaultDock { get { return DockPanelState.Left; } }

        public override Size DefaultSize { get { return new Size(200,300); } }
        
    }
}
