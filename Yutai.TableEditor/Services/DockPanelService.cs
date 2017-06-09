// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  DockPanelService.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  15:48
// 更新时间 :  2017/06/06  15:48

using System;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Services
{
    public class DockPanelService
    {
        public DockPanelService(IAppContext context, TableEditorPresenter presenter, TableEditorPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");

            var panels = context.DockPanels;
            panels.Lock();
            panels.Add(presenter.GetInternalObject() as IDockPanelView, plugin.Identity);
            panels.Unlock();
        }
    }
}