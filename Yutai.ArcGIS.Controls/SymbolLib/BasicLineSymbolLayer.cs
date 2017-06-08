using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class BasicLineSymbolLayer : BasicSymbolLayerBaseControl
    {
        private IContainer components = null;

        public BasicLineSymbolLayer()
        {
            this.InitializeComponent();
        }

        private void BasicLineSymbolLayer_Load(object sender, EventArgs e)
        {
            if (base.m_pBasicSymbol != null)
            {
                Control c = base.CreateControlByGeometricEffect(base.m_pBasicSymbol as IGeometricEffect, this);
                (c as GeometricEffectBaseControl).GeometricEffect = base.m_pBasicSymbol as IGeometricEffect;
                base.AddControl(c);
                for (int i = (base.m_pBasicSymbol as IGeometricEffects).Count - 1; i >= 0; i--)
                {
                    c = base.CreateControlByGeometricEffect((base.m_pBasicSymbol as IGeometricEffects).get_Element(i), this);
                    (c as GeometricEffectBaseControl).GeometricEffect = (base.m_pBasicSymbol as IGeometricEffects).get_Element(i);
                    base.AddControl(c);
                }
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
            base.Name = "BasicLineSymbolLayer";
            base.Size = new Size(0xd5, 0x100);
            base.Load += new EventHandler(this.BasicLineSymbolLayer_Load);
            base.ResumeLayout(false);
        }
    }
}

