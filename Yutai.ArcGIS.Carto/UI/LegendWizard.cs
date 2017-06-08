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
    public class LegendWizard : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnNext;
        private SimpleButton btnOK;
        private SimpleButton btnPre;
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
        private Panel panel1;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.btnPre = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1c8, 0x110);
            this.panel1.TabIndex = 0;
            this.btnPre.Enabled = false;
            this.btnPre.Location = new Point(0xd8, 280);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new Size(0x38, 0x18);
            this.btnPre.TabIndex = 1;
            this.btnPre.Text = "<上一步";
            this.btnPre.Click += new EventHandler(this.btnPre_Click);
            this.btnNext.Location = new Point(0x120, 280);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x38, 0x18);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(360, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x120, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "完成";
            this.btnOK.Visible = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1c8, 0x135);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnPre);
            base.Controls.Add(this.panel1);
            base.Name = "LegendWizard";
            this.Text = "图例向导";
            base.ResumeLayout(false);
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
            get
            {
                return this.imapSurroundFrame_0;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.legendSetupUserControl_0.StyleGallery = value;
            }
        }
    }
}

