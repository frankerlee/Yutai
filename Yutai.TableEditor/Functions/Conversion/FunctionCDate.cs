using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionCDate : FunctionBase
    {
        public FunctionCDate()
        {
            _caption = "CDate";
            _category = "Conversion";
            _key = "CDate";
            _expression = "CDate(date)";
            _description = "CDate 函数可把一个合法的日期和时间表达式转换为 Date 类型，并返回结果。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "date", Description = "必需的。任何有效的日期表达式。（比如 Date() 或者 Now()）"}
            };
        }
    }
}
