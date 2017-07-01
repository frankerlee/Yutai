using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionLeft : FunctionBase
    {
        public FunctionLeft()
        {
            _caption = "Left";
            _category = "String";
            _key = "Left";
            _expression = "Left(string,length)";
            _description = "Left 函数可从字符串的左侧返回指定数目的字符。";
            _parameters = new List<Parameter>()
            {
                new Parameter() {Order = 0, Name = "string", Description = "必需的。从其中返回字符的字符串。"},
                new Parameter()
                {
                    Order = 1,
                    Name = "length",
                    Description = @"必需的。规定需返回多少字符。如果设置为 0，则返回空字符串("")。如果设置为大于或等于字符串的长度，则返回整个字符串。"
                }
            };
        }
    }
}