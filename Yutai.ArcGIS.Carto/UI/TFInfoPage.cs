using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Carto.DesignLib;

namespace Yutai.ArcGIS.Carto.UI
{
    [ToolboxItem(false)]
    internal class TFInfoPage : UserControl
    {
        private ComboBox cboDataum;
        private GroupBox groupSR;
        private IContainer icontainer_0 = null;
        private YTTKAssiatant jlktkassiatant_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Panel panel1;
        private RadioButton rdo3;
        private RadioButton rdo6;
        private TextBox txtStartMultiple;
        private TextBox txtTH;
        private TextBox txtTM;
        private TextBox txtXInterval;
        private TextBox txtYInterval;
        private TextBox txtYOffset;

        public TFInfoPage()
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
                num = double.Parse(this.txtXInterval.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            try
            {
                num2 = double.Parse(this.txtYInterval.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            try
            {
                num3 = double.Parse(this.txtStartMultiple.Text);
            }
            catch
            {
                MessageBox.Show("请输入数字！");
                return false;
            }
            if (this.jlktkassiatant_0.TKType == TKType.TKStandard)
            {
                if (this.txtTH.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请输入图幅编号！");
                    return false;
                }
                if (!THTools.ValidateTFNo(this.txtTH.Text.Trim()))
                {
                    MessageBox.Show("图幅编号不正确！");
                    return false;
                }
            }
            this.jlktkassiatant_0.XInterval = num;
            this.jlktkassiatant_0.YInterval = num2;
            this.jlktkassiatant_0.StartCoodinateMultiple = num3;
            this.jlktkassiatant_0.MapTM = this.txtTM.Text;
            this.jlktkassiatant_0.MapTH = this.txtTH.Text;
            this.jlktkassiatant_0.StripType = this.rdo3.Checked ? StripType.STThreeDeg : StripType.STSixDeg;
            return true;
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
            if (this.jlktkassiatant_0.TKType == TKType.TKStandard)
            {
                if (!this.groupSR.Visible)
                {
                    this.groupSR.Visible = true;
                    this.panel1.Location = new Point(11, 200);
                }
            }
            else if (this.jlktkassiatant_0.TKType == TKType.TKRand)
            {
                this.groupSR.Visible = false;
                this.panel1.Location = new Point(11, 0x4a);
            }
            this.txtTM.Text = this.jlktkassiatant_0.MapTM;
            this.txtTH.Text = this.jlktkassiatant_0.MapTH;
            this.txtXInterval.Text = this.jlktkassiatant_0.XInterval.ToString();
            this.txtYInterval.Text = this.jlktkassiatant_0.YInterval.ToString();
            this.txtStartMultiple.Text = this.jlktkassiatant_0.StartCoodinateMultiple.ToString();
            if (this.jlktkassiatant_0.StripType == StripType.STThreeDeg)
            {
                this.rdo3.Checked = true;
                this.rdo6.Checked = false;
            }
            else
            {
                this.rdo6.Checked = true;
                this.rdo3.Checked = false;
            }
            if (this.jlktkassiatant_0.SpheroidType == SpheroidType.Xian1980)
            {
                this.cboDataum.SelectedIndex = 1;
            }
            else
            {
                this.cboDataum.SelectedIndex = 0;
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.txtTM = new TextBox();
            this.txtTH = new TextBox();
            this.groupSR = new GroupBox();
            this.cboDataum = new ComboBox();
            this.txtYOffset = new TextBox();
            this.rdo6 = new RadioButton();
            this.rdo3 = new RadioButton();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label6 = new Label();
            this.label5 = new Label();
            this.txtYInterval = new TextBox();
            this.txtXInterval = new TextBox();
            this.label7 = new Label();
            this.label8 = new Label();
            this.txtStartMultiple = new TextBox();
            this.label9 = new Label();
            this.panel1 = new Panel();
            this.groupSR.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图名";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(11, 0x31);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "图号";
            this.txtTM.Location = new Point(0x58, 20);
            this.txtTM.Name = "txtTM";
            this.txtTM.Size = new Size(190, 0x15);
            this.txtTM.TabIndex = 2;
            this.txtTH.Location = new Point(0x58, 0x2f);
            this.txtTH.Name = "txtTH";
            this.txtTH.Size = new Size(190, 0x15);
            this.txtTH.TabIndex = 3;
            this.groupSR.Controls.Add(this.cboDataum);
            this.groupSR.Controls.Add(this.txtYOffset);
            this.groupSR.Controls.Add(this.rdo6);
            this.groupSR.Controls.Add(this.rdo3);
            this.groupSR.Controls.Add(this.label4);
            this.groupSR.Controls.Add(this.label3);
            this.groupSR.Location = new Point(11, 0x4a);
            this.groupSR.Name = "groupSR";
            this.groupSR.Size = new Size(0x121, 120);
            this.groupSR.TabIndex = 4;
            this.groupSR.TabStop = false;
            this.groupSR.Text = "投影坐标系统参数";
            this.cboDataum.FormattingEnabled = true;
            this.cboDataum.Items.AddRange(new object[] { "北京54", "西安80" });
            this.cboDataum.Location = new Point(0x63, 0x1c);
            this.cboDataum.Name = "cboDataum";
            this.cboDataum.Size = new Size(0xa5, 20);
            this.cboDataum.TabIndex = 5;
            this.cboDataum.Text = "西安80";
            this.txtYOffset.Location = new Point(0x63, 0x38);
            this.txtYOffset.Name = "txtYOffset";
            this.txtYOffset.Size = new Size(0xa6, 0x15);
            this.txtYOffset.TabIndex = 4;
            this.txtYOffset.Visible = false;
            this.rdo6.AutoSize = true;
            this.rdo6.Location = new Point(0x8b, 0x52);
            this.rdo6.Name = "rdo6";
            this.rdo6.Size = new Size(0x41, 0x10);
            this.rdo6.TabIndex = 3;
            this.rdo6.Text = "6度分带";
            this.rdo6.UseVisualStyleBackColor = true;
            this.rdo3.AutoSize = true;
            this.rdo3.Checked = true;
            this.rdo3.Location = new Point(0x12, 0x56);
            this.rdo3.Name = "rdo3";
            this.rdo3.Size = new Size(0x41, 0x10);
            this.rdo3.TabIndex = 2;
            this.rdo3.TabStop = true;
            this.rdo3.Text = "3度分带";
            this.rdo3.UseVisualStyleBackColor = true;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(14, 0x3d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x53, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "坐标偏移参数:";
            this.label4.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(14, 0x20);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "参考椭球:";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(270, 0x39);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x11, 12);
            this.label6.TabIndex = 0x13;
            this.label6.Text = "米";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(270, 15);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x11, 12);
            this.label5.TabIndex = 0x12;
            this.label5.Text = "米";
            this.txtYInterval.Location = new Point(0x61, 0x30);
            this.txtYInterval.Name = "txtYInterval";
            this.txtYInterval.Size = new Size(0xa5, 0x15);
            this.txtYInterval.TabIndex = 0x11;
            this.txtXInterval.Location = new Point(0x61, 13);
            this.txtXInterval.Name = "txtXInterval";
            this.txtXInterval.Size = new Size(0xa5, 0x15);
            this.txtXInterval.TabIndex = 0x10;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(4, 0x33);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x4d, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "格网竖直间距";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(4, 0x16);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x4d, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "格网水平间距";
            this.txtStartMultiple.Location = new Point(0x63, 0x57);
            this.txtStartMultiple.Name = "txtStartMultiple";
            this.txtStartMultiple.Size = new Size(0xa3, 0x15);
            this.txtStartMultiple.TabIndex = 0x15;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(4, 90);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x59, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "起始点坐标倍数";
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtStartMultiple);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtXInterval);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtYInterval);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new Point(13, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x130, 0x7e);
            this.panel1.TabIndex = 0x16;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.groupSR);
            base.Controls.Add(this.txtTH);
            base.Controls.Add(this.txtTM);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "TFInfoPage";
            base.Size = new Size(0x152, 0x155);
            base.Load += new EventHandler(this.TFInfoPage_Load);
            this.groupSR.ResumeLayout(false);
            this.groupSR.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void TFInfoPage_Load(object sender, EventArgs e)
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

