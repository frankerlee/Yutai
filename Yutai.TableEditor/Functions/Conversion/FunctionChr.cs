using System.Collections.Generic;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionChr : FunctionBase
    {
        public FunctionChr()
        {
            _caption = "Chr";
            _category = "Conversion";
            _key = "Chr";
            _expression = "Chr ( x )";
            _description = "Chr 函数可把指定的 ANSI 字符代码转换为字符。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "x", Description = "必需的。标识某个字符的数字。"}
            };
        }
    }
}