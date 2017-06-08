// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  YutaiCommands.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/05/31  18:07
// 更新时间 :  2017/05/31  18:07

using System.Collections.Generic;
using Yutai.Plugins.Bookmark.Commands;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Bookmark.Menu
{
    public class YutaiCommands : CommandProviderBase
    {
        public YutaiCommands(IAppContext context, PluginIdentity identity) : base(context, identity)
        {
        }

        public override IEnumerable<YutaiCommand> GetCommands()
        {
            return new List<YutaiCommand>()
            {
               
                new CmdCreateBookmark(_context),
                new CmdManageBookmark(_context)
            };
        }
    }
}