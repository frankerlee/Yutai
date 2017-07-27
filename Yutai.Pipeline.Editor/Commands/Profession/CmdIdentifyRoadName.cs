using System;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Services;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Commands.Profession
{
    class CmdIdentifyRoadName : YutaiTool
    {
        private IdentifyRoadNameDockPanelService _dockPanelService;
        private PipelineEditorPlugin _plugin;
        private IPipelineConfig _config;

        public CmdIdentifyRoadName(IAppContext context, PipelineEditorPlugin plugin)
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
            base.m_caption = "道路名称识别";
            base.m_category = "PipelineEditor";
            base.m_bitmap = Properties.Resources.icon_pipe_roadknow;
            base.m_name = "PipelineEditor_IdentifyRoadName";
            base._key = "PipelineEditor_IdentifyRoadName";
            base.m_toolTip = "道路名称识别";
            base.m_checked = false;
            base.m_message = "道路名称识别";
            base._itemType = RibbonItemType.Tool;
        }

        public override void OnClick()
        {
            _context.SetCurrentTool(this);
            if (_dockPanelService == null)
                _dockPanelService = _context.Container.GetInstance<IdentifyRoadNameDockPanelService>();
            if (_dockPanelService.Visible == false)
                _dockPanelService.Show();
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
                {
                    _dockPanelService?.Hide();
                    return false;
                }
                if (ArcGIS.Common.Editor.Editor.EditMap == null)
                {
                    _dockPanelService?.Hide();
                    return false;
                }
                if (ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    _dockPanelService?.Hide();
                    return false;
                }
                if (ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                {
                    _dockPanelService?.Hide();
                    return false;
                }
                return true;
            }
        }
    }
}
