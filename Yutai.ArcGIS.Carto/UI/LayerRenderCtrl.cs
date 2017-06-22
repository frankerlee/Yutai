using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LayerRenderCtrl : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ChartRendererCtrl chartRendererCtrl_0 = new ChartRendererCtrl();
        private ChartRendererCtrl chartRendererCtrl_1 = new ChartRendererCtrl();
        private ClassBreaksRendererCtrl classBreaksRendererCtrl_0 = new ClassBreaksRendererCtrl();
        private Container container_0 = null;
        private DotDensityRendererCtrl dotDensityRendererCtrl_0 = new DotDensityRendererCtrl();
        private IBasicMap ibasicMap_0 = null;
        private ILayer ilayer_0 = null;
        private IUserControl iuserControl_0 = null;
        private MatchStyleGrallyCtrl matchStyleGrallyCtrl_0 = new MatchStyleGrallyCtrl();
        private ProportionalSymbolRendererCtrl proportionalSymbolRendererCtrl_0 = new ProportionalSymbolRendererCtrl();
        private RepresentationRendererPage representationRendererPage_0 = new RepresentationRendererPage();
        private SimpleRenderControl simpleRenderControl_0 = new SimpleRenderControl();
        private UniqueValueRendererCtrl uniqueValueRendererCtrl_0 = new UniqueValueRendererCtrl();
        private UniqueValueRendererMoreAttributeCtrl uniqueValueRendererMoreAttributeCtrl_0 = new UniqueValueRendererMoreAttributeCtrl();

        public LayerRenderCtrl()
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

 private void LayerRenderCtrl_Load(object sender, EventArgs e)
        {
            this.method_0();
            this.method_1();
            this.bool_0 = true;
        }

        private void method_0()
        {
            this.dotDensityRendererCtrl_0.CurrentLayer = this.ilayer_0;
            this.simpleRenderControl_0.CurrentLayer = this.ilayer_0;
            this.uniqueValueRendererCtrl_0.CurrentLayer = this.ilayer_0;
            this.classBreaksRendererCtrl_0.CurrentLayer = this.ilayer_0;
            this.proportionalSymbolRendererCtrl_0.CurrentLayer = this.ilayer_0;
            this.chartRendererCtrl_0.CurrentLayer = this.ilayer_0;
            this.chartRendererCtrl_1.ChartRenderType = 1;
            this.chartRendererCtrl_1.CurrentLayer = this.ilayer_0;
            this.uniqueValueRendererMoreAttributeCtrl_0.CurrentLayer = this.ilayer_0;
            this.matchStyleGrallyCtrl_0.CurrentLayer = this.ilayer_0;
            if (this.ilayer_0 is IGeoFeatureLayer)
            {
                this.treeView1.Nodes.AddRange(new TreeNode[] { new TreeNode("要素", new TreeNode[] { new TreeNode("简单渲染") }), new TreeNode("类别", new TreeNode[] { new TreeNode("唯一值渲染"), new TreeNode("唯一值渲染,多字段"), new TreeNode("匹配符号库") }), new TreeNode("数量", new TreeNode[] { new TreeNode("渐变颜色渲染"), new TreeNode("比例符号") }), new TreeNode("图表", new TreeNode[] { new TreeNode("饼图"), new TreeNode("直方图") }) });
                this.treeView1.Nodes[0].Tag = this.simpleRenderControl_0;
                this.treeView1.Nodes[0].Nodes[0].Tag = this.simpleRenderControl_0;
                this.treeView1.Nodes[1].Tag = this.uniqueValueRendererCtrl_0;
                this.treeView1.Nodes[1].Nodes[0].Tag = this.uniqueValueRendererCtrl_0;
                this.treeView1.Nodes[1].Nodes[1].Tag = this.uniqueValueRendererMoreAttributeCtrl_0;
                this.treeView1.Nodes[1].Nodes[2].Tag = this.matchStyleGrallyCtrl_0;
                this.treeView1.Nodes[2].Tag = this.classBreaksRendererCtrl_0;
                this.treeView1.Nodes[2].Nodes[0].Tag = this.classBreaksRendererCtrl_0;
                this.treeView1.Nodes[2].Nodes[1].Tag = this.proportionalSymbolRendererCtrl_0;
                this.treeView1.Nodes[3].Tag = this.chartRendererCtrl_0;
                this.treeView1.Nodes[3].Nodes[0].Tag = this.chartRendererCtrl_0;
                this.treeView1.Nodes[3].Nodes[1].Tag = this.chartRendererCtrl_1;
                this.simpleRenderControl_0.Visible = false;
                this.simpleRenderControl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.simpleRenderControl_0);
                this.uniqueValueRendererCtrl_0.Visible = false;
                this.uniqueValueRendererCtrl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.uniqueValueRendererCtrl_0);
                this.uniqueValueRendererMoreAttributeCtrl_0.Visible = false;
                this.uniqueValueRendererMoreAttributeCtrl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.uniqueValueRendererMoreAttributeCtrl_0);
                this.matchStyleGrallyCtrl_0.Visible = false;
                this.matchStyleGrallyCtrl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.matchStyleGrallyCtrl_0);
                this.classBreaksRendererCtrl_0.Dock = DockStyle.Fill;
                this.classBreaksRendererCtrl_0.Visible = false;
                this.panel.Controls.Add(this.classBreaksRendererCtrl_0);
                this.proportionalSymbolRendererCtrl_0.Dock = DockStyle.Fill;
                this.proportionalSymbolRendererCtrl_0.Visible = false;
                this.panel.Controls.Add(this.proportionalSymbolRendererCtrl_0);
                this.dotDensityRendererCtrl_0.Dock = DockStyle.Fill;
                this.dotDensityRendererCtrl_0.Visible = false;
                this.panel.Controls.Add(this.dotDensityRendererCtrl_0);
                this.chartRendererCtrl_0.Dock = DockStyle.Fill;
                this.chartRendererCtrl_0.Visible = false;
                this.chartRendererCtrl_0.ChartRenderType = 0;
                this.panel.Controls.Add(this.chartRendererCtrl_0);
                this.chartRendererCtrl_1.Dock = DockStyle.Fill;
                this.chartRendererCtrl_1.Visible = false;
                this.chartRendererCtrl_1.ChartRenderType = 1;
                this.panel.Controls.Add(this.chartRendererCtrl_1);
                if (RepresentationAssist.HasRepresentation((this.ilayer_0 as IFeatureLayer).FeatureClass))
                {
                    TreeNode node = new TreeNode("制图表现");
                    this.representationRendererPage_0.CurrentLayer = this.ilayer_0;
                    this.representationRendererPage_0.Dock = DockStyle.Fill;
                    this.representationRendererPage_0.Visible = false;
                    this.panel.Controls.Add(this.representationRendererPage_0);
                    IFeatureClass featureClass = (this.ilayer_0 as IFeatureLayer).FeatureClass;
                    IRepresentationWorkspaceExtension repWSExtFromFClass = RepresentationAssist.GetRepWSExtFromFClass(featureClass);
                    this.representationRendererPage_0.RepresentationWorkspaceExtension = repWSExtFromFClass;
                    IEnumDatasetName name = repWSExtFromFClass.get_FeatureClassRepresentationNames(featureClass);
                    for (IDatasetName name2 = name.Next(); name2 != null; name2 = name.Next())
                    {
                        TreeNode node2 = new TreeNode(name2.Name) {
                            Tag = this.representationRendererPage_0
                        };
                        node.Nodes.Add(node2);
                    }
                    this.treeView1.Nodes.Add(node);
                }
            }
            else if (this.ilayer_0 is IRasterLayer)
            {
                this.treeView1.Nodes.AddRange(new TreeNode[] { new TreeNode("唯一值"), new TreeNode("分类"), new TreeNode("拉伸") });
                this.treeView1.Nodes[0].Tag = this.uniqueValueRendererCtrl_0;
                this.treeView1.Nodes[1].Tag = this.classBreaksRendererCtrl_0;
                this.uniqueValueRendererCtrl_0.Visible = false;
                this.uniqueValueRendererCtrl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.uniqueValueRendererCtrl_0);
                this.classBreaksRendererCtrl_0.Visible = false;
                this.classBreaksRendererCtrl_0.Dock = DockStyle.Fill;
                this.panel.Controls.Add(this.classBreaksRendererCtrl_0);
            }
        }

        private void method_1()
        {
            if (this.ilayer_0 != null)
            {
                IGeoFeatureLayer layer = this.ilayer_0 as IGeoFeatureLayer;
                if (layer.FeatureClass != null)
                {
                    if (layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolygon)
                    {
                        if (this.treeView1.Nodes[2].Nodes.Count == 2)
                        {
                            TreeNode node = new TreeNode("点密度渲染");
                            this.treeView1.Nodes[2].Nodes.Add(node);
                            node.Tag = this.dotDensityRendererCtrl_0;
                        }
                    }
                    else if (this.treeView1.Nodes[2].Nodes.Count == 3)
                    {
                        this.treeView1.Nodes[2].Nodes.RemoveAt(2);
                    }
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
                            if (((renderer as IUniqueValueRenderer).LookupStyleset != null) && ((renderer as IUniqueValueRenderer).LookupStyleset.Length > 0))
                            {
                                this.treeView1.SelectedNode = this.treeView1.Nodes[1].Nodes[2];
                                this.iuserControl_0 = this.matchStyleGrallyCtrl_0;
                                this.matchStyleGrallyCtrl_0.Visible = true;
                            }
                            else
                            {
                                this.treeView1.SelectedNode = this.treeView1.Nodes[1].Nodes[0];
                                this.iuserControl_0 = this.uniqueValueRendererCtrl_0;
                                this.uniqueValueRendererCtrl_0.Visible = true;
                            }
                        }
                    }
                    else if (renderer is IClassBreaksRenderer)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[2].Nodes[0];
                        this.iuserControl_0 = this.classBreaksRendererCtrl_0;
                        this.classBreaksRendererCtrl_0.Visible = true;
                    }
                    else if (renderer is IProportionalSymbolRenderer)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[2].Nodes[1];
                        this.iuserControl_0 = this.proportionalSymbolRendererCtrl_0;
                        this.proportionalSymbolRendererCtrl_0.Visible = true;
                    }
                    else if (renderer is IDotDensityRenderer)
                    {
                        this.treeView1.SelectedNode = this.treeView1.Nodes[2].Nodes[2];
                        this.iuserControl_0 = this.dotDensityRendererCtrl_0;
                        this.dotDensityRendererCtrl_0.Visible = true;
                    }
                    else if (!(renderer is IRepresentationRenderer))
                    {
                        if (renderer is IChartRenderer)
                        {
                            IChartRenderer renderer2 = renderer as IChartRenderer;
                            if (renderer2.ChartSymbol is IPieChartSymbol)
                            {
                                this.treeView1.SelectedNode = this.treeView1.Nodes[3].Nodes[0];
                                this.iuserControl_0 = this.chartRendererCtrl_0;
                                this.chartRendererCtrl_0.Visible = true;
                            }
                            else if (renderer2.ChartSymbol is IBarChartSymbol)
                            {
                                this.treeView1.SelectedNode = this.treeView1.Nodes[3].Nodes[1];
                                this.iuserControl_0 = this.chartRendererCtrl_1;
                                this.chartRendererCtrl_1.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        this.iuserControl_0 = this.representationRendererPage_0;
                        string name = ((renderer as IRepresentationRenderer).RepresentationClass as IDataset).Name;
                        TreeNode node2 = this.treeView1.Nodes[this.treeView1.Nodes.Count - 1];
                        for (int i = 0; i < node2.Nodes.Count; i++)
                        {
                            if (node2.Nodes[i].Text == name)
                            {
                                this.treeView1.SelectedNode = node2.Nodes[i];
                                break;
                            }
                        }
                        this.representationRendererPage_0.Visible = true;
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
                    else if (!(tag is UniqueValueRendererCtrl))
                    {
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
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        public ILayer Layer
        {
            set
            {
                this.ilayer_0 = value;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ilayer_0 = value as ILayer;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.simpleRenderControl_0.StyleGallery = value;
                this.uniqueValueRendererCtrl_0.StyleGallery = value;
                this.matchStyleGrallyCtrl_0.StyleGallery = value;
                this.classBreaksRendererCtrl_0.StyleGallery = value;
                this.proportionalSymbolRendererCtrl_0.StyleGallery = value;
                this.dotDensityRendererCtrl_0.StyleGallery = value;
                this.chartRendererCtrl_0.StyleGallery = value;
                this.chartRendererCtrl_1.StyleGallery = value;
                this.uniqueValueRendererMoreAttributeCtrl_0.StyleGallery = value;
            }
        }
    }
}

