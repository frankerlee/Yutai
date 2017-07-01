using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmMultiDataConvert : Form
    {
        private Container container_0 = null;
        private MultiObjectClassSelectControl multiObjectClassSelectControl_0 = new MultiObjectClassSelectControl();

        public frmMultiDataConvert()
        {
            this.InitializeComponent();
            this.multiObjectClassSelectControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.multiObjectClassSelectControl_0);
        }

        public void Add(IGxObjectFilter igxObjectFilter_0)
        {
            this.multiObjectClassSelectControl_0.Add(igxObjectFilter_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.multiObjectClassSelectControl_0.CanDo())
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                this.multiObjectClassSelectControl_0.Do();
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                base.DialogResult = DialogResult.OK;
            }
        }

        public void Clear()
        {
            this.multiObjectClassSelectControl_0.Clear();
        }

        private void frmMultiDataConvert_Load(object sender, EventArgs e)
        {
        }

        public esriDatasetType ImportDatasetType
        {
            set { this.multiObjectClassSelectControl_0.ImportDatasetType = value; }
        }

        public IGxObject InGxObject
        {
            set { this.multiObjectClassSelectControl_0.InGxObject = value; }
        }

        public bool IsAnnotation
        {
            set { this.multiObjectClassSelectControl_0.IsAnnotation = value; }
        }

        public IGxObject OutGxObject
        {
            set { this.multiObjectClassSelectControl_0.OutGxObject = value; }
        }
    }
}