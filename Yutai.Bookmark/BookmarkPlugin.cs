// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  BookmarkPlugin.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/05/31  17:50
// 更新时间 :  2017/05/31  17:50

using System;
using Yutai.Plugins.Bookmark.Menu;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Bookmark
{
    [YutaiPlugin()]
    public class BookmarkPlugin : BasePlugin
    {
        private MenuGenerator _menuGenerator;
        private IAppContext _context;

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }

        public override void Initialize(IAppContext context)
        {
            //if (context == null) throw new ArgumentNullException("context");

            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
        }
    }
}