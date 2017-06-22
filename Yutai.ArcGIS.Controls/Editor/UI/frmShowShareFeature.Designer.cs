using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmShowShareFeature
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
            this.treeView1 = new TreeView();
            base.SuspendLayout();
            this.treeView1.Dock = DockStyle.Fill;
            this.treeView1.ImageIndex = -1;
            this.treeView1.Location = new Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = -1;
            this.treeView1.Size = new Size(218, 173);
            this.treeView1.TabIndex = 0;
            this.treeView1.MouseDown += new MouseEventHandler(this.treeView1_MouseDown);
            this.treeView1.Click += new EventHandler(this.treeView1_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(218, 173);
            base.Controls.Add(this.treeView1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmShowShareFeature";
            this.Text = "共享要素";
            base.Load += new EventHandler(this.frmShowShareFeature_Load);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private TreeView treeView1;
    }
}