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

    public class frmArcGISServerManageProperty : Form
    {
        private bool bool_0;
        private Button btnCancel;
        private Button btnOK;
        private ComboBox cboServerType;
        private CheckBox chkSaveManage;
        private CheckBox chkUseDefaultFolder;
        private GroupBox groupBox1;
        private IAGSServerConnection iagsserverConnection_0;
        
        private IContainer icontainer_0 = null;
        private IGxObject igxObject_0 = null;
        private IMapDescription imapDescription_0;
        private IMapServer imapServer_0;
       
        private Label label1;
        private Label label4;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Button Button1;
        private string string_0;
        private string string_1;
      
        private Button txtMUrl;
        private Button txtPaswordMan;
        private Button txtTempFolder;
        private Button txtUserManage;

        public frmArcGISServerManageProperty()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtTempFolder.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入过渡文件夹");
            }
            else
            {
                string path = Environment.SystemDirectory.Substring(0, 2) + @"\Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string text = this.txtMUrl.Text;
                if (text.IndexOf("http://") == 0)
                {
                    text = text.Substring(7);
                }
                else
                {
                    this.txtMUrl.Text = "http://" + this.txtMUrl.Text;
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
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmArcGISServerManageProperty_Load(object sender, EventArgs e)
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
            this.txtMUrl.Text = Convert.ToString(connectionProperties.GetProperty("URL"));
            this.chkSaveManage.Checked = !Convert.ToBoolean(connectionProperties.GetProperty("HIDEUSERPROPERTY"));
            if (this.chkSaveManage.Checked)
            {
                this.txtUserManage.Text = Convert.ToString(connectionProperties.GetProperty("USER"));
                byte[] property = (byte[]) connectionProperties.GetProperty("PASSWORD");
                string str = Encoding.ASCII.GetString(property);
                this.txtPaswordMan.Text = str;
            }
            this.chkUseDefaultFolder.Checked = Convert.ToBoolean(connectionProperties.GetProperty("USEDEFAULTSTAGINGFOLDER"));
            this.txtTempFolder.Text = Convert.ToString(connectionProperties.GetProperty("STAGINGFOLDER"));
            this.groupBox1.Enabled = true;
        }

        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSaveManage = new System.Windows.Forms.CheckBox();
            this.txtPaswordMan = new System.Windows.Forms.Button();
            this.txtUserManage = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Button1 = new System.Windows.Forms.Button();
            this.cboServerType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMUrl = new System.Windows.Forms.Button();
            this.txtTempFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chkUseDefaultFolder = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(224, 286);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 28);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(309, 286);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 28);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "取消";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSaveManage);
            this.groupBox1.Controls.Add(this.txtPaswordMan);
            this.groupBox1.Controls.Add(this.txtUserManage);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(14, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(388, 116);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帐户";
            // 
            // chkSaveManage
            // 
            this.chkSaveManage.Checked = true;
            this.chkSaveManage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveManage.Enabled = false;
            this.chkSaveManage.Location = new System.Drawing.Point(16, 88);
            this.chkSaveManage.Name = "chkSaveManage";
            this.chkSaveManage.Size = new System.Drawing.Size(112, 19);
            this.chkSaveManage.TabIndex = 4;
            this.chkSaveManage.Text = "保存用户名/密码";
            // 
            // txtPaswordMan
            // 
            this.txtPaswordMan.Location = new System.Drawing.Point(68, 52);
            this.txtPaswordMan.Name = "txtPaswordMan";
            this.txtPaswordMan.Size = new System.Drawing.Size(304, 21);
            this.txtPaswordMan.TabIndex = 3;
            this.txtPaswordMan.UseCompatibleTextRendering = true;
            // 
            // txtUserManage
            // 
            this.txtUserManage.Location = new System.Drawing.Point(68, 20);
            this.txtUserManage.Name = "txtUserManage";
            this.txtUserManage.Size = new System.Drawing.Size(304, 21);
            this.txtUserManage.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "密  码:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "用户名:";
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(361, 93);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(31, 28);
            this.Button1.TabIndex = 27;
            this.Button1.Text = "...";
            // 
            // cboServerType
            // 
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.Items.AddRange(new object[] {
            "ArcGIS Server"});
            this.cboServerType.Location = new System.Drawing.Point(89, 65);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new System.Drawing.Size(303, 20);
            this.cboServerType.TabIndex = 26;
            this.cboServerType.Text = "ArcGIS Server";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "服务器类型:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(305, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "ArcGIS Server:http://myserver:6080/arcgis/services";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "服务器URL:";
            // 
            // txtMUrl
            // 
            this.txtMUrl.Location = new System.Drawing.Point(89, 4);
            this.txtMUrl.Name = "txtMUrl";
            this.txtMUrl.Size = new System.Drawing.Size(303, 21);
            this.txtMUrl.TabIndex = 19;
            this.txtMUrl.Text = "http://";
            // 
            // txtTempFolder
            // 
            this.txtTempFolder.Location = new System.Drawing.Point(89, 99);
            this.txtTempFolder.Name = "txtTempFolder";
            this.txtTempFolder.Size = new System.Drawing.Size(266, 21);
            this.txtTempFolder.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "过渡文件夹:";
            // 
            // chkUseDefaultFolder
            // 
            this.chkUseDefaultFolder.Checked = true;
            this.chkUseDefaultFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseDefaultFolder.Location = new System.Drawing.Point(12, 128);
            this.chkUseDefaultFolder.Name = "chkUseDefaultFolder";
            this.chkUseDefaultFolder.Size = new System.Drawing.Size(138, 19);
            this.chkUseDefaultFolder.TabIndex = 30;
            this.chkUseDefaultFolder.Text = "使用默认过渡文件夹";
            // 
            // frmArcGISServerManageProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 335);
            this.Controls.Add(this.chkUseDefaultFolder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cboServerType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTempFolder);
            this.Controls.Add(this.txtMUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmArcGISServerManageProperty";
            this.Text = "ArcGIS Server管理员连接属性";
            this.Load += new System.EventHandler(this.frmArcGISServerManageProperty_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
            if (this.chkSaveManage.Checked)
            {
                set.SetProperty("USER", this.txtUserManage.Text.Trim());
            }
            set.SetProperty("Modulus", "dba16ec2c39b37a983b29026dca2859b28cc07bed0a9662bdea17d9fe486fed4d0e2e8a27ca1de05f186d2377da7ced5661e159d10abf5999258d11cb06b2fb3");
            if (!this.chkSaveManage.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveManage.Checked)
            {
                if (this.txtUserManage.Text.Trim().Length == 0)
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
            set.SetProperty("URL", this.txtMUrl.Text + "/admin");
            set.SetProperty("STAGINGFOLDER", this.txtTempFolder.Text);
            set.SetProperty("USEDEFAULTSTAGINGFOLDER", this.chkUseDefaultFolder.Checked);
            set.SetProperty("ANONYMOUS", false);
            set.SetProperty("CONNECTIONMODE", 1);
            set.SetProperty("SERVERTYPE", 1);
            set.SetProperty("HTTPTIMEOUT", 60);
            set.SetProperty("MESSAGEFORMAT", 2);
            set.SetProperty("SoapUrl", "http://" + string_4 + "/arcgis/services");
            set.SetProperty("RestUrl", "http://" + string_4 + "/arcgis/rest");
            set.SetProperty("AdminURL", "http://" + string_4 + "/arcgis/admin");
            set.SetProperty("AdminTokenUrl", "http://" + string_4 + "/arcgis/admin/generateToken");
            set.SetProperty("SupportsRSA", true);
            if (this.chkSaveManage.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveManage.Checked)
            {
                set.SetProperty("PASSWORD", this.txtPaswordMan.Text);
            }
            set.SetProperty("connectionfile", string_3);
            return set;
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

