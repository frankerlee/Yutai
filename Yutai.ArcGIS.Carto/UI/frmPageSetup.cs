using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmPageSetup : Form
    {
        private Container container_0 = null;
        protected IMapFrame m_pMapFrame = null;
        private PageSetupControl pageSetupControl_0 = new PageSetupControl();

        public frmPageSetup()
        {
            this.InitializeComponent();
            this.pageSetupControl_0.Dock = DockStyle.Fill;
            this.panel.Controls.Add(this.pageSetupControl_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.pageSetupControl_0.Do();
        }

 private void frmPageSetup_Load(object sender, EventArgs e)
        {
        }

 public IMapFrame MapFrame
        {
            set
            {
                this.pageSetupControl_0.MapFrame = value;
            }
        }

        public IPageLayout PageLayout
        {
            set
            {
                this.pageSetupControl_0.PageLayout = value;
            }
        }
    }
}

