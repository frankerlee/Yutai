using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionLCase : FunctionBase
    {
        public FunctionLCase()
        {
            _caption = "LCase";
            _category = "String";
            _key = "LCase";
            _expression = "LCase(string)";
            _description = "LCase 函数可把指定的字符串转换为小写。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "string", Description = "必需的。需要被转换为小写的字符串。"}
            };
        }
    }
}