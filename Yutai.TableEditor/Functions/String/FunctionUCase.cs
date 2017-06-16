using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionUCase : FunctionBase
    {
        public FunctionUCase()
        {
            _caption = "UCase";
            _category = "String";
            _key = "UCase";
            _expression = "UCase(string)";
            _description = "UCase 函数可把指定的字符串转换为大写。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "string", Description = "必需的。需被转换为大写的字符串。"}
            };
        }
    }
}
