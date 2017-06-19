using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionRTrim : FunctionBase
    {
        public FunctionRTrim()
        {
            _caption = "RTrim";
            _category = "String";
            _key = "RTrim";
            _expression = "RTrim(string)";
            _description = "RTrim 函数可删除字符串右侧的空格。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "string", Description = "字符串表达式。"}
            };
        }
    }
}
