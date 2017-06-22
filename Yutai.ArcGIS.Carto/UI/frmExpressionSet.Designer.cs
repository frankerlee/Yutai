using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmExpressionSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpressionSet));
            this.panel1 = new Panel();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 349);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(304, 32);
            this.panel1.TabIndex = 0;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(240, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(176, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(304, 349);
            this.panel2.TabIndex = 1;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(304, 381);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmExpressionSet";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "表达式";
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Panel panel1;
        private Panel panel2;
    }
}