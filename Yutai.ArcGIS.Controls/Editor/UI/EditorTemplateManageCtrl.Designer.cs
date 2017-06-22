using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class EditorTemplateManageCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorTemplateManageCtrl));
            this.toolStrip1 = new ToolStrip();
            this.toolStripDropDownButton1 = new ToolStripDropDownButton();
            this.ShowAllToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.过滤依据ToolStripMenuItem = new ToolStripMenuItem();
            this.PointFilterToolStripMenuItem = new ToolStripMenuItem();
            this.LineFilterToolStripMenuItem = new ToolStripMenuItem();
            this.FillFilterToolStripMenuItem = new ToolStripMenuItem();
            this.AnnoFilterToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.LayerFilterToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.分组依据ToolStripMenuItem = new ToolStripMenuItem();
            this.GeometryToolStripMenuItem = new ToolStripMenuItem();
            this.LayerGroupToolStripMenuItem1 = new ToolStripMenuItem();
            this.toolStripSeparator5 = new ToolStripSeparator();
            this.清除分组ToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripButton1 = new ToolStripButton();
            this.listView1 = new EditTemplateListView();
            this.contextMenuStrip1 = new ContextMenuStrip();
            this.DeleteToolStripMenuItem = new ToolStripMenuItem();
            this.CopyToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.PropertyToolStripMenuItem = new ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.toolStripDropDownButton1, this.toolStripButton1 });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(345, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            this.toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { this.ShowAllToolStripMenuItem, this.toolStripSeparator3, this.过滤依据ToolStripMenuItem, this.toolStripSeparator4, this.分组依据ToolStripMenuItem });
            this.toolStripDropDownButton1.Image = (Image) resources.GetObject("toolStripDropDownButton1.Image");
            this.toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new Size(29, 22);
            this.toolStripDropDownButton1.Text = "通过分组和过滤排列模板";
            this.ShowAllToolStripMenuItem.Name = "ShowAllToolStripMenuItem";
            this.ShowAllToolStripMenuItem.Size = new Size(148, 22);
            this.ShowAllToolStripMenuItem.Text = "显示所有模板";
            this.ShowAllToolStripMenuItem.Click += new EventHandler(this.显示所有模板ToolStripMenuItem_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(145, 6);
            this.过滤依据ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.PointFilterToolStripMenuItem, this.LineFilterToolStripMenuItem, this.FillFilterToolStripMenuItem, this.AnnoFilterToolStripMenuItem, this.toolStripSeparator2, this.LayerFilterToolStripMenuItem });
            this.过滤依据ToolStripMenuItem.Name = "过滤依据ToolStripMenuItem";
            this.过滤依据ToolStripMenuItem.Size = new Size(148, 22);
            this.过滤依据ToolStripMenuItem.Text = "过滤依据";
            this.PointFilterToolStripMenuItem.Name = "PointFilterToolStripMenuItem";
            this.PointFilterToolStripMenuItem.Size = new Size(100, 22);
            this.PointFilterToolStripMenuItem.Text = "点";
            this.PointFilterToolStripMenuItem.Click += new EventHandler(this.PointFilterToolStripMenuItem_Click);
            this.LineFilterToolStripMenuItem.Name = "LineFilterToolStripMenuItem";
            this.LineFilterToolStripMenuItem.Size = new Size(100, 22);
            this.LineFilterToolStripMenuItem.Text = "线";
            this.LineFilterToolStripMenuItem.Click += new EventHandler(this.LineFilterToolStripMenuItem_Click);
            this.FillFilterToolStripMenuItem.Name = "FillFilterToolStripMenuItem";
            this.FillFilterToolStripMenuItem.Size = new Size(100, 22);
            this.FillFilterToolStripMenuItem.Text = "面";
            this.FillFilterToolStripMenuItem.Click += new EventHandler(this.FillFilterToolStripMenuItem_Click);
            this.AnnoFilterToolStripMenuItem.Name = "AnnoFilterToolStripMenuItem";
            this.AnnoFilterToolStripMenuItem.Size = new Size(100, 22);
            this.AnnoFilterToolStripMenuItem.Text = "注记";
            this.AnnoFilterToolStripMenuItem.Click += new EventHandler(this.AnnoFilterToolStripMenuItem_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(97, 6);
            this.LayerFilterToolStripMenuItem.Name = "LayerFilterToolStripMenuItem";
            this.LayerFilterToolStripMenuItem.Size = new Size(100, 22);
            this.LayerFilterToolStripMenuItem.Text = "图层";
            this.LayerFilterToolStripMenuItem.Click += new EventHandler(this.LayerFilterToolStripMenuItem_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(145, 6);
            this.分组依据ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.GeometryToolStripMenuItem, this.LayerGroupToolStripMenuItem1, this.toolStripSeparator5, this.清除分组ToolStripMenuItem });
            this.分组依据ToolStripMenuItem.Name = "分组依据ToolStripMenuItem";
            this.分组依据ToolStripMenuItem.Size = new Size(148, 22);
            this.分组依据ToolStripMenuItem.Text = "分组依据";
            this.GeometryToolStripMenuItem.Name = "GeometryToolStripMenuItem";
            this.GeometryToolStripMenuItem.Size = new Size(124, 22);
            this.GeometryToolStripMenuItem.Text = "类型";
            this.GeometryToolStripMenuItem.Click += new EventHandler(this.GeometryToolStripMenuItem_Click);
            this.LayerGroupToolStripMenuItem1.Name = "LayerGroupToolStripMenuItem1";
            this.LayerGroupToolStripMenuItem1.Size = new Size(124, 22);
            this.LayerGroupToolStripMenuItem1.Text = "图层";
            this.LayerGroupToolStripMenuItem1.Click += new EventHandler(this.LayerGroupToolStripMenuItem1_Click);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new Size(121, 6);
            this.清除分组ToolStripMenuItem.Name = "清除分组ToolStripMenuItem";
            this.清除分组ToolStripMenuItem.Size = new Size(124, 22);
            this.清除分组ToolStripMenuItem.Text = "清除分组";
            this.清除分组ToolStripMenuItem.Click += new EventHandler(this.清除分组ToolStripMenuItem_Click);
            this.toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = (Image) resources.GetObject("toolStripButton1.Image");
            this.toolStripButton1.ImageTransparentColor = Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new Size(23, 22);
            this.toolStripButton1.Text = "组织模板";
            this.toolStripButton1.Click += new EventHandler(this.toolStripButton1_Click);
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(345, 312);
            this.listView1.TabIndex = 1;
            this.listView1.View = View.SmallIcon;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseUp += new MouseEventHandler(this.listView1_MouseUp);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.DeleteToolStripMenuItem, this.CopyToolStripMenuItem, this.toolStripSeparator1, this.PropertyToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(101, 76);
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new Size(100, 22);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new EventHandler(this.DeleteToolStripMenuItem_Click);
            this.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem";
            this.CopyToolStripMenuItem.Size = new Size(100, 22);
            this.CopyToolStripMenuItem.Text = "复制";
            this.CopyToolStripMenuItem.Click += new EventHandler(this.CopyToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(97, 6);
            this.PropertyToolStripMenuItem.Name = "PropertyToolStripMenuItem";
            this.PropertyToolStripMenuItem.Size = new Size(100, 22);
            this.PropertyToolStripMenuItem.Text = "属性";
            this.PropertyToolStripMenuItem.Click += new EventHandler(this.PropertyToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.toolStrip1);
            base.Name = "EditorTemplateManageCtrl";
            base.Size = new Size(345, 337);
            base.Load += new EventHandler(this.EditorTemplateManageCtrl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
        private ToolStripMenuItem AnnoFilterToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem CopyToolStripMenuItem;
        private ToolStripMenuItem DeleteToolStripMenuItem;
        private ToolStripMenuItem FillFilterToolStripMenuItem;
        private ToolStripMenuItem GeometryToolStripMenuItem;
        private ToolStripMenuItem LayerFilterToolStripMenuItem;
        private ToolStripMenuItem LayerGroupToolStripMenuItem1;
        private ToolStripMenuItem LineFilterToolStripMenuItem;
        private EditTemplateListView listView1;
        private ToolStripMenuItem PointFilterToolStripMenuItem;
        private ToolStripMenuItem PropertyToolStripMenuItem;
        private ToolStripMenuItem ShowAllToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem 分组依据ToolStripMenuItem;
        private ToolStripMenuItem 过滤依据ToolStripMenuItem;
        private ToolStripMenuItem 清除分组ToolStripMenuItem;
    }
}