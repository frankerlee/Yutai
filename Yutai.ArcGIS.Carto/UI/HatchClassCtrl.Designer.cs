using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Location;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class HatchClassCtrl
    {
        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

       
        private void InitializeComponent()
        {
            this.groupBox1 = new GroupBox();
            this.cboFields = new ComboBoxEdit();
            this.txtHatchInterval = new TextEdit();
            this.radioGroup1 = new RadioGroup();
            this.label1 = new Label();
            this.groupBox2 = new GroupBox();
            this.button1 = new Button();
            this.groupBox3 = new GroupBox();
            this.button2 = new Button();
            this.groupBox4 = new GroupBox();
            this.button5 = new Button();
            this.button4 = new Button();
            this.button3 = new Button();
            this.groupBox1.SuspendLayout();
            this.cboFields.Properties.BeginInit();
            this.txtHatchInterval.Properties.BeginInit();
            this.radioGroup1.Properties.BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.cboFields);
            this.groupBox1.Controls.Add(this.txtHatchInterval);
            this.groupBox1.Controls.Add(this.radioGroup1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(304, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "刻度间隔";
            this.cboFields.EditValue = "";
            this.cboFields.Location = new Point(168, 80);
            this.cboFields.Name = "cboFields";
            this.cboFields.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboFields.Size = new Size(112, 23);
            this.cboFields.TabIndex = 3;
            this.cboFields.SelectedIndexChanged += new EventHandler(this.cboFields_SelectedIndexChanged);
            this.txtHatchInterval.EditValue = "0";
            this.txtHatchInterval.Location = new Point(168, 40);
            this.txtHatchInterval.Name = "txtHatchInterval";
            this.txtHatchInterval.Size = new Size(88, 23);
            this.txtHatchInterval.TabIndex = 2;
            this.txtHatchInterval.EditValueChanged += new EventHandler(this.txtHatchInterval_EditValueChanged);
            this.radioGroup1.Location = new Point(16, 40);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = SystemColors.Control;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new RadioGroupItem[] { new RadioGroupItem(null, "刻度间隔"), new RadioGroupItem(null, "用此字段指定刻度间隔") });
            this.radioGroup1.Size = new Size(168, 64);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new EventHandler(this.radioGroup1_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(165, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "用路径度量单位指定刻度间隔";
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new Point(8, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(144, 48);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "刻度线定义";
            this.button1.Location = new Point(16, 17);
            this.button1.Name = "button1";
            this.button1.Size = new Size(112, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "增加刻度线定义...";
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new Point(168, 128);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(144, 48);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "预定义刻度线样式";
            this.button2.Location = new Point(16, 17);
            this.button2.Name = "button2";
            this.button2.Size = new Size(112, 24);
            this.button2.TabIndex = 0;
            this.button2.Text = "增加刻度线定义...";
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.button3);
            this.groupBox4.Location = new Point(8, 184);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(304, 48);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "其他选项";
            this.button5.Location = new Point(216, 16);
            this.button5.Name = "button5";
            this.button5.Size = new Size(72, 24);
            this.button5.TabIndex = 2;
            this.button5.Text = "SQL查询...";
            this.button4.Location = new Point(136, 16);
            this.button4.Name = "button4";
            this.button4.Size = new Size(72, 24);
            this.button4.TabIndex = 1;
            this.button4.Text = "比例范围...";
            this.button3.Location = new Point(16, 17);
            this.button3.Name = "button3";
            this.button3.Size = new Size(112, 24);
            this.button3.TabIndex = 0;
            this.button3.Text = "刻度线放置位置...";
            base.Controls.Add(this.groupBox4);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Name = "HatchClassCtrl";
            base.Size = new Size(328, 248);
            base.Load += new EventHandler(this.HatchClassCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            this.cboFields.Properties.EndInit();
            this.txtHatchInterval.Properties.EndInit();
            this.radioGroup1.Properties.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private ComboBoxEdit cboFields;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label1;
        private RadioGroup radioGroup1;
        private TextEdit txtHatchInterval;
    }
}