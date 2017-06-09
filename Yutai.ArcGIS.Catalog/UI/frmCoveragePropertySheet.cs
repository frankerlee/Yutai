using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ESRI.ArcGIS.DataSourcesFile;

namespace Yutai.ArcGIS.Catalog.UI
{
    public class frmCoveragePropertySheet : Form
    {
        private SimpleButton btnApply;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private CoverageGeneralPropertyPage coverageGeneralPropertyPage_0 = null;
        private CoverageTicPropertyPage coverageTicPropertyPage_0 = null;
        private CoverageTolerancePropertyPage coverageTolerancePropertyPage_0 = null;
        private ICoverageName icoverageName_0 = null;
        private Panel panel1;
        private SimpleButton simpleButton3;
        private XtraTabControl xtraTabControl1;

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
            XtraTabPage page = new XtraTabPage {
                Text = "常规"
            };
            this.coverageGeneralPropertyPage_0 = new CoverageGeneralPropertyPage();
            this.coverageGeneralPropertyPage_0.CoverageName = this.icoverageName_0;
            page.Controls.Add(this.coverageGeneralPropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "Tic和范围"
            };
            this.coverageTicPropertyPage_0 = new CoverageTicPropertyPage();
            this.coverageTicPropertyPage_0.CoverageName = this.icoverageName_0;
            page.Controls.Add(this.coverageTicPropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
            page = new XtraTabPage {
                Text = "容限值"
            };
            this.coverageTolerancePropertyPage_0 = new CoverageTolerancePropertyPage();
            this.coverageTolerancePropertyPage_0.CoverageName = this.icoverageName_0;
            page.Controls.Add(this.coverageTolerancePropertyPage_0);
            this.xtraTabControl1.TabPages.Add(page);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCoveragePropertySheet));
            this.panel1 = new Panel();
            this.btnOK = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.simpleButton3 = new SimpleButton();
            this.xtraTabControl1 = new XtraTabControl();
            this.panel1.SuspendLayout();
            this.xtraTabControl1.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.simpleButton3);
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
            this.simpleButton3.DialogResult = DialogResult.Cancel;
            this.simpleButton3.Location = new Point(0x170, 8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(0x38, 0x18);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "取消";
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
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmCoveragePropertySheet";
            this.Text = "frmCoveragePropertySheet";
            base.Load += new EventHandler(this.frmCoveragePropertySheet_Load);
            this.panel1.ResumeLayout(false);
            this.xtraTabControl1.EndInit();
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

