using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal partial class WeightPropertyPage : UserControl
    {
        private Container container_0 = null;
        private IGeometricNetwork igeometricNetwork_0 = null;

        public WeightPropertyPage()
        {
            this.InitializeComponent();
        }

 private string method_0(esriWeightType esriWeightType_0)
        {
            switch (esriWeightType_0)
            {
                case esriWeightType.esriWTNull:
                    return "空";

                case esriWeightType.esriWTBitGate:
                    return "位门";

                case esriWeightType.esriWTInteger:
                    return "长整型";

                case esriWeightType.esriWTSingle:
                    return "单精度";

                case esriWeightType.esriWTDouble:
                    return "双精度";

                case esriWeightType.esriWTBoolean:
                    return "布尔型";
            }
            return "";
        }

        private void WeightPropertyPage_Load(object sender, EventArgs e)
        {
            INetwork network = this.igeometricNetwork_0.Network;
            string[] items = new string[2];
            for (int i = 0; i < (network as INetSchema).WeightCount; i++)
            {
                INetWeight weight = (network as INetSchema).get_Weight(i);
                items[0] = weight.WeightName;
                items[1] = this.method_0(weight.WeightType);
                ListViewItem item = new ListViewItem(items);
                this.listView1.Items.Add(item);
            }
        }

        public IGeometricNetwork GeometricNetwork
        {
            set
            {
                this.igeometricNetwork_0 = value;
            }
        }
    }
}

