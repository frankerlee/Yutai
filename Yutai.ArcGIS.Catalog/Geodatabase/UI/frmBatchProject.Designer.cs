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
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmBatchProject
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            this.ispatialReference_0 = null;
            base.Dispose(bool_2);
        }

       
 private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchProject));
            this.groupBox2 = new GroupBox();
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
            this.btnDelete = new SimpleButton();
            this.listView1 = new ListView();
            this.btnSelectInputFeatures = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.btnInputFeatClassProperty = new SimpleButton();
            this.groupBox2.SuspendLayout();
            this.txtOutFeat.Properties.BeginInit();
            this.groupBox3.SuspendLayout();
            this.txtOutSR.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.btnSelectOut);
            this.groupBox2.Controls.Add(this.txtOutFeat);
            this.groupBox2.Location = new System.Drawing.Point(8, 200);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(328, 56);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "输出位置";
            this.btnSelectOut.ImageIndex = 0;
            this.btnSelectOut.Location = new System.Drawing.Point(280, 24);
            this.btnSelectOut.Name = "btnSelectOut";
            this.btnSelectOut.Size = new Size(32, 24);
            this.btnSelectOut.TabIndex = 16;
            this.btnSelectOut.Text = "打开";
            this.btnSelectOut.Click += new EventHandler(this.btnSelectOut_Click);
            this.txtOutFeat.EditValue = "";
            this.txtOutFeat.Location = new System.Drawing.Point(16, 24);
            this.txtOutFeat.Name = "txtOutFeat";
            this.txtOutFeat.Properties.ReadOnly = true;
            this.txtOutFeat.Size = new Size(256, 21);
            this.txtOutFeat.TabIndex = 15;
            this.txtOutFeat.TextChanged += new EventHandler(this.txtOutFeat_TextChanged);
            this.groupBox3.Controls.Add(this.btnSR);
            this.groupBox3.Controls.Add(this.txtOutSR);
            this.groupBox3.Location = new System.Drawing.Point(8, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(328, 56);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "输出坐标系";
            this.btnSR.Location = new System.Drawing.Point(280, 20);
            this.btnSR.Name = "btnSR";
            this.btnSR.Size = new Size(32, 24);
            this.btnSR.TabIndex = 17;
            this.btnSR.Text = "属性";
            this.btnSR.Click += new EventHandler(this.btnSR_Click);
            this.txtOutSR.EditValue = "";
            this.txtOutSR.Location = new System.Drawing.Point(16, 22);
            this.txtOutSR.Name = "txtOutSR";
            this.txtOutSR.Properties.ReadOnly = true;
            this.txtOutSR.Size = new Size(256, 21);
            this.txtOutSR.TabIndex = 16;
            this.openFileDialog_0.Filter = "shp文件|*.shp";
            this.btnOK.Location = new System.Drawing.Point(216, 328);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 328);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.progressBar1.Location = new System.Drawing.Point(8, 336);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(200, 16);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Visible = false;
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageIndex = 1;
            this.btnDelete.Location = new System.Drawing.Point(280, 56);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(32, 24);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(13, 20);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(256, 152);
            this.listView1.TabIndex = 17;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.List;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.btnSelectInputFeatures.ImageIndex = 0;
            this.btnSelectInputFeatures.Location = new System.Drawing.Point(280, 16);
            this.btnSelectInputFeatures.Name = "btnSelectInputFeatures";
            this.btnSelectInputFeatures.Size = new Size(32, 24);
            this.btnSelectInputFeatures.TabIndex = 16;
            this.btnSelectInputFeatures.Text = "添加";
            this.btnSelectInputFeatures.Click += new EventHandler(this.btnSelectInputFeatures_Click);
            this.groupBox1.Controls.Add(this.btnInputFeatClassProperty);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.listView1);
            this.groupBox1.Controls.Add(this.btnSelectInputFeatures);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(328, 184);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输入要素集或要素类";
            this.btnInputFeatClassProperty.Enabled = false;
            this.btnInputFeatClassProperty.Location = new System.Drawing.Point(280, 96);
            this.btnInputFeatClassProperty.Name = "btnInputFeatClassProperty";
            this.btnInputFeatClassProperty.Size = new Size(32, 24);
            this.btnInputFeatClassProperty.TabIndex = 19;
            this.btnInputFeatClassProperty.Text = "属性";
            this.btnInputFeatClassProperty.Click += new EventHandler(this.btnInputFeatClassProperty_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(354, 372);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmBatchProject";
            this.Text = "投影";
            this.groupBox2.ResumeLayout(false);
            this.txtOutFeat.Properties.EndInit();
            this.groupBox3.ResumeLayout(false);
            this.txtOutSR.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnDelete;
        private SimpleButton btnInputFeatClassProperty;
        private SimpleButton btnOK;
        private SimpleButton btnSelectInputFeatures;
        private SimpleButton btnSelectOut;
        private SimpleButton btnSR;
        private FolderBrowserDialog folderBrowserDialog_0;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private IContainer icontainer_0;
        private IDataset idataset_0;
        private IFeatureProgress_Event ifeatureProgress_Event_0;
        private int int_0;
        private int int_1;
        private IWorkspace iworkspace_0;
        private ListView listView1;
        private OpenFileDialog openFileDialog_0;
        private System.Windows.Forms.ProgressBar progressBar1;
        private string string_0;
        private TextEdit txtOutFeat;
        private TextEdit txtOutSR;
    }
}