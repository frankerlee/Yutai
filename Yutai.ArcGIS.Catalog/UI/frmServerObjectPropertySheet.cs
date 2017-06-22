using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmServerObjectPropertySheet : Form
    {
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private ServerObjectDocumentPropertyPage serverObjectDocumentPropertyPage_0 = new ServerObjectDocumentPropertyPage();
        private ServerObjectGeneralPropertyPage serverObjectGeneralPropertyPage_0 = new ServerObjectGeneralPropertyPage();
        private ServerObjectPoolPropertyPage serverObjectPoolPropertyPage_0 = new ServerObjectPoolPropertyPage();
        private ServerObjectProcessManagementPropertyPage serverObjectProcessManagementPropertyPage_0 = new ServerObjectProcessManagementPropertyPage();
        private ServerObjectSummaryPropertyPage serverObjectSummaryPropertyPage_0 = new ServerObjectSummaryPropertyPage();
        private string string_0 = "Started";

        public frmServerObjectPropertySheet()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

 private void frmServerObjectPropertySheet_Load(object sender, EventArgs e)
        {
            XtraTabPage page = new XtraTabPage {
                Text = "常规"
            };
            this.serverObjectGeneralPropertyPage_0.ServerObjectConfiguration = this.iserverObjectConfiguration_0;
            this.serverObjectGeneralPropertyPage_0.Status = this.string_0;
            this.serverObjectGeneralPropertyPage_0.AGSConnectionAdmin = this.iagsserverConnectionAdmin_0;
            this.serverObjectGeneralPropertyPage_0.Dock = DockStyle.Fill;
            page.Controls.Add(this.serverObjectGeneralPropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "参数"
            };
            this.serverObjectDocumentPropertyPage_0.Dock = DockStyle.Fill;
            this.serverObjectDocumentPropertyPage_0.ServerObjectConfiguration = this.iserverObjectConfiguration_0;
            this.serverObjectDocumentPropertyPage_0.Status = this.string_0;
            page.Controls.Add(this.serverObjectDocumentPropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "缓冲池"
            };
            this.serverObjectPoolPropertyPage_0.Dock = DockStyle.Fill;
            this.serverObjectPoolPropertyPage_0.ServerObjectConfiguration = this.iserverObjectConfiguration_0;
            this.serverObjectPoolPropertyPage_0.Status = this.string_0;
            page.Controls.Add(this.serverObjectPoolPropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "进程"
            };
            this.serverObjectProcessManagementPropertyPage_0.Dock = DockStyle.Fill;
            this.serverObjectProcessManagementPropertyPage_0.ServerObjectConfiguration = this.iserverObjectConfiguration_0;
            this.serverObjectProcessManagementPropertyPage_0.Status = this.string_0;
            page.Controls.Add(this.serverObjectProcessManagementPropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
        }

 public IAGSServerConnectionAdmin AGSConnectionAdmin
        {
            set
            {
                this.iagsserverConnectionAdmin_0 = value;
            }
        }

        public IServerObjectConfiguration ServerObjectConfig
        {
            set
            {
                this.iserverObjectConfiguration_0 = value;
            }
        }

        public string Status
        {
            set
            {
                this.string_0 = value;
            }
        }
    }
}

