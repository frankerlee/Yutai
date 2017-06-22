using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmBufferWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBufferWizard));
            this.btnNext = new SimpleButton();
            this.btnLast = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.panel1 = new Panel();
            base.SuspendLayout();
            this.btnNext.Location = new Point(400, 336);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(64, 24);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnLast.Enabled = false;
            this.btnLast.Location = new Point(328, 336);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new Size(64, 24);
            this.btnLast.TabIndex = 9;
            this.btnLast.Text = "<上一步";
            this.btnLast.Click += new EventHandler(this.btnLast_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(472, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.panel1.Location = new Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(536, 312);
            this.panel1.TabIndex = 13;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(552, 373);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnLast);
            base.Controls.Add(this.btnCancel);
            
            base.Name = "frmBufferWizard";
            this.Text = "缓冲区向导";
            base.Load += new EventHandler(this.frmBufferWizard_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnLast;
        private SimpleButton btnNext;
        private Panel panel1;
    }
}