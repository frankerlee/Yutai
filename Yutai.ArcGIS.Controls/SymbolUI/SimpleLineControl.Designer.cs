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
    partial class SimpleLineControl
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
            this.numericUpDownWidth = new SpinEdit();
            this.cboStyle = new ComboBoxEdit();
            this.colorEdit1.Properties.BeginInit();
            this.numericUpDownWidth.Properties.BeginInit();
            this.cboStyle.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 28);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(24, 61);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "样式";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(24, 92);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "宽度";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(64, 24);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 23);
            this.colorEdit1.TabIndex = 6;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            int[] bits = new int[4];
            this.numericUpDownWidth.EditValue = new decimal(bits);
            this.numericUpDownWidth.Location = new Point(64, 88);
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.numericUpDownWidth.Properties.DisplayFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            this.numericUpDownWidth.Properties.EditFormat.FormatString = "0.####";
            this.numericUpDownWidth.Properties.EditFormat.FormatType = FormatType.Numeric;
            bits = new int[4];
            bits[0] = 100;
            this.numericUpDownWidth.Properties.MaxValue = new decimal(bits);
            this.numericUpDownWidth.Properties.UseCtrlIncrement = false;
            this.numericUpDownWidth.Size = new Size(112, 23);
            this.numericUpDownWidth.TabIndex = 81;
            this.numericUpDownWidth.EditValueChanged += new EventHandler(this.numericUpDownWidth_EditValueChanged);
            this.numericUpDownWidth.TextChanged += new EventHandler(this.numericUpDownWidth_ValueChanged);
            this.cboStyle.EditValue = "";
            this.cboStyle.Location = new Point(64, 56);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboStyle.Properties.Items.AddRange(new object[] { "实线", "虚线", "点线", "短线-点线", "短线-点-点线", "无" });
            this.cboStyle.Size = new Size(112, 23);
            this.cboStyle.TabIndex = 82;
            this.cboStyle.SelectedIndexChanged += new EventHandler(this.cboStyle_SelectedIndexChanged);
            base.Controls.Add(this.cboStyle);
            base.Controls.Add(this.numericUpDownWidth);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "SimpleLineControl";
            base.Size = new Size(440, 328);
            base.Load += new EventHandler(this.SimpleLineControl_Load);
            this.colorEdit1.Properties.EndInit();
            this.numericUpDownWidth.Properties.EndInit();
            this.cboStyle.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboStyle;
        private ColorEdit colorEdit1;
        private Label label1;
        private Label label2;
        private Label label3;
        private SpinEdit numericUpDownWidth;
    }
}