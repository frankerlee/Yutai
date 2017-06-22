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
    partial class ExportMap2GIFPropertyPage
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
            this.cboImageCompression = new ComboBoxEdit();
            this.colorEdit1 = new ColorEdit();
            this.label4 = new Label();
            this.cboImageCompression.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "图像压缩:";
            this.cboImageCompression.EditValue = "无";
            this.cboImageCompression.Location = new Point(96, 16);
            this.cboImageCompression.Name = "cboImageCompression";
            this.cboImageCompression.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboImageCompression.Properties.Items.AddRange(new object[] { "无", "RLE", "LZW" });
            this.cboImageCompression.Size = new Size(96, 23);
            this.cboImageCompression.TabIndex = 10;
            this.cboImageCompression.SelectedIndexChanged += new EventHandler(this.cboImageCompression_SelectedIndexChanged);
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new Point(96, 64);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(48, 23);
            this.colorEdit1.TabIndex = 13;
            this.colorEdit1.EditValueChanged += new EventHandler(this.colorEdit1_EditValueChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(8, 64);
            this.label4.Name = "label4";
            this.label4.Size = new Size(48, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "透明色:";
            base.Controls.Add(this.colorEdit1);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboImageCompression);
            base.Controls.Add(this.label1);
            base.Name = "ExportMap2GIFPropertyPage";
            base.Size = new Size(216, 184);
            base.Load += new EventHandler(this.ExportMap2EMFPropertyPage_Load);
            this.cboImageCompression.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private ComboBoxEdit cboImageCompression;
        private ColorEdit colorEdit1;
        private Label label1;
        private Label label4;
    }
}