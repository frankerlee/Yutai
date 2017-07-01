using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.Date
{
    class FunctionNow : FunctionBase
    {
        public FunctionNow()
        {
            _caption = "Now";
            _category = "Date";
            _key = "Now";
            _expression = "Now";
            _description = "Now 函数可根据计算机系统的日期和时间设置返回当前的日期和时间。";
            _parameters = new List<Parameter>()
            {
            };
        }
    }
}