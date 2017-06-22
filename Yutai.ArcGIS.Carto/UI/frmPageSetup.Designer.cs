using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmPageSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPageSetup));
            this.btnOK = new SimpleButton();
            this.panel = new Panel();
            this.btnCancel = new SimpleButton();
            base.SuspendLayout();
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(88, 272);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(48, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel.Dock = DockStyle.Top;
            this.panel.Location = new Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new Size(208, 264);
            this.panel.TabIndex = 1;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Location = new Point(144, 272);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(48, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(208, 301);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.panel);
            base.Controls.Add(this.btnOK);
            
            base.Name = "frmPageSetup";
            this.Text = "页面设置";
            base.Load += new EventHandler(this.frmPageSetup_Load);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Panel panel;
    }
}