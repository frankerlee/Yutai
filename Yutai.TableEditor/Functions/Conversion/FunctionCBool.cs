using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionCBool : FunctionBase
    {
        public FunctionCBool()
        {
            _caption = "CBool";
            _category = "Conversion";
            _key = "CBool";
            _expression = "CBool(expression)";
            _description = "CBool 函数可把表达式转换为布尔类型。";
            _parameters = new List<Parameter>()
            {
                new Parameter()
                {
                    Order = 0,
                    Name = "expression",
                    Description = "必需的。任何合法的表达式。非零的值返回 True，零返回 False。如果表达式不能解释为数值，则将发生 run-time 错误。"
                }
            };
        }
    }
}