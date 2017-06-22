using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class LinePropertyControl
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
            this.numericUpDownOffset = new SpinEdit();
            this.numericUpDownOffset.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "偏移";
            int[] bits = new int[4];
            this.numericUpDownOffset.EditValue = new decimal(bits);
            this.numericUpDownOffset.Location = new Point(64, 24);
            this.numericUpDownOffset.Name = "numericUpDownOffset";
            this.numericUpDownOffset.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownOffset.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownOffset.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownOffset.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownOffset.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownOffset.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 100;
            bits[3] = -2147483648;
            this.numericUpDownOffset.Properties.MinValue = new decimal(bits);
            this.numericUpDownOffset.Size = new Size(96, 23);
            this.numericUpDownOffset.TabIndex = 74;
            this.numericUpDownOffset.TextChanged += new EventHandler(this.numericUpDownOffset_ValueChanged);
            base.Controls.Add(this.numericUpDownOffset);
            base.Controls.Add(this.label1);
            base.Name = "LinePropertyControl";
            base.Size = new Size(376, 312);
            base.Load += new EventHandler(this.LinePropertyControl_Load);
            this.numericUpDownOffset.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private Label label1;
        private SpinEdit numericUpDownOffset;
    }
}