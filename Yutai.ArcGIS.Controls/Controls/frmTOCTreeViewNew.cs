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
    public class frmTOCTreeViewNew : DockContent
    {
        private IContainer components = null;
        private IPageLayoutControl m_PageLayoutControl = null;
        private IMapControl2 m_pMainMapControl = null;
        internal TOCControl tocTreeView1;

        public event CurrentLayerChangedHandler CurrentLayerChanged;

        public frmTOCTreeViewNew()
        {
            this.InitializeComponent();
            this.tocTreeView1.tocTreeViewEx1.CurrentLayerChanged += new CurrentLayerChangedHandler(this.m_pTOCTreeViewWrap_CurrentLayerChanged);
        }

        public void BindControl(IPageLayoutControl pPageLayout, IMapControl2 pMapControl)
        {
            this.tocTreeView1.tocTreeViewEx1.Hook = pPageLayout;
            this.tocTreeView1.tocTreeViewEx1.SetMapCtrl(pMapControl);
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
            this.tocTreeView1.tocTreeViewEx1.RefreshTree();
        }

        private void frmTOCTreeView_OnPageLayoutReplaced(object newPageLayout)
        {
            this.tocTreeView1.tocTreeViewEx1.RefreshTree();
        }

        private void frmTOCTreeViewNew_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTOCTreeViewNew));
            this.tocTreeView1 = new TOCControl();
            base.SuspendLayout();
            this.tocTreeView1.AutoScroll = true;
            this.tocTreeView1.BackColor = SystemColors.ControlLightLight;
            this.tocTreeView1.Dock = DockStyle.Fill;
            this.tocTreeView1.Location = new Point(0, 0);
            this.tocTreeView1.Name = "tocTreeView1";
            this.tocTreeView1.Size = new Size(0x124, 0x111);
            this.tocTreeView1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x111);
            base.Controls.Add(this.tocTreeView1);
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.DockRight | DockAreas.DockLeft | DockAreas.Float;
            base.HideOnClose = true;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.Name = "frmTOCTreeViewNew";
            base.ShowHint = DockState.DockLeft;
            base.TabText = "图层树";
            this.Text = "图层树";
            base.Load += new EventHandler(this.frmTOCTreeViewNew_Load);
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
            this.tocTreeView1.tocTreeViewEx1.RefreshTree();
        }

        public IApplication Application
        {
            set
            {
                this.tocTreeView1.Application = value;
            }
        }

        public ILayer CurrentLayer
        {
            get
            {
                return this.tocTreeView1.tocTreeViewEx1.CurrentLayer;
            }
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
                    (this.m_pMainMapControl as IMapControlEvents2_Event).OnMapReplaced+=(new IMapControlEvents2_OnMapReplacedEventHandler(this.frmDatasourceTreeView_OnMapReplaced));
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
                    (this.m_PageLayoutControl as IPageLayoutControlEvents_Event).OnPageLayoutReplaced+=(new IPageLayoutControlEvents_OnPageLayoutReplacedEventHandler(this.frmTOCTreeView_OnPageLayoutReplaced));
                }
                this.tocTreeView1.tocTreeViewEx1.RefreshTree();
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.tocTreeView1.tocTreeViewEx1.StyleGallery = value;
            }
        }
    }
}

