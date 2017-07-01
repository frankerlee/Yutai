using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Pipeline.Analysis.QueryForms
{
    public partial class frmPromptQuerying : Form
    {
        private string _StatusInfo = "";

        internal PictureBox pictureBox;

        public string StatusInfo
        {
            get { return this._StatusInfo; }
            set
            {
                this._StatusInfo = value;
                this.ChangeStatusText();
            }
        }

        public frmPromptQuerying()
        {
            this.InitializeComponent();
        }

        public frmPromptQuerying(int i)
        {
            this.InitializeComponent();
            if (i == 1)
            {
                this.label.Text = "正在查询，请稍候...";
            }
            if (i == 2)
            {
                this.label.Text = "正在上传数据，请稍候...";
            }
            if (i == 3)
            {
                this.label.Text = "正在进行处理，请稍候...";
            }
            if (i == 4)
            {
                this.label.Text = "构造查询条件，请稍候...";
            }
            if (i == 5)
            {
                this.label.Text = "进行空间查询，请稍候...";
            }
        }

        public void SetMessage(string strMesage)
        {
            this.label.Text = strMesage;
        }

        public void ChangeStatusText()
        {
            try
            {
                if (base.InvokeRequired)
                {
                    base.Invoke(new MethodInvoker(this.ChangeStatusText));
                }
                else
                {
                    this.label.Text = this._StatusInfo;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}