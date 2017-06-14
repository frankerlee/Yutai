using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Forms
{
    public partial class frmAlongLineProperties : XtraForm
    {
        private IAppContext _context;

        private IPolyline m_polyline;

        IEditTemplate m_editTemplate;
        IFeatureLayer m_featureLayer;
        IFeatureClass m_featureClass;

        public frmAlongLineProperties(IAppContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public void SetPolyline(IPolyline polyline)
        {
            tbLineLength.Text = (polyline.Length.ToString("F"));
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public bool IsNOP
        {
            get { return rbNOP.Checked; }
        }

        public int NOP
        {
            get { return int.Parse(txtNOP.EditValue.ToString()); }
        }

        public double Distance
        {
            get { return double.Parse(txtDist.EditValue.ToString()); }
        }

        public bool IsEnds
        {
            get { return chkEnds.Checked; }
        }
    }
}