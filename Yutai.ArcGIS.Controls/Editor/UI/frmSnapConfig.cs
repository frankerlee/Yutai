using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class frmSnapConfig : Form
    {
        private SimpleButton btnCancle;
        private SimpleButton btnOK;
        private Container components = null;
        private SnapConfigControl m_pSnapConfigCtrl = new SnapConfigControl();
        private Panel panel1;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSnapConfig_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSnapConfig));
            this.panel1 = new Panel();
            this.btnCancle = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnCancle);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0xf3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x162, 0x1c);
            this.panel1.TabIndex = 0;
            this.btnCancle.DialogResult = DialogResult.Cancel;
            this.btnCancle.Location = new Point(0x100, 2);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(0x38, 0x18);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "取消";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xb8, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x162, 0x10f);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSnapConfig";
            this.Text = "捕捉配置";
            base.Load += new EventHandler(this.frmSnapConfig_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public IMap FocusMap
        {
            set
            {
                this.m_pSnapConfigCtrl.Map = value;
            }
        }

        public ISnapEnvironment SnapEnvironment
        {
            set
            {
                this.m_pSnapConfigCtrl.SnapEnvironment = value;
            }
        }
    }
}

