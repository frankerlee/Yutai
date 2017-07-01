using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionRnd : FunctionBase
    {
        public FunctionRnd()
        {
            _caption = "Rnd";
            _category = "Math";
            _key = "Rnd";
            _expression = "Rnd[(number)]";
            _description = "Rnd 函数可返回一个随机数。数字总是小于 1 但大于或等于 0 。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "number", Description = "可选的。合法的数值表达式。"}
            };
        }
    }
}