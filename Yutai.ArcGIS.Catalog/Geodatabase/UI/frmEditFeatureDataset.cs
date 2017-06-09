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
    public class frmEditFeatureDataset : Form
    {
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private CoordinateControl coordinateControl_0 = new CoordinateControl();
        private FeatureDatasetGeneralPage featureDatasetGeneralPage_0 = new FeatureDatasetGeneralPage();
        private IContainer icontainer_0 = null;
        private NewDatasetCoordinateDomainPage newDatasetCoordinateDomainPage_0 = new NewDatasetCoordinateDomainPage();
        private NewDatasetSpatialRefPage newDatasetSpatialRefPage_0 = new NewDatasetSpatialRefPage();
        private NewDatasetTolerancePage newDatasetTolerancePage_0 = new NewDatasetTolerancePage();
        private TabControl tabControl1;
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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditFeatureDataset));
            this.btnCancel = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.tabControl1 = new TabControl();
            base.SuspendLayout();
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x11e, 0x1e6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnApply.Location = new System.Drawing.Point(0x169, 0x1e6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x38, 0x18);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnOK.Location = new System.Drawing.Point(210, 0x1e6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.tabControl1.Location = new System.Drawing.Point(8, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1af, 0x1d4);
            this.tabControl1.TabIndex = 10;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1c3, 0x20a);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmEditFeatureDataset";
            this.Text = "要素集属性";
            base.Load += new EventHandler(this.frmEditFeatureDataset_Load);
            base.ResumeLayout(false);
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

