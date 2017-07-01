using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Location;
using Yutai.ArcGIS.Common.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmLayerPropertyEx : Form
    {
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private object object_0 = null;

        public frmLayerPropertyEx()
        {
            if (!CartoLicenseProviderCheck.Check())
            {
                base.Close();
            }
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.tabControl1.Controls.Count; i++)
            {
                if (
                    !((this.tabControl1.Controls[i] as TabPage).Controls[0] as ILayerAndStandaloneTablePropertyPage)
                        .Apply())
                {
                    return;
                }
            }
            if (this.ibasicMap_0 is IScene)
            {
                ILayerExtensions extensions = this.object_0 as ILayerExtensions;
                for (int j = 0; j <= (extensions.ExtensionCount - 1); j++)
                {
                    if (extensions.get_Extension(j) is I3DProperties)
                    {
                        (extensions.get_Extension(j) as I3DProperties).Apply3DProperties(this.object_0);
                        break;
                    }
                }
            }
            base.DialogResult = DialogResult.OK;
        }

        private void frmLayerPropertyEx_Load(object sender, EventArgs e)
        {
            int num2;
            this.tabControl1.Controls.Clear();
            ILayerAndStandaloneTablePropertyPage page = null;
            TabPage page2 = null;
            if (this.object_0 is ILayerExtensions)
            {
                int extensionCount = (this.object_0 as ILayerExtensions).ExtensionCount;
                for (num2 = 0; num2 < extensionCount; num2++)
                {
                    (this.object_0 as ILayerExtensions).get_Extension(num2);
                }
            }
            if (this.object_0 is IGroupLayer)
            {
                page2 = new TabPage();
                this.tabControl1.Controls.Add(page2);
                page2.Text = "常规";
                page = new LayerGeneralPropertyCtrl
                {
                    FocusMap = this.ibasicMap_0,
                    SelectItem = this.object_0
                };
                (page as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(page as Control);
                page2 = new TabPage();
                this.tabControl1.Controls.Add(page2);
                page2.Text = "组合";
                page = new GroupLayerPropertyPage
                {
                    FocusMap = this.ibasicMap_0,
                    SelectItem = this.object_0
                };
                (page as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(page as Control);
            }
            else if (this.object_0 is IBasemapSubLayer)
            {
                page2 = new TabPage();
                this.tabControl1.Controls.Add(page2);
                page2.Text = "常规";
                page = new LayerGeneralPropertyCtrl
                {
                    FocusMap = this.ibasicMap_0,
                    SelectItem = this.object_0
                };
                (page as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(page as Control);
            }
            else
            {
                if (this.object_0 is ILayer)
                {
                    page2 = new TabPage();
                    this.tabControl1.Controls.Add(page2);
                    page2.Text = "常规";
                    page = new LayerGeneralPropertyCtrl
                    {
                        FocusMap = this.ibasicMap_0,
                        SelectItem = this.object_0
                    };
                    (page as Control).Dock = DockStyle.Fill;
                    page2.Controls.Add(page as Control);
                    if (this.object_0 is IDataLayer)
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "数据源";
                        page = new LayerDataSourcePropertyPage
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                    }
                    if (this.object_0 is IFeatureSelection)
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "选择集";
                        page = new FeatureSelectionSetCtrl();
                        (page as FeatureSelectionSetCtrl).StyleGallery = this.istyleGallery_0;
                        page.FocusMap = this.ibasicMap_0;
                        page.SelectItem = this.object_0;
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                    }
                    else if (this.object_0 is ITopologyErrorSelection)
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "选择集";
                        page = new TopologyErrorSelectionCtrl
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                    }
                    if (this.object_0 is ICadLayer)
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "图层";
                        page = new CADDrawingLayersPropertyPage
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "变换";
                        page = new CADTransformationPropertyPage
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                    }
                    else if (!(this.object_0 is ICompositeGraphicsLayer))
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "显示";
                        page = new LayerDisplaySetCtrl
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                    }
                    if (this.object_0 is ITopologyLayer)
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "符号";
                        page = new ToplogyLayerSymbolCtrl
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as ToplogyLayerSymbolCtrl).StyleGallery = this.istyleGallery_0;
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "要素类";
                        page = new TopologyClassesPropertyPage
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "规则";
                        page = new TopologyRulesPropertyPage
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                    }
                    if (!(this.object_0 is IGeoFeatureLayer))
                    {
                        if (this.object_0 is IRasterLayer)
                        {
                            page2 = new TabPage();
                            this.tabControl1.Controls.Add(page2);
                            page2.Text = "符号";
                            page = new RasterRenderPropertyPage();
                            (page as RasterRenderPropertyPage).StyleGallery = this.istyleGallery_0;
                            page.FocusMap = this.ibasicMap_0;
                            page.SelectItem = this.object_0;
                            (page as Control).Dock = DockStyle.Fill;
                            page2.Controls.Add(page as Control);
                        }
                    }
                    else
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "符号";
                        page = new LayerRenderCtrl();
                        (page as LayerRenderCtrl).StyleGallery = this.istyleGallery_0;
                        page.FocusMap = this.ibasicMap_0;
                        page.SelectItem = this.object_0;
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "定义查询";
                        page = new LayerDefinitionExpressionCtrl
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                        if (this.ibasicMap_0 is IMap)
                        {
                            page2 = new TabPage();
                            this.tabControl1.Controls.Add(page2);
                            page2.Text = "标注";
                            page = new LayerLabelPropertyCtrl
                            {
                                FocusMap = this.ibasicMap_0,
                                SelectItem = this.object_0
                            };
                            (page as Control).Dock = DockStyle.Fill;
                            page2.Controls.Add(page as Control);
                        }
                        if (this.object_0 is ILayerExtensions)
                        {
                            ILayerExtensions extensions = this.object_0 as ILayerExtensions;
                            for (num2 = 0; num2 < extensions.ExtensionCount; num2++)
                            {
                                if (extensions.get_Extension(num2) is IHatchLayerExtension)
                                {
                                    page2 = new TabPage();
                                    this.tabControl1.Controls.Add(page2);
                                    page2.Text = "刻度线";
                                    page = new HatchLayerExtensionPropertyPage
                                    {
                                        FocusMap = this.ibasicMap_0,
                                        SelectItem = this.object_0
                                    };
                                    (page as Control).Dock = DockStyle.Fill;
                                    page2.Controls.Add(page as Control);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (this.object_0 is IStandaloneTable)
                {
                    page2 = new TabPage();
                    this.tabControl1.Controls.Add(page2);
                    page2.Text = "定义查询";
                    page = new LayerDefinitionExpressionCtrl
                    {
                        FocusMap = this.ibasicMap_0,
                        SelectItem = this.object_0
                    };
                    (page as Control).Dock = DockStyle.Fill;
                    page2.Controls.Add(page as Control);
                }
                if (!(this.object_0 is ICadLayer) && !(this.object_0 is ITopologyLayer))
                {
                    if (this.object_0 is ITinLayer)
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "符号";
                        page = new TinLayerRenderPropertyPage
                        {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                    }
                    else if (this.object_0 is ILayerFields)
                    {
                        try
                        {
                            if ((this.object_0 as ILayerFields).FieldCount > 0)
                            {
                                page2 = new TabPage();
                                this.tabControl1.Controls.Add(page2);
                                page2.Text = "连接和关联";
                                page = new JoiningAndRelatingPropertyPage();
                                (page as JoiningAndRelatingPropertyPage).OnJoinAndRelationChange +=
                                    new OnJoinAndRelationChangeHandler(this.method_0);
                                page.FocusMap = this.ibasicMap_0;
                                page.SelectItem = this.object_0;
                                (page as Control).Dock = DockStyle.Fill;
                                page2.Controls.Add(page as Control);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                if (this.object_0 is ILayerFields)
                {
                    page2 = new TabPage();
                    this.tabControl1.Controls.Add(page2);
                    page2.Text = "字段";
                    page = new LayerFieldsPage
                    {
                        FocusMap = this.ibasicMap_0,
                        SelectItem = this.object_0
                    };
                    (page as Control).Dock = DockStyle.Fill;
                    page2.Controls.Add(page as Control);
                }
            }
            if ((this.ibasicMap_0 is IScene) || this.method_1(this.object_0 as ILayer))
            {
                page2 = new TabPage();
                this.tabControl1.Controls.Add(page2);
                page2.Text = "基准高程";
                page = new BaseHeightPropertyPage
                {
                    FocusMap = this.ibasicMap_0,
                    SelectItem = this.object_0
                };
                (page as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(page as Control);
                page2 = new TabPage();
                this.tabControl1.Controls.Add(page2);
                page = new SceneRenderPropertyPage();
                page2.Text = (page as SceneRenderPropertyPage).Text;
                page.FocusMap = this.ibasicMap_0;
                page.SelectItem = this.object_0;
                (page as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(page as Control);
                if (this.object_0 is IFeatureLayer)
                {
                    page2 = new TabPage();
                    this.tabControl1.Controls.Add(page2);
                    page = new ExtrusionPropertyPage();
                    page2.Text = (page as ExtrusionPropertyPage).Text;
                    page.FocusMap = this.ibasicMap_0;
                    page.SelectItem = this.object_0;
                    (page as Control).Dock = DockStyle.Fill;
                    page2.Controls.Add(page as Control);
                }
            }
        }

        private void method_0()
        {
        }

        private bool method_1(ILayer ilayer_0)
        {
            ILayerExtensions extensions = ilayer_0 as ILayerExtensions;
            if (extensions != null)
            {
                for (int i = 0; i < extensions.ExtensionCount; i++)
                {
                    if (extensions.get_Extension(i) is I3DProperties)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public IBasicMap FocusMap
        {
            set { this.ibasicMap_0 = value; }
        }

        internal ILayer Layer
        {
            set { }
        }

        public object SelectItem
        {
            set { this.object_0 = value; }
        }

        public IStyleGallery StyleGallery
        {
            set { this.istyleGallery_0 = value; }
        }
    }
}