using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Forms.Mark;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Mark
{
    class CmdBiaoZhu : YutaiTool
    {
        private PipelineEditorPlugin _plugin;

        public CmdBiaoZhu(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            FrmPipeLabelsSetting frm = new FrmPipeLabelsSetting(_context, _plugin.PipeConfig);
            frm.ShowDialog();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "标注";
            base.m_category = "PipelineEditor";
            //base.m_bitmap = Properties.Resources.icon_valve;
            base.m_name = "PipelineEditor_BiaoZhu";
            base._key = "PipelineEditor_BiaoZhu";
            base.m_toolTip = "";
            base.m_checked = false;
            base.m_message = "";
            base.m_enabled = true;
            base._itemType = RibbonItemType.Button;
        }
        
        public override void OnDblClick()
        {

        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
        }

        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null)
                    return false;
                if (_context.FocusMap.LayerCount <= 0)
                    return false;
                return true;
            }
        }
    }
}
