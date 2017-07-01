using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionCInt : FunctionBase
    {
        public FunctionCInt()
        {
            _caption = "CInt";
            _category = "Conversion";
            _key = "CInt";
            _expression = "CInt(expression)";
            _description = "CInt 函数可把表达式转换为整数（Integer）类型。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "expression", Description = "必需的。任何有效的表达式。"}
            };
        }
    }
}