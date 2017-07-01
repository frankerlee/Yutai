using ESRI.ArcGIS.Carto;

namespace Yutai.Pipeline.Analysis.Classes
{
    public class LayerInfo
    {
        private IFeatureLayer ifeatureLayer_0;

        public IFeatureLayer FeatureLauer
        {
            get { return this.ifeatureLayer_0; }
            set { this.ifeatureLayer_0 = value; }
        }

        public LayerInfo(IFeatureLayer ipLayer)
        {
            this.ifeatureLayer_0 = ipLayer;
        }

        public override string ToString()
        {
            return this.ifeatureLayer_0.Name;
        }
    }
}