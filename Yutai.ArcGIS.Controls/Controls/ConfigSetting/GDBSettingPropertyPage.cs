using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal partial class GDBSettingPropertyPage : UserControl
    {
        private XmlNode m_appSettingsNode = null;
        private bool m_CanDo = false;

        public GDBSettingPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnSelectMDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "*.mdb|*.mdb"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtMDB.Text = dialog.FileName;
                AppConfig.layerconfigdb = this.txtMDB.Text;
                string directoryName = Path.GetDirectoryName(AppConfig.m_strConfigfile);
                string str2 = Path.GetDirectoryName(this.txtMDB.Text);
                string text = this.txtMDB.Text;
                if (directoryName == str2)
                {
                    text = Path.GetFileName(this.txtMDB.Text);
                }
                this.ChangeValue("gdbconnection", string.Format("dbclient=MDB;GDBName={0}", text));
                this.EnableControl();
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if ((this.radioGroup1.SelectedIndex == 0) || (this.radioGroup1.SelectedIndex == 1))
            {
                IPropertySet connectionProperties = new PropertySetClass();
                string str = this.txtServer.Text.Trim();
                connectionProperties.SetProperty("DB_CONNECTION_PROPERTIES", str);
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    connectionProperties.SetProperty("DBCLIENT", "sqlserver");
                    connectionProperties.SetProperty("Database", this.cboDatabase.Text);
                }
                else
                {
                    connectionProperties.SetProperty("DBCLIENT", "oracle");
                }
                if (this.chkIsOperateSystemYZ.Checked)
                {
                    connectionProperties.SetProperty("AUTHENTICATION_MODE", "OSA");
                }
                else
                {
                    connectionProperties.SetProperty("AUTHENTICATION_MODE", "DBMS");
                    connectionProperties.SetProperty("User", this.txtUser.Text);
                    connectionProperties.SetProperty("Password", this.txtPassword.Text);
                }
                IWorkspaceFactory factory = new SdeWorkspaceFactoryClass();
                try
                {
                    AppConfig.m_pWorkspace = factory.Open(connectionProperties, 0);
                    MessageBox.Show("空间数据库连接成功!", "测试连接");
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
            else if (this.txtMDB.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入个人数据库");
            }
            else if (!File.Exists(this.txtMDB.Text.Trim()))
            {
                MessageBox.Show("指定的个人数据库不存在");
            }
            else
            {
                MessageBox.Show("空间数据库连接成功!", "测试连接");
            }
        }

        private void cboDatabase_DropDown(object sender, EventArgs e)
        {
            if (this.cboDatabase.Items.Count == 0)
            {
                if (this.txtServer.Text.Length == 0)
                {
                    MessageBox.Show("请输入数据库实例名称!");
                }
                else
                {
                    if (!this.chkIsOperateSystemYZ.Checked)
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
                    string connectionString = this.GetConnectionString();
                    string cmdText = "SELECT name, filename FROM sysdatabases where dbid>4;";
                    SqlConnection connection = new SqlConnection(connectionString);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = new SqlCommand(cmdText, connection).ExecuteReader();
                        while (reader.Read())
                        {
                            if (File.Exists(reader["filename"].ToString()))
                            {
                                this.cboDatabase.Items.Add(reader["Name"]);
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                    finally
                    {
                        if (connection != null)
                        {
                            connection.Dispose();
                        }
                    }
                }
            }
        }

        private void cboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            if (this.cboDatabase.SelectedIndex >= 0)
            {
                string entConnection = this.GetEntConnection();
                this.ChangeValue("gdbconnection", entConnection);
            }
        }

        private void ChangeValue(string ChangeKey, string Value)
        {
            AppConfig.m_pWorkspace = null;
            if (this.m_appSettingsNode != null)
            {
                bool flag = false;
                for (int i = 0; i < this.m_appSettingsNode.ChildNodes.Count; i++)
                {
                    XmlNode node = this.m_appSettingsNode.ChildNodes[i];
                    if (node.NodeType != XmlNodeType.Comment)
                    {
                        XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[i].Attributes;
                        string str = attributes["key"].Value.ToLower();
                        if (ChangeKey == str)
                        {
                            flag = true;
                            attributes["value"].Value = Value;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "add", "");
                    XmlAttribute attribute = AppConfig.m_AppConfig.CreateAttribute("key");
                    attribute.Value = ChangeKey;
                    newChild.Attributes.Append(attribute);
                    attribute = AppConfig.m_AppConfig.CreateAttribute("value");
                    attribute.Value = Value;
                    newChild.Attributes.Append(attribute);
                    this.m_appSettingsNode.AppendChild(newChild);
                }
            }
        }

        private void chkIsOperateSystemYZ_CheckedChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            this.txtUser.Enabled = !this.chkIsOperateSystemYZ.Checked;
            this.txtPassword.Enabled = !this.chkIsOperateSystemYZ.Checked;
        }

        private void EnableControl()
        {
            this.btnTestConnection.Enabled = true;
            string str = this.txtServer.Text.Trim();
            if ((this.radioGroup1.SelectedIndex == 0) || (this.radioGroup1.SelectedIndex == 1))
            {
                this.btnTestConnection.Enabled = true;
                if (str.Length == 0)
                {
                    this.btnTestConnection.Enabled = false;
                }
                else if (this.chkIsOperateSystemYZ.Checked)
                {
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        this.btnTestConnection.Enabled = this.cboDatabase.Text.Length > 0;
                    }
                }
                else
                {
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        this.btnTestConnection.Enabled = this.cboDatabase.Text.Length > 0;
                    }
                    if (this.txtPassword.Text.Trim().Length == 0)
                    {
                        this.btnTestConnection.Enabled = false;
                    }
                    else if (this.txtUser.Text.Trim().Length == 0)
                    {
                        this.btnTestConnection.Enabled = false;
                    }
                    else
                    {
                        this.btnTestConnection.Enabled = true;
                    }
                }
            }
            else if (this.txtMDB.Text.Trim().Length == 0)
            {
                this.btnTestConnection.Enabled = false;
            }
        }

        private void GDBSettingPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private string GetConnectionString()
        {
            if (!this.chkIsOperateSystemYZ.Checked)
            {
                return
                    string.Format(
                        "Data Source={0};Initial Catalog=master;Integrated Security=False;User Id={1};Password={2} ",
                        this.txtServer.Text, this.txtUser.Text, this.txtPassword.Text);
            }
            return string.Format("Data Source={0};Initial Catalog=master;Integrated Security=SSPI;", this.txtServer.Text);
        }

        private string GetEntConnection()
        {
            return string.Format("dbclient={0};server={1};authentication_mode={2};user={3};password={4};database={5}",
                new object[]
                {
                    AppConfig.dbclient, this.txtServer.Text, this.chkIsOperateSystemYZ.Checked ? "OSA" : "DBMS",
                    this.txtUser.Text, this.txtPassword.Text, this.cboDatabase.Text
                });
        }

        public void Init()
        {
            this.m_CanDo = false;
            XmlElement documentElement = AppConfig.m_AppConfig.DocumentElement;
            if (documentElement != null)
            {
                AppConfig.oledb = "";
                this.m_appSettingsNode = documentElement.SelectSingleNode("appSettings");
                if (this.m_appSettingsNode == null)
                {
                    XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "appSettings", "");
                    documentElement.AppendChild(newChild);
                    XmlNode node2 = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "add", "");
                    XmlAttribute node = AppConfig.m_AppConfig.CreateAttribute("key");
                    node.Value = "GDBConnection";
                    node2.Attributes.Append(node);
                    node = AppConfig.m_AppConfig.CreateAttribute("value");
                    node.Value = "";
                    node2.Attributes.Append(node);
                    newChild.AppendChild(node2);
                    this.m_appSettingsNode = documentElement.SelectSingleNode("appSettings");
                }
                if (this.m_appSettingsNode != null)
                {
                    this.txtServer.Text = "";
                    this.txtUser.Text = "";
                    this.txtPassword.Text = "";
                    string str = "SQL";
                    this.radioGroup1.SelectedIndex = 0;
                    this.txtMDB.Text = "";
                    for (int i = 0; i < this.m_appSettingsNode.ChildNodes.Count; i++)
                    {
                        XmlNode node3 = this.m_appSettingsNode.ChildNodes[i];
                        if (node3.NodeType != XmlNodeType.Comment)
                        {
                            XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[i].Attributes;
                            string str2 = attributes["key"].Value.ToLower();
                            string str3 = attributes["value"].Value;
                            if (str2 == "gdbconnection")
                            {
                                string[] strArray = str3.Split(new char[] {';'});
                                foreach (string str5 in strArray)
                                {
                                    string[] strArray2 = str5.Split(new char[] {'='});
                                    switch (strArray2[0].ToLower())
                                    {
                                        case "dbclient":
                                        {
                                            str = strArray2[1].ToUpper();
                                            AppConfig.dbclient = str;
                                            if (!(str == "SDE"))
                                            {
                                                break;
                                            }
                                            this.radioGroup1.SelectedIndex = 0;
                                            continue;
                                        }
                                        case "server":
                                        {
                                            AppConfig.server = strArray2[1];
                                            this.txtServer.Text = strArray2[1];
                                            continue;
                                        }
                                        case "user":
                                        {
                                            AppConfig.user = strArray2[1];
                                            this.txtUser.Text = strArray2[1];
                                            continue;
                                        }
                                        case "password":
                                        {
                                            AppConfig.password = strArray2[1];
                                            this.txtPassword.Text = strArray2[1];
                                            continue;
                                        }
                                        case "authentication_mode":
                                        {
                                            AppConfig.authentication_mode = strArray2[1];
                                            if (!(AppConfig.authentication_mode.ToLower() == "osa"))
                                            {
                                                goto Label_041E;
                                            }
                                            this.chkIsOperateSystemYZ.Checked = true;
                                            continue;
                                        }
                                        case "database":
                                        {
                                            AppConfig.database = strArray2[1];
                                            this.cboDatabase.Text = strArray2[1];
                                            continue;
                                        }
                                        case "gdbname":
                                        {
                                            AppConfig.layerconfigdb = strArray2[1];
                                            if (AppConfig.layerconfigdb[1] != ':')
                                            {
                                                AppConfig.layerconfigdb = Path.Combine(Application.StartupPath,
                                                    AppConfig.layerconfigdb);
                                            }
                                            this.txtMDB.Text = AppConfig.layerconfigdb;
                                            continue;
                                        }
                                        default:
                                        {
                                            continue;
                                        }
                                    }
                                    if (str == "SQLSERVER")
                                    {
                                        this.radioGroup1.SelectedIndex = 0;
                                    }
                                    else if (str == "ORACLE")
                                    {
                                        this.radioGroup1.SelectedIndex = 1;
                                    }
                                    else
                                    {
                                        this.radioGroup1.SelectedIndex = 2;
                                    }
                                    continue;
                                    Label_041E:
                                    this.chkIsOperateSystemYZ.Checked = false;
                                }
                            }
                        }
                    }
                }
                if (AppConfig.oledb == "")
                {
                    AppConfig.oledb = AppConfig.server;
                }
                this.EnableControl();
                this.m_CanDo = true;
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "";
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = false;
                AppConfig.dbclient = "sqlserver";
                this.chkIsOperateSystemYZ.Enabled = true;
                str = string.Format(
                    "dbclient={0};server={1};authentication_mode={2};user={3};password={4};database={5}",
                    new object[]
                    {
                        AppConfig.dbclient, this.txtServer.Text, this.chkIsOperateSystemYZ.Checked ? "OSA" : "DBMS",
                        this.txtUser.Text, this.txtPassword.Text, "", this.cboDatabase.Text
                    });
                this.cboDatabase.Visible = true;
                this.label3.Visible = true;
            }
            else if (this.radioGroup1.SelectedIndex == 1)
            {
                if (this.chkIsOperateSystemYZ.Checked)
                {
                    this.chkIsOperateSystemYZ.Checked = false;
                }
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = false;
                AppConfig.dbclient = "Oracle";
                this.cboDatabase.Visible = false;
                this.label3.Visible = false;
                str = string.Format(
                    "dbclient={0};server={1};authentication_mode={2};user={3};password={4};database={5}",
                    new object[]
                    {
                        AppConfig.dbclient, this.txtServer.Text, this.chkIsOperateSystemYZ.Checked ? "OSA" : "DBMS",
                        this.txtUser.Text, this.txtPassword.Text, "", this.cboDatabase.Text
                    });
            }
            else
            {
                this.groupBox2.Enabled = true;
                this.groupBox1.Enabled = false;
                AppConfig.dbclient = "MDB";
                if (this.txtMDB.Text.Length > 0)
                {
                    string directoryName = Path.GetDirectoryName(AppConfig.m_strConfigfile);
                    string str3 = Path.GetDirectoryName(this.txtMDB.Text);
                    string text = this.txtMDB.Text;
                    if (directoryName == str3)
                    {
                        text = Path.GetFileName(this.txtMDB.Text);
                    }
                    str = string.Format("dbclient=MDB;GDBName={0}", text);
                }
            }
            AppConfig.m_pWorkspace = null;
            this.EnableControl();
            if (this.m_CanDo)
            {
                this.ChangeValue("gdbconnection", str);
            }
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            if (this.m_CanDo)
            {
                AppConfig.password = this.txtPassword.Text;
                string entConnection = this.GetEntConnection();
                this.ChangeValue("gdbconnection", entConnection);
            }
        }

        private void txtServer_EditValueChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            if (this.m_CanDo)
            {
                this.cboDatabase.Items.Clear();
                AppConfig.server = this.txtServer.Text;
                string entConnection = this.GetEntConnection();
                this.ChangeValue("gdbconnection", entConnection);
            }
        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            if (this.m_CanDo)
            {
                AppConfig.user = this.txtUser.Text;
                string entConnection = this.GetEntConnection();
                this.ChangeValue("gdbconnection", entConnection);
            }
        }

        private void txtVersion_EditValueChanged(object sender, EventArgs e)
        {
        }
    }
}