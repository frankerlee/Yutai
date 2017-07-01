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
    public class CmdViewFixedZoomOut : YutaiCommand
    {
        public CmdViewFixedZoomOut(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IActiveView focusMap = (IActiveView) _context.FocusMap;
            IEnvelope extent = focusMap.Extent;
            extent.Expand(1.25, 1.25, true);
            focusMap.Extent = extent;
            focusMap.Refresh();
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "固定缩小";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.icon_zoom_out_fixed;
            base.m_name = "View_FixedZoomOut";
            base._key = "View_FixedZoomOut";
            base.m_toolTip = "固定缩小";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}