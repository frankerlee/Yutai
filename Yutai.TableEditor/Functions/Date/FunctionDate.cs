using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Date
{
    class FunctionDate : FunctionBase
    {
        public FunctionDate()
        {
            _caption = "Date";
            _category = "Date";
            _key = "Date";
            _expression = "Date";
            _description = "Date 函数可返回当前的系统日期。";
            _parameters = new List<Parameter>()
            {
            };
        }
    }
}