using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;
using Yutai.Plugins.Enums;
using Editor2 = Yutai.ArcGIS.Common.Editor.Editor;


namespace Yutai.Plugins.Editor.Commands
{
    public class CmdEditorStart : YutaiCommand
    {
        private EditorPlugin _plugin;
        public static IWorkspaceEdit m_pEditWorkspace;

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
                if (Editor2.EditWorkspace != null)
                {
                    return false;
                }
                
                    if (!_context.Config.CanEdited)
                    {
                        return false;
                    }
                    if ((Editor2.EditMap != null) && (Editor2.EditMap != _context.MapControl.Map))
                    {
                        return false;
                    }
               
                return true;

            }
        }

        static CmdEditorStart()
        {
            CmdEditorStart.old_acctor_mc();
        }

        public CmdEditorStart(IAppContext context, BasePlugin plugin)
        {
           OnCreate(context);
            _plugin = plugin as EditorPlugin;
            
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_start;
            this.m_caption = "启动编辑";
            this.m_category = "Edit";
            this.m_message = "启动编辑";
            this.m_name = "Edit.Common.StartEdit";
            this._key = "Edit.Common.StartEdit";
            this.m_toolTip = "启动编辑";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _needUpdateEvent = true;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public void ActiveCommand()
        {
            m_pEditWorkspace = null;
            if (this.Enabled)
            {
                _context.ShowCommandString("正在启动编辑", CommandTipsType.CTTLog);
                
                if (Editor2.StartEditing(_context.MapControl.Map))
                {
                    m_pEditWorkspace = Editor2.EditWorkspace;
                  
                     _context.Config.IsInEdit = true;
                     _context.ShowCommandString("启动编辑", CommandTipsType.CTTEnd);
                    EditorEvent.StartEditing();
                    //EditToolUI.EditTemplateCtrl.Map = _context.MapControl.Map;
                    //base.m_HookHelper.DockWindows(EditToolUI.EditTemplateCtrl, null);
                }
                else 
                {
                    _context.ShowCommandString("未启动编辑", CommandTipsType.CTTEnd);
                }
            }
            else
            {
                if ((Editor2.EditMap != null) && (Editor2.EditMap != _context.MapControl.Map))
                {
                    _context.ShowCommandString("已处于编辑状态", CommandTipsType.CTTEnd);
                }
                else
                {
                    _context.ShowCommandString("开始编辑命令不可用", CommandTipsType.CTTEnd);
                }
            }

        }


        private static void old_acctor_mc()
        {
            CmdEditorStart.m_pEditWorkspace = null;
        }

        public override void OnClick()
        {
            this.ActiveCommand();
        }
    }
}