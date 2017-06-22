using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class HashLineSymbolControl
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
            this.label1 = new Label();
            this.numericUpDownAngle = new SpinEdit();
            this.btnLineSymbol = new SimpleButton();
            this.simpleButton1 = new NewSymbolButton();
            this.numericUpDownAngle.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 35);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "角度";
            int[] bits = new int[4];
            this.numericUpDownAngle.EditValue = new decimal(bits);
            this.numericUpDownAngle.Location = new Point(72, 32);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 360;
            this.numericUpDownAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 360;
            bits[3] = -2147483648;
            this.numericUpDownAngle.Properties.MinValue = new decimal(bits);
            this.numericUpDownAngle.Size = new Size(64, 23);
            this.numericUpDownAngle.TabIndex = 68;
            this.numericUpDownAngle.TextChanged += new EventHandler(this.numericUpDownAngle_ValueChanged);
            this.btnLineSymbol.Location = new Point(72, 168);
            this.btnLineSymbol.Name = "btnLineSymbol";
            this.btnLineSymbol.Size = new Size(96, 24);
            this.btnLineSymbol.TabIndex = 69;
            this.btnLineSymbol.Text = "细切线符号...";
            this.btnLineSymbol.Visible = false;
            this.btnLineSymbol.Click += new EventHandler(this.btnLineSymbol_Click);
            this.simpleButton1.Location = new Point(72, 64);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(96, 48);
            this.simpleButton1.Style = null;
            this.simpleButton1.TabIndex = 70;
            this.simpleButton1.Click += new EventHandler(this.btnLineSymbol_Click);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnLineSymbol);
            base.Controls.Add(this.numericUpDownAngle);
            base.Controls.Add(this.label1);
            base.Name = "HashLineSymbolControl";
            base.Size = new Size(360, 296);
            base.Load += new EventHandler(this.HashLineSymbolControl_Load);
            this.numericUpDownAngle.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnLineSymbol;
        private Label label1;
        private SpinEdit numericUpDownAngle;
        private NewSymbolButton simpleButton1;
    }
}