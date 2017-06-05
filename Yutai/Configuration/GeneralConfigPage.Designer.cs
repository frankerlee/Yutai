using System.Windows.Forms;

namespace Yutai.Configuration
{
    partial class GeneralConfigPage
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
            
            this.configPanelControl3 = new Yutai.UI.Controls.ConfigPanelControl();
            this.chkLocalDocumentation = new System.Windows.Forms.CheckBox();
            this.chkShowMenuToolTips = new System.Windows.Forms.CheckBox();
            this.configPanelControl2 = new Yutai.UI.Controls.ConfigPanelControl();
            this.chkShowWelcomeDialog = new System.Windows.Forms.CheckBox();
            this.chkLoadLastProject = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.configPanelControl3)).BeginInit();
            this.configPanelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configPanelControl2)).BeginInit();
            this.configPanelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // configPanelControl3
            // 
            this.configPanelControl3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.configPanelControl3.Controls.Add(this.chkLocalDocumentation);
            this.configPanelControl3.Controls.Add(this.chkShowMenuToolTips);
            this.configPanelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.configPanelControl3.HeaderText = "其他";
            this.configPanelControl3.Location = new System.Drawing.Point(0, 99);
            this.configPanelControl3.Name = "configPanelControl3";
            this.configPanelControl3.Size = new System.Drawing.Size(380, 114);
            this.configPanelControl3.TabIndex = 10;
            // 
            // chkLocalDocumentation
            // 
            this.chkLocalDocumentation.Location = new System.Drawing.Point(15, 61);
            this.chkLocalDocumentation.Name = "chkLocalDocumentation";
            this.chkLocalDocumentation.Size = new System.Drawing.Size(283, 19);
            this.chkLocalDocumentation.TabIndex = 11;
            this.chkLocalDocumentation.Text = "仅使用本地文档";
            // 
            // chkShowMenuToolTips
            // 
            this.chkShowMenuToolTips.Location = new System.Drawing.Point(15, 36);
            this.chkShowMenuToolTips.Name = "chkShowMenuToolTips";
            this.chkShowMenuToolTips.Size = new System.Drawing.Size(283, 19);
            this.chkShowMenuToolTips.TabIndex = 5;
            this.chkShowMenuToolTips.Text = "显示菜单按钮提示信息（需要重新启动）";
            // 
            // configPanelControl2
            // 
            this.configPanelControl2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.configPanelControl2.Controls.Add(this.chkShowWelcomeDialog);
            this.configPanelControl2.Controls.Add(this.chkLoadLastProject);
            this.configPanelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.configPanelControl2.HeaderText = "启动配置";
            this.configPanelControl2.Location = new System.Drawing.Point(0, 0);
            this.configPanelControl2.Name = "configPanelControl2";
            this.configPanelControl2.Size = new System.Drawing.Size(380, 99);
            this.configPanelControl2.TabIndex = 8;
            // 
            // chkShowWelcomeDialog
            // 
            this.chkShowWelcomeDialog.Location = new System.Drawing.Point(15, 34);
            this.chkShowWelcomeDialog.Name = "chkShowWelcomeDialog";
            this.chkShowWelcomeDialog.Size = new System.Drawing.Size(188, 19);
            this.chkShowWelcomeDialog.TabIndex = 4;
            this.chkShowWelcomeDialog.Text = "显示欢迎窗体";
            // 
            // chkLoadLastProject
            // 
            this.chkLoadLastProject.Location = new System.Drawing.Point(15, 62);
            this.chkLoadLastProject.Name = "chkLoadLastProject";
            this.chkLoadLastProject.Size = new System.Drawing.Size(188, 19);
            this.chkLoadLastProject.TabIndex = 3;
            this.chkLoadLastProject.Text = "启动时自动加载最后的项目";
            // 
            // GeneralConfigPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.configPanelControl3);
            this.Controls.Add(this.configPanelControl2);
            this.Name = "GeneralConfigPage";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 10, 9);
            this.Size = new System.Drawing.Size(390, 210);
            ((System.ComponentModel.ISupportInitialize)(this.configPanelControl3)).EndInit();
            this.configPanelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.configPanelControl2)).EndInit();
            this.configPanelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Controls.ConfigPanelControl configPanelControl2;
        private CheckBox chkLoadLastProject;
        private CheckBox chkShowWelcomeDialog;
        private UI.Controls.ConfigPanelControl configPanelControl3;
        private CheckBox chkShowMenuToolTips;
        private CheckBox chkLocalDocumentation;
    }
}
