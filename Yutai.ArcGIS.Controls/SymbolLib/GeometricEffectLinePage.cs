using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.Connection.Local;
using ESRI.ArcGIS.Display;
using Yutai.ArcGIS.Common.SymbolLib;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    internal class GeometricEffectLinePage : GeometricEffectBaseControl
    {
        private Button btnAddGemoetricEffic;
        private Button btnChangeEffic;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private bool m_CanDo = false;
        private BasicSymbolLayerBaseControl m_pControl = null;
        private Panel panel1;
        private Panel panel2;
        private SymbolItem symbolItem1;
        private TextBox textBox1;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private ToolStripMenuItem 修改ToolStripMenuItem;

        public GeometricEffectLinePage(BasicSymbolLayerBaseControl pControl)
        {
            this.InitializeComponent();
            this.m_pControl = pControl;
            base.m_pGeometricEffect = new BasicLineSymbolClass();
        }

        public override bool Apply()
        {
            double val = 0.0;
            try
            {
                val = double.Parse(this.textBox1.Text);
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
            IGraphicAttributes stroke = (base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes;
            stroke.set_Value((int) this.textBox1.Tag, val);
            stroke.set_Value((int) this.comboBox1.Tag, this.comboBox1.SelectedIndex);
            stroke.set_Value((int) this.comboBox2.Tag, this.comboBox2.SelectedIndex);
            stroke.set_Value((int) this.symbolItem1.Tag, this.symbolItem1.Symbol);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).set_Value((int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_CanDo)
            {
                ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).set_Value((int) (sender as ComboBox).Tag, (sender as ComboBox).SelectedIndex);
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

        private void GeometricEffectLinePage_Load(object sender, EventArgs e)
        {
            if (base.m_pGeometricEffect == null)
            {
                base.m_pGeometricEffect = new BasicLineSymbolClass();
            }
            int graphicAttributeCount = ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).GraphicAttributeCount;
            IGraphicAttributes stroke = (base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes;
            int attrId = stroke.get_ID(0);
            string str = stroke.get_Name(attrId);
            this.textBox1.Text = stroke.get_Value(attrId).ToString();
            this.textBox1.Tag = attrId;
            attrId = stroke.get_ID(1);
            str = stroke.get_Name(attrId);
            this.comboBox1.SelectedIndex = (int) stroke.get_Value(attrId);
            this.comboBox1.Tag = attrId;
            attrId = stroke.get_ID(2);
            str = stroke.get_Name(attrId);
            this.comboBox2.SelectedIndex = (int) stroke.get_Value(attrId);
            this.comboBox2.Tag = attrId;
            attrId = stroke.get_ID(3);
            str = stroke.get_Name(attrId);
            object obj2 = stroke.get_Value(attrId);
            this.symbolItem1.Symbol = obj2;
            this.symbolItem1.Tag = attrId;
            this.m_CanDo = true;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeometricEffectLinePage));
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.symbolItem1 = new SymbolItem();
            this.comboBox2 = new ComboBox();
            this.comboBox1 = new ComboBox();
            this.label3 = new Label();
            this.label5 = new Label();
            this.label4 = new Label();
            this.textBox1 = new TextBox();
            this.label2 = new Label();
            this.btnChangeEffic = new Button();
            this.imageList1 = new ImageList(this.components);
            this.panel2 = new Panel();
            this.btnAddGemoetricEffic = new Button();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.修改ToolStripMenuItem = new ToolStripMenuItem();
            this.删除ToolStripMenuItem = new ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.ForeColor = SystemColors.ActiveCaptionText;
            this.label1.Location = new Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x11, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "线";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.symbolItem1);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0xd4, 0x70);
            this.panel1.TabIndex = 1;
            this.symbolItem1.BackColor = SystemColors.ActiveCaptionText;
            this.symbolItem1.Location = new Point(0x41, 0x4e);
            this.symbolItem1.Name = "symbolItem1";
            this.symbolItem1.Size = new Size(0x7a, 30);
            this.symbolItem1.Symbol = null;
            this.symbolItem1.TabIndex = 10;
            this.symbolItem1.Click += new EventHandler(this.symbolItem1_Click);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] { "Miter", "Round", "Bevel" });
            this.comboBox2.Location = new Point(0x41, 0x34);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(0x7d, 20);
            this.comboBox2.TabIndex = 9;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "Butt", "Round", "Square" });
            this.comboBox1.Location = new Point(0x41, 0x1a);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(0x7e, 20);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 0x51);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "颜色:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 0x38);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x23, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "连接:";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 0x1d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "端点:";
            this.textBox1.Location = new Point(0x40, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x80, 0x15);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "8";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 4);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "宽度:";
            this.btnChangeEffic.ImageIndex = 0;
            this.btnChangeEffic.ImageList = this.imageList1;
            this.btnChangeEffic.Location = new Point(0xc1, 0);
            this.btnChangeEffic.Name = "btnChangeEffic";
            this.btnChangeEffic.RightToLeft = RightToLeft.Yes;
            this.btnChangeEffic.Size = new Size(0x10, 0x10);
            this.btnChangeEffic.TabIndex = 2;
            this.btnChangeEffic.UseVisualStyleBackColor = true;
            this.btnChangeEffic.Visible = false;
            this.imageList1.ImageStream = (ImageListStreamer) resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.White;
            this.imageList1.Images.SetKeyName(0, "Bitmap4.bmp");
            this.imageList1.Images.SetKeyName(1, "Bitmap5.bmp");
            this.panel2.BackColor = SystemColors.ControlDark;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnChangeEffic);
            this.panel2.Location = new Point(3, 0x10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0xd4, 0x10);
            this.panel2.TabIndex = 3;
            this.btnAddGemoetricEffic.ImageIndex = 1;
            this.btnAddGemoetricEffic.ImageList = this.imageList1;
            this.btnAddGemoetricEffic.Location = new Point(0xc3, 0);
            this.btnAddGemoetricEffic.Name = "btnAddGemoetricEffic";
            this.btnAddGemoetricEffic.RightToLeft = RightToLeft.Yes;
            this.btnAddGemoetricEffic.Size = new Size(0x10, 0x10);
            this.btnAddGemoetricEffic.TabIndex = 4;
            this.btnAddGemoetricEffic.UseVisualStyleBackColor = true;
            this.btnAddGemoetricEffic.Click += new EventHandler(this.btnAddGemoetricEffic_Click);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.修改ToolStripMenuItem, this.删除ToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(0x63, 0x30);
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new Size(0x62, 0x16);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new EventHandler(this.修改ToolStripMenuItem_Click);
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new Size(0x62, 0x16);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new EventHandler(this.删除ToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.btnAddGemoetricEffic);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "GeometricEffectLinePage";
            base.Size = new Size(0xd4, 0x90);
            base.Load += new EventHandler(this.GeometricEffectLinePage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
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
                ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).set_Value((int) this.symbolItem1.Tag, this.symbolItem1.Symbol);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
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
            ((base.m_pGeometricEffect as IBasicLineSymbol).Stroke as IGraphicAttributes).set_Value((int) (sender as TextBox).Tag, val);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_pControl != null)
            {
                this.m_pControl.RemoveControl(this);
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGeometricEffectList list = new frmGeometricEffectList {
                BasicSymbolLayerBaseControl = this.m_pControl
            };
            if ((list.ShowDialog() == DialogResult.OK) && (this.m_pControl != null))
            {
                this.m_pControl.ReplaceControl(this, list.SelectControl);
            }
        }
    }
}

