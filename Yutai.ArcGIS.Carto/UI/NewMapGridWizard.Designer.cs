using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class NewMapGridWizard
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
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.btnNext.Location = new System.Drawing.Point(224, 280);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(64, 24);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new System.Drawing.Point(152, 280);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(64, 24);
            this.btnLast.TabIndex = 5;
            this.btnLast.Text = "<上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(376, 264);
            this.panel1.TabIndex = 4;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(296, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(224, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "完成";
            this.btnOK.Visible = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.ClientSize = new Size(376, 333);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnCancel);
            base.Name = "NewMapGridWizard";
            this.Text = "地理网格向导";
            base.Load += new EventHandler(this.NewMapGridWizard_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private SimpleButton btnOK;
        private Panel panel1;
    }
}