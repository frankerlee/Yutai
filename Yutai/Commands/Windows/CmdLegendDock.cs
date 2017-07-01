using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Services.Serialization;
using Yutai.UI.Docking;

namespace Yutai.Commands.Windows
{
    class CmdLegendDock : YutaiCommand
    {
        public CmdLegendDock(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick()
        {
            ISecureContext sContext = _context as ISecureContext;
            DockPanel dock = _context.DockPanels.GetDockPanel(MapLegendDockPanel.DefaultDockName);
            if (dock == null) return;
            dock.Visible = !dock.Visible;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "二维图例";
            base.m_category = "Window";
            base.m_bitmap = Properties.Resources.icon_maplegend;
            base.m_name = "Window.Common.MapLegend";
            base._key = "Window_Common_MapLegend";
            base.m_toolTip = "二维图例";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}