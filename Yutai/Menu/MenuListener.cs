using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Shared;
using Yutai.UI.Docking;

namespace Yutai.Menu
{
    public class MenuListener
    {
        private readonly IAppContext _context;
      
        private readonly IProjectService _projectService;

        public MenuListener(
            IAppContext context,
            IProjectService projectService
           )
        {
            Logger.Current.Trace("In MenuListener");
            if (context == null) throw new ArgumentNullException("context");

            _context = context;

            var appContext = context as AppContext;
            if (appContext != null)
            {
                appContext.Broadcaster.ItemClicked += ItemClicked;
            }
           
        }

        public void RunCommand(string menuKey)
        {
            if (HandleCursorChanged(menuKey) || HandleProjectCommand(menuKey) || HandleDialogs(menuKey) ||
                HandleHelpMenu(menuKey) || HandleLayerMenu(menuKey) || HandleConfigChanged(menuKey))
            {
                _context.View.Update();
                return;
            }

            

            _context.View.Update();
        }

        private bool HandleConfigChanged(string itemKey)
        {
            var config = AppConfig.Instance;

          

            return false;
        }

        private bool HandleCursorChanged(string itemKey)
        {
            // MapCursorChanged event is raised automatically; no need to update UI manually
        
            return false;
        }

        private bool HandleDialogs(string itemKey)
        {
           
            return false;
        }

        private bool HandleHelpMenu(string itemKey)
        {
        

            return false;
        }

        private bool HandleLayerMenu(string itemKey)
        {
       

            return false;
        }

        private bool HandleProjectCommand(string itemKey)
        {
           
            return false;
        }

        private void ItemClicked(object sender, EventArgs e)
        {
           
        }

        private void MenuItemClicked(object sender, MenuItemEventArgs e)
        {
            RunCommand(e.ItemKey);
        }
        
    }
}
