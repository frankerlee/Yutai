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
using Syncfusion.Windows.Forms;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Identifer.Query
{
    public partial class frmSelectByLocation : MetroForm
    {

        private UcSelectByLocation _ucSelectByLocation = new UcSelectByLocation();

        private IAppContext _context;
        private bool _closeButton;

        /// <summary>
        /// 设置关联的地图对象
        /// </summary>
        public IMap Map
        {
            set
            {
                this._ucSelectByLocation.Map = value;
            }
        }

        public bool CloseButton
        {
            get { return _closeButton; }
            set
            {
                _closeButton = value;
                _ucSelectByLocation.CloseButton = _closeButton;
            }
        }

        /// <summary>
        /// </summary>
        public frmSelectByLocation()
        {
            this.InitializeComponent();
            this._ucSelectByLocation.Dock = DockStyle.Fill;
            base.Controls.Add(this._ucSelectByLocation);
        }

        public frmSelectByLocation(IAppContext context)
        {
            this.InitializeComponent();
            this._ucSelectByLocation.Dock = DockStyle.Fill;
            base.Controls.Add(this._ucSelectByLocation);
            _context = context;
            _ucSelectByLocation.Map = _context.MapControl.Map;
        }


    }
}
