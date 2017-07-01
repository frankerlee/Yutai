using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionAbs : FunctionBase
    {
        public FunctionAbs()
        {
            _caption = "Abs";
            _category = "Math";
            _key = "Abs";
            _expression = "Abs(number)";
            _description = "Abs 函数可返回指定的数字的绝对值。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "number", Description = "必需的。一个数值表达式。"}
            };
        }
    }
}