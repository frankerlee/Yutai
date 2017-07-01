using System;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class RowAssisant
    {
        public static object GetFieldValue(IRow irow_0, string string_0)
        {
            int index = irow_0.Fields.FindField(string_0);
            if (index != -1)
            {
                return irow_0.get_Value(index);
            }
            return DBNull.Value;
        }

        public static void SetFieldValue(IRow irow_0, string string_0, object object_0)
        {
            int index = irow_0.Fields.FindField(string_0);
            if (index != -1)
            {
                irow_0.set_Value(index, object_0);
            }
        }
    }
}