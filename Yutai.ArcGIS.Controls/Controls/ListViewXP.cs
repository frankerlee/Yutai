using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Controls.Controls
{
    [ToolboxBitmap(typeof(ListView))]
    public class ListViewXP : ListView
    {
        private LVS_EX styles;

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int SendMessage(IntPtr handle, int messg, int wparam, int lparam);
        public void SetExStyles()
        {
            this.styles = (LVS_EX) SendMessage(base.Handle, 4151, 0, 0);
            this.styles |= LVS_EX.LVS_EX_DOUBLEBUFFER | LVS_EX.LVS_EX_BORDERSELECT;
            SendMessage(base.Handle, 4150, 0, (int) this.styles);
        }

        public void SetExStyles(LVS_EX exStyle)
        {
            this.styles = (LVS_EX) SendMessage(base.Handle, 4151, 0, 0);
            this.styles |= exStyle;
            SendMessage(base.Handle, 4150, 0, (int) this.styles);
        }
    }
}

