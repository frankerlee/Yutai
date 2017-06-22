using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.NetworkAnalyst;

namespace Yutai.ArcGIS.Controls.ClosestFacilitySolver
{
    partial class frmClosesFacilitySolver
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClosesFacilitySolver));
            this.lstOutput = new ListBox();
            this.cmdSolve = new Button();
            this.txtCutOff = new TextBox();
            this.txtTargetFacility = new TextBox();
            this.cboCostAttribute = new ComboBox();
            this.chkUseHierarchy = new CheckBox();
            this.chkUseRestriction = new CheckBox();
            this.lblCutOff = new Label();
            this.lblNumFacility = new Label();
            this.lblCostAttribute = new Label();
            base.SuspendLayout();
            this.lstOutput.ItemHeight = 12;
            this.lstOutput.Location = new System.Drawing.Point(24, 232);
            this.lstOutput.Name = "lstOutput";
            this.lstOutput.Size = new Size(360, 196);
            this.lstOutput.TabIndex = 19;
            this.cmdSolve.Location = new System.Drawing.Point(24, 192);
            this.cmdSolve.Name = "cmdSolve";
            this.cmdSolve.Size = new Size(176, 22);
            this.cmdSolve.TabIndex = 18;
            this.cmdSolve.Text = "查找最近设施";
            this.cmdSolve.Click += new EventHandler(this.cmdSolve_Click);
            this.txtCutOff.Location = new System.Drawing.Point(224, 80);
            this.txtCutOff.Name = "txtCutOff";
            this.txtCutOff.Size = new Size(160, 21);
            this.txtCutOff.TabIndex = 17;
            this.txtTargetFacility.Location = new System.Drawing.Point(224, 40);
            this.txtTargetFacility.Name = "txtTargetFacility";
            this.txtTargetFacility.Size = new Size(160, 21);
            this.txtTargetFacility.TabIndex = 16;
            this.cboCostAttribute.Location = new System.Drawing.Point(224, 16);
            this.cboCostAttribute.Name = "cboCostAttribute";
            this.cboCostAttribute.Size = new Size(160, 20);
            this.cboCostAttribute.TabIndex = 15;
            this.cboCostAttribute.Text = "cboCostAttribute";
            this.chkUseHierarchy.Location = new System.Drawing.Point(24, 152);
            this.chkUseHierarchy.Name = "chkUseHierarchy";
            this.chkUseHierarchy.Size = new Size(168, 22);
            this.chkUseHierarchy.TabIndex = 14;
            this.chkUseHierarchy.Text = "使用层次";
            this.chkUseRestriction.Location = new System.Drawing.Point(24, 128);
            this.chkUseRestriction.Name = "chkUseRestriction";
            this.chkUseRestriction.Size = new Size(168, 22);
            this.chkUseRestriction.TabIndex = 13;
            this.chkUseRestriction.Text = "使用Oneway约束";
            this.lblCutOff.AutoSize = true;
            this.lblCutOff.Location = new System.Drawing.Point(24, 88);
            this.lblCutOff.Name = "lblCutOff";
            this.lblCutOff.Size = new Size(29, 12);
            this.lblCutOff.TabIndex = 12;
            this.lblCutOff.Text = "断开";
            this.lblNumFacility.AutoSize = true;
            this.lblNumFacility.Location = new System.Drawing.Point(24, 48);
            this.lblNumFacility.Name = "lblNumFacility";
            this.lblNumFacility.Size = new Size(65, 12);
            this.lblNumFacility.TabIndex = 11;
            this.lblNumFacility.Text = "目标设施数";
            this.lblCostAttribute.AutoSize = true;
            this.lblCostAttribute.Location = new System.Drawing.Point(24, 16);
            this.lblCostAttribute.Name = "lblCostAttribute";
            this.lblCostAttribute.Size = new Size(29, 12);
            this.lblCostAttribute.TabIndex = 10;
            this.lblCostAttribute.Text = "费用";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(536, 490);
            base.Controls.Add(this.lstOutput);
            base.Controls.Add(this.cmdSolve);
            base.Controls.Add(this.txtCutOff);
            base.Controls.Add(this.txtTargetFacility);
            base.Controls.Add(this.cboCostAttribute);
            base.Controls.Add(this.chkUseHierarchy);
            base.Controls.Add(this.chkUseRestriction);
            base.Controls.Add(this.lblCutOff);
            base.Controls.Add(this.lblNumFacility);
            base.Controls.Add(this.lblCostAttribute);
            base.Name = "frmClosesFacilitySolver";
            this.Text = "最近设置分析";
            base.Load += new EventHandler(this.frmClosesFacilitySolver_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Container components = null;
        private ComboBox cboCostAttribute;
        private CheckBox chkUseHierarchy;
        private CheckBox chkUseRestriction;
        private Button cmdSolve;
        private Label lblCostAttribute;
        private Label lblCutOff;
        private Label lblNumFacility;
        private ListBox lstOutput;
        private INAContext m_pNAContext;
        private TextBox txtCutOff;
        private TextBox txtTargetFacility;
    }
}