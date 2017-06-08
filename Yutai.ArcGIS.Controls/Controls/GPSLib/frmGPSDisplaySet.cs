using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Controls.Controls.GPSLib
{
    public class frmGPSDisplaySet : Form
    {
        private Button btnOK;
        private Button button2;
        private IContainer components = null;
        private IStyleGallery m_pSG = null;
        private TrailsSetCtrl m_TrailsSetCtrl = new TrailsSetCtrl();
        private TabControl tabControl1;

        public frmGPSDisplaySet()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.m_TrailsSetCtrl.Apply();
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmGPSDisplaySet_Load(object sender, EventArgs e)
        {
            TabPage page = new TabPage("跟踪线");
            this.m_TrailsSetCtrl.StyleGallery = this.m_pSG;
            this.m_TrailsSetCtrl.Dock = DockStyle.Fill;
            page.Controls.Add(this.m_TrailsSetCtrl);
            this.tabControl1.TabPages.Add(page);
        }

        private void InitializeComponent()
        {
            this.tabControl1 = new TabControl();
            this.button2 = new Button();
            this.btnOK = new Button();
            base.SuspendLayout();
            this.tabControl1.Dock = DockStyle.Top;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x1b4, 0x15c);
            this.tabControl1.TabIndex = 0;
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new Point(0x15f, 0x162);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x47, 0x18);
            this.button2.TabIndex = 15;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.btnOK.Location = new Point(0x112, 0x162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x47, 0x18);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b4, 390);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tabControl1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmGPSDisplaySet";
            this.Text = "GPS显示设置";
            base.Load += new EventHandler(this.frmGPSDisplaySet_Load);
            base.ResumeLayout(false);
        }

        public IStyleGallery StyleGallery
        {
            set
            {
                this.m_pSG = value;
            }
        }
    }
}

