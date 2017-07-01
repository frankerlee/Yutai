using System;
using DevExpress.XtraBars;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu
{
    public abstract class MenuServiceBase
    {
        protected readonly IAppContext _context;
        protected readonly PluginIdentity _identity;
        protected IRibbonMenu _ribbonMenu;

        public MenuServiceBase(IAppContext context, PluginIdentity identity)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (identity == null) throw new ArgumentNullException("identity");

            _context = context;
            _identity = identity;
            _ribbonMenu = context.RibbonMenu;
        }

        protected BarItem FindRibbonMenuItem(string itemKey)
        {
            return _ribbonMenu.FindItem(itemKey);
        }
    }
}