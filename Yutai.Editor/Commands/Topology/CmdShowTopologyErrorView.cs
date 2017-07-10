using System;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Menu;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands.Topology
{
    class CmdShowTopologyErrorView : YutaiCommand
    {
        private EditorPlugin _plugin;
        public static IWorkspaceEdit m_pEditWorkspace;
        private bool _checked = false;
        private TopologyErrorDockPanelService _dockPanelService;

        public override bool Enabled
        {
            get
            {
                if (_context.FocusMap == null)
                {
                    return false;
                }
                if (_context.FocusMap.LayerCount == 0)
                {
                    return false;
                }
                if ((Yutai.ArcGIS.Common.Editor.Editor.EditMap != null) && (Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.MapControl.Map))
                {
                    return false;
                }
                return (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null);
            }
        }

        public override bool Checked
        {
            get
            {
                if (_dockPanelService == null) return false;
                return _dockPanelService.Visible;
            }
        }

        public CmdShowTopologyErrorView(IAppContext context)
        {
            OnCreate(context);
            ///_plugin = plugin as EditorPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_template;
            this.m_caption = "拓扑错误";
            this.m_category = "Edit";
            this.m_message = "打开或关闭拓扑错误窗口";
            this.m_name = "Edit_Open_TopologyError";
            this._key = "Edit_Open_TopologyError";
            this.m_toolTip = "打开或关闭拓扑错误窗口";
            _context = hook as IAppContext;
            _checked = false;
            _itemType = RibbonItemType.CheckBox;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            if (_dockPanelService == null)
                _dockPanelService = _context.Container.GetInstance<TopologyErrorDockPanelService>();
            if (_dockPanelService.Visible == false)
            {
                _dockPanelService.Show();
                return;
            }
            else
            {
                _dockPanelService.Hide();
            }
        }
    }
}