using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmProgressBar1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProgressBar1));
            this.progressBar1 = new ProgressBar();
            this.Caption1 = new Label();
            this.label1 = new Label();
            base.SuspendLayout();
            this.progressBar1.Location = new Point(1, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(413, 13);
            this.progressBar1.TabIndex = 3;
            this.Caption1.AutoSize = true;
            this.Caption1.Location = new Point(2, 28);
            this.Caption1.Name = "Caption1";
            this.Caption1.Size = new Size(53, 12);
            this.Caption1.TabIndex = 2;
            this.Caption1.Text = "Caption1";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 12);
            this.label1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.GradientInactiveCaption;
            base.ClientSize = new Size(415, 74);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.Caption1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
         
            base.MaximizeBox = false;
            base.Name = "frmProgressBar1";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "frmProgressBar1";
            base.Load += new EventHandler(this.frmProgressBar1_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private IContainer components = null;
    }
}