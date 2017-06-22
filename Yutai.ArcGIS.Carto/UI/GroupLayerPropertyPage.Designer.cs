using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class GroupLayerPropertyPage
    {
        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupLayerPropertyPage));
            this.groupBox1 = new GroupBox();
            this.btnDown = new SimpleButton();
            this.btnUp = new SimpleButton();
            this.btnProperty = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnAdd = new SimpleButton();
            this.listBox1 = new ListBox();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.btnProperty);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(360, 192);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层";
            this.btnDown.Enabled = false;
            this.btnDown.Image = (System.Drawing.Image)resources.GetObject("btnDown.Image");
            this.btnDown.Location = new System.Drawing.Point(280, 152);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new Size(32, 24);
            this.btnDown.TabIndex = 5;
            this.btnDown.Click += new EventHandler(this.btnDown_Click);
            this.btnUp.Enabled = false;
            this.btnUp.Image = (System.Drawing.Image)resources.GetObject("btnUp.Image");
            this.btnUp.Location = new System.Drawing.Point(280, 120);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new Size(32, 24);
            this.btnUp.TabIndex = 4;
            this.btnUp.Click += new EventHandler(this.btnUp_Click);
            this.btnProperty.Enabled = false;
            this.btnProperty.Location = new System.Drawing.Point(280, 88);
            this.btnProperty.Name = "btnProperty";
            this.btnProperty.Size = new Size(48, 24);
            this.btnProperty.TabIndex = 3;
            this.btnProperty.Text = "属性";
            this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(280, 56);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(48, 24);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnAdd.Location = new System.Drawing.Point(280, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(48, 24);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(16, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(256, 160);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            base.Controls.Add(this.groupBox1);
            base.Name = "GroupLayerPropertyPage";
            base.Size = new Size(408, 240);
            base.Load += new EventHandler(this.GroupLayerPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnDown;
        private SimpleButton btnProperty;
        private SimpleButton btnUp;
        private GroupBox groupBox1;
        private ListBox listBox1;
    }
}