using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionCos : FunctionBase
    {
        public FunctionCos()
        {
            _caption = "Cos";
            _category = "Math";
            _key = "Cos";
            _expression = "Cos(number)";
            _description = "Cos 函数可返回指定数字（角度）的余弦。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "number"}
            };
        }
    }
}
