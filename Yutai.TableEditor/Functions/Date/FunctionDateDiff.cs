using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Date
{
    class FunctionDateDiff : FunctionBase
    {
        public FunctionDateDiff()
        {
            _caption = "DateDiff";
            _category = "Date";
            _key = "DateDiff";
            _expression = "DateDiff(interval,date1,date2)";
            _description = "DateDiff 函数可返回两个日期之间的时间间隔数。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "interval", Description = "必需的。需要增加的时间间隔。"},
                new Parameter() {Order = 1, Name = "date1", Description = "必需的。日期表达式。在计算中需要使用的两个日期。"},
                new Parameter() {Order = 2, Name = "date2", Description = "必需的。日期表达式。在计算中需要使用的两个日期。"}
            };
        }
    }
}