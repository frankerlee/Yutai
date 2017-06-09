using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog.UI
{
    public class frmNewArcGISServer : Form
    {
        private bool bool_0;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private SimpleButton btnSelect;
        private System.Windows.Forms.ComboBox cboServerType;
        private CheckEdit chkSaveManage;
        private CheckEdit chkSaveUserNameAndPsw;
        private CheckEdit chkUseDefaultFolder;
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
        private RadioGroup radioGroup1;
        private string string_0;
        private string string_1;
        private TextEdit txtMUrl;
        private TextEdit txtPassword;
        private TextEdit txtPaswordMan;
        private TextEdit txtServer;
        private TextEdit txtTempFolder;
        private TextEdit txtUser;
        private TextEdit txtUserManage;

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
                IAGSServerConnectionFactory factory = new AGSServerConnectionFactoryClass();
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
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.groupBox2 = new GroupBox();
            this.chkSaveUserNameAndPsw = new CheckEdit();
            this.txtPassword = new TextEdit();
            this.txtUser = new TextEdit();
            this.label3 = new Label();
            this.label2 = new Label();
            this.txtServer = new TextEdit();
            this.lblServerInfo = new Label();
            this.panel1 = new Panel();
            this.label5 = new Label();
            this.panel2 = new Panel();
            this.chkUseDefaultFolder = new CheckEdit();
            this.groupBox1 = new GroupBox();
            this.chkSaveManage = new CheckEdit();
            this.txtPaswordMan = new TextEdit();
            this.txtUserManage = new TextEdit();
            this.label8 = new Label();
            this.label9 = new Label();
            this.btnSelect = new SimpleButton();
            this.cboServerType = new System.Windows.Forms.ComboBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label4 = new Label();
            this.txtMUrl = new TextEdit();
            this.txtTempFolder = new TextEdit();
            this.label1 = new Label();
            this.radioGroup1 = new RadioGroup();
            this.groupBox2.SuspendLayout();
            this.chkSaveUserNameAndPsw.Properties.BeginInit();
            this.txtPassword.Properties.BeginInit();
            this.txtUser.Properties.BeginInit();
            this.txtServer.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.chkUseDefaultFolder.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.chkSaveManage.Properties.BeginInit();
            this.txtPaswordMan.Properties.BeginInit();
            this.txtUserManage.Properties.BeginInit();
            this.txtMUrl.Properties.BeginInit();
            this.txtTempFolder.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.Location = new Point(0x114, 0x159);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 0x1c);
            this.btnOK.TabIndex = 0x18;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x169, 0x159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x1c);
            this.btnCancel.TabIndex = 0x17;
            this.btnCancel.Text = "取消";
            this.groupBox2.Controls.Add(this.chkSaveUserNameAndPsw);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.txtUser);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(7, 0x5e);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x184, 0x74);
            this.groupBox2.TabIndex = 0x16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "帐户";
            this.chkSaveUserNameAndPsw.EditValue = true;
            this.chkSaveUserNameAndPsw.Enabled = false;
            this.chkSaveUserNameAndPsw.Location = new Point(0x10, 0x58);
            this.chkSaveUserNameAndPsw.Name = "chkSaveUserNameAndPsw";
            this.chkSaveUserNameAndPsw.Properties.Caption = "保存用户名/密码";
            this.chkSaveUserNameAndPsw.Size = new Size(0x70, 0x13);
            this.chkSaveUserNameAndPsw.TabIndex = 4;
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new Point(0x44, 0x34);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(0x130, 0x15);
            this.txtPassword.TabIndex = 3;
            this.txtUser.EditValue = "";
            this.txtUser.Location = new Point(0x44, 20);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(0x130, 0x15);
            this.txtUser.TabIndex = 2;
            this.txtUser.EditValueChanged += new EventHandler(this.txtUser_EditValueChanged);
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
            this.txtServer.EditValue = "http://";
            this.txtServer.Location = new Point(0x4c, 0x11);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(0x13f, 0x15);
            this.txtServer.TabIndex = 0x10;
            this.lblServerInfo.AutoSize = true;
            this.lblServerInfo.Location = new Point(5, 20);
            this.lblServerInfo.Name = "lblServerInfo";
            this.lblServerInfo.Size = new Size(0x41, 12);
            this.lblServerInfo.TabIndex = 12;
            this.lblServerInfo.Text = "服务器URL:";
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtServer);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.lblServerInfo);
            this.panel1.Location = new Point(12, 0x31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(410, 230);
            this.panel1.TabIndex = 0x19;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x4a, 50);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x131, 12);
            this.label5.TabIndex = 0x17;
            this.label5.Text = "ArcGIS Server:http://myserver:6080/arcgis/services";
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
            this.panel2.Location = new Point(12, 0x31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x1c0, 290);
            this.panel2.TabIndex = 0x1a;
            this.panel2.Visible = false;
            this.chkUseDefaultFolder.EditValue = true;
            this.chkUseDefaultFolder.Location = new Point(0x17, 140);
            this.chkUseDefaultFolder.Name = "chkUseDefaultFolder";
            this.chkUseDefaultFolder.Properties.Caption = "使用默认过渡文件夹";
            this.chkUseDefaultFolder.Size = new Size(0x8a, 0x13);
            this.chkUseDefaultFolder.TabIndex = 0x1d;
            this.chkUseDefaultFolder.CheckedChanged += new EventHandler(this.chkUseDefaultFolder_CheckedChanged);
            this.groupBox1.Controls.Add(this.chkSaveManage);
            this.groupBox1.Controls.Add(this.txtPaswordMan);
            this.groupBox1.Controls.Add(this.txtUserManage);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new Point(0x19, 0xa5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x184, 0x74);
            this.groupBox1.TabIndex = 0x1c;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "帐户";
            this.chkSaveManage.EditValue = true;
            this.chkSaveManage.Enabled = false;
            this.chkSaveManage.Location = new Point(0x10, 0x58);
            this.chkSaveManage.Name = "chkSaveManage";
            this.chkSaveManage.Properties.Caption = "保存用户名/密码";
            this.chkSaveManage.Size = new Size(0x70, 0x13);
            this.chkSaveManage.TabIndex = 4;
            this.txtPaswordMan.EditValue = "";
            this.txtPaswordMan.Location = new Point(0x44, 0x34);
            this.txtPaswordMan.Name = "txtPaswordMan";
            this.txtPaswordMan.Properties.PasswordChar = '*';
            this.txtPaswordMan.Size = new Size(0x130, 0x15);
            this.txtPaswordMan.TabIndex = 3;
            this.txtUserManage.EditValue = "";
            this.txtUserManage.Location = new Point(0x44, 20);
            this.txtUserManage.Name = "txtUserManage";
            this.txtUserManage.Size = new Size(0x130, 0x15);
            this.txtUserManage.TabIndex = 2;
            this.txtUserManage.EditValueChanged += new EventHandler(this.txtUserManage_EditValueChanged);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x10, 0x38);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x2f, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "密  码:";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x10, 0x18);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x2f, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "用户名:";
            this.btnSelect.Enabled = false;
            this.btnSelect.Location = new Point(0x170, 0x63);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new Size(0x1f, 0x1c);
            this.btnSelect.TabIndex = 0x1b;
            this.btnSelect.Text = "...";
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.Items.AddRange(new object[] { "ArcGIS Server" });
            this.cboServerType.Location = new Point(0x60, 0x47);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new Size(0x12f, 20);
            this.cboServerType.TabIndex = 0x1a;
            this.cboServerType.Text = "ArcGIS Server";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x13, 0x4a);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x47, 12);
            this.label7.TabIndex = 0x19;
            this.label7.Text = "服务器类型:";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x5e, 0x22);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0xfb, 12);
            this.label6.TabIndex = 0x18;
            this.label6.Text = "ArcGIS Server:http://myserver:6080/arcgis";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x13, 15);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "服务器URL:";
            this.txtMUrl.EditValue = "http://";
            this.txtMUrl.Location = new Point(0x60, 10);
            this.txtMUrl.Name = "txtMUrl";
            this.txtMUrl.Size = new Size(0x12f, 0x15);
            this.txtMUrl.TabIndex = 0x13;
            this.txtTempFolder.EditValue = "";
            this.txtTempFolder.Enabled = false;
            this.txtTempFolder.Location = new Point(0x60, 0x69);
            this.txtTempFolder.Name = "txtTempFolder";
            this.txtTempFolder.Size = new Size(0x10a, 0x15);
            this.txtTempFolder.TabIndex = 0x12;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 0x6d);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x47, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "过渡文件夹:";
            this.radioGroup1.Location = new Point(12, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "使用GIS服务"), new RadioGroupItem(null, "管理GIS服务"), new RadioGroupItem(null, "发布GIS服务") });
            this.radioGroup1.Size = new Size(0x1c0, 0x1f);
            this.radioGroup1.TabIndex = 0x1b;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1db, 0x181);
            base.Controls.Add(this.radioGroup1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewArcGISServer";
            this.Text = "添加ArcGIS Server";
            base.Load += new EventHandler(this.frmNewArcGISServer_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.chkSaveUserNameAndPsw.Properties.EndInit();
            this.txtPassword.Properties.EndInit();
            this.txtUser.Properties.EndInit();
            this.txtServer.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.chkUseDefaultFolder.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.chkSaveManage.Properties.EndInit();
            this.txtPaswordMan.Properties.EndInit();
            this.txtUserManage.Properties.EndInit();
            this.txtMUrl.Properties.EndInit();
            this.txtTempFolder.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
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
            IPropertySet set = new PropertySetClass();
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
            IPropertySet set = new PropertySetClass();
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

