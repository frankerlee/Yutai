using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class NetworkAnalystClosestFacilityResult : NetworkAnalystResult
    {
        private IEnvelope m_extent;
        private NetworkAnalystRouteResult[] m_routeResults;

        public IEnvelope Extent
        {
            get { return this.m_extent; }
            set { this.m_extent = value; }
        }

        public NetworkAnalystRouteResult[] RouteResults
        {
            get { return this.m_routeResults; }
            set { this.m_routeResults = value; }
        }
    }
}