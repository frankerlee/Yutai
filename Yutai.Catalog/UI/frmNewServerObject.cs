namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.GISClient;
    using ESRI.ArcGIS.Server;
  
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmNewServerObject : Form
    {
        private Button btnLast;
        private Button btnNext;
        private Container container_0 = null;
        private IAGSServerConnection iagsserverConnection_0 = null;
        private IAGSServerConnectionName iagsserverConnectionName_0 = null;
        private int int_0 = 0;
        private IServerObjectConfiguration iserverObjectConfiguration_0 = null;
        private Panel panel1;
        private Panel panel2;
        private Button Button3;
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
                        //CErrorLog.writeErrorLog(this, exception, "");
                    }
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    return;
            }
            this.int_0++;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmNewServerObject));
            this.panel1 = new Panel();
            this.Button3 = new Button();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.Button3);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 290);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1f8, 40);
            this.panel1.TabIndex = 0;
            this.Button3.DialogResult = DialogResult.Cancel;
            this.Button3.Location = new Point(0x170, 8);
            this.Button3.Name = "Button3";
            this.Button3.Size = new Size(80, 0x18);
            this.Button3.TabIndex = 2;
            this.Button3.Text = "取消";
            this.btnNext.Location = new Point(0x110, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(80, 0x18);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(0xb0, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(80, 0x18);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1f8, 290);
            this.panel2.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1f8, 330);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
          
            base.Name = "frmNewServerObject";
            this.Text = "New ServerObject";
            base.Load += new EventHandler(this.frmNewServerObject_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
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

