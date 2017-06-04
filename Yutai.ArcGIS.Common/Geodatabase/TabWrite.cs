using System;
using System.Runtime.InteropServices;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct TabWrite
    {
        public const string TABREAD = "mitab.dll";

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern System.IntPtr _mitab_c_create(string string_0, string string_1, string string_2, double double_0, double double_1, double double_2, double double_3);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_close(System.IntPtr intptr_0);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern int _mitab_c_add_field(System.IntPtr intptr_0, string string_0, int int_0, int int_1, int int_2, int int_3, int int_4);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern string _mitab_c_getlasterrormsg();

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern int _mitab_c_destroy_feature(System.IntPtr intptr_0);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern System.IntPtr _mitab_c_create_feature(System.IntPtr intptr_0, int int_0);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern System.IntPtr _mitab_c_write_feature(System.IntPtr intptr_0, System.IntPtr intptr_1);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_field(System.IntPtr intptr_0, int int_0, string string_0);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_font(System.IntPtr intptr_0, string string_0);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_points(System.IntPtr intptr_0, int int_0, int int_1, double[] double_0, double[] double_1);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_coordsys_xml(System.IntPtr intptr_0, string string_0);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_text_display(System.IntPtr intptr_0, double double_0, double double_1, double double_2, int int_0, int int_1, int int_2, int int_3, int int_4);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_text(System.IntPtr intptr_0, string string_0);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_symbol(System.IntPtr intptr_0, int int_0, int int_1, int int_2);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_pen(System.IntPtr intptr_0, int int_0, int int_1, int int_2);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_brush(System.IntPtr intptr_0, int int_0, int int_1, int int_2, int int_3);

        [System.Runtime.InteropServices.DllImport("mitab.dll")]
        public static extern void _mitab_c_set_arc(System.IntPtr intptr_0, double double_0, double double_1, double double_2, double double_3, double double_4, double double_5);
    }
}