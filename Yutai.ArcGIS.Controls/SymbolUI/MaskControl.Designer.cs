using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class MaskControl
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
            this.groupBox1 = new GroupBox();
            this.radMaskStyle = new RadioGroup();
            this.label1 = new Label();
            this.numericUpDownSize = new SpinEdit();
            this.btnFillSymbol = new NewSymbolButton();
            this.groupBox1.SuspendLayout();
            this.radMaskStyle.Properties.BeginInit();
            this.numericUpDownSize.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radMaskStyle);
            this.groupBox1.Location = new Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(80, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "样式";
            this.radMaskStyle.Location = new Point(8, 16);
            this.radMaskStyle.Name = "radMaskStyle";
            this.radMaskStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radMaskStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radMaskStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radMaskStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "无"), new RadioGroupItem(null, "中空") });
            this.radMaskStyle.Size = new Size(56, 48);
            this.radMaskStyle.TabIndex = 2;
            this.radMaskStyle.SelectedIndexChanged += new EventHandler(this.radMaskStyle_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 123);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "大小";
            int[] bits = new int[4];
            this.numericUpDownSize.EditValue = new decimal(bits);
            this.numericUpDownSize.Location = new Point(56, 120);
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownSize.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownSize.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownSize.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownSize.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownSize.Properties.MaxValue = new decimal(bits);
            this.numericUpDownSize.Properties.UseCtrlIncrement = false;
            this.numericUpDownSize.Size = new Size(88, 23);
            this.numericUpDownSize.TabIndex = 4;
            this.numericUpDownSize.TextChanged += new EventHandler(this.numericUpDownSize_ValueChanged);
            this.btnFillSymbol.Location = new Point(152, 120);
            this.btnFillSymbol.Name = "btnFillSymbol";
            this.btnFillSymbol.Size = new Size(104, 40);
            this.btnFillSymbol.Style = null;
            this.btnFillSymbol.TabIndex = 5;
            this.btnFillSymbol.Click += new EventHandler(this.btnFillSymbol_Click);
            base.Controls.Add(this.btnFillSymbol);
            base.Controls.Add(this.numericUpDownSize);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.Name = "MaskControl";
            base.Size = new Size(352, 240);
            base.Load += new EventHandler(this.MaskControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.radMaskStyle.Properties.EndInit();
            this.numericUpDownSize.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private NewSymbolButton btnFillSymbol;
        private GroupBox groupBox1;
        private Label label1;
        private SpinEdit numericUpDownSize;
        private RadioGroup radMaskStyle;
    }
}