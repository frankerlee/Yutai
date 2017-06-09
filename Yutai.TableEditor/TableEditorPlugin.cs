// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  TableEditorPlugin.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  11:06
// 更新时间 :  2017/06/06  11:06

using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.TableEditor.Menu;
using Yutai.Plugins.TableEditor.Services;

namespace Yutai.Plugins.TableEditor
{
    [YutaiPlugin()]
    public class TableEditorPlugin:BasePlugin
    {
        private IAppContext _context;
        private DockPanelService _dockPanelService;
        private ProjectListener _projectListener;
        private MenuListener _menuListener;

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }

        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuListener = _context.Container.GetSingleton<MenuListener>();
            _projectListener = _context.Container.GetInstance<ProjectListener>();
            _dockPanelService = _context.Container.GetInstance<DockPanelService>();
        }
    }
}