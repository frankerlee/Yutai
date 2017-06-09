using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.UI
{
    public class NewNetworkDatasetModifyConnectivityPage : UserControl
    {
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ColumnHeader columnHeader_2;
        private ComboBox comboBox1;
        private IContainer icontainer_0 = null;
        private int int_0 = -1;
        private Label label1;
        private ListView listView1;
        private ListViewItem listViewItem_0 = null;
        private ListViewItem.ListViewSubItem listViewSubItem_0 = null;
        private RadioButton rdoFalse;
        private RadioButton rdoTrue;

        public NewNetworkDatasetModifyConnectivityPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            NewNetworkDatasetHelper.NewNetworkDataset.ModifyConnectivity = this.rdoTrue.Checked;
            if (this.rdoTrue.Checked)
            {
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    ListViewItem item = this.listView1.Items[i];
                    if (item.Tag != null)
                    {
                        NewNetworkDatasetHelper.FeatureClassWrap tag = item.Tag as NewNetworkDatasetHelper.FeatureClassWrap;
                        if (tag.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                        {
                            if (item.SubItems[1].Text == "From End")
                            {
                                tag.FromElevationFieldName = item.SubItems[2].Text;
                            }
                            else if (item.SubItems[1].Text == "To End")
                            {
                                tag.ToElevationFieldName = item.SubItems[2].Text;
                            }
                        }
                        else
                        {
                            tag.ElevationFieldName = item.SubItems[2].Text;
                        }
                    }
                }
            }
            return true;
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            Control control = (Control) sender;
            this.listViewSubItem_0.Text = control.Text;
            control.Visible = false;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (((Control) sender).Visible && (this.listViewSubItem_0 != null))
            {
                ComboBox box = (ComboBox) sender;
                if (box.SelectedIndex == 0)
                {
                    this.listViewSubItem_0.Text = "";
                }
                else
                {
                    this.listViewSubItem_0.Text = box.Text;
                }
                box.Visible = false;
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.rdoFalse = new RadioButton();
            this.rdoTrue = new RadioButton();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.columnHeader_2 = new ColumnHeader();
            this.comboBox1 = new ComboBox();
            this.listView1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa1, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "是否用高程字段值修改连通性";
            this.rdoFalse.AutoSize = true;
            this.rdoFalse.Checked = true;
            this.rdoFalse.Location = new System.Drawing.Point(0x13, 0x19);
            this.rdoFalse.Name = "rdoFalse";
            this.rdoFalse.Size = new Size(0x23, 0x10);
            this.rdoFalse.TabIndex = 7;
            this.rdoFalse.TabStop = true;
            this.rdoFalse.Text = "否";
            this.rdoFalse.UseVisualStyleBackColor = true;
            this.rdoFalse.CheckedChanged += new EventHandler(this.rdoFalse_CheckedChanged);
            this.rdoTrue.AutoSize = true;
            this.rdoTrue.Location = new System.Drawing.Point(0x13, 0x38);
            this.rdoTrue.Name = "rdoTrue";
            this.rdoTrue.Size = new Size(0x23, 0x10);
            this.rdoTrue.TabIndex = 6;
            this.rdoTrue.Text = "是";
            this.rdoTrue.UseVisualStyleBackColor = true;
            this.rdoTrue.CheckedChanged += new EventHandler(this.rdoTrue_CheckedChanged);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2 });
            this.listView1.Controls.Add(this.comboBox1);
            this.listView1.Enabled = false;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0x13, 0x5b);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0x146, 160);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.MouseDoubleClick += new MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "源";
            this.columnHeader_0.Width = 0x65;
            this.columnHeader_1.Text = "End";
            this.columnHeader_1.Width = 0x62;
            this.columnHeader_2.Text = "Field";
            this.columnHeader_2.Width = 110;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0xd7, 130);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x79, 20);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.rdoFalse);
            base.Controls.Add(this.rdoTrue);
            base.Name = "NewNetworkDatasetModifyConnectivityPage";
            base.Size = new Size(390, 0x125);
            base.Load += new EventHandler(this.NewNetworkDatasetModifyConnectivityPage_Load);
            this.listView1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem itemAt = this.listView1.GetItemAt(e.X, e.Y);
            if (itemAt != null)
            {
                this.listViewItem_0 = itemAt;
                int left = itemAt.Bounds.Left;
                int index = 0;
                while (index < this.listView1.Columns.Count)
                {
                    left += this.listView1.Columns[index].Width;
                    if (left > e.X)
                    {
                        left -= this.listView1.Columns[index].Width;
                        this.listViewSubItem_0 = itemAt.SubItems[index];
                        this.int_0 = index;
                        break;
                    }
                    index++;
                }
                if (this.int_0 == 2)
                {
                    this.comboBox1.Items.Clear();
                    IFields fields = (itemAt.Tag as NewNetworkDatasetHelper.FeatureClassWrap).FeatureClass.Fields;
                    this.comboBox1.Items.Add("<none>");
                    for (index = 0; index < fields.FieldCount; index++)
                    {
                        IField field = fields.get_Field(index);
                        if ((((field.Type == esriFieldType.esriFieldTypeDouble) || (field.Type == esriFieldType.esriFieldTypeInteger)) || (field.Type == esriFieldType.esriFieldTypeSingle)) || (field.Type == esriFieldType.esriFieldTypeSmallInteger))
                        {
                            this.comboBox1.Items.Add(field.Name);
                        }
                    }
                    this.comboBox1.SelectedValueChanged += new EventHandler(this.comboBox1_SelectedValueChanged);
                    this.comboBox1.Leave += new EventHandler(this.comboBox1_Leave);
                    this.comboBox1.Location = new System.Drawing.Point(left, this.listView1.GetItemRect(this.listView1.Items.IndexOf(itemAt)).Y);
                    this.comboBox1.Width = this.listView1.Columns[this.int_0].Width;
                    if (this.comboBox1.Width > this.listView1.Width)
                    {
                        this.comboBox1.Width = this.listView1.ClientRectangle.Width;
                    }
                    this.comboBox1.Text = this.listViewSubItem_0.Text;
                    this.comboBox1.Visible = true;
                    this.comboBox1.BringToFront();
                    this.comboBox1.Focus();
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void NewNetworkDatasetModifyConnectivityPage_Load(object sender, EventArgs e)
        {
            string[] items = new string[3];
            for (int i = 0; i < NewNetworkDatasetHelper.NewNetworkDataset.FeatureClassWraps.Count; i++)
            {
                NewNetworkDatasetHelper.FeatureClassWrap wrap = NewNetworkDatasetHelper.NewNetworkDataset.FeatureClassWraps[i];
                if (wrap.IsUse)
                {
                    ListViewItem item;
                    if (wrap.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                    {
                        items[0] = wrap.FeatureClass.AliasName;
                        items[1] = "From End";
                        items[2] = "";
                        item = new ListViewItem(items) {
                            Tag = wrap
                        };
                        this.listView1.Items.Add(item);
                        items[1] = "To End";
                        item = new ListViewItem(items) {
                            Tag = wrap
                        };
                        this.listView1.Items.Add(item);
                    }
                    else
                    {
                        items[0] = wrap.FeatureClass.AliasName;
                        items[1] = "";
                        items[2] = "";
                        item = new ListViewItem(items) {
                            Tag = wrap
                        };
                        this.listView1.Items.Add(item);
                    }
                }
            }
        }

        private void rdoFalse_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdoTrue_CheckedChanged(object sender, EventArgs e)
        {
            this.listView1.Enabled = this.rdoTrue.Checked;
        }
    }
}

