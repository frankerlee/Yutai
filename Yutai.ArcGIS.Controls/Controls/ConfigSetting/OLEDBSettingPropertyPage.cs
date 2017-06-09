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
    internal class OLEDBSettingPropertyPage : UserControl
    {
        private SimpleButton btnSelectMDB;
        private SimpleButton btnTestConnection;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label12;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private XmlNode m_appSettingsNode = null;
        private bool m_CanDo = false;
        private int m_type = 0;
        private string OLETemplete = "Provider=Microsoft.Jet.OLEDB.4.0; Data source= #DATABASE#";
        private string OraceleTemplete = "Provider=MSDAORA.1;Data Source=#SERVER#;User Id=#USER#;Password=#PASSWORD#";
        private RadioGroup radioGroup1;
        private string SQLTemplete = "Integrated Security=False; Data Source=#SERVER#;User Id=#USER#;Password=#PASSWORD#;Initial Catalog= #DATABASE#";
        private TextEdit txtDatabase;
        private TextEdit txtMDB;
        private TextEdit txtPassword;
        private TextEdit txtServer;
        private TextEdit txtUser;

        public OLEDBSettingPropertyPage(int type)
        {
            this.InitializeComponent();
            this.m_type = type;
        }

        private void btnSelectMDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
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
                str = this.SQLTemplete.Replace("#SERVER#", this.txtServer.Text).Replace("#USER#", this.txtUser.Text).Replace("#PASSWORD#", this.txtPassword.Text).Replace("#DATABASE#", this.txtDatabase.Text);
                oleDb = DataProviderType.Sql;
            }
            else if (this.radioGroup1.SelectedIndex == 1)
            {
                str = this.OraceleTemplete.Replace("#SERVER#", this.txtServer.Text).Replace("#USER#", this.txtUser.Text).Replace("#PASSWORD#", this.txtPassword.Text);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
            str = ((str + " Data Source= " + this.txtServer.Text.Trim() + ";") + "User Id= " + this.txtUser.Text.Trim() + ";") + "Password= " + this.txtPassword.Text.Trim();
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
                            string[] strArray = str2.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                            this.SplitString(strArray[1]);
                        }
                    }
                }
            }
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnTestConnection = new SimpleButton();
            this.txtPassword = new TextEdit();
            this.txtUser = new TextEdit();
            this.txtDatabase = new TextEdit();
            this.txtServer = new TextEdit();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label1 = new Label();
            this.label2 = new Label();
            this.radioGroup1 = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.btnSelectMDB = new SimpleButton();
            this.txtMDB = new TextEdit();
            this.label12 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtPassword.Properties.BeginInit();
            this.txtUser.Properties.BeginInit();
            this.txtDatabase.Properties.BeginInit();
            this.txtServer.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtMDB.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnTestConnection);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x147, 0xa2);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性表连接串信息设置";
            this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
            this.btnTestConnection.Location = new Point(0x101, 130);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new Size(0x40, 0x18);
            this.btnTestConnection.TabIndex = 12;
            this.btnTestConnection.Text = "测试连接";
            this.btnTestConnection.Click += new EventHandler(this.btnTestConnection_Click);
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new Point(0x51, 0x67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new Size(240, 0x15);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.EditValueChanged += new EventHandler(this.txtPassword_EditValueChanged);
            this.txtUser.EditValue = "";
            this.txtUser.Location = new Point(0x51, 0x4b);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new Size(240, 0x15);
            this.txtUser.TabIndex = 9;
            this.txtUser.EditValueChanged += new EventHandler(this.txtUser_EditValueChanged);
            this.txtDatabase.EditValue = "";
            this.txtDatabase.Location = new Point(0x51, 0x2f);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new Size(240, 0x15);
            this.txtDatabase.TabIndex = 8;
            this.txtDatabase.EditValueChanged += new EventHandler(this.txtDatabase_EditValueChanged);
            this.txtServer.EditValue = "";
            this.txtServer.Location = new Point(0x51, 0x13);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new Size(240, 0x15);
            this.txtServer.TabIndex = 6;
            this.txtServer.EditValueChanged += new EventHandler(this.txtServer_EditValueChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(15, 0x6a);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "密码:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0x4f);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(14, 0x31);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2f, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据库:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(14, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x2f, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(14, 0x13);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x47, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "数据库类型:";
            this.radioGroup1.Location = new Point(0x58, 9);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 3;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "SQLServer"), new RadioGroupItem(null, "Oracle"), new RadioGroupItem(null, "MDB") });
            this.radioGroup1.Size = new Size(0xff, 0x20);
            this.radioGroup1.TabIndex = 3;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.btnSelectMDB);
            this.groupBox2.Controls.Add(this.txtMDB);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new Point(0x10, 0xe4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x147, 0x48);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "个人数据库配置";
            this.btnSelectMDB.Location = new Point(0x111, 0x2d);
            this.btnSelectMDB.Name = "btnSelectMDB";
            this.btnSelectMDB.Size = new Size(0x30, 0x18);
            this.btnSelectMDB.TabIndex = 13;
            this.btnSelectMDB.Text = "更改";
            this.btnSelectMDB.Click += new EventHandler(this.btnSelectMDB_Click);
            this.txtMDB.EditValue = "";
            this.txtMDB.Location = new Point(0x48, 0x12);
            this.txtMDB.Name = "txtMDB";
            this.txtMDB.Properties.ReadOnly = true;
            this.txtMDB.Size = new Size(0xf9, 0x15);
            this.txtMDB.TabIndex = 6;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x10, 0x15);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x2f, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "数据库:";
            base.Controls.Add(this.label2);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.radioGroup1);
            base.Name = "OLEDBSettingPropertyPage";
            base.Size = new Size(0x178, 0x150);
            base.Load += new EventHandler(this.GDBSettingPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtPassword.Properties.EndInit();
            this.txtUser.Properties.EndInit();
            this.txtDatabase.Properties.EndInit();
            this.txtServer.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.txtMDB.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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
            string[] strArray = Value.Split(new char[] { ';' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '=' });
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

