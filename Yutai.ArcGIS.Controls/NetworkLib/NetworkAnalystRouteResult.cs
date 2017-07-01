using System.Collections.Specialized;
using System.Data;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class NetworkAnalystRouteResult : NetworkAnalystResult
    {
        private DataTable m_directions;
        private IEnvelope[] m_directionsExtents;
        private IEnvelope m_extent;
        private Point[] m_maneuverPoints;
        private int m_routeID;
        private StringDictionary m_summary;

        public DataTable Directions
        {
            get { return this.m_directions; }
            set { this.m_directions = value; }
        }

        public IEnvelope RouteExtent
        {
            get { return this.m_extent; }
            set { this.m_extent = value; }
        }

        public int RouteID
        {
            get { return this.m_routeID; }
            set { this.m_routeID = value; }
        }

        public IEnvelope[] StepExtents
        {
            get { return this.m_directionsExtents; }
            set { this.m_directionsExtents = value; }
        }

        public Point[] StepManeuverPoints
        {
            get { return this.m_maneuverPoints; }
            set { this.m_maneuverPoints = value; }
        }

        public StringDictionary Summary
        {
            get { return this.m_summary; }
            set { this.m_summary = value; }
        }
    }
}