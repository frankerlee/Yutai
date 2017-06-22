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
    partial class BulidGN_IsContaincomplexEdge
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
 private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.rdoIsContainNet = new RadioGroup();
            this.groupBox2 = new GroupBox();
            this.btnClearAll = new SimpleButton();
            this.btnSelectAll = new SimpleButton();
            this.checkedListBox1 = new CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.rdoIsContainNet.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.rdoIsContainNet);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(240, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "是否包含复杂边";
            this.rdoIsContainNet.Location = new System.Drawing.Point(16, 24);
            this.rdoIsContainNet.Name = "rdoIsContainNet";
            this.rdoIsContainNet.Properties.Appearance.BackColor = SystemColors.Control;
            this.rdoIsContainNet.Properties.Appearance.Options.UseBackColor = true;
            this.rdoIsContainNet.Properties.BorderStyle = BorderStyles.NoBorder;
            this.rdoIsContainNet.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "否"), new RadioGroupItem(null, "是") });
            this.rdoIsContainNet.Size = new Size(184, 32);
            this.rdoIsContainNet.TabIndex = 0;
            this.rdoIsContainNet.SelectedIndexChanged += new EventHandler(this.rdoIsContainNet_SelectedIndexChanged);
            this.groupBox2.Controls.Add(this.btnClearAll);
            this.groupBox2.Controls.Add(this.btnSelectAll);
            this.groupBox2.Controls.Add(this.checkedListBox1);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(16, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(240, 144);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择要用来建立复杂边的线状要素类";
            this.btnClearAll.Location = new System.Drawing.Point(184, 56);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(40, 24);
            this.btnClearAll.TabIndex = 5;
            this.btnClearAll.Text = "取消";
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnSelectAll.Location = new System.Drawing.Point(184, 24);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 24);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.checkedListBox1.Location = new System.Drawing.Point(16, 24);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new Size(144, 100);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "BulidGN_IsContaincomplexEdge";
            base.Size = new Size(280, 256);
            base.Load += new EventHandler(this.BulidGN_IsContaincomplexEdge_Load);
            this.groupBox1.ResumeLayout(false);
            this.rdoIsContainNet.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnClearAll;
        private SimpleButton btnSelectAll;
        private CheckedListBox checkedListBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioGroup rdoIsContainNet;
    }
}