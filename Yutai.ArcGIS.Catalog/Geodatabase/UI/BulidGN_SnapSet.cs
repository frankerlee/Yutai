using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGN_SnapSet : UserControl
    {
        private bool bool_0 = false;
        private Container container_0 = null;

        public BulidGN_SnapSet()
        {
            this.InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.bool_0 = false;
            for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap = BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (wrap.IsUse && wrap.canChangeGeometry)
                {
                    wrap.canChangeGeometry = false;
                    this.chkChangeFC.SetItemChecked(i, false);
                }
            }
            this.bool_0 = true;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.bool_0 = false;
            for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap = BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (wrap.IsUse && !wrap.canChangeGeometry)
                {
                    wrap.canChangeGeometry = true;
                    this.chkChangeFC.SetItemChecked(i, true);
                }
            }
            this.bool_0 = true;
        }

        private void BulidGN_SnapSet_Load(object sender, EventArgs e)
        {
            if (BulidGeometryNetworkHelper.BulidGNHelper.IsSnap)
            {
                this.radioGroup1.SelectedIndex = 1;
            }
            else
            {
                this.radioGroup1.SelectedIndex = 0;
                this.groupBox2.Enabled = false;
            }
            for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap item = BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (item.IsUse)
                {
                    this.chkChangeFC.Items.Add(item, item.canChangeGeometry);
                }
            }
            this.txtSnaptol.Text = BulidGeometryNetworkHelper.BulidGNHelper.SnapTolerance.ToString();
            this.bool_0 = true;
        }

        public bool CanNext()
        {
            if (this.radioGroup1.SelectedIndex == 1)
            {
                if (BulidGeometryNetworkHelper.BulidGNHelper.SnapTolerance < BulidGeometryNetworkHelper.BulidGNHelper.MinSnapTolerance)
                {
                    MessageBox.Show("请输入大于或等于最小捕捉容差:" + BulidGeometryNetworkHelper.BulidGNHelper.MinSnapTolerance.ToString() + "的数字!");
                    return false;
                }
                if (this.chkChangeFC.CheckedItems.Count == 0)
                {
                    MessageBox.Show("至少要选择一个要捕捉的要素类!");
                    return false;
                }
            }
            return true;
        }

        private void chkChangeFC_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (this.bool_0)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap = BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[e.Index] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (wrap.IsUse)
                {
                    wrap.canChangeGeometry = e.NewValue == CheckState.Checked;
                }
            }
        }

 private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.groupBox2.Enabled = this.radioGroup1.SelectedIndex == 1;
            if (this.bool_0)
            {
                BulidGeometryNetworkHelper.BulidGNHelper.IsSnap = this.radioGroup1.SelectedIndex == 1;
            }
        }

        private void txtSnaptol_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                try
                {
                    BulidGeometryNetworkHelper.BulidGNHelper.SnapTolerance = double.Parse(this.txtSnaptol.Text);
                }
                catch
                {
                }
            }
        }
    }
}

