using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu.Ribbon
{
    public abstract class RibbonMenuServiceBase
    {
        protected readonly IAppContext _context;
        protected readonly PluginIdentity _identity;
        protected readonly IRibbonMenu _ribbonManager;

        public RibbonMenuServiceBase(IAppContext context, PluginIdentity identity)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (identity == null) throw new ArgumentNullException("identity");

            _context = context;
            _identity = identity;
            _ribbonManager = _context.RibbonMenu;
        }
    }
}