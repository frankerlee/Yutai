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
    partial class CartoLineControl
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
            this.colorEdit1 = new ColorEdit();
            this.label2 = new Label();
            this.groupBox1 = new GroupBox();
            this.radioGroupLineCapStyle = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.radioGroupLineJoinStyle = new RadioGroup();
            this.numericUpDownWidth = new SpinEdit();
            this.colorEdit1.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.radioGroupLineCapStyle.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.radioGroupLineJoinStyle.Properties.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(56, 24);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 23);
            this.colorEdit1.TabIndex = 1;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 58);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "宽度";
            this.groupBox1.Controls.Add(this.radioGroupLineCapStyle);
            this.groupBox1.Location = new Point(16, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(80, 96);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "线头";
            this.radioGroupLineCapStyle.Location = new Point(12, 16);
            this.radioGroupLineCapStyle.Name = "radioGroupLineCapStyle";
            this.radioGroupLineCapStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupLineCapStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupLineCapStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupLineCapStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "平头"), new RadioGroupItem(null, "圆头"), new RadioGroupItem(null, "方头") });
            this.radioGroupLineCapStyle.Size = new Size(56, 64);
            this.radioGroupLineCapStyle.TabIndex = 50;
            this.radioGroupLineCapStyle.SelectedIndexChanged += new EventHandler(this.radioGroupLineCapStyle_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.radioGroupLineJoinStyle);
            this.groupBox2.Location = new Point(128, 96);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(80, 96);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "线连接";
            this.radioGroupLineJoinStyle.Location = new Point(12, 16);
            this.radioGroupLineJoinStyle.Name = "radioGroupLineJoinStyle";
            this.radioGroupLineJoinStyle.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroupLineJoinStyle.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroupLineJoinStyle.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroupLineJoinStyle.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "平头"), new RadioGroupItem(null, "圆头"), new RadioGroupItem(null, "方头") });
            this.radioGroupLineJoinStyle.Size = new Size(56, 64);
            this.radioGroupLineJoinStyle.TabIndex = 51;
            this.radioGroupLineJoinStyle.SelectedIndexChanged += new EventHandler(this.radioGroupLineJoinStyle_SelectedIndexChanged);
            int[] bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new Point(56, 56);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            int[] bits2 = new int[4];
            bits2[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits2);
            this.numericUpDownWidth.Properties.UseCtrlIncrement = false;
            this.numericUpDownWidth.Size = new Size(80, 23);
            this.numericUpDownWidth.TabIndex = 48;
            this.numericUpDownWidth.TextChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label1);
            base.Name = "CartoLineControl";
            base.Size = new Size(288, 232);
            base.Load += new EventHandler(this.CartoLineControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.radioGroupLineCapStyle.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.radioGroupLineJoinStyle.Properties.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ColorEdit colorEdit1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private SpinEdit numericUpDownWidth;
        private RadioGroup radioGroupLineCapStyle;
        private RadioGroup radioGroupLineJoinStyle;
    }
}