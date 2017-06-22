using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmLayerPropertyExtend : Form
    {
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private object object_0 = null;

        public frmLayerPropertyExtend()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.tabControl1.Controls.Count; i++)
            {
                if (!((this.tabControl1.Controls[i] as TabPage).Controls[0] as ILayerAndStandaloneTablePropertyPage).Apply())
                {
                    return;
                }
            }
            base.DialogResult = DialogResult.OK;
        }

 private void frmLayerPropertyExtend_Load(object sender, EventArgs e)
        {
            this.tabControl1.Controls.Clear();
            ILayerAndStandaloneTablePropertyPage page = null;
            TabPage page2 = null;
            if (this.object_0 is ILayerExtensions)
            {
                int extensionCount = (this.object_0 as ILayerExtensions).ExtensionCount;
                for (int i = 0; i < extensionCount; i++)
                {
                    (this.object_0 as ILayerExtensions).get_Extension(i);
                }
            }
            if (this.object_0 is ILayer)
            {
                page2 = new TabPage();
                this.tabControl1.Controls.Add(page2);
                page2.Text = "常规";
                page = new LayerGeneralPropertyCtrl {
                    FocusMap = this.ibasicMap_0,
                    SelectItem = this.object_0
                };
                (page as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(page as Control);
                if (this.object_0 is IGeoFeatureLayer)
                {
                    page2 = new TabPage();
                    this.tabControl1.Controls.Add(page2);
                    page2.Text = "符号";
                    page = new LayerRenderCtrlExtend();
                    (page as LayerRenderCtrlExtend).StyleGallery = this.istyleGallery_0;
                    page.FocusMap = this.ibasicMap_0;
                    page.SelectItem = this.object_0;
                    (page as Control).Dock = DockStyle.Fill;
                    page2.Controls.Add(page as Control);
                    page2 = new TabPage {
                        Text = "定义查询"
                    };
                    page = new LayerDefinitionExpressionCtrl {
                        FocusMap = this.ibasicMap_0,
                        SelectItem = this.object_0
                    };
                    (page as Control).Dock = DockStyle.Fill;
                    if (this.ibasicMap_0 is IMap)
                    {
                        page2 = new TabPage();
                        this.tabControl1.Controls.Add(page2);
                        page2.Text = "标注";
                        page = new LayerLabelPropertyCtrl {
                            FocusMap = this.ibasicMap_0,
                            SelectItem = this.object_0
                        };
                        (page as Control).Dock = DockStyle.Fill;
                        page2.Controls.Add(page as Control);
                    }
                }
                else if (this.object_0 is IRasterLayer)
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
            if (this.object_0 is ILayerFields)
            {
                page2 = new TabPage();
                this.tabControl1.Controls.Add(page2);
                page2.Text = "字段";
                page = new LayerFieldsPageExtend {
                    FocusMap = this.ibasicMap_0,
                    SelectItem = this.object_0
                };
                (page as Control).Dock = DockStyle.Fill;
                page2.Controls.Add(page as Control);
            }
        }

 public IBasicMap FocusMap
        {
            set
            {
                this.ibasicMap_0 = value;
            }
        }

        internal ILayer Layer
        {
            set
            {
            }
        }

        public object SelectItem
        {
            set
            {
                this.object_0 = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.istyleGallery_0 = value;
            }
        }
    }
}

