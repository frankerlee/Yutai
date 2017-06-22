using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmBatchReconcile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatchReconcile));
            this.groupBox2 = new GroupBox();
            this.chkDeleteOnPost = new CheckEdit();
            this.chkPost = new CheckEdit();
            this.btnRefresh = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.VersionTreeView = new TreeView();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.btnStart = new SimpleButton();
            this.btnClose = new SimpleButton();
            this.txtMessage = new TextEdit();
            this.groupBox2.SuspendLayout();
            this.chkDeleteOnPost.Properties.BeginInit();
            this.chkPost.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.txtMessage.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.chkDeleteOnPost);
            this.groupBox2.Controls.Add(this.chkPost);
            this.groupBox2.Location = new Point(8, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(224, 72);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选项";
            this.chkDeleteOnPost.Location = new Point(16, 43);
            this.chkDeleteOnPost.Name = "chkDeleteOnPost";
            this.chkDeleteOnPost.Properties.Caption = "合并后删除子版本";
            this.chkDeleteOnPost.Size = new Size(128, 19);
            this.chkDeleteOnPost.TabIndex = 1;
            this.chkPost.Location = new Point(16, 17);
            this.chkPost.Name = "chkPost";
            this.chkPost.Properties.Caption = "统一子版本到母版本";
            this.chkPost.Size = new Size(136, 19);
            this.chkPost.TabIndex = 0;
            this.btnRefresh.Location = new Point(152, 215);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(72, 24);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "刷新版本树";
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            this.groupBox1.Controls.Add(this.VersionTreeView);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(224, 200);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择父版本";
            this.VersionTreeView.ImageIndex = 0;
            this.VersionTreeView.ImageList = this.imageList_0;
            this.VersionTreeView.Location = new Point(8, 24);
            this.VersionTreeView.Name = "VersionTreeView";
            this.VersionTreeView.SelectedImageIndex = 0;
            this.VersionTreeView.Size = new Size(208, 168);
            this.VersionTreeView.TabIndex = 0;
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "");
            this.imageList_0.Images.SetKeyName(1, "");
            this.imageList_0.Images.SetKeyName(2, "");
            this.btnStart.Location = new Point(80, 320);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(80, 24);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "开始合并";
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new Point(168, 320);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(64, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.txtMessage.EditValue = "";
            this.txtMessage.Location = new Point(8, 352);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.txtMessage.Properties.Appearance.Options.UseBackColor = true;
            this.txtMessage.Properties.ReadOnly = true;
            this.txtMessage.Size = new Size(224, 21);
            this.txtMessage.TabIndex = 8;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(240, 381);
            base.Controls.Add(this.txtMessage);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnStart);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.btnRefresh);
            base.Controls.Add(this.groupBox1);
            
            base.Name = "frmBatchReconcile";
            this.Text = "批量提交";
            base.Load += new EventHandler(this.frmBatchReconcile_Load);
            this.groupBox2.ResumeLayout(false);
            this.chkDeleteOnPost.Properties.EndInit();
            this.chkPost.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.txtMessage.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnClose;
        private SimpleButton btnRefresh;
        private SimpleButton btnStart;
        private CheckEdit chkDeleteOnPost;
        private CheckEdit chkPost;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private IVersionedWorkspace iversionedWorkspace_0;
        private TextEdit txtMessage;
        private TreeView VersionTreeView;
    }
}