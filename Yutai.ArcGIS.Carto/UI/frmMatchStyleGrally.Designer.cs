using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmMatchStyleGrally
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
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.matchStyleGrally1 = new MatchStyleGrallyControl();
            base.SuspendLayout();
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(160, 304);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(64, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(264, 304);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(64, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.matchStyleGrally1.Location = new Point(8, 8);
            this.matchStyleGrally1.Name = "matchStyleGrally1";
            this.matchStyleGrally1.Size = new Size(336, 296);
            this.matchStyleGrally1.TabIndex = 3;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(352, 333);
            base.Controls.Add(this.matchStyleGrally1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Name = "frmMatchStyleGrally";
            this.Text = "匹配符号库";
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private MatchStyleGrallyControl matchStyleGrally1;
    }
}