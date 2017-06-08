using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class frmGeometricEffectList : Form
    {
        private Button btnOK;
        private Button button1;
        private IContainer components = null;
        private ImageList imageList1;
        private IGeometricEffect m_GeometricEffect = null;
        private BasicSymbolLayerBaseControl m_pControl = null;
        private Control m_SelControl = null;
        private TreeView treeView1;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGeometricEffectList));
            TreeNode node = new TreeNode("点", 1, 1);
            TreeNode node2 = new TreeNode("线", 1, 1);
            TreeNode node3 = new TreeNode("面", 1, 1);
            this.imageList1 = new ImageList(this.components);
            this.treeView1 = new TreeView();
            this.btnOK = new Button();
            this.button1 = new Button();
            base.SuspendLayout();
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Bitmap1.bmp");
            this.imageList1.Images.SetKeyName(1, "Bitmap1.bmp");
            this.imageList1.Images.SetKeyName(2, "Bitmap2.bmp");
            this.imageList1.Images.SetKeyName(3, "Bitmap3.bmp");
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 1);
            this.treeView1.Name = "treeView1";
            node.ImageIndex = 1;
            node.Name = "节点0";
            node.SelectedImageIndex = 1;
            node.Text = "点";
            node2.ImageIndex = 1;
            node2.Name = "线";
            node2.SelectedImageIndex = 1;
            node2.Text = "线";
            node3.ImageIndex = 1;
            node3.Name = "节点2";
            node3.SelectedImageIndex = 1;
            node3.Text = "面";
            this.treeView1.Nodes.AddRange(new TreeNode[] { node, node2, node3 });
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(0xdb, 0xea);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.btnOK.Location = new System.Drawing.Point(50, 0xf3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x3a, 30);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "添加";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button1.DialogResult = DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(0x97, 0xf3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x3a, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xdd, 0x111);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.treeView1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGeometricEffectList";
            this.Text = "Geometric Effect";
            base.Load += new EventHandler(this.frmGeometricEffectList_Load);
            base.ResumeLayout(false);
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

