using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.SymbolLib
{
    partial class GeometricSmoothCurverPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeometricSmoothCurverPage));
            this.label1 = new Label();
            this.panel1 = new Panel();
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
            this.label1.Size = new Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "平滑";
            this.panel1.BackColor = SystemColors.Window;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(212, 37);
            this.panel1.TabIndex = 1;
            this.textBox1.Location = new Point(77, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(128, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "1";
            this.textBox1.Leave += new EventHandler(this.textBox1_Leave);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 11);
            this.label2.Name = "label2";
            this.label2.Size = new Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "平面容差:";
            this.btnChangeEffic.ImageIndex = 0;
            this.btnChangeEffic.ImageList = this.imageList1;
            this.btnChangeEffic.Location = new Point(194, 0);
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
            this.panel2.Location = new Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(247, 16);
            this.panel2.TabIndex = 3;
            this.btnAddGemoetricEffic.ImageIndex = 1;
            this.btnAddGemoetricEffic.ImageList = this.imageList1;
            this.btnAddGemoetricEffic.Location = new Point(197, 0);
            this.btnAddGemoetricEffic.Name = "btnAddGemoetricEffic";
            this.btnAddGemoetricEffic.RightToLeft = RightToLeft.Yes;
            this.btnAddGemoetricEffic.Size = new Size(16, 16);
            this.btnAddGemoetricEffic.TabIndex = 4;
            this.btnAddGemoetricEffic.UseVisualStyleBackColor = true;
            this.btnAddGemoetricEffic.Click += new EventHandler(this.btnAddGemoetricEffic_Click);
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.修改ToolStripMenuItem, this.删除ToolStripMenuItem });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(99, 48);
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new Size(98, 22);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new EventHandler(this.修改ToolStripMenuItem_Click);
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new Size(98, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new EventHandler(this.删除ToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.Control;
            base.Controls.Add(this.btnAddGemoetricEffic);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "GeometricSmoothCurverPage";
            base.Size = new Size(212, 69);
            base.Load += new EventHandler(this.GeometricSmoothCurverPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

       
        private IContainer components = null;
        private Button btnAddGemoetricEffic;
        private Button btnChangeEffic;
        private ContextMenuStrip contextMenuStrip1;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox1;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private ToolStripMenuItem 修改ToolStripMenuItem;
    }
}