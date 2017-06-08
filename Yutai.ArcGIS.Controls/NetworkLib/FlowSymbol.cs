using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    public class FlowSymbol
    {
        private ISymbol m_DeterminateFolwArrow = null;
        private ISymbol m_IndeterminateFolwArrow = null;
        private ISymbol m_UninitializedFolwArrow = null;

        public FlowSymbol()
        {
            ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass();
            IRgbColor color = new RgbColorClass {
                Green = 0,
                Blue = 0,
                Red = 0
            };
            symbol.Color = color;
            symbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            symbol.Size = 6.0;
            this.m_UninitializedFolwArrow = symbol as ISymbol;
            this.m_IndeterminateFolwArrow = (symbol as IClone).Clone() as ISymbol;
            IArrowMarkerSymbol symbol2 = new ArrowMarkerSymbolClass();
            IRgbColor color2 = new RgbColorClass {
                Green = 0,
                Blue = 0,
                Red = 0
            };
            symbol2.Color = color2;
            symbol2.Style = esriArrowMarkerStyle.esriAMSPlain;
            symbol2.Size = 9.0;
            this.m_DeterminateFolwArrow = symbol2 as ISymbol;
        }

        public ISymbol DeterminateFolwArrow
        {
            get
            {
                return this.m_DeterminateFolwArrow;
            }
            set
            {
                this.m_DeterminateFolwArrow = value;
            }
        }

        public ISymbol IndeterminateFolwArrow
        {
            get
            {
                return this.m_IndeterminateFolwArrow;
            }
            set
            {
                this.m_IndeterminateFolwArrow = value;
            }
        }

        public ISymbol UninitializedFolwArrow
        {
            get
            {
                return this.m_UninitializedFolwArrow;
            }
            set
            {
                this.m_UninitializedFolwArrow = value;
            }
        }
    }
}

