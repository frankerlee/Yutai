using System.Data;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Controls.Controls.Export
{
    internal class ExportToExcelHelper
    {
        public ICursor Cursor = null;
        public static ExportToExcelHelper ExcelHelper = null;
        public string TempleteFile = "";
        public int TempleteRowCount = 1;
        public int TempleteStartRowIndex = 0;
        public string Title = "";

        public bool Export()
        {
            IField field;
            if (this.Cursor == null)
            {
                return false;
            }
            DataTable table = new DataTable();
            IFields fields = this.Cursor.Fields;
            int index = 0;
            while (index < fields.FieldCount)
            {
                field = fields.get_Field(index);
                if (((field.Type != esriFieldType.esriFieldTypeBlob) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && (field.Type != esriFieldType.esriFieldTypeRaster))
                {
                    if (field.Type == esriFieldType.esriFieldTypeOID)
                    {
                        table.Columns.Add(field.Name);
                    }
                    else
                    {
                        table.Columns.Add(field.AliasName);
                    }
                }
                index++;
            }
            object[] values = new object[table.Columns.Count];
            int num2 = 0;
            for (IRow row = this.Cursor.NextRow(); row != null; row = this.Cursor.NextRow())
            {
                num2 = 0;
                for (index = 0; index < fields.FieldCount; index++)
                {
                    field = fields.get_Field(index);
                    if (((field.Type != esriFieldType.esriFieldTypeBlob) && (field.Type != esriFieldType.esriFieldTypeGeometry)) && (field.Type != esriFieldType.esriFieldTypeRaster))
                    {
                        values[num2++] = row.get_Value(index);
                    }
                }
                table.Rows.Add(values);
            }
            if (this.TempleteFile == "")
            {
                JLKExportExcel.ExportExcel(false, this.Title, table, this.Title);
            }
            else
            {
                JLKExportExcel.ExcelTemplatePrint(false, table, this.TempleteFile, this.Title, this.TempleteStartRowIndex, this.TempleteRowCount);
            }
            return true;
        }

        public bool Export(DataTable dt)
        {
            if (this.TempleteFile == "")
            {
                JLKExportExcel.ExportExcel(false, this.Title, dt, this.Title);
            }
            else
            {
                JLKExportExcel.ExcelTemplatePrint(false, dt, this.TempleteFile, this.Title, this.TempleteStartRowIndex, this.TempleteRowCount);
            }
            return true;
        }

        public static void Init()
        {
            ExcelHelper = new ExportToExcelHelper();
        }
    }
}

