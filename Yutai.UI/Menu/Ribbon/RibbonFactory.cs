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
    public class RibbonFactory
    {
        private static IRibbonMenu _instance;

        internal static IRibbonMenu InitMenus(RibbonControlAdv ribbonManager)
        {
            if (_instance == null)
            {
                var menuIndex = new RibbonMenuIndex(ribbonManager);
                _instance = new RibbonMenu(menuIndex);
                CreateDefaultHeaders(_instance);
            }
            return _instance;
        }

        private static void CreateDefaultHeaders(IRibbonMenu menu)
        {
            
        }
        internal static IRibbonMenu CreateMenus(IEnumerable<YutaiCommand> commands, RibbonControlAdv ribbonManager)
        {
            if (_instance == null)
            {
                var menuIndex = new RibbonMenuIndex(ribbonManager);
                _instance=new RibbonMenu(menuIndex);
            }
            foreach (YutaiCommand command in commands)
            {
                _instance.AddCommand(command);
            }
            return _instance;
        }
    }
}
