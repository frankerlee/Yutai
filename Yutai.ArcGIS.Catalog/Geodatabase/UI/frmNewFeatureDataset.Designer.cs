using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmNewFeatureDataset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewFeatureDataset));
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Location = new System.Drawing.Point(6, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(457, 465);
            this.panel1.TabIndex = 11;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(373, 481);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnNext.Location = new System.Drawing.Point(301, 481);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(56, 24);
            this.btnNext.TabIndex = 9;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Location = new System.Drawing.Point(229, 481);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(56, 24);
            this.btnLast.TabIndex = 8;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(475, 509);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            
            base.Name = "frmNewFeatureDataset";
            this.Text = "新建要素集";
            base.Load += new EventHandler(this.frmNewFeatureDataset_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private Panel panel1;
    }
}