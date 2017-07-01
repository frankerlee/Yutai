using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionExp : FunctionBase
    {
        public FunctionExp()
        {
            _caption = "Exp";
            _category = "Math";
            _key = "Exp";
            _expression = "Exp(number)";
            _description = "Exp 函数可e（自然对数的底）的幂次方。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "number"}
            };
        }
    }
}