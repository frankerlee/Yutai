using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class FieldWrapEx
    {
        private IField ifield_0;
        private IFieldInfo ifieldInfo_0;

        internal FieldWrapEx(IField ifield_1, IFieldInfo ifieldInfo_1)
        {
            this.ifield_0 = ifield_1;
            this.ifieldInfo_0 = ifieldInfo_1;
        }

        public override string ToString()
        {
            return this.ifieldInfo_0.Alias;
        }

        public string Name
        {
            get
            {
                return this.ifield_0.Name;
            }
        }
    }
}

