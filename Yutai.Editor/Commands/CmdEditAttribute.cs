using System;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Menu;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdEditAttribute : YutaiCommand
    {
        private AttributeEditDockPanelService _dockService;
        private bool bool_0 = false;

        public CmdEditAttribute(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_attribute;
            this.m_caption = "属性编辑";
            this.m_category = "Edit";
            this.m_message = "属性编辑";
            this.m_name = "Edit_Attribute";
            this._key = "Edit_Attribute";
            this.m_toolTip = "属性编辑";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                bool result;
                if (_context.FocusMap == null)
                {
                    result = false;
                }
                else if (_context.FocusMap.LayerCount == 0)
                {
                    if (_dockService != null) _dockService.Hide();
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                         Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    if (_dockService != null) _dockService.Hide();
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace == null)
                {
                    if (_dockService != null) _dockService.Hide();
                    result = false;
                }
                else
                {
                    result = true;
                }
                return result;
            }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (_dockService == null)
            {
                _dockService = _context.Container.GetInstance<AttributeEditDockPanelService>();
            }
            if (_dockService.Visible == false)
            {
                _dockService.Show();
                return;
            }
        }
    }
}