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
    public class frmTOCTreeView : DockContent
    {
        private IContainer components = null;
        private IPageLayoutControl m_PageLayoutControl = null;
        private IMapControl2 m_pMainMapControl = null;
        private TOCTreeViewWrapEx m_pTOCTreeViewWrap;
        internal TOCTreeView tocTreeView1;

        public event CurrentLayerChangedHandler CurrentLayerChanged;

        public frmTOCTreeView()
        {
            this.InitializeComponent();
            this.m_pTOCTreeViewWrap = new TOCTreeViewWrapEx(this.tocTreeView1);
            this.m_pTOCTreeViewWrap.CurrentLayerChanged += new CurrentLayerChangedHandler(this.m_pTOCTreeViewWrap_CurrentLayerChanged);
        }

        public void BindControl(IPageLayoutControl pPageLayout, IMapControl2 pMapControl)
        {
            this.m_pTOCTreeViewWrap.Hook = pPageLayout;
            this.m_pTOCTreeViewWrap.SetMapCtrl(pMapControl);
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
            this.m_pTOCTreeViewWrap.RefreshTree();
        }

        private void frmTOCTreeView_OnPageLayoutReplaced(object newPageLayout)
        {
            this.m_pTOCTreeViewWrap.RefreshTree();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTOCTreeView));
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
            base.Name = "frmTOCTreeView";
            base.ShowHint = DockState.DockLeft;
            base.TabText = "图层树";
            this.Text = "图层树";
            base.ResumeLayout(false);
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
            set
            {
                this.m_pTOCTreeViewWrap.Application = value;
            }
        }

        public ILayer CurrentLayer
        {
            get
            {
                return this.m_pTOCTreeViewWrap.CurrentLayer;
            }
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
                    (this.m_pMainMapControl as IMapControlEvents2_Event).OnMapReplaced+=(new IMapControlEvents2_OnMapReplacedEventHandler(this.frmDatasourceTreeView_OnMapReplaced));
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
                    (this.m_PageLayoutControl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced+=(new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(this.frmTOCTreeView_OnPageLayoutReplaced));
                }
                this.m_pTOCTreeViewWrap.RefreshTree();
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pTOCTreeViewWrap.StyleGallery = value;
            }
        }
    }
}

