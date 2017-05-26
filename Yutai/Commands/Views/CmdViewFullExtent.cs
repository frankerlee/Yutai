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

namespace Yutai.Commands.Views
{
    public class CmdViewFullExtent:YutaiCommand
    {
        public CmdViewFullExtent(IAppContext context)
        {
            OnCreate(context);
        }
        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }
        public override void OnClick()
        {
            IActiveView focusMap = (IActiveView)_context.MapControl.ActiveView;
            IEnvelope extent = focusMap.FullExtent;
            focusMap.Extent = extent;
            focusMap.Refresh();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "全图";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_zoom_max_extents;
            base.m_name = "View.Common.FullExtent";
            base._key = "View.Common.FullExtent";
            base.m_toolTip = "全图";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }
}
