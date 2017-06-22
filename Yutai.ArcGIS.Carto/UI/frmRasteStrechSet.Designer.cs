using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Carto.UI
{
    partial class frmRasteStrechSet
    {
        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

       
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRasteStrechSet));
            this.label1 = new Label();
            this.comboBox1 = new ComboBox();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.groupBox1 = new GroupBox();
            this.tabControl1 = new TabControl();
            this.comboBox2 = new ComboBox();
            this.checkBox1 = new CheckBox();
            this.btnOK = new Button();
            this.button2 = new Button();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "无", "自定义", "标准偏差", "等距直方图", "最小-最大值", "标准化直方图" });
            this.comboBox1.Location = new Point(75, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(204, 20);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "标注偏差";
            this.textBox1.Location = new Point(75, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(100, 21);
            this.textBox1.TabIndex = 3;
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new Point(14, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(257, 201);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "统计信息";
            this.tabControl1.Location = new Point(10, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(235, 151);
            this.tabControl1.TabIndex = 0;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] { "使用整个数据集", "采用以下设置" });
            this.comboBox2.Location = new Point(29, 18);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(171, 20);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(223, 44);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(48, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "反转";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(100, 287);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new Point(206, 287);
            this.button2.Name = "button2";
            this.button2.Size = new Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            base.ClientSize = new Size(300, 322);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.comboBox1);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmRasteStrechSet";
            this.Text = "拉伸设置";
            base.Load += new EventHandler(this.frmRasteStrechSet_Load);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

       
        private Button btnOK;
        private Button button2;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private TabControl tabControl1;
        private TextBox textBox1;
    }
}