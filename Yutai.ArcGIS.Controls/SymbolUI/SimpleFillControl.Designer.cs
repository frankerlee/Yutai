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
    partial class SimpleFillControl
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
            this.colorEdit1 = new ColorEdit();
            this.colorEditOutline = new ColorEdit();
            this.numericUpDownWidth = new SpinEdit();
            this.btnOutline = new NewSymbolButton();
            this.label5 = new Label();
            this.cboStyle = new ComboBoxEdit();
            this.label4 = new Label();
            this.colorEdit1.Properties.BeginInit();
            this.colorEditOutline.Properties.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            this.cboStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 55);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(16, 79);
            this.label2.Name = "label2";
            this.label2.Size = new Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "轮廓线颜色";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(16, 111);
            this.label3.Name = "label3";
            this.label3.Size = new Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "轮廓线宽";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(88, 47);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 21);
            this.colorEdit1.TabIndex = 4;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.colorEditOutline.EditValue = Color.Empty;
            this.colorEditOutline.Location = new Point(88, 71);
            this.colorEditOutline.Name = "colorEditOutline";
            this.colorEditOutline.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEditOutline.Size = new Size(48, 21);
            this.colorEditOutline.TabIndex = 5;
            this.colorEditOutline.EditValueChanged += new EventHandler(this.colorEditOutline_EditValueChanged);
            int[] bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new Point(88, 103);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits);
            this.numericUpDownWidth.Size = new Size(64, 21);
            this.numericUpDownWidth.TabIndex = 71;
            this.numericUpDownWidth.EditValueChanged += new EventHandler(this.numericUpDownWidth_EditValueChanged);
            this.numericUpDownWidth.TextChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
            this.btnOutline.Location = new Point(88, 135);
            this.btnOutline.Name = "btnOutline";
            this.btnOutline.Size = new Size(88, 40);
            this.btnOutline.Style = null;
            this.btnOutline.TabIndex = 79;
            this.btnOutline.Click += new EventHandler(this.btnOutline_Click);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(16, 151);
            this.label5.Name = "label5";
            this.label5.Size = new Size(71, 12);
            this.label5.TabIndex = 80;
            this.label5.Text = "轮廓线符号:";
            this.cboStyle.EditValue = "颜色填充";
            this.cboStyle.Location = new Point(88, 17);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.SelectedIndexChanged += new EventHandler(this.cboStyle_SelectedIndexChanged);
            this.cboStyle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboStyle.Properties.Items.AddRange(new object[] { "颜色填充", "不填充", "水平线填充", "竖直线填充", "45度下斜线填充", "45度上斜线填充", "十字丝填充", "X填充" });
            this.cboStyle.Size = new Size(80, 21);
            this.cboStyle.TabIndex = 90;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(16, 22);
            this.label4.Name = "label4";
            this.label4.Size = new Size(29, 12);
            this.label4.TabIndex = 89;
            this.label4.Text = "样式";
            base.Controls.Add(this.cboStyle);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.btnOutline);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.colorEditOutline);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "SimpleFillControl";
            base.Size = new Size(376, 232);
            base.Load += new EventHandler(this.SimpleFillControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.colorEditOutline.Properties.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            this.cboStyle.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private NewSymbolButton btnOutline;
        private ComboBoxEdit cboStyle;
        private ColorEdit colorEdit1;
        private ColorEdit colorEditOutline;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private SpinEdit numericUpDownWidth;
    }
}