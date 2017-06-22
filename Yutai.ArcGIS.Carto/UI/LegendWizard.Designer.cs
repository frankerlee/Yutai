using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class LegendWizard
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
            this.panel1 = new Panel();
            this.btnPre = new SimpleButton();
            this.btnNext = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            base.SuspendLayout();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(456, 272);
            this.panel1.TabIndex = 0;
            this.btnPre.Enabled = false;
            this.btnPre.Location = new Point(216, 280);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new Size(56, 24);
            this.btnPre.TabIndex = 1;
            this.btnPre.Text = "<上一步";
            this.btnPre.Click += new EventHandler(this.btnPre_Click);
            this.btnNext.Location = new Point(288, 280);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new Size(56, 24);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一步>";
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(360, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(288, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "完成";
            this.btnOK.Visible = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(456, 309);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnNext);
            base.Controls.Add(this.btnPre);
            base.Controls.Add(this.panel1);
            base.Name = "LegendWizard";
            this.Text = "图例向导";
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnNext;
        private SimpleButton btnOK;
        private SimpleButton btnPre;
        private Panel panel1;
    }
}