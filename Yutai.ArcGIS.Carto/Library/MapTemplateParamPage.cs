using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.MapCartoTemplateLib;

namespace Yutai.ArcGIS.Carto.Library
{
    public class MapTemplateParamPage : UserControl
    {
        private Button btnModify;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private int int_0 = -1;
        private List<string> list_0 = new List<string>();
        private List<ITextElement> list_1 = new List<ITextElement>();
        private List<string> list_2 = new List<string>();
        private ListView listView2;
        private ListViewItem.ListViewSubItem listViewSubItem_0 = null;
        private MapTemplate mapTemplate_0 = null;
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = null;
        private MapTemplateJoinTableElement mapTemplateJoinTableElement_0 = null;
        private Panel panelJTB;
        private Panel panelParam;
        private TextBox textBox_0;
        private TextBox txtJTB1;
        private TextBox txtJTB2;
        private TextBox txtJTB3;
        private TextBox txtJTB4;
        private TextBox txtJTB6;
        private TextBox txtJTB7;
        private TextBox txtJTB8;
        private TextBox txtJTB9;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            this.panelParam = new Panel();
            this.btnModify = new Button();
            this.listView2 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.panelJTB = new Panel();
            this.groupBox1 = new GroupBox();
            this.txtJTB8 = new TextBox();
            this.txtJTB7 = new TextBox();
            this.txtJTB6 = new TextBox();
            this.txtJTB9 = new TextBox();
            this.txtJTB4 = new TextBox();
            this.txtJTB3 = new TextBox();
            this.txtJTB2 = new TextBox();
            this.txtJTB1 = new TextBox();
            this.panelParam.SuspendLayout();
            this.panelJTB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panelParam.Controls.Add(this.btnModify);
            this.panelParam.Controls.Add(this.listView2);
            this.panelParam.Location = new Point(0x11, 0x69);
            this.panelParam.Name = "panelParam";
            this.panelParam.Size = new Size(0x1ba, 0xa8);
            this.panelParam.TabIndex = 14;
            this.btnModify.Enabled = false;
            this.btnModify.Location = new Point(0x169, 14);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new Size(60, 0x17);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "修改值";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new EventHandler(this.btnModify_Click);
            this.listView2.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView2.FullRowSelect = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new Point(12, 14);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(0x157, 0x89);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = View.Details;
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.columnHeader_0.Text = "参数名";
            this.columnHeader_0.Width = 110;
            this.columnHeader_1.Text = "值";
            this.columnHeader_1.Width = 0x60;
            this.panelJTB.Controls.Add(this.groupBox1);
            this.panelJTB.Location = new Point(0x11, 6);
            this.panelJTB.Name = "panelJTB";
            this.panelJTB.Size = new Size(0x163, 0x5f);
            this.panelJTB.TabIndex = 13;
            this.groupBox1.Controls.Add(this.txtJTB8);
            this.groupBox1.Controls.Add(this.txtJTB7);
            this.groupBox1.Controls.Add(this.txtJTB6);
            this.groupBox1.Controls.Add(this.txtJTB9);
            this.groupBox1.Controls.Add(this.txtJTB4);
            this.groupBox1.Controls.Add(this.txtJTB3);
            this.groupBox1.Controls.Add(this.txtJTB2);
            this.groupBox1.Controls.Add(this.txtJTB1);
            this.groupBox1.Location = new Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(340, 0x56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "接图表";
            this.txtJTB8.Location = new Point(0x71, 60);
            this.txtJTB8.Name = "txtJTB8";
            this.txtJTB8.Size = new Size(0x65, 0x15);
            this.txtJTB8.TabIndex = 8;
            this.txtJTB7.Location = new Point(7, 60);
            this.txtJTB7.Name = "txtJTB7";
            this.txtJTB7.Size = new Size(0x65, 0x15);
            this.txtJTB7.TabIndex = 7;
            this.txtJTB6.Location = new Point(0xdd, 0x25);
            this.txtJTB6.Name = "txtJTB6";
            this.txtJTB6.Size = new Size(0x65, 0x15);
            this.txtJTB6.TabIndex = 6;
            this.txtJTB9.Location = new Point(0xdd, 60);
            this.txtJTB9.Name = "txtJTB9";
            this.txtJTB9.Size = new Size(0x65, 0x15);
            this.txtJTB9.TabIndex = 9;
            this.txtJTB4.Location = new Point(7, 0x25);
            this.txtJTB4.Name = "txtJTB4";
            this.txtJTB4.Size = new Size(0x65, 0x15);
            this.txtJTB4.TabIndex = 3;
            this.txtJTB3.Location = new Point(0xdd, 15);
            this.txtJTB3.Name = "txtJTB3";
            this.txtJTB3.Size = new Size(0x65, 0x15);
            this.txtJTB3.TabIndex = 2;
            this.txtJTB2.Location = new Point(0x72, 15);
            this.txtJTB2.Name = "txtJTB2";
            this.txtJTB2.Size = new Size(0x65, 0x15);
            this.txtJTB2.TabIndex = 1;
            this.txtJTB1.Location = new Point(7, 15);
            this.txtJTB1.Name = "txtJTB1";
            this.txtJTB1.Size = new Size(0x65, 0x15);
            this.txtJTB1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panelParam);
            base.Controls.Add(this.panelJTB);
            base.Name = "MapTemplateParamPage";
            base.Size = new Size(0x214, 0x11f);
            base.Load += new EventHandler(this.MapTemplateParamPage_Load);
            this.panelParam.ResumeLayout(false);
            this.panelJTB.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
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

        private void method_1(object sender, EventArgs e)
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

