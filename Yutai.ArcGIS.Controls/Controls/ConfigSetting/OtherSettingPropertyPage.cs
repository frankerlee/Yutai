using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal partial class OtherSettingPropertyPage : UserControl
    {
        private string layerconfig = "";
        private XmlNode m_appSettingsNode = null;
        private bool m_CanDo = false;
        private string MenuConfig = "";

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
                            case 2147751273:
                            case 2147751169:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                goto Label_01E2;

                            case 2147751274:
                                MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                                break;

                            case 2147500037:
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
                            case 2147751273:
                            case 2147751169:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                break;

                            case 2147751274:
                                MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                                break;

                            case 2147500037:
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
                    frmLayerConfigTable table = new frmLayerConfigTable
                    {
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
                            case 2147751273:
                            case 2147751169:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                goto Label_01E2;

                            case 2147751274:
                                MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                                break;

                            case 2147500037:
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
                            case 2147751273:
                            case 2147751169:
                                MessageBox.Show("无法连接到空间数据库，请检查配置参数是否正确!", "测试连接");
                                break;

                            case 2147751274:
                                MessageBox.Show("该服务器上的SDE没有启动，请启动服务器上的SDE后在测试!", "测试连接");
                                break;

                            case 2147500037:
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
                    frmLayerConfigTable table = new frmLayerConfigTable
                    {
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