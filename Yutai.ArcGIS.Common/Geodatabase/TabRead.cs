using System;
using System.Runtime.InteropServices;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    internal struct TabRead
    {
        public const string TABREAD = "mitab.dll";

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void _mitab_c_close(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_destroy_feature(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_brush_bgcolor(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_brush_fgcolor(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_brush_pattern(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_brush_transparent(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern string _mitab_c_get_coordsys_xml(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_feature_count(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_feature_count_bytype(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern string _mitab_c_get_field_as_string(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_field_count(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern string _mitab_c_get_field_name(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_field_precision(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_field_type(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_field_width(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern string _mitab_c_get_font(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void _mitab_c_get_mif_bounds(IntPtr intptr_0, ref double double_0, ref double double_1,
            ref double double_2, ref double double_3);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern string _mitab_c_get_mif_coordsys(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_parts(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_pen_color(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_pen_pattern(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_pen_width(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr _mitab_c_get_projinfo(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_symbol_color(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_symbol_no(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_symbol_size(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern string _mitab_c_get_text(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double _mitab_c_get_text_angle(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_text_bgcolor(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_text_fgcolor(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double _mitab_c_get_text_height(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_text_justification(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_text_linetype(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_text_spacing(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double _mitab_c_get_text_width(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_type(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_get_vertex_count(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double _mitab_c_get_vertex_x(IntPtr intptr_0, int int_0, int int_1);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double _mitab_c_get_vertex_y(IntPtr intptr_0, int int_0, int int_1);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern string _mitab_c_getlasterrormsg();

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int _mitab_c_next_feature_id(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr _mitab_c_open(string string_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr _mitab_c_read_feature(IntPtr intptr_0, int int_0);
    }
}