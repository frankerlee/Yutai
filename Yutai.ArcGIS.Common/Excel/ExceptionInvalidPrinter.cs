using System;

namespace Yutai.ArcGIS.Common.Excel
{
    public class ExceptionInvalidPrinter : Exception
    {
        private string string_0;

        public ExceptionInvalidPrinter()
        {
            this.string_0 = "无效的打印机，请在控制面版中添加打印机！";
        }

        public ExceptionInvalidPrinter(string string_1)
        {
            this.string_0 = "无效的打印机，请在控制面版中添加打印机！";
            this.string_0 = string_1;
        }

        public override string Message
        {
            get { return this.string_0; }
        }
    }
}