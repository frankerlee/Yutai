using System;
using System.Runtime.InteropServices;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class MiApi
    {
        private MiApi()
        {
        }

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr mitab_c_add_field(IntPtr intptr_0, string string_0, int int_0, int int_1, int int_2,
            int int_3, int int_4);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_close(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr mitab_c_create(string string_0, string string_1, string string_2, double double_0,
            double double_1, double double_2, double double_3);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr mitab_c_create_feature(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_destroy_feature(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_brush_bgcolor(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_brush_fgcolor(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_brush_pattern(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_brush_transparent(IntPtr intptr_0);

        public static string mitab_c_get_extended_mif_coordsys(IntPtr intptr_0)
        {
            return Marshal.PtrToStringAnsi(MiApi.mitab_c_get_extended_mif_coordsys_1(intptr_0));
        }

        [DllImport("mitab.dll", CharSet = CharSet.None, EntryPoint = "mitab_c_get_extended_mif_coordsys",
             ExactSpelling = false)]
        private static extern IntPtr mitab_c_get_extended_mif_coordsys_1(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_feature_count(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_feature_id(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double mitab_c_get_field_as_double(IntPtr intptr_0, int int_0);

        public static string mitab_c_get_field_as_string(IntPtr intptr_0, int int_0)
        {
            return Marshal.PtrToStringAnsi(MiApi.mitab_c_get_field_as_string_1(intptr_0, int_0));
        }

        [DllImport("mitab.dll", CharSet = CharSet.None, EntryPoint = "mitab_c_get_field_as_string",
             ExactSpelling = false)]
        private static extern IntPtr mitab_c_get_field_as_string_1(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_field_count(IntPtr intptr_0);

        public static string mitab_c_get_field_name(IntPtr intptr_0, int int_0)
        {
            return Marshal.PtrToStringAnsi(MiApi.mitab_c_get_field_name_1(intptr_0, int_0));
        }

        [DllImport("mitab.dll", CharSet = CharSet.None, EntryPoint = "mitab_c_get_field_name", ExactSpelling = false)]
        private static extern IntPtr mitab_c_get_field_name_1(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_field_precision(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern FieldType mitab_c_get_field_type(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_field_width(IntPtr intptr_0, int int_0);

        public static string mitab_c_get_font(IntPtr intptr_0)
        {
            return Marshal.PtrToStringAnsi(MiApi.mitab_c_get_font_1(intptr_0));
        }

        [DllImport("mitab.dll", CharSet = CharSet.None, EntryPoint = "mitab_c_get_font", ExactSpelling = false)]
        private static extern IntPtr mitab_c_get_font_1(IntPtr intptr_0);

        public static string mitab_c_get_mif_coordsys(IntPtr intptr_0)
        {
            return Marshal.PtrToStringAnsi(MiApi.mitab_c_get_mif_coordsys_1(intptr_0));
        }

        [DllImport("mitab.dll", CharSet = CharSet.None, EntryPoint = "mitab_c_get_mif_coordsys", ExactSpelling = false)]
        private static extern IntPtr mitab_c_get_mif_coordsys_1(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_parts(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_pen_color(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_pen_pattern(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_pen_width(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr mitab_c_get_projinfo(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_symbol_color(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_symbol_no(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_symbol_size(IntPtr intptr_0);

        public static string mitab_c_get_text(IntPtr intptr_0)
        {
            return Marshal.PtrToStringAnsi(MiApi.mitab_c_get_text_1(intptr_0));
        }

        [DllImport("mitab.dll", CharSet = CharSet.None, EntryPoint = "mitab_c_get_text", ExactSpelling = false)]
        public static extern IntPtr mitab_c_get_text_1(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double mitab_c_get_text_angle(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_text_bgcolor(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_text_fgcolor(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double mitab_c_get_text_height(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_text_justification(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_text_linetype(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_text_spacing(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double mitab_c_get_text_width(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern FeatureType mitab_c_get_type(IntPtr intptr_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_get_vertex_count(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double mitab_c_get_vertex_x(IntPtr intptr_0, int int_0, int int_1);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern double mitab_c_get_vertex_y(IntPtr intptr_0, int int_0, int int_1);

        public static string mitab_c_getlasterrormsg()
        {
            return Marshal.PtrToStringAnsi(MiApi.mitab_c_getlasterrormsg_1());
        }

        [DllImport("mitab.dll", CharSet = CharSet.None, EntryPoint = "mitab_c_getlasterrormsg", ExactSpelling = false)]
        private static extern IntPtr mitab_c_getlasterrormsg_1();

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_getlasterrorno();

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_getlibversion();

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_is_interior_ring(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_load_coordsys_table(string string_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_next_feature_id(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr mitab_c_open(string string_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr mitab_c_read_feature(IntPtr intptr_0, int int_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_arc(IntPtr intptr_0, double double_0, double double_1, double double_2,
            double double_3, double double_4, double double_5);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_brush(IntPtr intptr_0, int int_0, int int_1, int int_2, int int_3);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_field(IntPtr intptr_0, int int_0, string string_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_font(IntPtr intptr_0, string string_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_pen(IntPtr intptr_0, int int_0, int int_1, int int_2);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_points(IntPtr intptr_0, int int_0, int int_1, ref double double_0,
            ref double double_1);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern int mitab_c_set_projinfo(IntPtr intptr_0, IntPtr intptr_1);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_symbol(IntPtr intptr_0, int int_0, int int_1, int int_2);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_text(IntPtr intptr_0, string string_0);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern void mitab_c_set_text_display(IntPtr intptr_0, double double_0, double double_1,
            double double_2, int int_0, int int_1, int int_2, int int_3, int int_4);

        [DllImport("mitab.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern IntPtr mitab_c_write_feature(IntPtr intptr_0, IntPtr intptr_1);
    }
}