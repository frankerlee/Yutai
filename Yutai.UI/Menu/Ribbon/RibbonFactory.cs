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

        internal static IRibbonMenu CreateMenus(IEnumerable<YutaiCommand> commands, RibbonControlAdv ribbonManager)
        {
            if (_instance == null)
            {
                var menuIndex = new RibbonMenuIndex();
                _instance=new RibbonMenu(menuIndex,ribbonManager);
            }
            foreach (YutaiCommand command in commands)
            {
                _instance.AddCommand(command);
            }
            return _instance;
        }
    }
}
