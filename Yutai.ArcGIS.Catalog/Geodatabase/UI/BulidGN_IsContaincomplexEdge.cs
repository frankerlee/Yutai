using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGN_IsContaincomplexEdge : UserControl
    {
        private Container container_0 = null;

        public BulidGN_IsContaincomplexEdge()
        {
            this.InitializeComponent();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap =
                    this.checkedListBox1.Items[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (wrap.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    this.checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap =
                    this.checkedListBox1.Items[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (wrap.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    this.checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void BulidGN_IsContaincomplexEdge_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap item =
                    BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[i] as
                        BulidGeometryNetworkHelper.FeatureClassWrap;
                if (item.IsUse && (item.GeometryType == esriGeometryType.esriGeometryPolyline))
                {
                    this.checkedListBox1.Items.Add(item, item.FeatureType == esriFeatureType.esriFTComplexEdge);
                }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            BulidGeometryNetworkHelper.FeatureClassWrap wrap =
                this.checkedListBox1.Items[e.Index] as BulidGeometryNetworkHelper.FeatureClassWrap;
            if (e.NewValue == CheckState.Checked)
            {
                wrap.FeatureType = esriFeatureType.esriFTComplexEdge;
            }
            else
            {
                wrap.FeatureType = esriFeatureType.esriFTSimpleEdge;
            }
        }

        private void rdoIsContainNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.rdoIsContainNet.SelectedIndex == 0)
            {
                this.groupBox2.Enabled = false;
            }
            else
            {
                this.groupBox2.Enabled = true;
            }
        }
    }
}