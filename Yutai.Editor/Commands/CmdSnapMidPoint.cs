using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSnapMidPoint : YutaiCommand
    {
        public CmdSnapMidPoint(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_midpoint;
            this.m_caption = "中点捕捉";
            this.m_category = "Edit";
            this.m_message = "中点捕捉";
            this.m_name = "Edit_Snap_Config_SnapMidPoint";
            this._key = "Edit_Snap_Config_SnapMidPoint";
            this.m_toolTip = "中点捕捉";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            base._needUpdateEvent = true;
        }

        public override bool Enabled
        {
            get { return true; }
        }

        public override bool Checked
        {
            get { return _context.Config.IsSnapMiddlePoint; }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
            
        }

        public override void OnClick()
        {
            _context.Config.IsSnapMiddlePoint = !_context.Config.IsSnapMiddlePoint;
        }
    }
}