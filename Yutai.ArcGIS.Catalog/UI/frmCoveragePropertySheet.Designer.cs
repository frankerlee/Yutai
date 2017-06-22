using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ESRI.ArcGIS.DataSourcesFile;

namespace Yutai.ArcGIS.Catalog.UI
{
    partial class frmCoveragePropertySheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCoveragePropertySheet));
            this.panel1 = new Panel();
            this.btnOK = new SimpleButton();
            this.btnApply = new SimpleButton();
            this.simpleButton3 = new SimpleButton();
            this.xtraTabControl1 = new XtraTabControl();
            this.panel1.SuspendLayout();
            this.xtraTabControl1.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.simpleButton3);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 325);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(504, 40);
            this.panel1.TabIndex = 4;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(296, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnApply.Location = new Point(432, 8);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(56, 24);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.simpleButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton3.Location = new Point(368, 8);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new Size(56, 24);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "取消";
            this.xtraTabControl1.Dock = DockStyle.Fill;
            this.xtraTabControl1.Location = new Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new Size(504, 365);
            this.xtraTabControl1.TabIndex = 3;
            this.xtraTabControl1.Text = "xtraTabControl1";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(504, 365);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.xtraTabControl1);
            
            base.Name = "frmCoveragePropertySheet";
            this.Text = "frmCoveragePropertySheet";
            base.Load += new EventHandler(this.frmCoveragePropertySheet_Load);
            this.panel1.ResumeLayout(false);
            this.xtraTabControl1.EndInit();
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnApply;
        private SimpleButton btnOK;
        private Panel panel1;
        private SimpleButton simpleButton3;
        private XtraTabControl xtraTabControl1;
    }
}