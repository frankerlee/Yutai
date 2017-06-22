using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmJoinTypeSet : Form
    {
        private Container container_0 = null;
        private esriJoinType esriJoinType_0 = esriJoinType.esriLeftOuterJoin;

        public frmJoinTypeSet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.esriJoinType_0 = esriJoinType.esriLeftOuterJoin;
            }
            else
            {
                this.esriJoinType_0 = esriJoinType.esriLeftInnerJoin;
            }
        }

 private void frmJoinTypeSet_Load(object sender, EventArgs e)
        {
            if (this.esriJoinType_0 == esriJoinType.esriLeftOuterJoin)
            {
                this.radioGroup1.SelectedIndex = 0;
            }
            else
            {
                this.radioGroup1.SelectedIndex = 1;
            }
        }

 private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public esriJoinType JoinType
        {
            get
            {
                return this.esriJoinType_0;
            }
            set
            {
                this.esriJoinType_0 = value;
            }
        }
    }
}

