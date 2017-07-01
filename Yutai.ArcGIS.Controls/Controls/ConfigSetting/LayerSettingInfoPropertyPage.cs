using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal partial class LayerSettingInfoPropertyPage : UserControl
    {
        private XmlNode m_pLayerSettingsNode = null;

        public LayerSettingInfoPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmLayerInfo info = new frmLayerInfo();
            if (info.ShowDialog() == DialogResult.OK)
            {
                string[] items = new string[] {info.LayerName, info.MinScale.ToString(), info.MaxScale.ToString()};
                if (this.LayerNameIsExist(items[0]))
                {
                    MessageBox.Show("图层已存在");
                }
                else
                {
                    ListViewItem item = new ListViewItem(items);
                    this.listView1.Items.Add(item);
                    XmlNode newChild = this.CreateLayerNode(items[0], items[1], items[2]);
                    item.Tag = newChild;
                    this.m_pLayerSettingsNode.ChildNodes[0].AppendChild(newChild);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.SelectedItems.Count - 1; i >= 0; i--)
            {
                ListViewItem item = this.listView1.SelectedItems[i];
                this.listView1.Items.Remove(item);
                this.m_pLayerSettingsNode.ChildNodes[0].RemoveChild(item.Tag as XmlNode);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            frmLayerInfo info = new frmLayerInfo
            {
                LayerName = item.Text,
                MinScale = double.Parse(item.SubItems[1].Text),
                MaxScale = double.Parse(item.SubItems[2].Text)
            };
            if (info.ShowDialog() == DialogResult.OK)
            {
                string[] strArray = new string[3];
                strArray[0] = info.LayerName;
                if ((strArray[0] != item.Text) && this.LayerNameIsExist(strArray[0]))
                {
                    MessageBox.Show("图层已存在");
                }
                else
                {
                    strArray[1] = info.MinScale.ToString();
                    strArray[2] = info.MaxScale.ToString();
                    item.Text = strArray[0];
                    item.SubItems[1].Text = strArray[1];
                    item.SubItems[2].Text = strArray[2];
                    XmlNode tag = item.Tag as XmlNode;
                    for (int i = 0; i < tag.Attributes.Count; i++)
                    {
                        XmlAttribute attribute = tag.Attributes[i];
                        if (attribute.Name.ToLower() == "name")
                        {
                            attribute.Value = strArray[0];
                        }
                        else if (attribute.Name.ToLower() == "minscale")
                        {
                            attribute.Value = strArray[1];
                        }
                        else if (attribute.Name.ToLower() == "maxscale")
                        {
                            attribute.Value = strArray[2];
                        }
                    }
                }
            }
        }

        private XmlNode CreateLayerNode(string name, string minscale, string maxscale)
        {
            XmlNode node = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "layer", "");
            XmlAttribute attribute = AppConfig.m_AppConfig.CreateAttribute("name");
            attribute.Value = name;
            node.Attributes.Append(attribute);
            attribute = AppConfig.m_AppConfig.CreateAttribute("minscale");
            attribute.Value = minscale.ToString();
            node.Attributes.Append(attribute);
            attribute = AppConfig.m_AppConfig.CreateAttribute("maxscale");
            attribute.Value = maxscale.ToString();
            node.Attributes.Append(attribute);
            return node;
        }

        private void CreateLayerSection(XmlNode pNode)
        {
            XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "section", "");
            pNode.AppendChild(newChild);
            XmlAttribute node = AppConfig.m_AppConfig.CreateAttribute("name");
            node.Value = "LayerSettings";
            newChild.Attributes.Append(node);
            node = AppConfig.m_AppConfig.CreateAttribute("type");
            node.Value =
                "JLK.ControlExtend.Configuration.OverviewWindowsLayerSettingsSectionHandler,JLK.ControlExtend.v.4.0,Version=4.0, Culture=neutral, PublicKeyToken=null";
            newChild.Attributes.Append(node);
            node = AppConfig.m_AppConfig.CreateAttribute("requirePermission");
            node.Value = "false";
            newChild.Attributes.Append(node);
            node = AppConfig.m_AppConfig.CreateAttribute("restartOnExternalChanges");
            node.Value = "false";
            newChild.Attributes.Append(node);
        }

        private XmlNode FindNode(string name)
        {
            for (int i = 0; i < this.m_pLayerSettingsNode.ChildNodes[0].ChildNodes.Count; i++)
            {
                XmlNode node = this.m_pLayerSettingsNode.ChildNodes[0].ChildNodes[i];
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    XmlAttribute attribute = node.Attributes[j];
                    if ((attribute.Name.ToLower() == "name") && (attribute.Value.ToLower() == name.ToLower()))
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        public void Init()
        {
            XmlNode node;
            this.listView1.Items.Clear();
            XmlElement documentElement = AppConfig.m_AppConfig.DocumentElement;
            this.m_pLayerSettingsNode = documentElement.SelectSingleNode("LayerSettings");
            if (this.m_pLayerSettingsNode == null)
            {
                node = documentElement.SelectSingleNode("configSections");
                if (node == null)
                {
                    node = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "configSections", "");
                    if (documentElement.FirstChild != null)
                    {
                        documentElement.InsertBefore(node, documentElement.FirstChild);
                    }
                    else
                    {
                        documentElement.AppendChild(node);
                    }
                    this.CreateLayerSection(node);
                }
                else if (!this.IsExistLayerSection(node))
                {
                    this.CreateLayerSection(node);
                }
                this.m_pLayerSettingsNode = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "LayerSettings", "");
            }
            if (this.m_pLayerSettingsNode.ChildNodes.Count == 0)
            {
                node = documentElement.SelectSingleNode("Layers");
                if (node == null)
                {
                    node = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "Layers", "");
                }
                this.m_pLayerSettingsNode.AppendChild(node);
            }
            string[] items = new string[3];
            this.listView1.Items.Clear();
            for (int i = 0; i < this.m_pLayerSettingsNode.ChildNodes[0].ChildNodes.Count; i++)
            {
                node = this.m_pLayerSettingsNode.ChildNodes[0].ChildNodes[i];
                items[1] = "0";
                items[2] = "0";
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    XmlAttribute attribute = node.Attributes[j];
                    if (attribute.Name.ToLower() == "name")
                    {
                        items[0] = attribute.Value;
                    }
                    else if (attribute.Name.ToLower() == "minscale")
                    {
                        items[1] = attribute.Value;
                    }
                    else if (attribute.Name.ToLower() == "maxscale")
                    {
                        items[2] = attribute.Value;
                    }
                }
                ListViewItem item = new ListViewItem(items)
                {
                    Tag = node
                };
                this.listView1.Items.Add(item);
            }
        }

        private bool IsExistLayerSection(XmlNode pParentNode)
        {
            for (int i = 0; i < pParentNode.ChildNodes.Count; i++)
            {
                XmlNode node = pParentNode.ChildNodes[i];
                for (int j = 0; j < node.Attributes.Count; j++)
                {
                    XmlAttribute attribute = node.Attributes[j];
                    if ((attribute.Name.ToLower() == "name") && (attribute.Value.ToLower() == "layersettings"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool LayerNameIsExist(string name)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItem item = this.listView1.Items[i];
                if (item.Text.ToLower() == name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private void LayerSettingInfoPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.listView1.SelectedItems.Count > 0;
            this.btnDelete.Enabled = flag;
            flag = this.listView1.SelectedItems.Count == 1;
            this.btnEdit.Enabled = flag;
        }
    }
}