﻿namespace Yutai.Forms
{
    partial class ErrorView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorView));
            this.textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnQuit = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnContinue = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btnReport = new Syncfusion.Windows.Forms.ButtonAdv();
            this.treeViewAdv1 = new Syncfusion.Windows.Forms.Tools.TreeViewAdv();
            this.btnCopy = new Syncfusion.Windows.Forms.ButtonAdv();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewAdv1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(484, 108);
            this.textBoxExt1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxExt1.Location = new System.Drawing.Point(12, 137);
            this.textBoxExt1.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.textBoxExt1.Multiline = true;
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.ReadOnly = true;
            this.textBoxExt1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExt1.Size = new System.Drawing.Size(484, 108);
            this.textBoxExt1.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Default;
            this.textBoxExt1.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMessage.Location = new System.Drawing.Point(82, 7);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(414, 49);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Error message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Yutai.Properties.Resources.img_warning64;
            this.pictureBox1.Location = new System.Drawing.Point(12, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.btnQuit.BeforeTouchSize = new System.Drawing.Size(85, 26);
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnQuit.IsBackStageButton = false;
            this.btnQuit.Location = new System.Drawing.Point(411, 254);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(85, 26);
            this.btnQuit.TabIndex = 1;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyle = false;
            this.btnQuit.UseVisualStyleBackColor = true;
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.btnContinue.BeforeTouchSize = new System.Drawing.Size(85, 26);
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnContinue.IsBackStageButton = false;
            this.btnContinue.Location = new System.Drawing.Point(320, 254);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(85, 26);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyle = false;
            this.btnContinue.UseVisualStyleBackColor = true;
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReport.BeforeTouchSize = new System.Drawing.Size(77, 26);
            this.btnReport.IsBackStageButton = false;
            this.btnReport.Location = new System.Drawing.Point(12, 254);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(77, 26);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewAdv1.BackColor = System.Drawing.Color.White;
            this.treeViewAdv1.BeforeTouchSize = new System.Drawing.Size(411, 72);
            this.treeViewAdv1.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.treeViewAdv1.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
            this.treeViewAdv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewAdv1.CanSelectDisabledNode = false;
            // 
            // 
            // 
            this.treeViewAdv1.HelpTextControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewAdv1.HelpTextControl.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv1.HelpTextControl.Name = "helpText";
            this.treeViewAdv1.HelpTextControl.Size = new System.Drawing.Size(49, 15);
            this.treeViewAdv1.HelpTextControl.TabIndex = 0;
            this.treeViewAdv1.HelpTextControl.Text = "help text";
            this.treeViewAdv1.Location = new System.Drawing.Point(85, 59);
            this.treeViewAdv1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.SelectedNodeBackground = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220))))));
            this.treeViewAdv1.ShowFocusRect = false;
            this.treeViewAdv1.Size = new System.Drawing.Size(411, 72);
            this.treeViewAdv1.Style = Syncfusion.Windows.Forms.Tools.TreeStyle.Metro;
            this.treeViewAdv1.TabIndex = 4;
            this.treeViewAdv1.Text = "treeViewAdv1";
            // 
            // 
            // 
            this.treeViewAdv1.ToolTipControl.BackColor = System.Drawing.SystemColors.Info;
            this.treeViewAdv1.ToolTipControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewAdv1.ToolTipControl.Location = new System.Drawing.Point(0, 0);
            this.treeViewAdv1.ToolTipControl.Name = "toolTip";
            this.treeViewAdv1.ToolTipControl.Size = new System.Drawing.Size(41, 15);
            this.treeViewAdv1.ToolTipControl.TabIndex = 1;
            this.treeViewAdv1.ToolTipControl.Text = "toolTip";
            this.treeViewAdv1.AfterSelect += new System.EventHandler(this.OntreeViewAdvAfterSelect);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCopy.BeforeTouchSize = new System.Drawing.Size(77, 26);
            this.btnCopy.IsBackStageButton = false;
            this.btnCopy.Location = new System.Drawing.Point(95, 254);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(77, 26);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.OnCopyClick);
            // 
            // ErrorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnContinue;
            this.ClientSize = new System.Drawing.Size(508, 292);
            this.Controls.Add(this.treeViewAdv1);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.textBoxExt1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ErrorView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ooops, we are down";
            this.Load += new System.EventHandler(this.ErrorView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeViewAdv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Syncfusion.Windows.Forms.ButtonAdv btnQuit;
        private Syncfusion.Windows.Forms.ButtonAdv btnContinue;
        private Syncfusion.Windows.Forms.ButtonAdv btnReport;
        private Syncfusion.Windows.Forms.Tools.TreeViewAdv treeViewAdv1;
        private Syncfusion.Windows.Forms.ButtonAdv btnCopy;
    }
}
