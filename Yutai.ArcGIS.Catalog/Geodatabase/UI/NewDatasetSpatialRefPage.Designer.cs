using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class NewDatasetSpatialRefPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDatasetSpatialRefPage));
            this.btnImport = new SimpleButton();
            this.btnModify = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.textBoxName = new TextEdit();
            this.label1 = new Label();
            this.label2 = new Label();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.treeView1 = new TreeView();
            this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
            this.GeoToolStripMenuItem = new ToolStripMenuItem();
            this.ProjToolStripMenuItem = new ToolStripMenuItem();
            this.textBoxName.Properties.BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.btnImport.Location = new System.Drawing.Point(266, 85);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new Size(56, 24);
            this.btnImport.TabIndex = 27;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new EventHandler(this.btnImport_Click);
            this.btnModify.Location = new System.Drawing.Point(266, 149);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(56, 24);
            this.btnModify.TabIndex = 26;
            this.btnModify.Text = "修改...";
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnNew.Location = new System.Drawing.Point(266, 117);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(56, 24);
            this.btnNew.TabIndex = 25;
            this.btnNew.Text = "新建...";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.textBoxName.EditValue = "";
            this.textBoxName.Location = new System.Drawing.Point(54, 49);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Properties.ReadOnly = true;
            this.textBoxName.Size = new Size(216, 21);
            this.textBoxName.TabIndex = 24;
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 51);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "名字:";
            this.label2.AllowDrop = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(309, 24);
            this.label2.TabIndex = 28;
            this.label2.Text = "选择数据集XY轴的坐标系统";
            this.imageList_0.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "ProjectFolder.bmp");
            this.imageList_0.Images.SetKeyName(1, "SelectedProjectFolder.bmp");
            this.imageList_0.Images.SetKeyName(2, "Project.bmp");
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList_0;
            this.treeView1.Location = new System.Drawing.Point(15, 85);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(245, 184);
            this.treeView1.TabIndex = 29;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.GeoToolStripMenuItem, this.ProjToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(153, 70);
            this.GeoToolStripMenuItem.Name = "GeoToolStripMenuItem";
            this.GeoToolStripMenuItem.Size = new Size(152, 22);
            this.GeoToolStripMenuItem.Text = "地理坐标系";
            this.GeoToolStripMenuItem.Click += new EventHandler(this.GeoToolStripMenuItem_Click);
            this.ProjToolStripMenuItem.Name = "ProjToolStripMenuItem";
            this.ProjToolStripMenuItem.Size = new Size(152, 22);
            this.ProjToolStripMenuItem.Text = "投影坐标系";
            this.ProjToolStripMenuItem.Click += new EventHandler(this.ProjToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnImport);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.textBoxName);
            base.Controls.Add(this.label1);
            base.Name = "NewDatasetSpatialRefPage";
            base.Size = new Size(378, 357);
            base.Load += new EventHandler(this.NewDatasetSpatialRefPage_Load);
            this.textBoxName.Properties.EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnImport;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem GeoToolStripMenuItem;
        private ImageList imageList_0;
        private Label label1;
        private Label label2;
        private ToolStripMenuItem ProjToolStripMenuItem;
        private TextEdit textBoxName;
        private TreeView treeView1;
    }
}