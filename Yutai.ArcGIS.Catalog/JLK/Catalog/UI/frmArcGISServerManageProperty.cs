namespace JLK.Catalog.UI
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.GISClient;
    using JLK.Catalog;
    using JLK.Editors;
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
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private System.Windows.Forms.ComboBox cboServerType;
        private CheckEdit chkSaveManage;
        private CheckEdit chkUseDefaultFolder;
        private GroupBox groupBox1;
        private IAGSServerConnection iagsserverConnection_0;
        [CompilerGenerated]
        private IAGSServerConnectionName iagsserverConnectionName_0;
        private IContainer icontainer_0 = null;
        private IGxObject igxObject_0 = null;
        private IMapDescription imapDescription_0;
        private IMapServer imapServer_0;
        [CompilerGenerated]
        private IPropertySet ipropertySet_0;
        private Label label1;
        private Label label4;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private SimpleButton simpleButton1;
        private string string_0;
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        private TextEdit txtMUrl;
        private TextEdit txtPaswordMan;
        private TextEdit txtTempFolder;
        private TextEdit txtUserManage;

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
                IAGSServerConnectionFactory factory = new AGSServerConnectionFactoryClass();
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
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.chkSaveManage = new CheckEdit();
            this.txtPaswordMan = new TextEdit();
            this.txtUserManage = new TextEdit();
            this.label8 = new Label();
            this.label9 = new Label();
            this.simpleButton1 = new SimpleButton();
            this.cboServerType = new System.Windows.Forms.ComboBox();
            this.label7 = new Label();
            this.label6 = new Label();
            this.label4 = new Label();
            this.txtMUrl = new TextEdit();
            this.txtTempFolder = new TextEdit();
            this.label1 = new Label();
            this.chkUseDefaultFolder = new CheckEdit();
            this.groupBox1.SuspendLayout();
            this.chkSaveManage.Properties.BeginInit();
            this.txtPaswordMan.Properties.BeginInit();
            this.txtUserManage.Properties.BeginInit();
            this.txtMUrl.Properties.BeginInit();
            this.txtTempFolder.Properties.BeginInit();
            this.chkUseDefaultFolder.Properties.BeginInit();
            base.SuspendLayout();
            this.btnOK.Location = new Point(0xe0, 0x11e);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(60, 0x1c);
            this.btnOK.TabIndex = 0x18;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(0x135, 0x11e);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(60, 0x1c);
            this.btnCancel.TabIndex = 0x17;
            this.btnCancel.Text = "取消";
            this.groupBox1.Controls.Add(this.chkSaveManage);
            this.groupBox1.Controls.Add(this.txtPaswordMan);
            this.groupBox1.Controls.Add(this.txtUserManage);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new Point(14, 0x99);
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
            this.simpleButton1.Location = new Point(0x169, 0x5d);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x1f, 0x1c);
            this.simpleButton1.TabIndex = 0x1b;
            this.simpleButton1.Text = "...";
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.Items.AddRange(new object[] { "ArcGIS Server" });
            this.cboServerType.Location = new Point(0x59, 0x41);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new Size(0x12f, 20);
            this.cboServerType.TabIndex = 0x1a;
            this.cboServerType.Text = "ArcGIS Server";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(12, 0x44);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x47, 12);
            this.label7.TabIndex = 0x19;
            this.label7.Text = "服务器类型:";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x57, 0x1c);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x131, 12);
            this.label6.TabIndex = 0x18;
            this.label6.Text = "ArcGIS Server:http://myserver:6080/arcgis/services";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "服务器URL:";
            this.txtMUrl.EditValue = "http://";
            this.txtMUrl.Location = new Point(0x59, 4);
            this.txtMUrl.Name = "txtMUrl";
            this.txtMUrl.Size = new Size(0x12f, 0x15);
            this.txtMUrl.TabIndex = 0x13;
            this.txtTempFolder.EditValue = "";
            this.txtTempFolder.Location = new Point(0x59, 0x63);
            this.txtTempFolder.Name = "txtTempFolder";
            this.txtTempFolder.Size = new Size(0x10a, 0x15);
            this.txtTempFolder.TabIndex = 0x12;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 0x67);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x47, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "过渡文件夹:";
            this.chkUseDefaultFolder.EditValue = true;
            this.chkUseDefaultFolder.Location = new Point(12, 0x80);
            this.chkUseDefaultFolder.Name = "chkUseDefaultFolder";
            this.chkUseDefaultFolder.Properties.Caption = "使用默认过渡文件夹";
            this.chkUseDefaultFolder.Size = new Size(0x8a, 0x13);
            this.chkUseDefaultFolder.TabIndex = 30;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a0, 0x14f);
            base.Controls.Add(this.chkUseDefaultFolder);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.simpleButton1);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cboServerType);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtTempFolder);
            base.Controls.Add(this.txtMUrl);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmArcGISServerManageProperty";
            this.Text = "ArcGIS Server管理员连接属性";
            base.Load += new EventHandler(this.frmArcGISServerManageProperty_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.chkSaveManage.Properties.EndInit();
            this.txtPaswordMan.Properties.EndInit();
            this.txtUserManage.Properties.EndInit();
            this.txtMUrl.Properties.EndInit();
            this.txtTempFolder.Properties.EndInit();
            this.chkUseDefaultFolder.Properties.EndInit();
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
            [CompilerGenerated]
            get
            {
                return this.iagsserverConnectionName_0;
            }
            [CompilerGenerated]
            set
            {
                this.iagsserverConnectionName_0 = value;
            }
        }

        public string ConnectionFile
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public IPropertySet ConnectionProperties
        {
            [CompilerGenerated]
            get
            {
                return this.ipropertySet_0;
            }
            [CompilerGenerated]
            set
            {
                this.ipropertySet_0 = value;
            }
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

