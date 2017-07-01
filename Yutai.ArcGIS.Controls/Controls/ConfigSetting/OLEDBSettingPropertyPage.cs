using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Yutai.ArcGIS.Common.Data;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal partial class OLEDBSettingPropertyPage : UserControl
    {
        private XmlNode m_appSettingsNode = null;
        private bool m_CanDo = false;
        private int m_type = 0;
        private string OLETemplete = "Provider=Microsoft.Jet.OLEDB.4.0; Data source= #DATABASE#";
        private string OraceleTemplete = "Provider=MSDAORA.1;Data Source=#SERVER#;User Id=#USER#;Password=#PASSWORD#";

        private string SQLTemplete =
            "Integrated Security=False; Data Source=#SERVER#;User Id=#USER#;Password=#PASSWORD#;Initial Catalog= #DATABASE#";

        public OLEDBSettingPropertyPage(int type)
        {
            this.InitializeComponent();
            this.m_type = type;
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
                string str = "Provider=Microsoft.Jet.OLEDB.4.0; Data source= " + this.txtMDB.Text;
                if (this.m_type == 0)
                {
                    this.ChangeValue("ghxbconnection", str);
                }
                else
                {
                    this.ChangeValue("sysprivdb", "OleDB||" + str);
                }
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            DataProviderType oleDb = DataProviderType.OleDb;
            string str = "";
            if (this.radioGroup1.SelectedIndex == 0)
            {
                str =
                    this.SQLTemplete.Replace("#SERVER#", this.txtServer.Text)
                        .Replace("#USER#", this.txtUser.Text)
                        .Replace("#PASSWORD#", this.txtPassword.Text)
                        .Replace("#DATABASE#", this.txtDatabase.Text);
                oleDb = DataProviderType.Sql;
            }
            else if (this.radioGroup1.SelectedIndex == 1)
            {
                str =
                    this.OraceleTemplete.Replace("#SERVER#", this.txtServer.Text)
                        .Replace("#USER#", this.txtUser.Text)
                        .Replace("#PASSWORD#", this.txtPassword.Text);
                oleDb = DataProviderType.OleDb;
            }
            else
            {
                str = this.OLETemplete.Replace("#DATABASE#", this.txtServer.Text);
            }
            if (str != "")
            {
                DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(oleDb, str);
                try
                {
                    try
                    {
                        if (dataAccessLayer.Open())
                        {
                            MessageBox.Show("连接成功！");
                            dataAccessLayer.Close();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
                finally
                {
                }
            }
        }

        private void ChangeValue(string ChangeKey, string Value)
        {
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

        private void EnableControl()
        {
            this.btnTestConnection.Enabled = true;
            if (this.txtUser.Text.Trim().Length == 0)
            {
                this.btnTestConnection.Enabled = false;
            }
            else if (this.txtServer.Text.Trim().Length == 0)
            {
                this.btnTestConnection.Enabled = false;
            }
            else
            {
                if (this.txtPassword.Text.Trim().Length == 0)
                {
                    this.btnTestConnection.Enabled = false;
                }
                else
                {
                    this.btnTestConnection.Enabled = true;
                }
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    if (this.txtDatabase.Text.Trim().Length == 0)
                    {
                        this.btnTestConnection.Enabled = false;
                    }
                    else
                    {
                        this.btnTestConnection.Enabled = true;
                    }
                }
            }
        }

        private void GDBSettingPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private string GetOleConnection()
        {
            string str = "Provider=";
            if (this.radioGroup1.SelectedIndex == 2)
            {
                return (str + "Microsoft.Jet.OLEDB.4.0; Data source= " + this.txtMDB.Text);
            }
            if (this.radioGroup1.SelectedIndex == -1)
            {
                return "无效连接";
            }
            if (this.txtServer.Text.Trim().Length == 0)
            {
                return "无效连接";
            }
            if (this.radioGroup1.SelectedIndex == 0)
            {
                str = "";
            }
            else
            {
                str = str + "MSDAORA.1;";
            }
            str = ((str + " Data Source= " + this.txtServer.Text.Trim() + ";") + "User Id= " + this.txtUser.Text.Trim() +
                   ";") + "Password= " + this.txtPassword.Text.Trim();
            if (this.radioGroup1.SelectedIndex != 0)
            {
                return str;
            }
            if (this.txtDatabase.Text.Trim().Length == 0)
            {
                str = str + ";Initial Catalog=sde";
            }
            else
            {
                str = str + ";Initial Catalog=" + this.txtDatabase.Text;
            }
            return (str + ";Integrated Security=False");
        }

        private string GetType()
        {
            if ((this.radioGroup1.SelectedIndex != 2) && (this.radioGroup1.SelectedIndex == 0))
            {
                return "SQLSERVER";
            }
            return "OleDB";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        public void Init()
        {
            this.m_CanDo = false;
            XmlElement documentElement = AppConfig.m_AppConfig.DocumentElement;
            this.m_appSettingsNode = documentElement.SelectSingleNode("appSettings");
            if (this.m_appSettingsNode == null)
            {
                XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "appSettings", "");
                documentElement.AppendChild(newChild);
                this.m_appSettingsNode = documentElement.SelectSingleNode("appSettings");
            }
            if (this.m_appSettingsNode != null)
            {
                this.txtDatabase.Text = "";
                this.txtServer.Text = "";
                this.txtUser.Text = "";
                this.txtPassword.Text = "";
                for (int i = 0; i < this.m_appSettingsNode.ChildNodes.Count; i++)
                {
                    XmlNode node2 = this.m_appSettingsNode.ChildNodes[i];
                    if (node2.NodeType != XmlNodeType.Comment)
                    {
                        XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[i].Attributes;
                        string str = attributes["key"].Value.ToLower();
                        string str2 = attributes["value"].Value;
                        if ((this.m_type == 0) && (str == "ghxbconnection"))
                        {
                            this.SplitString(str2);
                        }
                        else if ((this.m_type == 0) && (str == "ghxbconnectiontype"))
                        {
                            if (str2.ToLower() == "sql")
                            {
                                this.radioGroup1.SelectedIndex = 0;
                            }
                        }
                        else if ((this.m_type == 1) && (str == "sysprivdb"))
                        {
                            string[] strArray = str2.Split(new string[] {"||"}, StringSplitOptions.RemoveEmptyEntries);
                            this.SplitString(strArray[1]);
                        }
                    }
                }
            }
            this.m_CanDo = true;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                if (this.radioGroup1.SelectedIndex == 2)
                {
                    this.groupBox1.Enabled = false;
                    this.groupBox2.Enabled = true;
                    if (this.m_type == 0)
                    {
                        this.ChangeValue("ghxbconnection", this.GetOleConnection());
                        this.ChangeValue("ghxbconnectiontype", "OleDB");
                    }
                    else
                    {
                        this.ChangeValue("sysprivdb", "OleDB||" + this.GetOleConnection());
                    }
                }
                else if (this.radioGroup1.SelectedIndex == 0)
                {
                    this.groupBox1.Enabled = true;
                    this.groupBox2.Enabled = false;
                    this.txtDatabase.Enabled = this.radioGroup1.SelectedIndex == 0;
                    if (this.m_type == 0)
                    {
                        this.ChangeValue("ghxbconnection", this.GetOleConnection());
                        this.ChangeValue("ghxbconnectiontype", "SQL");
                    }
                    else
                    {
                        this.ChangeValue("sysprivdb", "SQLSERVER||" + this.GetOleConnection());
                    }
                    this.EnableControl();
                }
                else
                {
                    this.groupBox1.Enabled = true;
                    this.groupBox2.Enabled = false;
                    if (this.radioGroup1.SelectedIndex == 1)
                    {
                        this.txtDatabase.Text = "";
                    }
                    if (this.m_type == 0)
                    {
                        this.txtDatabase.Enabled = this.radioGroup1.SelectedIndex == 0;
                        this.ChangeValue("ghxbconnection", this.GetOleConnection());
                        this.ChangeValue("ghxbconnectiontype", "OleDB");
                    }
                    else
                    {
                        this.ChangeValue("sysprivdb", "OleDB||" + this.GetOleConnection());
                    }
                    this.EnableControl();
                }
            }
        }

        private void SplitString(string Value)
        {
            string[] strArray = Value.Split(new char[] {';'});
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] {'='});
                strArray2[0] = strArray2[0].Trim();
                strArray2[1] = strArray2[1].Trim();
                if (strArray2[0].ToLower() == "provider")
                {
                    if (strArray2[1].IndexOf("msdaora", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        this.radioGroup1.SelectedIndex = 1;
                        this.txtDatabase.Enabled = false;
                        this.groupBox1.Enabled = true;
                        this.groupBox2.Enabled = false;
                    }
                    else if (strArray2[1].IndexOf("sqloledb", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        this.radioGroup1.SelectedIndex = 0;
                        this.txtDatabase.Enabled = true;
                        this.groupBox1.Enabled = true;
                        this.groupBox2.Enabled = false;
                    }
                    else if (strArray2[1].IndexOf("Microsoft.Jet.OLEDB.4.0", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        this.radioGroup1.SelectedIndex = 2;
                        this.groupBox2.Enabled = true;
                        this.groupBox1.Enabled = true;
                    }
                }
                else if (strArray2[0].ToLower() == "data source")
                {
                    if (this.radioGroup1.SelectedIndex == 2)
                    {
                        this.txtMDB.Text = strArray2[1];
                    }
                    else
                    {
                        AppConfig.oledbserver = strArray2[1];
                        this.txtServer.Text = strArray2[1];
                    }
                }
                else if (strArray2[0].ToLower() == "user id")
                {
                    AppConfig.oledbuser = strArray2[1];
                    this.txtUser.Text = strArray2[1];
                }
                else if (strArray2[0].ToLower() == "password")
                {
                    AppConfig.oledbpassword = strArray2[1];
                    this.txtPassword.Text = strArray2[1];
                }
                else if (strArray2[0].ToLower() == "initial catalog")
                {
                    AppConfig.oledbdatabase = strArray2[1];
                    this.txtDatabase.Text = strArray2[1];
                }
            }
        }

        private void txtDatabase_EditValueChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            if (this.m_CanDo)
            {
                AppConfig.oledbdatabase = this.txtDatabase.Text;
                if (this.m_type == 0)
                {
                    this.ChangeValue("ghxbconnection", this.GetOleConnection());
                }
                else
                {
                    this.ChangeValue("sysprivdb", this.GetType() + "||" + this.GetOleConnection());
                }
            }
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            if (this.m_CanDo)
            {
                AppConfig.oledbpassword = this.txtPassword.Text;
                if (this.m_type == 0)
                {
                    this.ChangeValue("ghxbconnection", this.GetOleConnection());
                }
                else
                {
                    this.ChangeValue("sysprivdb", this.GetType() + "||" + this.GetOleConnection());
                }
            }
        }

        private void txtServer_EditValueChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            if (this.m_CanDo)
            {
                AppConfig.oledbserver = this.txtServer.Text;
                if (this.m_type == 0)
                {
                    this.ChangeValue("ghxbconnection", this.GetOleConnection());
                }
                else
                {
                    this.ChangeValue("sysprivdb", this.GetType() + "||" + this.GetOleConnection());
                }
            }
        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {
            this.EnableControl();
            if (this.m_CanDo)
            {
                AppConfig.oledbuser = this.txtUser.Text;
                if (this.m_type == 0)
                {
                    this.ChangeValue("ghxbconnection", this.GetOleConnection());
                }
                else
                {
                    this.ChangeValue("sysprivdb", this.GetType() + "||" + this.GetOleConnection());
                }
            }
        }
    }
}