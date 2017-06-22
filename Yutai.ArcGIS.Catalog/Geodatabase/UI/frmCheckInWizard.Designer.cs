using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.GeoDatabaseDistributed;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmCheckInWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckInWizard));
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.panel2 = new Panel();
            this.panelProgress = new Panel();
            this.progressBarFC = new System.Windows.Forms.ProgressBar();
            this.lblCheckFC = new Label();
            this.lblCheckOutType = new Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelProgress.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 285);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(440, 40);
            this.panel1.TabIndex = 1;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(344, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnNext.Location = new Point(272, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(56, 24);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(200, 8);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(56, 24);
            this.btnLast.TabIndex = 3;
            this.btnLast.Text = "上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel2.Controls.Add(this.panelProgress);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(440, 285);
            this.panel2.TabIndex = 2;
            this.panelProgress.Controls.Add(this.progressBarFC);
            this.panelProgress.Controls.Add(this.lblCheckFC);
            this.panelProgress.Controls.Add(this.lblCheckOutType);
            this.panelProgress.Location = new Point(48, 24);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new Size(360, 224);
            this.panelProgress.TabIndex = 0;
            this.panelProgress.Visible = false;
            this.progressBarFC.Location = new Point(8, 112);
            this.progressBarFC.Name = "progressBarFC";
            this.progressBarFC.Size = new Size(248, 16);
            this.progressBarFC.TabIndex = 2;
            this.lblCheckFC.AutoSize = true;
            this.lblCheckFC.Location = new Point(16, 80);
            this.lblCheckFC.Name = "lblCheckFC";
            this.lblCheckFC.Size = new Size(0, 12);
            this.lblCheckFC.TabIndex = 1;
            this.lblCheckOutType.AutoSize = true;
            this.lblCheckOutType.Location = new Point(16, 40);
            this.lblCheckOutType.Name = "lblCheckOutType";
            this.lblCheckOutType.Size = new Size(0, 12);
            this.lblCheckOutType.TabIndex = 0;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(440, 325);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmCheckInWizard";
            this.Text = "检入数据向导";
            base.Load += new EventHandler(this.frmCheckInWizard_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private Label lblCheckFC;
        private Label lblCheckOutType;
        private Panel panel1;
        private Panel panel2;
        private Panel panelProgress;
        private System.Windows.Forms.ProgressBar progressBarFC;
    }
}