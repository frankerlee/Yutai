using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionSin : FunctionBase
    {
        public FunctionSin()
        {
            _caption = "Sin";
            _category = "Math";
            _key = "Sin";
            _expression = "Sin(number)";
            _description = "Sin 函数可返回指定数字（角度）的正弦。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "number", Description = "必需的。将某个角表示为弧度的有效表达式。"}
            };
        }
    }
}
