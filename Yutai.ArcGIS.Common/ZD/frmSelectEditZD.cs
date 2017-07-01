using System.ComponentModel;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Common.ZD
{
    public partial class frmSelectEditZD : System.Windows.Forms.Form
    {
        private IContainer icontainer_0 = null;


        private System.Collections.Generic.List<IFeatureLayer> list_0;

        public System.Collections.Generic.List<IFeatureLayer> FeatureLayers { get; set; }

        public IFeatureLayer SelectFeatureLayer { get; protected set; }

        public frmSelectEditZD()
        {
            this.InitializeComponent();
        }

        private void frmSelectEditZD_Load(object sender, System.EventArgs e)
        {
            foreach (IFeatureLayer current in this.FeatureLayers)
            {
                this.comboBox1.Items.Add(new LayerObject(current));
            }
            this.comboBox1.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("请选择要编辑的宗地图层!");
            }
            else
            {
                this.SelectFeatureLayer = ((this.comboBox1.SelectedItem as LayerObject).Layer as IFeatureLayer);
                base.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}