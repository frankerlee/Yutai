using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Common
{
    partial class frmProgress2
    {
        protected override void Dispose(bool bool_1)
        {
            if ((!bool_1 ? false : this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            this.progressBar1 = new ProgressBar();
            this.Caption1 = new Label();
            base.SuspendLayout();
            this.progressBar1.Location = new Point(12, 37);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(337, 13);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Click += new EventHandler(this.progressBar1_Click);
            this.Caption1.AutoSize = true;
            this.Caption1.Location = new Point(13, 16);
            this.Caption1.Name = "Caption1";
            this.Caption1.Size = new Size(53, 12);
            this.Caption1.TabIndex = 4;
            this.Caption1.Text = "Caption1";
            base.TopMost = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(376, 80);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.Caption1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Name = "frmProgress2";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "frmProgress2";
            base.Load += new EventHandler(this.frmProgress2_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private ProcessAssist processAssist_0;
    }
}