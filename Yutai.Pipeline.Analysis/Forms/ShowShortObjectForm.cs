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
	public class ShowShortObjectForm : Form
	{
		private IContainer icontainer_0 = null;

		private TreeView treeView1;

		private Label label1;

		private IAppContext _context;

		public IAppContext pApp
		{
			set
			{
				this._context = value;
			}
		}

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
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(221, 341);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.treeView1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
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

		public ShowShortObjectForm(IAppContext context)
		{
			this.InitializeComponent();
		    _context = context;
		}

		public void AddLenght(double dbLen)
		{
			this.label1.Text = string.Format("最短路径长度为{0}米", dbLen.ToString("f2"));
		}

		public void AddPipeName(string strPipeName)
		{
			this.treeView1.Nodes.Add(strPipeName);
		}

		public void AddFeature(IFeature pFeature)
		{
			TreeNode treeNode = this.treeView1.Nodes[0].Nodes.Add(pFeature.OID.ToString());
			treeNode.Tag = pFeature;
		}

		public void AddShortPath(IPolyline pShortPath)
		{
			this.treeView1.Nodes[0].Tag = pShortPath;
		}

		private void treeView1_AfterSelect(object obj, TreeViewEventArgs treeViewEventArgs)
		{
			if (this.treeView1.SelectedNode != null)
			{
				if (this.treeView1.SelectedNode.Parent == null)
				{
					IPolyline polyline = this.treeView1.SelectedNode.Tag as IPolyline;
					if (polyline != null)
					{
						CMapOperator.ShowFeatureWithWink(_context.ActiveView.ScreenDisplay, polyline);
					}
				}
				else
				{
					IFeature feature = this.treeView1.SelectedNode.Tag as IFeature;
					if (feature != null)
					{
                        CMapOperator.ShowFeatureWithWink(_context.ActiveView.ScreenDisplay, feature.Shape);
					}
				}
			}
		}
	}
}
