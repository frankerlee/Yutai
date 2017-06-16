using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionCDbl : FunctionBase
    {
        public FunctionCDbl()
        {
            _caption = "CDbl";
            _category = "Conversion";
            _key = "CDbl";
            _expression = "CDbl(expression)";
            _description = "CDbl 函数可把表达式转换为双精度（Double）类型。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "expression", Description = "必需的。任何合法的表达式。"}
            };
        }
    }
}
