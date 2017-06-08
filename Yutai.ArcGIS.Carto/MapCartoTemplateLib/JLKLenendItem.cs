using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    internal class JLKLenendItem
    {
        private ISymbol isymbol_0 = null;
        private ISymbol isymbol_1 = null;
        private string string_0 = "";

        internal JLKLenendItem(ISymbol isymbol_2, string string_1, ISymbol isymbol_3)
        {
            this.isymbol_0 = isymbol_2;
            this.string_0 = string_1;
            this.isymbol_1 = isymbol_3;
        }

        public ISymbol BackSymbol
        {
            get
            {
                return this.isymbol_1;
            }
            set
            {
                this.isymbol_1 = value;
            }
        }

        public string Description
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public ISymbol Symbol
        {
            get
            {
                return this.isymbol_0;
            }
            set
            {
                this.isymbol_0 = value;
            }
        }
    }
}

