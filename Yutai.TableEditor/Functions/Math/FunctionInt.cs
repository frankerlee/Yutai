using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionInt : FunctionBase
    {
        public FunctionInt()
        {
            _caption = "Int";
            _category = "Math";
            _key = "Int";
            _expression = "Int(number)";
            _description = "Int 函数可返回指定数字的整数部分。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "number"}
            };
        }
    }
}
