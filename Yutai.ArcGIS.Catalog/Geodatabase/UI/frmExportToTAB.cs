using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmExportToTAB : Form
    {
        private Container container_0 = null;

        public frmExportToTAB()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.exportToTABControl1.CanDo())
            {
                this.exportToTABControl1.Do();
                base.Close();
            }
        }


    }
}