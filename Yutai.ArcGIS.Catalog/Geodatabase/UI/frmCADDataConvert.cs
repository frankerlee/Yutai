using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmCADDataConvert : Form
    {
        private CADDataConvertControl caddataConvertControl_0 = new CADDataConvertControl();
        private Container container_0 = null;

        public frmCADDataConvert()
        {
            this.InitializeComponent();
            this.caddataConvertControl_0.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(this.caddataConvertControl_0);
        }

        public void Add(IGxObjectFilter igxObjectFilter_0)
        {
            this.caddataConvertControl_0.Add(igxObjectFilter_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.caddataConvertControl_0.CanDo())
            {
                Cursor.Current = Cursors.WaitCursor;
                this.caddataConvertControl_0.Do();
                Cursor.Current = Cursors.Default;
                base.Close();
            }
        }

        public void Clear()
        {
            this.caddataConvertControl_0.Clear();
        }

        private void frmCADDataConvert_Load(object sender, EventArgs e)
        {
        }

        public IGxObject InGxObject
        {
            set { this.caddataConvertControl_0.InGxObject = value; }
        }

        public IGxObject OutGxObject
        {
            set { this.caddataConvertControl_0.OutGxObject = value; }
        }

        public IGxObjectFilter OutGxObjectFilter
        {
            set { this.caddataConvertControl_0.OutGxObjectFilter = value; }
        }
    }
}