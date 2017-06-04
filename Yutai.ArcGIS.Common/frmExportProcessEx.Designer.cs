using System.Drawing;

namespace Yutai.ArcGIS.Common
{
    partial class frmExportProcessEx
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
            this.lblGeoProcessor = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnDetial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            base.SuspendLayout();
            this.lblGeoProcessor.AutoSize = true;
            this.lblGeoProcessor.Location = new Point(13, 13);
            this.lblGeoProcessor.Name = "lblGeoProcessor";
            this.lblGeoProcessor.Size = new Size(41, 12);
            this.lblGeoProcessor.TabIndex = 0;
            this.lblGeoProcessor.Text = "label1";
            this.txtMessage.Location = new Point(15, 66);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new Size(265, 155);
            this.txtMessage.TabIndex = 1;
            this.progressBar1.Location = new Point(15, 32);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(222, 16);
            this.progressBar1.TabIndex = 2;
            this.button1.Location = new Point(205, 238);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.textBox1.Location = new Point(12, 159);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new Size(294, 173);
            this.textBox1.TabIndex = 1;
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new Size(47, 12);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(14, 131);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(132, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "处理完成后自动关闭";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.btnDetial.Location = new Point(254, 31);
            this.btnDetial.Name = "btnDetial";
            this.btnDetial.Size = new Size(75, 23);
            this.btnDetial.TabIndex = 4;
            this.btnDetial.Text = "详细信息>>";
            this.btnDetial.UseVisualStyleBackColor = true;
            this.btnDetial.Click += new System.EventHandler(this.btnDetial_Click);
            this.label1.Location = new Point(18, 60);
            this.label1.Name = "label1";
            this.label1.Size = new Size(288, 38);
            this.label1.TabIndex = 6;
            this.label1.Text = "";
            this.progressBar2.Location = new Point(16, 105);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new Size(222, 16);
            this.progressBar2.TabIndex = 5;
            base.ClientSize = new Size(346, 155);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.progressBar2);
            base.Controls.Add(this.btnDetial);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.lblInfo);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.progressBar1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmExportProcessEx";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            base.Load += new System.EventHandler(this.frmExportProcessEx_Load);
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmExportProcessEx_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblGeoProcessor;

        private System.Windows.Forms.TextBox txtMessage;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.ProgressBar progressBar1;

        private System.Windows.Forms.TextBox textBox1;

        private System.Windows.Forms.Label lblInfo;

        private System.Windows.Forms.CheckBox checkBox1;

        private System.Windows.Forms.Button btnDetial;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.ProgressBar progressBar2;
    }
}