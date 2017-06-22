using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class BulidGeometryNetwork_SelectFeatureClass
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
            this.panelNotEmpty.Size = new Size(336, 280);
            this.panelNotEmpty.TabIndex = 11;
            this.txtGNName.EditValue = "";
            this.txtGNName.Location = new System.Drawing.Point(16, 208);
            this.txtGNName.Name = "txtGNName";
            this.txtGNName.Size = new Size(240, 21);
            this.txtGNName.TabIndex = 11;
            this.txtGNName.EditValueChanged += new EventHandler(this.txtGNName_EditValueChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 176);
            this.label2.Name = "label2";
            this.label2.Size = new Size(77, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "几何网络名称";
            this.btnClearAll.Location = new System.Drawing.Point(240, 96);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new Size(40, 24);
            this.btnClearAll.TabIndex = 9;
            this.btnClearAll.Text = "取消";
            this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
            this.btnSelectAll.Location = new System.Drawing.Point(240, 56);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new Size(40, 24);
            this.btnSelectAll.TabIndex = 8;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
            this.chkListUseFeatureClass.Location = new System.Drawing.Point(16, 32);
            this.chkListUseFeatureClass.Name = "chkListUseFeatureClass";
            this.chkListUseFeatureClass.Size = new Size(208, 132);
            this.chkListUseFeatureClass.TabIndex = 7;
            this.chkListUseFeatureClass.ItemCheck += new ItemCheckEventHandler(this.chkListUseFeatureClass_ItemCheck);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(161, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "选择要创建几何网络的要素类";
            this.panelEmpty.Controls.Add(this.txtGNName1);
            this.panelEmpty.Controls.Add(this.label3);
            this.panelEmpty.Location = new System.Drawing.Point(0, 24);
            this.panelEmpty.Name = "panelEmpty";
            this.panelEmpty.Size = new Size(328, 80);
            this.panelEmpty.TabIndex = 12;
            this.txtGNName1.EditValue = "";
            this.txtGNName1.Location = new System.Drawing.Point(96, 16);
            this.txtGNName1.Name = "txtGNName1";
            this.txtGNName1.Size = new Size(200, 21);
            this.txtGNName1.TabIndex = 12;
            this.txtGNName1.EditValueChanged += new EventHandler(this.txtGNName1_EditValueChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 16);
            this.label3.Name = "label3";
            this.label3.Size = new Size(59, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "网络名称:";
            base.Controls.Add(this.panelNotEmpty);
            base.Name = "BulidGeometryNetwork_SelectFeatureClass";
            base.Size = new Size(344, 304);
            base.Load += new EventHandler(this.BulidGeometryNetwork_SelectFeatureClass_Load);
            this.panelNotEmpty.ResumeLayout(false);
            this.panelNotEmpty.PerformLayout();
            this.txtGNName.Properties.EndInit();
            this.panelEmpty.ResumeLayout(false);
            this.panelEmpty.PerformLayout();
            this.txtGNName1.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnClearAll;
        private SimpleButton btnSelectAll;
        private CheckedListBox chkListUseFeatureClass;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panelEmpty;
        private Panel panelNotEmpty;
        private TextEdit txtGNName;
        private TextEdit txtGNName1;
    }
}