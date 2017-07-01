using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Catalog.UI;


namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmRelating : Form
    {
        private Container container_0 = null;
        private IBasicMap ibasicMap_0 = null;
        private ITable itable_0 = null;

        public frmRelating()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (((this.cboRelatingField.SelectedIndex != -1) && (this.cboRelatingTable.SelectedIndex != -1)) &&
                (this.cboRelatingTableField.SelectedIndex != -1))
            {
                if (this.txtName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入关联名!");
                }
                else
                {
                    ITable featureClass = (this.cboRelatingTable.SelectedItem as ObjectWrap).Object as ITable;
                    if (featureClass is IFeatureLayer)
                    {
                        featureClass = (featureClass as IFeatureLayer).FeatureClass as ITable;
                    }
                    JoiningRelatingHelper.RelateTableLayer(this.txtName.Text.Trim(), this.itable_0,
                        this.cboRelatingField.Text, featureClass, this.cboRelatingTableField.Text);
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
        }

        private void btnOpenTable_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterDatasets(), true);
            if (file.ShowDialog() == DialogResult.OK)
            {
                ITable dataset = (file.Items.get_Element(0) as IGxDataset).Dataset as ITable;
                if (dataset != null)
                {
                    this.cboRelatingTable.Properties.Items.Add(new ObjectWrap(dataset));
                }
            }
        }

        private void cboRelatingField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cboRelatingField.SelectedIndex != -1) && (this.cboRelatingTable.SelectedIndex != -1))
            {
                this.method_2();
                if (this.cboRelatingTableField.Properties.Items.Count > 0)
                {
                    this.cboRelatingTableField.SelectedIndex = 0;
                }
            }
        }

        private void cboRelatingTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cboRelatingTable.SelectedIndex != -1) && (this.cboRelatingField.SelectedIndex != -1))
            {
                this.method_2();
                if (this.cboRelatingTableField.Properties.Items.Count > 0)
                {
                    this.cboRelatingTableField.SelectedIndex = 0;
                }
            }
        }

        private void cboRelatingTableField_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void frmRelating_Load(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void method_0(ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer != this.itable_0)
                {
                    if (layer is IAttributeTable)
                    {
                        ITable attributeTable = (layer as IAttributeTable).AttributeTable;
                        this.cboRelatingTable.Properties.Items.Add(new ObjectWrap(layer));
                    }
                    else if (layer is ICompositeLayer)
                    {
                        this.method_0(layer as ICompositeLayer);
                    }
                }
            }
        }

        private void method_1()
        {
            if (this.itable_0 != null)
            {
                int num;
                IFields fields = this.itable_0.Fields;
                if (fields != null)
                {
                    for (num = 0; num < fields.FieldCount; num++)
                    {
                        IField field = fields.get_Field(num);
                        switch (field.Type)
                        {
                            case esriFieldType.esriFieldTypeDouble:
                            case esriFieldType.esriFieldTypeInteger:
                            case esriFieldType.esriFieldTypeOID:
                            case esriFieldType.esriFieldTypeSingle:
                            case esriFieldType.esriFieldTypeSmallInteger:
                            case esriFieldType.esriFieldTypeString:
                                this.cboRelatingField.Properties.Items.Add(field.Name);
                                break;
                        }
                    }
                }
                if (this.cboRelatingField.Properties.Items.Count > 0)
                {
                    this.cboRelatingField.SelectedIndex = 0;
                }
                if (this.ibasicMap_0 != null)
                {
                    ITable attributeTable;
                    ILayer layer = null;
                    for (num = 0; num < this.ibasicMap_0.LayerCount; num++)
                    {
                        layer = this.ibasicMap_0.get_Layer(num);
                        if (layer != this.itable_0)
                        {
                            if (layer is IAttributeTable)
                            {
                                attributeTable = (layer as IAttributeTable).AttributeTable;
                                this.cboRelatingTable.Properties.Items.Add(new ObjectWrap(layer));
                            }
                            else if (layer is ICompositeLayer)
                            {
                                this.method_0(layer as ICompositeLayer);
                            }
                        }
                    }
                    ITableCollection tables = this.ibasicMap_0 as ITableCollection;
                    for (num = 0; num < tables.TableCount; num++)
                    {
                        attributeTable = tables.get_Table(num);
                        this.cboRelatingTable.Properties.Items.Add(new ObjectWrap(attributeTable));
                    }
                    if (this.cboRelatingTable.Properties.Items.Count > 0)
                    {
                        this.cboRelatingTable.SelectedIndex = 0;
                    }
                }
            }
        }

        private void method_2()
        {
            this.cboRelatingTableField.Properties.Items.Clear();
            this.cboRelatingTableField.Text = "";
            ITable table = this.itable_0;
            int index = table.FindField(this.cboRelatingField.Text);
            if (index != -1)
            {
                IField field = table.Fields.get_Field(index);
                object obj2 = (this.cboRelatingTable.SelectedItem as ObjectWrap).Object;
                ITable attributeTable = null;
                if (obj2 is IAttributeTable)
                {
                    attributeTable = (obj2 as IAttributeTable).AttributeTable;
                }
                else if (obj2 is ITable)
                {
                    attributeTable = obj2 as ITable;
                }
                if (attributeTable != null)
                {
                    int num2;
                    IField field2;
                    if (field.Type == esriFieldType.esriFieldTypeString)
                    {
                        for (num2 = 0; num2 < attributeTable.Fields.FieldCount; num2++)
                        {
                            field2 = attributeTable.Fields.get_Field(num2);
                            if (field2.Type == esriFieldType.esriFieldTypeString)
                            {
                                this.cboRelatingTableField.Properties.Items.Add(field2.Name);
                            }
                        }
                    }
                    else
                    {
                        for (num2 = 0; num2 < attributeTable.Fields.FieldCount; num2++)
                        {
                            field2 = attributeTable.Fields.get_Field(num2);
                            if ((((field2.Type == esriFieldType.esriFieldTypeDouble) ||
                                  (field2.Type == esriFieldType.esriFieldTypeInteger)) ||
                                 ((field2.Type == esriFieldType.esriFieldTypeOID) ||
                                  (field2.Type == esriFieldType.esriFieldTypeSingle))) ||
                                (field2.Type == esriFieldType.esriFieldTypeSmallInteger))
                            {
                                this.cboRelatingTableField.Properties.Items.Add(field2.Name);
                            }
                        }
                    }
                }
            }
        }

        public ITable CurrentSelectItem
        {
            set { this.itable_0 = value; }
        }

        public IBasicMap FocusMap
        {
            set { this.ibasicMap_0 = value; }
        }

        internal partial class ObjectWrap
        {
            private object object_0 = null;

            public ObjectWrap(object object_1)
            {
                this.object_0 = object_1;
            }

            public override string ToString()
            {
                if (this.object_0 is ILayer)
                {
                    return (this.object_0 as ILayer).Name;
                }
                if (this.object_0 is IDataset)
                {
                    return (this.object_0 as IDataset).BrowseName;
                }
                return "";
            }

            public object Object
            {
                get { return this.object_0; }
                set { this.object_0 = null; }
            }
        }
    }
}