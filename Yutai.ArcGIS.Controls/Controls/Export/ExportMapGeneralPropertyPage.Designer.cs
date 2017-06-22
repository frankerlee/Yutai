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
    partial class ExportMapGeneralPropertyPage
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
            this.txtResolution = new SpinEdit();
            this.label2 = new Label();
            this.panel1 = new Panel();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtResampleRatio = new SpinEdit();
            this.txtResolution.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.txtResampleRatio.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(48, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "分辨率:";
            int[] bits = new int[4];
            bits[0] = 300;
            this.txtResolution.EditValue = new decimal(bits);
            this.txtResolution.Location = new Point(80, 8);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.txtResolution.Size = new Size(97, 23);
            this.txtResolution.TabIndex = 1;
            this.txtResolution.EditValueChanged += new EventHandler(this.txtResolution_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(184, 8);
            this.label2.Name = "label2";
            this.label2.Size = new Size(23, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "dpi";
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtResampleRatio);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(216, 48);
            this.panel1.TabIndex = 6;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new Size(72, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "重采样比率:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(80, 16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "1:";
            bits = new int[4];
            bits[0] = 1;
            this.txtResampleRatio.EditValue = new decimal(bits);
            this.txtResampleRatio.Location = new Point(97, 12);
            this.txtResampleRatio.Name = "txtResampleRatio";
            this.txtResampleRatio.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            bits = new int[4];
            bits[0] = 5;
            this.txtResampleRatio.Properties.MaxValue = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            this.txtResampleRatio.Properties.MinValue = new decimal(bits);
            this.txtResampleRatio.Properties.UseCtrlIncrement = false;
            this.txtResampleRatio.Size = new Size(80, 23);
            this.txtResampleRatio.TabIndex = 6;
            this.txtResampleRatio.EditValueChanged += new EventHandler(this.txtResampleRatio_EditValueChanged);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtResolution);
            base.Controls.Add(this.label1);
            base.Name = "ExportMapGeneralPropertyPage";
            base.Size = new Size(216, 144);
            base.Load += new EventHandler(this.ExportMapGeneralPropertyPage_Load);
            this.txtResolution.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.txtResampleRatio.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private SpinEdit txtResampleRatio;
        private SpinEdit txtResolution;
    }
}