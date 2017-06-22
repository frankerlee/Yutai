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
    partial class BulidGN_SourceSkin
    {
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
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(200, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置网络的源点或汇入点";
            this.radioGroup1.Location = new System.Drawing.Point(16, 24);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否"), new RadioGroupItem(null, "是") });
            this.radioGroup1.Size = new Size(160, 32);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.btnClear.Enabled = false;
            this.btnClear.Location = new System.Drawing.Point(184, 152);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(40, 24);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "取消";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.btnSelectAll.Enabled = false;
            this.btnSelectAll.Location = new System.Drawing.Point(184, 120);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 24);
            this.btnSelectAll.TabIndex = 10;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.chkChangeFC.Enabled = false;
            this.chkChangeFC.Location = new System.Drawing.Point(16, 120);
            this.chkChangeFC.Name = "chkChangeFC";
            this.chkChangeFC.Size = new Size(160, 84);
            this.chkChangeFC.TabIndex = 9;
            this.chkChangeFC.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkChangeFC_ItemCheck);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 96);
            this.label2.Name = "label2";
            this.label2.Size = new Size(202, 17);
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

       
        private SimpleButton btnClear;
        private SimpleButton btnSelectAll;
        private CheckedListBox chkChangeFC;
        private GroupBox groupBox1;
        private Label label2;
        private RadioGroup radioGroup1;
    }
}