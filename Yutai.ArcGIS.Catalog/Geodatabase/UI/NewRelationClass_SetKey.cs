using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class NewRelationClass_SetKey : UserControl
    {
        private Container container_0 = null;

        public NewRelationClass_SetKey()
        {
            this.InitializeComponent();
        }

        private void cbodestForeignKey1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.OriginForeignKey = this.cbodestForeignKey1.Text;
        }

        private void cboDestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.destPrimaryKey = this.cboDestion.Text;
        }

        private void cboOrigin_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.OriginPrimaryKey = this.cboOrigin.Text;
            this.cboDestion.Properties.Items.Clear();
            IFields fields = NewRelationClassHelper.DestinationClass.Fields;
            int index = NewRelationClassHelper.OriginClass.Fields.FindField(this.cboOrigin.Text);
            if (index != -1)
            {
                esriFieldType type = NewRelationClassHelper.OriginClass.Fields.get_Field(index).Type;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (((field.Type != esriFieldType.esriFieldTypeBlob) ||
                         (field.Type != esriFieldType.esriFieldTypeGeometry)) && (type == field.Type))
                    {
                        this.cboDestion.Properties.Items.Add(field.Name);
                    }
                }
            }
        }

        private void cboOriginPrimaryKey1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.OriginPrimaryKey = this.cboOriginPrimaryKey1.Text;
            this.cbodestForeignKey1.Properties.Items.Clear();
            IFields fields = NewRelationClassHelper.DestinationClass.Fields;
            int index = NewRelationClassHelper.OriginClass.Fields.FindField(this.cboOriginPrimaryKey1.Text);
            if (index != -1)
            {
                esriFieldType type = NewRelationClassHelper.OriginClass.Fields.get_Field(index).Type;
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (((field.Type != esriFieldType.esriFieldTypeBlob) ||
                         (field.Type != esriFieldType.esriFieldTypeGeometry)) && (type == field.Type))
                    {
                        this.cbodestForeignKey1.Properties.Items.Add(field.Name);
                    }
                }
            }
        }

        private void NewRelationClass_SetKey_Load(object sender, EventArgs e)
        {
            IFields fields;
            int num;
            IField field;
            if (NewRelationClassHelper.Cardinality == esriRelCardinality.esriRelCardinalityManyToMany)
            {
                this.panel1.Visible = true;
                this.panel2.Visible = false;
                this.cboOrigin.Properties.Items.Clear();
                fields = NewRelationClassHelper.OriginClass.Fields;
                for (num = 0; num < fields.FieldCount; num++)
                {
                    field = fields.get_Field(num);
                    if ((field.Type != esriFieldType.esriFieldTypeBlob) ||
                        (field.Type != esriFieldType.esriFieldTypeGeometry))
                    {
                        this.cboOrigin.Properties.Items.Add(field.Name);
                    }
                }
                if (this.cboOrigin.Properties.Items.Count > 0)
                {
                    this.cboOrigin.SelectedIndex = 0;
                }
            }
            else
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
                this.cboOriginPrimaryKey1.Properties.Items.Clear();
                fields = NewRelationClassHelper.OriginClass.Fields;
                for (num = 0; num < fields.FieldCount; num++)
                {
                    field = fields.get_Field(num);
                    if ((field.Type != esriFieldType.esriFieldTypeBlob) ||
                        (field.Type != esriFieldType.esriFieldTypeGeometry))
                    {
                        this.cboOriginPrimaryKey1.Properties.Items.Add(field.Name);
                    }
                }
                if (this.cboOriginPrimaryKey1.Properties.Items.Count > 0)
                {
                    this.cboOriginPrimaryKey1.SelectedIndex = 0;
                }
            }
        }

        private void txtdestForeignKey_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.destForeignKey = this.txtdestForeignKey.Text;
        }

        private void txtOriginPrimaryKey_EditValueChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.OriginForeignKey = this.txtOriginPrimaryKey.Text;
        }
    }
}