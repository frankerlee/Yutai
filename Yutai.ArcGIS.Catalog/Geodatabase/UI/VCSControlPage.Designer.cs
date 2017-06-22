using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class VCSControlPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VCSControlPage));
            this.treeView1 = new TreeView();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.label2 = new Label();
            this.textBoxName = new TextEdit();
            this.label1 = new Label();
            this.btnImport = new SimpleButton();
            this.btnModify = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.textBoxName.Properties.BeginInit();
            base.SuspendLayout();
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList_0;
            this.treeView1.Location = new System.Drawing.Point(21, 88);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(245, 184);
            this.treeView1.TabIndex = 33;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "ProjectFolder.bmp");
            this.imageList_0.Images.SetKeyName(1, "SelectedProjectFolder.bmp");
            this.imageList_0.Images.SetKeyName(2, "Project.bmp");
            this.label2.AllowDrop = true;
            this.label2.Location = new System.Drawing.Point(19, 13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(309, 24);
            this.label2.TabIndex = 32;
            this.label2.Text = "选择数据集Z轴的坐标系统";
            this.textBoxName.EditValue = "";
            this.textBoxName.Location = new System.Drawing.Point(60, 52);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Properties.ReadOnly = true;
            this.textBoxName.Size = new Size(216, 21);
            this.textBoxName.TabIndex = 31;
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 54);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 30;
            this.label1.Text = "名字:";
            this.btnImport.Location = new System.Drawing.Point(272, 88);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new Size(56, 24);
            this.btnImport.TabIndex = 36;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new EventHandler(this.btnImport_Click);
            this.btnModify.Location = new System.Drawing.Point(272, 152);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(56, 24);
            this.btnModify.TabIndex = 35;
            this.btnModify.Text = "修改...";
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnNew.Location = new System.Drawing.Point(272, 120);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(56, 24);
            this.btnNew.TabIndex = 34;
            this.btnNew.Text = "新建...";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnImport);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBoxName);
            base.Controls.Add(this.label1);
            base.Name = "VCSControlPage";
            base.Size = new Size(346, 358);
            base.Load += new EventHandler(this.VCSControlPage_Load);
            this.textBoxName.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnImport;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private ImageList imageList_0;
        private Label label1;
        private Label label2;
        private TextEdit textBoxName;
        private TreeView treeView1;
    }
}