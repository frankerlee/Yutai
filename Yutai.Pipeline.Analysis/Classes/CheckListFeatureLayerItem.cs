using ESRI.ArcGIS.Carto;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class CheckListFeatureLayerItem
    {
        public IFeatureLayer m_pFeatureLayer;

        public override string ToString()
        {
            string result;
            if (this.m_pFeatureLayer == null)
            {
                result = "";
            }
            else
            {
                result = this.m_pFeatureLayer.Name;
            }
            return result;
        }
    }
}