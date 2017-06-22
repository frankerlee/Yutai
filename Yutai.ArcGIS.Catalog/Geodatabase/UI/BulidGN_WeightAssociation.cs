using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGN_WeightAssociation : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IList ilist_0 = new ArrayList();
        private int int_0 = 0;
        private int int_1 = 0;
        private ListViewItem listViewItem_0 = null;

        public BulidGN_WeightAssociation()
        {
            this.InitializeComponent();
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
            this.comboBox_0.Hide();
        }

        public void Apply()
        {
            for (int i = 0; i < this.ilist_0.Count; i++)
            {
                BulidGeometryNetworkHelper.WeightAssociation association = this.ilist_0[i] as BulidGeometryNetworkHelper.WeightAssociation;
                if (association.fieldName != "<无>")
                {
                    BulidGeometryNetworkHelper.BulidGNHelper.WeightAssociations.Clear();
                    BulidGeometryNetworkHelper.BulidGNHelper.WeightAssociations.Add(association);
                }
            }
        }

        private void BulidGN_WeightAssociation_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void comboBox_0_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox_0_LostFocus(object sender, EventArgs e)
        {
            this.comboBox_0.Hide();
        }

        private void comboBox_0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.bool_1)
            {
                if ((this.comboBox_0.SelectedIndex >= 0) && (this.listViewItem_0 != null))
                {
                    (this.listViewItem_0.Tag as BulidGeometryNetworkHelper.WeightAssociation).fieldName = this.comboBox_0.Text;
                    this.listViewItem_0.SubItems[1].Text = this.comboBox_0.Text;
                }
                this.comboBox_0.Hide();
            }
        }

        private void comboBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BulidGeometryNetworkHelper.Weight selectedItem = this.comboBoxEdit.SelectedItem as BulidGeometryNetworkHelper.Weight;
            this.listView1.Items.Clear();
            if (selectedItem != null)
            {
                int num = 0;
                for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; i++)
                {
                    BulidGeometryNetworkHelper.FeatureClassWrap wrap = BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                    if (wrap.IsUse)
                    {
                        num++;
                    }
                }
                int num3 = this.comboBoxEdit.SelectedIndex * num;
                string[] items = new string[2];
                for (int j = num3; j < (num3 + num); j++)
                {
                    BulidGeometryNetworkHelper.WeightAssociation association = this.ilist_0[j] as BulidGeometryNetworkHelper.WeightAssociation;
                    items[0] = association.featureClassName;
                    items[1] = association.fieldName;
                    ListViewItem item = new ListViewItem(items) {
                        Tag = association
                    };
                    this.listView1.Items.Add(item);
                }
            }
        }

 public void Init()
        {
            for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.Weights.Count; i++)
            {
                BulidGeometryNetworkHelper.Weight item = BulidGeometryNetworkHelper.BulidGNHelper.Weights[i] as BulidGeometryNetworkHelper.Weight;
                this.comboBoxEdit.Properties.Items.Add(item);
                for (int j = 0; j < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; j++)
                {
                    BulidGeometryNetworkHelper.FeatureClassWrap wrap = BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[j] as BulidGeometryNetworkHelper.FeatureClassWrap;
                    if (wrap.IsUse)
                    {
                        BulidGeometryNetworkHelper.WeightAssociation association = new BulidGeometryNetworkHelper.WeightAssociation {
                            networkWeightName = item.networkWeightName,
                            featureClassName = (wrap.FeatureClass as IDataset).Name,
                            fieldName = "<无>"
                        };
                        this.ilist_0.Add(association);
                    }
                }
            }
            if (this.comboBoxEdit.Properties.Items.Count > 0)
            {
                this.comboBoxEdit.SelectedIndex = 0;
            }
            BulidGeometryNetworkHelper.BulidGNHelper.WeightAssociations.Clear();
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
                if (num4 == 1)
                {
                    num3 = (num3 > this.listView1.ClientSize.Width) ? this.listView1.ClientSize.Width : num3;
                    this.listViewItem_0 = this.listView1.SelectedItems[0];
                    this.bool_1 = true;
                    this.comboBox_0.Size = new Size(num3 - left, this.listViewItem_0.Bounds.Height);
                    this.comboBox_0.Location = new Point(left, this.listViewItem_0.Bounds.Y);
                    this.comboBox_0.Text = this.listViewItem_0.SubItems[1].Text;
                    this.bool_0 = false;
                    this.comboBox_0.Focus();
                    this.comboBox_0.Show();
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
            this.comboBox_0.Items.Clear();
            this.comboBox_0.Items.Add("<无>");
            if (this.listView1.SelectedItems.Count > 0)
            {
                IFeatureClass class2 = this.method_0(this.listView1.SelectedItems[0].Text);
                if (class2 != null)
                {
                    BulidGeometryNetworkHelper.Weight selectedItem = this.comboBoxEdit.SelectedItem as BulidGeometryNetworkHelper.Weight;
                    if (selectedItem != null)
                    {
                        esriFieldType esriFieldTypeOID = esriFieldType.esriFieldTypeOID;
                        switch (selectedItem.weightType)
                        {
                            case esriWeightType.esriWTBitGate:
                            case esriWeightType.esriWTInteger:
                                esriFieldTypeOID = esriFieldType.esriFieldTypeInteger;
                                break;

                            case esriWeightType.esriWTSingle:
                                esriFieldTypeOID = esriFieldType.esriFieldTypeSingle;
                                break;

                            case esriWeightType.esriWTDouble:
                                esriFieldTypeOID = esriFieldType.esriFieldTypeDouble;
                                break;
                        }
                        IFields fields = class2.Fields;
                        for (int i = 0; i < fields.FieldCount; i++)
                        {
                            IField field = fields.get_Field(i);
                            if (((((field.Type != esriFieldType.esriFieldTypeOID) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && ((field.Type != esriFieldType.esriFieldTypeGUID) && (field.Type != esriFieldType.esriFieldTypeGlobalID))) && (field.Type != esriFieldType.esriFieldTypeRaster)) && (field.Type != esriFieldType.esriFieldTypeString))
                            {
                                if ((esriFieldTypeOID == esriFieldType.esriFieldTypeSingle) && (esriFieldTypeOID == field.Type))
                                {
                                    this.comboBox_0.Items.Add(field.AliasName);
                                }
                                else if (esriFieldTypeOID == esriFieldType.esriFieldTypeDouble)
                                {
                                    if ((field.Type == esriFieldType.esriFieldTypeDouble) || (field.Type == esriFieldType.esriFieldTypeSingle))
                                    {
                                        this.comboBox_0.Items.Add(field.AliasName);
                                    }
                                }
                                else if ((esriFieldTypeOID == esriFieldType.esriFieldTypeInteger) && ((field.Type == esriFieldType.esriFieldTypeSmallInteger) || (field.Type == esriFieldType.esriFieldTypeInteger)))
                                {
                                    this.comboBox_0.Items.Add(field.AliasName);
                                }
                            }
                        }
                    }
                }
            }
        }

        private IFeatureClass method_0(string string_0)
        {
            for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap = BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (wrap.IsUse && (string_0 == (wrap.FeatureClass as IDataset).Name))
                {
                    return wrap.FeatureClass;
                }
            }
            return null;
        }
    }
}

