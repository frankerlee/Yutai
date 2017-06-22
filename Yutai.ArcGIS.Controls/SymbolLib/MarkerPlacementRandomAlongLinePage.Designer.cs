using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    partial class MarkerPlacementRandomAlongLinePage
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerPlacementRandomAlongLinePage));
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.comboBox2 = new ComboBox();
            this.comboBox1 = new ComboBox();
            this.comboBox3 = new ComboBox();
            this.label5 = new Label();
            this.checkBox1 = new CheckBox();
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
            this.label1.Location = new Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "沿线随机放置";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(212, 152);
            this.panel1.TabIndex = 1;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] { "标记", "半个空白" });
            this.comboBox2.Location = new Point(74, 64);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new Size(128, 20);
            this.comboBox2.TabIndex = 12;
            this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "标记", "空白", "半个空白" });
            this.comboBox1.Location = new Point(74, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(128, 20);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] { "低", "中", "高" });
            this.comboBox3.Location = new Point(74, 90);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new Size(128, 20);
            this.comboBox3.TabIndex = 10;
            this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 98);
            this.label5.Name = "label5";
            this.label5.Size = new Size(47, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "随机数:";
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(14, 126);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(72, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "偏转效果";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 72);
            this.label4.Name = "label4";
            this.label4.Size = new Size(47, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "控制点:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 45);
            this.label3.Name = "label3";
            this.label3.Size = new Size(35, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "端点:";
            this.textBox1.Location = new Point(74, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(128, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "0";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "步长:";
            this.btnChangeEffic.ImageIndex = 0;
            this.btnChangeEffic.ImageList = this.imageList1;
            this.btnChangeEffic.Location = new Point(193, 0);
            this.btnChangeEffic.Name = "btnChangeEffic";
            this.btnChangeEffic.RightToLeft = RightToLeft.Yes;
            this.btnChangeEffic.Size = new Size(16, 16);
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
            this.panel2.Location = new Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(242, 16);
            this.panel2.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "MarkerPlacementRandomAlongLinePage";
            base.Size = new Size(212, 172);
            base.Load += new EventHandler(this.MarkerPlacementRandomAlongLinePage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnChangeEffic;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox1;
    }
}