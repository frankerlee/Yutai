using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Check.Classes;

namespace Yutai.Check.Controls
{
    public partial class GridControlView : UserControl, IGridView
    {
        private double expandNum = 5;
        private IMap _map;
        private IActiveView _activeView;
        private IFeatureLayer _featureLayer;
        private bool _isSelect;
        private bool _isPanTo;
        private bool _isZoomTo;

        public GridControlView()
        {
            InitializeComponent();
        }

        public bool DisplayRemarks
        {
            get
            {
                return this.mainGridView.OptionsView.ShowPreview = true;
            }
            set
            {
                this.mainGridView.OptionsView.ShowPreview = value;
                this.mainGridView.PreviewFieldName = value ? "[备注]" : "";
            }
        }

        public IFeatureLayer FeatureLayer
        {
            set { _featureLayer = value; }
        }

        public IMap Map
        {
            set
            {
                _map = value;
                _activeView = _map as IActiveView;
            }
        }

        public GridControl Grid => this.mainGridControl;

        public void ExportToXls(string exportFilePath)
        {
            this.mainGridControl.ExportToXls(exportFilePath);
        }

        public void ExportToXlsx(string exportFilePath)
        {
            this.mainGridControl.ExportToXlsx(exportFilePath);
        }

        public void ExportToRtf(string exportFilePath)
        {
            this.mainGridControl.ExportToRtf(exportFilePath);
        }

        public void ExportToPdf(string exportFilePath)
        {
            this.mainGridControl.ExportToPdf(exportFilePath);
        }

        public void ExportToHtml(string exportFilePath)
        {
            this.mainGridControl.ExportToHtml(exportFilePath);
        }

        public void ExportToMht(string exportFilePath)
        {
            this.mainGridControl.ExportToMht(exportFilePath);
        }

        public void BestFitColumns()
        {
            this.mainGridView.BestFitColumns();
        }

        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }

        public bool IsPanTo
        {
            get { return _isPanTo; }
            set { _isPanTo = value; }
        }

        public bool IsZoomTo
        {
            get { return _isZoomTo; }
            set { _isZoomTo = value; }
        }

        private void mainGridView_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (_map == null || _featureLayer == null)
                return;
            ExpandoObject featureItem = mainGridView.GetRow(e.RowHandle) as ExpandoObject;
            if (featureItem == null)
                return;
            var item = featureItem as IDictionary<string, System.Object>;
            if (_isSelect)
                SelectFeature(_featureLayer, (int)item["[编号]"]);
            if (_isPanTo)
                PanToFeature(_featureLayer, (int)item["[编号]"]);
            if (_isZoomTo)
                ZoomToFeature(_featureLayer, (int)item["[编号]"]);
        }

        public IFeature SelectFeature(IFeatureLayer featureLayer, int oid)
        {
            IFeature pFeature = featureLayer.FeatureClass.GetFeature(oid);
            _map.ClearSelection();
            _map.SelectFeature(featureLayer, pFeature);
            _activeView.Refresh();
            return pFeature;
        }
        public void PanToFeature(IFeatureLayer featureLayer, int oid)
        {
            IFeature pFeature = SelectFeature(featureLayer, oid);
            IGeometry pGeometry = pFeature.Shape;
            if (pGeometry == null || pGeometry.IsEmpty)
                return;
            IEnvelope pEnvelope = _activeView.Extent;
            IEnvelope pEnvelopeCenter = pGeometry.Envelope;
            double centerX = (pEnvelopeCenter.XMax + pEnvelopeCenter.XMin) / 2;
            double centerY = (pEnvelopeCenter.YMax + pEnvelopeCenter.YMin) / 2;
            IPoint point = new MapPointClass();
            point.PutCoords(centerX, centerY);
            pEnvelope.CenterAt(point);
            _activeView.Extent = pEnvelope;

            _activeView.Refresh();
        }
        public void ZoomToFeature(IFeatureLayer featureLayer, int oid)
        {
            IFeature pFeature = SelectFeature(featureLayer, oid);
            IGeometry pGeometry = pFeature.Shape;
            if (pGeometry == null || pGeometry.IsEmpty)
                return;
            IEnvelope pEnvelope = pGeometry.Envelope;
            if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                pEnvelope.XMax += 20;
                pEnvelope.XMin -= 20;
                pEnvelope.YMax += 20;
                pEnvelope.YMin -= 20;
            }
            else
            {
                pEnvelope.Expand(expandNum, expandNum, false);
            }
            _activeView.Extent = pEnvelope;
            _activeView.Refresh();
        }
    }
}
