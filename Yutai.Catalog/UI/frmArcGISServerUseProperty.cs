namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.GISClient;
    using Yutai.Catalog;
    
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class frmArcGISServerUseProperty : Form
    {
        private bool bool_0;
        private Button btnCancel;
        private Button btnOK;
        private CheckBox chkSaveUserNameAndPsw;
        private GroupBox groupBox2;
        private IAGSServerConnection iagsserverConnection_0;
      
        private IContainer icontainer_0 = null;
        private IGxObject igxObject_0 = null;
        private IMapDescription imapDescription_0;
        private IMapServer imapServer_0;
      
        private Label label2;
        private Label label3;
        private Label label5;
        private Label lblServerInfo;
        private string string_0;
        private string string_1;
     
        private Button txtPassword;
        private Button txtServer;
        private Button txtUser;

        public frmArcGISServerUseProperty()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string path = Environment.SystemDirectory.Substring(0, 2) + @"\Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = this.txtServer.Text;
            if (text.IndexOf("http://") == 0)
            {
                text = text.Substring(7);
            }
            else
            {
                this.txtServer.Text = "http://" + this.txtServer.Text;
            }
            string[] strArray = text.Split(new char[] { '/' });
            IAGSServerConnectionFactory factory = new AGSServerConnectionFactory();
            IPropertySet pConnectionProperties = this.method_2(this.ConnectionFile, strArray[0]);
            IAGSServerConnection connection = null;
            try
            {
                connection = factory.Open(pConnectionProperties, 0);
                if (connection == null)
                {
                    return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                return;
            }
            if (Directory.Exists(path))
            {
                IGxAGSConnection connection2 = new GxAGSConnection {
                    AGSServerConnectionName = connection.FullName as IAGSServerConnectionName
                };
                connection2.SaveToFile(this.ConnectionFile);
                this.igxObject_0 = connection2 as IGxObject;
            }
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmArcGISServerUseProperty_Load(object sender, EventArgs e)
        {
            IPropertySet connectionProperties = null;
            if (this.AGSServerConnectionName != null)
            {
                connectionProperties = this.AGSServerConnectionName.ConnectionProperties;
            }
            else
            {
                connectionProperties = this.ConnectionProperties;
            }
            this.txtServer.Text = Convert.ToString(connectionProperties.GetProperty("URL"));
            this.chkSaveUserNameAndPsw.Checked = !Convert.ToBoolean(connectionProperties.GetProperty("HIDEUSERPROPERTY"));
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                this.txtUser.Text = Convert.ToString(connectionProperties.GetProperty("USER"));
                byte[] property = (byte[]) connectionProperties.GetProperty("PASSWORD");
                string str = Encoding.ASCII.GetString(property);
                this.txtPassword.Text = str;
            }
            this.groupBox2.Enabled = true;
        }

        private void InitializeComponent()
        {
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.groupBox2 = new GroupBox();
            this.chkSaveUserNameAndPsw = new CheckBox();
            this.txtPassword = new Button();
            this.txtUser = new Button();
            this.label3 = new Label();
            this.label2 = new Label();
            this.txtServer = new Button();
            this.lblServerInfo = new Label();
            this.label5 = new Label();
            this.groupBox2.SuspendLayout();
           
            base.SuspendLayout();
            this.btnOK.Location = new Point(0xe4, 0xe2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 0x1c);
            this.btnOK.TabIndex = 0x18;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(0x139, 0xe2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x1c);
            this.btnCancel.TabIndex = 0x17;
            this.btnCancel.Text = "取消";
            this.groupBox2.Controls.Add(this.chkSaveUserNameAndPsw);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new Point(14, 0x5d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x184, 0x74);
            this.groupBox2.TabIndex = 0x16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帐户";
            this.chkSaveUserNameAndPsw.Checked=true;
            this.chkSaveUserNameAndPsw.Location = new Point(0x10, 0x58);
            this.chkSaveUserNameAndPsw.Name = "chkSaveUserNameAndPsw";
            this.chkSaveUserNameAndPsw.Text = "保存用户名/密码";
            this.chkSaveUserNameAndPsw.Size = new Size(0x70, 0x13);
            this.chkSaveUserNameAndPsw.TabIndex = 4;
            this.txtPassword.Text="";
            this.txtPassword.Location = new Point(0x44, 0x34);
            this.txtPassword.Name = "txtPassword";
            //this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(0x130, 0x15);
            this.txtPassword.TabIndex = 3;
            this.txtUser.Text="";
            this.txtUser.Location = new Point(0x44, 20);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(0x130, 0x15);
            this.txtUser.TabIndex = 2;
            this.txtUser.TextChanged += new EventHandler(this.txtUser_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x10, 0x38);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2f, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x2f, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名:";
            this.txtServer.Text = "http://";
            this.txtServer.Location = new Point(0x53, 0x10);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(0x13f, 0x15);
            this.txtServer.TabIndex = 0x10;
            this.lblServerInfo.AutoSize = true;
            this.lblServerInfo.Location = new Point(12, 0x13);
            this.lblServerInfo.Name = "lblServerInfo";
            this.lblServerInfo.Size = new Size(0x41, 12);
            this.lblServerInfo.TabIndex = 12;
            this.lblServerInfo.Text = "服务器URL:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x51, 0x31);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x131, 12);
            this.label5.TabIndex = 0x17;
            this.label5.Text = "ArcGIS Server:http://myserver:6080/arcgis/services";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b1, 270);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtServer);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.lblServerInfo);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmArcGISServerUseProperty";
            this.Text = "ArcGIS Server用户连接属性";
            base.Load += new EventHandler(this.frmArcGISServerUseProperty_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
          
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private IMapServer method_0(string string_3)
        {
            IAGSEnumServerObjectName serverObjectNames = this.iagsserverConnection_0.ServerObjectNames;
            IAGSServerObjectName name2 = null;
            while ((name2 = serverObjectNames.Next()) != null)
            {
                if (name2.Name == string_3)
                {
                    break;
                }
            }
            IName name3 = name2 as IName;
            return (name3.Open() as IMapServer);
        }

        private string method_1(string string_3)
        {
            string str = string_3.Substring(0, string_3.Length - 4);
            for (int i = 1; File.Exists(string_3); i++)
            {
                string_3 = str + " (" + i.ToString() + ").ags";
            }
            return string_3;
        }

        private IPropertySet method_2(string string_3, string string_4)
        {
            IPropertySet set = new PropertySet();
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("USER", this.txtUser.Text.Trim());
            }
            set.SetProperty("Modulus", "dba16ec2c39b37a983b29026dca2859b28cc07bed0a9662bdea17d9fe486fed4d0e2e8a27ca1de05f186d2377da7ced5661e159d10abf5999258d11cb06b2fb3");
            if (!this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                if (this.txtUser.Text.Trim().Length == 0)
                {
                    set.SetProperty("HIDEUSERPROPERTY", true);
                }
                else
                {
                    set.SetProperty("HIDEUSERPROPERTY", false);
                }
            }
            else
            {
                set.SetProperty("HIDEUSERPROPERTY", true);
            }
            set.SetProperty("URL", this.txtServer.Text);
            set.SetProperty("STAGINGFOLDER", "");
            set.SetProperty("USEDEFAULTSTAGINGFOLDER", true);
            set.SetProperty("ANONYMOUS", false);
            set.SetProperty("CONNECTIONMODE", 0);
            set.SetProperty("SERVERTYPE", 1);
            set.SetProperty("HTTPTIMEOUT", 60);
            set.SetProperty("MESSAGEFORMAT", 2);
            set.SetProperty("SoapUrl", "http://" + string_4 + "/arcgis/services");
            set.SetProperty("RestUrl", "http://" + string_4 + "/arcgis/rest");
            set.SetProperty("AdminURL", "http://" + string_4 + "/arcgis/admin");
            set.SetProperty("AdminTokenUrl", "http://" + string_4 + "/arcgis/admin/generateToken");
            set.SetProperty("SupportsRSA", true);
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("PASSWORD", this.txtPassword.Text);
            }
            set.SetProperty("connectionfile", string_3);
            return set;
        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtUser.Text.Trim().Length == 0)
            {
                this.chkSaveUserNameAndPsw.Enabled = false;
            }
            else
            {
                this.chkSaveUserNameAndPsw.Enabled = true;
            }
        }

        public IAGSServerConnectionName AGSServerConnectionName
        {
            get; set;
        }

        public string ConnectionFile
        {
            get; set;
        }

        public IPropertySet ConnectionProperties
        {
            get; set;
        }

        public IGxObject NewObject
        {
            get
            {
                return this.igxObject_0;
            }
        }
    }
}

