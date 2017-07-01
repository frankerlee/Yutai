using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmFeatureDataset : Form
    {
        private Container container_0 = null;
        private FeatureDatasetControl featureDatasetControl_0 = new FeatureDatasetControl();

        public frmFeatureDataset()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.featureDatasetControl_0.Apply();
                base.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误");
            }
        }

        private void frmFeatureDataset_Load(object sender, EventArgs e)
        {
            this.panel1.Controls.Add(this.featureDatasetControl_0);
        }

        private void method_0()
        {
        }

        public IFeatureDataset FeatureDataset
        {
            get { return this.featureDatasetControl_0.FeatureDataset; }
            set { this.featureDatasetControl_0.FeatureDataset = value; }
        }

        public IFeatureWorkspace FeatureWorkspace
        {
            set { this.featureDatasetControl_0.FeatureWorkspace = value; }
        }

        public bool IsEdit
        {
            set { this.featureDatasetControl_0.IsEdit = value; }
        }
    }
}