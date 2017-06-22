using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmTemplatesGroup
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplatesGroup));
            this.toolStrip1 = new ToolStrip();
            this.toolStripButton1 = new ToolStripDropDownButton();
            this.显示可见图层ToolStripMenuItem = new ToolStripMenuItem();
            this.显示有模板的图层ToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripButton2 = new ToolStripButton();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.imageList1 = new ImageList(this.components);
            this.listView2 = new ListView();
            this.imageList2 = new ImageList(this.components);
            this.button1 = new Button();
            this.DeletetoolStripButton = new ToolStripSplitButton();
            this.删除ToolStripMenuItem = new ToolStripMenuItem();
            this.删除图层中全部内容ToolStripMenuItem = new ToolStripMenuItem();
            this.删除地图中全部内容ToolStripMenuItem = new ToolStripMenuItem();
            this.propertytoolStripButton = new ToolStripButton();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripButton1, this.toolStripButton2, this.DeletetoolStripButton, this.propertytoolStripButton });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(406, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.DropDownItems.AddRange(new ToolStripItem[] { this.显示可见图层ToolStripMenuItem, this.显示有模板的图层ToolStripMenuItem });
            this.toolStripButton1.Image = (Image) resources.GetObject("toolStripButton1.Image");
            this.toolStripButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new Size(29, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.显示可见图层ToolStripMenuItem.Name = "显示可见图层ToolStripMenuItem";
            this.显示可见图层ToolStripMenuItem.Size = new Size(172, 22);
            this.显示可见图层ToolStripMenuItem.Text = "显示可见图层";
            this.显示可见图层ToolStripMenuItem.Click += new EventHandler(this.显示可见图层ToolStripMenuItem_Click);
            this.显示有模板的图层ToolStripMenuItem.Name = "显示有模板的图层ToolStripMenuItem";
            this.显示有模板的图层ToolStripMenuItem.Size = new Size(172, 22);
            this.显示有模板的图层ToolStripMenuItem.Text = "显示有模板的图层";
            this.显示有模板的图层ToolStripMenuItem.Click += new EventHandler(this.显示有模板的图层ToolStripMenuItem_Click);
            this.toolStripButton2.Image = (Image) resources.GetObject("toolStripButton2.Image");
            this.toolStripButton2.ImageTransparentColor = Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new Size(76, 22);
            this.toolStripButton2.Text = "新建模板";
            this.toolStripButton2.Click += new EventHandler(this.toolStripButton2_Click);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1 });
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(139, 235);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.Text = "图层列表";
            this.columnHeader1.Width = 128;
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "AnnoLayer");
            this.imageList1.Images.SetKeyName(1, "LineLayer");
            this.imageList1.Images.SetKeyName(2, "PointLayer");
            this.imageList1.Images.SetKeyName(3, "FillLayer");
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(157, 28);
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(237, 235);
            this.listView2.SmallImageList = this.imageList2;
            this.listView2.TabIndex = 2;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = View.SmallIcon;
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.imageList2.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList2.ImageSize = new Size(16, 16);
            this.imageList2.TransparentColor = Color.Transparent;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(319, 269);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.DeletetoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.DeletetoolStripButton.DropDownItems.AddRange(new ToolStripItem[] { this.删除ToolStripMenuItem, this.删除图层中全部内容ToolStripMenuItem, this.删除地图中全部内容ToolStripMenuItem });
            this.DeletetoolStripButton.Image = (Image) resources.GetObject("DeletetoolStripButton.Image");
            this.DeletetoolStripButton.ImageTransparentColor = Color.Magenta;
            this.DeletetoolStripButton.Name = "DeletetoolStripButton";
            this.DeletetoolStripButton.Size = new Size(48, 22);
            this.DeletetoolStripButton.Text = "删除";
            this.DeletetoolStripButton.ButtonClick += new EventHandler(this.DeletetoolStripButton_Click);
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new Size(184, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new EventHandler(this.DeletetoolStripButton_Click);
            this.删除图层中全部内容ToolStripMenuItem.Name = "删除图层中全部内容ToolStripMenuItem";
            this.删除图层中全部内容ToolStripMenuItem.Size = new Size(184, 22);
            this.删除图层中全部内容ToolStripMenuItem.Text = "删除图层中全部内容";
            this.删除图层中全部内容ToolStripMenuItem.Click += new EventHandler(this.删除图层中全部内容ToolStripMenuItem_Click);
            this.删除地图中全部内容ToolStripMenuItem.Name = "删除地图中全部内容ToolStripMenuItem";
            this.删除地图中全部内容ToolStripMenuItem.Size = new Size(184, 22);
            this.删除地图中全部内容ToolStripMenuItem.Text = "删除地图中全部内容";
            this.删除地图中全部内容ToolStripMenuItem.Click += new EventHandler(this.删除地图中全部内容ToolStripMenuItem_Click);
            this.propertytoolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.propertytoolStripButton.Image = (Image) resources.GetObject("propertytoolStripButton.Image");
            this.propertytoolStripButton.ImageTransparentColor = Color.Magenta;
            this.propertytoolStripButton.Name = "propertytoolStripButton";
            this.propertytoolStripButton.Size = new Size(36, 22);
            this.propertytoolStripButton.Text = "属性";
            this.propertytoolStripButton.Click += new EventHandler(this.propertytoolStripButton_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(406, 298);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.listView2);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.toolStrip1);
            base.Name = "frmTemplatesGroup";
            this.Text = "要素模板";
            base.Load += new EventHandler(this.frmTemplatesGroup_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private Button button1;
        private ColumnHeader columnHeader1;
        private ToolStripSplitButton DeletetoolStripButton;
        private ImageList imageList1;
        private ImageList imageList2;
        private ListView listView1;
        private ListView listView2;
        private ToolStripButton propertytoolStripButton;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private ToolStripMenuItem 删除地图中全部内容ToolStripMenuItem;
        private ToolStripMenuItem 删除图层中全部内容ToolStripMenuItem;
        private ToolStripMenuItem 显示可见图层ToolStripMenuItem;
        private ToolStripMenuItem 显示有模板的图层ToolStripMenuItem;
    }
}