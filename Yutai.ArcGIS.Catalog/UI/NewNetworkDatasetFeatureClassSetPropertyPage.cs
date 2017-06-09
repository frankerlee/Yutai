using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.UI
{
    public class NewNetworkDatasetFeatureClassSetPropertyPage : UserControl
    {
        private SimpleButton btnClearAll;
        private SimpleButton btnSelectAll;
        private CheckedListBox chkListUseFeatureClass;
        private IContainer icontainer_0 = null;
        private Label label1;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.btnClearAll = new SimpleButton();
            this.btnSelectAll = new SimpleButton();
            this.chkListUseFeatureClass = new CheckedListBox();
            this.label1 = new Label();
            base.SuspendLayout();
            this.btnClearAll.Location = new System.Drawing.Point(0xf3, 0x3b);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(40, 0x18);
            this.btnClearAll.TabIndex = 13;
            this.btnClearAll.Text = "清除";
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnSelectAll.Location = new System.Drawing.Point(0xf3, 0x1d);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 0x18);
            this.btnSelectAll.TabIndex = 12;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.chkListUseFeatureClass.Location = new System.Drawing.Point(11, 0x1d);
            this.chkListUseFeatureClass.Name = "chkListUseFeatureClass";
            this.chkListUseFeatureClass.Size = new Size(0xe2, 0xc4);
            this.chkListUseFeatureClass.TabIndex = 11;
            this.chkListUseFeatureClass.ItemCheck += new ItemCheckEventHandler(this.chkListUseFeatureClass_ItemCheck);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xad, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "选择要参与网络要素集的要素类";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.chkListUseFeatureClass);
            base.Controls.Add(this.label1);
            base.Name = "NewNetworkDatasetFeatureClassSetPropertyPage";
            base.Size = new Size(0x142, 0x109);
            base.Load += new EventHandler(this.NewNetworkDatasetFeatureClassSetPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
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

