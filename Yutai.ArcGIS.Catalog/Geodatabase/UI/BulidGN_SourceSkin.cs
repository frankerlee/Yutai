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
    public partial class BulidGN_SourceSkin : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;

        public BulidGN_SourceSkin()
        {
            this.InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkChangeFC.Items.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap1 =
                    this.chkChangeFC.Items[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                this.chkChangeFC.SetItemChecked(i, false);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkChangeFC.Items.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap1 =
                    this.chkChangeFC.Items[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                this.chkChangeFC.SetItemChecked(i, true);
            }
        }

        private void BulidGN_SourceSkin_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap item =
                    BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[i] as
                        BulidGeometryNetworkHelper.FeatureClassWrap;
                if ((item.GeometryType == esriGeometryType.esriGeometryPoint) && item.IsUse)
                {
                    this.chkChangeFC.Items.Add(item,
                        item.NetworkClassAncillaryRole == esriNetworkClassAncillaryRole.esriNCARSourceSink);
                }
            }
            this.bool_0 = true;
        }

        private void chkChangeFC_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (this.bool_0)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap =
                    this.chkChangeFC.Items[e.Index] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (e.NewValue == CheckState.Checked)
                {
                    wrap.NetworkClassAncillaryRole = esriNetworkClassAncillaryRole.esriNCARSourceSink;
                }
                else
                {
                    wrap.NetworkClassAncillaryRole = esriNetworkClassAncillaryRole.esriNCARNone;
                }
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.chkChangeFC.Enabled = false;
                this.btnSelectAll.Enabled = false;
                this.btnClear.Enabled = false;
            }
            else
            {
                this.chkChangeFC.Enabled = true;
                this.btnSelectAll.Enabled = true;
                this.btnClear.Enabled = true;
            }
        }
    }
}