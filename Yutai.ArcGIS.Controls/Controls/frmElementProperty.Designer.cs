using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Controls.Controls
{
  
    partial class frmElementProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmElementProperty));
            this.panel1 = new Panel();
            this.btnApply = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.tabControl1 = new TabControl();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 317);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(408, 32);
            this.panel1.TabIndex = 4;
            this.btnApply.Location = new Point(344, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(56, 24);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "应用";
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new Point(280, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(56, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(216, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(56, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(408, 317);
            this.tabControl1.TabIndex = 5;
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(408, 349);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.panel1);
            
            base.Name = "frmElementProperty";
            this.Text = "属性";
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Panel panel1;
        private TabControl tabControl1;
    }
}