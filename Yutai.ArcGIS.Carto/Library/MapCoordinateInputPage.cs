using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Carto.Library
{
    public class MapCoordinateInputPage : UserControl
    {
        private bool bool_0 = false;
        private double double_0 = 0.0;
        private double double_1 = 0.0;
        private IContainer icontainer_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private MapTemplateApplyHelp mapTemplateApplyHelp_0 = null;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private TextBox txtX;
        private TextBox txtY;

        public MapCoordinateInputPage()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            try
            {
                this.mapTemplateApplyHelp_0.CoordinateType = this.radioButton1.Checked ? 0 : 1;
                this.mapTemplateApplyHelp_0.X = double.Parse(this.txtX.Text);
                this.mapTemplateApplyHelp_0.Y = double.Parse(this.txtY.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("请检查输入是否正确!");
                return false;
            }
        }

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void InitializeComponent()
        {
            this.radioButton1 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.label3 = new Label();
            this.label4 = new Label();
            this.txtY = new TextBox();
            this.txtX = new TextBox();
            this.label2 = new Label();
            this.label1 = new Label();
            base.SuspendLayout();
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new Point(0x17, 0x11);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x77, 0x10);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "图幅内任一点坐标";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new Point(0x17, 0x27);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x6b, 0x10);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "图幅左下角坐标";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0xcd, 0x4b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x11, 12);
            this.label3.TabIndex = 0x1d;
            this.label3.Text = "米";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xd0, 0x70);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x11, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "米";
            this.txtY.Location = new Point(0x44, 0x6c);
            this.txtY.Name = "txtY";
            this.txtY.Size = new Size(0x83, 0x15);
            this.txtY.TabIndex = 0x1c;
            this.txtX.Location = new Point(0x44, 0x48);
            this.txtX.Name = "txtX";
            this.txtX.Size = new Size(0x83, 0x15);
            this.txtX.TabIndex = 0x1b;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x17, 0x6f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 0x1a;
            this.label2.Text = "纵坐标";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x15, 0x4b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x29, 12);
            this.label1.TabIndex = 0x19;
            this.label1.Text = "横坐标";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.txtY);
            base.Controls.Add(this.txtX);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.Name = "MapCoordinateInputPage";
            base.Size = new Size(0x10f, 0x10d);
            base.Load += new EventHandler(this.MapCoordinateInputPage_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void MapCoordinateInputPage_Load(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                this.radioButton2.Enabled = false;
                this.txtX.Text = this.double_0.ToString();
                this.txtY.Text = this.double_1.ToString();
            }
        }

        public void SetMouseClick(double double_2, double double_3)
        {
            this.double_0 = double_2;
            this.double_1 = double_3;
            this.bool_0 = true;
        }

        public MapTemplateApplyHelp MapTemplateHelp
        {
            get
            {
                return this.mapTemplateApplyHelp_0;
            }
            set
            {
                this.mapTemplateApplyHelp_0 = value;
            }
        }
    }
}

