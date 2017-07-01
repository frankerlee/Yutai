using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class GeometricEffectBaseControl : UserControl
    {
        protected IGeometricEffect m_pGeometricEffect = null;

        public virtual bool Apply()
        {
            return false;
        }

        public IGeometricEffect GeometricEffect
        {
            get { return this.m_pGeometricEffect; }
            set { this.m_pGeometricEffect = value; }
        }
    }
}