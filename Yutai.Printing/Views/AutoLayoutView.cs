using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Printing.Commands;
using Yutai.Plugins.Printing.Commands.Fence;
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
        private AutoLayoutHelper _helper = new AutoLayoutHelper();
        private List<IPrintPageInfo> _currentPageInfos;

        private ToolFenceCircle _fenceCircle;
        private ToolFenceCircleRadius _fencePoint;
        private CmdlFenceClear _fenceClear;
        private ToolFenceLine _fenceLine;
        private ToolFenceLineBuffer _fenceLineBuffer;
        private CmdlFenceExtent _fenceExtent;
        private ToolFencePolygon _fencePolygon;
        private ToolFenceRectangle _fenceRect;

        private CmdPrintSetup _printer;

        private MapTemplate _template;
        private bool isRunning;


        public AutoLayoutView()
        {
            InitializeComponent();
        }

        private class TemplateInfo
        {
            public int OID;
            public string Name;
            public int ClassID;

            public TemplateInfo(int oid, string pName, int classID)
            {
                OID = oid;
                Name = pName;
                ClassID = classID;
            }

            public override string ToString()
            {
                return string.Format("{0}-{1}", OID, Name);
            }
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
                InitCommands();
            }
            catch (Exception ex)
            {
                MessageService.Current.Warn(ex.Message);
            }
        }

        private void InitCommands()
        {
            _fenceCircle = new ToolFenceCircle(_context, _plugin);
            _fencePoint = new ToolFenceCircleRadius(_context, _plugin);
            _fenceClear = new CmdlFenceClear(_context, _plugin);
            _fenceRect = new ToolFenceRectangle(_context, _plugin);
            _fenceExtent = new CmdlFenceExtent(_context, _plugin);
            _fenceLine = new ToolFenceLine(_context, _plugin);
            _fenceLineBuffer = new ToolFenceLineBuffer(_context, _plugin);
            _fencePolygon = new ToolFencePolygon(_context, _plugin);
            _printer = new CmdPrintSetup(_context, _plugin);
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
            cmbScale.Properties.Items.AddRange(new double[]
                {500, 1000, 2000, 5000, 10000, 25000, 50000, 100000, 250000, 500000, 1000000});
        }

        private void LoadMapTemplate()
        {
            cmbMapTemplate.Properties.Items.Clear();

            if (_plugin.TemplateGallery == null || _plugin.TemplateGallery.MapTemplateClass == null) return;
            ITable pTable = _plugin.TemplateGallery.MapTemplateTable;
            ICursor pCursor = pTable.Search(null, false);
            IRow pRow = pCursor.NextRow();
            while (pRow != null)
            {
                string tName = pRow.Value[pRow.Fields.FindField("Name")].ToString();
                int classID = Convert.ToInt32(pRow.Value[pRow.Fields.FindField("ClassID")]);
                cmbMapTemplate.Properties.Items.Add(new TemplateInfo(pRow.OID, tName, classID));
                pRow = pCursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(pCursor);
        }

        //internal class MapTemplateInfo
        //{
        //    public MapTemplate _template;

        //    public MapTemplateInfo(MapTemplate template)
        //    {
        //        _template = template;
        //    }
        //    public override string ToString()
        //    {
        //        return _template.Name;
        //    }
        //}

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

      

        private IGeometry CombineFence(IGeometryArray geometryArray)
        {
            IGeometry firstGeometry = geometryArray.Element[0];
            ITopologicalOperator topo = firstGeometry as ITopologicalOperator;
            if (!topo.IsKnownSimple) topo.Simplify();
            for (int i = 1; i < geometryArray.Count; i++)
            {
                IGeometry newGeom = topo.Union(geometryArray.Element[i]);
                topo = newGeom as ITopologicalOperator;
                if (!topo.IsKnownSimple) topo.Simplify();
            }

            topo.Simplify();
            return topo as IGeometry;
        }

        private void StartStripQuery()
        {
           
            _helper.MapTemplate = _template;
            _helper.Scale = Convert.ToDouble(cmbScale.EditValue);
            if (_currentPageInfos != null)
                _currentPageInfos.Clear();
            else
                _currentPageInfos = new List<IPrintPageInfo>();
            for (int i = 0; i < _plugin.Fences.Count; i++)
            {
                List<IPrintPageInfo> pages = _helper.CreateStripMapPageInfos(_plugin.Fences.Element[i] as IPolyline);
                if (pages != null && pages.Count > 0)
                {
                    foreach (IPrintPageInfo page in pages)
                    {
                            page.AutoElements.Add(new PrintPageElement("PageName","图名",page.PageName));
                            page.AutoElements.Add(new PrintPageElement("PageID", "图号", page.PageID.ToString()));
                            page.AutoElements.Add(new PrintPageElement("TotalCount", "总页数", page.TotalCount.ToString()));
                        _currentPageInfos.Add(page);
                    }
                }
            }


            _plugin.PageInfos = _currentPageInfos;
            FillPageInfos();
            this.xtraTabControl1.SelectedTabPageIndex = 1;
        }

        private void btnSearchKey_Click(object sender, EventArgs e)
        {
            
            if (cmbSearchType.SelectedIndex < 0)
            {
                MessageService.Current.Warn("请选择出图模板!");
                return;
            }
            if (cmbScale.EditValue == null || Convert.ToDouble(cmbScale.EditValue) < 0)
            {
                MessageService.Current.Warn("请设置比例尺!!");
                return;
            }
            if (cmbSearchType.SelectedIndex == 0)
            {
                if (cmbIndexLayer.SelectedIndex < 0)
                {
                    MessageService.Current.Warn("请设置分幅图用于搜索!");
                    return;
                }
            }
            if (cmbSearchType.SelectedIndex == 0 && _plugin.Fences.Count == 0)
            {
                if (string.IsNullOrEmpty(txtSearchKey.Text.Trim()))
                {
                    MessageService.Current.Warn("请至少输入搜索关键字或者打印范围!");
                    return;
                }
            }

            if (cmbSearchType.SelectedIndex == 1)
            {
              
                if (_plugin.Fences == null || _plugin.Fences.Count == 0)
                {
                    MessageService.Current.Warn("请至设置一个打印范围!");
                    return;
                }
            }

            
            

            if (cmbLayoutType.SelectedIndex == 1)
                {
                if (_plugin.Fences.Count == 0)
                {
                    MessageService.Current.Warn("请至设置一个打印范围!");
                    return;
                }
                StartStripQuery();
                    return;
                }
           

            if (_plugin.Fences.Count > 1)
            {
                if (MessageService.Current.Ask("系统发现你设置了多个打印范围，系统将对此进行综合后排版，继续吗?") == false)
                    return;
                _fence = CombineFence(_plugin.Fences);
                _plugin.Fences.RemoveAll();
                _plugin.Fences.Add(_fence);
            }
            else
            {
                _fence = _plugin.Fences.Element[0];
            }
            _helper.Fence = _fence;
            _helper.MapTemplate = _template;
            _helper.SearchText = txtSearchKey.Text;
            _helper.IndexMap = ((IndexMapInfo) cmbIndexLayer.SelectedItem)._indexMap;
            _helper.Scale = Convert.ToDouble(cmbScale.EditValue);
            _helper.LayoutType = AutoLayoutType.Index;
            _helper.Execute();
            if (_helper.PageInfos != null)
            {
                
                foreach (IPrintPageInfo page in _helper.PageInfos)
                {
                    page.AutoElements.Add(new PrintPageElement("PageName", "图名", page.PageName));
                    page.AutoElements.Add(new PrintPageElement("PageID", "图号", page.PageID.ToString()));
                    page.AutoElements.Add(new PrintPageElement("TotalCount", "总页数", page.TotalCount.ToString()));
                    //_currentPageInfos.Add(page);
                }
                _currentPageInfos = _helper.PageInfos;
                _plugin.PageInfos = _helper.PageInfos;
                FillPageInfos();
                this.xtraTabControl1.SelectedTabPageIndex = 1;
            }
        }


        private void FillPageInfos()
        {
            lstPages.Items.Clear();
            foreach (IPrintPageInfo pageInfo in _currentPageInfos)
            {
                lstPages.Items.Add(new PageInfoWrap(pageInfo), true);
            }
        }


        internal partial class PageInfoWrap
        {
            private IPrintPageInfo pageInfo = null;

            internal PageInfoWrap(IPrintPageInfo page)
            {
                this.pageInfo = page;
            }

            public override string ToString()
            {
                return string.Format("{0} - {1}", pageInfo.PageID, this.pageInfo.PageName);
            }

            internal IPrintPageInfo PrintPageInfo
            {
                get { return this.pageInfo; }
            }
        }

        private void btnPoint_Click(object sender, EventArgs e)
        {
            _context.SetCurrentTool(_fencePoint);
        }

        private void cmbMapTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateInfo info = cmbMapTemplate.SelectedItem as TemplateInfo;
            _template = new MapTemplate(info.OID,
                _plugin.TemplateGallery.MapTemplateClass.FirstOrDefault(c => c.OID == info.ClassID));
            if (_template != null)
            {
                cmbScale.EditValue = _template.Scale;
            }
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            _context.SetCurrentTool(_fenceCircle);
        }

        private void btnClearClip_Click(object sender, EventArgs e)
        {
            _fenceClear.OnClick();
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            _context.SetCurrentTool(_fenceRect);
        }

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            _context.SetCurrentTool(_fencePolygon);
        }

        private void btnPolyline_Click(object sender, EventArgs e)
        {
            _context.SetCurrentTool(_fenceLine);
        }

        private void btnLineBuffer_Click(object sender, EventArgs e)
        {
            _context.SetCurrentTool(_fenceLineBuffer);
        }

        private void btnExtent_Click(object sender, EventArgs e)
        {
            _fenceExtent.OnClick();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            if (lstPages.SelectedIndex <= 0)
            {
                return;
            }
            int pageNum = lstPages.SelectedIndex - 1;
            lstPages.SelectedIndex = pageNum;
        }

        private void lstPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPages.SelectedIndex < 0)
            {
                propertyPage.SelectedObject = null;
                EnableButtons();
                return;
            }
            PageInfoWrap infoWrap = lstPages.Items[lstPages.SelectedIndex] as PageInfoWrap;
            LoadPage(infoWrap);
            EnableButtons();
        }

        private void EnableButtons()
        {
            int i = lstPages.SelectedIndex;
            if (i < 0)
            {
                btnFirst.Enabled = false;
                btnPre.Enabled = false;
                btnZoom.Enabled = false;
                btnPrint.Enabled = false;
            }
            if (i >= 0)
            {
                btnZoom.Enabled = true;
                btnPrint.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
            int j = lstPages.Items.Count - 1;

            if (i > 0)
            {
                btnFirst.Enabled = true;
                btnPre.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }

            if (i == j)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }

            if (lstPages.CheckedItems.Count > 0)
            {
                btnDeletePage.Enabled = true;
                btnBatchPrint.Enabled = true;
            }
            else
            {
                btnDeletePage.Enabled = false;
                btnBatchPrint.Enabled = false;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            lstPages.SelectedIndex = 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (lstPages.Items.Count - 1 == lstPages.SelectedIndex) return;
            lstPages.SelectedIndex = lstPages.SelectedIndex + 1;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            lstPages.SelectedIndex = lstPages.Items.Count - 1;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            PageInfoWrap infoWrap = lstPages.Items[lstPages.SelectedIndex] as PageInfoWrap;
            LoadPage(infoWrap);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.lstPages.SelectedIndex < 0)
            {
                MessageService.Current.Warn("先选择打印的页面!");
                return;
            }
            _printer.OnClick();
        }

        private void btnDeletePage_Click(object sender, EventArgs e)
        {
            if (lstPages.CheckedItems.Count == 0)
            {
                MessageService.Current.Warn("先将准备删除的页面打钩，再点击该操作!");
                return;
            }
            if (!MessageService.Current.Ask("你准备删除打钩的页面，继续吗?"))
            {
                return;
            }
            isRunning = true;
            int total = lstPages.Items.Count - 1;
            for (int i = total; i >= 0; i--)
            {
                bool isChecked = lstPages.GetItemChecked(i);
                if (isChecked) lstPages.Items.RemoveAt(i);
            }
            isRunning = false;
        }

        private void btnBatchPrint_Click(object sender, EventArgs e)
        {
        }

        private void LoadPage(PageInfoWrap infoWrap)
        {
            if (isRunning) return;
            propertyPage.SelectedObject = infoWrap.PrintPageInfo;

            IActiveView pActiveView = _context.FocusMap as IActiveView;
            pActiveView.ScreenDisplay.DisplayTransformation.Rotation = btnRotatePage.Checked ? infoWrap.PrintPageInfo.Angle : 0;
            IPoint centerPoint = new ESRI.ArcGIS.Geometry.Point();
            IEnvelope pEnv = infoWrap.PrintPageInfo.Boundary.Envelope;
            centerPoint.PutCoords(pEnv.XMin + pEnv.Width / 2.0, pEnv.YMin + pEnv.Height / 2.0);
            IEnvelope pExtentEnv = pActiveView.Extent;
            pExtentEnv.CenterAt(centerPoint);
            pActiveView.Extent = pExtentEnv;
            pActiveView.ScreenDisplay.DisplayTransformation.ScaleRatio = infoWrap.PrintPageInfo.Scale;
            pActiveView.Refresh();
            if (_context.MainView.ControlType != GISControlType.PageLayout)
            {
                return;
            }
            LoadPrintPage(infoWrap.PrintPageInfo);
            MessageService.Current.Info("Refresh");
            pActiveView.Extent = pExtentEnv;
            pActiveView.ScreenDisplay.DisplayTransformation.ScaleRatio = infoWrap.PrintPageInfo.Scale;
            pActiveView.Refresh();
        }

        private void DeleteAllElements(IActiveView pAV)
        {
            IGraphicsContainer graphicsContainer = pAV.GraphicsContainer;
            IMapFrame mapFrame = graphicsContainer.FindFrame(pAV.FocusMap) as IMapFrame;
            (mapFrame as IMapGrids).ClearMapGrids();
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            List<IElement> elements = new List<IElement>();
            try
            {
                graphicsContainer.DeleteAllElements();
                graphicsContainer.Reset();
                element = graphicsContainer.Next();
                if (element != null)
                {
                    graphicsContainer.DeleteElement(element);
                }
                
                graphicsContainer.AddElement(mapFrame as IElement, -1);
                pAV.FocusMap = mapFrame.Map;
            }
            catch (Exception exception)
            {
            }
        }

        private void LoadPrintPage(IPrintPageInfo pageInfo)
        {
            if (_template == null) return;
            //因为前面的操作可能对Template有赋值，因此，在将参数付过去之前需要进行初始化
            _template.Load();

            //! 首先将PageInfo里面的属性值赋给 template

            foreach (MapTemplateParam param in _template.MapTemplateParam)
            {
                string paramName = param.Name;
                IPrintPageElement element =
                    pageInfo.AutoElements.FirstOrDefault(c => c.Name == param.Name || c.AliasName == param.Name);
                if (element == null) continue;
                switch (param.ParamDataType)
                {
                    case DataType.Boolean:
                        param.Value = Convert.ToBoolean(element.Value);
                        break;
                    case DataType.DateTime:
                        param.Value = Convert.ToDateTime(element.Value);
                        break;
                    case DataType.Interger:
                        param.Value = Convert.ToInt32(element.Value);
                        break;
                    case DataType.Float:
                        param.Value = Convert.ToDouble(element.Value);
                        break;
                    case DataType.String:
                        param.Value = element.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }


            _layoutControl = _context.MainView.PageLayoutControl;
            this.DeleteAllElements(this._layoutControl.ActiveView);
            ((_layoutControl.ActiveView as IActiveView).FocusMap as IMapClipOptions).ClipType =
                esriMapClipType.esriMapClipNone;
            if ((_layoutControl.ActiveView as IActiveView).FocusMap is IMapAutoExtentOptions)
            {
                ((_layoutControl.ActiveView as IActiveView).FocusMap as IMapAutoExtentOptions).AutoExtentType =
                    esriExtentTypeEnum.esriExtentDefault;
            }
            IEnvelope extent = pageInfo.Boundary.Envelope;
            MessageService.Current.Info(string.Format("{0},{1},{2},{3}",extent.XMin,extent.YMin,extent.XMax,extent.YMax));

            if (_template.MapFramingType == MapFramingType.StandardFraming)
            {
                _template.CreateTKN(_layoutControl.ActiveView as IActiveView, extent.LowerLeft);
            }
            else
                _template.CreateTKByRect2(_layoutControl.ActiveView as IActiveView, extent);

            (_layoutControl.ActiveView as IGraphicsContainerSelect).UnselectAllElements();
        }

        private void btnClearPage_Click(object sender, EventArgs e)
        {
            _currentPageInfos.Clear();
            lstPages.Items.Clear();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstPages.Items.Count; i++)
            {
                lstPages.SetItemChecked(i, true);
            }
        }

        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstPages.Items.Count; i++)
            {
                lstPages.SetItemChecked(i, false);
            }
        }

        private void btnReserveSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstPages.Items.Count; i++)
            {
                bool check = lstPages.GetItemChecked(i);
                lstPages.SetItemChecked(i, !check);
            }
        }

        private void btnDrawIndex_Click(object sender, EventArgs e)
        {
            if (_currentPageInfos == null || _currentPageInfos.Count == 0) return;
            if (_context.MainView.ControlType != GISControlType.MapControl)
            {
                MessageService.Current.Warn("索引图最好在地图环境下绘制!");
                return;
            }

            _plugin.DrawPage = true;
            _plugin.PageInfos = _currentPageInfos;
            ((IActiveView) _context.FocusMap).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);

            //if (_mapControl3 != null)
            //{
            //    ((IActiveViewEvents_Event)_mapControl3.ActiveView).AfterDraw-= OnAfterDraw;
            //}
            //_mapControl3 = _context.MainView.MapControl;
            //((IActiveViewEvents_Event)_mapControl3.ActiveView).AfterDraw += OnAfterDraw;
        }

        private void chkDrawIndex_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDrawIndex.Checked)
            {
                _plugin.DrawPage = true;
                _plugin.PageInfos = _currentPageInfos;
                ((IActiveView) _context.FocusMap).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            else
            {
                _plugin.DrawPage = false;
                _plugin.PageInfos = _currentPageInfos;
                ((IActiveView) _context.FocusMap).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        private void cmbLayoutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLayoutType.SelectedIndex == 0)
            {
                btnPoint.Enabled = true;
                btnCircle.Enabled = true;
                btnRectangle.Enabled = true;
                btnPolygon.Enabled = true;
                btnLineBuffer.Enabled = true;
                btnPolyline.Enabled = false;
                btnExtent.Enabled = false;
                _plugin.ClearFence();
            }
            else if (cmbLayoutType.SelectedIndex == 1)
            {
                cmbScale.Enabled = true;
                btnPoint.Enabled = false;
                btnCircle.Enabled = false;
                btnRectangle.Enabled = false;
                btnPolygon.Enabled = false;
                btnLineBuffer.Enabled = false;
                btnPolyline.Enabled = true;
                btnExtent.Enabled = true;
                _plugin.ClearFence();
            }
        }

        private void btnRotatePage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearchType.SelectedIndex == 0)
            {
                cmbIndexLayer.Enabled = true;
                grpKey.Visible = true;
                grpFence.Visible = true;
                cmbScale.Enabled = false;
                cmbLayoutType.Enabled = false;
            }
            else if (cmbSearchType.SelectedIndex == 1)
            {
                cmbIndexLayer.Enabled = false;
                grpKey.Visible = false;
                grpFence.Visible = true;
                cmbLayoutType.Enabled = true;
            }
        }

      
    }
}