using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class NewRelationClass_SetCardinality : UserControl
    {
        private Container container_0 = null;

        public NewRelationClass_SetCardinality()
        {
            this.InitializeComponent();
        }

        private void chkAttribute_CheckedChanged(object sender, EventArgs e)
        {
            NewRelationClassHelper.IsAttributed = this.chkAttribute.Checked;
            if (NewRelationClassHelper.IsAttributed)
            {
                NewRelationClassHelper.relAttrFields = new FieldsClass();
            }
            else
            {
                NewRelationClassHelper.relAttrFields = null;
            }
        }

 private void NewRelationClass_SetCardinality_Load(object sender, EventArgs e)
        {
            NewRelationClassHelper.IsAttributed = false;
            NewRelationClassHelper.Cardinality = esriRelCardinality.esriRelCardinalityOneToOne;
        }

        private void rdoMore2More_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoMore2More.Checked)
            {
                NewRelationClassHelper.Cardinality = esriRelCardinality.esriRelCardinalityManyToMany;
            }
        }

        private void rdoOne2More_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoOne2More.Checked)
            {
                NewRelationClassHelper.Cardinality = esriRelCardinality.esriRelCardinalityOneToMany;
            }
        }

        private void rdoOnr2One_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoOnr2One.Checked)
            {
                NewRelationClassHelper.Cardinality = esriRelCardinality.esriRelCardinalityOneToOne;
            }
        }
    }
}

