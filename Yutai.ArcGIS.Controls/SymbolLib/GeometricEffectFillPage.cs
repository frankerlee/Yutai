using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class GeometricEffectFillPage : GeometricEffectBaseControl
    {
        private Button btnAddGemoetricEffic;
        private Button btnChangeEffic;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem gradientToolStripMenuItem;
        private SymbolItem HatchsymbolItem;
        private ToolStripMenuItem hatchToolStripMenuItem;
        private ImageList imageList1;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private bool m_CanDo = false;
        private BasicSymbolLayerBaseControl m_pControl = null;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private ToolStripMenuItem solidColorToolStripMenuItem;
        private SymbolItem symbolItem1;
        private SymbolItem symbolItemColor1;
        private SymbolItem symbolItemColor2;
        private TextBox txtAngle;
        private TextBox txtAngleGrad;
        private TextBox txtInterval;
        private TextBox txtOffset;
        private TextBox txtPercent;
        private TextBox txtStep;
        private TextBox txtWidth;

        public GeometricEffectFillPage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            base.m_pGeometricEffect = new BasicFillSymbolClass();
            this.m_pControl = pControl;
        }

        public override bool Apply()
        {
            IGraphicAttributes fillPattern = (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes;
            if (fillPattern.ClassName == "1")
            {
                fillPattern.set_Value((int) this.symbolItem1.Tag, this.symbolItem1.Symbol);
            }
            else
            {
                double num;
                double num2;
                if (fillPattern.ClassName == "2")
                {
                    num = 0.0;
                    num2 = 0.0;
                    double val = 0.0;
                    double num4 = 0.0;
                    try
                    {
                        num = double.Parse(this.txtWidth.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        num2 = double.Parse(this.txtAngle.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        val = double.Parse(this.txtStep.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        num4 = double.Parse(this.txtOffset.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    fillPattern.set_Value((int) this.HatchsymbolItem.Tag, this.HatchsymbolItem.Symbol);
                    fillPattern.set_Value((int) this.txtWidth.Tag, num);
                    fillPattern.set_Value((int) this.txtAngle.Tag, num2);
                    fillPattern.set_Value((int) this.txtStep.Tag, val);
                    fillPattern.set_Value((int) this.txtOffset.Tag, num4);
                }
                else if (fillPattern.ClassName == "3")
                {
                    num = 0.0;
                    double num5 = 0.0;
                    num2 = 0.0;
                    try
                    {
                        num = double.Parse(this.txtInterval.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        num5 = double.Parse(this.txtInterval.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    try
                    {
                        num2 = double.Parse(this.txtAngle.Text);
                    }
                    catch
                    {
                        MessageBox.Show("请出入数字型数据！");
                        return false;
                    }
                    if ((this.comboBox1.SelectedIndex == -1) || (this.comboBox2.SelectedIndex == -1))
                    {
                        return false;
                    }
                    fillPattern.set_Value((int) this.symbolItemColor1.Tag, this.symbolItemColor1.Symbol);
                    fillPattern.set_Value((int) this.symbolItemColor2.Tag, this.symbolItemColor2.Symbol);
                    fillPattern.set_Value((int) this.comboBox1.Tag, this.comboBox1.SelectedIndex);
                    fillPattern.set_Value((int) this.comboBox2.Tag, this.comboBox2.SelectedIndex);
                    fillPattern.set_Value((int) this.txtInterval.Tag, num);
                    fillPattern.set_Value((int) this.txtAngleGrad.Tag, num2);
                    fillPattern.set_Value((int) this.txtPercent.Tag, num5);
                }
            }
            return true;
        }

        private void btnAddGemoetricEffic_Click(object sender, EventArgs e)
        {
            frmGeometricEffectList list = new frmGeometricEffectList {
                BasicSymbolLayerBaseControl = this.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (this.m_pControl != null))
            {
                this.m_pControl.AddControl(this, list.SelectControl);
            }
        }

        private void btnChangeEffic_Click(object sender, EventArgs e)
        {
            this.contextMenuStrip1.Show(this, this.btnChangeEffic.Right, this.btnChangeEffic.Bottom);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value((int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value((int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
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

        private void GeometricEffectFillPage_Load(object sender, EventArgs e)
        {
            int num2;
            string str;
            int num3;
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new BasicFillSymbolClass();
            }
            IGraphicAttributes fillPattern = (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes;
            switch (fillPattern.GraphicAttributeCount)
            {
                case 1:
                    num2 = fillPattern.get_ID(0);
                    str = fillPattern.get_Name(num2);
                    this.symbolItem1.Symbol = fillPattern.get_Value(num2);
                    this.symbolItem1.Tag = num2;
                    this.panel1.Visible = true;
                    this.panel4.Visible = false;
                    this.panel3.Visible = false;
                    num3 = this.panel2.Bottom + this.panel1.Height;
                    base.Size = new Size(base.Width, num3);
                    this.solidColorToolStripMenuItem.Checked = true;
                    break;

                case 5:
                    this.panel1.Visible = false;
                    this.panel4.Visible = true;
                    this.panel3.Visible = false;
                    num3 = this.panel2.Bottom + this.panel4.Height;
                    base.Size = new Size(base.Width, num3);
                    num2 = fillPattern.get_ID(0);
                    str = fillPattern.get_Name(num2);
                    this.HatchsymbolItem.Symbol = fillPattern.get_Value(num2);
                    this.HatchsymbolItem.Tag = num2;
                    num2 = fillPattern.get_ID(1);
                    str = fillPattern.get_Name(num2);
                    this.txtWidth.Text = fillPattern.get_Value(num2).ToString();
                    this.txtWidth.Tag = num2;
                    num2 = fillPattern.get_ID(2);
                    str = fillPattern.get_Name(num2);
                    this.txtAngle.Text = fillPattern.get_Value(num2).ToString();
                    this.txtAngle.Tag = num2;
                    num2 = fillPattern.get_ID(3);
                    str = fillPattern.get_Name(num2);
                    this.txtStep.Text = fillPattern.get_Value(num2).ToString();
                    this.txtStep.Tag = num2;
                    num2 = fillPattern.get_ID(4);
                    str = fillPattern.get_Name(num2);
                    this.txtOffset.Text = fillPattern.get_Value(num2).ToString();
                    this.txtOffset.Tag = num2;
                    this.hatchToolStripMenuItem.Checked = true;
                    break;

                case 7:
                    this.panel1.Visible = false;
                    this.panel4.Visible = false;
                    this.panel3.Visible = true;
                    num3 = this.panel2.Bottom + this.panel3.Height;
                    base.Size = new Size(base.Width, num3);
                    num2 = fillPattern.get_ID(0);
                    str = fillPattern.get_Name(num2);
                    this.symbolItemColor1.Symbol = fillPattern.get_Value(num2);
                    this.symbolItemColor1.Tag = num2;
                    num2 = fillPattern.get_ID(1);
                    str = fillPattern.get_Name(num2);
                    this.symbolItemColor2.Symbol = fillPattern.get_Value(num2);
                    this.symbolItemColor2.Tag = num2;
                    num2 = fillPattern.get_ID(2);
                    str = fillPattern.get_Name(num2);
                    this.comboBox1.SelectedIndex = (int) fillPattern.get_Value(num2);
                    this.comboBox1.Tag = num2;
                    num2 = fillPattern.get_ID(3);
                    str = fillPattern.get_Name(num2);
                    this.comboBox2.SelectedIndex = (int) fillPattern.get_Value(num2);
                    this.comboBox2.Tag = num2;
                    num2 = fillPattern.get_ID(4);
                    str = fillPattern.get_Name(num2);
                    this.txtInterval.Text = fillPattern.get_Value(num2).ToString();
                    this.txtInterval.Tag = num2;
                    num2 = fillPattern.get_ID(5);
                    str = fillPattern.get_Name(num2);
                    this.txtPercent.Text = fillPattern.get_Value(num2).ToString();
                    this.txtPercent.Tag = num2;
                    num2 = fillPattern.get_ID(6);
                    str = fillPattern.get_Name(num2);
                    this.txtAngleGrad.Text = fillPattern.get_Value(num2).ToString();
                    this.txtAngleGrad.Tag = num2;
                    this.gradientToolStripMenuItem.Checked = true;
                    break;
            }
            this.m_CanDo = true;
        }

        private void gradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            if (!this.gradientToolStripMenuItem.Checked)
            {
                this.gradientToolStripMenuItem.Checked = true;
                this.solidColorToolStripMenuItem.Checked = false;
                this.hatchToolStripMenuItem.Checked = false;
                IFillPattern pattern = new GradientPatternClass();
                IGraphicAttributes attributes = pattern as IGraphicAttributes;
                this.panel1.Visible = false;
                this.panel4.Visible = false;
                this.panel3.Visible = true;
                int height = this.panel2.Bottom + this.panel3.Height;
                base.Size = new Size(base.Width, height);
                int attrId = attributes.get_ID(0);
                string str = attributes.get_Name(attrId);
                this.symbolItemColor1.Symbol = attributes.get_Value(attrId);
                this.symbolItemColor1.Tag = attrId;
                attrId = attributes.get_ID(1);
                str = attributes.get_Name(attrId);
                this.symbolItemColor2.Symbol = attributes.get_Value(attrId);
                this.symbolItemColor2.Tag = attrId;
                attrId = attributes.get_ID(2);
                str = attributes.get_Name(attrId);
                this.comboBox1.Tag = attrId;
                this.comboBox1.SelectedIndex = (int) attributes.get_Value(attrId);
                attrId = attributes.get_ID(3);
                str = attributes.get_Name(attrId);
                this.comboBox2.Tag = attrId;
                this.comboBox2.SelectedIndex = (int) attributes.get_Value(attrId);
                attrId = attributes.get_ID(4);
                str = attributes.get_Name(attrId);
                this.txtInterval.Tag = attrId;
                this.txtInterval.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(5);
                str = attributes.get_Name(attrId);
                this.txtPercent.Tag = attrId;
                this.txtPercent.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(5);
                str = attributes.get_Name(attrId);
                this.txtAngleGrad.Tag = attrId;
                this.txtAngleGrad.Text = attributes.get_Value(attrId).ToString();
                try
                {
                    (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern = pattern;
                }
                catch
                {
                }
            }
            this.m_CanDo = true;
        }

        private void HatchsymbolItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            IRgbColor rgbColor = new RgbColorClass {
                RGB = (this.HatchsymbolItem.Symbol as IColor).RGB
            };
            dialog.Color = Converter.FromRGBColor(rgbColor);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rgbColor = Converter.ToRGBColor(dialog.Color);
                this.HatchsymbolItem.Symbol = rgbColor;
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value((int) this.HatchsymbolItem.Tag, this.HatchsymbolItem.Symbol);
            }
        }

        private void HatchsymbolItem_Load(object sender, EventArgs e)
        {
        }

        private void hatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            if (!this.hatchToolStripMenuItem.Checked)
            {
                this.hatchToolStripMenuItem.Checked = true;
                this.solidColorToolStripMenuItem.Checked = false;
                this.gradientToolStripMenuItem.Checked = false;
                IFillPattern pattern = new LinePatternClass();
                IGraphicAttributes attributes = pattern as IGraphicAttributes;
                this.panel1.Visible = false;
                this.panel4.Visible = true;
                this.panel3.Visible = false;
                int height = this.panel2.Bottom + this.panel4.Height;
                base.Size = new Size(base.Width, height);
                int attrId = attributes.get_ID(0);
                string str = attributes.get_Name(attrId);
                this.HatchsymbolItem.Tag = attrId;
                this.HatchsymbolItem.Symbol = attributes.get_Value(attrId);
                attrId = attributes.get_ID(1);
                str = attributes.get_Name(attrId);
                this.txtWidth.Tag = attrId;
                this.txtWidth.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(2);
                str = attributes.get_Name(attrId);
                this.txtAngle.Tag = attrId;
                this.txtAngle.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(3);
                str = attributes.get_Name(attrId);
                this.txtStep.Tag = attrId;
                this.txtStep.Text = attributes.get_Value(attrId).ToString();
                attrId = attributes.get_ID(4);
                str = attributes.get_Name(attrId);
                this.txtOffset.Tag = attrId;
                this.txtOffset.Text = attributes.get_Value(attrId).ToString();
                try
                {
                    (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern = pattern;
                }
                catch
                {
                }
            }
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeometricEffectFillPage));
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.symbolItem1 = new SymbolItem();
            this.label3 = new Label();
            this.btnChangeEffic = new Button();
            this.imageList1 = new ImageList(this.components);
            this.panel2 = new Panel();
            this.btnAddGemoetricEffic = new Button();
            this.panel3 = new Panel();
            this.comboBox2 = new ComboBox();
            this.comboBox1 = new ComboBox();
            this.label13 = new Label();
            this.label14 = new Label();
            this.txtAngleGrad = new TextBox();
            this.label10 = new Label();
            this.txtPercent = new TextBox();
            this.label11 = new Label();
            this.txtInterval = new TextBox();
            this.label12 = new Label();
            this.symbolItemColor2 = new SymbolItem();
            this.label9 = new Label();
            this.symbolItemColor1 = new SymbolItem();
            this.label2 = new Label();
            this.panel4 = new Panel();
            this.txtOffset = new TextBox();
            this.label8 = new Label();
            this.txtStep = new TextBox();
            this.label7 = new Label();
            this.txtAngle = new TextBox();
            this.label6 = new Label();
            this.txtWidth = new TextBox();
            this.label5 = new Label();
            this.HatchsymbolItem = new SymbolItem();
            this.label4 = new Label();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.solidColorToolStripMenuItem = new ToolStripMenuItem();
            this.hatchToolStripMenuItem = new ToolStripMenuItem();
            this.gradientToolStripMenuItem = new ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.ForeColor = SystemColors.ActiveCaptionText;
            this.label1.Location = new Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "面";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.symbolItem1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new Point(0, 0x21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xd4, 0x2c);
            this.panel1.TabIndex = 1;
            this.symbolItem1.BackColor = SystemColors.ActiveCaptionText;
            this.symbolItem1.Location = new Point(0x42, 9);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0x7a, 30);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 11;
            this.symbolItem1.Click += new EventHandler(this.symbolItem1_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 0x10);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "颜色:";
            this.btnChangeEffic.ImageIndex = 0;
            this.btnChangeEffic.ImageList = this.imageList1;
            this.btnChangeEffic.Location = new Point(0xc1, 0);
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
            this.panel2.Location = new Point(3, 0x10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xd1, 0x10);
            this.panel2.TabIndex = 3;
            this.btnAddGemoetricEffic.ImageIndex = 1;
            this.btnAddGemoetricEffic.ImageList = this.imageList1;
            this.btnAddGemoetricEffic.Location = new Point(0xc4, 0);
            this.btnAddGemoetricEffic.Name = "btnAddGemoetricEffic";
            this.btnAddGemoetricEffic.RightToLeft = RightToLeft.Yes;
            this.btnAddGemoetricEffic.Size = new Size(0x10, 0x10);
            this.btnAddGemoetricEffic.TabIndex = 4;
            this.btnAddGemoetricEffic.UseVisualStyleBackColor = true;
            this.btnAddGemoetricEffic.Click += new EventHandler(this.btnAddGemoetricEffic_Click);
            this.panel3.BackColor = SystemColors.Window;
            this.panel3.Controls.Add(this.comboBox2);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.txtAngleGrad);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtPercent);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.txtInterval);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.symbolItemColor2);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.symbolItemColor1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new Point(0, 0x21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0xd4, 200);
            this.panel3.TabIndex = 12;
            this.panel3.Visible = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] { "Linear", "Rectangle", "Circle", "Buffer" });
            this.comboBox2.Location = new Point(0x51, 0x56);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x77, 20);
            this.comboBox2.TabIndex = 0x1d;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "HSV", "CIE Lab", "LabLCH" });
            this.comboBox1.Location = new Point(0x51, 0x3d);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x77, 20);
            this.comboBox1.TabIndex = 0x1c;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label13.AutoSize = true;
            this.label13.Location = new Point(6, 0x59);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x23, 12);
            this.label13.TabIndex = 0x1b;
            this.label13.Text = "样式:";
            this.label14.AutoSize = true;
            this.label14.Location = new Point(6, 0x40);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x23, 12);
            this.label14.TabIndex = 0x1a;
            this.label14.Text = "算法:";
            this.txtAngleGrad.Location = new Point(0x51, 0xa3);
            this.txtAngleGrad.Name = "txtAngleGrad";
            this.txtAngleGrad.Size = new Size(0x77, 0x15);
            this.txtAngleGrad.TabIndex = 0x19;
            this.txtAngleGrad.Text = "0";
            this.txtAngleGrad.Leave += new EventHandler(this.txtAngleGrad_Leave);
            this.label10.AutoSize = true;
            this.label10.Location = new Point(3, 0xa6);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x23, 12);
            this.label10.TabIndex = 0x18;
            this.label10.Text = "角度:";
            this.txtPercent.Location = new Point(0x51, 0x89);
            this.txtPercent.Name = "txtPercent";
            this.txtPercent.Size = new Size(0x77, 0x15);
            this.txtPercent.TabIndex = 0x17;
            this.txtPercent.Text = "0";
            this.txtPercent.Leave += new EventHandler(this.txtAngleGrad_Leave);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(3, 140);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x2f, 12);
            this.label11.TabIndex = 0x16;
            this.label11.Text = "百分比:";
            this.txtInterval.Location = new Point(0x51, 0x6f);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new Size(0x77, 0x15);
            this.txtInterval.TabIndex = 0x15;
            this.txtInterval.Text = "0";
            this.txtInterval.Leave += new EventHandler(this.txtAngleGrad_Leave);
            this.label12.AutoSize = true;
            this.label12.Location = new Point(3, 0x72);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x23, 12);
            this.label12.TabIndex = 20;
            this.label12.Text = "间隔:";
            this.symbolItemColor2.BackColor = SystemColors.ActiveCaptionText;
            this.symbolItemColor2.Location = new Point(0x51, 0x25);
            this.symbolItemColor2.Name = "symbolItemColor2";
            this.symbolItemColor2.Size = new Size(0x77, 0x13);
            this.symbolItemColor2.Symbol = null;
            this.symbolItemColor2.TabIndex = 13;
            this.symbolItemColor2.Click += new EventHandler(this.symbolItemColor2_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(6, 0x25);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x29, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "颜色2:";
            this.symbolItemColor1.BackColor = SystemColors.ActiveCaptionText;
            this.symbolItemColor1.Location = new Point(0x4f, 9);
            this.symbolItemColor1.Name = "symbolItemColor1";
            this.symbolItemColor1.Size = new Size(0x77, 0x13);
            this.symbolItemColor1.Symbol = null;
            this.symbolItemColor1.TabIndex = 11;
            this.symbolItemColor1.Click += new EventHandler(this.symbolItemColor1_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "颜色1:";
            this.panel4.BackColor = SystemColors.Window;
            this.panel4.Controls.Add(this.txtOffset);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.txtStep);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.txtAngle);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.txtWidth);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.HatchsymbolItem);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new Point(0, 0x21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0xd4, 0x98);
            this.panel4.TabIndex = 13;
            this.panel4.Visible = false;
            this.txtOffset.Location = new Point(0x51, 0x80);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new Size(0x77, 0x15);
            this.txtOffset.TabIndex = 0x13;
            this.txtOffset.Text = "0";
            this.txtOffset.Leave += new EventHandler(this.txtAngleGrad_Leave);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(1, 0x83);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x23, 12);
            this.label8.TabIndex = 0x12;
            this.label8.Text = "偏移:";
            this.txtStep.Location = new Point(0x51, 0x69);
            this.txtStep.Name = "txtStep";
            this.txtStep.Size = new Size(0x77, 0x15);
            this.txtStep.TabIndex = 0x11;
            this.txtStep.Text = "0";
            this.txtStep.Leave += new EventHandler(this.txtAngleGrad_Leave);
            this.label7.AutoSize = true;
            this.label7.Location = new Point(3, 0x6c);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x23, 12);
            this.label7.TabIndex = 0x10;
            this.label7.Text = "步长:";
            this.txtAngle.Location = new Point(0x51, 0x4d);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new Size(0x77, 0x15);
            this.txtAngle.TabIndex = 15;
            this.txtAngle.Text = "0";
            this.txtAngle.Leave += new EventHandler(this.txtAngleGrad_Leave);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(3, 80);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x23, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "角度:";
            this.txtWidth.Location = new Point(0x51, 50);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new Size(0x77, 0x15);
            this.txtWidth.TabIndex = 13;
            this.txtWidth.Text = "0";
            this.txtWidth.Leave += new EventHandler(this.txtAngleGrad_Leave);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(3, 0x35);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "宽度:";
            this.HatchsymbolItem.BackColor = SystemColors.ActiveCaptionText;
            this.HatchsymbolItem.Location = new Point(0x4f, 14);
            this.HatchsymbolItem.Name = "HatchsymbolItem";
            this.HatchsymbolItem.Size = new Size(0x77, 30);
            this.HatchsymbolItem.Symbol = null;
            this.HatchsymbolItem.TabIndex = 11;
            this.HatchsymbolItem.Load += new EventHandler(this.HatchsymbolItem_Load);
            this.HatchsymbolItem.Click += new EventHandler(this.HatchsymbolItem_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 0x10);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "颜色:";
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.solidColorToolStripMenuItem, this.hatchToolStripMenuItem, this.gradientToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x7d, 70);
            this.solidColorToolStripMenuItem.Name = "solidColorToolStripMenuItem";
            this.solidColorToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.solidColorToolStripMenuItem.Text = "Solid Color";
            this.solidColorToolStripMenuItem.Click += new EventHandler(this.solidColorToolStripMenuItem_Click);
            this.hatchToolStripMenuItem.Name = "hatchToolStripMenuItem";
            this.hatchToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.hatchToolStripMenuItem.Text = "Hatch";
            this.hatchToolStripMenuItem.Click += new EventHandler(this.hatchToolStripMenuItem_Click);
            this.gradientToolStripMenuItem.Name = "gradientToolStripMenuItem";
            this.gradientToolStripMenuItem.Size = new Size(0x7c, 0x16);
            this.gradientToolStripMenuItem.Text = "Gradient";
            this.gradientToolStripMenuItem.Click += new EventHandler(this.gradientToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.panel4);
            base.Controls.Add(this.panel3);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.btnAddGemoetricEffic);
            base.Controls.Add(this.panel1);
            base.Name = "GeometricEffectFillPage";
            base.Size = new Size(0xd4, 0xe8);
            base.Load += new EventHandler(this.GeometricEffectFillPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void solidColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.m_CanDo = false;
            if (!this.solidColorToolStripMenuItem.Checked)
            {
                this.solidColorToolStripMenuItem.Checked = true;
                this.hatchToolStripMenuItem.Checked = false;
                this.gradientToolStripMenuItem.Checked = false;
                IFillPattern pattern = new SolidColorPatternClass();
                IGraphicAttributes attributes = pattern as IGraphicAttributes;
                int attrId = attributes.get_ID(0);
                string str = attributes.get_Name(attrId);
                this.symbolItem1.Tag = attrId;
                this.symbolItem1.Symbol = attributes.get_Value(attrId);
                this.panel1.Visible = true;
                this.panel4.Visible = false;
                this.panel3.Visible = false;
                int height = this.panel2.Bottom + this.panel1.Height;
                base.Size = new Size(base.Width, height);
                try
                {
                    (base.m_pGeometricEffect as IBasicFillSymbol).FillPattern = pattern;
                }
                catch
                {
                }
            }
            this.m_CanDo = true;
        }

        private void symbolItem1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            IRgbColor rgbColor = new RgbColorClass {
                RGB = (this.symbolItem1.Symbol as IColor).RGB
            };
            dialog.Color = Converter.FromRGBColor(rgbColor);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rgbColor = Converter.ToRGBColor(dialog.Color);
                this.symbolItem1.Symbol = rgbColor;
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value((int) this.symbolItem1.Tag, this.symbolItem1.Symbol);
            }
        }

        private void symbolItemColor1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            IRgbColor rgbColor = new RgbColorClass {
                RGB = (this.symbolItemColor1.Symbol as IColor).RGB
            };
            dialog.Color = Converter.FromRGBColor(rgbColor);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rgbColor = Converter.ToRGBColor(dialog.Color);
                this.symbolItemColor1.Symbol = rgbColor;
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value((int) this.symbolItemColor1.Tag, this.symbolItemColor1.Symbol);
            }
        }

        private void symbolItemColor2_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            IRgbColor rgbColor = new RgbColorClass {
                RGB = (this.symbolItemColor2.Symbol as IColor).RGB
            };
            dialog.Color = Converter.FromRGBColor(rgbColor);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rgbColor = Converter.ToRGBColor(dialog.Color);
                this.symbolItemColor2.Symbol = rgbColor;
                ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value((int) this.symbolItemColor2.Tag, this.symbolItemColor2.Symbol);
            }
        }

        private void txtAngleGrad_Leave(object sender, EventArgs e)
        {
            double val = 0.0;
            try
            {
                val = double.Parse((sender as TextBox).Text);
            }
            catch
            {
                return;
            }
            ((base.m_pGeometricEffect as IBasicFillSymbol).FillPattern as IGraphicAttributes).set_Value((int) (sender as TextBox).Tag, val);
        }
    }
}

