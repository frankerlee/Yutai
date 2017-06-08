namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class BulidGN_WeightAssociation : UserControl
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private System.Windows.Forms.ComboBox comboBox_0;
        private ComboBoxEdit comboBoxEdit;
        private Container container_0 = null;
        private IList ilist_0 = new ArrayList();
        private int int_0 = 0;
        private int int_1 = 0;
        private Label label1;
        private Label label2;
        private ListView listView1;
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

        protected override void Dispose(bool bool_2)
        {
            if (bool_2 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_2);
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.comboBoxEdit = new ComboBoxEdit();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.label2 = new Label();
            this.comboBoxEdit.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "网络权重";
            this.comboBoxEdit.EditValue = "";
            this.comboBoxEdit.Location = new Point(0x10, 40);
            this.comboBoxEdit.Name = "comboBoxEdit";
            this.comboBoxEdit.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.comboBoxEdit.Size = new Size(240, 0x17);
            this.comboBoxEdit.TabIndex = 1;
            this.comboBoxEdit.SelectedIndexChanged += new EventHandler(this.comboBoxEdit_SelectedIndexChanged);
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new Point(0x18, 0x68);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(240, 0x68);
            this.listView1.TabIndex = 2;
            this.listView1.View = View.Details;
            this.listView1.MouseDown += new MouseEventHandler(this.listView1_MouseDown);
            this.listView1.DoubleClick += new EventHandler(this.listView1_DoubleClick);
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeader_0.Text = "要素类";
            this.columnHeader_0.Width = 0x66;
            this.columnHeader_1.Text = "字段";
            this.columnHeader_1.Width = 0x7a;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x10, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x99, 0x11);
            this.label2.TabIndex = 3;
            this.label2.Text = "权重和要素类字段关联设置";
            base.Controls.Add(this.label2);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.comboBoxEdit);
            base.Controls.Add(this.label1);
            base.Name = "BulidGN_WeightAssociation";
            base.Size = new Size(0x130, 0xe8);
            base.Load += new EventHandler(this.BulidGN_WeightAssociation_Load);
            this.comboBoxEdit.Properties.EndInit();
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

