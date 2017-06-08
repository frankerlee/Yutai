using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal class OtherSettingPropertyPage : UserControl
    {
        private SimpleButton btnSelectTable;
        private CheckEdit checkEditInit;
        private CheckEdit checkEditLogin;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private string layerconfig = "";
        private XmlNode m_appSettingsNode = null;
        private bool m_CanDo = false;
        private string MenuConfig = "";
        private SimpleButton simpleButton1;
        private TextEdit textEdit1;
        private TextEdit txtLayerName;

        public OtherSettingPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnSelectTable_Click(object sender, EventArgs e)
        {
            IWorkspaceFactory factory;
            Exception exception;
            if (AppConfig.m_pWorkspace != null)
            {
                goto Label_01EE;
            }
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            if ((AppConfig.dbclient.ToLower() == "sqlserver") || (AppConfig.dbclient.ToLower() == "oracle"))
            {
                factory = new SdeWorkspaceFactoryClass();
                try
                {
                    AppConfig.m_pWorkspace = factory.Open(this.GetConnectionProperty(), 0);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    if (exception is COMException)
                    {
                        switch (((uint) (exception as COMException).ErrorCode))
                        {
                            case 0x80041569:
                            case 0x80041501:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                goto Label_01E2;

                            case 0x8004156a:
                                MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                                break;

                            case 0x80004005:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                break;
                        }
                    }
                }
            }
            else
            {
                try
                {
                    factory = new AccessWorkspaceFactoryClass();
                    AppConfig.m_pWorkspace = factory.OpenFromFile(AppConfig.layerconfigdb, 0);
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    if (exception is COMException)
                    {
                        switch (((uint) (exception as COMException).ErrorCode))
                        {
                            case 0x80041569:
                            case 0x80041501:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                break;

                            case 0x8004156a:
                                MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                                break;

                            case 0x80004005:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                break;
                        }
                    }
                }
            }
        Label_01E2:
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        Label_01EE:
            if (AppConfig.m_pWorkspace != null)
            {
                try
                {
                    frmLayerConfigTable table = new frmLayerConfigTable {
                        Workspace = AppConfig.m_pWorkspace,
                        LayerConfig = this.layerconfig
                    };
                    if ((table.ShowDialog() == DialogResult.OK) && (table.LayerConfig != ""))
                    {
                        this.layerconfig = table.LayerConfig;
                        this.txtLayerName.Text = table.LayerConfig;
                    }
                }
                catch (Exception exception3)
                {
                    MessageBox.Show(exception3.ToString());
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkEditInit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                bool flag = false;
                for (int i = 0; i < this.m_appSettingsNode.ChildNodes.Count; i++)
                {
                    XmlNode node = this.m_appSettingsNode.ChildNodes[i];
                    if (node.NodeType != XmlNodeType.Comment)
                    {
                        XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[i].Attributes;
                        if (attributes["key"].Value.ToLower() == "initflag")
                        {
                            flag = true;
                            if (this.checkEditInit.Checked)
                            {
                                attributes["value"].Value = "Init";
                            }
                            else
                            {
                                attributes["value"].Value = "0";
                            }
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "add", "");
                    XmlAttribute attribute = AppConfig.m_AppConfig.CreateAttribute("key");
                    attribute.Value = "InitFlag";
                    newChild.Attributes.Append(attribute);
                    attribute = AppConfig.m_AppConfig.CreateAttribute("value");
                    if (this.checkEditInit.Checked)
                    {
                        attribute.Value = "Init";
                    }
                    else
                    {
                        attribute.Value = "0";
                    }
                    newChild.Attributes.Append(attribute);
                    this.m_appSettingsNode.AppendChild(newChild);
                }
            }
        }

        private void checkEditLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                bool flag = false;
                for (int i = 0; i < this.m_appSettingsNode.ChildNodes.Count; i++)
                {
                    XmlNode node = this.m_appSettingsNode.ChildNodes[i];
                    if (node.NodeType != XmlNodeType.Comment)
                    {
                        XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[i].Attributes;
                        if (attributes["key"].Value.ToLower() == "islogin")
                        {
                            flag = true;
                            if (this.checkEditLogin.Checked)
                            {
                                attributes["value"].Value = "1";
                            }
                            else
                            {
                                attributes["value"].Value = "0";
                            }
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "add", "");
                    XmlAttribute attribute = AppConfig.m_AppConfig.CreateAttribute("key");
                    attribute.Value = "IsLogin";
                    newChild.Attributes.Append(attribute);
                    attribute = AppConfig.m_AppConfig.CreateAttribute("value");
                    if (this.checkEditLogin.Checked)
                    {
                        attribute.Value = "1";
                    }
                    else
                    {
                        attribute.Value = "0";
                    }
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

        private IPropertySet GetConnectionProperty()
        {
            IPropertySet set = new PropertySetClass();
            set.SetProperty("SERVER", AppConfig.server);
            set.SetProperty("INSTANCE", AppConfig.instance);
            set.SetProperty("DATABASE", AppConfig.database);
            set.SetProperty("USER", AppConfig.user);
            set.SetProperty("PASSWORD", AppConfig.password);
            set.SetProperty("VERSION", AppConfig.version);
            return set;
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
                this.txtLayerName.Text = "";
                for (int i = 0; i < this.m_appSettingsNode.ChildNodes.Count; i++)
                {
                    XmlNode node2 = this.m_appSettingsNode.ChildNodes[i];
                    if (node2.NodeType != XmlNodeType.Comment)
                    {
                        XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[i].Attributes;
                        string str = attributes["key"].Value.ToLower();
                        string str2 = attributes["value"].Value;
                        switch (str)
                        {
                            case "layerconfig":
                                this.layerconfig = str2;
                                this.txtLayerName.Text = str2;
                                break;

                            case "menuconfig":
                                this.MenuConfig = str2;
                                this.textEdit1.Text = str2;
                                break;

                            case "initflag":
                                if (str2.Trim().ToLower() == "init")
                                {
                                    this.checkEditInit.Checked = true;
                                }
                                else
                                {
                                    this.checkEditInit.Checked = false;
                                }
                                break;

                            case "islogin":
                                if (str2.Trim() == "1")
                                {
                                    this.checkEditLogin.Checked = true;
                                }
                                else
                                {
                                    this.checkEditLogin.Checked = false;
                                }
                                break;
                        }
                    }
                }
            }
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.btnSelectTable = new SimpleButton();
            this.txtLayerName = new TextEdit();
            this.label1 = new Label();
            this.checkEditLogin = new CheckEdit();
            this.checkEditInit = new CheckEdit();
            this.groupBox2 = new GroupBox();
            this.simpleButton1 = new SimpleButton();
            this.textEdit1 = new TextEdit();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.txtLayerName.Properties.BeginInit();
            this.checkEditLogin.Properties.BeginInit();
            this.checkEditInit.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.textEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnSelectTable);
            this.groupBox1.Controls.Add(this.txtLayerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe8, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图层配置表信息设置";
            this.btnSelectTable.Location = new Point(120, 0x30);
            this.btnSelectTable.Name = "btnSelectTable";
            this.btnSelectTable.Size = new Size(0x48, 0x18);
            this.btnSelectTable.TabIndex = 3;
            this.btnSelectTable.Text = "选择表...";
            this.btnSelectTable.Click += new EventHandler(this.btnSelectTable_Click);
            this.txtLayerName.EditValue = "";
            this.txtLayerName.Location = new Point(0x30, 0x10);
            this.txtLayerName.Name = "txtLayerName";
            this.txtLayerName.Size = new Size(0x90, 0x15);
            this.txtLayerName.TabIndex = 2;
            this.txtLayerName.EditValueChanged += new EventHandler(this.txtLayerName_EditValueChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "表名";
            this.checkEditLogin.Location = new Point(14, 0x72);
            this.checkEditLogin.Name = "checkEditLogin";
            this.checkEditLogin.Properties.Caption = "启用登录窗体";
            this.checkEditLogin.Size = new Size(120, 0x13);
            this.checkEditLogin.TabIndex = 1;
            this.checkEditLogin.CheckedChanged += new EventHandler(this.checkEditLogin_CheckedChanged);
            this.checkEditInit.Location = new Point(14, 320);
            this.checkEditInit.Name = "checkEditInit";
            this.checkEditInit.Properties.Caption = "初始化菜单文件";
            this.checkEditInit.Size = new Size(0x80, 0x13);
            this.checkEditInit.TabIndex = 2;
            this.checkEditInit.Visible = false;
            this.checkEditInit.CheckedChanged += new EventHandler(this.checkEditInit_CheckedChanged);
            this.groupBox2.Controls.Add(this.simpleButton1);
            this.groupBox2.Controls.Add(this.textEdit1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new Point(0x10, 0x134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xe8, 0x4a);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单配置表设置";
            this.groupBox2.Visible = false;
            this.simpleButton1.Location = new Point(120, 0x30);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x48, 0x18);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "选择表...";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click_1);
            this.textEdit1.EditValue = "";
            this.textEdit1.Location = new Point(0x30, 0x10);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new Size(0x90, 0x15);
            this.textEdit1.TabIndex = 2;
            this.textEdit1.EditValueChanged += new EventHandler(this.textEdit1_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x18);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "表名";
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.checkEditInit);
            base.Controls.Add(this.checkEditLogin);
            base.Controls.Add(this.groupBox1);
            base.Name = "OtherSettingPropertyPage";
            base.Size = new Size(0x108, 0x128);
            base.Load += new EventHandler(this.OtherSettingPropertyPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.txtLayerName.Properties.EndInit();
            this.checkEditLogin.Properties.EndInit();
            this.checkEditInit.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.textEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void OtherSettingPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            IWorkspaceFactory factory;
            Exception exception;
            if (AppConfig.m_pWorkspace != null)
            {
                goto Label_01EE;
            }
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            if ((AppConfig.dbclient.ToLower() == "sqlserver") || (AppConfig.dbclient.ToLower() == "oracle"))
            {
                factory = new SdeWorkspaceFactoryClass();
                try
                {
                    AppConfig.m_pWorkspace = factory.Open(this.GetConnectionProperty(), 0);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    if (exception is COMException)
                    {
                        switch (((uint) (exception as COMException).ErrorCode))
                        {
                            case 0x80041569:
                            case 0x80041501:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                goto Label_01E2;

                            case 0x8004156a:
                                MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                                break;

                            case 0x80004005:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                break;
                        }
                    }
                }
            }
            else
            {
                try
                {
                    factory = new AccessWorkspaceFactoryClass();
                    AppConfig.m_pWorkspace = factory.OpenFromFile(AppConfig.layerconfigdb, 0);
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    if (exception is COMException)
                    {
                        switch (((uint) (exception as COMException).ErrorCode))
                        {
                            case 0x80041569:
                            case 0x80041501:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                break;

                            case 0x8004156a:
                                MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                                break;

                            case 0x80004005:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                break;
                        }
                    }
                }
            }
        Label_01E2:
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        Label_01EE:
            if (AppConfig.m_pWorkspace != null)
            {
                try
                {
                    frmLayerConfigTable table = new frmLayerConfigTable {
                        Workspace = AppConfig.m_pWorkspace,
                        LayerConfig = this.MenuConfig
                    };
                    if ((table.ShowDialog() == DialogResult.OK) && (table.LayerConfig != ""))
                    {
                        this.MenuConfig = table.LayerConfig;
                        this.textEdit1.Text = table.LayerConfig;
                    }
                }
                catch (Exception exception3)
                {
                    MessageBox.Show(exception3.ToString());
                }
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                bool flag = false;
                if (this.m_appSettingsNode != null)
                {
                    for (int i = 0; i < this.m_appSettingsNode.ChildNodes.Count; i++)
                    {
                        XmlNode node = this.m_appSettingsNode.ChildNodes[i];
                        if (node.NodeType != XmlNodeType.Comment)
                        {
                            XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[i].Attributes;
                            if (attributes["key"].Value.ToLower() == "menuconfig")
                            {
                                flag = true;
                                attributes["value"].Value = this.textEdit1.Text;
                                break;
                            }
                        }
                    }
                    if (!flag)
                    {
                        XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "add", "");
                        XmlAttribute attribute = AppConfig.m_AppConfig.CreateAttribute("key");
                        attribute.Value = "MenuConfig";
                        newChild.Attributes.Append(attribute);
                        attribute = AppConfig.m_AppConfig.CreateAttribute("value");
                        attribute.Value = this.textEdit1.Text;
                        newChild.Attributes.Append(attribute);
                        this.m_appSettingsNode.AppendChild(newChild);
                    }
                }
            }
        }

        private void txtLayerName_EditValueChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                bool flag = false;
                if (this.m_appSettingsNode != null)
                {
                    for (int i = 0; i < this.m_appSettingsNode.ChildNodes.Count; i++)
                    {
                        XmlNode node = this.m_appSettingsNode.ChildNodes[i];
                        if (node.NodeType != XmlNodeType.Comment)
                        {
                            XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[i].Attributes;
                            if (attributes["key"].Value.ToLower() == "layerconfig")
                            {
                                flag = true;
                                attributes["value"].Value = this.txtLayerName.Text;
                                break;
                            }
                        }
                    }
                    if (!flag)
                    {
                        XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "add", "");
                        XmlAttribute attribute = AppConfig.m_AppConfig.CreateAttribute("key");
                        attribute.Value = "LayerConfig";
                        newChild.Attributes.Append(attribute);
                        attribute = AppConfig.m_AppConfig.CreateAttribute("value");
                        attribute.Value = this.txtLayerName.Text;
                        newChild.Attributes.Append(attribute);
                        this.m_appSettingsNode.AppendChild(newChild);
                    }
                }
            }
        }
    }
}

