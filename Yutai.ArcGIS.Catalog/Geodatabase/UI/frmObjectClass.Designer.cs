using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmObjectClass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmObjectClass));
            this.btnLast = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.btnLast.Location = new System.Drawing.Point(168, 480);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(56, 24);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnNext.Location = new System.Drawing.Point(240, 480);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(56, 24);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(312, 480);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(392, 456);
            this.panel1.TabIndex = 3;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(408, 509);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            
            base.Name = "frmObjectClass";
            this.Text = "新建";
            base.Load += new EventHandler(this.frmObjectClass_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private enumUseType enumUseType_0;
        private IFeatureDataset ifeatureDataset_0;
        private IFeatureWorkspace ifeatureWorkspace_0;
        private IFieldsEdit ifieldsEdit_0;
        private Panel panel1;
    }
}