using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmProject
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            this.ispatialReference_0 = null;
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProject));
            this.groupBox1 = new GroupBox();
            this.btnSelectIn = new SimpleButton();
            this.txtInputFeat = new TextEdit();
            this.groupBox2 = new GroupBox();
            this.pictureBox1 = new PictureBox();
            this.btnSelectOut = new SimpleButton();
            this.txtOutFeat = new TextEdit();
            this.groupBox3 = new GroupBox();
            this.btnSR = new SimpleButton();
            this.txtOutSR = new TextEdit();
            this.openFileDialog_0 = new OpenFileDialog();
            this.folderBrowserDialog_0 = new FolderBrowserDialog();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.txtInputFeat.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.txtOutFeat.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtOutSR.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnSelectIn);
            this.groupBox1.Controls.Add(this.txtInputFeat);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(376, 56);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入要素集或要素类";
            this.btnSelectIn.Image = (Image) resources.GetObject("btnSelectIn.Image");
            this.btnSelectIn.Location = new System.Drawing.Point(320, 24);
            this.btnSelectIn.Name = "btnSelectIn";
            this.btnSelectIn.Size = new Size(32, 24);
            this.btnSelectIn.TabIndex = 15;
            this.btnSelectIn.Click += new EventHandler(this.btnSelectIn_Click);
            this.txtInputFeat.EditValue = "";
            this.txtInputFeat.Location = new System.Drawing.Point(16, 24);
            this.txtInputFeat.Name = "txtInputFeat";
            this.txtInputFeat.Properties.ReadOnly = true;
            this.txtInputFeat.Size = new Size(296, 21);
            this.txtInputFeat.TabIndex = 14;
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.btnSelectOut);
            this.groupBox2.Controls.Add(this.txtOutFeat);
            this.groupBox2.Location = new System.Drawing.Point(16, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(376, 56);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出要数集或要素类";
            this.pictureBox1.Image = (Image) resources.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new System.Drawing.Point(8, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(10, 10);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.btnSelectOut.Image = (Image) resources.GetObject("btnSelectOut.Image");
            this.btnSelectOut.Location = new System.Drawing.Point(320, 24);
            this.btnSelectOut.Name = "btnSelectOut";
            this.btnSelectOut.Size = new Size(32, 24);
            this.btnSelectOut.TabIndex = 16;
            this.btnSelectOut.Click += new EventHandler(this.btnSelectOut_Click);
            this.txtOutFeat.EditValue = "";
            this.txtOutFeat.Location = new System.Drawing.Point(16, 24);
            this.txtOutFeat.Name = "txtOutFeat";
            this.txtOutFeat.Properties.ReadOnly = true;
            this.txtOutFeat.Size = new Size(296, 21);
            this.txtOutFeat.TabIndex = 15;
            this.txtOutFeat.TextChanged += new EventHandler(this.txtOutFeat_TextChanged);
            this.groupBox3.Controls.Add(this.btnSR);
            this.groupBox3.Controls.Add(this.txtOutSR);
            this.groupBox3.Location = new System.Drawing.Point(16, 152);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(376, 56);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "输出坐标系";
            this.btnSR.Image = (Image) resources.GetObject("btnSR.Image");
            this.btnSR.Location = new System.Drawing.Point(320, 20);
            this.btnSR.Name = "btnSR";
            this.btnSR.Size = new Size(32, 24);
            this.btnSR.TabIndex = 17;
            this.btnSR.Click += new EventHandler(this.btnSR_Click);
            this.txtOutSR.EditValue = "";
            this.txtOutSR.Location = new System.Drawing.Point(16, 22);
            this.txtOutSR.Name = "txtOutSR";
            this.txtOutSR.Properties.ReadOnly = true;
            this.txtOutSR.Size = new Size(296, 21);
            this.txtOutSR.TabIndex = 16;
            this.openFileDialog_0.Filter = "shp文件|*.shp";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(248, 216);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(328, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.progressBar1.Location = new System.Drawing.Point(24, 224);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(216, 16);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Visible = false;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(408, 261);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmProject";
            this.Text = "投影";
            this.groupBox1.ResumeLayout(false);
            this.txtInputFeat.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.txtOutFeat.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtOutSR.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnSelectIn;
        private SimpleButton btnSelectOut;
        private SimpleButton btnSR;
        private FolderBrowserDialog folderBrowserDialog_0;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IDataset idataset_0;
        private IFeatureProgress_Event ifeatureProgress_Event_0;
        private int int_0;
        private int int_1;
        private IWorkspace iworkspace_0;
        private OpenFileDialog openFileDialog_0;
        private PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private string string_0;
        private TextEdit txtInputFeat;
        private TextEdit txtOutFeat;
        private TextEdit txtOutSR;
    }
}