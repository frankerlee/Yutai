using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class BasicMarkerSymbolLayer : BasicSymbolLayerBaseControl
    {
        private IContainer components = null;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "BasicMarkerSymbolLayer";
            base.Size = new Size(0xd5, 0x100);
            base.Load += new EventHandler(this.BasicMarkerSymbolLayer_Load);
            base.ResumeLayout(false);
        }
    }
}

