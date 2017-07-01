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

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmTOCTreeView : DockContent
    {
        private IPageLayoutControl m_PageLayoutControl = null;
        private IMapControl2 m_pMainMapControl = null;
        internal TOCTreeView tocTreeView1;

        public event CurrentLayerChangedHandler CurrentLayerChanged;

        public frmTOCTreeView()
        {
            this.InitializeComponent();
            this.m_pTOCTreeViewWrap = new TOCTreeViewWrapEx(this.tocTreeView1);
            this.m_pTOCTreeViewWrap.CurrentLayerChanged +=
                new CurrentLayerChangedHandler(this.m_pTOCTreeViewWrap_CurrentLayerChanged);
        }

        public void BindControl(IPageLayoutControl pPageLayout, IMapControl2 pMapControl)
        {
            this.m_pTOCTreeViewWrap.Hook = pPageLayout;
            this.m_pTOCTreeViewWrap.SetMapCtrl(pMapControl);
        }

        private void frmDatasourceTreeView_OnMapReplaced(object newMap)
        {
            this.m_pTOCTreeViewWrap.RefreshTree();
        }

        private void frmTOCTreeView_OnPageLayoutReplaced(object newPageLayout)
        {
            this.m_pTOCTreeViewWrap.RefreshTree();
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
            this.m_pTOCTreeViewWrap.RefreshTree();
        }

        public IApplication Application
        {
            set { this.m_pTOCTreeViewWrap.Application = value; }
        }

        public ILayer CurrentLayer
        {
            get { return this.m_pTOCTreeViewWrap.CurrentLayer; }
        }

        public IMapControl2 MainMapControl
        {
            set
            {
                this.m_PageLayoutControl = null;
                this.m_pMainMapControl = value;
                this.m_pTOCTreeViewWrap.Hook = this.m_pMainMapControl;
                if (this.m_pMainMapControl != null)
                {
                    (this.m_pMainMapControl as IMapControlEvents2_Event).OnMapReplaced +=
                        (new IMapControlEvents2_OnMapReplacedEventHandler(this.frmDatasourceTreeView_OnMapReplaced));
                }
                this.m_pTOCTreeViewWrap.RefreshTree();
            }
        }

        public IPageLayoutControl PageLayoutControl
        {
            set
            {
                this.m_PageLayoutControl = value;
                this.m_pTOCTreeViewWrap.Hook = this.m_PageLayoutControl;
                if (this.m_PageLayoutControl != null)
                {
                    (this.m_PageLayoutControl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced +=
                    (new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(
                        this.frmTOCTreeView_OnPageLayoutReplaced));
                }
                this.m_pTOCTreeViewWrap.RefreshTree();
            }
        }

        public IStyleGallery StyleGallery
        {
            set { this.m_pTOCTreeViewWrap.StyleGallery = value; }
        }
    }
}