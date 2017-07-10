using System;
using System.ComponentModel;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Display;

namespace Yutai.Plugins.Editor.Forms
{
	internal partial class frmShowShareFeature : System.Windows.Forms.Form
	{

		private Container container_0 = null;

		private IMap imap_0 = null;

		private ITopologyGraph itopologyGraph_0 = null;

		public IMap FocusMap
		{
			set
			{
				this.imap_0 = value;
			}
		}

		public ITopologyGraph TopologyGraph
		{
			set
			{
				this.itopologyGraph_0 = value;
			}
		}

		public frmShowShareFeature()
		{
			this.InitializeComponent();
		}

	private void frmShowShareFeature_Load(object sender, EventArgs e)
		{
			IEnumTopologyParent selectionParents = this.itopologyGraph_0.SelectionParents;
			selectionParents.Reset();
			esriTopologyParent esriTopologyParent = selectionParents.Next();
			IFeatureClass featureClass = null;
			System.Windows.Forms.TreeNode treeNode = null;
			while (esriTopologyParent.m_pFC != null)
			{
				if (featureClass != esriTopologyParent.m_pFC)
				{
					featureClass = esriTopologyParent.m_pFC;
					treeNode = new System.Windows.Forms.TreeNode(featureClass.AliasName);
					treeNode.Tag = featureClass;
					this.treeView1.Nodes.Add(treeNode);
				}
				System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode(esriTopologyParent.m_FID.ToString());
				treeNode2.Tag = esriTopologyParent.m_FID;
				treeNode.Nodes.Add(treeNode2);
				esriTopologyParent = selectionParents.Next();
			}
		}

		private void treeView1_Click(object sender, EventArgs e)
		{
		}

		private void treeView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.method_0(e.X, e.Y);
		}

		private void method_0(int int_0, int int_1)
		{
			try
			{
				System.Windows.Forms.TreeNode nodeAt = this.treeView1.GetNodeAt(int_0, int_1);
				if (nodeAt != null)
				{
					if (nodeAt.Parent != null)
					{
						IActiveView activeView = (IActiveView)this.imap_0;
						int iD = (int)nodeAt.Tag;
						IFeatureClass featureClass = nodeAt.Parent.Tag as IFeatureClass;
						IFeature feature = featureClass.GetFeature(iD);
						if (feature != null)
						{
							Flash.FlashFeature(activeView.ScreenDisplay, feature);
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
