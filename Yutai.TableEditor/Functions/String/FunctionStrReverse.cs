using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionStrReverse : FunctionBase
    {
        public FunctionStrReverse()
        {
            _caption = "StrReverse";
            _category = "String";
            _key = "StrReverse";
            _expression = "StrReverse(string)";
            _description = "StrReverse 函数可反转一个字符串。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "string", Description = "必需的。需被反转的字符串。"}
            };
        }
    }
}
