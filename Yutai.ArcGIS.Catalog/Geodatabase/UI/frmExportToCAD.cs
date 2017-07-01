using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmExportToCAD : Form
    {
        private Container container_0 = null;

        public frmExportToCAD()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.exportToCADControl1.CanDo())
            {
                this.exportToCADControl1.Do();
                base.Close();
            }
        }
    }
}