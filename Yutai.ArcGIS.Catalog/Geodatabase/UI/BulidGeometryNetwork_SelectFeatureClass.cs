using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class BulidGeometryNetwork_SelectFeatureClass : UserControl
    {
        private bool bool_0 = true;
        private Container container_0 = null;

        public BulidGeometryNetwork_SelectFeatureClass()
        {
            this.InitializeComponent();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkListUseFeatureClass.Items.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap = this.chkListUseFeatureClass.Items[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
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
                BulidGeometryNetworkHelper.FeatureClassWrap wrap = this.chkListUseFeatureClass.Items[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if (!wrap.IsUse)
                {
                    this.chkListUseFeatureClass.SetItemChecked(i, true);
                    wrap.IsUse = true;
                }
            }
        }

        private void BulidGeometryNetwork_SelectFeatureClass_Load(object sender, EventArgs e)
        {
            string[] strArray = BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Name.Split(new char[] { '.' });
            string str = strArray[strArray.Length - 1];
            BulidGeometryNetworkHelper.BulidGNHelper.Name = str + "_Net";
            if (BulidGeometryNetworkHelper.BulidGNHelper.IsEmpty)
            {
                this.panelEmpty.Visible = true;
                this.panelNotEmpty.Visible = false;
                this.txtGNName1.Text = str + "_Net";
            }
            else
            {
                this.panelEmpty.Visible = false;
                this.panelNotEmpty.Visible = true;
                if (BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Subsets != null)
                {
                    this.txtGNName.Text = str + "_Net";
                    IEnumDataset subsets = BulidGeometryNetworkHelper.BulidGNHelper.FeatureDataset.Subsets;
                    if (subsets != null)
                    {
                        subsets.Reset();
                        for (IDataset dataset2 = subsets.Next(); dataset2 != null; dataset2 = subsets.Next())
                        {
                            if (((dataset2 is IFeatureClass) && ((dataset2 as IFeatureClass).FeatureType == esriFeatureType.esriFTSimple)) && (((dataset2 as IFeatureClass).ShapeType == esriGeometryType.esriGeometryPolyline) || ((dataset2 as IFeatureClass).ShapeType == esriGeometryType.esriGeometryPoint)))
                            {
                                BulidGeometryNetworkHelper.FeatureClassWrap wrap;
                                int index = (dataset2 as IFeatureClass).Fields.FindField("Enabled");
                                if (index != -1)
                                {
                                    IField field = (dataset2 as IFeatureClass).Fields.get_Field(index);
                                    if ((field.Type == esriFieldType.esriFieldTypeSmallInteger) || (field.Type == esriFieldType.esriFieldTypeInteger))
                                    {
                                        wrap = new BulidGeometryNetworkHelper.FeatureClassWrap(dataset2 as IFeatureClass);
                                        BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Add(wrap);
                                        this.chkListUseFeatureClass.Items.Add(wrap, wrap.IsUse);
                                    }
                                }
                                else
                                {
                                    wrap = new BulidGeometryNetworkHelper.FeatureClassWrap(dataset2 as IFeatureClass);
                                    BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Add(wrap);
                                    this.chkListUseFeatureClass.Items.Add(wrap, wrap.IsUse);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void chkListUseFeatureClass_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BulidGeometryNetworkHelper.FeatureClassWrap wrap = this.chkListUseFeatureClass.Items[e.Index] as BulidGeometryNetworkHelper.FeatureClassWrap;
            if (e.NewValue == CheckState.Checked)
            {
                wrap.IsUse = true;
            }
            else
            {
                wrap.IsUse = false;
            }
        }

 private void method_0(object sender, EventArgs e)
        {
        }

        private void txtGNName_EditValueChanged(object sender, EventArgs e)
        {
            BulidGeometryNetworkHelper.BulidGNHelper.Name = this.txtGNName.Text;
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.txtGNName1.Text = this.txtGNName.Text;
                this.bool_0 = true;
            }
        }

        private void txtGNName1_EditValueChanged(object sender, EventArgs e)
        {
            BulidGeometryNetworkHelper.BulidGNHelper.Name = this.txtGNName1.Text;
            if (this.bool_0)
            {
                this.bool_0 = false;
                this.txtGNName.Text = this.txtGNName1.Text;
                this.bool_0 = true;
            }
        }
    }
}

