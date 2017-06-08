using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Controls.Controls.TOCDisplay;

namespace Yutai.ArcGIS.Controls.Controls.TOCTreeview
{
    public class TOCControl : UserControl
    {
        private IContainer components = null;
        internal TOCTreeViewEx tocTreeViewEx1;
        private ToolStrip toolbar;
        private ToolStripButton toolDrawDBConfig;
        private ToolStripButton toolDrawOrder;
        private ToolStripButton toolDrawSorce;
        private ToolStripButton toolXMLConfig;

        public TOCControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TOCControl));
            this.toolbar = new ToolStrip();
            this.toolDrawOrder = new ToolStripButton();
            this.toolDrawSorce = new ToolStripButton();
            this.toolDrawDBConfig = new ToolStripButton();
            this.tocTreeViewEx1 = new TOCTreeViewEx();
            this.toolXMLConfig = new ToolStripButton();
            this.toolbar.SuspendLayout();
            base.SuspendLayout();
            this.toolbar.Items.AddRange(new ToolStripItem[] { this.toolDrawOrder, this.toolDrawSorce, this.toolDrawDBConfig, this.toolXMLConfig });
            this.toolbar.Location = new Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new Size(0x155, 0x19);
            this.toolbar.TabIndex = 0;
            this.toolbar.Text = "按绘制顺序列出";
            this.toolDrawOrder.Checked = true;
            this.toolDrawOrder.CheckState = CheckState.Checked;
            this.toolDrawOrder.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolDrawOrder.Image = (Image) resources.GetObject("toolDrawOrder.Image");
            this.toolDrawOrder.ImageTransparentColor = Color.Magenta;
            this.toolDrawOrder.Name = "toolDrawOrder";
            this.toolDrawOrder.Size = new Size(0x17, 0x16);
            this.toolDrawOrder.Text = "按图层顺序";
            this.toolDrawOrder.Click += new EventHandler(this.toolDrawOrder_Click);
            this.toolDrawSorce.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolDrawSorce.Image = (Image) resources.GetObject("toolDrawSorce.Image");
            this.toolDrawSorce.ImageTransparentColor = Color.Magenta;
            this.toolDrawSorce.Name = "toolDrawSorce";
            this.toolDrawSorce.Size = new Size(0x17, 0x16);
            this.toolDrawSorce.Text = "按源绘制";
            this.toolDrawSorce.Click += new EventHandler(this.toolDrawSorce_Click);
            this.toolDrawDBConfig.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolDrawDBConfig.Image = (Image) resources.GetObject("toolDrawDBConfig.Image");
            this.toolDrawDBConfig.ImageTransparentColor = Color.Magenta;
            this.toolDrawDBConfig.Name = "toolDrawDBConfig";
            this.toolDrawDBConfig.Size = new Size(0x17, 0x16);
            this.toolDrawDBConfig.Text = "数据库配置结构";
            this.toolDrawDBConfig.ToolTipText = "数据库配置结构";
            this.toolDrawDBConfig.Visible = false;
            this.toolDrawDBConfig.Click += new EventHandler(this.toolDrawDBConfig_Click);
            this.tocTreeViewEx1.BackColor = SystemColors.Window;
            this.tocTreeViewEx1.CanDrag = true;
            this.tocTreeViewEx1.CanEditStyle = true;
            this.tocTreeViewEx1.Dock = DockStyle.Fill;
            this.tocTreeViewEx1.Indent = 0x10;
            this.tocTreeViewEx1.Location = new Point(0, 0x19);
            this.tocTreeViewEx1.Name = "tocTreeViewEx1";
            this.tocTreeViewEx1.SelectedNode = null;
            this.tocTreeViewEx1.ShowLines = false;
            this.tocTreeViewEx1.ShowPlusMinus = false;
            this.tocTreeViewEx1.ShowRootLines = false;
            this.tocTreeViewEx1.Size = new Size(0x155, 0x13e);
            this.tocTreeViewEx1.StyleGallery = null;
            this.tocTreeViewEx1.TabIndex = 1;
            this.tocTreeViewEx1.TOCTreeViewType = TOCTreeViewType.TOCTree;
            this.toolXMLConfig.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolXMLConfig.Image = (Image) resources.GetObject("toolXMLConfig.Image");
            this.toolXMLConfig.ImageTransparentColor = Color.Magenta;
            this.toolXMLConfig.Name = "toolXMLConfig";
            this.toolXMLConfig.Size = new Size(0x17, 0x16);
            this.toolXMLConfig.Text = "XML配置结构";
            this.toolXMLConfig.Visible = false;
            this.toolXMLConfig.Click += new EventHandler(this.toolXMLConfig_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.tocTreeViewEx1);
            base.Controls.Add(this.toolbar);
            base.Name = "TOCControl";
            base.Size = new Size(0x155, 0x157);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void SetMapCtrl(object MapCtrl)
        {
            this.tocTreeViewEx1.SetMapCtrl(MapCtrl);
        }

        private void toolDrawDBConfig_Click(object sender, EventArgs e)
        {
            if (!this.toolDrawDBConfig.Checked)
            {
                this.toolDrawOrder.Checked = false;
                this.toolDrawSorce.Checked = false;
                this.toolDrawDBConfig.Checked = true;
                this.toolXMLConfig.Checked = false;
                this.tocTreeViewEx1.TOCTreeViewType = TOCTreeViewType.DBConfigTree;
                this.tocTreeViewEx1.RefreshTree();
            }
        }

        private void toolDrawOrder_Click(object sender, EventArgs e)
        {
            if (!this.toolDrawOrder.Checked)
            {
                this.toolDrawSorce.Checked = false;
                this.toolDrawOrder.Checked = true;
                this.toolDrawDBConfig.Checked = false;
                this.toolXMLConfig.Checked = false;
                this.tocTreeViewEx1.TOCTreeViewType = TOCTreeViewType.TOCTree;
                this.tocTreeViewEx1.RefreshTree();
            }
        }

        private void toolDrawSorce_Click(object sender, EventArgs e)
        {
            if (!this.toolDrawSorce.Checked)
            {
                this.toolDrawOrder.Checked = false;
                this.toolDrawSorce.Checked = true;
                this.toolDrawDBConfig.Checked = false;
                this.toolXMLConfig.Checked = false;
                this.tocTreeViewEx1.TOCTreeViewType = TOCTreeViewType.DSTree;
                this.tocTreeViewEx1.RefreshTree();
            }
        }

        private void toolXMLConfig_Click(object sender, EventArgs e)
        {
            if (!this.toolDrawDBConfig.Checked)
            {
                this.toolDrawOrder.Checked = false;
                this.toolDrawSorce.Checked = false;
                this.toolDrawDBConfig.Checked = false;
                this.toolXMLConfig.Checked = true;
                this.tocTreeViewEx1.TOCTreeViewType = TOCTreeViewType.XMLConfigTree;
                this.tocTreeViewEx1.RefreshTree();
            }
        }

        public IApplication Application
        {
            set
            {
                this.tocTreeViewEx1.Application = value;
            }
        }

        public object Hook
        {
            set
            {
                this.tocTreeViewEx1.Hook = value;
                this.tocTreeViewEx1.RefreshTree();
            }
        }

        public ITable LayerConfigTable
        {
            set
            {
                this.tocTreeViewEx1.LayerConfigTable = value;
                if (value != null)
                {
                    this.toolDrawDBConfig.Visible = true;
                }
            }
        }

        public TOCTreeViewEx TocTreeView
        {
            get
            {
                return this.tocTreeViewEx1;
            }
        }
    }
}

