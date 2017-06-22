using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Controls.ApplicationStyle;

namespace Yutai.ArcGIS.Controls.Controls.ConfigSetting
{
    partial class frmAppConfigSettingEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAppConfigSettingEx));
            this.simpleButton1 = new SimpleButton();
            this.btnExit = new SimpleButton();
            this.panel1 = new Panel();
            this.btnSave = new SimpleButton();
            this.panel2 = new Panel();
            this.comboBoxEdit1 = new ComboBoxEdit();
            this.label1 = new Label();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage5 = new TabPage();
            this.tabPage4 = new TabPage();
            this.tabPage2 = new TabPage();
            this.tabPage3 = new TabPage();
            this.tabPage6 = new TabPage();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            base.SuspendLayout();
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new Point(12, 6);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(118, 24);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "打开配置文件";
            this.simpleButton1.Visible = false;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new Point(490, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(64, 24);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 442);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(588, 34);
            this.panel1.TabIndex = 2;
            this.btnSave.Location = new Point(386, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(96, 24);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存配置参数";
            this.btnSave.Click += new EventHandler(this.btnSave_Click);
            this.panel2.Controls.Add(this.comboBoxEdit1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(588, 29);
            this.panel2.TabIndex = 3;
            this.comboBoxEdit1.Location = new Point(62, 5);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Size = new Size(454, 21);
            this.comboBoxEdit1.TabIndex = 1;
            this.comboBoxEdit1.SelectedIndexChanged += new EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置文件";
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(0, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(588, 413);
            this.tabControl1.TabIndex = 4;
            this.tabPage1.Location = new Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new Size(580, 387);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "空间数据库连接设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage5.Location = new Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new Padding(3);
            this.tabPage5.Size = new Size(580, 387);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "导航图层配置信息";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage4.Location = new Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(580, 387);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "属性数据库连接设置";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabPage2.Location = new Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new Size(580, 387);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "符号库文件配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Visible = false;
            this.tabPage3.Location = new Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new Size(580, 387);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "其他配置信息";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage6.Location = new Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new Padding(3);
            this.tabPage6.Size = new Size(557, 417);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "权限管理数据库连接设置";
            this.tabPage6.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(588, 476);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAppConfigSettingEx";
            this.Text = "JLKEngine2015-应用系统参数设置";
            base.Load += new EventHandler(this.frmAppConfigSettingEx_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        
        private IContainer components = null;
        private SimpleButton btnExit;
        private SimpleButton btnSave;
        private ComboBoxEdit comboBoxEdit1;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private SimpleButton simpleButton1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
    }
}