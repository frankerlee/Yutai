using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Date
{
    class FunctionDatePart : FunctionBase
    {
        public FunctionDatePart()
        {
            _caption = "DatePart";
            _category = "Date";
            _key = "DatePart";
            _expression = "DatePart(interval,date)";
            _description = "DatePart 函数可返回给定日期的指定部分。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "interval", Description = "必需的。需要增加的时间间隔。"},
                new Parameter() { Order = 1, Name = "date", Description = "必需的。需计算的日期表达式。"}
            };
        }
    }
}
