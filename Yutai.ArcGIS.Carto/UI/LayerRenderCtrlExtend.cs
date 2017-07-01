using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LayerRenderCtrlExtend : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private ILayer ilayer_0 = null;
        private IUserControl iuserControl_0 = null;
        private SimpleRenderControl simpleRenderControl_0 = new SimpleRenderControl();
        private UniqueValueRendererCtrl uniqueValueRendererCtrl_0 = new UniqueValueRendererCtrl();

        private UniqueValueRendererMoreAttributeCtrl uniqueValueRendererMoreAttributeCtrl_0 =
            new UniqueValueRendererMoreAttributeCtrl();

        public LayerRenderCtrlExtend()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.bool_1 = false;
            if (this.iuserControl_0 != null)
            {
                this.iuserControl_0.Apply();
            }
            return true;
        }

        private void LayerRenderCtrlExtend_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.method_1();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.simpleRenderControl_0.CurrentLayer = this.ilayer_0;
            this.uniqueValueRendererCtrl_0.CurrentLayer = this.ilayer_0;
            this.uniqueValueRendererMoreAttributeCtrl_0.CurrentLayer = this.ilayer_0;
            if (this.ilayer_0 is IGeoFeatureLayer)
            {
                this.treeView1.Nodes.AddRange(new TreeNode[]
                {
                    new TreeNode("要素", new TreeNode[] {new TreeNode("简单渲染")}),
                    new TreeNode("类别", new TreeNode[] {new TreeNode("唯一值渲染"), new TreeNode("唯一值渲染,多字段")})
                });
                this.treeView1.Nodes[0].Tag = this.simpleRenderControl_0;
                this.treeView1.Nodes[0].Nodes[0].Tag = this.simpleRenderControl_0;
                this.treeView1.Nodes[1].Tag = this.uniqueValueRendererCtrl_0;
                this.treeView1.Nodes[1].Nodes[0].Tag = this.uniqueValueRendererCtrl_0;
                this.treeView1.Nodes[1].Nodes[1].Tag = this.uniqueValueRendererMoreAttributeCtrl_0;
                this.simpleRenderControl_0.Visible = false;
                this.simpleRenderControl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.simpleRenderControl_0);
                this.uniqueValueRendererCtrl_0.Visible = false;
                this.uniqueValueRendererCtrl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.uniqueValueRendererCtrl_0);
                this.uniqueValueRendererMoreAttributeCtrl_0.Visible = false;
                this.uniqueValueRendererMoreAttributeCtrl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.uniqueValueRendererMoreAttributeCtrl_0);
            }
            else if (this.ilayer_0 is IRasterLayer)
            {
                this.treeView1.Nodes.AddRange(new TreeNode[] {new TreeNode("唯一值")});
                this.treeView1.Nodes[0].Tag = this.uniqueValueRendererCtrl_0;
                this.uniqueValueRendererCtrl_0.Visible = false;
                this.uniqueValueRendererCtrl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.uniqueValueRendererCtrl_0);
            }
        }

        private void method_1()
        {
            if (this.ilayer_0 != null)
            {
                IGeoFeatureLayer layer = this.ilayer_0 as IGeoFeatureLayer;
                if (this.ibasicMap_0 == null)
                {
                }
                if (layer.FeatureClass != null)
                {
                    if (this.iuserControl_0 != null)
                    {
                        (this.iuserControl_0 as UserControl).Visible = false;
                    }
                    IFeatureRenderer renderer = layer.Renderer;
                    if (renderer is ISimpleRenderer)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[0].Nodes[0];
                        this.iuserControl_0 = this.simpleRenderControl_0;
                        this.simpleRenderControl_0.Visible = true;
                    }
                    else if (renderer is IUniqueValueRenderer)
                    {
                        if ((renderer as IUniqueValueRenderer).FieldCount > 1)
                        {
                            this.treeView1.SelectedNode = this.treeView1.Nodes[1].Nodes[1];
                            this.iuserControl_0 = this.uniqueValueRendererMoreAttributeCtrl_0;
                            this.uniqueValueRendererMoreAttributeCtrl_0.Visible = true;
                        }
                        else if ((renderer as IUniqueValueRenderer).FieldCount == 1)
                        {
                            if (((renderer as IUniqueValueRenderer).LookupStyleset != null) &&
                                ((renderer as IUniqueValueRenderer).LookupStyleset.Length > 0))
                            {
                                this.treeView1.SelectedNode = this.treeView1.Nodes[1].Nodes[2];
                            }
                            else
                            {
                                this.treeView1.SelectedNode = this.treeView1.Nodes[1].Nodes[0];
                                this.iuserControl_0 = this.uniqueValueRendererCtrl_0;
                                this.uniqueValueRendererCtrl_0.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.bool_0 && (this.treeView1.SelectedNode != null))
            {
                IUserControl tag = this.treeView1.SelectedNode.Tag as IUserControl;
                if (tag != null)
                {
                    if (tag is RepresentationRendererPage)
                    {
                        (tag as RepresentationRendererPage).RepresentationClassName = this.treeView1.SelectedNode.Text;
                    }
                    if (this.iuserControl_0 != null)
                    {
                        this.iuserControl_0.Visible = false;
                    }
                    this.iuserControl_0 = tag;
                    try
                    {
                        tag.Visible = true;
                    }
                    catch
                    {
                        tag.Visible = false;
                    }
                    this.bool_1 = true;
                }
            }
        }

        public IBasicMap FocusMap
        {
            set { this.ibasicMap_0 = value; }
        }

        public bool IsPageDirty
        {
            get { return this.bool_1; }
        }

        public ILayer Layer
        {
            set { this.ilayer_0 = value; }
        }

        public object SelectItem
        {
            set { this.ilayer_0 = value as ILayer; }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.simpleRenderControl_0.StyleGallery = value;
                this.uniqueValueRendererCtrl_0.StyleGallery = value;
                this.uniqueValueRendererMoreAttributeCtrl_0.StyleGallery = value;
            }
        }
    }
}