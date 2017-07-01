using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmDataExclusion : Form
    {
        private AttributeQueryControl attributeQueryControl_0 = new AttributeQueryControl();
        private Container container_0 = null;
        private DataExclusionLegendCtrl dataExclusionLegendCtrl_0 = new DataExclusionLegendCtrl();
        private IDataExclusion idataExclusion_0 = null;
        private IGeoFeatureLayer igeoFeatureLayer_0 = null;

        public frmDataExclusion()
        {
            this.InitializeComponent();
            this.dataExclusionLegendCtrl_0.Dock = DockStyle.Fill;
            this.attributeQueryControl_0.Dock = DockStyle.Fill;
            this.TPQuery.Controls.Add(this.attributeQueryControl_0);
            this.TPLegend.Controls.Add(this.dataExclusionLegendCtrl_0);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.dataExclusionLegendCtrl_0.Apply();
            this.attributeQueryControl_0.Apply();
            this.idataExclusion_0.ExclusionClause = this.attributeQueryControl_0.WhereCaluse;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.dataExclusionLegendCtrl_0.Apply();
            this.attributeQueryControl_0.Apply();
            try
            {
                this.idataExclusion_0.ExclusionClause = this.attributeQueryControl_0.WhereCaluse;
            }
            catch
            {
            }
        }

        public IDataExclusion DataExclusion
        {
            set
            {
                this.idataExclusion_0 = value;
                this.dataExclusionLegendCtrl_0.DataExclusion = value;
                this.attributeQueryControl_0.WhereCaluse = this.idataExclusion_0.ExclusionClause;
            }
        }

        public IGeoFeatureLayer FeatureLayer
        {
            set
            {
                this.igeoFeatureLayer_0 = value;
                this.attributeQueryControl_0.CurrentLayer = value;
                this.dataExclusionLegendCtrl_0.GeometryType = this.igeoFeatureLayer_0.FeatureClass.ShapeType;
            }
        }

        public IStyleGallery StyleGallery
        {
            set { this.dataExclusionLegendCtrl_0.StyleGallery = value; }
        }
    }
}