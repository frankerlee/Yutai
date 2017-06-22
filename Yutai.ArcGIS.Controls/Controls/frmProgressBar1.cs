using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmProgressBar1 : Form, IKDProgressBar1, IKDProgressBar
    {
        public Label Caption1;
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

 private void frmProgressBar1_Load(object sender, EventArgs e)
        {
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

