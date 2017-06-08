using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.UI.Menu;

namespace Yutai.Menu
{
    internal class MenuUpdater : MenuServiceBase
    {
        private readonly IMap  _map;
        private readonly IActiveView _activeView;
        private List<string> _commandKeys;

        public MenuUpdater(IAppContext context, PluginIdentity identity,List<string> commandKeys )
            : base(context, identity)
        {
            _map = context.MapControl.Map;
            _activeView = context.MapControl.Map as IActiveView;
            _commandKeys = commandKeys;
        }

        public void Update(bool rendered)
        {
            UpdateMenu();
        }

        private void UpdateMenu()
        {
            _context.RibbonMenu.UpdateMenu();
         
        }

    }
}
