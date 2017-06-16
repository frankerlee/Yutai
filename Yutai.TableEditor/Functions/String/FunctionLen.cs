using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionLen : FunctionBase
    {
        public FunctionLen()
        {
            _caption = "Len";
            _category = "String";
            _key = "Len";
            _expression = "Len(string)";
            _description = "Len 函数可返回字符串中字符的数目。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "string", Description = "字符串表达式。"}
            };
        }
    }
}
