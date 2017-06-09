using System;

namespace Yutai.ArcGIS.Common.Excel
{
    public class ExceptionExcelCreateInstance : Exception
    {
        private string string_0;

        public ExceptionExcelCreateInstance()
        {
            this.string_0 = "创建Excel类实例时错误！";
        }

        public ExceptionExcelCreateInstance(string string_1)
        {
            this.string_0 = "创建Excel类实例时错误！";
            this.string_0 = string_1;
        }

        public override string Message
        {
            get
            {
                return this.string_0;
            }
        }
    }
}

