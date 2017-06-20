using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmLayerPropertyExtend : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private IStyleGallery istyleGallery_0 = null;
        private object object_0 = null;
        private Panel panel1;
        private Panel panel2;
        private TabControl tabControl1;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerPropertyExtend));
            this.panel1 = new Panel();
            this.tabControl1 = new TabControl();
            this.panel2 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x26d, 0x176);
            this.panel1.TabIndex = 0;
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x26d, 0x176);
            this.tabControl1.TabIndex = 0;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(0, 0x178);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x26d, 0x20);
            this.panel2.TabIndex = 1;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x1e8, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnOK.Location = new Point(0x1a8, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x26d, 0x198);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmLayerPropertyEx";
            this.Text = "图层属性";
            base.Load += new EventHandler(this.frmLayerPropertyExtend_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
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

