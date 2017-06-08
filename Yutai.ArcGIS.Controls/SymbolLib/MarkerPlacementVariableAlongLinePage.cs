using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class MarkerPlacementVariableAlongLinePage : MarkerPlacementBaseControl
    {
        private Button btnChangeEffic;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private IContainer components = null;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private bool m_CanDo = false;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;

        public MarkerPlacementVariableAlongLinePage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            base.m_pControl = pControl;
        }

        public override bool Apply()
        {
            double val = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            try
            {
                val = double.Parse(this.textBox1.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            try
            {
                num2 = double.Parse(this.textBox2.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            try
            {
                num3 = double.Parse(this.textBox3.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            try
            {
                num4 = double.Parse(this.textBox4.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            try
            {
                num5 = double.Parse(this.textBox5.Text);
            }
            catch
            {
                MessageBox.Show("请出入数字型数据！");
                return false;
            }
            base.m_pGeometricEffect.set_Value((int) this.textBox1.Tag, val);
            base.m_pGeometricEffect.set_Value((int) this.textBox2.Tag, num2);
            base.m_pGeometricEffect.set_Value((int) this.textBox3.Tag, num3);
            base.m_pGeometricEffect.set_Value((int) this.textBox4.Tag, num4);
            base.m_pGeometricEffect.set_Value((int) this.textBox5.Tag, num5);
            base.m_pGeometricEffect.set_Value((int) this.comboBox1.Tag, this.comboBox1.SelectedIndex);
            base.m_pGeometricEffect.set_Value((int) this.comboBox2.Tag, this.comboBox3.SelectedIndex);
            base.m_pGeometricEffect.set_Value((int) this.comboBox3.Tag, this.comboBox3.SelectedIndex);
            return true;
        }

        private void btnChangeEffic_Click(object sender, EventArgs e)
        {
            frmMarkerPlacementList list = new frmMarkerPlacementList {
                BasicSymbolLayerBaseControl = base.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (base.m_pControl != null))
            {
                base.m_pControl.ReplaceControl(this, list.SelectControl);
                base.m_pBasicMarkerSymbol.MarkerPlacement = (list.SelectControl as MarkerPlacementBaseControl).GeometricEffect as IMarkerPlacement;
                (list.SelectControl as MarkerPlacementBaseControl).BasicMarkerSymbol = base.m_pBasicMarkerSymbol;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                base.m_pGeometricEffect.set_Value((int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
            }
        }

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
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerPlacementVariableAlongLinePage));
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.comboBox2 = new ComboBox();
            this.comboBox1 = new ComboBox();
            this.textBox5 = new TextBox();
            this.label9 = new Label();
            this.textBox4 = new TextBox();
            this.label8 = new Label();
            this.textBox3 = new TextBox();
            this.label7 = new Label();
            this.textBox2 = new TextBox();
            this.label6 = new Label();
            this.comboBox3 = new ComboBox();
            this.label5 = new Label();
            this.label4 = new Label();
            this.label3 = new Label();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.btnChangeEffic = new Button();
            this.imageList1 = new ImageList(this.components);
            this.panel2 = new Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.ForeColor = SystemColors.ActiveCaptionText;
            this.label1.Location = new Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "可变大小";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xd4, 0xd6);
            this.panel1.TabIndex = 1;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] { "标记", "半个空白" });
            this.comboBox2.Location = new Point(0x4a, 0x37);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x80, 20);
            this.comboBox2.TabIndex = 20;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "标记", "空白", "半个空白" });
            this.comboBox1.Location = new Point(0x4a, 0x20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x80, 20);
            this.comboBox1.TabIndex = 0x13;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.textBox5.Location = new Point(0x4a, 0xbd);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(0x80, 0x15);
            this.textBox5.TabIndex = 0x12;
            this.textBox5.Text = "0";
            this.textBox5.Leave += new EventHandler(this.textBox1_Leave);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(12, 0xc0);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x23, 12);
            this.label9.TabIndex = 0x11;
            this.label9.Text = "偏移:";
            this.textBox4.Location = new Point(0x4a, 0x85);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(0x80, 0x15);
            this.textBox4.TabIndex = 0x10;
            this.textBox4.Text = "3";
            this.textBox4.Leave += new EventHandler(this.textBox1_Leave);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(12, 0x88);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x3b, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "缩放倍数:";
            this.textBox3.Location = new Point(0x4a, 0x6b);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(0x80, 0x15);
            this.textBox3.TabIndex = 14;
            this.textBox3.Text = "1.5";
            this.textBox3.Leave += new EventHandler(this.textBox1_Leave);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(12, 110);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x23, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "最大:";
            this.textBox2.Location = new Point(0x4a, 80);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x80, 0x15);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "0.5";
            this.textBox2.Leave += new EventHandler(this.textBox1_Leave);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(12, 0x53);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x23, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "最小:";
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] { "随机", "增加", "减小", "先增加在减小" });
            this.comboBox3.Location = new Point(0x4a, 0xa1);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(0x80, 20);
            this.comboBox3.TabIndex = 10;
            this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 0xa9);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "方法:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0x3e);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x2f, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "控制点:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x23);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "端点:";
            this.textBox1.Location = new Point(0x4a, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x80, 0x15);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "1";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 7);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "步长:";
            this.btnChangeEffic.ImageIndex = 0;
            this.btnChangeEffic.ImageList = this.imageList1;
            this.btnChangeEffic.Location = new Point(0xc0, 0);
            this.btnChangeEffic.Name = "btnChangeEffic";
            this.btnChangeEffic.RightToLeft = RightToLeft.Yes;
            this.btnChangeEffic.Size = new Size(0x10, 0x10);
            this.btnChangeEffic.TabIndex = 2;
            this.btnChangeEffic.UseVisualStyleBackColor = true;
            this.btnChangeEffic.Click += new EventHandler(this.btnChangeEffic_Click);
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.White;
            this.imageList1.Images.SetKeyName(0, "Bitmap4.bmp");
            this.imageList1.Images.SetKeyName(1, "Bitmap5.bmp");
            this.panel2.BackColor = SystemColors.ControlDark;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnChangeEffic);
            this.panel2.Location = new Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xd1, 0x10);
            this.panel2.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "MarkerPlacementVariableAlongLinePage";
            base.Size = new Size(0xd4, 0xe8);
            base.Load += new EventHandler(this.MarkerPlacementVariableAlongLinePage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void MarkerPlacementVariableAlongLinePage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new MarkerPlacementVariableAlongLineClass();
            }
            int graphicAttributeCount = base.m_pGeometricEffect.GraphicAttributeCount;
            int attrId = base.m_pGeometricEffect.get_ID(0);
            string str = base.m_pGeometricEffect.get_Name(attrId);
            object obj2 = base.m_pGeometricEffect.get_Value(attrId);
            if (obj2 is double[])
            {
                double[] numArray = obj2 as double[];
                string str2 = numArray[0].ToString();
                for (int i = 1; i < numArray.Length; i++)
                {
                    str2 = str2 + "," + numArray[i].ToString();
                }
                this.textBox1.Text = str2;
            }
            else
            {
                this.textBox1.Text = base.m_pGeometricEffect.get_Value(attrId).ToString();
            }
            this.textBox1.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(1);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.comboBox1.SelectedIndex = (int) base.m_pGeometricEffect.get_Value(attrId);
            this.comboBox1.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(2);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.comboBox2.SelectedIndex = (int) base.m_pGeometricEffect.get_Value(attrId);
            this.comboBox2.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(3);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.textBox2.Text = base.m_pGeometricEffect.get_Value(attrId).ToString();
            this.textBox2.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(4);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.textBox3.Text = base.m_pGeometricEffect.get_Value(attrId).ToString();
            this.textBox3.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(5);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.textBox4.Text = base.m_pGeometricEffect.get_Value(attrId).ToString();
            this.textBox4.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(6);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.comboBox3.SelectedIndex = (int) base.m_pGeometricEffect.get_Value(attrId);
            this.comboBox3.Tag = attrId;
            attrId = base.m_pGeometricEffect.get_ID(7);
            str = base.m_pGeometricEffect.get_Name(attrId);
            this.textBox5.Text = base.m_pGeometricEffect.get_Value(attrId).ToString();
            this.textBox5.Tag = attrId;
            this.m_CanDo = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (sender == this.textBox1)
            {
                string[] strArray = (sender as TextBox).Text.Split(new char[] { ',' });
                double[] val = new double[strArray.Length];
                for (int i = 0; i < val.Length; i++)
                {
                    try
                    {
                        val[i] = double.Parse(strArray[i]);
                    }
                    catch
                    {
                        return;
                    }
                }
                base.m_pGeometricEffect.set_Value((int) (sender as TextBox).Tag, val);
            }
            else
            {
                double num2 = 0.0;
                try
                {
                    num2 = double.Parse((sender as TextBox).Text);
                }
                catch
                {
                    return;
                }
                base.m_pGeometricEffect.set_Value((int) (sender as TextBox).Tag, num2);
            }
        }
    }
}

