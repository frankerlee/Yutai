using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionString : FunctionBase
    {
        public FunctionString()
        {
            _caption = "String";
            _category = "String";
            _key = "String";
            _expression = "String(number,character)";
            _description = "String 函数可返回包含指定长度的重复字符的一个字符串。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "number", Description = "必需的。被返回字符串的长度。"},
                new Parameter() {Order = 1, Name = "character", Description = "必需的。被重复的字符。"}
            };
        }
    }
}