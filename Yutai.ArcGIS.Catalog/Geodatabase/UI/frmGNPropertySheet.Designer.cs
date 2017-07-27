using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    partial class frmGNPropertySheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGNPropertySheet));
            this.panel1 = new Panel();
            this.btnOK = new SimpleButton();
            this.btnCnacel = new SimpleButton();
            this.xtraTabControl1 = new XtraTabControl();
            this.panel1.SuspendLayout();
            this.xtraTabControl1.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCnacel);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 340);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(532, 32);
            this.panel1.TabIndex = 0;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(295, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnCnacel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCnacel.Location = new Point(357, 6);
            this.btnCnacel.Name = "btnCnacel";
            this.btnCnacel.Size = new Size(56, 24);
            this.btnCnacel.TabIndex = 6;
            this.btnCnacel.Text = "取消";
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(532, 340);
            this.xtraTabControl1.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(532, 372);
            base.Controls.Add(this.xtraTabControl1);
            base.Controls.Add(this.panel1);
          
            base.Name = "frmGNPropertySheet";
            this.Text = "网络属性";
            base.Load += new EventHandler(this.frmGNPropertySheet_Load);
            this.panel1.ResumeLayout(false);
            this.xtraTabControl1.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCnacel;
        private SimpleButton btnOK;
        private Panel panel1;
        private XtraTabControl xtraTabControl1;
    }
}