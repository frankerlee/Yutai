using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common.BaseClasses;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class PropertySheet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertySheet));
            this.tabControl1 = new TabControl();
            this.panel1 = new Panel();
            this.btnApply = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(412, 241);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 241);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(412, 32);
            this.panel1.TabIndex = 6;
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
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(412, 273);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PropertySheet";
            base.StartPosition = FormStartPosition.CenterParent;
            base.Load += new EventHandler(this.PropertySheet_Load);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private SimpleButton btnApply;
        private SimpleButton btnCancel;
        private SimpleButton btnOK;
        private Panel panel1;
        private TabControl tabControl1;
    }
}