using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionAtn: FunctionBase
    {
        public FunctionAtn()
        {
            _caption = "Atn";
            _category = "Math";
            _key = "Atn";
            _expression = "Atn(number)";
            _description = "Atn 函数可返回指定数字的正切。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "number", Description = "必需的。一个数值表达式。"}
            };
        }
    }
}
