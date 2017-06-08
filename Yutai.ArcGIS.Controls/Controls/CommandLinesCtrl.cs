using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class CommandLinesCtrl : UserControl, ICommandLineWindows
    {
        private Container components = null;
        private int m_CommandTipInfo = 0;
        private int m_CurrentLine = 1;
        protected int m_MaxCommandLine = 100;
        protected IFramework m_pFrameWork = null;
        private Panel panel1;
        private TextEdit txtCommandLine;
        private MemoEdit txtCommandLineList;
        private Label txtCommandTip;

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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Init()
        {
            this.m_CurrentLine = 1;
        }

        private void InitializeComponent()
        {
            this.panel1 = new Panel();
            this.txtCommandLine = new TextEdit();
            this.txtCommandTip = new Label();
            this.txtCommandLineList = new MemoEdit();
            this.panel1.SuspendLayout();
            this.txtCommandLine.Properties.BeginInit();
            this.txtCommandLineList.Properties.BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.txtCommandLine);
            this.panel1.Controls.Add(this.txtCommandTip);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 0x67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1c8, 0x11);
            this.panel1.TabIndex = 1;
            this.txtCommandLine.Dock = DockStyle.Fill;
            this.txtCommandLine.EditValue = "";
            this.txtCommandLine.Location = new Point(0x2a, 0);
            this.txtCommandLine.Name = "txtCommandLine";
            this.txtCommandLine.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtCommandLine.Size = new Size(0x19e, 0x11);
            this.txtCommandLine.TabIndex = 1;
            this.txtCommandLine.KeyUp += new KeyEventHandler(this.CommandLine_KeyUp);
            this.txtCommandTip.Dock = DockStyle.Left;
            this.txtCommandTip.Name = "txtCommandTip";
            this.txtCommandTip.BackColor = this.txtCommandLine.BackColor;
            this.txtCommandTip.AutoSize = true;
            this.txtCommandTip.TabIndex = 2;
            this.txtCommandLineList.Dock = DockStyle.Fill;
            this.txtCommandLineList.EditValue = "";
            this.txtCommandLineList.Location = new Point(0, 0);
            this.txtCommandLineList.Name = "txtCommandLineList";
            this.txtCommandLineList.Properties.ReadOnly = true;
            this.txtCommandLineList.Size = new Size(0x1c8, 0x67);
            this.txtCommandLineList.TabIndex = 3;
            base.Controls.Add(this.txtCommandLineList);
            base.Controls.Add(this.panel1);
            base.Name = "CommandLinesCtrl";
            base.Size = new Size(0x1c8, 120);
            this.panel1.ResumeLayout(false);
            this.txtCommandLine.Properties.EndInit();
            this.txtCommandLineList.Properties.EndInit();
            base.ResumeLayout(false);
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
            get
            {
                return this.m_pFrameWork;
            }
            set
            {
                this.m_pFrameWork = value as IFramework;
            }
        }

        public int MaxCommandLine
        {
            get
            {
                return this.m_MaxCommandLine;
            }
            set
            {
                this.m_MaxCommandLine = value;
            }
        }
    }
}

