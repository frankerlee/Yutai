using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ExportToTABControl
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportToTABControl));
            this.lblSelectObjects = new Label();
            this.btnSelectInputFeatures = new SimpleButton();
            this.txtOutLocation = new TextEdit();
            this.btnSelectOutLocation = new SimpleButton();
            this.label2 = new Label();
            this.listView1 = new ListView();
            this.btnDelete = new SimpleButton();
            this.cboType = new ComboBoxEdit();
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblObj = new Label();
            this.labelFeatureClass = new Label();
            this.txtOutLocation.Properties.BeginInit();
            this.cboType.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(8, 8);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(66, 17);
            this.lblSelectObjects.TabIndex = 0;
            this.lblSelectObjects.Text = "输入要素类";
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(272, 24);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 2;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.txtOutLocation.EditValue = @"c:\";
            this.txtOutLocation.Location = new Point(8, 216);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.InactiveCaptionText;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(248, 23);
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
            this.label2.Size = new Size(54, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "输出位置";
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(8, 32);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(256, 120);
            this.listView1.TabIndex = 6;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(272, 64);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.cboType.EditValue = "TAB";
            this.cboType.Location = new Point(72, 160);
            this.cboType.Name = "cboType";
            this.cboType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboType.Properties.Items.AddRange(new object[] { "TAB", "MIF" });
            this.cboType.Size = new Size(192, 23);
            this.cboType.TabIndex = 9;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 162);
            this.label1.Name = "label1";
            this.label1.Size = new Size(54, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "输出类型";
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lblObj);
            this.panel1.Controls.Add(this.labelFeatureClass);
            this.panel1.Location = new Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(304, 240);
            this.panel1.TabIndex = 11;
            this.panel1.Visible = false;
            this.progressBar2.Location = new Point(8, 128);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(256, 24);
            this.progressBar2.TabIndex = 3;
            this.progressBar1.Location = new Point(8, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(256, 24);
            this.progressBar1.TabIndex = 2;
            this.lblObj.AutoSize = true;
            this.lblObj.Location = new Point(8, 85);
            this.lblObj.Name = "lblObj";
            this.lblObj.Size = new Size(0, 17);
            this.lblObj.TabIndex = 1;
            this.labelFeatureClass.AutoSize = true;
            this.labelFeatureClass.Location = new Point(8, 8);
            this.labelFeatureClass.Name = "labelFeatureClass";
            this.labelFeatureClass.Size = new Size(0, 17);
            this.labelFeatureClass.TabIndex = 0;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cboType);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.txtOutLocation);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            base.Name = "ExportToTABControl";
            base.Size = new Size(304, 264);
            base.Load += new EventHandler(this.ExportToTABControl_Load);
            this.txtOutLocation.Properties.EndInit();
            this.cboType.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnDelete;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOutLocation;
        private ComboBoxEdit cboType;
        private Label label1;
        private Label label2;
        private Label labelFeatureClass;
        private Label lblObj;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private TextEdit txtOutLocation;
    }
}