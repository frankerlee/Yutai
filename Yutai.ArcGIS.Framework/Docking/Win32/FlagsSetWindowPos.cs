using System;

namespace Yutai.ArcGIS.Framework.Docking.Win32
{
    [Flags]
    internal enum FlagsSetWindowPos : uint
    {
        SWP_ASYNCWINDOWPOS = 16384,
        SWP_DEFERERASE = 8192,
        SWP_DRAWFRAME = 32,
        SWP_FRAMECHANGED = 32,
        SWP_HIDEWINDOW = 128,
        SWP_NOACTIVATE = 16,
        SWP_NOCOPYBITS = 256,
        SWP_NOMOVE = 2,
        SWP_NOOWNERZORDER = 512,
        SWP_NOREDRAW = 8,
        SWP_NOREPOSITION = 512,
        SWP_NOSENDCHANGING = 1024,
        SWP_NOSIZE = 1,
        SWP_NOZORDER = 4,
        SWP_SHOWWINDOW = 64
    }
}