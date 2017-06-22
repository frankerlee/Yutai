using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class ObjectClassInfoEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectClassInfoEdit));
            this.btnOK = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.tabControl1 = new TabControl();
            base.SuspendLayout();
            this.btnOK.Location = new System.Drawing.Point(168, 496);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnApply.Location = new System.Drawing.Point(296, 496);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(56, 24);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(232, 496);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(368, 480);
            this.tabControl1.TabIndex = 3;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(384, 533);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnOK);
            
            base.Name = "ObjectClassInfoEdit";
            this.Text = "新建";
            base.Load += new EventHandler(this.ObjectClassInfoEdit_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private enumUseType enumUseType_0;
        private IFeatureDataset ifeatureDataset_0;
        private IFeatureWorkspace ifeatureWorkspace_0;
        private IFieldsEdit ifieldsEdit_0;
        private IObjectClass iobjectClass_0;
        private TabControl tabControl1;
    }
}