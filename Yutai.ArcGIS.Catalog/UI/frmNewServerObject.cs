using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmNewServerObject : Form
    {
        private Container container_0 = null;
        private IAGSServerConnection iagsserverConnection_0 = null;
        private IAGSServerConnectionName iagsserverConnectionName_0 = null;
        private int int_0 = 0;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private SODocumentPropertyPage sodocumentPropertyPage_0 = new SODocumentPropertyPage();
        private SOGeneralPropertyPage sogeneralPropertyPage_0 = new SOGeneralPropertyPage();
        private SOPoolPropertyPage sopoolPropertyPage_0 = new SOPoolPropertyPage();
        private SOProcessManagementPropertyPage soprocessManagementPropertyPage_0 = new SOProcessManagementPropertyPage();
        private SOSummaryPropertyPage sosummaryPropertyPage_0 = new SOSummaryPropertyPage();

        public frmNewServerObject()
        {
            this.InitializeComponent();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.sogeneralPropertyPage_0.Visible = true;
                    this.sodocumentPropertyPage_0.Visible = false;
                    this.btnLast.Enabled = false;
                    break;

                case 2:
                    this.sodocumentPropertyPage_0.Visible = true;
                    this.sopoolPropertyPage_0.Visible = false;
                    break;

                case 3:
                    this.sopoolPropertyPage_0.Visible = true;
                    this.soprocessManagementPropertyPage_0.Visible = false;
                    break;

                case 4:
                    this.soprocessManagementPropertyPage_0.Visible = true;
                    this.sosummaryPropertyPage_0.Visible = false;
                    this.btnNext.Text = "下一步";
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (!(this.sogeneralPropertyPage_0.SOName.Trim() == ""))
                    {
                        this.sogeneralPropertyPage_0.Visible = false;
                        if (this.iserverObjectConfiguration_0 == null)
                        {
                            this.iserverObjectConfiguration_0 = this.sogeneralPropertyPage_0.CreateServerObjectConfig();
                            this.sodocumentPropertyPage_0.ServerObjectConfiguration = this.iserverObjectConfiguration_0;
                            this.sopoolPropertyPage_0.ServerObjectConfiguration = this.iserverObjectConfiguration_0;
                            this.soprocessManagementPropertyPage_0.ServerObjectConfiguration = this.iserverObjectConfiguration_0;
                            this.sosummaryPropertyPage_0.ServerObjectConfiguration = this.iserverObjectConfiguration_0;
                        }
                        this.sogeneralPropertyPage_0.Apply();
                        this.sodocumentPropertyPage_0.Visible = true;
                        this.btnLast.Enabled = true;
                        break;
                    }
                    MessageBox.Show("请输入Server object名字!");
                    return;

                case 1:
                    if (!(this.sodocumentPropertyPage_0.Docunment.Trim() == ""))
                    {
                        this.sodocumentPropertyPage_0.Apply();
                        this.sodocumentPropertyPage_0.Visible = false;
                        this.sopoolPropertyPage_0.Visible = true;
                        break;
                    }
                    MessageBox.Show("请设定地图文档!");
                    return;

                case 2:
                    this.sopoolPropertyPage_0.Apply();
                    this.sopoolPropertyPage_0.Visible = false;
                    this.soprocessManagementPropertyPage_0.Visible = true;
                    break;

                case 3:
                    this.soprocessManagementPropertyPage_0.Apply();
                    this.soprocessManagementPropertyPage_0.Visible = false;
                    this.sosummaryPropertyPage_0.Visible = true;
                    this.btnNext.Text = "完成";
                    break;

                case 4:
                    try
                    {
                        (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.AddConfiguration(this.iserverObjectConfiguration_0);
                        if (this.sosummaryPropertyPage_0.isStart)
                        {
                            (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.StartConfiguration(this.iserverObjectConfiguration_0.Name, this.iserverObjectConfiguration_0.TypeName);
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                        Logger.Current.Error("",exception, "");
                    }
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    return;
            }
            this.int_0++;
        }

 private void frmNewServerObject_Load(object sender, EventArgs e)
        {
            this.sogeneralPropertyPage_0.AGSServerConnection = this.iagsserverConnection_0;
            this.sogeneralPropertyPage_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.sogeneralPropertyPage_0);
            this.sodocumentPropertyPage_0.Visible = false;
            this.sodocumentPropertyPage_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.sodocumentPropertyPage_0);
            this.sopoolPropertyPage_0.Visible = false;
            this.sopoolPropertyPage_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.sopoolPropertyPage_0);
            this.soprocessManagementPropertyPage_0.Visible = false;
            this.soprocessManagementPropertyPage_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.soprocessManagementPropertyPage_0);
            this.sosummaryPropertyPage_0.Visible = false;
            this.sosummaryPropertyPage_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.sosummaryPropertyPage_0);
        }

 public IAGSServerConnectionName AGSServerConnectionName
        {
            set
            {
                this.iagsserverConnectionName_0 = value;
                this.iagsserverConnection_0 = (this.iagsserverConnectionName_0 as IName).Open() as IAGSServerConnection;
            }
        }
    }
}

