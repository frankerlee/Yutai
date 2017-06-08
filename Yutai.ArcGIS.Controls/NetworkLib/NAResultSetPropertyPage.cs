using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Yutai.ArcGIS.Controls.NetworkLib
{
    internal class NAResultSetPropertyPage : UserControl
    {
        private CheckEdit checkEdit1;
        private CheckEdit checkEdit2;
        private CheckEdit checkEdit3;
        private ColorEdit colorEdit1;
        private Container components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private bool m_CanDo = false;
        private bool m_IsDirty = false;
        private bool m_TestOK = false;
        private Panel panel1;
        private RadioGroup radioGroup1;
        private RadioGroup radioGroup2;

        public NAResultSetPropertyPage()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Init()
        {
            this.m_CanDo = false;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.groupBox2 = new GroupBox();
            this.checkEdit3 = new CheckEdit();
            this.checkEdit2 = new CheckEdit();
            this.label4 = new Label();
            this.radioGroup2 = new RadioGroup();
            this.label3 = new Label();
            this.groupBox1 = new GroupBox();
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.radioGroup1 = new RadioGroup();
            this.label2 = new Label();
            this.colorEdit1 = new ColorEdit();
            this.checkEdit1 = new CheckEdit();
            this.groupBox2.SuspendLayout();
            this.checkEdit3.Properties.BeginInit();
            this.checkEdit2.Properties.BeginInit();
            this.radioGroup2.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.radioGroup1.Properties.BeginInit();
            this.colorEdit1.Properties.BeginInit();
            this.checkEdit1.Properties.BeginInit();
            base.SuspendLayout();
            this.groupBox2.Controls.Add(this.checkEdit3);
            this.groupBox2.Controls.Add(this.checkEdit2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.radioGroup2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(8, 0xa8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x108, 160);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "属性表连接串信息设置";
            this.checkEdit3.EditValue = true;
            this.checkEdit3.Location = new System.Drawing.Point(0x10, 0x80);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "连接点";
            this.checkEdit3.Size = new Size(80, 0x13);
            this.checkEdit3.TabIndex = 7;
            this.checkEdit2.EditValue = true;
            this.checkEdit2.Location = new System.Drawing.Point(0x10, 0x68);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "边";
            this.checkEdit2.Size = new Size(0x40, 0x13);
            this.checkEdit2.TabIndex = 6;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x58);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4f, 0x11);
            this.label4.TabIndex = 5;
            this.label4.Text = "在结果中包括";
            this.radioGroup2.Location = new System.Drawing.Point(0x10, 0x20);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup2.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup2.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup2.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "全部要素"), new RadioGroupItem(null, "停止追踪的要素") });
            this.radioGroup2.Size = new Size(120, 0x30);
            this.radioGroup2.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0x10);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x36, 0x11);
            this.label3.TabIndex = 3;
            this.label3.Text = "结果包括";
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.colorEdit1);
            this.groupBox1.Controls.Add(this.checkEdit1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x108, 0x98);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "结果格式";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 0x18);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x42, 0x11);
            this.label1.TabIndex = 2;
            this.label1.Text = "返回结果为";
            this.panel1.Controls.Add(this.radioGroup1);
            this.panel1.Location = new System.Drawing.Point(0x10, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xd0, 0x68);
            this.panel1.TabIndex = 6;
            this.radioGroup1.Location = new System.Drawing.Point(0x10, -24);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "绘图"), new RadioGroupItem(null, "要素集") });
            this.radioGroup1.Size = new Size(0xa8, 0x98);
            this.radioGroup1.TabIndex = 6;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x70, 0x58);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x67, 0x11);
            this.label2.TabIndex = 9;
            this.label2.Text = "追踪任务结果颜色";
            this.colorEdit1.EditValue = Color.Empty;
            this.colorEdit1.Location = new System.Drawing.Point(0x38, 0x58);
            this.colorEdit1.Name = "colorEdit1";
            this.colorEdit1.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.colorEdit1.Size = new Size(0x30, 0x17);
            this.colorEdit1.TabIndex = 8;
            this.checkEdit1.Location = new System.Drawing.Point(0x38, 0x40);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "绘制复杂边的单个要素";
            this.checkEdit1.Size = new Size(0x90, 0x13);
            this.checkEdit1.TabIndex = 7;
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.groupBox2);
            base.Name = "NAResultSetPropertyPage";
            base.Size = new Size(0x128, 0x158);
            base.Load += new EventHandler(this.NAResultSetPropertyPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.checkEdit3.Properties.EndInit();
            this.checkEdit2.Properties.EndInit();
            this.radioGroup2.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.radioGroup1.Properties.EndInit();
            this.colorEdit1.Properties.EndInit();
            this.checkEdit1.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void NAResultSetPropertyPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }
    }
}

