using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    public partial class MapTemplateParamPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private int int_0 = -1;
        private List<string> m_lstString = new List<string>();
        private List<ITextElement> m_lstTextElements = new List<ITextElement>();
        private List<string> m_lstString2 = new List<string>();
        private ListViewItem.ListViewSubItem listViewSubItem_0 = null;
        private MapTemplate m_MapTemplate = null;
        private MapTemplateApplyHelp m_mapTemplateApplyHelp = null;
        private MapTemplateJoinTableElement m_mapTemplateJoinTableElement = null;

        public MapTemplateParamPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.ApplyJTB(this.txtJTB1.Text, 1);
            this.ApplyJTB(this.txtJTB2.Text, 2);
            this.ApplyJTB(this.txtJTB3.Text, 3);
            this.ApplyJTB(this.txtJTB4.Text, 4);
            this.ApplyJTB(this.txtJTB6.Text, 6);
            this.ApplyJTB(this.txtJTB7.Text, 7);
            this.ApplyJTB(this.txtJTB8.Text, 8);
            this.ApplyJTB(this.txtJTB9.Text, 9);
            for (int i = 0; i < this.listView2.Items.Count; i++)
            {
                ListViewItem item = this.listView2.Items[i];
                if (item.Tag != null)
                {
                    MapTemplateParam tag = item.Tag as MapTemplateParam;
                    tag.Value = item.SubItems[1].Text;
                }
            }
            return true;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ListViewItem item = this.listView2.SelectedItems[0];
            frmInputText text = new frmInputText
            {
                Text = item.Text,
                InputText = item.SubItems[1].Text
            };
            if (text.ShowDialog() == DialogResult.OK)
            {
                item.SubItems[1].Text = text.InputText;
            }
        }

        public void Init()
        {
            this.m_mapTemplateJoinTableElement = null;
            this.listView2.Items.Clear();
            bool flag = false;
            int num = 0;
            while (num < this.m_MapTemplate.MapTemplateElement.Count)
            {
                if (this.m_MapTemplate.MapTemplateElement[num].MapTemplateElementType ==
                    MapTemplateElementType.JoinTableElement)
                {
                    flag = true;
                    this.panelJTB.Tag = this.m_MapTemplate.MapTemplateElement[num];
                    this.m_mapTemplateJoinTableElement =
                        this.m_MapTemplate.MapTemplateElement[num] as MapTemplateJoinTableElement;
                    break;
                }
                num++;
            }
            this.panelJTB.Enabled = flag;
            string[] items = new string[2];
            for (num = 0; num < this.m_MapTemplate.MapTemplateParam.Count; num++)
            {
                MapTemplateParam param = this.m_MapTemplate.MapTemplateParam[num];
                items[0] = param.Name;
                items[1] = "";
                ListViewItem item = new ListViewItem(items)
                {
                    Tag = param
                };
                this.listView2.Items.Add(item);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnModify.Enabled = this.listView2.SelectedItems.Count > 0;
        }

        private void MapTemplateParamPage_Load(object sender, EventArgs e)
        {
        }

        private void method_0(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.listViewSubItem_0.Text = this.textBox_0.Text;
                this.textBox_0.Visible = false;
            }
        }

        private void method_1(Control sender, EventArgs e)
        {
            Control control = sender;
            this.listViewSubItem_0.Text = control.Text;
            control.Visible = false;
        }

        private Control method_2(string string_0)
        {
            foreach (Control control in this.groupBox1.Controls)
            {
                if (control.Name == string_0)
                {
                    return control;
                }
            }
            return null;
        }

        private void ApplyJTB(string pName, int pIndex)
        {
            if (this.m_mapTemplateJoinTableElement != null)
            {
                this.m_mapTemplateJoinTableElement.SetJTBTH(pName, pIndex - 1);
            }
        }

        public MapTemplate CartoTemplateData
        {
            set { this.m_MapTemplate = value; }
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get { return this.m_mapTemplateApplyHelp; }
            set { this.m_mapTemplateApplyHelp = value; }
        }
    }
}