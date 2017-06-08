using System;
using System.Runtime.InteropServices;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    internal class DragHelper
    {
        static DragHelper()
        {
            InitCommonControls();
        }

        [DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImageList_BeginDrag(IntPtr intptr_0, int int_0, int int_1, int int_2);
        [DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImageList_DragEnter(IntPtr intptr_0, int int_0, int int_1);
        [DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImageList_DragLeave(IntPtr intptr_0);
        [DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImageList_DragMove(int int_0, int int_1);
        [DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        public static extern bool ImageList_DragShowNolock(bool bool_0);
        [DllImport("comctl32.dll", CharSet=CharSet.Auto)]
        public static extern void ImageList_EndDrag();
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControls();
    }
}

