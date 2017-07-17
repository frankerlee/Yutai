using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Helper;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Controls
{
    public partial class UcExtentSetting : UserControl
    {
        public event EventHandler StartDrawEvent;
        public event EventHandler DrawCompleteEvent;
        private IAppContext _context;
        private IMap _map;
        private System.Windows.Forms.Cursor _defaultCursor;
        private System.Windows.Forms.Cursor _drawCursor;
        private INewPolygonFeedback _polygonFeedback;
        private IPolygon _polygon;
        private IGraphicsContainer _graphicsContainer;

        public UcExtentSetting()
        {
            InitializeComponent();
            this.Enabled = false;
            _defaultCursor = System.Windows.Forms.Cursors.Default;
            _drawCursor = System.Windows.Forms.Cursors.Cross;
        }

        ~UcExtentSetting()
        {
            Destory();
        }

        public IMap Map
        {
            set
            {
                _map = value;
                if (_map == null)
                    this.Enabled = false;
                else
                {
                    this.Enabled = true;
                    _graphicsContainer = _map as IGraphicsContainer;
                    _graphicsContainer.DeleteAllElements();
                }
            }
        }

        public IAppContext Context
        {
            set
            {
                _context = value;
                if (_context == null)
                    radioGroupExtentType.Properties.Items[3].Enabled = false;
                else
                    radioGroupExtentType.Properties.Items[3].Enabled = true;
            }
        }

        public IDictionary<int, IGeometry> BoundGeometrys
        {
            get
            {
                IDictionary<int, IGeometry> boundGeometrys = new Dictionary<int, IGeometry>();

                IActiveView activeView = _map as IActiveView;
                if (activeView == null)
                    return boundGeometrys;
                int type = (int)radioGroupExtentType.EditValue;
                switch (type)
                {
                    case 0: // 全部范围
                        {
                            boundGeometrys.Add(0, activeView.FullExtent);
                        }
                        break;
                    case 1: // 当前视图
                        {
                            boundGeometrys.Add(0, activeView.Extent);
                        }
                        break;
                    case 2: // 依据索引
                        {
                            for (int i = 0; i < checkedListBoxIndexes.Items.Count; i++)
                            {
                                if (checkedListBoxIndexes.GetItemChecked(i))
                                {
                                    int oid = (int)checkedListBoxIndexes.Items[i];
                                    if (oid >= 0)
                                    {
                                        IFeature feature = IndexLayer.FeatureClass.GetFeature(oid);
                                        if (feature != null && !feature.Shape.IsEmpty)
                                        {
                                            boundGeometrys.Add(oid, feature.ShapeCopy.Envelope);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 3:
                        {
                            if (_polygon != null)
                            {
                                boundGeometrys.Add(0, _polygon);
                            }
                        }
                        break;
                    default:
                        break;
                }

                return boundGeometrys;
            }
        }

        private IFeatureLayer IndexLayer
        {
            get { return ucSelectFeatureClass.SelectFeatureLayer; }
        }

        private void radioGroupExtentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int editValue = (int)radioGroupExtentType.EditValue;
            if (editValue == 2)
            {
                groupBoxIndexLayer.Enabled = true;
                LoadIndexLayers();
                this.Cursor = Cursors.Default;
                Destory();
            }
            else if (editValue == 3)
            {
                if (_context == null)
                    return;
                groupBoxIndexLayer.Enabled = false;
                InitDraw();
            }
            else
            {
                groupBoxIndexLayer.Enabled = false;
                this.Cursor = Cursors.Default;
                Destory();
            }
        }

        public void Destory()
        {
            _polygon = null;
            _polygonFeedback = null;
            this.Cursor = Cursors.Default;
            _graphicsContainer?.DeleteAllElements();
            ((IMapControlEvents2_Event)_context.MapControl).OnKeyDown -= OnOnKeyDown;
            ((IMapControlEvents2_Event)_context.MapControl).OnMouseDown -= OnOnMouseDown;
            ((IMapControlEvents2_Event)_context.MapControl).OnDoubleClick -= OnOnDoubleClick;
            ((IMapControlEvents2_Event)_context.MapControl).OnMouseMove -= OnOnMouseMove;
        }

        private void InitDraw()
        {
            if (_context == null)
                return;
            OnStartDrawEvent();
            ((IMapControlEvents2_Event)_context.MapControl).OnKeyDown += OnOnKeyDown;
            ((IMapControlEvents2_Event)_context.MapControl).OnMouseDown += OnOnMouseDown;
            ((IMapControlEvents2_Event)_context.MapControl).OnDoubleClick += OnOnDoubleClick;
            ((IMapControlEvents2_Event)_context.MapControl).OnMouseMove += OnOnMouseMove;
        }

        private void OnOnMouseMove(int button, int shift, int i, int i1, double mapX, double mapY)
        {
            if (_polygonFeedback == null)
                return;
            IPoint point = new PointClass();
            point.PutCoords(mapX, mapY);
            _polygonFeedback.MoveTo(point);
        }

        private void OnOnKeyDown(int keyCode, int shift)
        {
            if (keyCode == 27)
            {
                Destory();
            }
        }

        private void OnOnDoubleClick(int button, int shift, int i, int i1, double mapX, double mapY)
        {
            if (button == 2)
                return;
            if (_polygonFeedback == null)
                return;
            IPoint point = new PointClass();
            point.PutCoords(mapX, mapY);
            _polygonFeedback.AddPoint(point);
            _polygon = _polygonFeedback.Stop();
            IRgbColor rgbColorClass = new RgbColor();
            rgbColorClass.Red = (0);
            rgbColorClass.Green = (255);
            rgbColorClass.Blue = (255);
            ISimpleFillSymbol simpleFillSymbolClass = new SimpleFillSymbol();
            ((ISymbol)simpleFillSymbolClass).ROP2 = (esriRasterOpCode)(10);
            simpleFillSymbolClass.Color = (rgbColorClass);
            IElement element = new PolygonElement();
            element.Geometry = _polygon;
            ((IFillShapeElement)element).Symbol = simpleFillSymbolClass;
            _graphicsContainer.DeleteAllElements();
            _graphicsContainer.AddElement(element, 100);
            _context.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            OnDrawCompleteEvent();
            _polygon = null;
            _polygonFeedback = null;
        }

        private void OnOnMouseDown(int button, int shift, int i, int i1, double mapX, double mapY)
        {
            if (button == 2)
                return;

            IActiveView activeView = _context.ActiveView;
            IPoint point = new PointClass();
            point.PutCoords(mapX, mapY);
            if (_polygonFeedback == null)
            {
                _polygonFeedback = new NewPolygonFeedback()
                {
                    Display = activeView.ScreenDisplay
                };
                _polygonFeedback.Start(point);
            }
            else
            {
                _polygonFeedback.AddPoint(point);
            }
        }

        private void LoadIndexLayers()
        {
            if (_map == null)
                return;
            ucSelectFeatureClass.GeometryType = esriGeometryType.esriGeometryPolygon;
            ucSelectFeatureClass.Map = _map;
        }

        private void buttonByView_Click(object sender, EventArgs e)
        {
            if (checkedListBoxIndexes.Items.Count > 0)
                checkedListBoxIndexes.Items.Clear();
            if (IndexLayer == null)
            {
                MessageBox.Show(@"请选择索引图层！");
                return;
            }
            IActiveView activeView = _map as IActiveView;
            if (activeView == null)
                return;
            IEnvelope envelope = activeView.Extent;
            List<IFeature> features = MapHelper.GetFeaturesByEnvelope(envelope, IndexLayer);
            for (int i = 0; i < features.Count; i++)
            {
                IFeature feature = features[i];
                checkedListBoxIndexes.Items.Add(feature.OID);
            }
        }

        private void buttonBySelection_Click(object sender, EventArgs e)
        {
            if (checkedListBoxIndexes.Items.Count > 0)
                checkedListBoxIndexes.Items.Clear();
            if (IndexLayer == null)
            {
                MessageBox.Show(@"请选择索引图层！");
                return;
            }
            List<IFeature> features = MapHelper.GetFeaturesBySelected(IndexLayer);
            for (int i = 0; i < features.Count; i++)
            {
                IFeature feature = features[i];
                checkedListBoxIndexes.Items.Add(feature.OID);
            }
        }

        private void buttonByAll_Click(object sender, EventArgs e)
        {
            if (checkedListBoxIndexes.Items.Count > 0)
                checkedListBoxIndexes.Items.Clear();
            if (IndexLayer == null)
            {
                MessageBox.Show(@"请选择索引图层！");
                return;
            }
            IActiveView activeView = _map as IActiveView;
            if (activeView == null)
                return;
            IEnvelope envelope = activeView.FullExtent;
            List<IFeature> features = MapHelper.GetFeaturesByEnvelope(envelope, IndexLayer);
            for (int i = 0; i < features.Count; i++)
            {
                IFeature feature = features[i];
                checkedListBoxIndexes.Items.Add(feature.OID);
            }
        }

        private void buttonRefersh_Click(object sender, EventArgs e)
        {
            LoadIndexLayers();
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxIndexes.Items.Count; i++)
            {
                checkedListBoxIndexes.SetItemChecked(i, true);
            }
        }

        private void buttonSelectNull_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxIndexes.Items.Count; i++)
            {
                checkedListBoxIndexes.SetItemChecked(i, false);
            }
        }

        protected virtual void OnDrawCompleteEvent()
        {
            DrawCompleteEvent?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnStartDrawEvent()
        {
            StartDrawEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
