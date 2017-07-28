using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Classes
{
    class FeatureSearchByPoint : IFeatureSearchByPoint
    {
        private double _tolerance;
        private IFeatureLayer _lineFeatureLayer;
        private IPoint _point;

        public FeatureSearchByPoint() : base()
        {
        }

        public double Tolerance
        {
            get { return _tolerance; }
            set { _tolerance = value; }
        }

        public IFeatureLayer LineFeatureLayer
        {
            get { return _lineFeatureLayer; }
            set { _lineFeatureLayer = value; }
        }

        public IPoint Point
        {
            get { return _point; }
            set { _point = value; }
        }

        public IFeature GetNearFeature()
        {
            if (Point == null || LineFeatureLayer == null)
                return null;

            ITopologicalOperator pTopologicalOperator = Point as ITopologicalOperator;
            if (pTopologicalOperator == null)
                return null;
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = pTopologicalOperator.Buffer(Tolerance);
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            pSpatialFilter.GeometryField = LineFeatureLayer.FeatureClass.ShapeFieldName;

            IFeatureCursor pFeatureCursor = LineFeatureLayer.Search(pSpatialFilter, false);
            IFeature pFeature = pFeatureCursor.NextFeature();
            Marshal.ReleaseComObject(pFeatureCursor);
            return pFeature;
        }
    }
}
