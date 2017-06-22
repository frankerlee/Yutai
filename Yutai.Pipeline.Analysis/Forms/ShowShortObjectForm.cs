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
	public partial class ShowShortObjectForm : Form
	{
		private IContainer icontainer_0 = null;




		public IAppContext pApp
		{
			set
			{
				this._context = value;
			}
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
