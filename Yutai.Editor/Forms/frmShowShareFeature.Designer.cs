using System;

namespace Yutai.Plugins.Editor.Forms
{
	    partial class frmShowShareFeature
    {
		protected override void Dispose(bool bool_0)
		{
			if (bool_0 && this.container_0 != null)
			{
				this.container_0.Dispose();
			}
			base.Dispose(bool_0);
		}

	
	private void InitializeComponent()
		{
			this.treeView1 = new System.Windows.Forms.TreeView();
			base.SuspendLayout();
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(218, 173);
			this.treeView1.TabIndex = 0;
			this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
			this.treeView1.Click += new EventHandler(this.treeView1_Click);
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			base.ClientSize = new System.Drawing.Size(218, 173);
			base.Controls.Add(this.treeView1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmShowShareFeature";
			this.Text = "共享要素";
			base.Load += new EventHandler(this.frmShowShareFeature_Load);
			base.ResumeLayout(false);
		}

	
		private System.Windows.Forms.TreeView treeView1;
    }
}