using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class BasicFillSymbolLayer : BasicSymbolLayerBaseControl
    {
        public BasicFillSymbolLayer()
        {
            this.InitializeComponent();
        }

        private void BasicFillSymbolLayer_Load(object sender, EventArgs e)
        {
            Control c = base.CreateControlByGeometricEffect(base.m_pBasicSymbol as IGeometricEffect, this);
            (c as GeometricEffectBaseControl).GeometricEffect = base.m_pBasicSymbol as IGeometricEffect;
            base.AddControl(c);
            for (int i = (base.m_pBasicSymbol as IGeometricEffects).Count - 1; i >= 0; i--)
            {
                c = base.CreateControlByGeometricEffect((base.m_pBasicSymbol as IGeometricEffects).get_Element(i), this);
                (c as GeometricEffectBaseControl).GeometricEffect =
                    (base.m_pBasicSymbol as IGeometricEffects).get_Element(i);
                base.AddControl(c);
            }
        }
    }
}