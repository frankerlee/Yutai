using System.Collections.Generic;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionAsc : FunctionBase
    {
        public FunctionAsc()
        {
            _caption = "Asc";
            _category = "Conversion";
            _key = "Asc";
            _expression = "Asc(string)";
            _description = "Asc 函数可把字符串中的第一个字母转换为对应的 ANSI 代码，并返回结果。";
            _parameters = new List<Parameter>()
            {
                new Parameter() { Order = 0, Name = "string", Description = "必需的。字符串表达式。不能为空字符串！"}
            };
        }
    }
}
