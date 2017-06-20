using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class frmMarkerPlacementList : Form
    {
        private Button btnOK;
        private Button button1;
        private IContainer components = null;
        private ImageList imageList1;
        private IMarkerPlacement m_MarkerPlacement = null;
        private BasicSymbolLayerBaseControl m_pControl = null;
        private Control m_SelControl = null;
        private TreeView treeView1;

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

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMarkerPlacementList));
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
            this.imageList1.Images.SetKeyName(2, "Bitmap6.bmp");
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
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmMarkerPlacementList";
            this.Text = "点放置位置";
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

        public IMarkerPlacement MarkerPlacement
        {
            get
            {
                return this.m_MarkerPlacement;
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

