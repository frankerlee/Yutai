using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmLayerProperty : Form
    {
        private Container container_0 = null;
        private JoiningAndRelatingPropertyPage joiningAndRelatingPropertyPage_0 = new JoiningAndRelatingPropertyPage();
        private LayerDefinitionExpressionCtrl layerDefinitionExpressionCtrl_0 = new LayerDefinitionExpressionCtrl();
        private LayerDisplaySetCtrl layerDisplaySetCtrl_0 = new LayerDisplaySetCtrl();
        private LayerGeneralPropertyCtrl layerGeneralPropertyCtrl_0 = new LayerGeneralPropertyCtrl();
        private LayerLabelPropertyCtrl layerLabelPropertyCtrl_0 = new LayerLabelPropertyCtrl();

        public frmLayerProperty()
        {
            this.InitializeComponent();
            this.layerGeneralPropertyCtrl_0.Dock = DockStyle.Fill;
            this.tabPageGeneral.Controls.Add(this.layerGeneralPropertyCtrl_0);
            this.layerDisplaySetCtrl_0.Dock = DockStyle.Fill;
            this.tabPageDisplay.Controls.Add(this.layerDisplaySetCtrl_0);
            this.layerDefinitionExpressionCtrl_0.Dock = DockStyle.Fill;
            this.tabPageDefinitionExpression.Controls.Add(this.layerDefinitionExpressionCtrl_0);
            this.layerLabelPropertyCtrl_0.Dock = DockStyle.Fill;
            this.tabPageLayerLabel.Controls.Add(this.layerLabelPropertyCtrl_0);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.layerGeneralPropertyCtrl_0.Apply();
            this.layerDefinitionExpressionCtrl_0.Apply();
            this.layerDisplaySetCtrl_0.Apply();
            this.layerLabelPropertyCtrl_0.Apply();
            base.Close();
        }

 private void frmLayerProperty_Load(object sender, EventArgs e)
        {
        }

 public ILayer Layer
        {
            set
            {
                this.layerGeneralPropertyCtrl_0.Layer = value;
                this.layerDefinitionExpressionCtrl_0.FeatureLayerDefinition = value as IFeatureLayerDefinition;
                this.layerDisplaySetCtrl_0.Layer = value;
                this.layerLabelPropertyCtrl_0.GeoFeatureLayer = value as IGeoFeatureLayer;
            }
        }
    }
}

