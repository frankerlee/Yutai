using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class NewNetworkDatasetModifyConnectivityPage : UserControl
    {
        private IContainer icontainer_0 = null;
        private int int_0 = -1;
        private ListViewItem listViewItem_0 = null;
        private ListViewItem.ListViewSubItem listViewSubItem_0 = null;

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

