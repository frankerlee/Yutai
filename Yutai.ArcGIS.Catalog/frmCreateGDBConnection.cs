using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog
{
    public class frmCreateGDBConnection : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private Button button1;
        private ComboBox cboDatabase;
        private ComboBox cboServerType;
        private ComboBox cboYZType;
        private CheckBox chkSavePassword;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private string string_0 = "";
        private string string_1 = "";
        [CompilerGenerated]
        private string string_2;
        private TextBox txtDatabaseInstace;
        private TextBox txtPassword;
        private TextBox txtUser;

        public frmCreateGDBConnection()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                IPropertySet connectionProperties = this.method_1(true);
                if (connectionProperties != null)
                {
                    string path = Environment.SystemDirectory.Substring(0, 2) + @"Users\Administrator\AppData\Roaming\ESRI\Desktop10.2\ArcCatalog\";
                    string str2 = RegistryTools.GetRegistryKey("HKEY_CURRENT_USER", @"Software\ESRI\Desktop10.2\CoreRuntime\Locator\Settings", "LocatorDirectory");
                    if (!string.IsNullOrEmpty(str2) && ((str2.IndexOf(@"Locators\", StringComparison.OrdinalIgnoreCase) > 0) && (str2.IndexOf("ArcCatalog", StringComparison.OrdinalIgnoreCase) == -1)))
                    {
                        path = str2.Replace("Locators", "ArcCatalog");
                    }
                    if (Directory.Exists(path))
                    {
                        string str3 = this.txtDatabaseInstace.Text.Trim();
                        if (str3 == ".")
                        {
                            str3 = "local-";
                        }
                        if (str3.IndexOf(@"\") != -1)
                        {
                            str3 = str3.Replace(@"\", "_");
                        }
                        if (this.cboDatabase.Text.Length > 0)
                        {
                            str3 = str3 + "-" + this.cboDatabase.Text;
                        }
                        str3 = str3 + "-" + this.cboServerType.Text;
                        string str4 = path + "连接到" + str3 + ".sde";
                        str4 = this.method_2(str4);
                        this.ConnectionPath = str4;
                        ((IWorkspaceFactory) Activator.CreateInstance(System.Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory"))).Create(path, Path.GetFileName(str4), connectionProperties, 0);
                        base.DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception exception)
            {
                if (exception is COMException)
                {
                    uint errorCode = (uint) (exception as COMException).ErrorCode;
                    if ((errorCode == 0x80041569) || (errorCode == 0x80041501))
                    {
                        MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                    }
                    else if (errorCode == 0x8004156a)
                    {
                        MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                    }
                    else if (errorCode == 0x80004005)
                    {
                        MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                    }
                    else
                    {
                        MessageBox.Show(exception.Message, "测试连接");
                    }
                }
                else
                {
                    MessageBox.Show(exception.Message, "测试连接");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            try
            {
                IPropertySet connectionProperties = this.method_1(true);
                if (connectionProperties != null)
                {
                    ((IWorkspaceFactory) Activator.CreateInstance(System.Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory"))).Open(connectionProperties, 0);
                    System.Windows.Forms.Cursor.Current = Cursors.Default;
                    this.btnOK.Enabled = true;
                    MessageBox.Show("连接成功！");
                }
                return;
            }
            catch (Exception exception)
            {
                this.btnOK.Enabled = false;
                if (exception is COMException)
                {
                    uint errorCode = (uint) (exception as COMException).ErrorCode;
                    if ((errorCode == 0x80041569) || (errorCode == 0x80041501))
                    {
                        MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                    }
                    else if (errorCode == 0x8004156a)
                    {
                        MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                    }
                    else if (errorCode == 0x80004005)
                    {
                        MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                    }
                    else
                    {
                        MessageBox.Show(exception.Message, "测试连接");
                    }
                }
                else
                {
                    MessageBox.Show(exception.Message, "测试连接");
                }
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void cboDatabase_DropDown(object sender, EventArgs e)
        {
            if (this.cboServerType.Text == "SQL Server")
            {
                if (this.cboDatabase.Items.Count != 0)
                {
                    return;
                }
                if (this.txtDatabaseInstace.Text.Length == 0)
                {
                    MessageBox.Show("请输入数据库实例名称!");
                    return;
                }
                if (this.cboYZType.SelectedIndex == 0)
                {
                    if (this.txtUser.Text.Length == 0)
                    {
                        MessageBox.Show("请输入登陆用户名!");
                        return;
                    }
                    if (this.txtPassword.Text.Length == 0)
                    {
                        MessageBox.Show("请输入登陆密码!");
                        return;
                    }
                }
                string connectionString = this.method_0();
                string cmdText = "SELECT name, filename FROM sysdatabases where dbid>4;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = new SqlCommand(cmdText, connection).ExecuteReader();
                        while (reader.Read())
                        {
                            this.cboDatabase.Items.Add(reader["Name"]);
                        }
                        reader.Close();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    return;
                }
            }
            if (!(this.cboServerType.Text == "Oracle"))
            {
            }
        }

        private void cboServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.string_0 != this.cboServerType.Text)
            {
                this.txtPassword.Text = "";
                this.txtUser.Text = "";
                this.string_0 = this.cboServerType.Text;
                this.cboDatabase.Items.Clear();
            }
            if (this.cboServerType.SelectedIndex == 0)
            {
                this.cboYZType.Enabled = true;
            }
            else if (this.cboYZType.SelectedIndex != 0)
            {
                this.cboYZType.SelectedIndex = 0;
            }
            this.panel1.Visible = this.cboServerType.Text != "Oracle";
        }

        private void cboYZType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtUser.Enabled = this.cboYZType.SelectedIndex == 0;
            this.txtPassword.Enabled = this.cboYZType.SelectedIndex == 0;
            this.chkSavePassword.Visible = this.cboYZType.SelectedIndex == 0;
            if (this.string_1 != this.cboYZType.Text)
            {
                this.string_1 = this.cboYZType.Text;
                this.cboDatabase.Items.Clear();
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCreateGDBConnection_Load(object sender, EventArgs e)
        {
            this.string_0 = this.cboServerType.Text;
            this.string_1 = this.cboYZType.Text;
            this.btnOK.Enabled = false;
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cboServerType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDatabaseInstace = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboYZType = new System.Windows.Forms.ComboBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboDatabase = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkSavePassword = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库平台";
            // 
            // cboServerType
            // 
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.Items.AddRange(new object[] {
            "SQL Server",
            "Oracle"});
            this.cboServerType.Location = new System.Drawing.Point(96, 16);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new System.Drawing.Size(224, 20);
            this.cboServerType.TabIndex = 1;
            this.cboServerType.Text = "SQL Server";
            this.cboServerType.SelectedIndexChanged += new System.EventHandler(this.cboServerType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据库实例";
            // 
            // txtDatabaseInstace
            // 
            this.txtDatabaseInstace.Location = new System.Drawing.Point(96, 67);
            this.txtDatabaseInstace.Name = "txtDatabaseInstace";
            this.txtDatabaseInstace.Size = new System.Drawing.Size(224, 21);
            this.txtDatabaseInstace.TabIndex = 3;
            this.txtDatabaseInstace.TextChanged += new System.EventHandler(this.txtDatabaseInstace_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "身份验证方式";
            // 
            // cboYZType
            // 
            this.cboYZType.FormattingEnabled = true;
            this.cboYZType.Items.AddRange(new object[] {
            "数据库身份验证",
            "操作系统身份验证"});
            this.cboYZType.Location = new System.Drawing.Point(96, 118);
            this.cboYZType.Name = "cboYZType";
            this.cboYZType.Size = new System.Drawing.Size(224, 20);
            this.cboYZType.TabIndex = 5;
            this.cboYZType.Text = "操作系统身份验证";
            this.cboYZType.SelectedIndexChanged += new System.EventHandler(this.cboYZType_SelectedIndexChanged);
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(141, 157);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(179, 21);
            this.txtUser.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "用户名";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(141, 195);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(179, 21);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码";
            // 
            // cboDatabase
            // 
            this.cboDatabase.FormattingEnabled = true;
            this.cboDatabase.Location = new System.Drawing.Point(90, 11);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(224, 20);
            this.cboDatabase.TabIndex = 11;
            this.cboDatabase.DropDown += new System.EventHandler(this.cboDatabase_DropDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "数据库";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cboDatabase);
            this.panel1.Location = new System.Drawing.Point(6, 257);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 42);
            this.panel1.TabIndex = 12;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(150, 316);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(259, 316);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkSavePassword
            // 
            this.chkSavePassword.AutoSize = true;
            this.chkSavePassword.Location = new System.Drawing.Point(96, 228);
            this.chkSavePassword.Name = "chkSavePassword";
            this.chkSavePassword.Size = new System.Drawing.Size(72, 16);
            this.chkSavePassword.TabIndex = 15;
            this.chkSavePassword.Text = "保存密码";
            this.chkSavePassword.UseVisualStyleBackColor = true;
            this.chkSavePassword.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 311);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "测试连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmCreateGDBConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 346);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkSavePassword);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboYZType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatabaseInstace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboServerType);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateGDBConnection";
            this.Text = "数据库连接";
            this.Load += new System.EventHandler(this.frmCreateGDBConnection_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private string method_0()
        {
            if (this.cboYZType.SelectedIndex == 0)
            {
                return string.Format("Data Source={0};Initial Catalog=master;Integrated Security=False;User Id={1};Password={2} ", this.txtDatabaseInstace.Text, this.txtUser.Text, this.txtPassword.Text);
            }
            return string.Format("Data Source={0};Initial Catalog=master;Integrated Security=SSPI;", this.txtDatabaseInstace.Text);
        }

        private IPropertySet method_1(bool bool_0)
        {
            if (this.txtDatabaseInstace.Text.Length == 0)
            {
                MessageBox.Show("请输入实例");
                return null;
            }
            IPropertySet set2 = new PropertySetClass();
            if (this.cboServerType.SelectedIndex == 0)
            {
                set2.SetProperty("DBCLIENT", "sqlserver");
            }
            else
            {
                set2.SetProperty("DBCLIENT", "oracle");
            }
            set2.SetProperty("DB_CONNECTION_PROPERTIES", this.txtDatabaseInstace.Text);
            if (this.cboServerType.Text == "SQL Server")
            {
                if (this.cboDatabase.Text.Length == 0)
                {
                    MessageBox.Show("请选择数据库");
                    return null;
                }
                set2.SetProperty("DATABASE", this.cboDatabase.Text);
            }
            else if (!(this.cboServerType.Text == "Oracle"))
            {
            }
            if (this.cboYZType.SelectedIndex == 0)
            {
                if (this.txtUser.Text.Length == 0)
                {
                    MessageBox.Show("请输入用户名");
                    return null;
                }
                if (this.txtPassword.Text.Length == 0)
                {
                    MessageBox.Show("请输入密码名");
                    return null;
                }
                set2.SetProperty("AUTHENTICATION_MODE", "DBMS");
                set2.SetProperty("USER", this.txtUser.Text);
                set2.SetProperty("PASSWORD", this.txtPassword.Text);
                return set2;
            }
            set2.SetProperty("AUTHENTICATION_MODE", "OSA");
            return set2;
        }

        private string method_2(string string_3)
        {
            string str = string_3.Substring(0, string_3.Length - 4);
            for (int i = 1; File.Exists(string_3); i++)
            {
                string_3 = str + " (" + i.ToString() + ").sde";
            }
            return string_3;
        }

        private void txtDatabaseInstace_TextChanged(object sender, EventArgs e)
        {
            this.cboDatabase.Items.Clear();
        }

        public string ConnectionPath
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
    }
}

