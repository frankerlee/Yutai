using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.Helper;

namespace Yutai.Pipeline.Editor.Classes
{
    class LineFeatureCalculate : ILineFeatureCalculate
    {
        private IFeature _lineFeature;
        private int _idxDmgcField;
        private int _idxQdmsField;
        private int _idxZdmsField;
        private IFeatureLayer _lineFeatureLayer;
        private IFeatureLayer _pointFeatureLayer;

        private double _fromPointZ = -1;
        private double _toPointZ = -1;

        private IFeatureSearchByPolyline pSearchByPolyline;
        private IPoint _point;

        public LineFeatureCalculate(IFeature lineFeature, IFeatureLayer lineFeatureLayer, IFeatureLayer pointFeatureLayer)
            : base()
        {
            _lineFeature = lineFeature;
            _lineFeatureLayer = lineFeatureLayer;
            _pointFeatureLayer = pointFeatureLayer;
            pSearchByPolyline = new FeatureSearchByPolyline();
            pSearchByPolyline.PointFeatureLayer = _pointFeatureLayer;
            pSearchByPolyline.Polyline = LineFeature.ShapeCopy as IPolyline;
        }

        public IFeature LineFeature
        {
            get { return _lineFeature; }
            set { _lineFeature = value; }
        }

        public IPoint Point
        {
            get { return _point; }
            set { _point = value; }
        }

        public int IdxDMGCField
        {
            get { return _idxDmgcField; }
            set { _idxDmgcField = value; }
        }

        public int IdxQDMSField
        {
            get { return _idxQdmsField; }
            set { _idxQdmsField = value; }
        }

        public int IdxZDMSField
        {
            get { return _idxZdmsField; }
            set { _idxZdmsField = value; }
        }

        public double GetQDGCValue()
        {
            if (_fromPointZ <= -1)
            {
                IFeature pFeature = pSearchByPolyline.GetNearFromFeature();
                object obj = pFeature.Value[IdxDMGCField];
                if (obj is DBNull || obj == null)
                    return 0;
                _fromPointZ = Convert.ToDouble(obj);
            }
            return _fromPointZ;
        }

        public double GetZDGCValue()
        {
            if (_toPointZ <= -1)
            {
                IFeature pFeature = pSearchByPolyline.GetNearToFeature();
                object obj = pFeature.Value[IdxDMGCField];
                if (obj is DBNull || obj == null)
                    return 0;
                _toPointZ = Convert.ToDouble(obj);
            }
            return _toPointZ;
        }

        public double GetHeightByPoint()
        {
            IPolyline polyline = LineFeature.ShapeCopy as IPolyline;
            object objQdms = LineFeature.Value[IdxQDMSField];
            object objZdms = LineFeature.Value[IdxZDMSField];
            double dQdms, dZdms;
            if (objQdms is DBNull || objQdms == null)
                dQdms = 0;
            else
                dQdms = Convert.ToDouble(objQdms);
            if (objZdms is DBNull || objZdms == null)
                dZdms = 0;
            else
                dZdms = Convert.ToDouble(objZdms);

            return GeometryHelper.GetZValue(polyline.FromPoint, GetQDGCValue() - dQdms, polyline.ToPoint, GetZDGCValue() - dZdms, Point);
        }

        public double GetGroundHeightByPoint()
        {
            IPolyline polyline = LineFeature.ShapeCopy as IPolyline;
            return GeometryHelper.GetZValue(polyline.FromPoint, GetQDGCValue(), polyline.ToPoint, GetZDGCValue(), Point);
        }

        public double GetDepthByPoint()
        {
            return GetGroundHeightByPoint() - GetHeightByPoint();
        }

        public IFeatureLayer LineFeatureLayer
        {
            get { return _lineFeatureLayer; }
            set { _lineFeatureLayer = value; }
        }

        public IFeatureLayer PointFeatureLayer
        {
            get { return _pointFeatureLayer; }
            set { _pointFeatureLayer = value; }
        }
    }
}
