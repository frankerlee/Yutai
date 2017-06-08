namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.Display;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmNewReprensentationWizard : Form
    {
        private Button btnCancel;
        private Button btnLast;
        private Button btnNext;
        private IContainer icontainer_0 = null;
        private IFeatureClass ifeatureClass_0 = null;
        private int int_0 = 0;
        private IRepresentationClass irepresentationClass_0 = null;
        private IRepresentationRules irepresentationRules_0 = null;
        private Panel panel1;
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
                        UID gUID = new UIDClass {
                            Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                        };
                        IRepresentationWorkspaceExtension extension = manager.FindExtension(gUID) as IRepresentationWorkspaceExtension;
                        if (extension != null)
                        {
                            new RepresentationRulesClass();
                            this.irepresentationClass_0 = extension.CreateRepresentationClass(this.ifeatureClass_0, this.reprensationGeneralPage_0.RepresentationName, this.reprensationGeneralPage_0.RuleIDFieldName, this.reprensationGeneralPage_0.OverrideFieldName, this.reprensationGeneralPage_0.RequireShapeOverride, this.irepresentationRules_0, null);
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmNewReprensentationWizard));
            this.panel1 = new Panel();
            this.btnNext = new Button();
            this.btnLast = new Button();
            this.btnCancel = new Button();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1dc, 0x13a);
            this.panel1.TabIndex = 15;
            this.btnNext.Location = new System.Drawing.Point(0x146, 0x146);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(0x40, 0x1c);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new System.Drawing.Point(0xfc, 0x146);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(0x40, 0x1c);
            this.btnLast.TabIndex = 12;
            this.btnLast.Text = "<上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x18c, 0x146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x40, 0x1c);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1dc, 0x16e);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmNewReprensentationWizard";
            this.Text = "新建制图表现向导";
            base.Load += new EventHandler(this.frmNewReprensentationWizard_Load);
            base.ResumeLayout(false);
        }

        private IBasicSymbol method_0(IFeatureClass ifeatureClass_1)
        {
            IBasicSymbol symbol = null;
            if ((ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryMultipoint) || (ifeatureClass_1.ShapeType == esriGeometryType.esriGeometryPoint))
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
        }
    }
}

