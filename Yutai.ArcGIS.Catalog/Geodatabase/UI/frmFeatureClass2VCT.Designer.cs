using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmFeatureClass2VCT
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFeatureClass2VCT));
            this.btnDelete = new SimpleButton();
            this.listView1 = new ListView();
            this.label2 = new Label();
            this.txtOutLocation = new TextEdit();
            this.btnSelectInputFeatures = new SimpleButton();
            this.lblSelectObjects = new Label();
            this.btnSelectOutLocation = new SimpleButton();
            this.panel1 = new Panel();
            this.labelFN = new Label();
            this.labelFileName = new Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.simpleButton1 = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.txtOutLocation.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new System.Drawing.Point(272, 64);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 13;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(8, 32);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(256, 152);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 192);
            this.label2.Name = "label2";
            this.label2.Size = new Size(71, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "输出VCT文件";
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new System.Drawing.Point(8, 216);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.InactiveCaptionText;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(248, 21);
            this.txtOutLocation.TabIndex = 10;
            this.btnSelectInputFeatures.Image = (Image)resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new System.Drawing.Point(272, 24);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 9;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new System.Drawing.Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(53, 12);
            this.lblSelectObjects.TabIndex = 8;
            this.lblSelectObjects.Text = "选择要素";
            this.btnSelectOutLocation.Image = (Image)resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new System.Drawing.Point(272, 216);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(24, 24);
            this.btnSelectOutLocation.TabIndex = 14;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.panel1.Controls.Add(this.labelFN);
            this.panel1.Controls.Add(this.labelFileName);
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(352, 242);
            this.panel1.TabIndex = 16;
            this.panel1.Visible = false;
            this.labelFN.AutoSize = true;
            this.labelFN.Location = new System.Drawing.Point(24, 64);
            this.labelFN.Name = "labelFN";
            this.labelFN.Size = new Size(0, 12);
            this.labelFN.TabIndex = 19;
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(24, 24);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new Size(0, 12);
            this.labelFileName.TabIndex = 18;
            this.progressBar2.Location = new System.Drawing.Point(24, 88);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(256, 24);
            this.progressBar2.TabIndex = 16;
            this.simpleButton1.Location = new System.Drawing.Point(168, 256);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(72, 24);
            this.simpleButton1.TabIndex = 17;
            this.simpleButton1.Text = "转换";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(248, 256);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(80, 24);
            this.simpleButton2.TabIndex = 18;
            this.simpleButton2.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(368, 293);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.lblSelectObjects);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmFeatureClass2VCT";
            this.Text = "导出VCT数据";
            this.txtOutLocation.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }


        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private Label label2;
        private Label labelFileName;
        private Label labelFN;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private TextEdit txtOutLocation;
    }
}