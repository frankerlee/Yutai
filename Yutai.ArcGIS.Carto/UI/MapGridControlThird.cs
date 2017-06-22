using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class MapGridControlThird : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private IMapGrid imapGrid_0 = null;

        public MapGridControlThird()
        {
            this.InitializeComponent();
        }

 private void MapGridControlThird_Load(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.method_0();
            }
        }

        private void MapGridControlThird_VisibleChanged(object sender, EventArgs e)
        {
            if (!base.Visible)
            {
            }
        }

        private void method_0()
        {
            if ((this.imapGrid_0 != null) && !(this.imapGrid_0 is IMeasuredGrid))
            {
            }
        }

        public IMapGrid MapGrid
        {
            set
            {
                this.imapGrid_0 = value;
            }
        }
    }
}

