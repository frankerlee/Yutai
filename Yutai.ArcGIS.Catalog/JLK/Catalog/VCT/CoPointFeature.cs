namespace JLK.Catalog.VCT
{
    using System;

    internal class CoPointFeature : AbsCoFeature, ICoPointFeature
    {
        private CoPointCollection coPointCollection_0;

        public CoPointFeature(ICoLayer icoLayer_1) : base(icoLayer_1, CoFeatureType.Point)
        {
            this.coPointCollection_0 = new CoPointCollection();
        }

        ~CoPointFeature()
        {
            this.coPointCollection_0.Clear();
            this.coPointCollection_0 = null;
        }

        public CoPointCollection Point
        {
            get
            {
                return this.coPointCollection_0;
            }
            set
            {
                this.coPointCollection_0 = value;
            }
        }
    }
}

