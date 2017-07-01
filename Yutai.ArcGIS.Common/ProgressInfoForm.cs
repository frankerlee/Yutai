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
    public class ProgressInfoForm : Form
    {
        private IContainer components = null;

        protected ProcessAssist m_ProcessAssist = null;


        public bool IsProcess { get; set; }

        public virtual ProcessAssist ProcessAssist
        {
            set { this.m_ProcessAssist = value; }
        }


        private void method_0()
        {
            this.components = new Container();
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "ProgressInfoForm";
        }

        public ProgressInfoForm()
        {
            this.method_0();
            this.IsProcess = false;
        }

        public virtual void EndProcess()
        {
            this.IsProcess = false;
        }

        protected override void OnClosing(CancelEventArgs cancelEventArgs_0)
        {
            if (this.IsProcess)
            {
                cancelEventArgs_0.Cancel = true;
            }
            base.OnClosing(cancelEventArgs_0);
        }
    }
}