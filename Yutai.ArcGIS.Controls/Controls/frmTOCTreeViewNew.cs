using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.Controls.TOCDisplay;
using Yutai.ArcGIS.Framework.Docking;
using TOCControl = Yutai.ArcGIS.Controls.Controls.TOCTreeview.TOCControl;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmTOCTreeViewNew : DockContent
    {
        private IPageLayoutControl m_PageLayoutControl = null;
        private IMapControl2 m_pMainMapControl = null;
        internal TOCControl tocTreeView1;

        public event CurrentLayerChangedHandler CurrentLayerChanged;

        public frmTOCTreeViewNew()
        {
            this.InitializeComponent();
            this.tocTreeView1.tocTreeViewEx1.CurrentLayerChanged +=
                new CurrentLayerChangedHandler(this.m_pTOCTreeViewWrap_CurrentLayerChanged);
        }

        public void BindControl(IPageLayoutControl pPageLayout, IMapControl2 pMapControl)
        {
            this.tocTreeView1.tocTreeViewEx1.Hook = pPageLayout;
            this.tocTreeView1.tocTreeViewEx1.SetMapCtrl(pMapControl);
        }

        private void frmDatasourceTreeView_OnMapReplaced(object newMap)
        {
            this.tocTreeView1.tocTreeViewEx1.RefreshTree();
        }

        private void frmTOCTreeView_OnPageLayoutReplaced(object newPageLayout)
        {
            this.tocTreeView1.tocTreeViewEx1.RefreshTree();
        }

        private void frmTOCTreeViewNew_Load(object sender, EventArgs e)
        {
        }

        private void m_pTOCTreeViewWrap_CurrentLayerChanged(object sender, EventArgs e)
        {
            if (this.CurrentLayerChanged != null)
            {
                this.CurrentLayerChanged(this, e);
            }
        }

        public void RefreshTree()
        {
            this.tocTreeView1.tocTreeViewEx1.RefreshTree();
        }

        public IApplication Application
        {
            set { this.tocTreeView1.Application = value; }
        }

        public ILayer CurrentLayer
        {
            get { return this.tocTreeView1.tocTreeViewEx1.CurrentLayer; }
        }

        public IMapControl2 MainMapControl
        {
            set
            {
                this.m_PageLayoutControl = null;
                this.m_pMainMapControl = value;
                this.tocTreeView1.Hook = this.m_pMainMapControl;
                if (this.m_pMainMapControl != null)
                {
                    (this.m_pMainMapControl as IMapControlEvents2_Event).OnMapReplaced +=
                        (new IMapControlEvents2_OnMapReplacedEventHandler(this.frmDatasourceTreeView_OnMapReplaced));
                }
                this.tocTreeView1.tocTreeViewEx1.RefreshTree();
            }
        }

        public IPageLayoutControl PageLayoutControl
        {
            set
            {
                this.m_PageLayoutControl = value;
                this.tocTreeView1.Hook = this.m_PageLayoutControl;
                if (this.m_PageLayoutControl != null)
                {
                    (this.m_PageLayoutControl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced +=
                    (new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(
                        this.frmTOCTreeView_OnPageLayoutReplaced));
                }
                this.tocTreeView1.tocTreeViewEx1.RefreshTree();
            }
        }

        public IStyleGallery StyleGallery
        {
            set { this.tocTreeView1.tocTreeViewEx1.StyleGallery = value; }
        }
    }
}