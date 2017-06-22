using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal partial class frmGeometricEffectList : Form
    {
        private IGeometricEffect m_GeometricEffect = null;
        private BasicSymbolLayerBaseControl m_pControl = null;
        private Control m_SelControl = null;

        public frmGeometricEffectList()
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
            IGeometricEffect effect;
            TreeNode node2;
            this.treeView1.Nodes.Clear();
            if (this.m_pControl.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                node2 = new TreeNode("点");
                this.treeView1.Nodes.Add(node2);
                node2.ImageIndex = 1;
                node2.SelectedImageIndex = 1;
                node2.Expand();
                node = new TreeNode("射线");
                effect = new GeometricEffectRadialClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricEffectRadialfromPointPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("缓冲区");
                effect = new GeometricEffectBufferClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectBufferPage(this.m_pControl);
                node2.Nodes.Add(node);
            }
            else if (this.m_pControl.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                node2 = new TreeNode("线");
                this.treeView1.Nodes.Add(node2);
                node2.ImageIndex = 1;
                node2.SelectedImageIndex = 1;
                node2.Expand();
                node = new TreeNode("添加控制点");
                effect = new GeometricEffectAddControlPointsClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricEffectAddControlPointsPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("短线");
                effect = new GeometricEffectCutClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricCutCurverPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("虚线");
                effect = new GeometricEffectDashClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricEffectDashesPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("偏移线");
                effect = new GeometricEffectOffsetClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricEffectOffsetCurvePage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("反转线");
                effect = new GeometricEffectReverseClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricEffectReverseCurverPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("简化几何对象");
                effect = new GeometricEffectSimplifyClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricEffectSimplyPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("平滑几何对象");
                effect = new GeometricEffectSmoothClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricSmoothCurverPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("缓冲区");
                effect = new GeometricEffectBufferClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectBufferPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("闭合面");
                effect = new GeometricEffectEnclosingPolygonClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectEnclosingPolygonPage(this.m_pControl);
                node2.Nodes.Add(node);
            }
            else if (this.m_pControl.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                node2 = new TreeNode("点");
                this.treeView1.Nodes.Add(node2);
                node2.ImageIndex = 1;
                node2.SelectedImageIndex = 1;
                node2.Expand();
                node = new TreeNode("短线");
                effect = new GeometricEffectCutClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricCutCurverPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("虚线");
                effect = new GeometricEffectDashClass();
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
                node.Tag = new GeometricEffectDashesPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("添加控制点");
                effect = new GeometricEffectAddControlPointsClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectAddControlPointsPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("缓冲区");
                effect = new GeometricEffectBufferClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectBufferPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("空洞");
                effect = new GeometricEffectDonutClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectDonutPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("闭合面");
                effect = new GeometricEffectEnclosingPolygonClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectEnclosingPolygonPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("偏移线");
                effect = new GeometricEffectOffsetClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectOffsetCurvePage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("简化几何对象");
                effect = new GeometricEffectSimplifyClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricEffectSimplyPage(this.m_pControl);
                node2.Nodes.Add(node);
                node = new TreeNode("平滑几何对象");
                effect = new GeometricEffectSmoothClass();
                node.ImageIndex = 3;
                node.SelectedImageIndex = 3;
                node.Tag = new GeometricSmoothCurverPage(this.m_pControl);
                node2.Nodes.Add(node);
            }
        }

 private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.btnOK.Enabled = (this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.Tag != null);
        }

        public BasicSymbolLayerBaseControl BasicSymbolLayerBaseControl
        {
            set
            {
                this.m_pControl = value;
            }
        }

        public IGeometricEffect GeometricEffect
        {
            get
            {
                return this.m_GeometricEffect;
            }
        }

        public Control SelectControl
        {
            get
            {
                return this.m_SelControl;
            }
        }
    }
}

