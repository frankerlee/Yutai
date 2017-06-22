using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGN_Weights : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private Container container_0 = null;
        private ListViewItem listViewItem_0 = null;

        public BulidGN_Weights()
        {
            this.InitializeComponent();
            this.textBox_0 = new TextBox();
            this.textBox_0.Size = new Size(0, 0);
            this.textBox_0.Location = new Point(0, 0);
            this.textBox_0.BorderStyle = BorderStyle.FixedSingle;
            this.textBox_0.Font = this.listView1.Font;
            this.listView1.Controls.Add(this.textBox_0);
            this.textBox_0.AutoSize = false;
            this.textBox_0.KeyPress += new KeyPressEventHandler(this.textBox_0_KeyPress);
            this.textBox_0.LostFocus += new EventHandler(this.textBox_0_LostFocus);
            this.comboBox_0 = new System.Windows.Forms.ComboBox();
            this.comboBox_0.Size = new Size(0, 0);
            this.comboBox_0.Location = new Point(0, 0);
            this.listView1.Controls.Add(this.comboBox_0);
            this.comboBox_0.SelectedIndexChanged += new EventHandler(this.comboBox_0_SelectedIndexChanged);
            this.comboBox_0.LostFocus += new EventHandler(this.comboBox_0_LostFocus);
            this.comboBox_0.KeyPress += new KeyPressEventHandler(this.comboBox_0_KeyPress);
            this.comboBox_0.Font = this.Font;
            this.comboBox_0.BackColor = Color.LightBlue;
            this.comboBox_0.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox_0.Items.Add("Integer");
            this.comboBox_0.Items.Add("Single");
            this.comboBox_0.Items.Add("Double");
            this.comboBox_0.Items.Add("Bitgate");
            this.comboBox_0.Hide();
            this.comboBox_1 = new System.Windows.Forms.ComboBox();
            this.comboBox_1.Size = new Size(0, 0);
            this.comboBox_1.Location = new Point(0, 0);
            this.listView1.Controls.Add(this.comboBox_1);
            this.comboBox_1.SelectedIndexChanged += new EventHandler(this.comboBox_1_SelectedIndexChanged);
            this.comboBox_1.LostFocus += new EventHandler(this.comboBox_1_LostFocus);
            this.comboBox_1.KeyPress += new KeyPressEventHandler(this.comboBox_1_KeyPress);
            this.comboBox_1.Font = this.Font;
            this.comboBox_1.BackColor = Color.LightBlue;
            this.comboBox_1.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 1; i < 32; i++)
            {
                this.comboBox_1.Items.Add(i.ToString());
            }
            this.comboBox_1.Hide();
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            string[] items = new string[2];
            items[0] = "";
            ListViewItem item = new ListViewItem(items);
            this.listView1.Items.Add(item);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                BulidGeometryNetworkHelper.Weight tag = this.listView1.SelectedItems[0].Tag as BulidGeometryNetworkHelper.Weight;
                if (tag != null)
                {
                    BulidGeometryNetworkHelper.BulidGNHelper.Weights.Remove(tag);
                }
                this.listView1.Items.RemoveAt(this.listView1.SelectedIndices[0]);
            }
        }

        private void BulidGN_Weights_Load(object sender, EventArgs e)
        {
        }

        private void comboBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == '\x001b'))
            {
                this.comboBox_0.Hide();
            }
        }

        private void comboBox_0_LostFocus(object sender, EventArgs e)
        {
            this.comboBox_0.Hide();
        }

        private void comboBox_0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bool_1)
            {
                int selectedIndex = this.comboBox_0.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    string text = this.comboBox_0.Text;
                    int num1 = this.comboBox_0.SelectedIndex;
                    this.listViewItem_0.SubItems[1].Text = text;
                    BulidGeometryNetworkHelper.Weight tag = this.listViewItem_0.Tag as BulidGeometryNetworkHelper.Weight;
                    switch (this.comboBox_0.SelectedIndex)
                    {
                        case 0:
                            tag.weightType = esriWeightType.esriWTInteger;
                            break;

                        case 1:
                            tag.weightType = esriWeightType.esriWTSingle;
                            break;

                        case 2:
                            tag.weightType = esriWeightType.esriWTDouble;
                            break;

                        case 3:
                            tag.weightType = esriWeightType.esriWTBitGate;
                            break;
                    }
                    if (selectedIndex == 3)
                    {
                        this.listViewItem_0.SubItems.Add("1");
                        if (tag != null)
                        {
                            tag.bitGateSize = 1;
                        }
                    }
                    else if (this.listViewItem_0.SubItems.Count == 3)
                    {
                        this.listViewItem_0.SubItems.RemoveAt(2);
                    }
                }
                this.comboBox_0.Hide();
            }
        }

        private void comboBox_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '\r') || (e.KeyChar == '\x001b'))
            {
                this.comboBox_1.Hide();
            }
        }

        private void comboBox_1_LostFocus(object sender, EventArgs e)
        {
            this.comboBox_1.Hide();
        }

        private void comboBox_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bool_1)
            {
                int selectedIndex = this.comboBox_1.SelectedIndex;
                if (selectedIndex >= 0)
                {
                    string text = this.comboBox_1.Text;
                    this.listViewItem_0.SubItems[2].Text = text;
                    BulidGeometryNetworkHelper.Weight tag = this.listViewItem_0.Tag as BulidGeometryNetworkHelper.Weight;
                    if (tag != null)
                    {
                        tag.bitGateSize = selectedIndex + 1;
                    }
                }
                this.comboBox_1.Hide();
            }
        }

 private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                Rectangle itemRect = this.listView1.GetItemRect(this.listView1.SelectedIndices[0]);
                int num = this.int_0;
                int left = itemRect.Left;
                int num3 = itemRect.Left;
                int num4 = 0;
                while (num4 < this.listView1.Columns.Count)
                {
                    left = num3;
                    num3 += this.listView1.Columns[num4].Width;
                    if ((num > left) && (num < num3))
                    {
                        break;
                    }
                    num4++;
                }
                num3 = (num3 > this.listView1.ClientSize.Width) ? this.listView1.ClientSize.Width : num3;
                this.listViewItem_0 = this.listView1.SelectedItems[0];
                if (num4 == 0)
                {
                    this.bool_0 = false;
                    this.textBox_0.Size = new Size(num3 - left, this.listViewItem_0.Bounds.Height);
                    this.textBox_0.Location = new Point(left, this.listViewItem_0.Bounds.Y);
                    this.textBox_0.Show();
                    this.textBox_0.Text = this.listViewItem_0.SubItems[0].Text;
                    this.textBox_0.SelectAll();
                    this.textBox_0.Focus();
                }
                else if (num4 == 1)
                {
                    this.bool_0 = false;
                    this.bool_1 = true;
                    this.comboBox_0.Size = new Size(num3 - left, this.listViewItem_0.Bounds.Height);
                    this.comboBox_0.Location = new Point(left, this.listViewItem_0.Bounds.Y);
                    this.comboBox_0.Show();
                    this.comboBox_0.Text = this.listViewItem_0.SubItems[1].Text;
                    this.comboBox_0.Focus();
                    this.bool_1 = false;
                }
                else
                {
                    this.bool_1 = true;
                    this.comboBox_1.Size = new Size(num3 - left, this.listViewItem_0.Bounds.Height);
                    this.comboBox_1.Location = new Point(left, this.listViewItem_0.Bounds.Y);
                    this.comboBox_1.Show();
                    this.comboBox_1.Text = this.listViewItem_0.SubItems[1].Text;
                    this.comboBox_1.Focus();
                    this.bool_1 = false;
                }
            }
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            this.int_0 = e.X;
            this.int_1 = e.Y;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.listView1.Enabled = false;
                this.btnAddRow.Enabled = false;
                this.btnDelete.Enabled = false;
                BulidGeometryNetworkHelper.BulidGNHelper.HasWeight = false;
                BulidGeometryNetworkHelper.BulidGNHelper.Weights.Clear();
                this.listView1.Items.Clear();
            }
            else
            {
                this.listView1.Enabled = true;
                this.btnAddRow.Enabled = true;
                this.btnDelete.Enabled = true;
                BulidGeometryNetworkHelper.BulidGNHelper.HasWeight = true;
            }
        }

        private void textBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.bool_0 = false;
                this.textBox_0.Hide();
            }
            else if (e.KeyChar == '\x001b')
            {
                this.bool_0 = true;
                this.textBox_0.Hide();
            }
        }

        private void textBox_0_LostFocus(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.textBox_0.Hide();
            }
            else
            {
                if (this.listViewItem_0 != null)
                {
                    BulidGeometryNetworkHelper.Weight tag;
                    bool flag;
                    int num;
                    BulidGeometryNetworkHelper.Weight weight2;
                    if (this.listViewItem_0.Tag != null)
                    {
                        tag = this.listViewItem_0.Tag as BulidGeometryNetworkHelper.Weight;
                        flag = true;
                        for (num = 0; num < BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count; num++)
                        {
                            weight2 = BulidGeometryNetworkHelper.BulidGNHelper.Weights[num] as BulidGeometryNetworkHelper.Weight;
                            if ((tag != weight2) && (weight2.networkWeightName == tag.networkWeightName))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            tag.networkWeightName = this.textBox_0.Text;
                            if (this.listViewItem_0.SubItems[0].Text != this.textBox_0.Text)
                            {
                                this.listViewItem_0.SubItems[0].Text = this.textBox_0.Text;
                                if (this.listViewItem_0.SubItems.Count == 1)
                                {
                                    this.listViewItem_0.SubItems.Add("");
                                }
                            }
                        }
                    }
                    else
                    {
                        tag = new BulidGeometryNetworkHelper.Weight {
                            networkWeightName = this.textBox_0.Text,
                            weightType = esriWeightType.esriWTNull
                        };
                        flag = true;
                        for (num = 0; num < BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count; num++)
                        {
                            weight2 = BulidGeometryNetworkHelper.BulidGNHelper.Weights[num] as BulidGeometryNetworkHelper.Weight;
                            if (weight2.networkWeightName == tag.networkWeightName)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            this.listViewItem_0.Tag = tag;
                            BulidGeometryNetworkHelper.BulidGNHelper.Weights.Add(tag);
                            if (this.listViewItem_0.SubItems[0].Text != this.textBox_0.Text)
                            {
                                this.listViewItem_0.SubItems[0].Text = this.textBox_0.Text;
                                if (this.listViewItem_0.SubItems.Count == 1)
                                {
                                    this.listViewItem_0.SubItems.Add("");
                                }
                            }
                        }
                    }
                }
                this.bool_0 = true;
                this.textBox_0.Hide();
            }
        }
    }
}

