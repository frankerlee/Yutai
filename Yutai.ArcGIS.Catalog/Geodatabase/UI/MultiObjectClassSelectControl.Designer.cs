using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class MultiObjectClassSelectControl
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiObjectClassSelectControl));
            this.lblSelectObjects = new Label();
            this.btnSelectInputFeatures = new SimpleButton();
            this.txtOutLocation = new TextEdit();
            this.btnSelectOutLocation = new SimpleButton();
            this.label2 = new Label();
            this.btnDelete = new SimpleButton();
            this.panel2 = new Panel();
            this.txtScale = new TextEdit();
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.panel1 = new Panel();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblObj = new Label();
            this.labelFeatureClass = new Label();
            this.txtOutLocation.Properties.BeginInit();
            this.panel2.SuspendLayout();
            this.txtScale.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(65, 12);
            this.lblSelectObjects.TabIndex = 0;
            this.lblSelectObjects.Text = "输入要素类";
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(272, 24);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 2;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new Point(8, 216);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtOutLocation.Properties.Appearance.ForeColor = Color.Black;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.Appearance.Options.UseForeColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(248, 21);
            this.txtOutLocation.TabIndex = 3;
            this.btnSelectOutLocation.Image = (Image) resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new Point(264, 216);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(24, 24);
            this.btnSelectOutLocation.TabIndex = 4;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 192);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "输出位置";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(272, 64);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.panel2.Controls.Add(this.txtScale);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new Point(7, 156);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(294, 34);
            this.panel2.TabIndex = 9;
            this.txtScale.EditValue = "5000";
            this.txtScale.Location = new Point(62, 6);
            this.txtScale.Name = "txtScale";
            this.txtScale.Properties.Appearance.BackColor = SystemColors.HighlightText;
            this.txtScale.Properties.Appearance.Options.UseBackColor = true;
            this.txtScale.Size = new Size(195, 21);
            this.txtScale.TabIndex = 11;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "比例尺1:";
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(7, 29);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(256, 158);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblObj);
            this.panel1.Controls.Add(this.labelFeatureClass);
            this.panel1.Location = new Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(304, 253);
            this.panel1.TabIndex = 12;
            this.panel1.Visible = false;
            this.progressBar2.Location = new Point(8, 128);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(256, 24);
            this.progressBar2.TabIndex = 3;
            this.progressBar2.Visible = false;
            this.progressBar1.Location = new Point(8, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(256, 24);
            this.progressBar1.TabIndex = 2;
            this.lblObj.AutoSize = true;
            this.lblObj.Location = new Point(8, 85);
            this.lblObj.Name = "lblObj";
            this.lblObj.Size = new Size(0, 12);
            this.lblObj.TabIndex = 1;
            this.labelFeatureClass.AutoSize = true;
            this.labelFeatureClass.Location = new Point(8, 8);
            this.labelFeatureClass.Name = "labelFeatureClass";
            this.labelFeatureClass.Size = new Size(0, 12);
            this.labelFeatureClass.TabIndex = 0;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            base.Name = "MultiObjectClassSelectControl";
            base.Size = new Size(304, 264);
            base.Load += new EventHandler(this.MultiObjectClassSelectControl_Load);
            this.txtOutLocation.Properties.EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.txtScale.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private esriDatasetType esriDatasetType_0;
        private Label label1;
        private Label label2;
        private Label labelFeatureClass;
        private Label lblObj;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private TextEdit txtOutLocation;
        private TextEdit txtScale;
    }
}