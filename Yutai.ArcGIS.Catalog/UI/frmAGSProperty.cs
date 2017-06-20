using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using ESRI.ArcGIS.GISClient;

namespace Yutai.ArcGIS.Catalog.UI
{
    public class frmAGSProperty : Form
    {
        private Container container_0 = null;
        private IAGSServerConnectionAdmin iagsserverConnectionAdmin_0 = null;
        private Panel panel1;
        private XtraTabControl xtraTabControl1;

        public frmAGSProperty()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmAGSProperty_Load(object sender, EventArgs e)
        {
            XtraTabPage page = new XtraTabPage {
                Text = "目录"
            };
            Control control = new ServerDirectoryPropertyPage();
            (control as ServerDirectoryPropertyPage).AGSServerConnectionAdmin = this.iagsserverConnectionAdmin_0;
            page.Controls.Add(control);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "计算机"
            };
            control = new ServerHostPropertyPage();
            (control as ServerHostPropertyPage).AGSServerConnectionAdmin = this.iagsserverConnectionAdmin_0;
            page.Controls.Add(control);
            this.xtraTabControl1.TabPages.Add(page);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAGSProperty));
            this.panel1 = new Panel();
            this.xtraTabControl1 = new XtraTabControl();
            this.xtraTabControl1.BeginInit();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x11a);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(480, 0x30);
            this.panel1.TabIndex = 0;
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(480, 0x11a);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(480, 330);
            base.Controls.Add(this.xtraTabControl1);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmAGSProperty";
            this.Text = "frmAGSProperty";
            base.Load += new EventHandler(this.frmAGSProperty_Load);
            this.xtraTabControl1.EndInit();
            base.ResumeLayout(false);
        }

        public IAGSServerConnectionAdmin AGSServerConnectionAdmin
        {
            set
            {
                this.iagsserverConnectionAdmin_0 = value;
            }
        }
    }
}

