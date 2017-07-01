using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public class GPSManager
    {
        public static esriGpsPositionInfo CurrentGpsPositionInfo;
        public static bool CurrentGpsPositionInfoIsValid = false;
        public static bool GPSConnectionOpen = false;
        private static IPoint m_CurrentPosition = null;
        private static IRealTimeFeedEvents_Event m_RealTimeFeedEvents_Event = null;
        private static IRealTimeFeedManager m_RealTimeFeedManager = null;

        public static event ConnectionStatusUpdatedEventHandler ConnectionStatusUpdated;

        public static event DateTimeUpdatedEventHandler DateTimeUpdated;

        public static event DgpsInfoUpdatedEventHandler DgpsInfoUpdated;

        public static event DopInfoUpdatedEventHandler DopInfoUpdated;

        public static event GpsPositionUpdatedHandler GpsPositionUpdated;

        public static event GroundCourseUpdatedEventHandler GroundCourseUpdated;

        public static event MagneticVarianceUpdatedEventHandler MagneticVarianceUpdated;

        public static event SatelliteInfoUpdatedEventHandler SatelliteInfoUpdated;

        public static IPoint GetFitCurrentPosition(IMap pMap, ISpatialReference pSR)
        {
            IGeographicCoordinateSystem geographicCoordinateSystem;
            IGeographicCoordinateSystem system2;
            if (m_CurrentPosition.SpatialReference is IUnknownCoordinateSystem)
            {
                return m_CurrentPosition;
            }
            if (pSR is IUnknownCoordinateSystem)
            {
                return m_CurrentPosition;
            }
            if (m_CurrentPosition.SpatialReference is IProjectedCoordinateSystem)
            {
                geographicCoordinateSystem =
                    (m_CurrentPosition.SpatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem;
            }
            else
            {
                geographicCoordinateSystem = m_CurrentPosition.SpatialReference as IGeographicCoordinateSystem;
            }
            if (pSR is IProjectedCoordinateSystem)
            {
                system2 = (pSR as IProjectedCoordinateSystem).GeographicCoordinateSystem;
            }
            else
            {
                system2 = pSR as IGeographicCoordinateSystem;
            }
            IPoint point = (m_CurrentPosition as IClone).Clone() as IPoint;
            IGeoTransformationOperationSet geographicTransformations =
                (pMap as IMapGeographicTransformations).GeographicTransformations;
            if (geographicTransformations != null)
            {
                esriTransformDirection direction;
                IGeoTransformation transformation;
                geographicTransformations.Get(geographicCoordinateSystem, system2, out direction, out transformation);
                if (transformation != null)
                {
                    (point as IGeometry2).ProjectEx(pSR, direction, transformation, true, 10.0, 10.0);
                }
                else
                {
                    (point as IGeometry2).Project(pSR);
                }
            }
            return point;
        }

        public static void InitRealTimeFeedManager()
        {
            if (m_RealTimeFeedManager == null)
            {
                m_RealTimeFeedManager = new RealTimeFeedManagerClass();
            }
        }

        private static void m_RealTimeFeedEvents_Event_ConnectionStatusUpdated(
            ref esriGpsConnectionStatus pConnectionStatus)
        {
            OnConnectionStatusUpdated(pConnectionStatus);
        }

        private static void m_RealTimeFeedEvents_Event_DateTimeUpdated(ref esriGpsDateTime pNewDateTime)
        {
            OnDateTimeUpdated(pNewDateTime);
        }

        private static void m_RealTimeFeedEvents_Event_DgpsInfoUpdated(ref esriGpsDgpsInfo pNewDGPSInfo)
        {
            OnDgpsInfoUpdated(pNewDGPSInfo);
        }

        private static void m_RealTimeFeedEvents_Event_DopInfoUpdated(ref esriGpsDOPInfo pdop)
        {
            OnDopInfoUpdated(pdop);
        }

        private static void m_RealTimeFeedEvents_Event_GroundCourseUpdated(ref esriGpsGroundCourse pGroundCourse)
        {
            OnGroundCourseUpdated(pGroundCourse);
        }

        private static void m_RealTimeFeedEvents_Event_MagneticVarianceUpdated(ref esriGpsMagneticVariance pMagneticVar)
        {
            OnMagneticVarianceUpdated(pMagneticVar);
        }

        private static void m_RealTimeFeedEvents_Event_PositionUpdated(ref esriGpsPositionInfo position, bool estimate)
        {
            CurrentGpsPositionInfoIsValid = true;
            CurrentGpsPositionInfo = position;
            IPoint p = new PointClass();
            if (m_CurrentPosition == null)
            {
                m_CurrentPosition = new PointClass();
                m_CurrentPosition.SpatialReference = m_RealTimeFeedManager.RealTimeFeed.SpatialReference;
            }
            m_CurrentPosition.PutCoords(position.longitude, position.latitude);
            p.PutCoords(position.longitude, position.latitude);
            p.SpatialReference = m_RealTimeFeedManager.RealTimeFeed.SpatialReference;
            if ((m_RealTimeFeedManager.Map != null) && (m_RealTimeFeedManager as IGpsDisplayProperties).AutoPan)
            {
                IActiveView map = m_RealTimeFeedManager.Map as IActiveView;
                if (
                    !((p.SpatialReference is IUnknownCoordinateSystem) ||
                      (m_RealTimeFeedManager.Map.SpatialReference is IUnknownCoordinateSystem)))
                {
                    p.Project(m_RealTimeFeedManager.Map.SpatialReference);
                }
                IEnvelope extent = map.Extent;
                extent.CenterAt(p);
                map.Extent = extent;
                map.Refresh();
            }
            OnGpsPositionUpdated(position);
        }

        private static void m_RealTimeFeedEvents_Event_SatelliteInfoUpdated(int satelliteCount)
        {
            OnSatelliteInfoUpdated(satelliteCount);
        }

        public static void OnConnectionStatusUpdated(esriGpsConnectionStatus pConnectionStatus)
        {
            if (ConnectionStatusUpdated != null)
            {
                ConnectionStatusUpdated(pConnectionStatus);
            }
        }

        public static void OnDateTimeUpdated(esriGpsDateTime pNewDateTime)
        {
            if (DateTimeUpdated != null)
            {
                DateTimeUpdated(pNewDateTime);
            }
        }

        public static void OnDgpsInfoUpdated(esriGpsDgpsInfo pNewDGPSInfo)
        {
            if (DgpsInfoUpdated != null)
            {
                DgpsInfoUpdated(pNewDGPSInfo);
            }
        }

        public static void OnDopInfoUpdated(esriGpsDOPInfo pdop)
        {
            if (DopInfoUpdated != null)
            {
                DopInfoUpdated(pdop);
            }
        }

        public static void OnGpsPositionUpdated(esriGpsPositionInfo position)
        {
            if (GpsPositionUpdated != null)
            {
                GpsPositionUpdated(position);
            }
        }

        public static void OnGroundCourseUpdated(esriGpsGroundCourse pGroundCourse)
        {
            if (GroundCourseUpdated != null)
            {
                GroundCourseUpdated(pGroundCourse);
            }
        }

        public static void OnMagneticVarianceUpdated(esriGpsMagneticVariance pMagneticVar)
        {
            if (MagneticVarianceUpdated != null)
            {
                MagneticVarianceUpdated(pMagneticVar);
            }
        }

        public static void OnSatelliteInfoUpdated(int satelliteCount)
        {
            if (SatelliteInfoUpdated != null)
            {
                SatelliteInfoUpdated(satelliteCount);
            }
        }

        public static IPoint CurrentPosition
        {
            get { return m_CurrentPosition; }
        }

        public static bool IsInit
        {
            get { return (m_RealTimeFeedManager != null); }
        }

        public static IMap Map
        {
            get
            {
                if (m_RealTimeFeedManager == null)
                {
                    InitRealTimeFeedManager();
                }
                return m_RealTimeFeedManager.Map;
            }
            set
            {
                if (m_RealTimeFeedManager == null)
                {
                    InitRealTimeFeedManager();
                }
                m_RealTimeFeedManager.Map = value;
            }
        }

        public static IRealTimeFeed RealTimeFeed
        {
            get
            {
                if (m_RealTimeFeedManager == null)
                {
                    InitRealTimeFeedManager();
                }
                return m_RealTimeFeedManager.RealTimeFeed;
            }
            set
            {
                if (m_RealTimeFeedManager == null)
                {
                    InitRealTimeFeedManager();
                }
                m_RealTimeFeedManager.RealTimeFeed = value;
                m_RealTimeFeedEvents_Event = value as IRealTimeFeedEvents_Event;
                m_RealTimeFeedEvents_Event.PositionUpdated +=
                (new IRealTimeFeedEvents_PositionUpdatedEventHandler(
                    GPSManager.m_RealTimeFeedEvents_Event_PositionUpdated));
                m_RealTimeFeedEvents_Event.SatelliteInfoUpdated +=
                (new IRealTimeFeedEvents_SatelliteInfoUpdatedEventHandler(
                    GPSManager.m_RealTimeFeedEvents_Event_SatelliteInfoUpdated));
                m_RealTimeFeedEvents_Event.MagneticVarianceUpdated +=
                (new IRealTimeFeedEvents_MagneticVarianceUpdatedEventHandler(
                    GPSManager.m_RealTimeFeedEvents_Event_MagneticVarianceUpdated));
                m_RealTimeFeedEvents_Event.GroundCourseUpdated +=
                (new IRealTimeFeedEvents_GroundCourseUpdatedEventHandler(
                    GPSManager.m_RealTimeFeedEvents_Event_GroundCourseUpdated));
                m_RealTimeFeedEvents_Event.DopInfoUpdated +=
                (new IRealTimeFeedEvents_DopInfoUpdatedEventHandler(
                    GPSManager.m_RealTimeFeedEvents_Event_DopInfoUpdated));
                m_RealTimeFeedEvents_Event.DgpsInfoUpdated +=
                (new IRealTimeFeedEvents_DgpsInfoUpdatedEventHandler(
                    GPSManager.m_RealTimeFeedEvents_Event_DgpsInfoUpdated));
                m_RealTimeFeedEvents_Event.DateTimeUpdated +=
                (new IRealTimeFeedEvents_DateTimeUpdatedEventHandler(
                    GPSManager.m_RealTimeFeedEvents_Event_DateTimeUpdated));
                m_RealTimeFeedEvents_Event.ConnectionStatusUpdated +=
                (new IRealTimeFeedEvents_ConnectionStatusUpdatedEventHandler(
                    GPSManager.m_RealTimeFeedEvents_Event_ConnectionStatusUpdated));
            }
        }

        public static IRealTimeFeedManager RealTimeFeedManager
        {
            get
            {
                if (m_RealTimeFeedManager == null)
                {
                    InitRealTimeFeedManager();
                }
                return m_RealTimeFeedManager;
            }
        }

        public delegate void ConnectionStatusUpdatedEventHandler(esriGpsConnectionStatus pConnectionStatus);

        public delegate void DateTimeUpdatedEventHandler(esriGpsDateTime pNewDateTime);

        public delegate void DgpsInfoUpdatedEventHandler(esriGpsDgpsInfo pNewDGPSInfo);

        public delegate void DopInfoUpdatedEventHandler(esriGpsDOPInfo pdop);

        public delegate void GpsPositionUpdatedHandler(esriGpsPositionInfo position);

        public delegate void GroundCourseUpdatedEventHandler(esriGpsGroundCourse pGroundCourse);

        public delegate void MagneticVarianceUpdatedEventHandler(esriGpsMagneticVariance pMagneticVar);

        public delegate void SatelliteInfoUpdatedEventHandler(int satelliteCount);
    }
}