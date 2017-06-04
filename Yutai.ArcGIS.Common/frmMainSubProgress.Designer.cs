using System.ComponentModel;
using System.Drawing;

namespace Yutai.ArcGIS.Common
{
    partial class frmMainSubProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblMain = new System.Windows.Forms.Label();
            this.progressMain = new System.Windows.Forms.ProgressBar();
            this.timer_0 = new System.Windows.Forms.Timer(this.components);
            this.lblSub = new System.Windows.Forms.Label();
            this.progressSub = new System.Windows.Forms.ProgressBar();
            base.SuspendLayout();
            this.lblMain.AutoSize = true;
            this.lblMain.Location = new Point(13, 13);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new Size(41, 12);
            this.lblMain.TabIndex = 0;
            this.lblMain.Text = "      ";
            this.progressMain.Location = new Point(12, 41);
            this.progressMain.Name = "progressMain";
            this.progressMain.Size = new Size(254, 18);
            this.progressMain.TabIndex = 1;
            this.lblSub.AutoSize = true;
            this.lblSub.Location = new Point(13, 75);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new Size(41, 12);
            this.lblSub.TabIndex = 2;
            this.lblSub.Text = "      ";
            this.progressSub.Location = new Point(15, 125);
            this.progressSub.Name = "progressSub";
            this.progressSub.Size = new Size(254, 18);
            this.progressSub.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(288, 155);
            base.Controls.Add(this.progressSub);
            base.Controls.Add(this.lblSub);
            base.Controls.Add(this.progressMain);
            base.Controls.Add(this.lblMain);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Name = "frmMainSubProgress";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmProgress";
            base.Load += new System.EventHandler(this.frmMainSubProgress_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMain;

        private System.Windows.Forms.ProgressBar progressMain;

        private System.Windows.Forms.Timer timer_0;

        private System.Windows.Forms.Label lblSub;

        private System.Windows.Forms.ProgressBar progressSub;
    }
}