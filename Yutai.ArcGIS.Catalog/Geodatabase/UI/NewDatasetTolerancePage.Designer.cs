using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class NewDatasetTolerancePage
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
            this.gropuM = new GroupBox();
            this.lblMResolution = new Label();
            this.txtMPrecision = new TextEdit();
            this.groupZ = new GroupBox();
            this.lblZResolution = new Label();
            this.txtZPrecision = new TextEdit();
            this.groupBoxXY = new GroupBox();
            this.lblXYResolution = new Label();
            this.txtXYPrecision = new TextEdit();
            this.chkUseDefault = new CheckEdit();
            this.gropuM.SuspendLayout();
            this.txtMPrecision.Properties.BeginInit();
            this.groupZ.SuspendLayout();
            this.txtZPrecision.Properties.BeginInit();
            this.groupBoxXY.SuspendLayout();
            this.txtXYPrecision.Properties.BeginInit();
            this.chkUseDefault.Properties.BeginInit();
            base.SuspendLayout();
            this.gropuM.Controls.Add(this.lblMResolution);
            this.gropuM.Controls.Add(this.txtMPrecision);
            this.gropuM.Location = new System.Drawing.Point(16, 168);
            this.gropuM.Name = "gropuM";
            this.gropuM.Size = new Size(316, 58);
            this.gropuM.TabIndex = 5;
            this.gropuM.TabStop = false;
            this.gropuM.Text = "M容差";
            this.lblMResolution.AutoSize = true;
            this.lblMResolution.Location = new System.Drawing.Point(163, 17);
            this.lblMResolution.Name = "lblMResolution";
            this.lblMResolution.Size = new Size(0, 12);
            this.lblMResolution.TabIndex = 40;
            this.txtMPrecision.EditValue = "";
            this.txtMPrecision.Location = new System.Drawing.Point(16, 18);
            this.txtMPrecision.Name = "txtMPrecision";
            this.txtMPrecision.Size = new Size(140, 21);
            this.txtMPrecision.TabIndex = 29;
            this.groupZ.Controls.Add(this.lblZResolution);
            this.groupZ.Controls.Add(this.txtZPrecision);
            this.groupZ.Location = new System.Drawing.Point(16, 92);
            this.groupZ.Name = "groupZ";
            this.groupZ.Size = new Size(316, 61);
            this.groupZ.TabIndex = 4;
            this.groupZ.TabStop = false;
            this.groupZ.Text = "Z容差";
            this.lblZResolution.AutoSize = true;
            this.lblZResolution.Location = new System.Drawing.Point(158, 28);
            this.lblZResolution.Name = "lblZResolution";
            this.lblZResolution.Size = new Size(0, 12);
            this.lblZResolution.TabIndex = 40;
            this.txtZPrecision.EditValue = "";
            this.txtZPrecision.Location = new System.Drawing.Point(16, 22);
            this.txtZPrecision.Name = "txtZPrecision";
            this.txtZPrecision.Size = new Size(134, 21);
            this.txtZPrecision.TabIndex = 32;
            this.groupBoxXY.Controls.Add(this.lblXYResolution);
            this.groupBoxXY.Controls.Add(this.txtXYPrecision);
            this.groupBoxXY.Location = new System.Drawing.Point(16, 13);
            this.groupBoxXY.Name = "groupBoxXY";
            this.groupBoxXY.Size = new Size(316, 61);
            this.groupBoxXY.TabIndex = 3;
            this.groupBoxXY.TabStop = false;
            this.groupBoxXY.Text = "XY容差";
            this.lblXYResolution.AutoSize = true;
            this.lblXYResolution.Location = new System.Drawing.Point(164, 26);
            this.lblXYResolution.Name = "lblXYResolution";
            this.lblXYResolution.Size = new Size(0, 12);
            this.lblXYResolution.TabIndex = 39;
            this.txtXYPrecision.EditValue = "";
            this.txtXYPrecision.Location = new System.Drawing.Point(16, 21);
            this.txtXYPrecision.Name = "txtXYPrecision";
            this.txtXYPrecision.Size = new Size(142, 21);
            this.txtXYPrecision.TabIndex = 36;
            this.chkUseDefault.EditValue = true;
            this.chkUseDefault.Location = new System.Drawing.Point(16, 242);
            this.chkUseDefault.Name = "chkUseDefault";
            this.chkUseDefault.Properties.Caption = "使用默认精度和坐标范围(推荐)";
            this.chkUseDefault.Size = new Size(210, 19);
            this.chkUseDefault.TabIndex = 6;
            this.chkUseDefault.CheckedChanged += new EventHandler(this.chkUseDefault_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.chkUseDefault);
            base.Controls.Add(this.gropuM);
            base.Controls.Add(this.groupZ);
            base.Controls.Add(this.groupBoxXY);
            base.Name = "NewDatasetTolerancePage";
            base.Size = new Size(355, 325);
            base.VisibleChanged += new EventHandler(this.NewDatasetTolerancePage_VisibleChanged);
            base.Load += new EventHandler(this.NewDatasetTolerancePage_Load);
            this.gropuM.ResumeLayout(false);
            this.gropuM.PerformLayout();
            this.txtMPrecision.Properties.EndInit();
            this.groupZ.ResumeLayout(false);
            this.groupZ.PerformLayout();
            this.txtZPrecision.Properties.EndInit();
            this.groupBoxXY.ResumeLayout(false);
            this.groupBoxXY.PerformLayout();
            this.txtXYPrecision.Properties.EndInit();
            this.chkUseDefault.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private GroupBox gropuM;
        private GroupBox groupBoxXY;
        private GroupBox groupZ;
        private Label lblMResolution;
        private Label lblXYResolution;
        private Label lblZResolution;
        private TextEdit txtMPrecision;
        private TextEdit txtXYPrecision;
        private TextEdit txtZPrecision;
    }
}