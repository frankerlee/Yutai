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
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class frmSpatialAndAttributeQuery : Form
    {
        private UcSpatialAndAttributeQuery m_pSelectByLocationCtrl = new UcSpatialAndAttributeQuery();
        private IAppContext _context;
        public IMap Map
        {
            set
            {
                this.m_pSelectByLocationCtrl.Map = value;
            }
        }

        public frmSpatialAndAttributeQuery(IAppContext context)
        {
            this.InitializeComponent();
            this.m_pSelectByLocationCtrl.Dock = DockStyle.Fill;
            base.Controls.Add(this.m_pSelectByLocationCtrl);
            _context = context;
            Map = _context.MapControl.Map;
        }
        
    }
}
