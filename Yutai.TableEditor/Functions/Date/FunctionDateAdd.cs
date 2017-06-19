using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Date
{
    class FunctionDateAdd : FunctionBase
    {
        public FunctionDateAdd()
        {
            _caption = "DateAdd";
            _category = "Date";
            _key = "DateAdd";
            _expression = "DateAdd ( interval, number, date)";
            _description = "DateAdd 函数可返回已添加指定时间间隔的日期。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "interval", Description = "必需的。需要增加的时间间隔。"},
                new Parameter() { Order = 1, Name = "number", Description = "必需的。需要添加的时间间隔的数目。可对未来的日期使用正值，对过去的日期使用负值。"},
                new Parameter() { Order = 2, Name = "date", Description = "必需的。代表被添加的时间间隔的日期的变量或文字。"}
            };
        }
    }
}
