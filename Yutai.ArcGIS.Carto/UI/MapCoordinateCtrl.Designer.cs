using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Controls.Controls;


namespace Yutai.ArcGIS.Carto.UI
{
    partial class MapCoordinateCtrl
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
 private void InitializeComponent()
        {
            this.icontainer_0 = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapCoordinateCtrl));
            this.label1 = new Label();
            this.textBoxDetail = new MemoEdit();
            this.btnClear = new SimpleButton();
            this.label2 = new Label();
            this.treeView1 = new TreeView();
            this.imageList_0 = new ImageList(this.icontainer_0);
            this.contextMenu_0 = new ContextMenu();
            this.menuItem_0 = new MenuItem();
            this.menuItem_1 = new MenuItem();
            this.btnModify = new SimpleButton();
            this.btnNew = new SimpleButton();
            this.btnSelect = new SimpleButton();
            this.btnImport = new SimpleButton();
            this.btnGeoTransformation = new SimpleButton();
            this.textBoxDetail.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前坐标系统";
            this.textBoxDetail.EditValue = "";
            this.textBoxDetail.Location = new System.Drawing.Point(8, 32);
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.Size = new Size(216, 160);
            this.textBoxDetail.TabIndex = 1;
            this.btnClear.Location = new System.Drawing.Point(240, 40);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(56, 24);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清除";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 200);
            this.label2.Name = "label2";
            this.label2.Size = new Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "选择一个标系统";
            this.label2.Visible = false;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList_0;
            this.treeView1.Location = new System.Drawing.Point(8, 216);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(224, 120);
            this.treeView1.TabIndex = 4;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.imageList_0.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList_0.TransparentColor = Color.Transparent;
            this.imageList_0.Images.SetKeyName(0, "");
            this.imageList_0.Images.SetKeyName(1, "");
            this.imageList_0.Images.SetKeyName(2, "");
            this.contextMenu_0.MenuItems.AddRange(new MenuItem[] { this.menuItem_0, this.menuItem_1 });
            this.menuItem_0.Index = 0;
            this.menuItem_0.Text = "地理坐标系";
            this.menuItem_0.Click += new EventHandler(this.menuItem_0_Click);
            this.menuItem_1.Index = 1;
            this.menuItem_1.Text = "投影坐标系";
            this.menuItem_1.Click += new EventHandler(this.menuItem_1_Click);
            this.btnModify.Location = new System.Drawing.Point(240, 216);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(56, 24);
            this.btnModify.TabIndex = 17;
            this.btnModify.Text = "修改...";
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.btnNew.Location = new System.Drawing.Point(240, 248);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new Size(56, 24);
            this.btnNew.TabIndex = 16;
            this.btnNew.Text = "新建...";
            this.btnNew.Click += new EventHandler(this.btnNew_Click);
            this.btnSelect.Location = new System.Drawing.Point(240, 280);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(56, 24);
            this.btnSelect.TabIndex = 18;
            this.btnSelect.Text = "选择";
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.btnImport.Location = new System.Drawing.Point(240, 312);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new Size(56, 24);
            this.btnImport.TabIndex = 23;
            this.btnImport.Text = "导入";
            this.btnImport.Click += new EventHandler(this.btnImport_Click);
            this.btnGeoTransformation.Location = new System.Drawing.Point(240, 168);
            this.btnGeoTransformation.Name = "btnGeoTransformation";
            this.btnGeoTransformation.Size = new Size(56, 24);
            this.btnGeoTransformation.TabIndex = 24;
            this.btnGeoTransformation.Text = "转换...";
            this.btnGeoTransformation.Click += new EventHandler(this.btnGeoTransformation_Click);
            base.Controls.Add(this.btnGeoTransformation);
            base.Controls.Add(this.btnImport);
            base.Controls.Add(this.btnSelect);
            base.Controls.Add(this.btnModify);
            base.Controls.Add(this.btnNew);
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.textBoxDetail);
            base.Controls.Add(this.label1);
            base.Name = "MapCoordinateCtrl";
            base.Size = new Size(304, 352);
            base.Load += new EventHandler(this.MapCoordinateCtrl_Load);
            this.textBoxDetail.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        void IPropertyPage.Hide()
        {
            base.Hide();
        }

       
        private SimpleButton btnClear;
        private SimpleButton btnGeoTransformation;
        private SimpleButton btnImport;
        private SimpleButton btnModify;
        private SimpleButton btnNew;
        private SimpleButton btnSelect;
        private ContextMenu contextMenu_0;
        private IBasicMap ibasicMap_0;
        private IContainer icontainer_0;
        private ImageList imageList_0;
        private ISpatialReference ispatialReference_0;
        private Label label1;
        private Label label2;
        private MenuItem menuItem_0;
        private MenuItem menuItem_1;
        private MemoEdit textBoxDetail;
        private TreeView treeView1;
    }
}