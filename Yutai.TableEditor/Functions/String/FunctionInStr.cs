using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.TableEditor.Functions.String
{
    class FunctionInStr : FunctionBase
    {
        public FunctionInStr()
        {
            _caption = "InStr";
            _category = "String";
            _key = "InStr";
            _expression = "InStr([start,]string1,string2[,compare])";
            _description = "InStr 函数可返回一个字符串在另一个字符串中首次出现的位置。";
            _parameters = new List<Parameter>()
            {
                new Parameter()
                {
                    Order = 0,
                    Name = "start",
                    Description = "可选的。规定每次搜索的起始位置。默认是搜索起始位置是第一个字符。如果已规定 compare 参数，则必须有此参数。"
                },
                new Parameter() {Order = 1, Name = "string1", Description = "必需的。需要被搜索的字符串。"},
                new Parameter() {Order = 2, Name = "string2", Description = "必需的。需搜索的字符串。"},
                new Parameter() {Order = 2, Name = "compare", Description = @"必需的。规定要使用的字符串比较类型。默认是 0 。可采用下列值：
0 = vbBinaryCompare - 执行二进制比较。
1 = vbTextCompare - 执行文本比较。"}
            };
        }
    }
}