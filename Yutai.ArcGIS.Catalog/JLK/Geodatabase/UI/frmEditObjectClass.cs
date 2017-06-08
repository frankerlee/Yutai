namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmEditObjectClass : Form
    {
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private CoordinateControl coordinateControl_0 = new CoordinateControl();
        private IContainer icontainer_0 = null;
        private NewDatasetCoordinateDomainPage newDatasetCoordinateDomainPage_0 = new NewDatasetCoordinateDomainPage();
        private NewDatasetSpatialRefPage newDatasetSpatialRefPage_0 = new NewDatasetSpatialRefPage();
        private NewDatasetTolerancePage newDatasetTolerancePage_0 = new NewDatasetTolerancePage();
        private NewObjectClassFieldsPage newObjectClassFieldsPage_0 = new NewObjectClassFieldsPage();
        private NewObjectClassGeneralPage newObjectClassGeneralPage_0 = new NewObjectClassGeneralPage();
        private RepresentationPropertyPage representationPropertyPage_0 = new RepresentationPropertyPage();
        private TabControl tabControl1;
        private VCSCoordinateInfoPage vcscoordinateInfoPage_0 = new VCSCoordinateInfoPage();

        public frmEditObjectClass()
        {
            this.InitializeComponent();
            NewObjectClassHelper.m_pObjectClassHelper = new NewObjectClassHelper();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.method_2();
            this.btnApply.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.btnApply.Enabled)
            {
                if (this.method_2())
                {
                    base.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                base.DialogResult = DialogResult.OK;
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void frmEditObjectClass_Load(object sender, EventArgs e)
        {
            this.btnApply.Enabled = false;
            TabPage page = new TabPage("常规");
            this.newObjectClassGeneralPage_0.Dock = DockStyle.Fill;
            page.Controls.Add(this.newObjectClassGeneralPage_0);
            this.tabControl1.TabPages.Add(page);
            if ((NewObjectClassHelper.m_pObjectClassHelper.ObjectClass.ObjectClassID != -1) && NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
            {
                page = new TabPage("容差");
                this.newDatasetTolerancePage_0.Dock = DockStyle.Fill;
                page.Controls.Add(this.newDatasetTolerancePage_0);
                this.tabControl1.TabPages.Add(page);
            }
            page = new TabPage("XY坐标系统");
            this.coordinateControl_0.Dock = DockStyle.Fill;
            this.coordinateControl_0.IsEdit = false;
            this.coordinateControl_0.SpatialRefrence = NewObjectClassHelper.m_pObjectClassHelper.SpatialReference;
            page.Controls.Add(this.coordinateControl_0);
            this.tabControl1.TabPages.Add(page);
            if ((NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision && (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is ISpatialReference3)) && ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem != null))
            {
                page = new TabPage("Z坐标系统");
                this.vcscoordinateInfoPage_0.Dock = DockStyle.Fill;
                this.vcscoordinateInfoPage_0.IsEdit = false;
                this.vcscoordinateInfoPage_0.SpatialReference = (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem;
                page.Controls.Add(this.vcscoordinateInfoPage_0);
                this.tabControl1.TabPages.Add(page);
            }
            if (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass.ObjectClassID != -1)
            {
                page = new TabPage("精度和域");
                this.newDatasetCoordinateDomainPage_0.Dock = DockStyle.Fill;
                page.Controls.Add(this.newDatasetCoordinateDomainPage_0);
                this.tabControl1.TabPages.Add(page);
            }
            page = new TabPage("字段");
            this.newObjectClassFieldsPage_0.Workspace = (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass as IDataset).Workspace;
            this.newObjectClassFieldsPage_0.Dock = DockStyle.Fill;
            page.Controls.Add(this.newObjectClassFieldsPage_0);
            this.tabControl1.TabPages.Add(page);
            if (((NewObjectClassHelper.m_pObjectClassHelper.ObjectClass.ObjectClassID != -1) && (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass is IFeatureClass)) && this.method_0(NewObjectClassHelper.m_pObjectClassHelper.ObjectClass as IFeatureClass))
            {
                page = new TabPage("制图表现");
                this.representationPropertyPage_0.Dock = DockStyle.Fill;
                this.representationPropertyPage_0.FeatureClass = NewObjectClassHelper.m_pObjectClassHelper.ObjectClass as IFeatureClass;
                page.Controls.Add(this.representationPropertyPage_0);
                this.tabControl1.TabPages.Add(page);
            }
            this.newObjectClassGeneralPage_0.ValueChanged += new ValueChangedHandler(this.method_1);
            this.newObjectClassFieldsPage_0.ValueChanged += new ValueChangedHandler(this.method_1);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(frmEditObjectClass));
            this.tabControl1 = new TabControl();
            this.btnCancel = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1ac, 0x1d4);
            this.tabControl1.TabIndex = 0;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x13e, 0x1e6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(0x189, 0x1e6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x38, 0x18);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnOK.Location = new System.Drawing.Point(0xf2, 0x1e6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c4, 0x20a);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmEditObjectClass";
            this.Text = "对象类属性";
            base.Load += new EventHandler(this.frmEditObjectClass_Load);
            base.ResumeLayout(false);
        }

        private bool method_0(IFeatureClass ifeatureClass_0)
        {
            if (ifeatureClass_0 != null)
            {
                try
                {
                    IDataset dataset = ifeatureClass_0 as IDataset;
                    IWorkspaceExtensionManager workspace = dataset.Workspace as IWorkspaceExtensionManager;
                    UID gUID = new UIDClass {
                        Value = "{FD05270A-8E0B-4823-9DEE-F149347C32B6}"
                    };
                    IRepresentationWorkspaceExtension extension = workspace.FindExtension(gUID) as IRepresentationWorkspaceExtension;
                    if (extension == null)
                    {
                        return false;
                    }
                    return true;
                }
                catch
                {
                }
            }
            return false;
        }

        private void method_1(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
        }

        private bool method_2()
        {
            if (!ObjectClassShareData.m_IsShapeFile)
            {
                IClassSchemaEdit4 objectClass = NewObjectClassHelper.m_pObjectClassHelper.ObjectClass as IClassSchemaEdit4;
                if (this.newObjectClassGeneralPage_0.AliasName != NewObjectClassHelper.m_pObjectClassHelper.ObjectClass.AliasName)
                {
                    (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass as IClassSchemaEdit).AlterAliasName(this.newObjectClassGeneralPage_0.AliasName);
                }
            }
            return this.newObjectClassFieldsPage_0.Apply();
        }

        public IObjectClass ObjectClass
        {
            set
            {
                NewObjectClassHelper.m_pObjectClassHelper.ObjectClass = value;
            }
        }
    }
}

