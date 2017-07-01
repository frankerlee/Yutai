using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmAGSProperty : Form
    {
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;

        public frmAGSProperty()
        {
            this.InitializeComponent();
        }

        private void frmAGSProperty_Load(object sender, EventArgs e)
        {
            XtraTabPage page = new XtraTabPage
            {
                Text = "目录"
            };
            Control control = new ServerDirectoryPropertyPage();
            (control as ServerDirectoryPropertyPage).AGSServerConnectionAdmin = this.iagsserverConnectionAdmin_0;
            page.Controls.Add(control);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage
            {
                Text = "计算机"
            };
            control = new ServerHostPropertyPage();
            (control as ServerHostPropertyPage).AGSServerConnectionAdmin = this.iagsserverConnectionAdmin_0;
            page.Controls.Add(control);
            this.xtraTabControl1.TabPages.Add(page);
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin
        {
            set { this.iagsserverConnectionAdmin_0 = value; }
        }
    }
}