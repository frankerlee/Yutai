using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class TKConfigPage : UserControl
    {
        private Button btnSelectLegend;
        private CheckBox checkBox1;
        private GroupBox groupBox8;
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = null;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox txtInOutDis;
        private TextBox txtLegendItem;
        private TextBox txtOutBorderWidth;
        private TextBox txtR1C1;
        private TextBox txtR1C2;
        private TextBox txtR1C3;
        private TextBox txtR2C1;
        private TextBox txtR2C2;
        private TextBox txtR2C3;
        private TextBox txtR3C1;
        private TextBox txtR3C2;
        private TextBox txtR3C3;
        private TextBox txtTitleSpace;

        public TKConfigPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            try
            {
                num = double.Parse(this.txtInOutDis.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            try
            {
                num2 = double.Parse(this.txtOutBorderWidth.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            try
            {
                num3 = double.Parse(this.txtTitleSpace.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            if (this.checkBox1.Checked && (this.txtLegendItem.Text.Length == 0))
            {
                MessageBox.Show("请选择图例模板！");
                return false;
            }
            this.jlktkassiatant_0.HasLegend = this.checkBox1.Checked;
            this.jlktkassiatant_0.LegendTemplate = this.checkBox1.Checked ? this.txtLegendItem.Text : "";
            this.jlktkassiatant_0.InOutDist = num;
            this.jlktkassiatant_0.OutBorderWidth = num2;
            this.jlktkassiatant_0.TitleDist = num3;
            this.jlktkassiatant_0.Row1Col1Text = this.txtR1C1.Text.Trim();
            this.jlktkassiatant_0.Row1Col2Text = this.txtR1C2.Text.Trim();
            this.jlktkassiatant_0.Row1Col3Text = this.txtR1C3.Text.Trim();
            this.jlktkassiatant_0.Row2Col1Text = this.txtR2C1.Text.Trim();
            this.jlktkassiatant_0.Row2Col3Text = this.txtR2C3.Text.Trim();
            this.jlktkassiatant_0.Row3Col1Text = this.txtR3C1.Text.Trim();
            this.jlktkassiatant_0.Row3Col2Text = this.txtR3C2.Text.Trim();
            this.jlktkassiatant_0.Row3Col3Text = this.txtR3C3.Text.Trim();
            return true;
        }

        private void btnSelectLegend_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "*.xml|*.xml"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtLegendItem.Text = dialog.FileName;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.btnSelectLegend.Enabled = this.checkBox1.Checked;
            this.txtLegendItem.Enabled = this.checkBox1.Checked;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        public void Init()
        {
            this.txtInOutDis.Text = this.jlktkassiatant_0.InOutDist.ToString();
            this.txtOutBorderWidth.Text = this.jlktkassiatant_0.OutBorderWidth.ToString();
            this.txtTitleSpace.Text = this.jlktkassiatant_0.TitleDist.ToString();
            this.txtR1C1.Text = this.jlktkassiatant_0.Row1Col1Text;
            this.txtR1C2.Text = this.jlktkassiatant_0.Row1Col2Text;
            this.txtR1C3.Text = this.jlktkassiatant_0.Row1Col3Text;
            this.txtR2C1.Text = this.jlktkassiatant_0.Row2Col1Text;
            this.txtR2C3.Text = this.jlktkassiatant_0.Row2Col3Text;
            this.txtR3C1.Text = this.jlktkassiatant_0.Row3Col1Text;
            this.txtR3C2.Text = this.jlktkassiatant_0.Row3Col2Text;
            this.txtR3C3.Text = this.jlktkassiatant_0.Row3Col3Text;
        }

        private void InitializeComponent()
        {
            this.txtInOutDis = new TextBox();
            this.label4 = new Label();
            this.label7 = new Label();
            this.label1 = new Label();
            this.txtOutBorderWidth = new TextBox();
            this.label2 = new Label();
            this.groupBox8 = new GroupBox();
            this.txtR3C3 = new TextBox();
            this.txtR2C3 = new TextBox();
            this.txtR1C3 = new TextBox();
            this.txtR3C2 = new TextBox();
            this.txtR2C2 = new TextBox();
            this.txtR1C2 = new TextBox();
            this.txtR2C1 = new TextBox();
            this.txtR3C1 = new TextBox();
            this.txtR1C1 = new TextBox();
            this.label5 = new Label();
            this.txtTitleSpace = new TextBox();
            this.label6 = new Label();
            this.checkBox1 = new CheckBox();
            this.txtLegendItem = new TextBox();
            this.btnSelectLegend = new Button();
            this.groupBox8.SuspendLayout();
            base.SuspendLayout();
            this.txtInOutDis.Location = new Point(0x4b, 0x41);
            this.txtInOutDis.Name = "txtInOutDis";
            this.txtInOutDis.Size = new Size(0x55, 0x15);
            this.txtInOutDis.TabIndex = 11;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(7, 0x44);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "内外框间距";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0xa6, 0x44);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x1d, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "厘米";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(360, 0x44);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0x11;
            this.label1.Text = "厘米";
            this.txtOutBorderWidth.Location = new Point(0x10d, 0x41);
            this.txtOutBorderWidth.Name = "txtOutBorderWidth";
            this.txtOutBorderWidth.Size = new Size(0x55, 0x15);
            this.txtOutBorderWidth.TabIndex = 0x10;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xc9, 0x44);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "外框宽度";
            this.groupBox8.Controls.Add(this.txtR3C3);
            this.groupBox8.Controls.Add(this.txtR2C3);
            this.groupBox8.Controls.Add(this.txtR1C3);
            this.groupBox8.Controls.Add(this.txtR3C2);
            this.groupBox8.Controls.Add(this.txtR2C2);
            this.groupBox8.Controls.Add(this.txtR1C2);
            this.groupBox8.Controls.Add(this.txtR2C1);
            this.groupBox8.Controls.Add(this.txtR3C1);
            this.groupBox8.Controls.Add(this.txtR1C1);
            this.groupBox8.Location = new Point(9, 0x66);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new Size(0x160, 0x73);
            this.groupBox8.TabIndex = 0x3a;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "接图表信息";
            this.txtR3C3.Location = new Point(0xeb, 80);
            this.txtR3C3.Name = "txtR3C3";
            this.txtR3C3.Size = new Size(0x65, 0x15);
            this.txtR3C3.TabIndex = 11;
            this.txtR2C3.Location = new Point(0xeb, 0x35);
            this.txtR2C3.Name = "txtR2C3";
            this.txtR2C3.Size = new Size(0x65, 0x15);
            this.txtR2C3.TabIndex = 10;
            this.txtR1C3.Location = new Point(0xeb, 0x17);
            this.txtR1C3.Name = "txtR1C3";
            this.txtR1C3.Size = new Size(0x65, 0x15);
            this.txtR1C3.TabIndex = 9;
            this.txtR3C2.Location = new Point(0x80, 80);
            this.txtR3C2.Name = "txtR3C2";
            this.txtR3C2.Size = new Size(0x65, 0x15);
            this.txtR3C2.TabIndex = 8;
            this.txtR2C2.BackColor = SystemColors.InactiveBorder;
            this.txtR2C2.Enabled = false;
            this.txtR2C2.Location = new Point(0x80, 0x35);
            this.txtR2C2.Name = "txtR2C2";
            this.txtR2C2.Size = new Size(0x65, 0x15);
            this.txtR2C2.TabIndex = 7;
            this.txtR1C2.Location = new Point(0x80, 0x17);
            this.txtR1C2.Name = "txtR1C2";
            this.txtR1C2.Size = new Size(0x65, 0x15);
            this.txtR1C2.TabIndex = 6;
            this.txtR2C1.Location = new Point(0x15, 0x35);
            this.txtR2C1.Name = "txtR2C1";
            this.txtR2C1.Size = new Size(0x65, 0x15);
            this.txtR2C1.TabIndex = 5;
            this.txtR3C1.Location = new Point(0x15, 80);
            this.txtR3C1.Name = "txtR3C1";
            this.txtR3C1.Size = new Size(0x65, 0x15);
            this.txtR3C1.TabIndex = 4;
            this.txtR1C1.Location = new Point(0x15, 0x17);
            this.txtR1C1.Name = "txtR1C1";
            this.txtR1C1.Size = new Size(0x65, 0x15);
            this.txtR1C1.TabIndex = 3;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x11e, 0x17);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x1d, 12);
            this.label5.TabIndex = 0x3d;
            this.label5.Text = "厘米";
            this.txtTitleSpace.Location = new Point(110, 20);
            this.txtTitleSpace.Name = "txtTitleSpace";
            this.txtTitleSpace.Size = new Size(170, 0x15);
            this.txtTitleSpace.TabIndex = 60;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(7, 0x17);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x59, 12);
            this.label6.TabIndex = 0x3b;
            this.label6.Text = "图名与外框间距";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x11, 0xe5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x48, 0x10);
            this.checkBox1.TabIndex = 0x3e;
            this.checkBox1.Text = "生成图例";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.txtLegendItem.Location = new Point(0x5f, 0xe2);
            this.txtLegendItem.Name = "txtLegendItem";
            this.txtLegendItem.ReadOnly = true;
            this.txtLegendItem.Size = new Size(220, 0x15);
            this.txtLegendItem.TabIndex = 0x3f;
            this.btnSelectLegend.Location = new Point(0x148, 0xe2);
            this.btnSelectLegend.Name = "btnSelectLegend";
            this.btnSelectLegend.Size = new Size(0x21, 0x13);
            this.btnSelectLegend.TabIndex = 0x40;
            this.btnSelectLegend.Text = "...";
            this.btnSelectLegend.UseVisualStyleBackColor = true;
            this.btnSelectLegend.Click += new EventHandler(this.btnSelectLegend_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnSelectLegend);
            base.Controls.Add(this.txtLegendItem);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.txtTitleSpace);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.groupBox8);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtOutBorderWidth);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.txtInOutDis);
            base.Controls.Add(this.label4);
            base.Name = "TKConfigPage";
            base.Size = new Size(0x1a6, 0x127);
            base.Load += new EventHandler(this.TKConfigPage_Load);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void TKConfigPage_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        internal YTTKAssiatant YTTKAssiatant
        {
            set
            {
                this.jlktkassiatant_0 = value;
            }
        }
    }
}

