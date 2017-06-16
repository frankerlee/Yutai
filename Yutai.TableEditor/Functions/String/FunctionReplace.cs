using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionReplace : FunctionBase
    {
        public FunctionReplace()
        {
            _caption = "Replace";
            _category = "String";
            _key = "Replace";
            _expression = "Replace(string,find,replacewith)";
            _description = "Replace 函数可使用一个字符串替换另一个字符串指定的次数。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "string", Description = "必需的。需要被搜索的字符串。"},
                new Parameter() { Order = 1, Name = "find", Description = "必需的。将被替换的字符串部分。"},
                new Parameter() { Order = 2, Name = "replacewith", Description = "必需的。用于替换的子字符串。"}
            };
        }
    }
}
