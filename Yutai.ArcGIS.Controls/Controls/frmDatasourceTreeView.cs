using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.Controls.TOCDisplay;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class frmDatasourceTreeView : DockContent
    {
        private IContainer components = null;
        private IPageLayoutControl m_PageLayoutControl = null;
        private DataSourceTreeView m_pDataSourceTreeView;
        private IMapControl2 m_pMainMapControl = null;
        internal TOCTreeView tocTreeView1;

        public event CurrentLayerChangedHandler CurrentLayerChanged;

        public frmDatasourceTreeView()
        {
            this.InitializeComponent();
            this.m_pDataSourceTreeView = new DataSourceTreeView(this.tocTreeView1);
            this.tocTreeView1.AfterSelect += new TOCTreeView.AfterSelectEventHandler(this.tocTreeView1_AfterSelect);
        }

        public void BindControl(IPageLayoutControl pPageLayout, IMapControl2 pMapControl)
        {
            this.m_pDataSourceTreeView.Hook = pPageLayout;
            this.m_pDataSourceTreeView.SetMapCtrl(pMapControl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmDatasourceTreeView_OnMapReplaced(object newMap)
        {
            this.m_pDataSourceTreeView.RefreshTree();
        }

        private void frmTOCTreeView_OnPageLayoutReplaced(object newPageLayout)
        {
            this.m_pDataSourceTreeView.RefreshTree();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatasourceTreeView));
            this.tocTreeView1 = new TOCTreeView();
            base.SuspendLayout();
            this.tocTreeView1.AutoScroll = true;
            this.tocTreeView1.BackColor = SystemColors.ControlLightLight;
            this.tocTreeView1.CanDrag = true;
            this.tocTreeView1.Dock = DockStyle.Fill;
            this.tocTreeView1.Indent = 14;
            this.tocTreeView1.Location = new Point(0, 0);
            this.tocTreeView1.Name = "tocTreeView1";
            this.tocTreeView1.SelectedNode = null;
            this.tocTreeView1.ShowLines = false;
            this.tocTreeView1.ShowPlusMinus = true;
            this.tocTreeView1.ShowRootLines = false;
            this.tocTreeView1.Size = new Size(0x124, 0x111);
            this.tocTreeView1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x111);
            base.Controls.Add(this.tocTreeView1);
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.DockRight | DockAreas.DockLeft | DockAreas.Float;
            base.HideOnClose = true;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmDatasourceTreeView";
            base.ShowHint = DockState.DockLeft;
            base.TabText = "数据源";
            this.Text = "数据源";
            base.ResumeLayout(false);
        }

        public void RefreshTree()
        {
            this.m_pDataSourceTreeView.RefreshTree();
        }

        private void tocTreeView1_AfterSelect(TOCTreeNode pSelectNode)
        {
        }

        public IApplication Application
        {
            set
            {
                this.m_pDataSourceTreeView.Application = value;
            }
        }

        public IMapControl2 MainMapControl
        {
            set
            {
                try
                {
                    this.m_PageLayoutControl = null;
                    this.m_pMainMapControl = value;
                    this.m_pDataSourceTreeView.Hook = this.m_pMainMapControl;
                    if (this.m_pMainMapControl != null)
                    {
                        (this.m_pMainMapControl as IMapControlEvents2_Event).OnMapReplaced+=(new IMapControlEvents2_OnMapReplacedEventHandler(this.frmDatasourceTreeView_OnMapReplaced));
                    }
                    this.m_pDataSourceTreeView.RefreshTree();
                }
                catch
                {
                }
            }
        }

        public IPageLayoutControl PageLayoutControl
        {
            set
            {
                this.m_PageLayoutControl = value;
                this.m_pDataSourceTreeView.Hook = this.m_PageLayoutControl;
                if (this.m_PageLayoutControl != null)
                {
                    (this.m_PageLayoutControl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced+=(new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(this.frmTOCTreeView_OnPageLayoutReplaced));
                }
                this.m_pDataSourceTreeView.RefreshTree();
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pDataSourceTreeView.StyleGallery = value;
            }
        }
    }
}

