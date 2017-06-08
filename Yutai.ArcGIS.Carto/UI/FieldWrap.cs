using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class FieldWrap
    {
        private IField ifield_0 = null;

        public FieldWrap(IField ifield_1)
        {
            this.ifield_0 = ifield_1;
        }

        public override string ToString()
        {
            return this.ifield_0.AliasName;
        }

        public string Name
        {
            get
            {
                return this.ifield_0.Name;
            }
        }

        public esriFieldType Type
        {
            get
            {
                return this.ifield_0.Type;
            }
        }
    }
}

