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
    internal class SymbolsSettingPropertyPage : UserControl
    {
        private SimpleButton btnAddSymbol;
        private SimpleButton btnDelete;
        private Container components = null;
        private Label label1;
        private ListBox listBox1;
        private ListBox listBox2;
        private XmlNode m_appSettingsNode = null;
        private bool m_CanDo = false;
        private string m_StylesPath = "";
        private XmlNode m_SymbolsNode = null;
        private RadioGroup radioGroup1;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            this.btnAddSymbol = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.label1 = new Label();
            this.listBox1 = new ListBox();
            this.radioGroup1 = new RadioGroup();
            this.listBox2 = new ListBox();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.btnAddSymbol.Location = new Point(0x14e, 0x20);
            this.btnAddSymbol.Name = "btnAddSymbol";
            this.btnAddSymbol.Size = new Size(0x30, 0x18);
            this.btnAddSymbol.TabIndex = 2;
            this.btnAddSymbol.Text = "添加...";
            this.btnAddSymbol.Click += new EventHandler(this.btnAddSymbol_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(0x14e, 0x40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x30, 0x18);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "符号库文件列表";
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new Point(8, 0x20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0x138, 160);
            this.listBox1.TabIndex = 6;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.radioGroup1.Location = new Point(8, 0xcb);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "style样式库文件"), new RadioGroupItem(null, "serverstyle样式库文件") });
            this.radioGroup1.Size = new Size(0x138, 0x18);
            this.radioGroup1.TabIndex = 7;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new Point(8, 0x20);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new Size(0x138, 160);
            this.listBox2.TabIndex = 8;
            this.listBox2.Visible = false;
            this.listBox2.SelectedIndexChanged += new EventHandler(this.listBox2_SelectedIndexChanged);
            base.Controls.Add(this.listBox2);
            base.Controls.Add(this.radioGroup1);
            base.Controls.Add(this.listBox1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAddSymbol);
            base.Name = "SymbolsSettingPropertyPage";
            base.Size = new Size(400, 0x109);
            base.Load += new EventHandler(this.SymbolsSettingPropertyPage_Load);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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

