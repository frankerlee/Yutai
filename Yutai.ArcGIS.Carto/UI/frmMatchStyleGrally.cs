using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmMatchStyleGrally : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private MatchStyleGrallyControl matchStyleGrally1;

        public frmMatchStyleGrally()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.matchStyleGrally1.MakeUniqueValueRenderer();
            base.Close();
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
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.matchStyleGrally1 = new MatchStyleGrallyControl();
            base.SuspendLayout();
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(160, 0x130);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x40, 0x18);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x108, 0x130);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.matchStyleGrally1.Location = new Point(8, 8);
            this.matchStyleGrally1.Name = "matchStyleGrally1";
            this.matchStyleGrally1.Size = new Size(0x150, 0x128);
            this.matchStyleGrally1.TabIndex = 3;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x160, 0x14d);
            base.Controls.Add(this.matchStyleGrally1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Name = "frmMatchStyleGrally";
            this.Text = "匹配符号库";
            base.ResumeLayout(false);
        }

        public IMap FocusMap
        {
            set
            {
                this.matchStyleGrally1.FocusMap = value;
            }
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.matchStyleGrally1.m_pSG = value;
            }
        }
    }
}

