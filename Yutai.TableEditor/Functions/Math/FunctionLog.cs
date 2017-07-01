using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Math
{
    class FunctionLog : FunctionBase
    {
        public FunctionLog()
        {
            _caption = "Log";
            _category = "Math";
            _key = "Log";
            _expression = "Log(number)";
            _description = "Log 函数可返回指定数据的自然对数。自然对数是以 e 为底的对数。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "number", Description = "必需的。大于 0 合法的数值表达式。"}
            };
        }
    }
}