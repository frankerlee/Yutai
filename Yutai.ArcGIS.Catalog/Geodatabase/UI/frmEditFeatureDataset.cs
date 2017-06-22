using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmEditFeatureDataset : Form
    {
        private CoordinateControl coordinateControl_0 = new CoordinateControl();
        private FeatureDatasetGeneralPage featureDatasetGeneralPage_0 = new FeatureDatasetGeneralPage();
        private IContainer icontainer_0 = null;
        private NewDatasetCoordinateDomainPage newDatasetCoordinateDomainPage_0 = new NewDatasetCoordinateDomainPage();
        private NewDatasetSpatialRefPage newDatasetSpatialRefPage_0 = new NewDatasetSpatialRefPage();
        private NewDatasetTolerancePage newDatasetTolerancePage_0 = new NewDatasetTolerancePage();
        private VCSCoordinateInfoPage vcscoordinateInfoPage_0 = new VCSCoordinateInfoPage();

        public frmEditFeatureDataset()
        {
            this.InitializeComponent();
            NewObjectClassHelper.m_pObjectClassHelper = new NewObjectClassHelper();
            NewObjectClassHelper.m_pObjectClassHelper.IsEdit = true;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.btnApply.Enabled = false;
            this.method_2();
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

 private void frmEditFeatureDataset_Load(object sender, EventArgs e)
        {
            this.btnApply.Enabled = false;
            TabPage page = new TabPage("常规");
            this.featureDatasetGeneralPage_0.Dock = DockStyle.Fill;
            page.Controls.Add(this.featureDatasetGeneralPage_0);
            this.tabControl1.TabPages.Add(page);
            this.featureDatasetGeneralPage_0.ValueChanged += new ValueChangedHandler(this.method_0);
            if (NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision)
            {
                page = new TabPage("容差");
                this.newDatasetTolerancePage_0.Dock = DockStyle.Fill;
                page.Controls.Add(this.newDatasetTolerancePage_0);
                this.tabControl1.TabPages.Add(page);
            }
            page = new TabPage("XY坐标系统");
            this.coordinateControl_0.Dock = DockStyle.Fill;
            this.coordinateControl_0.IsEdit = true;
            this.coordinateControl_0.SpatialRefrence = NewObjectClassHelper.m_pObjectClassHelper.SpatialReference;
            page.Controls.Add(this.coordinateControl_0);
            this.tabControl1.TabPages.Add(page);
            this.coordinateControl_0.ValueChanged += new ValueChangedHandler(this.method_1);
            if ((NewObjectClassHelper.m_pObjectClassHelper.IsHighPrecision && (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference is ISpatialReference3)) && ((NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem != null))
            {
                page = new TabPage("Z坐标系统");
                this.vcscoordinateInfoPage_0.Dock = DockStyle.Fill;
                this.vcscoordinateInfoPage_0.IsEdit = false;
                this.vcscoordinateInfoPage_0.SpatialReference = (NewObjectClassHelper.m_pObjectClassHelper.SpatialReference as ISpatialReference3).VerticalCoordinateSystem;
                page.Controls.Add(this.vcscoordinateInfoPage_0);
                this.tabControl1.TabPages.Add(page);
            }
            page = new TabPage("精度和域");
            this.newDatasetCoordinateDomainPage_0.Dock = DockStyle.Fill;
            page.Controls.Add(this.newDatasetCoordinateDomainPage_0);
            this.tabControl1.TabPages.Add(page);
        }

 private void method_0(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
        }

        private void method_1(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
        }

        private bool method_2()
        {
            COMException exception;
            Exception exception2;
            this.featureDatasetGeneralPage_0.Apply();
            IDataset featureDataset = NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset;
            try
            {
                if (featureDataset.Name != NewObjectClassHelper.m_pObjectClassHelper.Name)
                {
                    featureDataset.Rename(NewObjectClassHelper.m_pObjectClassHelper.Name);
                }
            }
            catch (COMException exception1)
            {
                exception = exception1;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("",exception, "");
                NewObjectClassHelper.m_pObjectClassHelper.Name = featureDataset.Name;
            }
            catch (Exception exception3)
            {
                exception2 = exception3;
                Logger.Current.Error("",exception2, "");
            }
            try
            {
                if (this.coordinateControl_0.IsDirty)
                {
                    IGeoDatasetSchemaEdit edit = featureDataset as IGeoDatasetSchemaEdit;
                    if (edit.CanAlterSpatialReference)
                    {
                        edit.AlterSpatialReference(this.coordinateControl_0.SpatialRefrence);
                    }
                    this.coordinateControl_0.Apply();
                }
            }
            catch (COMException exception4)
            {
                exception = exception4;
                MessageBox.Show(exception.Message);
                Logger.Current.Error("",exception, "");
            }
            catch (Exception exception5)
            {
                exception2 = exception5;
                Logger.Current.Error("",exception2, "");
            }
            return true;
        }

        public IFeatureDataset FeatureDataset
        {
            set
            {
                NewObjectClassHelper.m_pObjectClassHelper.FeatureDataset = value;
                NewObjectClassHelper.m_pObjectClassHelper.HasZ = true;
                NewObjectClassHelper.m_pObjectClassHelper.HasM= true;
            }
        }
    }
}

