using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Commands.Views
{
    public class CmdViewZoomNext:YutaiCommand
    {
        public CmdViewZoomNext(IAppContext context)
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
            if (extentStack.CanRedo())
            {
                extentStack.Redo();
                focusMap.Refresh();
            }
        }

        public override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "下一视图";
            base.m_category = "View";
            base.m_bitmap = Properties.Resources.img_zoom_next24;
            base.m_name = "View.Common.NextExtent";
            base._key = "View.Common.NextExtent";
            base.m_toolTip = "下一视图";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.NormalItem;
        }
    }
}
