using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionSpace : FunctionBase
    {
        public FunctionSpace()
        {
            _caption = "Space";
            _category = "String";
            _key = "Space";
            _expression = "Space(number)";
            _description = "Space 函数可返回一个由指定数目的空格组成的字符串。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "number", Description = "必需的。字符串中的空格数目。"}
            };
        }
    }
}
