using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class frmNewNetworkDatasetWizard : Form
    {
        private IContainer icontainer_0 = null;
        private IFeatureDataset ifeatureDataset_0 = null;
        private INetworkDataset inetworkDataset_0 = null;
        private int int_0 = 0;

        private NewNetworkDatasetAttributePropertyPage newNetworkDatasetAttributePropertyPage_0 =
            new NewNetworkDatasetAttributePropertyPage();

        private NewNetworkDatasetConnectivityPage newNetworkDatasetConnectivityPage_0 =
            new NewNetworkDatasetConnectivityPage();

        private NewNetworkDatasetDirectionPropertyPage newNetworkDatasetDirectionPropertyPage_0 =
            new NewNetworkDatasetDirectionPropertyPage();

        private NewNetworkDatasetFeatureClassSetPropertyPage newNetworkDatasetFeatureClassSetPropertyPage_0 =
            new NewNetworkDatasetFeatureClassSetPropertyPage();

        private NewNetworkDatasetModifyConnectivityPage newNetworkDatasetModifyConnectivityPage_0 =
            new NewNetworkDatasetModifyConnectivityPage();

        private NewNetworkDatasetNamePropertyPage newNetworkDatasetNamePropertyPage_0 =
            new NewNetworkDatasetNamePropertyPage();

        private NewNetworkDatasetTurnsPage newNetworkDatasetTurnsPage_0 = new NewNetworkDatasetTurnsPage();

        public frmNewNetworkDatasetWizard()
        {
            this.InitializeComponent();
            NewNetworkDatasetHelper.Init();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    return;

                case 1:
                    this.newNetworkDatasetNamePropertyPage_0.Visible = true;
                    this.newNetworkDatasetFeatureClassSetPropertyPage_0.Visible = false;
                    this.btnLast.Enabled = false;
                    break;

                case 2:
                    this.newNetworkDatasetFeatureClassSetPropertyPage_0.Visible = true;
                    this.newNetworkDatasetModifyConnectivityPage_0.Visible = false;
                    break;

                case 3:
                    this.newNetworkDatasetModifyConnectivityPage_0.Visible = true;
                    this.newNetworkDatasetTurnsPage_0.Visible = false;
                    break;

                case 4:
                    this.newNetworkDatasetTurnsPage_0.Visible = true;
                    this.newNetworkDatasetAttributePropertyPage_0.Visible = false;
                    this.btnNext.Text = "下一步";
                    break;
            }
            this.int_0--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (this.int_0)
            {
                case 0:
                    if (!this.newNetworkDatasetNamePropertyPage_0.Apply())
                    {
                        return;
                    }
                    this.newNetworkDatasetNamePropertyPage_0.Visible = false;
                    this.newNetworkDatasetFeatureClassSetPropertyPage_0.Visible = true;
                    this.btnLast.Enabled = true;
                    break;

                case 1:
                    if (!this.newNetworkDatasetFeatureClassSetPropertyPage_0.Apply())
                    {
                        return;
                    }
                    this.newNetworkDatasetFeatureClassSetPropertyPage_0.Visible = false;
                    this.newNetworkDatasetModifyConnectivityPage_0.Visible = true;
                    break;

                case 2:
                    if (!this.newNetworkDatasetModifyConnectivityPage_0.Apply())
                    {
                        return;
                    }
                    this.newNetworkDatasetModifyConnectivityPage_0.Visible = false;
                    this.newNetworkDatasetTurnsPage_0.Visible = true;
                    break;

                case 3:
                    if (!this.newNetworkDatasetTurnsPage_0.Apply())
                    {
                        return;
                    }
                    this.newNetworkDatasetTurnsPage_0.Visible = false;
                    this.newNetworkDatasetAttributePropertyPage_0.Visible = true;
                    this.btnNext.Text = "完成";
                    break;

                case 4:
                    if (this.newNetworkDatasetDirectionPropertyPage_0.Apply())
                    {
                        this.inetworkDataset_0 = NewNetworkDatasetHelper.NewNetworkDataset.CreateNetworkDataset();
                        base.DialogResult = DialogResult.OK;
                    }
                    return;
            }
            this.int_0++;
        }

        private void frmNewNetworkDatasetWizard_Load(object sender, EventArgs e)
        {
            this.newNetworkDatasetNamePropertyPage_0.Dock = DockStyle.Fill;
            this.newNetworkDatasetNamePropertyPage_0.Visible = true;
            this.panel1.Controls.Add(this.newNetworkDatasetNamePropertyPage_0);
            this.newNetworkDatasetFeatureClassSetPropertyPage_0.Dock = DockStyle.Fill;
            this.newNetworkDatasetFeatureClassSetPropertyPage_0.Visible = false;
            this.panel1.Controls.Add(this.newNetworkDatasetFeatureClassSetPropertyPage_0);
            this.newNetworkDatasetConnectivityPage_0.Dock = DockStyle.Fill;
            this.newNetworkDatasetConnectivityPage_0.Visible = false;
            this.panel1.Controls.Add(this.newNetworkDatasetConnectivityPage_0);
            this.newNetworkDatasetModifyConnectivityPage_0.Dock = DockStyle.Fill;
            this.newNetworkDatasetModifyConnectivityPage_0.Visible = false;
            this.panel1.Controls.Add(this.newNetworkDatasetModifyConnectivityPage_0);
            this.newNetworkDatasetTurnsPage_0.Dock = DockStyle.Fill;
            this.newNetworkDatasetTurnsPage_0.Visible = false;
            this.panel1.Controls.Add(this.newNetworkDatasetTurnsPage_0);
            this.newNetworkDatasetAttributePropertyPage_0.Dock = DockStyle.Fill;
            this.newNetworkDatasetAttributePropertyPage_0.Visible = false;
            this.panel1.Controls.Add(this.newNetworkDatasetAttributePropertyPage_0);
            this.newNetworkDatasetDirectionPropertyPage_0.Dock = DockStyle.Fill;
            this.newNetworkDatasetDirectionPropertyPage_0.Visible = false;
            this.panel1.Controls.Add(this.newNetworkDatasetDirectionPropertyPage_0);
        }

        public IFeatureDataset FeatureDataset
        {
            set { NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset = value; }
        }

        public INetworkDataset NetworkDataset
        {
            get { return this.inetworkDataset_0; }
        }
    }
}