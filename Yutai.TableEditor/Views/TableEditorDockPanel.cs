using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.TableEditor.Commands;
using Yutai.Plugins.TableEditor.Editor;
using Yutai.Plugins.TableEditor.Enums;
using Yutai.Plugins.TableEditor.Menu;
using Yutai.UI.Controls;

namespace Yutai.Plugins.TableEditor.Views
{
    public partial class TableEditorDockPanel : DockPanelControlBase, ITableEditorView
    {
        private readonly IAppContext _context;
        private List<YutaiCommand> _commands;

        public TableEditorDockPanel(IAppContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            InitializeComponent();
            InitMenu();
            TableViews = new Dictionary<string, ITableView>();
            MapView = new MapView(_context.MapControl.Map);
        }

        ~TableEditorDockPanel()
        {
        }

        private void InitMenu()
        {
            CreateCommands();
            foreach (YutaiCommand yutaiCommand in _commands)
            {
                AddCommand(yutaiCommand);
            }
        }

        private void AddCommand(YutaiCommand command)
        {
            if (command is YutaiMenuCommand)
            {
                ToolStripDropDownButton toolStripDropDownButton = new ToolStripDropDownButton();
                toolStripDropDownButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
                toolStripDropDownButton.Name = command.Name;
                toolStripDropDownButton.Text = command.Caption;
                toolStrip.Items.Add(toolStripDropDownButton);
            }
            else if (command is YutaiSeparatorCommand)
            {
                if (string.IsNullOrWhiteSpace(command.Key))
                {
                    toolStrip.Items.Add(new ToolStripSeparator());
                }
                else
                {
                    ToolStripDropDownButton dropDown = toolStrip.Items[command.Key] as ToolStripDropDownButton;
                    dropDown.DropDownItems.Add(new ToolStripSeparator());
                }
            }
            else
            {
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
                    toolStrip.Items.Add(menu);
                }
                else if (names.Length == 2)
                {
                    ToolStripDropDownButton dropDown = toolStrip.Items[names[0]] as ToolStripDropDownButton;

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
        }

        private void CreateCommands()
        {
            if (_commands == null)
            {
                _commands = new List<YutaiCommand>()
                {
                    new YutaiMenuCommand("tedSelection", "tedSelection", "tedSelection", "选择", ""),
                    new CmdZoomToCurrentFeature(_context, this),
                    new CmdZoomToSelectedFeatures(_context, this),
                    new YutaiSeparatorCommand("tedSelection"),
                    new CmdBuildQuery(_context, this),
                    new YutaiSeparatorCommand("tedSelection"),
                    new CmdAttributeExplorer(_context, this),
                    new YutaiSeparatorCommand("tedSelection"),
                    new CmdSelectAll(_context, this),
                    new CmdSelectNone(_context, this),
                    new CmdInvertSelection(_context, this),
                    new YutaiSeparatorCommand("tedSelection"),
                    new CmdExportAll(_context, this),
                    //new CmdExportSelection(_context, this),
                    new YutaiMenuCommand("tedFields", "tedFields", "tedFields", "字段", ""),
                    new CmdAddField(_context, this),
                    new YutaiSeparatorCommand("tedFields"),
                    new CmdShowAliases(_context, this),
                    new CmdShowAllFields(_context, this),
                    new YutaiMenuCommand("tedTools", "tedTools", "tedTools", "工具", ""),
                    new CmdFind(_context, this),
                    new YutaiSeparatorCommand("tedTools"),
                    new CmdJoinDatasource(_context, this),
                    new YutaiSeparatorCommand(),
                    new YutaiMenuCommand("tedLayout", "tedLayout", "tedLayout", "布局", ""),
                    new CmdReloadTable(_context, this),
                    new CmdClearSorting(_context, this),
                };
            }
        }

        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        public IMapView MapView { get; }
        public Dictionary<string, ITableView> TableViews { get; set; }

        public XtraTabControl MainTabControl
        {
            get { return xtraTabControl; }
        }

        public void ActivatePage(string pageName)
        {
            XtraTabPage page = xtraTabControl.TabPages.FirstOrDefault(c => c.Name == pageName);
            if (page != null) xtraTabControl.SelectedTabPage = page;
        }

        public void ClosePage(string pageName)
        {
            TableViews.Remove(pageName);
            XtraTabPage page = xtraTabControl.TabPages.FirstOrDefault(c => c.Name == pageName);
            if (page != null) xtraTabControl.TabPages.Remove(page);
        }

        public void ClosePage()
        {
            TableViews.Remove(CurrentGridView.Name);

            xtraTabControl.TabPages.Remove(xtraTabControl.SelectedTabPage);
        }

        public void OpenTable(IFeatureLayer featureLayer)
        {
            if (TableViews.ContainsKey(featureLayer.Name))
            {
                ActivatePage(featureLayer.Name);
                return;
            }

            ITableView pTableView = new TablePage(_context, this, featureLayer);
            xtraTabControl.TabPages.Add(pTableView as XtraTabPage);
            xtraTabControl.SelectedTabPage = pTableView as XtraTabPage;

            TableViews.Add(featureLayer.Name, pTableView);
        }
        
        public void Clear()
        {
            TableViews.Clear();
            xtraTabControl.TabPages.Clear();
        }

        public ITableView CurrentGridView
        {
            get { return tabControl.SelectedTab as ITableView; }
        }

        public override Bitmap Image
        {
            get { return Properties.Resources.icon_attribute_table; }
        }

        public override string Caption
        {
            get { return "属性表"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock
        {
            get { return DockPanelState.Bottom; }
        }

        public override string DockName
        {
            get { return DefaultDockName; }
        }

        public virtual string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "Plug_TableEditor_View";

        private void xtraTabControl_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;
            XtraTabPage page = arg?.Page as XtraTabPage;
            if (page == null)
                return;
            ClosePage(page.Name);
        }
    }
}