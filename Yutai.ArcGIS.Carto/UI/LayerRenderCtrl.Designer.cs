using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LayerRenderCtrl
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
            this.panel1 = new Panel();
            this.treeView1 = new TreeView();
            this.panel = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(136, 256);
            this.panel1.TabIndex = 1;
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new Size(136, 256);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.panel.Dock = DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(136, 0);
            this.panel.Name = "panel";
            this.panel.Size = new Size(320, 256);
            this.panel.TabIndex = 2;
            base.Controls.Add(this.panel);
            base.Controls.Add(this.panel1);
            base.Name = "LayerRenderCtrl";
            base.Size = new Size(456, 256);
            base.Load += new EventHandler(this.LayerRenderCtrl_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private Panel panel;
        private Panel panel1;
        private TreeView treeView1;
    }
}