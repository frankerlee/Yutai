using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class frmProgress : Form
    {
        private IContainer components = null;
        private int pos = 0;
        private Timer timer1;
        private XpProgressBar xpProgressBar1;

        public frmProgress()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.timer1 = new Timer(this.components);
            this.xpProgressBar1 = new XpProgressBar();
            base.SuspendLayout();
            this.timer1.Enabled = true;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.xpProgressBar1.ColorBackGround = Color.White;
            this.xpProgressBar1.ColorBarBorder = Color.FromArgb(170, 240, 170);
            this.xpProgressBar1.ColorBarCenter = Color.FromArgb(10, 150, 10);
            this.xpProgressBar1.ColorText = Color.Black;
            this.xpProgressBar1.Dock = DockStyle.Fill;
            this.xpProgressBar1.Location = new Point(0, 0);
            this.xpProgressBar1.Name = "xpProgressBar1";
            this.xpProgressBar1.Position = 0;
            this.xpProgressBar1.PositionMax = 100;
            this.xpProgressBar1.PositionMin = 0;
            this.xpProgressBar1.Size = new Size(0xc0, 0x16);
            this.xpProgressBar1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xc0, 0x16);
            base.Controls.Add(this.xpProgressBar1);
            base.FormBorderStyle = FormBorderStyle.Fixed3D;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmProgress";
            base.StartPosition = FormStartPosition.CenterScreen;
            base.TopMost = true;
            base.Load += new EventHandler(this.frmProgress_Load);
            base.ResumeLayout(false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.pos == this.xpProgressBar1.PositionMax)
            {
                this.pos = this.xpProgressBar1.PositionMin;
            }
            this.pos++;
            this.xpProgressBar1.Position = this.pos;
            Application.DoEvents();
        }

        public string Messge
        {
            set
            {
                this.xpProgressBar1.Text = value;
            }
        }
    }
}

