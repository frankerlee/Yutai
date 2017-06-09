using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class BulidGeometryNetwork_SelectFeatureClass : UserControl
    {
        private bool bool_0 = true;
        private SimpleButton btnClearAll;
        private SimpleButton btnSelectAll;
        private CheckedListBox chkListUseFeatureClass;
        private Container container_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panelEmpty;
        private Panel panelNotEmpty;
        private TextEdit txtGNName;
        private TextEdit txtGNName1;

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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.panelNotEmpty = new Panel();
            this.txtGNName = new TextEdit();
            this.label2 = new Label();
            this.btnClearAll = new SimpleButton();
            this.btnSelectAll = new SimpleButton();
            this.chkListUseFeatureClass = new CheckedListBox();
            this.label1 = new Label();
            this.panelEmpty = new Panel();
            this.txtGNName1 = new TextEdit();
            this.label3 = new Label();
            this.panelNotEmpty.SuspendLayout();
            this.txtGNName.Properties.BeginInit();
            this.panelEmpty.SuspendLayout();
            this.txtGNName1.Properties.BeginInit();
            base.SuspendLayout();
            this.panelNotEmpty.Controls.Add(this.txtGNName);
            this.panelNotEmpty.Controls.Add(this.label2);
            this.panelNotEmpty.Controls.Add(this.btnClearAll);
            this.panelNotEmpty.Controls.Add(this.btnSelectAll);
            this.panelNotEmpty.Controls.Add(this.chkListUseFeatureClass);
            this.panelNotEmpty.Controls.Add(this.label1);
            this.panelNotEmpty.Controls.Add(this.panelEmpty);
            this.panelNotEmpty.Location = new System.Drawing.Point(0, 0);
            this.panelNotEmpty.Name = "panelNotEmpty";
            this.panelNotEmpty.Size = new Size(0x150, 280);
            this.panelNotEmpty.TabIndex = 11;
            this.txtGNName.EditValue = "";
            this.txtGNName.Location = new System.Drawing.Point(0x10, 0xd0);
            this.txtGNName.Name = "txtGNName";
            this.txtGNName.Size = new Size(240, 0x15);
            this.txtGNName.TabIndex = 11;
            this.txtGNName.EditValueChanged += new EventHandler(this.txtGNName_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x18, 0xb0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4d, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "几何网络名称";
            this.btnClearAll.Location = new System.Drawing.Point(240, 0x60);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(40, 0x18);
            this.btnClearAll.TabIndex = 9;
            this.btnClearAll.Text = "取消";
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnSelectAll.Location = new System.Drawing.Point(240, 0x38);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 0x18);
            this.btnSelectAll.TabIndex = 8;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.chkListUseFeatureClass.Location = new System.Drawing.Point(0x10, 0x20);
            this.chkListUseFeatureClass.Name = "chkListUseFeatureClass";
            this.chkListUseFeatureClass.Size = new Size(0xd0, 0x84);
            this.chkListUseFeatureClass.TabIndex = 7;
            this.chkListUseFeatureClass.ItemCheck += new ItemCheckEventHandler(this.chkListUseFeatureClass_ItemCheck);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xa1, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "选择要创建几何网络的要素类";
            this.panelEmpty.Controls.Add(this.txtGNName1);
            this.panelEmpty.Controls.Add(this.label3);
            this.panelEmpty.Location = new System.Drawing.Point(0, 0x18);
            this.panelEmpty.Name = "panelEmpty";
            this.panelEmpty.Size = new Size(0x148, 80);
            this.panelEmpty.TabIndex = 12;
            this.txtGNName1.EditValue = "";
            this.txtGNName1.Location = new System.Drawing.Point(0x60, 0x10);
            this.txtGNName1.Name = "txtGNName1";
            this.txtGNName1.Size = new Size(200, 0x15);
            this.txtGNName1.TabIndex = 12;
            this.txtGNName1.EditValueChanged += new EventHandler(this.txtGNName1_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0x10);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "网络名称:";
            base.Controls.Add(this.panelNotEmpty);
            base.Name = "BulidGeometryNetwork_SelectFeatureClass";
            base.Size = new Size(0x158, 0x130);
            base.Load += new EventHandler(this.BulidGeometryNetwork_SelectFeatureClass_Load);
            this.panelNotEmpty.ResumeLayout(false);
            this.panelNotEmpty.PerformLayout();
            this.txtGNName.Properties.EndInit();
            this.panelEmpty.ResumeLayout(false);
            this.panelEmpty.PerformLayout();
            this.txtGNName1.Properties.EndInit();
            base.ResumeLayout(false);
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

