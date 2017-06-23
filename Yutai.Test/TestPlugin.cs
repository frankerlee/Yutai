using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mef;
using Yutai.Plugins.Mvp;
using Yutai.Test.Menu;

namespace Yutai.Test
{
    [YutaiPlugin()]
    public class TestPlugin:BasePlugin
    {
        private MenuGenerator _menuGenerator;
        private IAppContext _context;
        public override void Initialize(IAppContext context)
        {
            _context = context;
            _menuGenerator = context.Container.GetInstance<MenuGenerator>();
        }

        protected override void RegisterServices(IApplicationContainer container)
        {
            CompositionRoot.Compose(container);
        }
    }
}
