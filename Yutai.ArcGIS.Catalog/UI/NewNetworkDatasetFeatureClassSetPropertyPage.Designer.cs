using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class NewNetworkDatasetFeatureClassSetPropertyPage
    {
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
            this.btnClearAll.Location = new System.Drawing.Point(243, 59);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(40, 24);
            this.btnClearAll.TabIndex = 13;
            this.btnClearAll.Text = "清除";
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnSelectAll.Location = new System.Drawing.Point(243, 29);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 24);
            this.btnSelectAll.TabIndex = 12;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.chkListUseFeatureClass.Location = new System.Drawing.Point(11, 29);
            this.chkListUseFeatureClass.Name = "chkListUseFeatureClass";
            this.chkListUseFeatureClass.Size = new Size(226, 196);
            this.chkListUseFeatureClass.TabIndex = 11;
            this.chkListUseFeatureClass.ItemCheck += new ItemCheckEventHandler(this.chkListUseFeatureClass_ItemCheck);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(173, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "选择要参与网络要素集的要素类";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.btnClearAll);
            base.Controls.Add(this.btnSelectAll);
            base.Controls.Add(this.chkListUseFeatureClass);
            base.Controls.Add(this.label1);
            base.Name = "NewNetworkDatasetFeatureClassSetPropertyPage";
            base.Size = new Size(322, 265);
            base.Load += new EventHandler(this.NewNetworkDatasetFeatureClassSetPropertyPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private SimpleButton btnClearAll;
        private SimpleButton btnSelectAll;
        private CheckedListBox chkListUseFeatureClass;
        private Label label1;
    }
}