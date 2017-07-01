using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmMatchStyleGrally : Form
    {
        private Container container_0 = null;

        public frmMatchStyleGrally()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.matchStyleGrally1.MakeUniqueValueRenderer();
            base.Close();
        }

        public IMap FocusMap
        {
            set { this.matchStyleGrally1.FocusMap = value; }
        }

        public IStyleGallery StyleGallery
        {
            set { this.matchStyleGrally1.m_pSG = value; }
        }
    }
}