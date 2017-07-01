using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class WeightWrap
    {
        private INetWeight m_pNetWeight = null;

        public WeightWrap(INetWeight pNetWeight)
        {
            this.m_pNetWeight = pNetWeight;
        }

        public override string ToString()
        {
            return this.m_pNetWeight.WeightName;
        }

        public INetWeight NetWeight
        {
            get { return this.m_pNetWeight; }
        }
    }
}