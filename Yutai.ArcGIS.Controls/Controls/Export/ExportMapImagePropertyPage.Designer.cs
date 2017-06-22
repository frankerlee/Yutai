using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Output;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    partial class ExportMapImagePropertyPage
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
            this.txtHeight = new SpinEdit();
            this.txtWidth = new SpinEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label4 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.cboImageType = new ComboBoxEdit();
            this.txtHeight.Properties.BeginInit();
            this.txtWidth.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.cboImageType.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(23, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "高:";
            int[] bits = new int[4];
            bits[0] = 300;
            this.txtHeight.EditValue = new decimal(bits);
            this.txtHeight.Location = new Point(40, 8);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtHeight.Size = new Size(97, 23);
            this.txtHeight.TabIndex = 1;
            this.txtHeight.EditValueChanged += new EventHandler(this.txtResolution_EditValueChanged);
            int[] bits2 = new int[4];
            bits2[0] = 1;
            this.txtWidth.EditValue = new decimal(bits2);
            this.txtWidth.Location = new Point(40, 40);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            int[] bits3 = new int[4];
            bits3[0] = 5;
            this.txtWidth.Properties.MaxValue = new decimal(bits3);
            int[] bits4 = new int[4];
            bits4[0] = 1;
            this.txtWidth.Properties.MinValue = new decimal(bits4);
          //  this.txtWidth.Properties.UseCtrlIncrement = false;
            this.txtWidth.Size = new Size(97, 23);
            this.txtWidth.TabIndex = 8;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(23, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "宽:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 80);
            this.label2.Name = "label2";
            this.label2.Size = new Size(54, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "图片类型";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 104);
            this.label4.Name = "label4";
            this.label4.Size = new Size(48, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "背景色:";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(80, 104);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 23);
            this.colorEdit1.TabIndex = 11;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.cboImageType.EditValue = "BiLevelMask";
            this.cboImageType.Location = new Point(80, 72);
            this.cboImageType.Name = "cboImageType";
            this.cboImageType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboImageType.Properties.Items.AddRange(new object[] { "BiLevelMask", "BiLevelThreshold", "Grayscale", "Indexed", "TrueColor" });
            this.cboImageType.Size = new Size(104, 23);
            this.cboImageType.TabIndex = 12;
            this.cboImageType.SelectedIndexChanged += new EventHandler(this.cboImageType_SelectedIndexChanged);
            base.Controls.Add(this.cboImageType);
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtWidth);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.txtHeight);
            base.Controls.Add(this.label1);
            base.Name = "ExportMapImagePropertyPage";
            base.Size = new Size(216, 144);
            base.Load += new EventHandler(this.ExportMapGeneralPropertyPage_Load);
            this.txtHeight.Properties.EndInit();
            this.txtWidth.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.cboImageType.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboImageType;
        private ColorEdit colorEdit1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private SpinEdit txtHeight;
        private SpinEdit txtWidth;
    }
}