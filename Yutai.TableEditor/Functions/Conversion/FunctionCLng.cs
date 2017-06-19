using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionCLng : FunctionBase
    {
        public FunctionCLng()
        {
            _caption = "CLng";
            _category = "Conversion";
            _key = "CLng";
            _expression = "CLng(expression)";
            _description = "CLng 函数可把表达式转换为长整形（Long）类型。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "expression", Description = "必需的。任何有效的表达式。"}
            };
        }
    }
}
