using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionFix : FunctionBase
    {
        public FunctionFix()
        {
            _caption = "Fix";
            _category = "Math";
            _key = "Fix";
            _expression = "Fix ( number )";
            _description = "Fix 函数可返回指定数字的整数部分。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "number"}
            };
        }
    }
}
