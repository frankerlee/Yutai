using System;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Profession
{
    class CmdPointLineToRightAngle : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;

        public CmdPointLineToRightAngle(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "生成直角（点线）";
            base.m_category = "PipelineEditor";
            //base.m_bitmap = Properties.Resources.icon_valve;
            base.m_name = "PipelineEditor_PointLineToRightAngle";
            base._key = "PipelineEditor_PointLineToRightAngle";
            base.m_toolTip = "生成直角（点线）";
            base.m_checked = false;
            base.m_message = "生成直角（点线）";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Tool;
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
        }

        public override void OnDblClick()
        {

        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
        }
    }
}
