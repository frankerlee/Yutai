using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmNewFeatureDataset : Form
    {
        private FeatureDatasetGeneralPage featureDatasetGeneralPage_0 = new FeatureDatasetGeneralPage();
        private IContainer icontainer_0 = null;
        private IFeatureDataset ifeatureDataset_0 = null;
        private int int_0 = 0;
        private NewDatasetCoordinateDomainPage newDatasetCoordinateDomainPage_0 = new NewDatasetCoordinateDomainPage();
        private NewDatasetSpatialRefPage newDatasetSpatialRefPage_0 = new NewDatasetSpatialRefPage();
        private NewDatasetTolerancePage newDatasetTolerancePage_0 = new NewDatasetTolerancePage();
        private VCSControlPage vcscontrolPage_0 = new VCSControlPage();

        public frmNewFeatureDataset()
        {
            this.InitializeComponent();
            NewObjectClassHelper.m_pObjectClassHelper = new NewObjectClassHelper();
            NewObjectClassHelper.m_pObjectClassHelper.HasZ = true;
            NewObjectClassHelper.m_pObjectClassHelper.HasM = true;
            this.newDatasetTolerancePage_0.chkUseDefault.CheckedChanged += new EventHandler(this.method_0);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.btnLast.Enabled = false;
                    this.featureDatasetGeneralPage_0.Visible = true;
                    this.newDatasetSpatialRefPage_0.Visible = false;
                    break;

                case 2:
                    this.newDatasetSpatialRefPage_0.Visible = true;
                    if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                    {
                        this.newDatasetCoordinateDomainPage_0.Visible = false;
                        this.btnNext.Text = "下一步";
                        break;
                    }
                    if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem)
                    {
                        this.newDatasetTolerancePage_0.Visible = false;
                        break;
                    }
                    this.vcscontrolPage_0.Visible = false;
                    break;

                case 3:
                    if (NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                    {
                        if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem)
                        {
                            this.newDatasetTolerancePage_0.Visible = true;
                            if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                            {
                                this.btnNext.Text = "下一步";
                                return;
                            }
                            this.newDatasetCoordinateDomainPage_0.Visible = false;
                            this.btnNext.Text = "下一步";
                            break;
                        }
                        this.vcscontrolPage_0.Visible = true;
                        this.newDatasetTolerancePage_0.Visible = false;
                    }
                    break;

                case 4:
                    if (!(NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem))
                    {
                        this.newDatasetTolerancePage_0.Visible = true;
                        if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                        {
                            this.newDatasetCoordinateDomainPage_0.Visible = false;
                            this.btnNext.Text = "下一步";
                        }
                    }
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (!this.featureDatasetGeneralPage_0.Apply())
                    {
                        return;
                    }
                    this.btnLast.Enabled = true;
                    this.featureDatasetGeneralPage_0.Visible = false;
                    this.newDatasetSpatialRefPage_0.Visible = true;
                    break;

                case 1:
                    if (!this.newDatasetSpatialRefPage_0.Apply())
                    {
                        return;
                    }
                    this.newDatasetSpatialRefPage_0.Visible = false;
                    if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                    {
                        this.newDatasetCoordinateDomainPage_0.Visible = true;
                        this.btnNext.Text = "完成";
                        break;
                    }
                    if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem)
                    {
                        this.newDatasetTolerancePage_0.Visible = true;
                        break;
                    }
                    this.vcscontrolPage_0.Visible = true;
                    break;

                case 2:
                    if (!NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
                    {
                        this.ifeatureDataset_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateFeatureDataset();
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                        return;
                    }
                    if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem)
                    {
                        if (!this.newDatasetTolerancePage_0.Apply())
                        {
                            return;
                        }
                        if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                        {
                            this.ifeatureDataset_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateFeatureDataset();
                            base.DialogResult = DialogResult.OK;
                            base.Close();
                            return;
                        }
                        this.newDatasetTolerancePage_0.Visible = false;
                        this.newDatasetCoordinateDomainPage_0.Visible = true;
                        this.btnNext.Text = "完成";
                        break;
                    }
                    this.vcscontrolPage_0.Apply();
                    this.vcscontrolPage_0.Visible = false;
                    this.newDatasetTolerancePage_0.Visible = true;
                    if (NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                    {
                        this.btnNext.Text = "完成";
                    }
                    break;

                case 3:
                    if (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is IUnknownCoordinateSystem)
                    {
                        if (!this.newDatasetCoordinateDomainPage_0.Apply())
                        {
                            return;
                        }
                        this.ifeatureDataset_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateFeatureDataset();
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                        break;
                    }
                    if (!this.newDatasetTolerancePage_0.Apply())
                    {
                        return;
                    }
                    if (!NewObjectClassHelper.m_pObjectClassHelper.UseDefaultDomain)
                    {
                        this.newDatasetTolerancePage_0.Visible = false;
                        this.newDatasetCoordinateDomainPage_0.Visible = true;
                        this.btnNext.Text = "完成";
                        break;
                    }
                    this.ifeatureDataset_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateFeatureDataset();
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    break;

                case 4:
                    if (this.newDatasetCoordinateDomainPage_0.Apply())
                    {
                        this.ifeatureDataset_0 = NewObjectClassHelper.m_pObjectClassHelper.CreateFeatureDataset();
                        base.DialogResult = DialogResult.OK;
                        base.Close();
                    }
                    return;
            }
            this.int_0++;
        }

        private void frmNewFeatureDataset_Load(object sender, EventArgs e)
        {
            this.newDatasetTolerancePage_0.Dock = DockStyle.Fill;
            this.newDatasetTolerancePage_0.Visible = false;
            this.panel1.Controls.Add(this.newDatasetTolerancePage_0);
            this.newDatasetCoordinateDomainPage_0.Dock = DockStyle.Fill;
            this.newDatasetCoordinateDomainPage_0.Visible = false;
            this.panel1.Controls.Add(this.newDatasetCoordinateDomainPage_0);
            this.newDatasetSpatialRefPage_0.Dock = DockStyle.Fill;
            this.newDatasetSpatialRefPage_0.Visible = false;
            this.panel1.Controls.Add(this.newDatasetSpatialRefPage_0);
            this.vcscontrolPage_0.Dock = DockStyle.Fill;
            this.vcscontrolPage_0.Visible = false;
            this.panel1.Controls.Add(this.vcscontrolPage_0);
            this.featureDatasetGeneralPage_0.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(this.featureDatasetGeneralPage_0);
            this.btnLast.Enabled = false;
        }

        private void method_0(object sender, EventArgs e)
        {
            if (this.newDatasetTolerancePage_0.chkUseDefault.Checked)
            {
                this.btnNext.Text = "完成";
            }
            else
            {
                this.btnNext.Text = "下一步";
            }
        }

        public IFeatureDataset FeatureDataset
        {
            get { return this.ifeatureDataset_0; }
        }

        public IWorkspace Workspace
        {
            set { NewObjectClassHelper.m_pObjectClassHelper.Workspace = value; }
        }
    }
}