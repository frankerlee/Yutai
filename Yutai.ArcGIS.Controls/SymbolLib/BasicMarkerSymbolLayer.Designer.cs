using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    partial class BasicMarkerSymbolLayer
    {
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
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Name = "BasicMarkerSymbolLayer";
            base.Size = new Size(213, 256);
            base.Load += new EventHandler(this.BasicMarkerSymbolLayer_Load);
            base.ResumeLayout(false);
        }
    
        private IContainer components = null;
    }
}