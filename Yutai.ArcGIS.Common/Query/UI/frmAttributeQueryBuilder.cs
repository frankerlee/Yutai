using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Query.UI
{
    public partial class frmAttributeQueryBuilder : Form
    {
        private AttributeQueryBuliderControl attributeQueryBuliderControl_0 = new AttributeQueryBuliderControl();
        private Container container_0 = null;
        private string string_0 = "";

        public frmAttributeQueryBuilder()
        {
            this.InitializeComponent();
            this.attributeQueryBuliderControl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.attributeQueryBuliderControl_0);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            this.string_0 = this.attributeQueryBuliderControl_0.WhereCaluse;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.attributeQueryBuliderControl_0.ClearWhereCaluse();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void frmAttributeQueryBuilder_Load(object sender, EventArgs e)
        {
        }

        public ILayer CurrentLayer
        {
            set { this.attributeQueryBuliderControl_0.CurrentLayer = value; }
        }

        public ITable Table
        {
            set { this.attributeQueryBuliderControl_0.Table = value; }
        }

        public string WhereCaluse
        {
            get { return this.string_0; }
            set { this.attributeQueryBuliderControl_0.WhereCaluse = value; }
        }
    }
}