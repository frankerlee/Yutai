using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionSqr : FunctionBase
    {
        public FunctionSqr()
        {
            _caption = "Sqr";
            _category = "Math";
            _key = "Sqr";
            _expression = "Sqr(number)";
            _description = "Sqr 函数可返回一个数的平方根。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "number", Description = "必需的。大于等于 0 的有效数值表达式。"}
            };
        }
    }
}
