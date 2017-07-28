using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Classes
{
    class FeatureSearchByPolyline : IFeatureSearchByPolyline
    {
        private double _tolerance = 0.0001;
        private IFeatureLayer _pointFeatureLayer;
        private IPolyline _polyline;

        public FeatureSearchByPolyline()
            : base()
        {
        }

        public double Tolerance
        {
            get { return _tolerance; }
            set { _tolerance = value; }
        }

        public IFeatureLayer PointFeatureLayer
        {
            get { return _pointFeatureLayer; }
            set { _pointFeatureLayer = value; }
        }

        public IPolyline Polyline
        {
            get { return _polyline; }
            set { _polyline = value; }
        }

        public IFeature GetNearFromFeature()
        {
            if (Polyline == null || PointFeatureLayer == null)
                return null;

            ITopologicalOperator pTopologicalOperator = Polyline.FromPoint as ITopologicalOperator;
            if (pTopologicalOperator == null)
                return null;
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = pTopologicalOperator.Buffer(Tolerance);
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            pSpatialFilter.GeometryField = PointFeatureLayer.FeatureClass.ShapeFieldName;

            IFeatureCursor pFeatureCursor = PointFeatureLayer.Search(pSpatialFilter, false);
            IFeature pFeature = pFeatureCursor.NextFeature();
            Marshal.ReleaseComObject(pFeatureCursor);
            return pFeature;
        }

        public IFeature GetNearToFeature()
        {
            if (Polyline == null || PointFeatureLayer == null)
                return null;

            ITopologicalOperator pTopologicalOperator = Polyline.ToPoint as ITopologicalOperator;
            if (pTopologicalOperator == null)
                return null;
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = pTopologicalOperator.Buffer(Tolerance);
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            pSpatialFilter.GeometryField = PointFeatureLayer.FeatureClass.ShapeFieldName;

            IFeatureCursor pFeatureCursor = PointFeatureLayer.Search(pSpatialFilter, false);
            IFeature pFeature = pFeatureCursor.NextFeature();
            Marshal.ReleaseComObject(pFeatureCursor);
            return pFeature;
        }
    }
}
