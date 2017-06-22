using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmSnapConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSnapConfig));
            this.panel1 = new Panel();
            this.btnCancle = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnCancle);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 243);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(354, 28);
            this.panel1.TabIndex = 0;
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new Point(256, 2);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(56, 24);
            this.btnCancle.TabIndex = 1;
            this.btnCancle.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(184, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(354, 271);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmSnapConfig";
            this.Text = "捕捉配置";
            base.Load += new EventHandler(this.frmSnapConfig_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnCancle;
        private SimpleButton btnOK;
        private Panel panel1;
    }
}