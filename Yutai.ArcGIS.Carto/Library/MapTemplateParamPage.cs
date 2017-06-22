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
        private List<string> list_0 = new List<string>();
        private List<ITextElement> list_1 = new List<ITextElement>();
        private List<string> list_2 = new List<string>();
        private ListViewItem.ListViewSubItem listViewSubItem_0 = null;
        private MapTemplate mapTemplate_0 = null;
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = null;
        private MapTemplateJoinTableElement mapTemplateJoinTableElement_0 = null;

        public MapTemplateParamPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.method_3(this.txtJTB1.Text, 1);
            this.method_3(this.txtJTB2.Text, 2);
            this.method_3(this.txtJTB3.Text, 3);
            this.method_3(this.txtJTB4.Text, 4);
            this.method_3(this.txtJTB6.Text, 6);
            this.method_3(this.txtJTB7.Text, 7);
            this.method_3(this.txtJTB8.Text, 8);
            this.method_3(this.txtJTB9.Text, 9);
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
            frmInputText text = new frmInputText {
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
            this.mapTemplateJoinTableElement_0 = null;
            this.listView2.Items.Clear();
            bool flag = false;
            int num = 0;
            while (num < this.mapTemplate_0.MapTemplateElement.Count)
            {
                if (this.mapTemplate_0.MapTemplateElement[num].MapTemplateElementType == MapTemplateElementType.JoinTableElement)
                {
                    flag = true;
                    this.panelJTB.Tag = this.mapTemplate_0.MapTemplateElement[num];
                    this.mapTemplateJoinTableElement_0 = this.mapTemplate_0.MapTemplateElement[num] as MapTemplateJoinTableElement;
                    break;
                }
                num++;
            }
            this.panelJTB.Enabled = flag;
            string[] items = new string[2];
            for (num = 0; num < this.mapTemplate_0.MapTemplateParam.Count; num++)
            {
                MapTemplateParam param = this.mapTemplate_0.MapTemplateParam[num];
                items[0] = param.Name;
                items[1] = "";
                ListViewItem item = new ListViewItem(items) {
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

        private void method_3(string string_0, int int_1)
        {
            if (this.mapTemplateJoinTableElement_0 != null)
            {
                this.mapTemplateJoinTableElement_0.SetJTBTH(string_0, int_1 - 1);
            }
        }

        public MapTemplate CartoTemplateData
        {
            set
            {
                this.mapTemplate_0 = value;
            }
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get
            {
                return this.mapTemplateApplyHelp_0;
            }
            set
            {
                this.mapTemplateApplyHelp_0 = value;
            }
        }
    }
}

