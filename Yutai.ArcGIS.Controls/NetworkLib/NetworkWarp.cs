using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public class NetworkWarp
    {
        private IGeometricNetwork m_pGeoNetwork = null;

        public NetworkWarp(IGeometricNetwork pGeoNetwork)
        {
            this.m_pGeoNetwork = pGeoNetwork;
        }

        public override string ToString()
        {
            if (this.m_pGeoNetwork == null)
            {
                return "";
            }
            return (this.m_pGeoNetwork as IDataset).Name;
        }

        public IGeometricNetwork GeometricNetwork
        {
            get
            {
                return this.m_pGeoNetwork;
            }
        }
    }
}

