using System.Reflection;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Raster.UI
{
    public class RasterAdjustHelper
    {
        private bool bool_0 = false;
        private esriGeoTransTypeEnum esriGeoTransTypeEnum_0 = esriGeoTransTypeEnum.esriGeoTransPolyOrder1;
        private IMap imap_0 = null;
        private IPointCollection ipointCollection_0 = new Multipoint();
        private IPointCollection ipointCollection_1 = new Multipoint();
        private IRasterLayer irasterLayer_0;
        public IKHookHelper m_HookHelper = null;
        public static RasterAdjustHelper m_RasterAdjustHelper;

        public static event OnAddPointsHandler OnAddPoints;

        static RasterAdjustHelper()
        {
            old_acctor_mc();
        }

        public void AddPointPair(IPoint ipoint_0, IPoint ipoint_1)
        {
            object before = Missing.Value;
            this.SourcePointCollection.AddPoint(ipoint_0, ref before, ref before);
            this.DestPointCollection.AddPoint(ipoint_1, ref before, ref before);
            if (OnAddPoints != null)
            {
                OnAddPoints(ipoint_0, ipoint_1);
            }
            if (this.AutoAdjust)
            {
                this.Adjust();
            }
        }

        public void Adjust()
        {
            IRasterGeometryProc proc = new RasterGeometryProc();
            proc.Reset(this.irasterLayer_0.Raster);
            if (this.DestPointCollection.PointCount == 1)
            {
                IPoint point = this.SourcePointCollection.get_Point(0);
                IPoint point2 = this.DestPointCollection.get_Point(0);
                double deltaX = point2.X - point.X;
                double deltaY = point2.Y - point.Y;
                proc.Shift(deltaX, deltaY, this.irasterLayer_0.Raster);
            }
            else if (this.DestPointCollection.PointCount == 2)
            {
                proc.TwoPointsAdjust(this.SourcePointCollection, this.DestPointCollection, this.OperatorLayer.Raster);
            }
            else if (this.DestPointCollection.PointCount >= 3)
            {
                proc.Warp(this.SourcePointCollection, this.DestPointCollection, this.GeoTransType,
                    this.OperatorLayer.Raster);
            }
            proc.Register(this.OperatorLayer.Raster);
            proc = null;
            if (this.m_HookHelper.FocusMap != null)
            {
                (this.m_HookHelper.FocusMap as IActiveView).Refresh();
            }
        }

        public void Clear()
        {
            if (this.SourcePointCollection.PointCount > 0)
            {
                this.SourcePointCollection.RemovePoints(0, this.SourcePointCollection.PointCount);
            }
            if (this.DestPointCollection.PointCount > 0)
            {
                this.DestPointCollection.RemovePoints(0, this.DestPointCollection.PointCount);
            }
        }

        public void DeleteControlPointPair(int int_0)
        {
            if ((int_0 >= 0) && (int_0 <= this.SourcePointCollection.PointCount))
            {
                this.SourcePointCollection.RemovePoints(int_0, 1);
                this.DestPointCollection.RemovePoints(int_0, 1);
                if (this.m_HookHelper.FocusMap != null)
                {
                    (this.m_HookHelper.FocusMap as IActiveView).Refresh();
                }
            }
        }

        public static void Init()
        {
            m_RasterAdjustHelper = new RasterAdjustHelper();
        }

        private static void old_acctor_mc()
        {
            m_RasterAdjustHelper = null;
        }

        public static void Release()
        {
            m_RasterAdjustHelper = null;
        }

        public void Save()
        {
            IRasterProps props = (IRasterProps) this.irasterLayer_0.Raster;
            if (this.m_HookHelper.FocusMap != null)
            {
                props.SpatialReference = this.m_HookHelper.FocusMap.SpatialReference;
            }
            IRasterGeometryProc proc = new RasterGeometryProc();
            IRaster pRaster = this.irasterLayer_0.Raster;
            (this.irasterLayer_0 as IDataLayer2).Disconnect();
            proc.Register(pRaster);
            this.irasterLayer_0.CreateFromRaster(pRaster);
            this.DestPointCollection.RemovePoints(0, this.DestPointCollection.PointCount);
            this.SourcePointCollection.RemovePoints(0, this.SourcePointCollection.PointCount);
            if (this.m_HookHelper.FocusMap != null)
            {
                (this.m_HookHelper.FocusMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                    this.OperatorLayer, null);
            }
        }

        public bool AutoAdjust
        {
            get { return this.bool_0; }
            set { this.bool_0 = value; }
        }

        public IPointCollection DestPointCollection
        {
            get { return this.ipointCollection_1; }
        }

        public IMap FocusMap
        {
            set { this.imap_0 = value; }
        }

        public esriGeoTransTypeEnum GeoTransType
        {
            get { return this.esriGeoTransTypeEnum_0; }
            set { this.esriGeoTransTypeEnum_0 = value; }
        }

        public IRasterLayer OperatorLayer
        {
            get { return this.irasterLayer_0; }
            set { this.irasterLayer_0 = value; }
        }

        public static RasterAdjustHelper RasterAdjust
        {
            get { return m_RasterAdjustHelper; }
        }

        public IPointCollection SourcePointCollection
        {
            get { return this.ipointCollection_0; }
        }

        public delegate void OnAddPointsHandler(IPoint ipoint_0, IPoint ipoint_1);
    }
}