using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionMid : FunctionBase
    {
        public FunctionMid()
        {
            _caption = "Mid";
            _category = "String";
            _key = "Mid";
            _expression = "Mid(string,start[,length])";
            _description = "Mid 函数可从字符串中返回指定数目的字符。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "string", Description = "必需的。从其中返回字符的字符串表达式。如果字符串包含 Null，则返回 Null。"},
                new Parameter() {Order = 1, Name = "start", Description = @"必需的。规定起始位置。如果设置为大于字符串中的字符数目，则返回空字符串("")。"},
                new Parameter()
                {
                    Order = 2,
                    Name = "length",
                    Description = @"可选的。要返回的字符数目。如果省略或 length 超过文本的字符数（包括 start 处的字符），将返回字符串中从 start 到字符串结束的所有字符。"
                }
            };
        }
    }
}