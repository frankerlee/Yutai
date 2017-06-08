using System;
using System.Windows.Forms;

namespace Yutai.ArcGIS.Framework.Docking
{
    internal class DragForm : Form
    {
        public DragForm()
        {
            base.FormBorderStyle = FormBorderStyle.None;
            base.ShowInTaskbar = false;
            base.SetStyle(ControlStyles.Selectable, false);
            base.Enabled = false;
        }

        public virtual void Show(bool bActivate)
        {
            if (bActivate)
            {
                base.Show();
            }
            else
            {
                NativeMethods.ShowWindow(base.Handle, 4);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                m.Result = (IntPtr) (-1L);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x80;
                return createParams;
            }
        }
    }
}

