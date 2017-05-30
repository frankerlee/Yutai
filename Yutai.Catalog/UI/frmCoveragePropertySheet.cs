namespace Yutai.Catalog.UI
{
    using ESRI.ArcGIS.DataSourcesFile;
   
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmCoveragePropertySheet : Form
    {
        private Button btnApply;
        private Button btnOK;
        private Container container_0 = null;
        private CoverageGeneralPropertyPage coverageGeneralPropertyPage_0 = null;
        private CoverageTicPropertyPage coverageTicPropertyPage_0 = null;
        private CoverageTolerancePropertyPage coverageTolerancePropertyPage_0 = null;
        private ICoverageName icoverageName_0 = null;
        private Panel panel1;
        private Button Button3;
        private TabControl xtraTabControl1;

        public frmCoveragePropertySheet()
        {
            this.InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.coverageTicPropertyPage_0.Apply();
            this.coverageTolerancePropertyPage_0.Apply();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.coverageTicPropertyPage_0.Apply();
            this.coverageTolerancePropertyPage_0.Apply();
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmCoveragePropertySheet_Load(object sender, EventArgs e)
        {
            TabPage page = new TabPage {
                Text = "常规"
            };
            this.coverageGeneralPropertyPage_0 = new CoverageGeneralPropertyPage();
            this.coverageGeneralPropertyPage_0.CoverageName = this.icoverageName_0;
            page.Controls.Add(this.coverageGeneralPropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
            page = new TabPage {
                Text = "Tic和范围"
            };
            this.coverageTicPropertyPage_0 = new CoverageTicPropertyPage();
            this.coverageTicPropertyPage_0.CoverageName = this.icoverageName_0;
            page.Controls.Add(this.coverageTicPropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
            page = new TabPage {
                Text = "容限值"
            };
            this.coverageTolerancePropertyPage_0 = new CoverageTolerancePropertyPage();
            this.coverageTolerancePropertyPage_0.CoverageName = this.icoverageName_0;
            page.Controls.Add(this.coverageTolerancePropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmCoveragePropertySheet));
            this.panel1 = new Panel();
            this.btnOK = new Button();
            this.btnApply = new Button();
            this.Button3 = new Button();
            this.xtraTabControl1 = new TabControl();
            this.panel1.SuspendLayout();
           
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.Button3);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x145);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1f8, 40);
            this.panel1.TabIndex = 4;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x128, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnApply.Location = new Point(0x1b0, 8);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x38, 0x18);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.Button3.DialogResult = DialogResult.Cancel;
            this.Button3.Location = new Point(0x170, 8);
            this.Button3.Name = "Button3";
            this.Button3.Size = new Size(0x38, 0x18);
            this.Button3.TabIndex = 2;
            this.Button3.Text = "取消";
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(0x1f8, 0x16d);
            this.xtraTabControl1.TabIndex = 3;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x1f8, 0x16d);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.xtraTabControl1);
            
            base.Name = "frmCoveragePropertySheet";
            this.Text = "frmCoveragePropertySheet";
            base.Load += new EventHandler(this.frmCoveragePropertySheet_Load);
            this.panel1.ResumeLayout(false);
           
            base.ResumeLayout(false);
        }

        public ICoverageName CoverageName
        {
            set
            {
                this.icoverageName_0 = value;
            }
        }
    }
}

