using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LayerFieldsPageExtend : UserControl, ILayerAndStandaloneTablePropertyPage
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private IContainer icontainer_0 = null;
        private ILayerFields ilayerFields_0 = null;

        public LayerFieldsPageExtend()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            if (this.bool_0)
            {
                this.bool_0 = false;
                if (this.cboFields.SelectedIndex >= 0)
                {
                    if (this.ilayer_0 is IFeatureLayer)
                    {
                        (this.ilayer_0 as IFeatureLayer).DisplayField = this.cboFields.Text;
                    }
                    else if (this.ilayer_0 is ITinLayer)
                    {
                        (this.ilayer_0 as ITinLayer).DisplayField = this.cboFields.Text;
                    }
                }
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    ListViewItem item = this.listView1.Items[i];
                    IFieldInfo info = this.ilayerFields_0.get_FieldInfo(i);
                    string text = item.SubItems[1].Text;
                    if (text != info.Alias)
                    {
                        info.Alias = text;
                    }
                    info.Visible = item.Checked;
                }
            }
            return true;
        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

 private void LayerFieldsPageExtend_Load(object sender, EventArgs e)
        {
            this.listView1.ItemChecked += new ItemCheckedEventHandler(this.listView1_ItemChecked);
            if (this.ilayerFields_0 != null)
            {
                int num = -1;
                string displayField = "";
                if (this.ilayer_0 is IFeatureLayer)
                {
                    displayField = (this.ilayer_0 as IFeatureLayer).DisplayField;
                }
                else if (this.ilayer_0 is ITinLayer)
                {
                    displayField = (this.ilayer_0 as ITinLayer).DisplayField;
                }
                try
                {
                    string[] items = new string[5];
                    for (int i = 0; i < this.ilayerFields_0.FieldCount; i++)
                    {
                        IField field = this.ilayerFields_0.get_Field(i);
                        if (((((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeRaster)) && (field.Type != esriFieldType.esriFieldTypeBlob)) && (field.Type != esriFieldType.esriFieldTypeOID)) && field.Editable)
                        {
                            IFieldInfo info = this.ilayerFields_0.get_FieldInfo(i);
                            items[0] = field.Name;
                            items[1] = info.Alias;
                            items[2] = this.method_0(field.Type);
                            items[3] = field.Length.ToString();
                            items[4] = field.Precision.ToString();
                            ListViewItem item = new ListViewItem(items);
                            this.listView1.Items.Add(item);
                        }
                        if (((field.Type != esriFieldType.esriFieldTypeGeometry) && (field.Type != esriFieldType.esriFieldTypeRaster)) && (field.Type != esriFieldType.esriFieldTypeBlob))
                        {
                            this.cboFields.Properties.Items.Add(field.Name);
                            if (field.Name == displayField)
                            {
                                num = this.cboFields.Properties.Items.Count - 1;
                            }
                        }
                    }
                }
                catch
                {
                }
                this.listView1.ValueChanged += new ValueChangedHandler(this.method_2);
                if (displayField.Length == 0)
                {
                    this.cboFields.Enabled = false;
                    this.cboFields.Text = "";
                }
                else
                {
                    this.cboFields.Enabled = true;
                    this.cboFields.SelectedIndex = num;
                }
                this.bool_1 = true;
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        private string method_0(esriFieldType esriFieldType_0)
        {
            switch (esriFieldType_0)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "短整形";

                case esriFieldType.esriFieldTypeInteger:
                    return "整形";

                case esriFieldType.esriFieldTypeSingle:
                    return "单精度";

                case esriFieldType.esriFieldTypeDouble:
                    return "双精度";

                case esriFieldType.esriFieldTypeString:
                    return "字符串";

                case esriFieldType.esriFieldTypeDate:
                    return "日期";

                case esriFieldType.esriFieldTypeOID:
                    return "OID";

                case esriFieldType.esriFieldTypeGeometry:
                    return "几何对象";

                case esriFieldType.esriFieldTypeBlob:
                    return "二进制";

                case esriFieldType.esriFieldTypeRaster:
                    return "栅格";

                case esriFieldType.esriFieldTypeGUID:
                    return "GUID";

                case esriFieldType.esriFieldTypeGlobalID:
                    return "GlobalID";

                case esriFieldType.esriFieldTypeXML:
                    return "XML";
            }
            return "未知类型";
        }

        private ITable method_1()
        {
            if (this.ilayer_0 == null)
            {
                return null;
            }
            if (this.ilayer_0 is IDisplayTable)
            {
                return (this.ilayer_0 as IDisplayTable).DisplayTable;
            }
            if (this.ilayer_0 is IAttributeTable)
            {
                return (this.ilayer_0 as IAttributeTable).AttributeTable;
            }
            if (this.ilayer_0 is IFeatureLayer)
            {
                return ((this.ilayer_0 as IFeatureLayer).FeatureClass as ITable);
            }
            return (this.ilayer_0 as ITable);
        }

        private void method_2(object sender, ValueChangedEventArgs e)
        {
            if (this.bool_1)
            {
                this.bool_0 = true;
            }
        }

        public IBasicMap FocusMap
        {
            set
            {
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_0;
            }
        }

        public object SelectItem
        {
            set
            {
                this.ilayerFields_0 = value as ILayerFields;
                this.ilayer_0 = value as ILayer;
            }
        }
    }
}

