using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.UI.Controls;

namespace Yutai.Plugins.Printing.Views
{
    public partial class AutoLayoutView : DockPanelControlBase, IAutoLayoutView
    {
        private IMapControl3 _mapControl3;
        private IPageLayoutControl2 _layoutControl;
        private IAppContext _context;
        private IActiveViewEvents_Event m_iPageLayout;
        private PrintingPlugin _plugin;

        private IGeometry _fence;
        private AutoLayoutHelper _helper=new AutoLayoutHelper();
        private List<IPrintPageInfo> _currentPageInfos;
        
        public AutoLayoutView()
        {
            InitializeComponent();
        }

        public void Initialize(IAppContext context, PrintingPlugin plugin)
        {
            try
            {
                _context = context;
                _plugin = plugin;
                _layoutControl = _context.MainView.PageLayoutControl;
                LoadMapTemplate();
                FillScaleList();
                LoadIndexMaps();
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn(ex.Message);
            }
        }

        private void LoadIndexMaps()
        {
            cmbIndexLayer.Properties.Items.Clear();
            foreach (IIndexMap indexMap in _plugin.PrintingConfig.IndexMaps)
            {
                cmbIndexLayer.Properties.Items.Add(new IndexMapInfo(indexMap));
            }
            
        }

        private class IndexMapInfo
        {
            public IIndexMap _indexMap;
            public IndexMapInfo(IIndexMap indexMap)
            {
                _indexMap = indexMap;
            }

            public override string ToString()
            {
                return _indexMap.Name;
            }
        }


        public void SetBuddyControl()
        {
            if (_context.MainView.ControlType == GISControlType.MapControl)
            {
                _mapControl3 = _context.MapControl as IMapControl3;
                _layoutControl = null;
            }
            else
            {
                _layoutControl = _context.MainView.PageLayoutControl;
                _mapControl3 = null;
            }
            

        }

        private void FillScaleList()
        {
            cmbScale.Properties.Items.Clear();
            cmbScale.Properties.Items.AddRange(new double[] { 500, 1000, 2000, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000 });
        }
        private void LoadMapTemplate()
        {
            cmbMapTemplate.Properties.Items.Clear();
            if (_plugin.TemplateGallery == null || _plugin.TemplateGallery.MapTemplateClass == null) return;
            foreach (var tClass in _plugin.TemplateGallery.MapTemplateClass)
            {
                foreach (var tTemplate in tClass.MapTemplate)
                {
                    cmbMapTemplate.Properties.Items.Add(new MapTemplateInfo(tTemplate));
                }
            }
        }

        internal class MapTemplateInfo
        {
            public MapTemplate _template;

            public MapTemplateInfo(MapTemplate template)
            {
                _template = template;
            }
            public override string ToString()
            {
                return _template.Name;
            }
        }

        #region Override DockPanelControlBase

        public override Bitmap Image
        {
            get { return Properties.Resources.icon_layout; }
        }

        public override string Caption
        {
            get { return "模板制图"; }
            set { Caption = value; }
        }

        public override DockPanelState DefaultDock
        {
            get { return DockPanelState.Right; }
        }

        public override string DockName
        {
            get { return DefaultDockName; }
        }

        public override string DefaultNestDockName
        {
            get { return ""; }
        }

        public const string DefaultDockName = "AutoLayout_Viewer";
        public IEnumerable<ToolStripItemCollection> ToolStrips
        {
            get { yield break; }
        }

        public IEnumerable<Control> Buttons
        {
            get { yield break; }
        }



        #endregion

        private void rdoSelectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoSelectMode.SelectedIndex == 0)
            {
                grpIndexMap.Enabled = true;
                grpKey.Enabled = true;
                grpFence.Enabled = true;
                cmbScale.Enabled = false;
            }
            else if (rdoSelectMode.SelectedIndex == 1)
            {
                grpIndexMap.Enabled = false;
                grpKey.Enabled = false;
                grpFence.Enabled = true;
                cmbScale.Enabled = true;
            }
        }

        private void btnSearchKey_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchKey.Text.Trim()))
            {
                MessageService.Current.Warn("请输入搜索关键字!");
                return;
            }
            _helper.Fence = _fence;
            _helper.MapTemplate = ((MapTemplateInfo) cmbMapTemplate.SelectedItem)._template;
            _helper.SearchText = txtSearchKey.Text;
            _helper.IndexMap = ((IndexMapInfo) cmbIndexLayer.SelectedItem)._indexMap;
            _helper.Scale = Convert.ToDouble(cmbScale.EditValue);
            _helper.LayoutType= AutoLayoutType.Index;
            _helper.Execute();
            if (_helper.PageInfos != null)
            {
                _currentPageInfos = _helper.PageInfos;
                FillPageInfos();
            }
        }


        private void FillPageInfos()
        {
            lstPages.Items.Clear();
            foreach (IPrintPageInfo pageInfo in _currentPageInfos)
            {
                
            }
        }
    }
}
