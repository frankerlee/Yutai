using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class BasicMarkerSymbolLayer : BasicSymbolLayerBaseControl
    {

        public BasicMarkerSymbolLayer()
        {
            this.InitializeComponent();
        }

        private void BasicMarkerSymbolLayer_Load(object sender, EventArgs e)
        {
            IMarkerPlacement markerPlacement = (base.m_pBasicSymbol as IBasicMarkerSymbol).MarkerPlacement;
            Control c = base.CreateMarkerPlaceControl(markerPlacement, this);
            if (c != null)
            {
                (c as MarkerPlacementBaseControl).BasicMarkerSymbol = base.m_pBasicSymbol as IBasicMarkerSymbol;
                base.AddControl(c);
            }
            Control control2 = base.CreateControlByGeometricEffect(base.m_pBasicSymbol as IGeometricEffect, this);
            (control2 as GeometricEffectBaseControl).GeometricEffect = base.m_pBasicSymbol as IGeometricEffect;
            base.AddControl(control2);
            for (int i = (base.m_pBasicSymbol as IGeometricEffects).Count - 1; i >= 0; i--)
            {
                control2 = base.CreateControlByGeometricEffect((base.m_pBasicSymbol as IGeometricEffects).get_Element(i), this);
                (control2 as GeometricEffectBaseControl).GeometricEffect = (base.m_pBasicSymbol as IGeometricEffects).get_Element(i);
                base.AddControl(control2);
            }
        }


    }
}