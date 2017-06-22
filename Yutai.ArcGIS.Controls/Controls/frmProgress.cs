using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.ControlExtend;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmProgress : Form
    {
        private int pos = 0;

        public frmProgress()
        {
            this.InitializeComponent();
        }

 private void frmProgress_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
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

