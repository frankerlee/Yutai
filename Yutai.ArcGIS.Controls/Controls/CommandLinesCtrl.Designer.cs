using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Yutai.ArcGIS.Framework;

namespace Yutai.ArcGIS.Controls.Controls
{
    partial class CommandLinesCtrl
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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
            this.panel1.Location = new Point(0, 103);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(456, 17);
            this.panel1.TabIndex = 1;
            this.txtCommandLine.Dock = DockStyle.Fill;
            this.txtCommandLine.EditValue = "";
            this.txtCommandLine.Location = new Point(42, 0);
            this.txtCommandLine.Name = "txtCommandLine";
            this.txtCommandLine.Properties.BorderStyle = BorderStyles.NoBorder;
            this.txtCommandLine.Size = new Size(414, 17);
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
            this.txtCommandLineList.Size = new Size(456, 103);
            this.txtCommandLineList.TabIndex = 3;
            base.Controls.Add(this.txtCommandLineList);
            base.Controls.Add(this.panel1);
            base.Name = "CommandLinesCtrl";
            base.Size = new Size(456, 120);
            this.panel1.ResumeLayout(false);
            this.txtCommandLine.Properties.EndInit();
            this.txtCommandLineList.Properties.EndInit();
            base.ResumeLayout(false);
        }

       
        private Container components = null;
        private Panel panel1;
        private TextEdit txtCommandLine;
        private MemoEdit txtCommandLineList;
        private Label txtCommandTip;
    }
}