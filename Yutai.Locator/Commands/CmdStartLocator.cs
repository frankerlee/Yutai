using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Yutai.UI.Docking;

namespace Yutai.Plugins.Locator.Commands
{
    public class CmdStartLocator : YutaiCommand
    {
        public CmdStartLocator(IAppContext context)
        {
            OnCreate(context);
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
            ISecureContext sContext = _context as ISecureContext;
            IDockPanel dock = _context.DockPanels.Find(DockPanelKeys.Locator);
            if (dock == null) return;
            if (sContext.YutaiProject == null ||sContext.YutaiProject.Locators==null || sContext.YutaiProject.Locators.Count==0 )
            {
                MessageService.Current.Warn("当前项目没有设置定位器");
                
                dock.Visible = false;
            }
            else
            {
                dock.Visible = true;
                dock.Activate();
            }
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "定位器";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.YTLocator;
            base.m_name = "View.Locator.StartLocator";
            base._key = "View.Locator.StartLocator";
            base.m_toolTip = "定位器";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}
