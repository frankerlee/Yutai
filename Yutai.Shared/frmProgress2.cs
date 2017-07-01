using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Shared
{
    public class frmProgress2 : Form
    {
        private IContainer icontainer_0 = null;

        public ProgressBar progressBar1;

        public Label Caption1;

        private ProcessAssist processAssist_0;

        //  private bool bool_0 = false;

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

        protected override void Dispose(bool bool_1)
        {
            if ((!bool_1 ? false : this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmProgress2_Load(object sender, EventArgs e)
        {
        }

        public void Increment()
        {
            this.progressBar1.Increment(this.progressBar1.Step);
            Application.DoEvents();
        }

        private void InitializeComponent()
        {
            this.progressBar1 = new ProgressBar();
            this.Caption1 = new Label();
            base.SuspendLayout();
            this.progressBar1.Location = new Point(12, 37);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(337, 13);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Click += new EventHandler(this.progressBar1_Click);
            this.Caption1.AutoSize = true;
            this.Caption1.Location = new Point(13, 16);
            this.Caption1.Name = "Caption1";
            this.Caption1.Size = new Size(53, 12);
            this.Caption1.TabIndex = 4;
            this.Caption1.Text = "Caption1";
            base.TopMost = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(376, 80);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.Caption1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "frmProgress2";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "frmProgress2";
            base.Load += new EventHandler(this.frmProgress2_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
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
            // this.bool_0 = true;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }
    }
}