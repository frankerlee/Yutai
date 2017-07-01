using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmNewReprensentationWizard : Form
    {
        private IContainer icontainer_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private int int_0 = 0;
        private IRepresentationClass irepresentationClass_0 = null;
        private IRepresentationRules irepresentationRules_0 = null;
        private ReprensationGeneralPage reprensationGeneralPage_0 = new ReprensationGeneralPage();
        private RepresentationRulesPage representationRulesPage_0 = new RepresentationRulesPage();

        public frmNewReprensentationWizard()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.reprensationGeneralPage_0.Visible = true;
                    this.representationRulesPage_0.Visible = false;
                    this.btnLast.Enabled = false;
                    this.btnNext.Text = "下一步>";
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (!this.reprensationGeneralPage_0.Apply())
                    {
                        return;
                    }
                    this.reprensationGeneralPage_0.Visible = false;
                    this.representationRulesPage_0.Visible = true;
                    this.btnLast.Enabled = true;
                    this.btnNext.Text = "完成";
                    break;

                case 1:
                {
                    IDataset dataset = this.ifeatureClass_0 as IDataset;
                    IWorkspace workspace = dataset.Workspace;
                    try
                    {
                        IWorkspaceExtensionManager manager = workspace as IWorkspaceExtensionManager;
                        UID gUID = new UIDClass
                        {
                            Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                        };
                        IRepresentationWorkspaceExtension extension =
                            manager.FindExtension(gUID) as IRepresentationWorkspaceExtension;
                        if (extension != null)
                        {
                            new RepresentationRulesClass();
                            this.irepresentationClass_0 = extension.CreateRepresentationClass(this.ifeatureClass_0,
                                this.reprensationGeneralPage_0.RepresentationName,
                                this.reprensationGeneralPage_0.RuleIDFieldName,
                                this.reprensationGeneralPage_0.OverrideFieldName,
                                this.reprensationGeneralPage_0.RequireShapeOverride, this.irepresentationRules_0, null);
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        return;
                    }
                    base.DialogResult = DialogResult.OK;
                    return;
                }
            }
            this.int_0++;
        }

        private void frmNewReprensentationWizard_Load(object sender, EventArgs e)
        {
            this.irepresentationRules_0 = new RepresentationRulesClass();
            IRepresentationRule repRule = this.method_1(this.ifeatureClass_0);
            this.irepresentationRules_0.Add(repRule);
            this.reprensationGeneralPage_0.FeatureClass = this.ifeatureClass_0;
            this.reprensationGeneralPage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.reprensationGeneralPage_0);
            this.representationRulesPage_0.Dock = DockStyle.Fill;
            this.representationRulesPage_0.Visible = false;
            this.representationRulesPage_0.FeatureClass = this.ifeatureClass_0;
            this.representationRulesPage_0.RepresentationRules = this.irepresentationRules_0;
            this.panel1.Controls.Add(this.representationRulesPage_0);
        }

        private IBasicSymbol method_0(IFeatureClass ifeatureClass_1)
        {
            IBasicSymbol symbol = null;
            if ((ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryMultipoint) ||
                (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPoint))
            {
                return new BasicMarkerSymbolClass();
            }
            if (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                return new BasicLineSymbolClass();
            }
            if (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPolygon)
            {
                symbol = new BasicFillSymbolClass();
            }
            return symbol;
        }

        private IRepresentationRule method_1(IFeatureClass ifeatureClass_1)
        {
            IBasicSymbol symbol = this.method_0(ifeatureClass_1);
            IRepresentationRule rule = new RepresentationRuleClass();
            rule.InsertLayer(0, symbol);
            return rule;
        }

        public IFeatureClass FeatureClass
        {
            set { this.ifeatureClass_0 = value; }
        }

        public IRepresentationClass RepresentationClass
        {
            get { return this.irepresentationClass_0; }
        }
    }
}