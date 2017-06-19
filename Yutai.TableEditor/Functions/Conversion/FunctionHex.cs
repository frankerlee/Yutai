using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionHex : FunctionBase
    {
        public FunctionHex()
        {
            _caption = "Hex";
            _category = "Conversion";
            _key = "Hex";
            _expression = "Hex(number)";
            _description = "Hex 函数可返回一个表示指定数字的十六进制值的字符串。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "number", Description = "必需的。任何有效的表达式。"}
            };
        }
    }
}
