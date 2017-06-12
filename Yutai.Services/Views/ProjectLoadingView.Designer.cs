namespace Yutai.Services.Views
{
    partial class ProjectLoadingView
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMessage = new System.Windows.Forms.Label();
            this.progressBarAdv1 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(14, 34);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(55, 14);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "引导中...";
            // 
            // progressBarAdv1
            // 
            this.progressBarAdv1.Location = new System.Drawing.Point(17, 10);
            this.progressBarAdv1.Name = "progressBarAdv1";
            this.progressBarAdv1.Size = new System.Drawing.Size(471, 18);
            this.progressBarAdv1.TabIndex = 2;
            // 
            // ProjectLoadingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 60);
            this.ControlBox = false;
            this.Controls.Add(this.progressBarAdv1);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectLoadingView";
            this.Text = "Loading Project:";
            ((System.ComponentModel.ISupportInitialize)(this.progressBarAdv1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private DevExpress.XtraEditors.ProgressBarControl progressBarAdv1;
    }
}
