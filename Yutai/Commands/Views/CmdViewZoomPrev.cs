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
            IActiveView focusMap = (IActiveView)_context.FocusMap;
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
                if (this._context.FocusMap == null)
                {
                    return false;
                }
                if (this._context.FocusMap is IMapAutoExtentOptions)
                {
                    bool flag = false;
                    ((IMapAutoExtentOptions) this._context.FocusMap).LockedZoom(ref flag);
                    if (flag)
                    {
                        return false;
                    }
                    IActiveView focusMap = (IActiveView)_context.FocusMap;
                    IExtentStack extentStack = focusMap.ExtentStack;
                    if (extentStack.CanUndo())
                    {
                        return true;
                    }
                    return false;
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
            base.m_name = "View_PreviousExtent";
            base._key = "View_PreviousExtent";
            base.m_toolTip = "前一视图";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
    }
}
