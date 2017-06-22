using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    partial class MarkerPlacementInsidePolygonPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerPlacementInsidePolygonPage));
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.checkBox1 = new CheckBox();
            this.textBox5 = new TextBox();
            this.label9 = new Label();
            this.textBox4 = new TextBox();
            this.label8 = new Label();
            this.textBox3 = new TextBox();
            this.textBox2 = new TextBox();
            this.label5 = new Label();
            this.comboBox1 = new ComboBox();
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
            this.label1.Size = new Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "多边形内部";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(212, 206);
            this.panel1.TabIndex = 1;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(72, 98);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(84, 16);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "偏移奇数行";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
            this.textBox5.Location = new Point(72, 150);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Size(128, 21);
            this.textBox5.TabIndex = 18;
            this.textBox5.Text = "0";
            this.textBox5.Leave += new EventHandler(this.textBox1_Leave);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(12, 153);
            this.label9.Name = "label9";
            this.label9.Size = new Size(41, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "Y偏移:";
            this.textBox4.Location = new Point(72, 121);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Size(128, 21);
            this.textBox4.TabIndex = 16;
            this.textBox4.Text = "3";
            this.textBox4.Leave += new EventHandler(this.textBox1_Leave);
            this.label8.AutoSize = true;
            this.label8.Location = new Point(12, 130);
            this.label8.Name = "label8";
            this.label8.Size = new Size(41, 12);
            this.label8.TabIndex = 15;
            this.label8.Text = "X偏移:";
            this.textBox3.Location = new Point(72, 72);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Size(128, 21);
            this.textBox3.TabIndex = 14;
            this.textBox3.Text = "1.5";
            this.textBox3.Leave += new EventHandler(this.textBox1_Leave);
            this.textBox2.Location = new Point(72, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(128, 21);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "0.5";
            this.textBox2.Leave += new EventHandler(this.textBox1_Leave);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 179);
            this.label5.Name = "label5";
            this.label5.Size = new Size(35, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "裁剪:";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "裁剪边界处的标记", "所有和边界相交的标记", "和边界相接的标记" });
            this.comboBox1.Location = new Point(72, 177);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(128, 20);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 72);
            this.label4.Name = "label4";
            this.label4.Size = new Size(35, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "角度:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 45);
            this.label3.Name = "label3";
            this.label3.Size = new Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "Y步长:";
            this.textBox1.Location = new Point(72, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(128, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "1";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "X步长:";
            this.btnChangeEffic.ImageIndex = 0;
            this.btnChangeEffic.ImageList = this.imageList1;
            this.btnChangeEffic.Location = new Point(193, 1);
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
            this.panel2.Location = new Point(3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(254, 16);
            this.panel2.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "MarkerPlacementInsidePolygonPage";
            base.Size = new Size(212, 218);
            base.Load += new EventHandler(this.MarkerPlacementInsidePolygonPage_Load);
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
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label8;
        private Label label9;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
    }
}