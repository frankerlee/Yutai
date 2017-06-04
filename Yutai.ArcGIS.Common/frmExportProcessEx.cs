using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Common
{
    public partial class frmExportProcessEx : System.Windows.Forms.Form
    {
        private IConvertEvent iconvertEvent_0 = null;

        private int int_0 = 0;

        private int int_1 = -1;

        private int int_2 = -1;

        private int int_3 = -1;

        private string string_0 = "";

        private int int_4 = 0;

        private bool bool_0 = false;

        private bool bool_1 = false;

       

       

        public IConvertEvent ConvertEvent
        {
            set
            {
                this.iconvertEvent_0 = value;
                this.method_0();
            }
        }

        public bool AutoCloseCheckEnable
        {
            set
            {
                this.checkBox1.Enabled = value;
            }
        }

        public bool IsAutoClose
        {
            get
            {
                return this.checkBox1.Checked;
            }
            set
            {
                this.checkBox1.Checked = value;
            }
        }

        public frmExportProcessEx()
        {
            this.InitializeComponent();
        }

        private void method_0()
        {
            this.iconvertEvent_0.SetMaxValueEvent += new SetMaxValueHandler(this.method_9);
            this.iconvertEvent_0.SetMessageEvent += new SetMessageHandler(this.method_8);
            this.iconvertEvent_0.SetMinValueEvent += new SetMinValueHandler(this.method_7);
            this.iconvertEvent_0.SetPositionEvent += new SetPositionHandler(this.method_6);
            this.iconvertEvent_0.FinishEvent += new FinishHander(this.method_5);
            if (this.iconvertEvent_0 is IConvertEventEx)
            {
                IConvertEventEx convertEventEx = this.iconvertEvent_0 as IConvertEventEx;
                convertEventEx.SetFeatureClassMaxValueEvent += new SetFeatureClassMaxValueHandler(this.method_4);
                convertEventEx.SetFeatureClassMinValueEvent += new SetFeatureClassMinValueHandler(this.method_3);
                convertEventEx.SetFeatureClassPositionEvent += new SetFeatureClassPositionHandler(this.method_2);
                convertEventEx.SetHandleFeatureInfoEvent += new SetHandleFeatureInfoHandler(this.method_1);
            }
        }

        private void method_1(string string_1)
        {
            this.string_0 = string_1;
        }

        private void method_2(int int_5)
        {
            this.progressBar2.Value = int_5 + 1;
            string text = string.Format("正在转换要素类[{0}]，共有{1}个要素类，正在处理第{2}要素类", this.string_0, this.int_4, int_5 + 1);
            this.label1.Text = text;
            System.Windows.Forms.Application.DoEvents();
        }

        private void method_3(int int_5)
        {
            this.progressBar2.Minimum = int_5;
        }

        private void method_4(int int_5)
        {
            this.progressBar2.Maximum = int_5;
            this.int_4 = int_5;
        }

        private void method_5()
        {
            this.lblInfo.Text = "转换完成!";
            this.label1.Text = "转换完成!";
            this.bool_0 = true;
        }

        private void method_6(int int_5)
        {
            this.progressBar1.Value = int_5 + 1;
            string text;
            if (this.int_0 == 0)
            {
                text = string.Format("共选中{0}个实体，正在处理第{1}个实体", this.int_3, int_5 + 1);
            }
            else
            {
                text = string.Format("共有{0}个要素类，正在处理第{1}要素类", this.int_3, int_5 + 1);
            }
            this.lblInfo.Text = text;
            System.Windows.Forms.Application.DoEvents();
        }

        private void method_7(int int_5)
        {
            this.int_1 = int_5;
            if (this.int_2 != -1)
            {
                this.int_3 = this.int_2 - this.int_1;
            }
            this.progressBar1.Minimum = int_5;
        }

        private void method_8(string string_1)
        {
            if (this.textBox1.Lines.Length == 200)
            {
                this.textBox1.Text = this.textBox1.Text.Substring(this.textBox1.Lines[0].Length + 2) + string_1 + "\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length - string_1.Length + 1;
                this.textBox1.ScrollToCaret();
            }
            else
            {
                System.Windows.Forms.TextBox expr_8A = this.textBox1;
                expr_8A.Text = expr_8A.Text + string_1 + "\r\n";
                this.textBox1.SelectionStart = this.textBox1.Text.Length - string_1.Length + 1;
                this.textBox1.ScrollToCaret();
            }
        }

        private void method_9(int int_5)
        {
            this.int_2 = int_5;
            if (this.int_2 != -1)
            {
                this.int_3 = this.int_2 - this.int_1;
            }
            this.progressBar1.Maximum = int_5;
        }

        private void frmExportProcessEx_Load(object sender, System.EventArgs e)
        {
        }

        private void btnDetial_Click(object sender, System.EventArgs e)
        {
            if (!this.bool_1)
            {
                this.btnDetial.Text = "<<详细信息";
                this.bool_1 = true;
                base.Size = new Size(352, 369);
            }
            else
            {
                this.btnDetial.Text = "详细信息>>";
                this.bool_1 = false;
                base.Size = new Size(337, 180);
            }
        }

        private void frmExportProcessEx_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (!this.bool_0)
            {
                e.Cancel = true;
            }
        }

     

      
    }
}
