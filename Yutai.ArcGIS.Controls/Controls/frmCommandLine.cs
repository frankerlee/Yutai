using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Framework.Docking;

namespace Yutai.ArcGIS.Controls.Controls
{
    public class frmCommandLine : DockContent, ICommandLineWindows
    {
        private IContainer components = null;
        private CommandLinesCtrl ctrl = new CommandLinesCtrl();

        public frmCommandLine()
        {
            this.InitializeComponent();
            base.Load += new EventHandler(this.frmCommandLine_Load);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCommandLine));
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x111);
            base.DockAreas = DockAreas.DockBottom | DockAreas.DockTop | DockAreas.DockRight | DockAreas.DockLeft | DockAreas.Float;
            base.HideOnClose = true;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.Name = "frmCommandLine";
            base.ShowHint = DockState.DockBottom;
            base.TabText = "命令行";
            this.Text = "命令行";
            base.ResumeLayout(false);
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
            get
            {
                return this.ctrl.Framework;
            }
            set
            {
                this.ctrl.Framework = value;
            }
        }

        public int MaxCommandLine
        {
            get
            {
                return this.ctrl.MaxCommandLine;
            }
            set
            {
                this.ctrl.MaxCommandLine = value;
            }
        }
    }
}

