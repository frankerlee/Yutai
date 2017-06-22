using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal partial class SymbolsSettingPropertyPage : UserControl
    {
        private XmlNode m_appSettingsNode = null;
        private bool m_CanDo = false;
        private string m_StylesPath = "";
        private XmlNode m_SymbolsNode = null;

        public SymbolsSettingPropertyPage()
        {
            this.InitializeComponent();
        }

        private void btnAddSymbol_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (this.radioGroup1.SelectedIndex == 0)
            {
                dialog.Filter = "样式库文件(*.style)|*.style";
            }
            else
            {
                dialog.Filter = "样式库文件(*.serverstyle)|*.serverstyle";
            }
            if (Directory.Exists(this.m_StylesPath))
            {
                dialog.InitialDirectory = this.m_StylesPath;
            }
            dialog.Multiselect = true;
            dialog.Title = "选择样式库";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < dialog.FileNames.Length; i++)
                {
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        this.listBox1.Items.Add(dialog.FileNames[i]);
                    }
                    else
                    {
                        this.listBox2.Items.Add(dialog.FileNames[i]);
                    }
                    int num2 = this.m_SymbolsNode.Attributes.Count + 1;
                    XmlAttribute node = AppConfig.m_AppConfig.CreateAttribute("setting" + this.GetIndex().ToString());
                    node.Value = dialog.FileNames[i];
                    this.m_SymbolsNode.Attributes.Append(node);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int num;
            int num2;
            if (this.radioGroup1.SelectedIndex == 0)
            {
                for (num = this.listBox1.SelectedIndices.Count - 1; num >= 0; num--)
                {
                    num2 = this.listBox1.SelectedIndices[num];
                    while (num2 < (this.m_SymbolsNode.Attributes.Count - 1))
                    {
                        this.m_SymbolsNode.Attributes[num2].Value = this.m_SymbolsNode.Attributes[num2 + 1].Value;
                        num2++;
                    }
                    this.m_SymbolsNode.Attributes.RemoveAt(this.m_SymbolsNode.Attributes.Count - 1);
                    this.listBox1.Items.RemoveAt(this.listBox1.SelectedIndices[num]);
                }
            }
            else
            {
                for (num = this.listBox2.SelectedIndices.Count - 1; num >= 0; num--)
                {
                    for (num2 = this.listBox2.SelectedIndices[num]; num2 < (this.m_SymbolsNode.Attributes.Count - 1); num2++)
                    {
                        this.m_SymbolsNode.Attributes[num2].Value = this.m_SymbolsNode.Attributes[num2 + 1].Value;
                    }
                    this.m_SymbolsNode.Attributes.RemoveAt(this.m_SymbolsNode.Attributes.Count - 1);
                    this.listBox2.Items.RemoveAt(this.listBox2.SelectedIndices[num]);
                }
            }
        }

 private int GetIndex()
        {
            int num = 1;
            if (this.m_SymbolsNode.Attributes.Count == 0)
            {
                return 1;
            }
            for (int i = 0; i < this.m_SymbolsNode.Attributes.Count; i++)
            {
                int num3 = int.Parse(this.m_SymbolsNode.Attributes[i].Name.Trim().Substring(7));
                if (num <= num3)
                {
                    num = num3 + 1;
                }
            }
            return num;
        }

        public void Init()
        {
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
                int num;
                this.listBox1.Items.Clear();
                this.listBox2.Items.Clear();
                this.btnDelete.Enabled = false;
                for (num = 0; num < this.m_appSettingsNode.ChildNodes.Count; num++)
                {
                    XmlNode node2 = this.m_appSettingsNode.ChildNodes[num];
                    if (node2.NodeType != XmlNodeType.Comment)
                    {
                        XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[num].Attributes;
                        string str = attributes["key"].Value.ToLower();
                        string str2 = attributes["value"].Value;
                        if (str == "stylefiletype")
                        {
                            if (str2 == "0")
                            {
                                this.radioGroup1.SelectedIndex = 0;
                                this.listBox1.Visible = true;
                            }
                            else
                            {
                                this.radioGroup1.SelectedIndex = 1;
                                this.listBox2.Visible = true;
                            }
                            break;
                        }
                    }
                }
                this.m_StylesPath = Application.StartupPath + @"\styles";
                this.m_SymbolsNode = documentElement.SelectSingleNode("Symbols");
                if (this.m_SymbolsNode == null)
                {
                    this.m_SymbolsNode = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "Symbols", "");
                    documentElement.AppendChild(this.m_SymbolsNode);
                }
                this.listBox1.Items.Clear();
                this.listBox2.Items.Clear();
                for (num = 0; num < this.m_SymbolsNode.Attributes.Count; num++)
                {
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        this.listBox1.Items.Add(this.m_SymbolsNode.Attributes[num].Value);
                    }
                    else
                    {
                        this.listBox2.Items.Add(this.m_SymbolsNode.Attributes[num].Value);
                    }
                }
            }
            this.m_CanDo = true;
        }

 private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                if (this.listBox1.SelectedItems.Count > 0)
                {
                    this.btnDelete.Enabled = true;
                }
                else
                {
                    this.btnDelete.Enabled = false;
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 1)
            {
                if (this.listBox2.SelectedItems.Count > 0)
                {
                    this.btnDelete.Enabled = true;
                }
                else
                {
                    this.btnDelete.Enabled = false;
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                this.listBox1.Visible = this.radioGroup1.SelectedIndex == 0;
                this.listBox2.Visible = this.radioGroup1.SelectedIndex == 1;
                if (this.radioGroup1.SelectedIndex == 0)
                {
                    this.btnDelete.Enabled = this.listBox1.SelectedItems.Count > 0;
                }
                else
                {
                    this.btnDelete.Enabled = this.listBox2.SelectedItems.Count > 0;
                }
                if (this.m_SymbolsNode != null)
                {
                    int num;
                    ListBox box;
                    XmlAttribute attribute;
                    this.m_SymbolsNode.Attributes.RemoveAll();
                    bool flag = false;
                    for (num = 0; num < this.m_appSettingsNode.ChildNodes.Count; num++)
                    {
                        XmlNode node = this.m_appSettingsNode.ChildNodes[num];
                        if (node.NodeType != XmlNodeType.Comment)
                        {
                            XmlAttributeCollection attributes = this.m_appSettingsNode.ChildNodes[num].Attributes;
                            if (attributes["key"].Value.ToLower() == "stylefiletype")
                            {
                                flag = true;
                                attributes["value"].Value = this.radioGroup1.SelectedIndex.ToString();
                                break;
                            }
                        }
                    }
                    if (this.radioGroup1.SelectedIndex == 0)
                    {
                        box = this.listBox1;
                    }
                    else
                    {
                        box = this.listBox2;
                    }
                    for (num = 0; num < box.Items.Count; num++)
                    {
                        attribute = AppConfig.m_AppConfig.CreateAttribute("setting" + num.ToString());
                        attribute.Value = box.Items[num].ToString();
                        this.m_SymbolsNode.Attributes.Append(attribute);
                    }
                    if (!flag)
                    {
                        XmlNode newChild = AppConfig.m_AppConfig.CreateNode(XmlNodeType.Element, "add", "");
                        attribute = AppConfig.m_AppConfig.CreateAttribute("key");
                        attribute.Value = "StyleFileType";
                        newChild.Attributes.Append(attribute);
                        attribute = AppConfig.m_AppConfig.CreateAttribute("value");
                        attribute.Value = this.radioGroup1.SelectedIndex.ToString();
                        newChild.Attributes.Append(attribute);
                        this.m_appSettingsNode.AppendChild(newChild);
                    }
                }
            }
        }

        private void SymbolsSettingPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}

