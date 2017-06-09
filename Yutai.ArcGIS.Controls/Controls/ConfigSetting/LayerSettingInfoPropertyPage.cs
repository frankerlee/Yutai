using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    internal class LayerSettingInfoPropertyPage : UserControl
    {
        private SimpleButton btnAdd;
        private SimpleButton btnDelete;
        private SimpleButton btnEdit;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private IContainer components = null;
        private Label label1;
        private ListView listView1;
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
                string[] items = new string[] { info.LayerName, info.MinScale.ToString(), info.MaxScale.ToString() };
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
            frmLayerInfo info = new frmLayerInfo {
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
            node.Value = "JLK.ControlExtend.Configuration.OverviewWindowsLayerSettingsSectionHandler,JLK.ControlExtend.v.4.0,Version=4.0, Culture=neutral, PublicKeyToken=null";
            newChild.Attributes.Append(node);
            node = AppConfig.m_AppConfig.CreateAttribute("requirePermission");
            node.Value = "false";
            newChild.Attributes.Append(node);
            node = AppConfig.m_AppConfig.CreateAttribute("restartOnExternalChanges");
            node.Value = "false";
            newChild.Attributes.Append(node);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
                ListViewItem item = new ListViewItem(items) {
                    Tag = node
                };
                this.listView1.Items.Add(item);
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.btnAdd = new SimpleButton();
            this.btnDelete = new SimpleButton();
            this.btnEdit = new SimpleButton();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层列表";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
            this.listView1.Location = new Point(3, 0x19);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(410, 160);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader1.Text = "图层名";
            this.columnHeader1.Width = 0x87;
            this.columnHeader2.Text = "最小比例";
            this.columnHeader2.Width = 0x80;
            this.columnHeader3.Text = "最大比例";
            this.columnHeader3.Width = 0x87;
            this.btnAdd.Location = new Point(0xdb, 0xbf);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(0x30, 0x18);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(0x11f, 0xbf);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(0x30, 0x18);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new Point(0x160, 0xbf);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new Size(0x30, 0x18);
            this.btnEdit.TabIndex = 0x10;
            this.btnEdit.Text = "编辑";
            this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnEdit);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAdd);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Name = "LayerSettingInfoPropertyPage";
            base.Size = new Size(0x1be, 0x115);
            base.Load += new EventHandler(this.LayerSettingInfoPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
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

