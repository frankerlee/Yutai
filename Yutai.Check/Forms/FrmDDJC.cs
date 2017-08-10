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
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;

namespace Yutai.Check.Forms
{
    public partial class FrmDDJC : Form
    {
        private readonly IAppContext _context;

        public FrmDDJC(IAppContext context)
        {
            InitializeComponent();
            _context = context;
        }

        public IFeatureLayer FeatureLayer => this.ucSelectFeatureClass1.SelectFeatureLayer;
        public IDictionary<int, IField> SelectedFields => this.ucSelectFields1.SelectedFieldDictionary;

        public void RefreshLayers()
        {
            this.ucSelectFeatureClass1.GeometryType = esriGeometryType.esriGeometryPoint;
            this.ucSelectFeatureClass1.Map = _context.FocusMap;
            this.ucSelectFeatureClass1.SelectComplateEvent += delegate
            {
                this.ucSelectFields1.Fields = this.ucSelectFeatureClass1.SelectFeatureClass.Fields;
            };
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshLayers();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.ucSelectFields1.SelectAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.ucSelectFields1.SelectClear();
        }
    }
}
