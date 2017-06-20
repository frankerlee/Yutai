using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    public class frmPageSetup : Form
    {
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        protected IMapFrame m_pMapFrame = null;
        private PageSetupControl pageSetupControl_0 = new PageSetupControl();
        private Panel panel;

        public frmPageSetup()
        {
            this.InitializeComponent();
            this.pageSetupControl_0.Dock = DockStyle.Fill;
            this.panel.Controls.Add(this.pageSetupControl_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.pageSetupControl_0.Do();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmPageSetup_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPageSetup));
            this.btnOK = new SimpleButton();
            this.panel = new Panel();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x58, 0x110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x30, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel.Dock = DockStyle.Top;
            this.panel.Location = new Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new Size(0xd0, 0x108);
            this.panel.TabIndex = 1;
            this.btnCancel.DialogResult = DialogResult.OK;
            this.btnCancel.Location = new Point(0x90, 0x110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x30, 0x18);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xd0, 0x12d);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.panel);
            base.Controls.Add(this.btnOK);
            
            base.Name = "frmPageSetup";
            this.Text = "页面设置";
            base.Load += new EventHandler(this.frmPageSetup_Load);
            base.ResumeLayout(false);
        }

        public IMapFrame MapFrame
        {
            set
            {
                this.pageSetupControl_0.MapFrame = value;
            }
        }

        public IPageLayout PageLayout
        {
            set
            {
                this.pageSetupControl_0.PageLayout = value;
            }
        }
    }
}

