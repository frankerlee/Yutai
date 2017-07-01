using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Yutai.ArcGIS.Framework;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class CommandLinesCtrl : UserControl, ICommandLineWindows
    {
        private int m_CommandTipInfo = 0;
        private int m_CurrentLine = 1;
        protected int m_MaxCommandLine = 100;
        protected IFramework m_pFrameWork = null;

        public CommandLinesCtrl()
        {
            this.InitializeComponent();
            this.txtCommandTip.Text = "命令:";
        }

        private void AddStringToCommandLineList(string str)
        {
            str = str.Trim();
            if (str.Length != 0)
            {
                if (this.txtCommandLineList.Lines.Length == this.m_MaxCommandLine)
                {
                    int index = this.txtCommandLineList.Text.IndexOf("\r\n", 0);
                    string str2 = this.txtCommandLineList.Text.Substring(index + 2);
                    this.txtCommandLineList.Text = str2 + "\r\n" + str;
                }
                else if (this.txtCommandLineList.Text.Length == 0)
                {
                    this.txtCommandLineList.Text = str;
                }
                else
                {
                    this.txtCommandLineList.Text = this.txtCommandLineList.Text + "\r\n" + str;
                }
            }
        }

        private void AdjustTextBoxWidth(TextEdit pTextBox)
        {
            SizeF ef = pTextBox.CreateGraphics().MeasureString(pTextBox.Text, pTextBox.Font);
            pTextBox.Width = (int) ef.Width;
        }

        private void CommandLine_KeyUp(object sender, KeyEventArgs e)
        {
            string text;
            if (e.KeyCode == Keys.Enter)
            {
                if (this.m_pFrameWork != null)
                {
                    text = this.txtCommandLine.Text;
                    text.Trim();
                    if (text.Length == 0)
                    {
                        this.m_pFrameWork.Excute(text);
                    }
                    else if (this.m_CommandTipInfo == 0)
                    {
                        this.m_pFrameWork.HandleCommandLine(text);
                    }
                    else if (text[0] == '\'')
                    {
                        text = text.Substring(1);
                        this.m_pFrameWork.HandleCommandLine(text);
                    }
                    else
                    {
                        this.m_pFrameWork.Excute(text);
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.m_pFrameWork.Excute("ESC");
            }
            else if (this.m_CommandTipInfo != 0)
            {
                text = this.txtCommandLine.Text;
                text.Trim();
                this.m_pFrameWork.CommandLines = text;
            }
        }

        public void Init()
        {
            this.m_CurrentLine = 1;
        }

        public void LockCommandLine(bool flag)
        {
        }

        private void m_keyboard_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void m_keyboard_KeyUp(object sender, KeyEventArgs e)
        {
            if (!this.Focused)
            {
                string text;
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.m_pFrameWork != null)
                    {
                        text = this.txtCommandLine.Text;
                        text.Trim();
                        if (text.Length == 0)
                        {
                            this.m_pFrameWork.Excute(text);
                        }
                        else if (this.m_CommandTipInfo == 0)
                        {
                            this.m_pFrameWork.HandleCommandLine(text);
                        }
                        else if (text[0] == '\'')
                        {
                            text = text.Substring(1);
                            this.m_pFrameWork.HandleCommandLine(text);
                        }
                        else
                        {
                            this.m_pFrameWork.Excute(text);
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    this.m_pFrameWork.Excute("ESC");
                }
                else if (this.m_CommandTipInfo != 0)
                {
                    text = this.txtCommandLine.Text;
                    text.Trim();
                    this.m_pFrameWork.CommandLines = text;
                }
            }
        }

        public void ShowCommandString(string str, short state)
        {
            if (base.Visible && !this.txtCommandLine.Focused)
            {
                this.txtCommandLine.Focus();
            }
            if ((state == 0) && (this.m_CommandTipInfo == 0))
            {
                string str2 = this.txtCommandLine.Text.Trim();
                if (str2.Length > 0)
                {
                    this.AddStringToCommandLineList(this.txtCommandTip.Text + str2);
                }
                try
                {
                    this.txtCommandLine.Text = "";
                }
                catch
                {
                }
            }
            if (state == 0)
            {
                if (this.m_CommandTipInfo == 0)
                {
                    this.txtCommandLine.Text = str;
                }
                else
                {
                    this.txtCommandLine.Text = "'" + str;
                }
            }
            else if (state == 1)
            {
                this.AddStringToCommandLineList(this.txtCommandTip.Text + this.txtCommandLine.Text);
                this.m_CommandTipInfo = 1;
                this.txtCommandTip.Text = str;
                try
                {
                    this.txtCommandLine.Text = "";
                }
                catch
                {
                }
            }
            else if (state == 2)
            {
                this.m_CommandTipInfo = 1;
                this.txtCommandLine.Text = str;
            }
            else if (state == 3)
            {
                this.AddStringToCommandLineList(this.txtCommandTip.Text + this.txtCommandLine.Text);
                if (str.Length > 0)
                {
                    this.AddStringToCommandLineList(str);
                }
                this.m_CommandTipInfo = 0;
                this.txtCommandTip.Text = "命令:";
                try
                {
                    this.txtCommandLine.Text = "";
                }
                catch
                {
                }
            }
            else if (state == 4)
            {
                if (str.Length > 0)
                {
                    string str3 = this.txtCommandLine.Text.Trim();
                    if ((str3.Length > 0) && (str3 != "命令:"))
                    {
                        try
                        {
                            this.txtCommandLine.Text = "";
                        }
                        catch
                        {
                        }
                        this.AddStringToCommandLineList(this.txtCommandTip.Text + str3);
                    }
                    this.AddStringToCommandLineList(str);
                }
            }
            else if (state == 5)
            {
                this.AddStringToCommandLineList(this.txtCommandTip.Text + this.txtCommandLine.Text);
                if (str.Length > 0)
                {
                    this.AddStringToCommandLineList(str);
                }
                this.m_CommandTipInfo = 0;
                this.txtCommandTip.Text = "命令:";
                try
                {
                    this.txtCommandLine.Text = "";
                }
                catch
                {
                }
            }
            else if (state == 6)
            {
                this.AddStringToCommandLineList(this.txtCommandTip.Text + this.txtCommandLine.Text);
                if (str.Length > 0)
                {
                    this.AddStringToCommandLineList(str);
                }
                this.m_CommandTipInfo = 0;
                this.txtCommandTip.Text = "命令:";
                try
                {
                    this.txtCommandLine.Text = "";
                }
                catch
                {
                }
            }
            this.txtCommandLineList.SelectionStart = this.txtCommandLineList.Text.Length;
            this.txtCommandLineList.ScrollToCaret();
            this.txtCommandLine.SelectionStart = this.txtCommandLine.Text.Length;
        }

        public object Framework
        {
            get { return this.m_pFrameWork; }
            set { this.m_pFrameWork = value as IFramework; }
        }

        public int MaxCommandLine
        {
            get { return this.m_MaxCommandLine; }
            set { this.m_MaxCommandLine = value; }
        }
    }
}