using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DevExpress.XtraBars.Ribbon;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;

namespace Yutai.UI.Menu.Ribbon
{
    public class RibbonFactory
    {
        private static IRibbonMenu _instance;

        internal static IRibbonMenu InitMenus(RibbonControl ribbonManager, RibbonStatusBar statusBar)
        {
            if (_instance == null)
            {
                var menuIndex = new RibbonMenuIndex(ribbonManager, statusBar);
                _instance = new RibbonMenu(menuIndex);
                CreateDefaultHeaders(_instance);
            }
            return _instance;
        }

        private static void CreateDefaultHeaders(IRibbonMenu menu)
        {
            //这儿后期需要增加建立ApplicationMenu和QuickAccess的代码
        }

        internal static IRibbonMenu CreateMenus(IEnumerable<YutaiCommand> commands, RibbonControl ribbonManager,
            RibbonStatusBar statusBar, XmlDocument xmlDoc)
        {
            if (_instance == null)
            {
                var menuIndex = new RibbonMenuIndex(ribbonManager, statusBar);
                _instance = new RibbonMenu(menuIndex);
            }
            _instance.AddCommands(xmlDoc, commands);
            return _instance;
        }
    }
}