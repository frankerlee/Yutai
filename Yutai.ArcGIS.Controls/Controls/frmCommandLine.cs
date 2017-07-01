using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmCommandLine : DockContent, ICommandLineWindows
    {
        private CommandLinesCtrl ctrl = new CommandLinesCtrl();

        public frmCommandLine()
        {
            this.InitializeComponent();
            base.Load += new EventHandler(this.frmCommandLine_Load);
        }

        private void frmCommandLine_Load(object sender, EventArgs e)
        {
            this.ctrl.Dock = DockStyle.Fill;
            base.Controls.Add(this.ctrl);
        }

        public void Init()
        {
            this.ctrl.Init();
        }

        public void LockCommandLine(bool flag)
        {
            this.ctrl.LockCommandLine(flag);
        }

        public void ShowCommandString(string str, short state)
        {
            this.ctrl.ShowCommandString(str, state);
        }

        public object Framework
        {
            get { return this.ctrl.Framework; }
            set { this.ctrl.Framework = value; }
        }

        public int MaxCommandLine
        {
            get { return this.ctrl.MaxCommandLine; }
            set { this.ctrl.MaxCommandLine = value; }
        }
    }
}