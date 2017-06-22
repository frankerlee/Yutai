using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    partial class MarkerPlacementRandomInPolygonPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkerPlacementRandomInPolygonPage));
            this.label1 = new Label();
            this.panel1 = new Panel();
            this.textBox2 = new TextBox();
            this.label5 = new Label();
            this.comboBox1 = new ComboBox();
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
            this.label1.Size = new Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "多边形中随机放置";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(212, 86);
            this.panel1.TabIndex = 1;
            this.textBox2.Location = new Point(77, 36);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(128, 21);
            this.textBox2.TabIndex = 18;
            this.textBox2.Text = "0.5";
            this.textBox2.Leave += new EventHandler(this.textBox1_Leave);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 64);
            this.label5.Name = "label5";
            this.label5.Size = new Size(35, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "裁剪:";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] { "裁剪边界处的标记", "所有和边界相交的标记", "和边界相接的标记" });
            this.comboBox1.Location = new Point(77, 62);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(128, 20);
            this.comboBox1.TabIndex = 16;
            this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 40);
            this.label3.Name = "label3";
            this.label3.Size = new Size(41, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "Y步长:";
            this.textBox1.Location = new Point(77, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(128, 21);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "1";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(41, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "X步长:";
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
            this.panel2.Location = new Point(3, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(235, 16);
            this.panel2.TabIndex = 3;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "MarkerPlacementRandomInPolygonPage";
            base.Size = new Size(212, 102);
            base.Load += new EventHandler(this.MarkerPlacementRandomInPolygonPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnChangeEffic;
        private ComboBox comboBox1;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox1;
        private TextBox textBox2;
    }
}