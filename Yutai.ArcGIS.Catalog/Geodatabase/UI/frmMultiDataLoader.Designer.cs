using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmMultiDataLoader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMultiDataLoader));
            this.label1 = new Label();
            this.textEdit1 = new TextEdit();
            this.btnOpen = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.listView1 = new ListView();
            this.btnSelectInputFeatures = new SimpleButton();
            this.lblSelectObjects = new Label();
            this.panel1 = new Panel();
            this.lblFN = new Label();
            this.progressBarFN = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textEdit1.Properties.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择目标数据";
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(8, 32);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new Size(224, 21);
            this.textEdit1.TabIndex = 1;
            this.btnOpen.Image = (Image) resources.GetObject("btnOpen.Image");
            this.btnOpen.Location = new Point(256, 32);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(24, 24);
            this.btnOpen.TabIndex = 11;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            this.btnOK.Location = new Point(168, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new Point(232, 280);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(56, 24);
            this.simpleButton2.TabIndex = 13;
            this.simpleButton2.Text = "取消";
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = (Image) resources.GetObject("btnDelete.Image");
            this.btnDelete.Location = new Point(266, 135);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(24, 24);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(2, 103);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(256, 152);
            this.listView1.TabIndex = 17;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.btnSelectInputFeatures.Image = (Image) resources.GetObject("btnSelectInputFeatures.Image");
            this.btnSelectInputFeatures.Location = new Point(266, 95);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(24, 24);
            this.btnSelectInputFeatures.TabIndex = 16;
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.lblSelectObjects.AutoSize = true;
            this.lblSelectObjects.Location = new Point(2, 79);
            this.lblSelectObjects.Name = "lblSelectObjects";
            this.lblSelectObjects.Size = new Size(89, 12);
            this.lblSelectObjects.TabIndex = 15;
            this.lblSelectObjects.Text = "输入要合并数据";
            this.panel1.Controls.Add(this.lblFN);
            this.panel1.Controls.Add(this.progressBarFN);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Location = new Point(0, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(296, 264);
            this.panel1.TabIndex = 19;
            this.panel1.Visible = false;
            this.lblFN.AutoSize = true;
            this.lblFN.Location = new Point(16, 8);
            this.lblFN.Name = "lblFN";
            this.lblFN.Size = new Size(0, 12);
            this.lblFN.TabIndex = 17;
            this.progressBarFN.Location = new Point(16, 40);
            this.progressBarFN.Name = "progressBarFN";
            this.progressBarFN.Size = new Size(176, 16);
            this.progressBarFN.TabIndex = 16;
            this.progressBar1.Location = new Point(16, 88);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(176, 16);
            this.progressBar1.TabIndex = 15;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(292, 311);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.btnSelectInputFeatures);
            base.Controls.Add(this.lblSelectObjects);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.textEdit1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmMultiDataLoader";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "数据装载";
            this.textEdit1.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnDelete;
        private SimpleButton btnOK;
        private SimpleButton btnOpen;
        private SimpleButton btnSelectInputFeatures;
        private Label label1;
        private Label lblFN;
        private Label lblSelectObjects;
        private ListView listView1;
        private Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBarFN;
        private SimpleButton simpleButton2;
        private TextEdit textEdit1;
    }
}