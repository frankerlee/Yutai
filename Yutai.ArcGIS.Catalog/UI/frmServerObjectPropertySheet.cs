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
    public class frmServerObjectPropertySheet : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private Panel panel1;
        private Panel panel2;
        private ServerObjectDocumentPropertyPage serverObjectDocumentPropertyPage_0 = new ServerObjectDocumentPropertyPage();
        private ServerObjectGeneralPropertyPage serverObjectGeneralPropertyPage_0 = new ServerObjectGeneralPropertyPage();
        private ServerObjectPoolPropertyPage serverObjectPoolPropertyPage_0 = new ServerObjectPoolPropertyPage();
        private ServerObjectProcessManagementPropertyPage serverObjectProcessManagementPropertyPage_0 = new ServerObjectProcessManagementPropertyPage();
        private ServerObjectSummaryPropertyPage serverObjectSummaryPropertyPage_0 = new ServerObjectSummaryPropertyPage();
        private SimpleButton simpleButton3;
        private string string_0 = "Started";
        private XtraTabControl xtraTabControl1;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServerObjectPropertySheet));
            this.simpleButton3 = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2 = new Panel();
            this.xtraTabControl1 = new XtraTabControl();
            this.panel1 = new Panel();
            this.panel2.SuspendLayout();
            this.xtraTabControl1.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.simpleButton3.DialogResult = DialogResult.Cancel;
            this.simpleButton3.Location = new Point(0x170, 8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(80, 0x18);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "取消";
            this.btnCancel.Location = new Point(0x110, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(80, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "应用";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xb0, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(80, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel2.Controls.Add(this.xtraTabControl1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1f8, 330);
            this.panel2.TabIndex = 1;
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(0x1f8, 330);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.simpleButton3);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 290);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1f8, 40);
            this.panel1.TabIndex = 2;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1f8, 330);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmServerObjectPropertySheet";
            this.Text = "ServerObject属性";
            base.Load += new EventHandler(this.frmServerObjectPropertySheet_Load);
            this.panel2.ResumeLayout(false);
            this.xtraTabControl1.EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
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

