using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionLTrim : FunctionBase
    {
        public FunctionLTrim()
        {
            _caption = "LTrim";
            _category = "String";
            _key = "LTrim";
            _expression = "Len(string)";
            _description = "LTrim 函数可删除字符串左侧的空格。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "string", Description = "字符串表达式。"}
            };
        }
    }
}