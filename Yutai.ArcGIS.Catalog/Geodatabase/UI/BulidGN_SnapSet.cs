using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class BulidGN_SnapSet : UserControl
    {
        private bool bool_0 = false;
        private SimpleButton btnClear;
        private SimpleButton btnSelectAll;
        private CheckedListBox chkChangeFC;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private RadioGroup radioGroup1;
        private TextEdit txtSnaptol;

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
            this.groupBox2 = new GroupBox();
            this.btnClear = new SimpleButton();
            this.btnSelectAll = new SimpleButton();
            this.chkChangeFC = new CheckedListBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.txtSnaptol = new TextEdit();
            this.groupBox1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtSnaptol.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe0, 0x30);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "是否捕捉要素";
            this.radioGroup1.Location = new Point(0x10, 0x10);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否"), new RadioGroupItem(null, "是") });
            this.radioGroup1.Size = new Size(0x98, 0x18);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.btnSelectAll);
            this.groupBox2.Controls.Add(this.chkChangeFC);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtSnaptol);
            this.groupBox2.Location = new Point(8, 0x40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xe0, 0xc0);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "捕捉设置";
            this.btnClear.Location = new Point(0xb0, 0x80);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(40, 0x18);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "取消";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnSelectAll.Location = new Point(0xb0, 0x60);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 0x18);
            this.btnSelectAll.TabIndex = 6;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.chkChangeFC.Location = new Point(8, 0x60);
            this.chkChangeFC.Name = "chkChangeFC";
            this.chkChangeFC.Size = new Size(160, 0x54);
            this.chkChangeFC.TabIndex = 3;
            this.chkChangeFC.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkChangeFC_ItemCheck);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(8, 0x48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x80, 0x11);
            this.label2.TabIndex = 2;
            this.label2.Text = "选择可以移动的要素类";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x36, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "捕捉容差";
            this.txtSnaptol.Location = new Point(8, 40);
            this.txtSnaptol.Name = "txtSnaptol";
            this.txtSnaptol.Size = new Size(0x98, 0x17);
            this.txtSnaptol.TabIndex = 1;
            this.txtSnaptol.EditValueChanged += new EventHandler(this.txtSnaptol_EditValueChanged);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BulidGN_SnapSet";
            base.Size = new Size(0xf8, 0x128);
            base.Load += new EventHandler(this.BulidGN_SnapSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtSnaptol.Properties.EndInit();
            base.ResumeLayout(false);
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

