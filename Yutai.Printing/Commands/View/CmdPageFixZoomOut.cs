using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdPageFixZoomOut : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.ActiveView is IPageLayout; }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "固定缩小";
            this.m_toolTip = "页面固定缩小";
            this.m_category = "页面操作";
            base.m_bitmap = Properties.Resources.PageFixZoomOut;
            base.m_name = "Printing_PageFixZoomOut";
            _key = "Printing_PageFixZoomOut";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdPageFixZoomOut(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            IActiveView activeView = (IActiveView) this._context.ActiveView;
            IEnvelope extent = activeView.Extent;
            extent.Expand(1.25, 1.25, true);
            activeView.Extent = extent;
            activeView.Refresh();
            //if (this._context.Hook is IApplication)
            //{
            //	DocumentManager.DocumentChanged((this._context.Hook as IApplication).Hook);
            //}
            //else
            //{
            //	DocumentManager.DocumentChanged(this._context.Hook);
            //}
        }
    }
}