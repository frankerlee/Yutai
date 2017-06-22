using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Common.SymbolUI;

namespace Yutai.ArcGIS.Controls.SymbolUI
{
    partial class GradientFillControl
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
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.colorRampComboBox = new ColorRampComboBox();
            this.numericUpDownIntervalCount = new SpinEdit();
            this.numericUpDownAngle = new SpinEdit();
            this.numericUpDownPrecent = new SpinEdit();
            this.cboGradientFillStyle = new ComboBoxEdit();
            this.label6 = new Label();
            this.btnOutline = new NewSymbolButton();
            this.numericUpDownIntervalCount.Properties.BeginInit();
            this.numericUpDownAngle.Properties.BeginInit();
            this.numericUpDownPrecent.Properties.BeginInit();
            this.cboGradientFillStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 44);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "间隔";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 75);
            this.label2.Name = "label2";
            this.label2.Size = new Size(42, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "百分比";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 106);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "角度";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(184, 44);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "样式";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(184, 106);
            this.label5.Name = "label5";
            this.label5.Size = new Size(54, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "颜色梯度";
            this.colorRampComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            this.colorRampComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.colorRampComboBox.Location = new Point(248, 104);
            this.colorRampComboBox.Name = "colorRampComboBox";
            this.colorRampComboBox.Size = new Size(120, 22);
            this.colorRampComboBox.TabIndex = 9;
            this.colorRampComboBox.SelectedIndexChanged += new EventHandler(this.colorRampComboBox_SelectedIndexChanged);
            int[] bits = new int[4];
            bits[0] = 2;
            this.numericUpDownIntervalCount.EditValue = new decimal(bits);
            this.numericUpDownIntervalCount.Location = new Point(64, 40);
            this.numericUpDownIntervalCount.Name = "numericUpDownIntervalCount";
            this.numericUpDownIntervalCount.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 500;
            this.numericUpDownIntervalCount.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 2;
            this.numericUpDownIntervalCount.Properties.MinValue = new decimal(bits);
            this.numericUpDownIntervalCount.Properties.UseCtrlIncrement = false;
            this.numericUpDownIntervalCount.Size = new Size(64, 23);
            this.numericUpDownIntervalCount.TabIndex = 72;
            this.numericUpDownIntervalCount.TextChanged += new EventHandler(this.numericUpDownIntervalCount_ValueChanged);
            bits = new int[4];
            this.numericUpDownAngle.EditValue = new decimal(bits);
            this.numericUpDownAngle.Location = new Point(64, 104);
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 360;
            this.numericUpDownAngle.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 360;
            bits[3] = -2147483648;
            this.numericUpDownAngle.Properties.MinValue = new decimal(bits);
            this.numericUpDownAngle.Properties.UseCtrlIncrement = false;
            this.numericUpDownAngle.Size = new Size(64, 23);
            this.numericUpDownAngle.TabIndex = 71;
            this.numericUpDownAngle.TextChanged += new EventHandler(this.numericUpDownAngle_ValueChanged);
            bits = new int[4];
            this.numericUpDownPrecent.EditValue = new decimal(bits);
            this.numericUpDownPrecent.Location = new Point(64, 72);
            this.numericUpDownPrecent.Name = "numericUpDownPrecent";
            this.numericUpDownPrecent.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownPrecent.Properties.MaxValue = new decimal(bits);
            this.numericUpDownPrecent.Properties.UseCtrlIncrement = false;
            this.numericUpDownPrecent.Size = new Size(64, 23);
            this.numericUpDownPrecent.TabIndex = 70;
            this.numericUpDownPrecent.TextChanged += new EventHandler(this.numericUpDownPrecent_ValueChanged);
            this.cboGradientFillStyle.EditValue = "线性";
            this.cboGradientFillStyle.Location = new Point(248, 40);
            this.cboGradientFillStyle.Name = "cboGradientFillStyle";
            this.cboGradientFillStyle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboGradientFillStyle.Properties.Items.AddRange(new object[] { "线性", "方形", "圆形", "缓冲区" });
            this.cboGradientFillStyle.Size = new Size(120, 23);
            this.cboGradientFillStyle.TabIndex = 73;
            this.cboGradientFillStyle.SelectedIndexChanged += new EventHandler(this.cboGradientFillStyle_SelectedIndexChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(184, 144);
            this.label6.Name = "label6";
            this.label6.Size = new Size(72, 17);
            this.label6.TabIndex = 80;
            this.label6.Text = "轮廓线符号:";
            this.btnOutline.Location = new Point(256, 136);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new Size(112, 32);
            this.btnOutline.Style = null;
            this.btnOutline.TabIndex = 79;
            this.btnOutline.Click += new EventHandler(this.btnOutline_Click);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.btnOutline);
            base.Controls.Add(this.cboGradientFillStyle);
            base.Controls.Add(this.numericUpDownIntervalCount);
            base.Controls.Add(this.numericUpDownAngle);
            base.Controls.Add(this.numericUpDownPrecent);
            base.Controls.Add(this.colorRampComboBox);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "GradientFillControl";
            base.Size = new Size(400, 288);
            base.Load += new EventHandler(this.GradientFillControl1_Load);
            this.numericUpDownIntervalCount.Properties.EndInit();
            this.numericUpDownAngle.Properties.EndInit();
            this.numericUpDownPrecent.Properties.EndInit();
            this.cboGradientFillStyle.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private NewSymbolButton btnOutline;
        private ComboBoxEdit cboGradientFillStyle;
        private ColorRampComboBox colorRampComboBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private SpinEdit numericUpDownAngle;
        private SpinEdit numericUpDownIntervalCount;
        private SpinEdit numericUpDownPrecent;
    }
}