namespace Yutai.ArcGIS.Catalog.VCT
{
    public class CoFieldMapper
    {
        private ICoField icoField_0 = null;
        private ICoField icoField_1 = null;

        public CoFieldMapper(ICoField icoField_2, ICoField icoField_3)
        {
            this.icoField_0 = icoField_2;
            this.icoField_1 = icoField_3;
        }

        public ICoField DestField
        {
            get
            {
                return this.icoField_1;
            }
            set
            {
                this.icoField_1 = value;
            }
        }

        public ICoField SourceField
        {
            get
            {
                return this.icoField_0;
            }
            set
            {
                this.icoField_0 = value;
            }
        }
    }
}

