using ESRI.ArcGIS.Carto;
using System;
using Yutai.ArcGIS.Common;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public class CmdClearClipBounds : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.FocusMap != null && !(this._context.ActiveView is IPageLayout); }
        }

        public override void OnCreate(object hook)
        {
            this.m_caption = "清除边界";
            this.m_category = "工具";
            this.m_name = "ClearClip";
            this.m_message = "清除裁剪区域";
            this.m_toolTip = "清除裁剪区域";
            base.m_bitmap = Properties.Resources.icon_clip_delete;
            base.m_name = "Printing_ClearClipBounds";
            _key = "Printing_ClearClipBounds";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdClearClipBounds(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            (this._context.FocusMap as IMapAdmin2).ClipBounds = null;
            if (this._context.Hook is IApplication)
            {
                (this._context.Hook as IApplication).MapClipChanged(null);
            }
            (this._context.ActiveView.FocusMap as IActiveView).Refresh();
        }
    }
}