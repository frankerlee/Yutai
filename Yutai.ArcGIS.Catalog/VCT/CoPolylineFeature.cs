using System.Collections.Generic;

namespace Yutai.ArcGIS.Catalog.VCT
{
    internal class CoPolylineFeature : AbsCoFeature, ICoPolylineFeature
    {
        private List<CoPointCollection> list_1;

        public CoPolylineFeature(ICoLayer icoLayer_1) : base(icoLayer_1, CoFeatureType.Polyline)
        {
            this.list_1 = new List<CoPointCollection>();
        }

        ~CoPolylineFeature()
        {
            this.list_1.Clear();
            this.list_1 = null;
        }

        public List<CoPointCollection> Points
        {
            get { return this.list_1; }
        }
    }
}