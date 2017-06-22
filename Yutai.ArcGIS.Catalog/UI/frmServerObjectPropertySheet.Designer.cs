using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class frmServerObjectPropertySheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServerObjectPropertySheet));
            this.simpleButton3 = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2 = new Panel();
            this.xtraTabControl1 = new XtraTabControl();
            this.panel1 = new Panel();
            this.panel2.SuspendLayout();
            this.xtraTabControl1.BeginInit();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.simpleButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton3.Location = new Point(368, 8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(80, 24);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "取消";
            this.btnCancel.Location = new Point(272, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(80, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "应用";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(176, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(80, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel2.Controls.Add(this.xtraTabControl1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(504, 330);
            this.panel2.TabIndex = 1;
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(504, 330);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.simpleButton3);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 290);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(504, 40);
            this.panel1.TabIndex = 2;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(504, 330);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            
            base.Name = "frmServerObjectPropertySheet";
            this.Text = "ServerObject属性";
            base.Load += new EventHandler(this.frmServerObjectPropertySheet_Load);
            this.panel2.ResumeLayout(false);
            this.xtraTabControl1.EndInit();
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Panel panel1;
        private Panel panel2;
        private SimpleButton simpleButton3;
        private XtraTabControl xtraTabControl1;
    }
}