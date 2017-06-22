using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    partial class MarkerPlacementDecorationPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerPlacementDecorationPage));
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.checkBox3 = new CheckBox();
            this.checkBox2 = new CheckBox();
            this.textBox3 = new TextBox();
            this.textBox2 = new TextBox();
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
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "装饰";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.checkBox3);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(212, 144);
            this.panel1.TabIndex = 1;
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new Point(14, 122);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new Size(72, 16);
            this.checkBox3.TabIndex = 10;
            this.checkBox3.Text = "沿线偏转";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new EventHandler(this.checkBox3_CheckedChanged);
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new Point(14, 106);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new Size(84, 16);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "Flip First";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
            this.textBox3.Location = new Point(75, 60);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(128, 21);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "0";
            this.textBox3.Leave += new EventHandler(this.textBox1_Leave);
            this.textBox2.Location = new Point(75, 33);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(128, 21);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "0";
            this.textBox2.Leave += new EventHandler(this.textBox1_Leave);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(14, 87);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(72, 16);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Flip All";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 64);
            this.label4.Name = "label4";
            this.label4.Size = new Size(59, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "终止位置:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "起始位置:";
            this.textBox1.Location = new Point(75, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(128, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "0";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "标记:";
            this.btnChangeEffic.ImageIndex = 0;
            this.btnChangeEffic.ImageList = this.imageList1;
            this.btnChangeEffic.Location = new Point(192, 0);
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
            this.panel2.Location = new Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(236, 16);
            this.panel2.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "MarkerPlacementDecorationPage";
            base.Size = new Size(212, 159);
            base.Load += new EventHandler(this.MarkerPlacementDecorationPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnChangeEffic;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
    }
}