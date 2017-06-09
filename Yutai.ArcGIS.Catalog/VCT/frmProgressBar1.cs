using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Catalog.VCT
{
    public class frmProgressBar1 : Form
    {
        public Label Caption1;
        private IContainer icontainer_0 = null;
        public Label label1;
        public ProgressBar progressBar1;

        public frmProgressBar1()
        {
            try
            {
                this.InitializeComponent();
                this.Caption1.Text = "";
            }
            catch (Exception)
            {
            }
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProgressBar1));
            this.progressBar1 = new ProgressBar();
            this.Caption1 = new Label();
            this.label1 = new Label();
            base.SuspendLayout();
            this.progressBar1.Location = new Point(1, 0x31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(0x19d, 13);
            this.progressBar1.TabIndex = 3;
            this.Caption1.AutoSize = true;
            this.Caption1.Location = new Point(2, 0x1c);
            this.Caption1.Name = "Caption1";
            this.Caption1.Size = new Size(0x35, 12);
            this.Caption1.TabIndex = 2;
            this.Caption1.Text = "Caption1";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 12);
            this.label1.TabIndex = 4;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.GradientInactiveCaption;
            base.ClientSize = new Size(0x19f, 0x4a);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.Caption1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = (Icon) resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "frmProgressBar1";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "frmProgressBar1";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void KDClose()
        {
            base.Close();
        }

        public void KDHide()
        {
            base.Hide();
        }

        public void KDRefresh()
        {
            this.Refresh();
        }

        public void KDShow()
        {
            base.Show();
        }

        public string KDCaption1
        {
            get
            {
                return this.Caption1.Text;
            }
            set
            {
                this.Caption1.Text = value;
            }
        }

        public ProgressBar KDProgressBar1
        {
            get
            {
                return this.progressBar1;
            }
            set
            {
                this.progressBar1 = value;
            }
        }

        public string KDTitle
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public override string Text
        {
            get
            {
                if (this.label1 != null)
                {
                    return this.label1.Text;
                }
                return base.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }
    }
}

