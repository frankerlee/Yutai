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
    public partial class frmCreateGDBConnection : Form
    {
        private IContainer icontainer_0 = null;
        private string string_0 = "";
        private string string_1 = "";
        [CompilerGenerated]

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
                    if ((errorCode == 2147751273) || (errorCode == 2147751169))
                    {
                        MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                    }
                    else if (errorCode == 2147751274)
                    {
                        MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                    }
                    else if (errorCode == 2147500037)
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
                    if ((errorCode == 2147751273) || (errorCode == 2147751169))
                    {
                        MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                    }
                    else if (errorCode == 2147751274)
                    {
                        MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                    }
                    else if (errorCode == 2147500037)
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

 private void frmCreateGDBConnection_Load(object sender, EventArgs e)
        {
            this.string_0 = this.cboServerType.Text;
            this.string_1 = this.cboYZType.Text;
            this.btnOK.Enabled = false;
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

