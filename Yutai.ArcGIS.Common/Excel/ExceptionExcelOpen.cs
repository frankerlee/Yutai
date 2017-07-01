using System;

namespace Yutai.ArcGIS.Common.Excel
{
    public class ExceptionExcelOpen : Exception
    {
        private string string_0;

        public ExceptionExcelOpen()
        {
            this.string_0 = "打开Excel时错误！";
        }

        public ExceptionExcelOpen(string string_1)
        {
            this.string_0 = "打开Excel时错误！";
            this.string_0 = string_1;
        }

        public override string Message
        {
            get { return this.string_0; }
        }
    }
}