using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.UI
{
    public partial class NewNetworkDatasetFeatureClassSetPropertyPage : UserControl
    {
        private IContainer icontainer_0 = null;

        public NewNetworkDatasetFeatureClassSetPropertyPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            for (int i = 0; i < this.chkListUseFeatureClass.Items.Count; i++)
            {
                NewNetworkDatasetHelper.FeatureClassWrap wrap = this.chkListUseFeatureClass.Items[i] as NewNetworkDatasetHelper.FeatureClassWrap;
                if (wrap.IsUse)
                {
                    return true;
                }
            }
            MessageBox.Show("请选择要参与到网络要素集中的要素类！");
            return false;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkListUseFeatureClass.Items.Count; i++)
            {
                NewNetworkDatasetHelper.FeatureClassWrap wrap = this.chkListUseFeatureClass.Items[i] as NewNetworkDatasetHelper.FeatureClassWrap;
                if (wrap.IsUse)
                {
                    this.chkListUseFeatureClass.SetItemChecked(i, false);
                    wrap.IsUse = false;
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkListUseFeatureClass.Items.Count; i++)
            {
                NewNetworkDatasetHelper.FeatureClassWrap wrap = this.chkListUseFeatureClass.Items[i] as NewNetworkDatasetHelper.FeatureClassWrap;
                if (!wrap.IsUse)
                {
                    this.chkListUseFeatureClass.SetItemChecked(i, true);
                    wrap.IsUse = true;
                }
            }
        }

        private void chkListUseFeatureClass_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            NewNetworkDatasetHelper.FeatureClassWrap wrap = this.chkListUseFeatureClass.Items[e.Index] as NewNetworkDatasetHelper.FeatureClassWrap;
            if (e.NewValue == CheckState.Checked)
            {
                wrap.IsUse = true;
            }
            else
            {
                wrap.IsUse = false;
            }
        }

 private void NewNetworkDatasetFeatureClassSetPropertyPage_Load(object sender, EventArgs e)
        {
            try
            {
                if (NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset.Subsets != null)
                {
                    IEnumDataset subsets = NewNetworkDatasetHelper.NewNetworkDataset.FeatureDataset.Subsets;
                    if (subsets != null)
                    {
                        subsets.Reset();
                        for (IDataset dataset2 = subsets.Next(); dataset2 != null; dataset2 = subsets.Next())
                        {
                            if (((dataset2 is IFeatureClass) && ((dataset2 as IFeatureClass).FeatureType == esriFeatureType.esriFTSimple)) && (((dataset2 as IFeatureClass).ShapeType == esriGeometryType.esriGeometryPolyline) || ((dataset2 as IFeatureClass).ShapeType == esriGeometryType.esriGeometryPoint)))
                            {
                                NewNetworkDatasetHelper.FeatureClassWrap item = new NewNetworkDatasetHelper.FeatureClassWrap(dataset2 as IFeatureClass);
                                NewNetworkDatasetHelper.NewNetworkDataset.FeatureClassWraps.Add(item);
                                this.chkListUseFeatureClass.Items.Add(item, item.IsUse);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}

