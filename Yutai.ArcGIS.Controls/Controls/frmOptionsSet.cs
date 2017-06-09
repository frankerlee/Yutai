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
    public class frmOptionsSet : Form
    {
        private Button btnCancle;
        private Button btnOK;
        private IContainer components = null;
        private GroupBox groupBox1;
        private RadioButton rdoAlwaysBuild;
        private RadioButton rdoAlwaysPrompt;
        private RadioButton rdoNeverBuild;
        private TabControl tabControl1;
        private TabPage tabPage1;

        public frmOptionsSet()
        {
            this.InitializeComponent();
            string path = Application.StartupPath + @"\sysconfig.cfg";
            if (File.Exists(path))
            {
                using (TextReader reader = new StreamReader(path, Encoding.Default))
                {
                    string str2 = reader.ReadLine();
                    if (str2.Length > 0)
                    {
                        string[] strArray = str2.Split(new char[] { '=' });
                        if ((strArray.Length > 1) && (strArray[0].ToLower() == "pyramiddialogset"))
                        {
                            try
                            {
                                int num = Convert.ToInt32(strArray[1]);
                                if (num == 0)
                                {
                                    this.rdoAlwaysPrompt.Checked = true;
                                }
                                else if (num == 1)
                                {
                                    this.rdoAlwaysBuild.Checked = true;
                                }
                                else
                                {
                                    this.rdoNeverBuild.Checked = true;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\sysconfig.cfg";
            using (TextWriter writer = new StreamWriter(path, false, Encoding.Default))
            {
                this.WriteRasterCfg(writer);
            }
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmOptionsSet_Load(object sender, EventArgs e)
        {
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
            this.tabControl1.Size = new Size(0x153, 0x90);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x14b, 0x76);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "栅格";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add(this.rdoNeverBuild);
            this.groupBox1.Controls.Add(this.rdoAlwaysBuild);
            this.groupBox1.Controls.Add(this.rdoAlwaysPrompt);
            this.groupBox1.Location = new Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x127, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "创建金字塔对话框设置";
            this.rdoAlwaysPrompt.AutoSize = true;
            this.rdoAlwaysPrompt.Checked = true;
            this.rdoAlwaysPrompt.Location = new Point(15, 0x15);
            this.rdoAlwaysPrompt.Name = "rdoAlwaysPrompt";
            this.rdoAlwaysPrompt.Size = new Size(0xa7, 0x10);
            this.rdoAlwaysPrompt.TabIndex = 0;
            this.rdoAlwaysPrompt.TabStop = true;
            this.rdoAlwaysPrompt.Text = "构建金字塔前，都显示提示";
            this.rdoAlwaysPrompt.UseVisualStyleBackColor = true;
            this.rdoAlwaysBuild.AutoSize = true;
            this.rdoAlwaysBuild.Location = new Point(15, 0x2b);
            this.rdoAlwaysBuild.Name = "rdoAlwaysBuild";
            this.rdoAlwaysBuild.Size = new Size(0xb3, 0x10);
            this.rdoAlwaysBuild.TabIndex = 1;
            this.rdoAlwaysBuild.Text = "总是构建金字塔，不显示提示";
            this.rdoAlwaysBuild.UseVisualStyleBackColor = true;
            this.rdoNeverBuild.AutoSize = true;
            this.rdoNeverBuild.Location = new Point(15, 0x41);
            this.rdoNeverBuild.Name = "rdoNeverBuild";
            this.rdoNeverBuild.Size = new Size(0xa7, 0x10);
            this.rdoNeverBuild.TabIndex = 2;
            this.rdoNeverBuild.Text = "不构建金字塔，不显示提示";
            this.rdoNeverBuild.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(0x8f, 150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.DialogResult = DialogResult.Cancel;
            this.btnCancle.Location = new Point(0xee, 0x95);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(0x4b, 0x17);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x153, 0xb9);
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

        private void WriteRasterCfg(TextWriter reader)
        {
            PyramidPromptType alwaysPrompt = PyramidPromptType.AlwaysPrompt;
            if (this.rdoAlwaysBuild.Checked)
            {
                alwaysPrompt = PyramidPromptType.AlwaysBuildNoPrompt;
            }
            else if (this.rdoNeverBuild.Checked)
            {
                alwaysPrompt = PyramidPromptType.NeverBuildNoPrompt;
            }
            ApplicationRef.Application.PyramidPromptType = alwaysPrompt;
            string str = string.Format("PyramidDialogSet={0}", (int) alwaysPrompt);
            reader.Write(str);
        }
    }
}

