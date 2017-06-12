using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Editor.Menu;
using Yutai.Plugins.Editor.Views;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Enums;
using Editor2 = Yutai.ArcGIS.Common.Editor.Editor;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdShowEditTemplate : YutaiCommand
    {
        private EditorPlugin _plugin;
        public static IWorkspaceEdit m_pEditWorkspace;
        private bool _checked = false;
        private TemplateDockPanelService _dockPanelService;
        public override bool Enabled
        {
            get
            {

                if (_context.MapControl.Map == null)
                {
                    return false;
                }
                if (_context.MapControl.Map.LayerCount == 0)
                {
                    return false;
                }
                if ((Editor2.EditMap != null) && (Editor2.EditMap != _context.MapControl.Map))
                {
                    return false;
                }
                return (Editor2.EditWorkspace != null);
            }

        }

        public override bool Checked { get
        {
            if (_dockPanelService == null) return false;return _dockPanelService.Visible; } }

        public CmdShowEditTemplate(IAppContext context)
        {
            OnCreate(context);
            ///_plugin = plugin as EditorPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_template;
            this.m_caption = "模板窗口";
            this.m_category = "Edit";
            this.m_message = "模板窗口";
            this.m_name = "Edit_Open_Template";
            this._key = "Edit_Open_Template";
            this.m_toolTip = "打开或关闭模板窗口";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _checked = false;
           _itemType= RibbonItemType.CheckBox;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }





        public override void OnClick()
        {
            if(_dockPanelService==null)
                _dockPanelService = _context.Container.GetInstance<TemplateDockPanelService>();
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