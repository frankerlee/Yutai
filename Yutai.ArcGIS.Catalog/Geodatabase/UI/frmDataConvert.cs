using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmDataConvert : Form
    {
        private Container container_0 = null;
        private ObjectSelectControl objectSelectControl_0 = new ObjectSelectControl();

        public frmDataConvert()
        {
            this.InitializeComponent();
            this.objectSelectControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.objectSelectControl_0);
            this.objectSelectControl_0.ImportDatasetType = esriDatasetType.esriDTFeatureClass;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.objectSelectControl_0.CanDo())
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                try
                {
                    this.objectSelectControl_0.Do();
                }
                catch
                {
                }
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void frmDataConvert_Load(object sender, EventArgs e)
        {
        }

        public esriDatasetType ImportDatasetType
        {
            set { this.objectSelectControl_0.ImportDatasetType = value; }
        }

        public IGxObject InGxObject
        {
            set { this.objectSelectControl_0.InGxObject = value; }
        }

        public IGxObject OutGxObject
        {
            set { this.objectSelectControl_0.OutGxObject = value; }
        }
    }
}