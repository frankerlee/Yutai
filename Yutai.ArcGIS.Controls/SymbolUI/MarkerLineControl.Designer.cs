using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class MarkerLineControl
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
            this.btnMarkSymbol = new NewSymbolButton();
            this.label1 = new Label();
            base.SuspendLayout();
            this.btnMarkSymbol.Location = new Point(88, 72);
            this.btnMarkSymbol.Name = "btnMarkSymbol";
            this.btnMarkSymbol.Size = new Size(96, 56);
            this.btnMarkSymbol.Style = null;
            this.btnMarkSymbol.TabIndex = 1;
            this.btnMarkSymbol.Click += new EventHandler(this.btnMarkSymbol_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 88);
            this.label1.Name = "label1";
            this.label1.Size = new Size(48, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "点符号:";
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnMarkSymbol);
            base.Name = "MarkerLineControl";
            base.Size = new Size(424, 264);
            base.Load += new EventHandler(this.MarkerLineControl_Load);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private NewSymbolButton btnMarkSymbol;
        private Label label1;
    }
}