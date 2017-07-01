using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class frmMarkerPlacementList : Form
    {
        private IMarkerPlacement m_MarkerPlacement = null;
        private BasicSymbolLayerBaseControl m_pControl = null;
        private Control m_SelControl = null;

        public frmMarkerPlacementList()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                this.m_SelControl = this.treeView1.SelectedNode.Tag as Control;
            }
            base.DialogResult = DialogResult.OK;
        }

        private void frmGeometricEffectList_Load(object sender, EventArgs e)
        {
            TreeNode node;
            IMarkerPlacement placement;
            TreeNode node2;
            this.treeView1.Nodes.Clear();
            if (this.m_pControl.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                node2 = new TreeNode("点");
                this.treeView1.Nodes.Add(node2);
                node2.ImageIndex = 1;
                node2.SelectedImageIndex = 1;
                node2.Expand();
                node = new TreeNode("指定位置");
                placement = new MarkerPlacementOnPointClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementOnPointPage(this.m_pControl);
                node2.Nodes.Add(node);
            }
            else if (this.m_pControl.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                node2 = new TreeNode("线");
                this.treeView1.Nodes.Add(node2);
                node2.ImageIndex = 1;
                node2.SelectedImageIndex = 1;
                node2.Expand();
                node = new TreeNode("沿线放置");
                placement = new MarkerPlacementAlongLineClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementAlongLinePage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("放在末端");
                placement = new MarkerPlacementAtExtremitiesClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementAtExtremitiesPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("装饰");
                placement = new MarkerPlacementDecorationClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementDecorationPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("放在线上");
                placement = new MarkerPlacementOnLineClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementOnLinePage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("沿线随机放置");
                placement = new MarkerPlacementRandomAlongLineClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementRandomAlongLinePage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("可变尺寸");
                placement = new MarkerPlacementVariableAlongLineClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementVariableAlongLinePage(this.m_pControl);
                node2.Nodes.Add(node);
            }
            else if (this.m_pControl.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                node2 = new TreeNode("点");
                this.treeView1.Nodes.Add(node2);
                node2.ImageIndex = 1;
                node2.SelectedImageIndex = 1;
                node2.Expand();
                node = new TreeNode("沿线放置");
                placement = new MarkerPlacementAlongLineClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementAlongLinePage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("装饰");
                placement = new MarkerPlacementDecorationClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementDecorationPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("在多边形内");
                placement = new MarkerPlacementInsidePolygonClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementInsidePolygonPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("沿线排列");
                placement = new MarkerPlacementOnLineClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementOnLinePage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("多边形中心");
                placement = new MarkerPlacementPolygonCenterClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementPolygonCenterPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("沿线随机放置");
                placement = new MarkerPlacementRandomAlongLineClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementRandomAlongLinePage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("多边形内随机放置");
                placement = new MarkerPlacementRandomInPolygonClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementRandomInPolygonPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("可变尺寸");
                placement = new MarkerPlacementVariableAlongLineClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new MarkerPlacementVariableAlongLinePage(this.m_pControl);
                node2.Nodes.Add(node);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.btnOK.Enabled = (this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.Tag != null);
        }

        public BasicSymbolLayerBaseControl BasicSymbolLayerBaseControl
        {
            set { this.m_pControl = value; }
        }

        public IMarkerPlacement MarkerPlacement
        {
            get { return this.m_MarkerPlacement; }
        }

        public Control SelectControl
        {
            get { return this.m_SelControl; }
        }
    }
}