namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Geodatabase;
    using JLK.Editors;
    using JLK.Tab;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmGNPropertySheet : Form
    {
        private SimpleButton btnCnacel;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private IGeometricNetwork igeometricNetwork_0 = null;
        private Panel panel1;
        private XtraTabControl xtraTabControl1;

        public frmGNPropertySheet()
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

        private void frmGNPropertySheet_Load(object sender, EventArgs e)
        {
            XtraTabPage page = new XtraTabPage {
                Text = "常规"
            };
            GeometryNetGeneralPropertyPage page2 = new GeometryNetGeneralPropertyPage {
                GeometricNetwork = this.igeometricNetwork_0
            };
            page.Controls.Add(page2);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "连通性"
            };
            GeometryNewConnectivityPropertyPage page3 = new GeometryNewConnectivityPropertyPage {
                GeometricNetwork = this.igeometricNetwork_0
            };
            page.Controls.Add(page3);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "权重"
            };
            WeightPropertyPage page4 = new WeightPropertyPage {
                GeometricNetwork = this.igeometricNetwork_0
            };
            page.Controls.Add(page4);
            this.xtraTabControl1.TabPages.Add(page);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmGNPropertySheet));
            this.panel1 = new Panel();
            this.btnOK = new SimpleButton();
            this.btnCnacel = new SimpleButton();
            this.xtraTabControl1 = new XtraTabControl();
            this.panel1.SuspendLayout();
            this.xtraTabControl1.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCnacel);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 340);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x214, 0x20);
            this.panel1.TabIndex = 0;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x127, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnCnacel.DialogResult = DialogResult.Cancel;
            this.btnCnacel.Location = new Point(0x165, 6);
            this.btnCnacel.Name = "btnCnacel";
            this.btnCnacel.Size = new Size(0x38, 0x18);
            this.btnCnacel.TabIndex = 6;
            this.btnCnacel.Text = "取消";
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(0x214, 340);
            this.xtraTabControl1.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x214, 0x174);
            base.Controls.Add(this.xtraTabControl1);
            base.Controls.Add(this.panel1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "frmGNPropertySheet";
            this.Text = "网络属性";
            base.Load += new EventHandler(this.frmGNPropertySheet_Load);
            this.panel1.ResumeLayout(false);
            this.xtraTabControl1.EndInit();
            base.ResumeLayout(false);
        }

        public IGeometricNetwork GeometricNetwork
        {
            set
            {
                this.igeometricNetwork_0 = value;
            }
        }
    }
}

