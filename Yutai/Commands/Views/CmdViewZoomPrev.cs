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
    public class CmdViewZoomPrev:YutaiCommand
    {
        public CmdViewZoomPrev(IAppContext context)
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
            IExtentStack extentStack = focusMap.ExtentStack;
            if (extentStack.CanUndo())
            {
                extentStack.Undo();
                focusMap.Refresh();
            }
        }
        public override bool Enabled
        {
            get
            {
                IActiveView focusMap = (IActiveView)_context.MapControl.ActiveView;
                IExtentStack extentStack = focusMap.ExtentStack;
                if (extentStack.CanUndo())
                {
                    return true;
                }
                return false;
            }
        }
        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "前一视图";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.img_zoom_prev24;
            base.m_name = "View.Common.PreviousExtent";
            base._key = "View.Common.PreviousExtent";
            base.m_toolTip = "前一视图";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }
}
