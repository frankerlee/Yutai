namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BulidGN_Weights : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = true;
        private SimpleButton btnAddRow;
        private SimpleButton btnDelete;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private System.Windows.Forms.ComboBox comboBox_0;
        private System.Windows.Forms.ComboBox comboBox_1;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private int int_0;
        private int int_1;
        private Label label1;
        private ListView listView1;
        private ListViewItem listViewItem_0 = null;
        private RadioGroup radioGroup1;
        private TextBox textBox_0;

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
            for (int i = 1; i < 0x20; i++)
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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
        }

        private void InitializeComponent()
        {
            ListViewItem item = new ListViewItem(new string[] { "", "" }, -1);
            this.groupBox1 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.btnDelete = new SimpleButton();
            this.btnAddRow = new SimpleButton();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(0x10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xd0, 0x38);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "是否设置权重";
            this.radioGroup1.Location = new Point(0x10, 0x18);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否"), new RadioGroupItem(null, "是") });
            this.radioGroup1.Size = new Size(0x90, 0x18);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x10, 0x48);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x74, 0x11);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置权重名称和类型";
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.Enabled = false;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Items.AddRange(new ListViewItem[] { item });
            this.listView1.Location = new Point(0x10, 0x60);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0xd8, 0x80);
            this.listView1.TabIndex = 2;
            this.listView1.View = View.Details;
            this.listView1.MouseDown += new MouseEventHandler(this.listView1_MouseDown);
            this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "权重名";
            this.columnHeader_0.Width = 0x47;
            this.columnHeader_1.Text = "类型";
            this.columnHeader_1.Width = 70;
            this.columnHeader_2.Text = "位门";
            this.columnHeader_2.Width = 0x42;
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new Point(240, 0x80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(40, 0x18);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            this.btnAddRow.Enabled = false;
            this.btnAddRow.Location = new Point(240, 0x60);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new Size(40, 0x18);
            this.btnAddRow.TabIndex = 8;
            this.btnAddRow.Text = "添加";
            this.btnAddRow.Click += new EventHandler(this.btnAddRow_Click);
            base.Controls.Add(this.btnDelete);
            base.Controls.Add(this.btnAddRow);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.groupBox1);
            base.Name = "BulidGN_Weights";
            base.Size = new Size(0x128, 240);
            base.Load += new EventHandler(this.BulidGN_Weights_Load);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
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

