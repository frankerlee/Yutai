using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionCStr : FunctionBase
    {
        public FunctionCStr()
        {
            _caption = "CStr";
            _category = "Conversion";
            _key = "CStr";
            _expression = "CStr(expression)";
            _description = "CStr 函数可把表达式转换为字符串（String）类型。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "expression", Description = "必需的。任何有效的表达式。"}
            };
        }
    }
}