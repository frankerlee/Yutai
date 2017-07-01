using ESRI.ArcGIS.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Printing.Commands
{
    public sealed class CmdClearClipGeometry : YutaiCommand
    {
        public override bool Enabled
        {
            get { return this._context.FocusMap != null && this._context.FocusMap.ClipGeometry != null; }
        }

        public override void OnCreate(object hook)
        {
            this.m_category = "制图工具";
            this.m_caption = "清除";
            this.m_message = "清除裁剪区";
            this.m_toolTip = "清除裁剪区";
            this.m_name = "ClearClipRegion";
            base.m_bitmap = Properties.Resources.icon_clip_delete;
            base.m_name = "Printing_ClearClipGeometry";
            _key = "Printing_ClearClipGeometry";
            base.m_checked = false;
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
            _context = hook as IAppContext;
        }

        public CmdClearClipGeometry(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            this._context.FocusMap.ClipGeometry = null;
            this._context.ActiveView.Refresh();
        }
    }
}