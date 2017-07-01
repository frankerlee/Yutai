using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class LegendWizard : Form
    {
        private Container container_0 = null;
        private IArray iarray_0 = new ArrayClass();
        private ILegend ilegend_0 = new LegendClass_2();
        private IMap imap_0 = null;
        private IMapSurroundFrame imapSurroundFrame_0 = null;
        private int int_0 = 1;
        private LegendFormatSetupCtrl legendFormatSetupCtrl_0 = new LegendFormatSetupCtrl();
        private LegendFrameUserControl legendFrameUserControl_0 = new LegendFrameUserControl();
        private LegendLayerUserControl legendLayerUserControl_0 = new LegendLayerUserControl();
        private LegendSetupUserControl legendSetupUserControl_0 = new LegendSetupUserControl();
        private LegendTitleUserControl legendTitleUserControl_0 = new LegendTitleUserControl();

        public LegendWizard()
        {
            this.InitializeComponent();
            this.legendLayerUserControl_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.legendLayerUserControl_0);
            this.legendTitleUserControl_0.Dock = DockStyle.Fill;
            this.legendTitleUserControl_0.Visible = false;
            this.panel1.Controls.Add(this.legendTitleUserControl_0);
            this.legendFrameUserControl_0.Dock = DockStyle.Fill;
            this.legendFrameUserControl_0.Visible = false;
            this.panel1.Controls.Add(this.legendFrameUserControl_0);
            this.legendSetupUserControl_0.Dock = DockStyle.Fill;
            this.legendSetupUserControl_0.Visible = false;
            this.panel1.Controls.Add(this.legendSetupUserControl_0);
            this.legendFormatSetupCtrl_0.Dock = DockStyle.Fill;
            this.legendFormatSetupCtrl_0.Visible = false;
            this.panel1.Controls.Add(this.legendFormatSetupCtrl_0);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                this.int_0++;
                switch (this.int_0)
                {
                    case 2:
                        this.legendLayerUserControl_0.Visible = false;
                        this.legendTitleUserControl_0.Visible = true;
                        this.btnPre.Enabled = true;
                        return;

                    case 3:
                        this.legendTitleUserControl_0.Visible = false;
                        this.legendFrameUserControl_0.Visible = true;
                        return;

                    case 4:
                        this.legendFrameUserControl_0.Visible = false;
                        this.legendSetupUserControl_0.Visible = true;
                        return;

                    case 5:
                        this.btnOK.Visible = true;
                        this.btnNext.Visible = false;
                        this.legendSetupUserControl_0.Visible = false;
                        this.legendFormatSetupCtrl_0.Visible = true;
                        return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.ilegend_0.AutoAdd = true;
            this.ilegend_0.AutoReorder = true;
            this.ilegend_0.AutoVisibility = true;
            this.imapSurroundFrame_0.MapSurround = this.ilegend_0;
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            this.int_0--;
            switch (this.int_0)
            {
                case 1:
                    this.legendLayerUserControl_0.Visible = true;
                    this.legendTitleUserControl_0.Visible = false;
                    this.btnPre.Enabled = false;
                    break;

                case 2:
                    this.legendTitleUserControl_0.Visible = true;
                    this.legendFrameUserControl_0.Visible = false;
                    break;

                case 3:
                    this.legendFrameUserControl_0.Visible = true;
                    this.legendSetupUserControl_0.Visible = false;
                    break;

                case 4:
                    this.btnOK.Visible = false;
                    this.btnNext.Visible = true;
                    this.legendSetupUserControl_0.Visible = true;
                    this.legendFormatSetupCtrl_0.Visible = false;
                    break;
            }
        }

        public IMap FocusMap
        {
            set
            {
                this.imap_0 = value;
                this.ilegend_0.Map = this.imap_0;
                this.legendLayerUserControl_0.FocusMap = value;
                this.legendLayerUserControl_0.Legend = this.ilegend_0;
                this.legendSetupUserControl_0.Legend = this.ilegend_0;
                this.legendTitleUserControl_0.Legend = this.ilegend_0;
                this.legendFormatSetupCtrl_0.Legend = this.ilegend_0;
            }
        }

        public IMapSurroundFrame InitialLegendFrame
        {
            set
            {
                this.imapSurroundFrame_0 = value;
                this.legendFrameUserControl_0.LegendFrame = value;
            }
        }

        public IMapSurroundFrame LegendFrame
        {
            get { return this.imapSurroundFrame_0; }
        }

        public IStyleGallery StyleGallery
        {
            set { this.legendSetupUserControl_0.StyleGallery = value; }
        }
    }
}