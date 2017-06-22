using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Pipeline.Analysis.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Forms
{
	    partial class ShowShortObjectForm
    {
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.icontainer_0 != null)
			{
				this.icontainer_0.Dispose();
			}
			base.Dispose(disposing);
		}

	
	private void InitializeComponent()
		{
			this.treeView1 = new TreeView();
			this.label1 = new Label();
			base.SuspendLayout();
			this.treeView1.Location = new System.Drawing.Point(0, 23);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new Size(221, 317);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new Size(41, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "label1";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new Size(221, 341);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.treeView1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Location = new System.Drawing.Point(10, 120);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ShowShortObjectForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "最短路径所经管线列表";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

	
		private TreeView treeView1;
		private Label label1;
		private IAppContext _context;
    }
}