using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmVersions
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersions));
            this.treeView1 = new TreeView();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.treeView1.HideSelection = false;
            this.treeView1.ImageList = this.imageList_0;
            this.treeView1.Location = new Point(16, 16);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new Size(200, 168);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.imageList_0.ImageSize = new Size(16, 16);
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(72, 192);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(144, 192);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(72, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(232, 221);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.treeView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.Name = "frmVersions";
            this.Text = "改变版本";
            base.Load += new EventHandler(this.frmVersions_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private IContainer icontainer_0;
        private IGraphicsContainer igraphicsContainer_0;
        private ImageList imageList_0;
        private string string_0;
        private string string_1;
        private TreeView treeView1;
    }
}