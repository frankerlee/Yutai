using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Forms.Mark;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Mark
{
    class CmdCheQiSheZhi : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;

        public CmdCheQiSheZhi(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
            _config = _plugin.PipeConfig;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (_plugin.CheQiConfig == null)
            {
                _plugin.CheQiConfig = new FrmCheQiConfig(_context);
            }
            (_plugin.CheQiConfig as FrmCheQiConfig).ShowDialog();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "扯旗设置";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_pipe_cqsz;
            base.m_name = "PipelineEditor_CheQiSheZhi";
            base._key = "PipelineEditor_CheQiSheZhi";
            base.m_toolTip = "";
            base.m_checked = false;
            base.m_message = "";
            base._itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null)
                    return false;
                if (_context.FocusMap.LayerCount <= 0)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap == null)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                    return false;
                if (ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                    return false;
                return true;
            }
        }
    }
}
