using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Controls.Editor.UI;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Properties;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Controls;

namespace Yutai.Plugins.Editor.Views
{
    public partial class EditTemplateView : DockPanelControlBase, IEditTemplateView
    {
        private IAppContext _context;
        private IMapControlEvents2_Event _mapEvent;
        private List<YutaiCommand> commands;
        private ImageList imageList;

        public EditTemplateView(IAppContext context)
        {
            InitializeComponent();
            //动态加载模板
            if (!DesignMode)
            {
                _context = context;
                editorTemplateManageCtrl21.Map = _context.FocusMap;
                _mapEvent=_context.MapControl as IMapControlEvents2_Event;
                EditorEvent.OnEditTemplateChange+= EditorEventOnOnEditTemplateChange;
                imageList = new ImageList();
            }
            //_context.RibbonMenu.comm
        }

        private void EditorEventOnOnEditTemplateChange(YTEditTemplate editTemplate0)
        {
            if(editTemplate0==null)
            { lstConstructionTools.Items.Clear();
                return;
            }
            BuildListFromCommands(editTemplate0.FeatureLayer.FeatureClass.ShapeType);
        }

        private void BuildListFromCommands(esriGeometryType geometryType)
        {
            lstConstructionTools.Items.Clear();
            commands= _context.RibbonMenu.SubItems.GetShapeCommands(geometryType);
            if (commands == null) return;
            imageList.Images.Clear();
            foreach (YutaiTool yutaiTool in commands)
            {
                imageList.Images.Add(yutaiTool.Image);
            }
            lstConstructionTools.ImageList = imageList;
            int i = 0;
            foreach (YutaiTool yutaiTool in commands)
            {
                lstConstructionTools.Items.Add(yutaiTool.Caption, i);
                i++;
            }
        }

        public void LinkMap()
        {
            if (_context != null)
            {
                editorTemplateManageCtrl21.Map = _context.FocusMap;
            }
        }
        public IEnumerable<ToolStripItemCollection> ToolStrips { get {yield break;} }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }

        public IFeatureLayer CurrentEditLayer
        {
            get { return _context.CurrentLayer as IFeatureLayer; }
        }

        public IConstructTool CurrentConstructTool { get; }
        public void OnTemplateSelectedChanged()
        {
            //throw new NotImplementedException();
        }

        public void OnConstructToolToolChanged()
        {
            //throw new NotImplementedException();
        }

        public void Initialize(IAppContext context)
        {
            editorTemplateManageCtrl21.Map = _context.FocusMap;

        }

        public override Bitmap Image { get { return Resources.icon_template; } }
        public override string Caption
        {
            get { return "要素模板"; }
            set { Caption = value; }
        }
        public override DockPanelState DefaultDock{ get { return DockPanelState.Right; } }
        public override string DockName { get { return DefaultDockName; } }
        public override string DefaultNestDockName { get { return ""; } }
        public const string DefaultDockName = "Editor_Feature_Template";

        private void lstConstructionTools_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstConstructionTools.SelectedIndex < 0) return;
            commands[lstConstructionTools.SelectedIndex].OnClick();
            _context.SetCurrentTool(commands[lstConstructionTools.SelectedIndex] as YutaiTool);

        }
    }
}
