using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace Yutai.Catalog.UI
{
    partial class AGSGeneralPropertyPage
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLogPath = new System.Windows.Forms.TextBox();
            this.txtLogSize = new System.Windows.Forms.TextBox();
            this.cboLogLevel = new System.Windows.Forms.ComboBox();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器连接超时";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboLogLevel);
            this.groupBox1.Controls.Add(this.txtLogSize);
            this.groupBox1.Controls.Add(this.txtLogPath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(23, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 149);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器日志属性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "日志文件路径:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "日志文件大小:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "日志级别:";
            // 
            // txtLogPath
            // 
            this.txtLogPath.Location = new System.Drawing.Point(105, 27);
            this.txtLogPath.Name = "txtLogPath";
            this.txtLogPath.Size = new System.Drawing.Size(169, 21);
            this.txtLogPath.TabIndex = 3;
            // 
            // txtLogSize
            // 
            this.txtLogSize.Location = new System.Drawing.Point(105, 57);
            this.txtLogSize.Name = "txtLogSize";
            this.txtLogSize.Size = new System.Drawing.Size(147, 21);
            this.txtLogSize.TabIndex = 4;
            // 
            // cboLogLevel
            // 
            this.cboLogLevel.FormattingEnabled = true;
            this.cboLogLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cboLogLevel.Location = new System.Drawing.Point(105, 87);
            this.cboLogLevel.Name = "cboLogLevel";
            this.cboLogLevel.Size = new System.Drawing.Size(68, 20);
            this.cboLogLevel.TabIndex = 5;
            this.cboLogLevel.Text = "3";
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(128, 18);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(169, 21);
            this.txtTimeOut.TabIndex = 4;
            // 
            // AGSGeneralPropertyPage
            // 
            this.Controls.Add(this.txtTimeOut);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "AGSGeneralPropertyPage";
            this.Size = new System.Drawing.Size(321, 223);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }





        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private ComboBox cboLogLevel;
        private TextBox txtLogSize;
        private TextBox txtLogPath;
        private TextBox txtTimeOut;
    }
}
