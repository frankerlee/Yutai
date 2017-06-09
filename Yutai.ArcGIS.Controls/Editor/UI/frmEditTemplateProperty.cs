using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.Editor.UI
{
    public class frmEditTemplateProperty : Form
    {
        private Button btnApply;
        private Button btnCancle;
        private Button btnOK;
        private ComboBox cboTools;
        private IContainer components = null;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblLayer;
        private PropertyControls m_property = new PropertyControls();
        private Panel panel1;
        private StyleLable styleLable1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TextBox txtDescription;
        private TextBox txtLabel;
        private TextBox txtName;

        public frmEditTemplateProperty()
        {
            this.InitializeComponent();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.EditTemplate.Name = this.txtName.Text;
            this.EditTemplate.Description = this.txtDescription.Text;
            this.EditTemplate.Label = this.txtLabel.Text;
            this.m_property.Apply();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmEditTemplateProperty_Load(object sender, EventArgs e)
        {
            this.txtName.Text = this.EditTemplate.Name;
            this.txtDescription.Text = this.EditTemplate.Description;
            this.lblLayer.Text = this.EditTemplate.FeatureLayer.Name;
            this.txtLabel.Text = this.EditTemplate.Label;
            this.m_property.EditTemplate = this.EditTemplate;
            this.m_property.Dock = DockStyle.Fill;
            this.styleLable1.Style = this.EditTemplate.Symbol;
            this.panel1.Controls.Add(this.m_property);
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
            this.tabControl1.Size = new Size(0x1dc, 0x238);
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
            this.tabPage1.Location = new Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x1d4, 0x21e);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(3, 0xd6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1ce, 0x145);
            this.panel1.TabIndex = 11;
            this.groupBox1.Controls.Add(this.styleLable1);
            this.groupBox1.Location = new Point(0x131, 0x6c);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x8f, 100);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "符号";
            this.cboTools.FormattingEnabled = true;
            this.cboTools.Location = new Point(0x48, 120);
            this.cboTools.Name = "cboTools";
            this.cboTools.Size = new Size(0xcd, 20);
            this.cboTools.TabIndex = 9;
            this.cboTools.Visible = false;
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new Point(0x53, 0x7d);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new Size(0x3b, 12);
            this.lblLayer.TabIndex = 8;
            this.lblLayer.Text = "目标图层:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(7, 0x7d);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3b, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "目标图层:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(7, 0x7b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "默认工具:";
            this.label4.Visible = false;
            this.txtLabel.Location = new Point(0x2f, 0x51);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new Size(0x191, 0x15);
            this.txtLabel.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 0x54);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "标签:";
            this.txtDescription.Location = new Point(0x2f, 0x2d);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new Size(0x191, 0x15);
            this.txtDescription.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x30);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "描述:";
            this.txtName.Location = new Point(0x2f, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(0x191, 0x15);
            this.txtName.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x86, 0x23f);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancle.Location = new Point(0xe2, 0x240);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(0x4b, 0x17);
            this.btnCancle.TabIndex = 2;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnApply.Enabled = false;
            this.btnApply.Location = new Point(0x15b, 0x23f);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new Size(0x4b, 0x17);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Visible = false;
            this.btnApply.Click += new EventHandler(this.btnApply_Click);
            this.styleLable1.Location = new Point(0x18, 0x1a);
            this.styleLable1.Name = "styleLable1";
            this.styleLable1.Size = new Size(0x71, 0x3e);
            this.styleLable1.Style = null;
            this.styleLable1.TabIndex = 0;
            this.styleLable1.Text = "无法绘制符号!";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(490, 610);
            base.Controls.Add(this.btnApply);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
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

        private void label6_Paint(object sender, PaintEventArgs e)
        {
            if (this.EditTemplate.Symbol != null)
            {
                IStyleDraw draw = StyleDrawFactory.CreateStyleDraw(this.EditTemplate.Symbol);
                if (draw != null)
                {
                    draw.Draw(e.Graphics.GetHdc().ToInt32(), e.ClipRectangle, 96.0, 1.0);
                }
            }
        }

        public YTEditTemplate EditTemplate { get; set; }
    }
}

