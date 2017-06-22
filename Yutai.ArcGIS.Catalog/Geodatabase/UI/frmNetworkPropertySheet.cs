using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmNetworkPropertySheet : Form
    {
        private IContainer icontainer_0 = null;
        private INetworkDataset inetworkDataset_0 = null;

        public frmNetworkPropertySheet()
        {
            this.InitializeComponent();
        }

 private void frmNetworkPropertySheet_Load(object sender, EventArgs e)
        {
            int num2;
            string[] strArray;
            ListViewItem item;
            this.lblNetworkName.Text = (this.inetworkDataset_0 as IDataset).Name;
            this.lblNetworkType.Text = this.method_0(this.inetworkDataset_0.NetworkType);
            INetworkQuery query = this.inetworkDataset_0 as INetworkQuery;
            string str = "";
            str = ((query.get_ElementCount(esriNetworkElementType.esriNETJunction).ToString() + "个连接点\r\n") + query.get_ElementCount(esriNetworkElementType.esriNETEdge).ToString() + "条边\r\n") + query.get_ElementCount(esriNetworkElementType.esriNETTurn).ToString() + "个转向\r\n";
            this.lblNetworkElements.Text = str;
            for (num2 = 0; num2 < this.inetworkDataset_0.SourceCount; num2++)
            {
                strArray = new string[3];
                INetworkSource source = this.inetworkDataset_0.get_Source(num2);
                strArray[0] = source.Name;
                strArray[1] = this.method_1(source.SourceType);
                strArray[2] = this.method_2(source.ElementType);
                item = new ListViewItem(strArray);
                this.lsvSource.Items.Add(item);
                if (source.SourceType == esriNetworkSourceType.esriNSTEdgeFeature)
                {
                    strArray[0] = source.Name;
                    strArray[1] = "From End";
                    strArray[2] = (source as IEdgeFeatureSource).FromElevationFieldName;
                    item = new ListViewItem(strArray);
                    this.lsvElevation.Items.Add(item);
                    strArray[1] = "To End";
                    strArray[2] = (source as IEdgeFeatureSource).ToElevationFieldName;
                    item = new ListViewItem(strArray);
                    this.lsvElevation.Items.Add(item);
                    if (source.NetworkSourceDirections == null)
                    {
                    }
                }
                else if (source.SourceType == esriNetworkSourceType.esriNSTJunctionFeature)
                {
                    strArray[0] = source.Name;
                    strArray[1] = "";
                    strArray[2] = (source as IJunctionFeatureSource).ElevationFieldName;
                    item = new ListViewItem(strArray);
                    this.lsvElevation.Items.Add(item);
                }
            }
            for (num2 = 0; num2 < this.inetworkDataset_0.AttributeCount; num2++)
            {
                strArray = new string[6];
                INetworkAttribute attribute = this.inetworkDataset_0.get_Attribute(num2);
                strArray[0] = "";
                strArray[1] = "";
                strArray[2] = attribute.Name;
                strArray[3] = CommonHelper.GetUsageTypeDescriptor(attribute.UsageType);
                strArray[4] = CommonHelper.GetNetworkUnitTypeDescriptor(attribute.Units);
                strArray[5] = CommonHelper.GetDataTypeDescriptor(attribute.DataType);
                item = new ListViewItem(strArray);
                this.lsvAttributes.Items.Add(item);
            }
        }

 private string method_0(esriNetworkDatasetType esriNetworkDatasetType_0)
        {
            switch (esriNetworkDatasetType_0)
            {
                case esriNetworkDatasetType.esriNDTGeodatabase:
                    return "Geodatabase-based network dataset";

                case esriNetworkDatasetType.esriNDTShapefile:
                    return "Shapefile-based network dataset";

                case esriNetworkDatasetType.esriNDTSDC:
                    return "SDC-based network dataset";
            }
            return "The network dataset type is unknown";
        }

        private string method_1(esriNetworkSourceType esriNetworkSourceType_0)
        {
            switch (esriNetworkSourceType_0)
            {
                case esriNetworkSourceType.esriNSTSystemJunction:
                    return "SystemJunction";

                case esriNetworkSourceType.esriNSTJunctionFeature:
                    return "JunctionFeature";

                case esriNetworkSourceType.esriNSTEdgeFeature:
                    return "EdgeFeature";

                case esriNetworkSourceType.esriNSTTurnFeature:
                    return "TurnFeature";
            }
            return "NetworkSource";
        }

        private string method_2(esriNetworkElementType esriNetworkElementType_0)
        {
            switch (esriNetworkElementType_0)
            {
                case esriNetworkElementType.esriNETJunction:
                    return "Junction";

                case esriNetworkElementType.esriNETEdge:
                    return "Edge";
            }
            return "Turn";
        }

        public INetworkDataset NetworkDataset
        {
            set
            {
                this.inetworkDataset_0 = value;
            }
        }
    }
}

