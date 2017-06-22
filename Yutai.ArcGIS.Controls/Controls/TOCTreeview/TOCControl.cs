using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.Controls.TOCDisplay;

namespace Yutai.ArcGIS.Controls.Controls.TOCTreeview
{
    public partial class TOCControl : UserControl
    {
        internal TOCTreeViewEx tocTreeViewEx1;

        public TOCControl()
        {
            this.InitializeComponent();
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

