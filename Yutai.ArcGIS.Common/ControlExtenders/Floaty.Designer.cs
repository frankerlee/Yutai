using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Common.ControlExtenders
{
    partial class Floaty
    {

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.ClientSize = new Size(178, 122);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            base.MaximizeBox = false;
            base.Name = "Floaty";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            base.ResumeLayout(false);
            this.bool_3 = true;
            this.bool_2 = true;
        }

       
        private bool bool_0;
        private bool bool_1;
        private bool bool_2;
        private bool bool_3;
        private DockExtender dockExtender_0;
        private DockState dockState_0;
    }
}