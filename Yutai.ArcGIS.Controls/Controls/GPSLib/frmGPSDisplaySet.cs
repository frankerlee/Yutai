using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public partial class frmGPSDisplaySet : Form
    {
        private IStyleGallery m_pSG = null;
        private TrailsSetCtrl m_TrailsSetCtrl = new TrailsSetCtrl();

        public frmGPSDisplaySet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_TrailsSetCtrl.Apply();
            base.DialogResult = DialogResult.OK;
        }

 private void frmGPSDisplaySet_Load(object sender, EventArgs e)
        {
            TabPage page = new TabPage("跟踪线");
            this.m_TrailsSetCtrl.StyleGallery = this.m_pSG;
            this.m_TrailsSetCtrl.Dock = DockStyle.Fill;
            page.Controls.Add(this.m_TrailsSetCtrl);
            this.tabControl1.TabPages.Add(page);
        }

 public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }
    }
}

