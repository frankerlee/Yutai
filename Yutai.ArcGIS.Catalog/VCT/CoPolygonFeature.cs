using System.Collections.Generic;

namespace Yutai.ArcGIS.Catalog.VCT
{
    internal class CoPolygonFeature : AbsCoFeature, ICoPolygonFeature
    {
        private List<CoPointCollection> list_1;

        public CoPolygonFeature(ICoLayer icoLayer_1) : base(icoLayer_1, CoFeatureType.Polygon)
        {
            this.list_1 = new List<CoPointCollection>();
        }

        ~CoPolygonFeature()
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