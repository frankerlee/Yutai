using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public partial class frmSnapConfig : Form
    {
        private SnapConfigControl m_pSnapConfigCtrl = new SnapConfigControl();

        public frmSnapConfig()
        {
            this.InitializeComponent();
            this.m_pSnapConfigCtrl.Dock = DockStyle.Fill;
            base.Controls.Add(this.m_pSnapConfigCtrl);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_pSnapConfigCtrl.InitSnapEnvironment();
        }

        private void frmSnapConfig_Load(object sender, EventArgs e)
        {
        }

        public IMap FocusMap
        {
            set { this.m_pSnapConfigCtrl.Map = value; }
        }

        public ISnapEnvironment SnapEnvironment
        {
            set { this.m_pSnapConfigCtrl.SnapEnvironment = value; }
        }
    }
}