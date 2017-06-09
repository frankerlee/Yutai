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
    public class BulidGN_SourceSkin : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnClear;
        private SimpleButton btnSelectAll;
        private CheckedListBox chkChangeFC;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private Label label2;
        private RadioGroup radioGroup1;

        public BulidGN_SourceSkin()
        {
            this.InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkChangeFC.Items.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap1 = this.chkChangeFC.Items[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                this.chkChangeFC.SetItemChecked(i, false);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.chkChangeFC.Items.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap1 = this.chkChangeFC.Items[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                this.chkChangeFC.SetItemChecked(i, true);
            }
        }

        private void BulidGN_SourceSkin_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps.Count; i++)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap item = BulidGeometryNetworkHelper.BulidGNHelper.FeatureClassWraps[i] as BulidGeometryNetworkHelper.FeatureClassWrap;
                if ((item.GeometryType == esriGeometryType.esriGeometryPoint) && item.IsUse)
                {
                    this.chkChangeFC.Items.Add(item, item.NetworkClassAncillaryRole == esriNetworkClassAncillaryRole.esriNCARSourceSink);
                }
            }
            this.bool_0 = true;
        }

        private void chkChangeFC_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            if (this.bool_0)
            {
                BulidGeometryNetworkHelper.FeatureClassWrap wrap = this.chkChangeFC.Items[e.Index] as BulidGeometryNetworkHelper.FeatureClassWrap;
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
            this.groupBox1 = new GroupBox();
            this.radioGroup1 = new RadioGroup();
            this.btnClear = new SimpleButton();
            this.btnSelectAll = new SimpleButton();
            this.chkChangeFC = new CheckedListBox();
            this.label2 = new Label();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 0x10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(200, 0x40);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置网络的源点或汇入点";
            this.radioGroup1.Location = new System.Drawing.Point(0x10, 0x18);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否"), new RadioGroupItem(null, "是") });
            this.radioGroup1.Size = new Size(160, 0x20);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(0xb8, 0x98);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(40, 0x18);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "取消";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnSelectAll.Enabled = false;
            this.btnSelectAll.Location = new System.Drawing.Point(0xb8, 120);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 0x18);
            this.btnSelectAll.TabIndex = 10;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.chkChangeFC.Enabled = false;
            this.chkChangeFC.Location = new System.Drawing.Point(0x10, 120);
            this.chkChangeFC.Name = "chkChangeFC";
            this.chkChangeFC.Size = new Size(160, 0x54);
            this.chkChangeFC.TabIndex = 9;
            this.chkChangeFC.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkChangeFC_ItemCheck);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0x60);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xca, 0x11);
            this.label2.TabIndex = 8;
            this.label2.Text = "选择可以用作源点或汇入点的要素类";
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.chkChangeFC);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BulidGN_SourceSkin";
            base.Size = new Size(280, 240);
            base.Load += new EventHandler(this.BulidGN_SourceSkin_Load);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            base.ResumeLayout(false);
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

