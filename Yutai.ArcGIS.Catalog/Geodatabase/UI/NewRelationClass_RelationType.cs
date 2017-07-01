using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class NewRelationClass_RelationType : UserControl
    {
        private Container container_0 = null;

        public NewRelationClass_RelationType()
        {
            this.InitializeComponent();
        }

        private void NewRelationClass_RelationType_Load(object sender, EventArgs e)
        {
            NewRelationClassHelper.IsComposite = false;
        }

        private void rdoRelationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoRelationType.SelectedIndex == 0)
            {
                NewRelationClassHelper.IsComposite = false;
            }
            else
            {
                NewRelationClassHelper.IsComposite = true;
            }
        }
    }
}