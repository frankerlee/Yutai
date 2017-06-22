using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class CoverageTolerancePropertyPage : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;
        private ICoverage icoverage_0 = null;
        private ICoverageName icoverageName_0 = null;

        public CoverageTolerancePropertyPage()
        {
            this.InitializeComponent();
        }

        public void Apply()
        {
            double toleranceValue = 0.0;
            try
            {
                toleranceValue = double.Parse(this.txtFuzzy.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTFuzzy, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.textDangle.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTDangle, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtEdit.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTEdit, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtGrain.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTGrain, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtNodeSnap.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTNodeSnap, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtSnap.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTSnap, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtTicMatch.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTTicMatch, toleranceValue);
            }
            catch
            {
            }
            try
            {
                toleranceValue = double.Parse(this.txtWeed.Text);
                this.icoverage_0.set_Tolerance(esriCoverageToleranceType.esriCTTWeed, toleranceValue);
            }
            catch
            {
            }
        }

        private void CoverageTolerancePropertyPage_Load(object sender, EventArgs e)
        {
            this.txtFuzzy.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTFuzzy).ToString();
            this.lblFuzzy.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTFuzzy) ? "Verified" : "缺省值";
            this.textDangle.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTDangle).ToString();
            this.lblDangle.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTDangle) ? "Verified" : "缺省值";
            this.txtEdit.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTEdit).ToString();
            this.lblEdit.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTEdit) ? "Verified" : "缺省值";
            this.txtGrain.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTGrain).ToString();
            this.lblGrain.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTGrain) ? "Verified" : "缺省值";
            this.txtNodeSnap.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTNodeSnap).ToString();
            this.lblNodeSnap.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTNodeSnap) ? "Verified" : "缺省值";
            this.txtSnap.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTSnap).ToString();
            this.lblSnap.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTSnap) ? "Verified" : "缺省值";
            this.txtTicMatch.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTTicMatch).ToString();
            this.lblTicMatch.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTTicMatch) ? "Verified" : "缺省值";
            this.txtWeed.Text = this.icoverage_0.get_Tolerance(esriCoverageToleranceType.esriCTTWeed).ToString();
            this.lblWeed.Text = this.icoverage_0.get_ToleranceStatus(esriCoverageToleranceType.esriCTTWeed) ? "Verified" : "缺省值";
        }

 public ICoverageName CoverageName
        {
            set
            {
                this.icoverageName_0 = value;
                this.icoverage_0 = (this.icoverageName_0 as IName).Open() as ICoverage;
            }
        }
    }
}

