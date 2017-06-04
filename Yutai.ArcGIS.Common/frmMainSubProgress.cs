using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common
{
    public partial class frmMainSubProgress : ProgressInfoForm
    {
        private MainSunProcessAssist mainSunProcessAssist_0 = null;

        private bool bool_1 = false;

        private IContainer icontainer_1 = null;

        

        public override ProcessAssist ProcessAssist
        {
            set
            {
                this.m_ProcessAssist = value;
                this.ProcessAssist2 = (this.m_ProcessAssist as MainSunProcessAssist);
            }
        }

        public MainSunProcessAssist ProcessAssist2
        {
            set
            {
                this.mainSunProcessAssist_0 = value;
                this.mainSunProcessAssist_0.OnIncrement += new OnIncrementHandler(this.method_6);
                this.mainSunProcessAssist_0.OnSetPostion += new OnSetPostionHandler(this.method_7);
                this.mainSunProcessAssist_0.OnSetMessage += new OnSetMessageHandler(this.method_8);
                this.mainSunProcessAssist_0.OnSetMaxValue += new OnSetMaxValueHandler(this.method_9);
                this.mainSunProcessAssist_0.OnSetAutoProcess += new OnSetAutoProcessHandler(this.method_10);
                this.mainSunProcessAssist_0.OnSubIncrement += new MainSunProcessAssist.OnSubIncrementHandler(this.method_5);
                this.mainSunProcessAssist_0.OnSetSubMaxValue += new MainSunProcessAssist.OnSetSubMaxValueHandler(this.method_4);
                this.mainSunProcessAssist_0.OnSetSubMessage += new MainSunProcessAssist.OnSetSubMessageHandler(this.method_3);
                this.mainSunProcessAssist_0.OnSetSubPostion += new MainSunProcessAssist.OnSetSubPostionHandler(this.method_2);
                this.mainSunProcessAssist_0.OnResetSubInfo += new MainSunProcessAssist.OnResetSubInfoHandler(this.method_1);
            }
        }

        private void method_1()
        {
            this.lblSub.Text = "";
            this.progressSub.Value = 0;
            System.Windows.Forms.Application.DoEvents();
        }

        private void method_2(int int_0)
        {
            this.progressSub.Value = int_0;
        }

        private void method_3(string string_0)
        {
            this.lblSub.Text = string_0;
            System.Windows.Forms.Application.DoEvents();
        }

        private void method_4(int int_0)
        {
            this.progressSub.Maximum = int_0;
        }

        private void method_5(int int_0)
        {
            this.progressSub.Increment(int_0);
        }

        private void method_6(int int_0)
        {
            this.progressMain.Increment(int_0);
            System.Windows.Forms.Application.DoEvents();
        }

        private void method_7(int int_0)
        {
            this.progressMain.Value = int_0;
        }

        private void method_8(string string_0)
        {
            this.lblMain.Text = string_0;
            System.Windows.Forms.Application.DoEvents();
        }

        private void method_9(int int_0)
        {
            this.progressMain.Maximum = int_0;
        }

        private void method_10()
        {
            this.bool_1 = true;
        }

        public frmMainSubProgress()
        {
            this.InitializeComponent();
            this.timer_0.Tick += new System.EventHandler(this.timer_0_Tick);
        }

        private void timer_0_Tick(object sender, System.EventArgs e)
        {
            if (this.progressMain.Value == this.progressMain.Maximum)
            {
                this.progressMain.Value = this.progressMain.Minimum;
            }
            else
            {
                this.progressMain.Increment(1);
            }
        }

        private void frmMainSubProgress_Load(object sender, System.EventArgs e)
        {
            if (this.bool_1)
            {
                this.timer_0.Enabled = true;
            }
        }

      

       
    }
}
