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
    public partial class frmDatasourceTreeView : DockContent
    {
        private IPageLayoutControl m_PageLayoutControl = null;
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

 private void frmDatasourceTreeView_OnMapReplaced(object newMap)
        {
            this.m_pDataSourceTreeView.RefreshTree();
        }

        private void frmTOCTreeView_OnPageLayoutReplaced(object newPageLayout)
        {
            this.m_pDataSourceTreeView.RefreshTree();
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

