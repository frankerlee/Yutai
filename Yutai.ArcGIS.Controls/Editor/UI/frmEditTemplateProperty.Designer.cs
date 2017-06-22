using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    partial class frmEditTemplateProperty
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
            this.panel1 = new Panel();
            this.groupBox1 = new GroupBox();
            this.cboTools = new ComboBox();
            this.lblLayer = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.txtLabel = new TextBox();
            this.label3 = new Label();
            this.txtDescription = new TextBox();
            this.label2 = new Label();
            this.txtName = new TextBox();
            this.label1 = new Label();
            this.btnOK = new Button();
            this.btnCancle = new Button();
            this.btnApply = new Button();
            this.styleLable1 = new StyleLable();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(476, 568);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.cboTools);
            this.tabPage1.Controls.Add(this.lblLayer);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtLabel);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtDescription);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(468, 542);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(3, 214);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(462, 325);
            this.panel1.TabIndex = 11;
            this.groupBox1.Controls.Add(this.styleLable1);
            this.groupBox1.Location = new Point(305, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(143, 100);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号";
            this.cboTools.FormattingEnabled = true;
            this.cboTools.Location = new Point(72, 120);
            this.cboTools.Name = "cboTools";
            this.cboTools.Size = new Size(205, 20);
            this.cboTools.TabIndex = 9;
            this.cboTools.Visible = false;
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new Point(83, 125);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new Size(59, 12);
            this.lblLayer.TabIndex = 8;
            this.lblLayer.Text = "目标图层:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(7, 125);
            this.label5.Name = "label5";
            this.label5.Size = new Size(59, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "目标图层:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(7, 123);
            this.label4.Name = "label4";
            this.label4.Size = new Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "默认工具:";
            this.label4.Visible = false;
            this.txtLabel.Location = new Point(47, 81);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(401, 21);
            this.txtLabel.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 84);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "标签:";
            this.txtDescription.Location = new Point(47, 45);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(401, 21);
            this.txtDescription.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "描述:";
            this.txtName.Location = new Point(47, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(401, 21);
            this.txtName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new Point(134, 575);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.Location = new Point(226, 576);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(75, 23);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnApply.Enabled = false;
            this.btnApply.Location = new Point(347, 575);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(75, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Visible = false;
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.styleLable1.Location = new Point(24, 26);
            this.styleLable1.Name = "styleLable1";
            this.styleLable1.Size = new Size(113, 62);
            this.styleLable1.Style = null;
            this.styleLable1.TabIndex = 0;
            this.styleLable1.Text = "无法绘制符号!";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new Size(490, 610);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmEditTemplateProperty";
            this.Text = "模板属性";
            base.Load += new EventHandler(this.frmEditTemplateProperty_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnApply;
        private Button btnCancle;
        private Button btnOK;
        private ComboBox cboTools;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblLayer;
        private Panel panel1;
        private StyleLable styleLable1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TextBox txtDescription;
        private TextBox txtLabel;
        private TextBox txtName;
    }
}