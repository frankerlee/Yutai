using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class FeatureDatasetControl
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
            this.label1 = new Label();
            this.txtFeatureDatasetName = new TextEdit();
            this.groupBox1 = new GroupBox();
            this.btnEditSR = new SimpleButton();
            this.chkShowDetail = new CheckEdit();
            this.memoEditSRDescription = new MemoEdit();
            this.label2 = new Label();
            this.txtFeatureDatasetName.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.chkShowDetail.Properties.BeginInit();
            this.memoEditSRDescription.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.txtFeatureDatasetName.EditValue = "";
            this.txtFeatureDatasetName.Location = new System.Drawing.Point(64, 8);
            this.txtFeatureDatasetName.Name = "txtFeatureDatasetName";
            this.txtFeatureDatasetName.Size = new Size(192, 23);
            this.txtFeatureDatasetName.TabIndex = 1;
            this.groupBox1.Controls.Add(this.btnEditSR);
            this.groupBox1.Controls.Add(this.chkShowDetail);
            this.groupBox1.Controls.Add(this.memoEditSRDescription);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(264, 264);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "空间参考";
            this.btnEditSR.Location = new System.Drawing.Point(160, 231);
            this.btnEditSR.Name = "btnEditSR";
            this.btnEditSR.Size = new Size(64, 24);
            this.btnEditSR.TabIndex = 5;
            this.btnEditSR.Text = "编辑...";
            this.btnEditSR.Click += new EventHandler(this.btnEditSR_Click);
            this.chkShowDetail.Location = new System.Drawing.Point(16, 231);
            this.chkShowDetail.Name = "chkShowDetail";
            this.chkShowDetail.Properties.Caption = "显示详细信息";
            this.chkShowDetail.Size = new Size(104, 19);
            this.chkShowDetail.TabIndex = 4;
            this.chkShowDetail.CheckedChanged += new EventHandler(this.chkShowDetail_CheckedChanged);
            this.memoEditSRDescription.EditValue = "";
            this.memoEditSRDescription.Location = new System.Drawing.Point(16, 48);
            this.memoEditSRDescription.Name = "memoEditSRDescription";
            this.memoEditSRDescription.Properties.Appearance.BackColor = SystemColors.InactiveBorder;
            this.memoEditSRDescription.Properties.Appearance.Options.UseBackColor = true;
            this.memoEditSRDescription.Properties.ReadOnly = true;
            this.memoEditSRDescription.Size = new Size(232, 168);
            this.memoEditSRDescription.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 23);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "说明:";
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.txtFeatureDatasetName);
            base.Controls.Add(this.label1);
            base.Name = "FeatureDatasetControl";
            base.Size = new Size(296, 320);
            base.Load += new EventHandler(this.FeatureDatasetControl_Load);
            this.txtFeatureDatasetName.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.chkShowDetail.Properties.EndInit();
            this.memoEditSRDescription.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnEditSR;
        private CheckEdit chkShowDetail;
        private GroupBox groupBox1;
        private IFeatureDataset ifeatureDataset_0;
        private IFeatureWorkspace ifeatureWorkspace_0;
        private Label label1;
        private Label label2;
        private MemoEdit memoEditSRDescription;
        private TextEdit txtFeatureDatasetName;
    }
}