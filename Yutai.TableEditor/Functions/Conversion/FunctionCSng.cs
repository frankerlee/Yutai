// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  FunctionCSng.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/16  14:24
// 更新时间 :  2017/06/16  14:24

using System.Collections.Generic;

namespace Yutai.Plugins.TableEditor.Functions.Conversion
{
    class FunctionCSng : FunctionBase
    {
        public FunctionCSng()
        {
            _caption = "CSng";
            _category = "Conversion";
            _key = "CSng";
            _expression = "CSng(expression)";
            _description = "CSng 函数可把表达式转换为单精度（Single）类型。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "expression", Description = "必需的。任何有效的表达式。"}
            };
        }
    }
}