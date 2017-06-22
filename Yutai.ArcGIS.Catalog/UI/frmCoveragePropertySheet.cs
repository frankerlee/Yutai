using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ESRI.ArcGIS.DataSourcesFile;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmCoveragePropertySheet : Form
    {
        private Container container_0 = null;
        private CoverageGeneralPropertyPage coverageGeneralPropertyPage_0 = null;
        private CoverageTicPropertyPage coverageTicPropertyPage_0 = null;
        private CoverageTolerancePropertyPage coverageTolerancePropertyPage_0 = null;
        private ICoverageName icoverageName_0 = null;

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

 public ICoverageName CoverageName
        {
            set
            {
                this.icoverageName_0 = value;
            }
        }
    }
}

