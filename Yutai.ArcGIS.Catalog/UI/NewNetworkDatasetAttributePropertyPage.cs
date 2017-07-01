using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class NewNetworkDatasetAttributePropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public NewNetworkDatasetAttributePropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            NewNetworkDatasetHelper.NewNetworkDataset.NetworkDirections = null;
            bool flag = NewNetworkDatasetHelper.NewNetworkDataset.HasLineFeatureClass();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                ListViewItem item = this.listView1.Items[i];
                if (flag && ((item.Tag as INetworkAttribute).UsageType == esriNetworkAttributeUsageType.esriNAUTCost))
                {
                    NewNetworkDatasetHelper.NewNetworkDataset.CreateDirection((item.Tag as INetworkAttribute).Name);
                }
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewNetworkAttribute attribute = new frmNewNetworkAttribute();
            if (attribute.ShowDialog() == DialogResult.OK)
            {
                string[] items = new string[]
                {
                    "", "", attribute.NetworkAttribute.Name,
                    CommonHelper.GetUsageTypeDescriptor(attribute.NetworkAttribute.UsageType),
                    CommonHelper.GetNetworkUnitTypeDescriptor(attribute.NetworkAttribute.Units),
                    CommonHelper.GetDataTypeDescriptor(attribute.NetworkAttribute.DataType)
                };
                INetworkConstantEvaluator evaluator = new NetworkConstantEvaluatorClass();
                IEvaluatedNetworkAttribute networkAttribute = attribute.NetworkAttribute as IEvaluatedNetworkAttribute;
                evaluator.ConstantValue = 0;
                networkAttribute.set_DefaultEvaluator(esriNetworkElementType.esriNETEdge, evaluator as INetworkEvaluator);
                networkAttribute.set_DefaultEvaluator(esriNetworkElementType.esriNETJunction,
                    evaluator as INetworkEvaluator);
                networkAttribute.set_DefaultEvaluator(esriNetworkElementType.esriNETTurn, evaluator as INetworkEvaluator);
                ListViewItem item = new ListViewItem(items)
                {
                    Tag = attribute.NetworkAttribute
                };
                this.listView1.Items.Add(item);
                NewNetworkDatasetHelper.NewNetworkDataset.Attributes.Add(attribute.NetworkAttribute);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.SelectedIndices.Count - 1; i >= 0; i--)
            {
                int index = this.listView1.SelectedIndices[i];
                this.listView1.Items.RemoveAt(index);
                NewNetworkDatasetHelper.NewNetworkDataset.Attributes.Remove(index);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
            {
                this.listView1.Items.RemoveAt(i);
                NewNetworkDatasetHelper.NewNetworkDataset.Attributes.Remove(i);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = this.listView1.SelectedItems.Count > 0;
        }
    }
}