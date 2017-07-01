using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class MarkerPlacementBaseControl : UserControl
    {
        protected IBasicMarkerSymbol m_pBasicMarkerSymbol = null;
        protected BasicSymbolLayerBaseControl m_pControl = null;
        protected IGraphicAttributes m_pGeometricEffect = null;

        public virtual bool Apply()
        {
            return false;
        }

        public IBasicMarkerSymbol BasicMarkerSymbol
        {
            get { return this.m_pBasicMarkerSymbol; }
            set
            {
                this.m_pBasicMarkerSymbol = value;
                this.m_pGeometricEffect = this.m_pBasicMarkerSymbol.MarkerPlacement as IGraphicAttributes;
            }
        }

        public IGraphicAttributes GeometricEffect
        {
            get { return this.m_pGeometricEffect; }
            set { this.m_pGeometricEffect = value; }
        }
    }
}