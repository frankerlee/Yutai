using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSnapSketch : YutaiCommand
    {
        public CmdSnapSketch(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_sketch;
            this.m_caption = "草图捕捉";
            this.m_category = "Edit";
            this.m_message = "草图捕捉";
            this.m_name = "Edit.Snap.Config.SnapSketch";
            this._key = "Edit.Snap.Config.SnapSketch";
            this.m_toolTip = "草图捕捉";
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
            get { return _context.Config.IsSnapSketch; }
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
            if (sender != null && base._needUpdateEvent)
            {
                _context.UpdateUI();
            }
        }

        public override void OnClick()
        {
            _context.Config.IsSnapSketch = !_context.Config.IsSnapSketch;
        }
    }
}