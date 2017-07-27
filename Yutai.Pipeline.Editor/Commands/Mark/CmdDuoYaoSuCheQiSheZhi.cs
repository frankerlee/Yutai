using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Forms.Mark;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Mark
{
    class CmdDuoYaoSuCheQiSheZhi : YutaiTool
    {
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;

        public CmdDuoYaoSuCheQiSheZhi(IAppContext context, PipelineEditorPlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin;
        }

        public override void OnClick(object sender, EventArgs args)
        {
            if (_plugin.MultiCheQiConfig == null)
            {
                _plugin.MultiCheQiConfig = new FrmMultiCheQiConfig(_context);
            }
            (_plugin.MultiCheQiConfig as FrmMultiCheQiConfig).ShowDialog();
        }

        public sealed override void OnCreate(object hook)
        {
            _context = hook as IAppContext;
            base.m_caption = "多要素扯旗设置";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_pipe_dyscqsz;
            base.m_name = "PipelineEditor_DuoYaoSuCheQiSheZhi";
            base._key = "PipelineEditor_DuoYaoSuCheQiSheZhi";
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
