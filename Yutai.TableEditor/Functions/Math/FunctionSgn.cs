using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionSgn : FunctionBase
    {
        public FunctionSgn()
        {
            _caption = "Sgn";
            _category = "Math";
            _key = "Sgn";
            _expression = "Sgn(number)";
            _description = @"Sgn 函数可返回指示指定数字的符号的整数。
如果数字是：
> 0 - Sgn 会返回 1。
< 0 - Sgn 会返回 - 1。
= 0 - Sgn 会返回 0。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "number", Description = "可选的。合法的数值表达式。"}
            };
        }
    }
}