using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Forms
{
    public partial class FrmLDJC : Form
    {
        private readonly IAppContext _context;

        public FrmLDJC(IAppContext context)
        {
            InitializeComponent();
            _context = context;
        }
        
        public IFeatureLayer PointFeatureLayer => this.ucSelectFeatureClass1.SelectFeatureLayer;
        
        public IFeatureLayer LineFeatureLayer => this.ucSelectFeatureClass2.SelectFeatureLayer;

        public void RefreshLayers()
        {
            this.ucSelectFeatureClass1.GeometryType = esriGeometryType.esriGeometryPoint;
            this.ucSelectFeatureClass1.Map = _context.FocusMap;

            this.ucSelectFeatureClass2.GeometryType = esriGeometryType.esriGeometryPolyline;
            this.ucSelectFeatureClass2.Map = _context.FocusMap;
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshLayers();
        }
    }
}
