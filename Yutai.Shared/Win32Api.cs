﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Yutai.Shared
{
    public static class Win32Api
    {
        public static int GWL_EXSTYLE = (-20);
        public static int GWL_STYLE = (-16);

        private struct POINTAPI
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        private static extern void GetCursorPos(ref POINTAPI lpPoint);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetFocus();

        public static System.Drawing.Point GetCursorLocation()
        {
            POINTAPI pnt = new POINTAPI();
            GetCursorPos(ref pnt);
            return new System.Drawing.Point(pnt.x, pnt.y);
        }

        public static Control GetFocusedControl()
        {
            IntPtr focusedHandle = GetFocus();
            return focusedHandle != IntPtr.Zero ? Control.FromHandle(focusedHandle) : null;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point pt);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern long StrFormatByteSize(long fileSize, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer, int bufferSize);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        #region Suspend/Resume redraw

        //http ://stackoverflow.com/questions/778095/windows-forms-using-backgroundimage-slows-down-drawing-of-the-forms-controls
        
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void SuspendDrawing(this Control target)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }

        public static void ResumeDrawing(this Control target)
        {
            ResumeDrawing(target, true);
        }

        public static void ResumeDrawing(this Control target, bool redraw)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Invalidate();
            }
        }

        #endregion

        #region Flash window

        public struct FLASHWINFO
        {
            public UInt32 cbSize;
            public IntPtr hwnd;
            public UInt32 dwFlags;
            public UInt32 uCount;
            public UInt32 dwTimeout;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        private const UInt32 FLASHW_STOP = 0;
        private const UInt32 FLASHW_CAPTION = 1;
        private const UInt32 FLASHW_TRAY = 2;
        private const UInt32 FLASHW_ALL = 3;
        private const UInt32 FLASHW_TIMER = 4;
        private const UInt32 FLASHW_TIMERNOFG = 12;

        public static void FlashForm(ref Form thisForm)
        {
            var flash = new FLASHWINFO();
            flash.cbSize = Convert.ToUInt32(Marshal.SizeOf(flash));
            flash.hwnd = thisForm.Handle;
            //flash.dwFlags = FLASHW_TRAY | FLASHW_TIMERNOFG | FLASHW_ALL;
            flash.dwFlags = FLASHW_ALL;
            flash.uCount = 3;

            //Leaving out .dwCount seems to work just fine for me, the uCount above keeps it from flashing

            FlashWindowEx(ref flash);
        }

        #endregion
    }
}
