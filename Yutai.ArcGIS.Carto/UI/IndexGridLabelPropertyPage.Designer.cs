using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.SymbolLib;
using Yutai.ArcGIS.Controls.SymbolUI;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class IndexGridLabelPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.cboLabelType = new ComboBoxEdit();
            this.btnFont = new StyleButton();
            this.colorEdit1 = new ColorEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.groupBox1.SuspendLayout();
            this.cboLabelType.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboLabelType);
            this.groupBox1.Controls.Add(this.btnFont);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(256, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标签样式";
            this.cboLabelType.EditValue = "按钮制表符";
            this.cboLabelType.Location = new Point(88, 24);
            this.cboLabelType.Name = "cboLabelType";
            this.cboLabelType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboLabelType.Properties.Items.AddRange(new object[] { "按钮制表符", "背景填充", "连续的制表符", "圆形的制表符" });
            this.cboLabelType.Size = new Size(104, 23);
            this.cboLabelType.TabIndex = 5;
            this.cboLabelType.SelectedIndexChanged += new EventHandler(this.cboLabelType_SelectedIndexChanged);
            this.btnFont.Location = new Point(88, 88);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new Size(80, 24);
            this.btnFont.TabIndex = 4;
            this.btnFont.Click += new EventHandler(this.btnFont_Click);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(88, 56);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(88, 23);
            this.colorEdit1.TabIndex = 3;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(24, 96);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "符号";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(24, 56);
            this.label2.Name = "label2";
            this.label2.Size = new Size(29, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "颜色";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(42, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "标签类";
            this.groupBox2.Controls.Add(this.radioGroup1);
            this.groupBox2.Location = new Point(16, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(264, 80);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "标签配置";
            this.radioGroup1.Location = new Point(8, 16);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "列标A,B,C……,行标1,2,3……"), new RadioGroupItem(null, "列标1,2,3……,行标A,B,C……") });
            this.radioGroup1.Size = new Size(184, 56);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "IndexGridLabelPropertyPage";
            base.Size = new Size(296, 264);
            base.Load += new EventHandler(this.IndexGridLabelPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboLabelType.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private StyleButton btnFont;
        private ComboBoxEdit cboLabelType;
        private ColorEdit colorEdit1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private RadioGroup radioGroup1;
    }
}