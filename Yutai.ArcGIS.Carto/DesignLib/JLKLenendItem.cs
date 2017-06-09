using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    internal class YTLegendItem
    {
        private ISymbol isymbol_0 = null;
        private string string_0 = "";

        internal YTLegendItem(ISymbol isymbol_1, string string_1)
        {
            this.isymbol_0 = isymbol_1;
            this.string_0 = string_1;
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

