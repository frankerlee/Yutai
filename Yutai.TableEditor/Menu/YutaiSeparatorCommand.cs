// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  YutaiSeparatorCommand.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/07  16:10
// 更新时间 :  2017/06/07  16:10

using System;
using Yutai.Plugins.Concrete;

namespace Yutai.Plugins.TableEditor.Menu
{
    public class YutaiSeparatorCommand : YutaiCommand
    {
        public YutaiSeparatorCommand()
        {
            
        }
        public YutaiSeparatorCommand(string key)
        {
            this._key = key;
        }
        public override void OnClick(object sender, EventArgs args)
        {
        }

        public override void OnCreate(object hook)
        {
        }
    }
}