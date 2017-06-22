using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class LineFillControl
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
            this.colorEdit1 = new ColorEdit();
            this.label1 = new Label();
            this.label7 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.numericUpDownAngle = new SpinEdit();
            this.numericUpDownOffset = new SpinEdit();
            this.numericUpDownSpace = new SpinEdit();
            this.btnFillLine = new NewSymbolButton();
            this.btnOutline = new NewSymbolButton();
            this.label4 = new Label();
            this.label5 = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.numericUpDownAngle.Properties.BeginInit();
            this.numericUpDownOffset.Properties.BeginInit();
            this.numericUpDownSpace.Properties.BeginInit();
            base.SuspendLayout();
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(56, 24);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 23);
            this.colorEdit1.TabIndex = 8;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "颜色";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(16, 56);
            this.label7.Name = "label7";
            this.label7.Size = new Size(29, 17);
            this.label7.TabIndex = 44;
            this.label7.Text = "角度";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 88);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 46;
            this.label2.Text = "偏移";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 120);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 17);
            this.label3.TabIndex = 48;
            this.label3.Text = "间隔";
            int[] bits = new int[4];
            this.numericUpDownAngle.EditValue = new decimal(bits);
            this.numericUpDownAngle.Location = new Point(56, 56);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownAngle.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownAngle.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownAngle.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownAngle.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 360;
            this.numericUpDownAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 360;
            bits[3] = -2147483648;
            this.numericUpDownAngle.Properties.MinValue = new decimal(bits);
            this.numericUpDownAngle.Properties.UseCtrlIncrement = false;
            this.numericUpDownAngle.Size = new Size(64, 23);
            this.numericUpDownAngle.TabIndex = 72;
            this.numericUpDownAngle.TextChanged += new EventHandler(this.numericUpDownAngle_ValueChanged);
            bits = new int[4];
            this.numericUpDownOffset.EditValue = new decimal(bits);
            this.numericUpDownOffset.Location = new Point(56, 88);
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
            this.numericUpDownOffset.Properties.UseCtrlIncrement = false;
            this.numericUpDownOffset.Size = new Size(64, 23);
            this.numericUpDownOffset.TabIndex = 73;
            this.numericUpDownOffset.TextChanged += new EventHandler(this.numericUpDownOffset_ValueChanged);
            bits = new int[4];
            this.numericUpDownSpace.EditValue = new decimal(bits);
            this.numericUpDownSpace.Location = new Point(56, 120);
            this.numericUpDownSpace.Name = "numericUpDownSpace";
            this.numericUpDownSpace.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownSpace.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownSpace.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownSpace.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownSpace.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownSpace.Properties.MaxValue = new decimal(bits);
            this.numericUpDownSpace.Properties.UseCtrlIncrement = false;
            this.numericUpDownSpace.Size = new Size(64, 23);
            this.numericUpDownSpace.TabIndex = 74;
            this.numericUpDownSpace.TextChanged += new EventHandler(this.numericUpDownSpace_ValueChanged);
            this.btnFillLine.Location = new Point(224, 24);
            this.btnFillLine.Name = "btnFillLine";
            this.btnFillLine.Size = new Size(88, 32);
            this.btnFillLine.Style = null;
            this.btnFillLine.TabIndex = 75;
            this.btnFillLine.Click += new EventHandler(this.btnFillLine_Click);
            this.btnOutline.Location = new Point(224, 80);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new Size(88, 32);
            this.btnOutline.Style = null;
            this.btnOutline.TabIndex = 76;
            this.btnOutline.Click += new EventHandler(this.btnOutline_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(144, 32);
            this.label4.Name = "label4";
            this.label4.Size = new Size(72, 17);
            this.label4.TabIndex = 77;
            this.label4.Text = "填充线符号:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(144, 88);
            this.label5.Name = "label5";
            this.label5.Size = new Size(72, 17);
            this.label5.TabIndex = 78;
            this.label5.Text = "轮廓线符号:";
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.btnOutline);
            base.Controls.Add(this.btnFillLine);
            base.Controls.Add(this.numericUpDownSpace);
            base.Controls.Add(this.numericUpDownOffset);
            base.Controls.Add(this.numericUpDownAngle);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label1);
            base.Name = "LineFillControl";
            base.Size = new Size(344, 232);
            base.Load += new EventHandler(this.LineFillControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.numericUpDownAngle.Properties.EndInit();
            this.numericUpDownOffset.Properties.EndInit();
            this.numericUpDownSpace.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private NewSymbolButton btnFillLine;
        private NewSymbolButton btnOutline;
        private ColorEdit colorEdit1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private SpinEdit numericUpDownAngle;
        private SpinEdit numericUpDownOffset;
        private SpinEdit numericUpDownSpace;
    }
}