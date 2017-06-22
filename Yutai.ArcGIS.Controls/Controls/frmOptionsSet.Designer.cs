using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Enums;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class frmOptionsSet
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
            this.tabPage1 = new TabPage();
            this.groupBox1 = new GroupBox();
            this.rdoAlwaysPrompt = new RadioButton();
            this.rdoAlwaysBuild = new RadioButton();
            this.rdoNeverBuild = new RadioButton();
            this.btnOK = new Button();
            this.btnCancle = new Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = DockStyle.Top;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(339, 144);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(331, 118);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "栅格";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.rdoNeverBuild);
            this.groupBox1.Controls.Add(this.rdoAlwaysBuild);
            this.groupBox1.Controls.Add(this.rdoAlwaysPrompt);
            this.groupBox1.Location = new Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(295, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "创建金字塔对话框设置";
            this.rdoAlwaysPrompt.AutoSize = true;
            this.rdoAlwaysPrompt.Checked = true;
            this.rdoAlwaysPrompt.Location = new Point(15, 21);
            this.rdoAlwaysPrompt.Name = "rdoAlwaysPrompt";
            this.rdoAlwaysPrompt.Size = new Size(167, 16);
            this.rdoAlwaysPrompt.TabIndex = 0;
            this.rdoAlwaysPrompt.TabStop = true;
            this.rdoAlwaysPrompt.Text = "构建金字塔前，都显示提示";
            this.rdoAlwaysPrompt.UseVisualStyleBackColor = true;
            this.rdoAlwaysBuild.AutoSize = true;
            this.rdoAlwaysBuild.Location = new Point(15, 43);
            this.rdoAlwaysBuild.Name = "rdoAlwaysBuild";
            this.rdoAlwaysBuild.Size = new Size(179, 16);
            this.rdoAlwaysBuild.TabIndex = 1;
            this.rdoAlwaysBuild.Text = "总是构建金字塔，不显示提示";
            this.rdoAlwaysBuild.UseVisualStyleBackColor = true;
            this.rdoNeverBuild.AutoSize = true;
            this.rdoNeverBuild.Location = new Point(15, 65);
            this.rdoNeverBuild.Name = "rdoNeverBuild";
            this.rdoNeverBuild.Size = new Size(167, 16);
            this.rdoNeverBuild.TabIndex = 2;
            this.rdoNeverBuild.Text = "不构建金字塔，不显示提示";
            this.rdoNeverBuild.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(143, 150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new Point(238, 149);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(75, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(339, 185);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.Name = "frmOptionsSet";
            this.Text = "选项";
            base.Load += new EventHandler(this.frmOptionsSet_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnCancle;
        private Button btnOK;
        private GroupBox groupBox1;
        private RadioButton rdoAlwaysBuild;
        private RadioButton rdoAlwaysPrompt;
        private RadioButton rdoNeverBuild;
        private TabControl tabControl1;
        private TabPage tabPage1;
    }
}