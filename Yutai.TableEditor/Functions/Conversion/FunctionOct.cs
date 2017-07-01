using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionOct : FunctionBase
    {
        public FunctionOct()
        {
            _caption = "Oct";
            _category = "Conversion";
            _key = "Oct";
            _expression = "Oct(number)";
            _description = "Oct 函数可返回表示指定数字八进制值的字符串。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "number", Description = "必需的。任何有效的表达式。"}
            };
        }
    }
}