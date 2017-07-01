using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.Plugins.Events;

namespace Yutai.ArcGIS.Common
{
    public partial class frmProgress2 : Form
    {
        private IContainer icontainer_0 = null;

        public ProgressBar progressBar1;

        public Label Caption1;


        private bool bool_0 = false;

        public int MaxValue
        {
            set { this.progressBar1.Maximum = value; }
        }

        public string Message
        {
            set
            {
                this.Caption1.Text = value;
                Application.DoEvents();
            }
        }

        public ProcessAssist ProcessAssist
        {
            set
            {
                this.processAssist_0 = value;
                this.processAssist_0.OnIncrement += new OnIncrementHandler(this.method_0);
                this.processAssist_0.OnSetPostion += new OnSetPostionHandler(this.method_1);
                this.processAssist_0.OnSetMessage += new OnSetMessageHandler(this.method_2);
                this.processAssist_0.OnSetMaxValue += new OnSetMaxValueHandler(this.method_3);
                this.processAssist_0.OnSetAutoProcess += new OnSetAutoProcessHandler(this.method_4);
            }
        }

        public int Step
        {
            set { this.progressBar1.Step = value; }
        }

        public frmProgress2()
        {
            this.InitializeComponent();
        }

        private void frmProgress2_Load(object sender, EventArgs e)
        {
        }

        public void Increment()
        {
            this.progressBar1.Increment(this.progressBar1.Step);
            Application.DoEvents();
        }

        private void method_0(int int_0)
        {
            this.progressBar1.Increment(int_0);
        }

        private void method_1(int int_0)
        {
            this.progressBar1.Value = int_0;
        }

        private void method_2(string string_0)
        {
            this.Caption1.Text = string_0;
            Application.DoEvents();
        }

        private void method_3(int int_0)
        {
            this.progressBar1.Maximum = int_0;
            Application.DoEvents();
        }

        private void method_4()
        {
            this.bool_0 = true;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }
    }
}