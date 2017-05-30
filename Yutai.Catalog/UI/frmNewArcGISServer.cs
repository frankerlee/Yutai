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
    using System.Windows.Forms;

    public class frmNewArcGISServer : Form
    {
        private bool bool_0;
        private Button btnCancel;
        private Button btnOK;
        private Button btnSelect;
        private ComboBox cboServerType;
        private CheckBox chkSaveManage;
        private CheckBox chkSaveUserNameAndPsw;
        private CheckBox chkUseDefaultFolder;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private IAGSServerConnection iagsserverConnection_0;
        private IContainer icontainer_0 = null;
        private IGxObject igxObject_0 = null;
        private IMapDescription imapDescription_0;
        private IMapServer imapServer_0;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblServerInfo;
        private Panel panel1;
        private Panel panel2;
        private ComboBox radioGroup1;
        private string string_0;
        private string string_1;
        private Button txtMUrl;
        private Button txtPassword;
        private Button txtPaswordMan;
        private Button txtServer;
        private Button txtTempFolder;
        private Button txtUser;
        private Label label10;
        private Button txtUserManage;

        public frmNewArcGISServer()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (((this.radioGroup1.SelectedIndex >= 1) && !this.chkUseDefaultFolder.Checked) && (this.txtTempFolder.Text.Trim().Length == 0))
            {
                MessageBox.Show("请输入过渡文件夹");
            }
            else
            {
                IPropertySet set;
                object obj2;
                object obj3;
                string path = Environment.SystemDirectory.Substring(0, 2) + @"\Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string str2 = "";
                string text = "";
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    text = this.txtServer.Text;
                }
                else
                {
                    text = this.txtMUrl.Text;
                }
                string str4 = "";
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    str4 = "(用户)";
                }
                else if (this.radioGroup1.SelectedIndex == 1)
                {
                    str4 = "(系统管理员)";
                }
                else if (this.radioGroup1.SelectedIndex == 2)
                {
                    str4 = "(发布者)";
                }
                if (text.IndexOf("http://") == 0)
                {
                    text = text.Substring(7);
                }
                else
                {
                    this.txtServer.Text = "http://" + this.txtServer.Text;
                }
                string[] strArray = text.Split(new char[] { '/' });
                string str5 = strArray[0];
                if (strArray.Length > 0)
                {
                    str2 = path + "arcgis on" + strArray[0].Replace(":", "_") + str4 + ".ags";
                }
                else
                {
                    str2 = path + "arcgis on" + str4 + ".ags";
                }
                str2 = this.method_1(str2);
                IAGSServerConnectionFactory factory = new AGSServerConnectionFactory();
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    set = this.method_2(str2, str5);
                }
                else
                {
                    set = this.method_3(str2, str5);
                }
                set.GetAllProperties(out obj2, out obj3);
                IAGSServerConnection connection = null;
                try
                {
                    connection = factory.Open(set, 0);
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
                    connection2.SaveToFile(str2);
                    this.igxObject_0 = connection2 as IGxObject;
                }
                base.DialogResult = DialogResult.OK;
            }
        }

        private void chkUseDefaultFolder_CheckedChanged(object sender, EventArgs e)
        {
            this.txtTempFolder.Enabled = this.chkUseDefaultFolder.Checked;
            this.btnSelect.Enabled = this.chkUseDefaultFolder.Checked;
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmNewArcGISServer_Load(object sender, EventArgs e)
        {
            this.txtTempFolder.Text = Path.GetTempPath();
        }

        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSaveUserNameAndPsw = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.Button();
            this.lblServerInfo = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkUseDefaultFolder = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSaveManage = new System.Windows.Forms.CheckBox();
            this.txtPaswordMan = new System.Windows.Forms.Button();
            this.txtUserManage = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.cboServerType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMUrl = new System.Windows.Forms.Button();
            this.txtTempFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioGroup1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(276, 345);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 28);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(361, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 28);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "取消";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkSaveUserNameAndPsw);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(7, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(388, 116);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帐户";
            // 
            // chkSaveUserNameAndPsw
            // 
            this.chkSaveUserNameAndPsw.Checked = true;
            this.chkSaveUserNameAndPsw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveUserNameAndPsw.Enabled = false;
            this.chkSaveUserNameAndPsw.Location = new System.Drawing.Point(16, 88);
            this.chkSaveUserNameAndPsw.Name = "chkSaveUserNameAndPsw";
            this.chkSaveUserNameAndPsw.Size = new System.Drawing.Size(112, 19);
            this.chkSaveUserNameAndPsw.TabIndex = 4;
            this.chkSaveUserNameAndPsw.Text = "保存用户名/密码";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(68, 52);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(304, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(68, 20);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(304, 21);
            this.txtUser.TabIndex = 2;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_EditValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "密  码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "用户名:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(76, 17);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(319, 21);
            this.txtServer.TabIndex = 16;
            this.txtServer.Text = "http://";
            // 
            // lblServerInfo
            // 
            this.lblServerInfo.AutoSize = true;
            this.lblServerInfo.Location = new System.Drawing.Point(5, 20);
            this.lblServerInfo.Name = "lblServerInfo";
            this.lblServerInfo.Size = new System.Drawing.Size(65, 12);
            this.lblServerInfo.TabIndex = 12;
            this.lblServerInfo.Text = "服务器URL:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtServer);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.lblServerInfo);
            this.panel1.Location = new System.Drawing.Point(12, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 230);
            this.panel1.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(74, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(305, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "ArcGIS Server:http://myserver:6080/arcgis/services";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkUseDefaultFolder);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnSelect);
            this.panel2.Controls.Add(this.cboServerType);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtMUrl);
            this.panel2.Controls.Add(this.txtTempFolder);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(12, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(448, 290);
            this.panel2.TabIndex = 26;
            this.panel2.Visible = false;
            // 
            // chkUseDefaultFolder
            // 
            this.chkUseDefaultFolder.Checked = true;
            this.chkUseDefaultFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseDefaultFolder.Location = new System.Drawing.Point(23, 140);
            this.chkUseDefaultFolder.Name = "chkUseDefaultFolder";
            this.chkUseDefaultFolder.Size = new System.Drawing.Size(138, 19);
            this.chkUseDefaultFolder.TabIndex = 29;
            this.chkUseDefaultFolder.Text = "使用默认过渡文件夹";
            this.chkUseDefaultFolder.CheckedChanged += new System.EventHandler(this.chkUseDefaultFolder_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSaveManage);
            this.groupBox1.Controls.Add(this.txtPaswordMan);
            this.groupBox1.Controls.Add(this.txtUserManage);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(25, 165);
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
            // 
            // txtUserManage
            // 
            this.txtUserManage.Location = new System.Drawing.Point(68, 20);
            this.txtUserManage.Name = "txtUserManage";
            this.txtUserManage.Size = new System.Drawing.Size(304, 21);
            this.txtUserManage.TabIndex = 2;
            this.txtUserManage.TextChanged += new System.EventHandler(this.txtUserManage_EditValueChanged);
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
            // btnSelect
            // 
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new System.Drawing.Point(368, 99);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(31, 28);
            this.btnSelect.TabIndex = 27;
            this.btnSelect.Text = "...";
            // 
            // cboServerType
            // 
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.Items.AddRange(new object[] {
            "ArcGIS Server"});
            this.cboServerType.Location = new System.Drawing.Point(96, 71);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new System.Drawing.Size(303, 20);
            this.cboServerType.TabIndex = 26;
            this.cboServerType.Text = "ArcGIS Server";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "服务器类型:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(94, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(251, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "ArcGIS Server:http://myserver:6080/arcgis";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "服务器URL:";
            // 
            // txtMUrl
            // 
            this.txtMUrl.Location = new System.Drawing.Point(96, 10);
            this.txtMUrl.Name = "txtMUrl";
            this.txtMUrl.Size = new System.Drawing.Size(303, 21);
            this.txtMUrl.TabIndex = 19;
            this.txtMUrl.Text = "http://";
            // 
            // txtTempFolder
            // 
            this.txtTempFolder.Enabled = false;
            this.txtTempFolder.Location = new System.Drawing.Point(96, 105);
            this.txtTempFolder.Name = "txtTempFolder";
            this.txtTempFolder.Size = new System.Drawing.Size(266, 21);
            this.txtTempFolder.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "过渡文件夹:";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Items.AddRange(new object[] {
            "使用GIS服务",
            "管理GIS服务",
            "发布GIS服务"});
            this.radioGroup1.Location = new System.Drawing.Point(108, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Size = new System.Drawing.Size(352, 20);
            this.radioGroup1.TabIndex = 27;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 28;
            this.label10.Text = "服务器操作:";
            // 
            // frmNewArcGISServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 385);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewArcGISServer";
            this.Text = "添加ArcGIS Server";
            this.Load += new System.EventHandler(this.frmNewArcGISServer_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private IMapServer method_0(string string_2)
        {
            IAGSEnumServerObjectName serverObjectNames = this.iagsserverConnection_0.ServerObjectNames;
            IAGSServerObjectName name2 = null;
            while ((name2 = serverObjectNames.Next()) != null)
            {
                if (name2.Name == string_2)
                {
                    break;
                }
            }
            IName name3 = name2 as IName;
            return (name3.Open() as IMapServer);
        }

        private string method_1(string string_2)
        {
            string str = string_2.Substring(0, string_2.Length - 4);
            for (int i = 1; File.Exists(string_2); i++)
            {
                string_2 = str + " (" + i.ToString() + ").ags";
            }
            return string_2;
        }

        private IPropertySet method_2(string string_2, string string_3)
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
            set.SetProperty("CONNECTIONMODE", this.radioGroup1.SelectedIndex);
            set.SetProperty("SERVERTYPE", 1);
            set.SetProperty("HTTPTIMEOUT", 60);
            set.SetProperty("MESSAGEFORMAT", 2);
            set.SetProperty("SoapUrl", "http://" + string_3 + "/arcgis/services");
            set.SetProperty("RestUrl", "http://" + string_3 + "/arcgis/rest");
            set.SetProperty("AdminURL", "http://" + string_3 + "/arcgis/admin");
            set.SetProperty("AdminTokenUrl", "http://" + string_3 + "/arcgis/admin/generateToken");
            set.SetProperty("SupportsRSA", true);
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveUserNameAndPsw.Checked)
            {
                set.SetProperty("PASSWORD", this.txtPassword.Text);
            }
            set.SetProperty("connectionfile", string_2);
            return set;
        }

        private IPropertySet method_3(string string_2, string string_3)
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
            set.SetProperty("CONNECTIONMODE", this.radioGroup1.SelectedIndex);
            set.SetProperty("SERVERTYPE", 1);
            set.SetProperty("HTTPTIMEOUT", 60);
            set.SetProperty("MESSAGEFORMAT", 2);
            set.SetProperty("SoapUrl", "http://" + string_3 + "/arcgis/services");
            set.SetProperty("RestUrl", "http://" + string_3 + "/arcgis/rest");
            set.SetProperty("AdminURL", "http://" + string_3 + "/arcgis/admin");
            set.SetProperty("AdminTokenUrl", "http://" + string_3 + "/arcgis/admin/generateToken");
            set.SetProperty("SupportsRSA", true);
            if (this.chkSaveManage.Checked)
            {
                set.SetProperty("Exponent", "1001");
            }
            if (this.chkSaveManage.Checked)
            {
                set.SetProperty("PASSWORD", this.txtPaswordMan.Text);
            }
            set.SetProperty("connectionfile", string_2);
            return set;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panel1.Visible = this.radioGroup1.SelectedIndex == 0;
            this.panel2.Visible = this.radioGroup1.SelectedIndex >= 1;
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

        private void txtUserManage_EditValueChanged(object sender, EventArgs e)
        {
            this.chkSaveManage.Enabled = true;
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

