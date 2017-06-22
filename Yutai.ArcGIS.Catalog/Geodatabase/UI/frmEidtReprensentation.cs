using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmEidtReprensentation : Form
    {
        private IContainer icontainer_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private IRepresentationClass irepresentationClass_0 = null;
        private IRepresentationRules irepresentationRules_0 = null;
        private ReprensationGeneralPage reprensationGeneralPage_0 = new ReprensationGeneralPage();
        private RepresentationRulesPage representationRulesPage_0 = new RepresentationRulesPage();

        public frmEidtReprensentation()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
        }

 private void frmEidtReprensentation_Load(object sender, EventArgs e)
        {
            this.reprensationGeneralPage_0.RepresentationClass = this.irepresentationClass_0;
            this.reprensationGeneralPage_0.Dock = DockStyle.Fill;
            this.tabPage1.Controls.Add(this.reprensationGeneralPage_0);
            this.representationRulesPage_0.Dock = DockStyle.Fill;
            this.representationRulesPage_0.FeatureClass = this.ifeatureClass_0;
            this.representationRulesPage_0.RepresentationRules = this.irepresentationRules_0;
            this.tabPage2.Controls.Add(this.representationRulesPage_0);
        }

 public IFeatureClass FeatureClass
        {
            set
            {
                this.ifeatureClass_0 = value;
            }
        }

        public IRepresentationClass RepresentationClass
        {
            get
            {
                return this.irepresentationClass_0;
            }
            set
            {
                this.irepresentationClass_0 = value;
                this.ifeatureClass_0 = this.irepresentationClass_0.FeatureClass;
                this.irepresentationRules_0 = this.irepresentationClass_0.RepresentationRules;
            }
        }
    }
}

