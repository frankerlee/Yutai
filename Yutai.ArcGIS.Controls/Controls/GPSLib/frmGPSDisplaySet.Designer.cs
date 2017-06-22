using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    partial class frmGPSDisplaySet
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
            this.tabControl1 = new TabControl();
            this.button2 = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            this.tabControl1.Dock = DockStyle.Top;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(436, 348);
            this.tabControl1.TabIndex = 0;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(351, 354);
            this.button2.Name = "button2";
            this.button2.Size = new Size(71, 24);
            this.button2.TabIndex = 15;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(274, 354);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(71, 24);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(436, 390);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGPSDisplaySet";
            this.Text = "GPS显示设置";
            base.Load += new EventHandler(this.frmGPSDisplaySet_Load);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnOK;
        private Button button2;
        private TabControl tabControl1;
    }
}