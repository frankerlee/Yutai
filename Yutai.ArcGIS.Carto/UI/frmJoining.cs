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
    internal partial class frmJoining : Form
    {
        private Container container_0 = null;
        private esriJoinType esriJoinType_0 = esriJoinType.esriLeftOuterJoin;
        private IBasicMap ibasicMap_0 = null;
        private ITable itable_0 = null;

        public frmJoining()
        {
            this.InitializeComponent();
        }

        private void btnJoiningType_Click(object sender, EventArgs e)
        {
            frmJoinTypeSet set = new frmJoinTypeSet
            {
                JoinType = this.esriJoinType_0
            };
            if (set.ShowDialog() == DialogResult.OK)
            {
                this.esriJoinType_0 = set.JoinType;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (((this.cboJoiningField.SelectedIndex != -1) && (this.cboJoiningTable.SelectedIndex != -1)) &&
                (this.cboJoiningTableField.SelectedIndex != -1))
            {
                ITable featureClass = (this.cboJoiningTable.SelectedItem as ObjectWrap).Object as ITable;
                if (featureClass is IFeatureLayer)
                {
                    featureClass = (featureClass as IFeatureLayer).FeatureClass as ITable;
                }
                JoiningRelatingHelper.JoinTableLayer(this.itable_0, this.cboJoiningField.Text, featureClass,
                    this.cboJoiningTableField.Text);
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
                    this.cboJoiningTable.Properties.Items.Add(new ObjectWrap(dataset));
                }
            }
        }

        private void cboJoiningField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cboJoiningField.SelectedIndex != -1) && (this.cboJoiningTable.SelectedIndex != -1))
            {
                this.method_2();
                if (this.cboJoiningTableField.Properties.Items.Count > 0)
                {
                    this.cboJoiningTableField.SelectedIndex = 0;
                }
            }
        }

        private void cboJoiningTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.cboJoiningTable.SelectedIndex != -1) && (this.cboJoiningField.SelectedIndex != -1))
            {
                this.method_2();
                if (this.cboJoiningTableField.Properties.Items.Count > 0)
                {
                    this.cboJoiningTableField.SelectedIndex = 0;
                }
            }
        }

        private void cboJoiningTableField_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void frmJoining_Load(object sender, EventArgs e)
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
                        if (!JoiningRelatingHelper.TableIsJoinLayer(this.itable_0, attributeTable))
                        {
                            this.cboJoiningTable.Properties.Items.Add(new ObjectWrap(layer));
                        }
                    }
                    else if (layer is IGroupLayer)
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
                                this.cboJoiningField.Properties.Items.Add(field.Name);
                                break;
                        }
                    }
                }
                if (this.cboJoiningField.Properties.Items.Count > 0)
                {
                    this.cboJoiningField.SelectedIndex = 0;
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
                                if (!JoiningRelatingHelper.TableIsJoinLayer(this.itable_0, attributeTable))
                                {
                                    this.cboJoiningTable.Properties.Items.Add(new ObjectWrap(layer));
                                }
                            }
                            else if (layer is IGroupLayer)
                            {
                                this.method_0(layer as ICompositeLayer);
                            }
                        }
                    }
                    IStandaloneTableCollection tables = this.ibasicMap_0 as IStandaloneTableCollection;
                    for (num = 0; num < tables.StandaloneTableCount; num++)
                    {
                        attributeTable = tables.get_StandaloneTable(num) as ITable;
                        if (!JoiningRelatingHelper.TableIsJoinLayer(this.itable_0, attributeTable))
                        {
                            this.cboJoiningTable.Properties.Items.Add(new ObjectWrap(attributeTable));
                        }
                    }
                    if (this.cboJoiningTable.Properties.Items.Count > 0)
                    {
                        this.cboJoiningTable.SelectedIndex = 0;
                    }
                }
            }
        }

        private void method_2()
        {
            this.cboJoiningTableField.Properties.Items.Clear();
            this.cboJoiningTableField.Text = "";
            ITable table = this.itable_0;
            int index = table.FindField(this.cboJoiningField.Text);
            if (index != -1)
            {
                IField field = table.Fields.get_Field(index);
                object obj2 = (this.cboJoiningTable.SelectedItem as ObjectWrap).Object;
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
                                this.cboJoiningTableField.Properties.Items.Add(field2.Name);
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
                                this.cboJoiningTableField.Properties.Items.Add(field2.Name);
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