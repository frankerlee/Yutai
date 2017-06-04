using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    public class CmdEditorStop : YutaiCommand
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
                if (Editor2.EditMap != null && Editor2.EditMap != _context.MapControl.Map)
                {
                    return false;
                }
                if (_context.MapControl.Map.LayerCount == 0)
                {
                    if (Editor2.EditWorkspace != null)
                    {
                        this.TryClose();
                    }
                    return false;
                }
                return (Editor2.EditWorkspace != null);
            }
        }


        public CmdEditorStop(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as EditorPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_stop;
            this.m_caption = "停止编辑";
            this.m_category = "Edit";
            this.m_message = "停止编辑";
            this.m_name = "Edit.Common.StopEdit";
            this._key = "Edit.Common.StopEdit";
            this.m_toolTip = "停止编辑";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public void ActiveCommand()
        {
            if (this.Enabled)
            {
                this.TryClose();
            }
            else
            {
                _context.ShowCommandString("不能结束编辑", CommandTipsType.CTTEnd);
            }
        }

        private void TryClose()
        {
            _context.ShowCommandString("结束编辑", CommandTipsType.CTTEnd);

            bool hasEdits = false;
            EditorEvent.BeginStopEditing();
            IActiveView focusMap = _context.MapControl.Map as IActiveView;
            _context.MapControl.Map.ClearSelection();
            try
            {
                Editor2.EditWorkspace.HasEdits(ref hasEdits);
            }
            catch
            {
            }
            if (hasEdits)
            {
                DialogResult result = MessageService.Current.AskWithCancel("数据已经被修改过，保存修改吗?");
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes)
                {
                    Editor2.EditWorkspace.StopEditing(true);
                    Editor2.EditWorkspace = null;
                    Editor2.EditMap = null;
                    Editor2.CurrentEditTemplate = null;
                    //_context.HideDockWindow(EditToolUI.EditTemplateCtrl);
                    focusMap.Refresh();
                }
                else
                {
                    Editor2.EditWorkspace.StopEditing(false);
                    Editor2.EditWorkspace = null;
                    Editor2.EditMap = null;
                    Editor2.CurrentEditTemplate = null;
                    //_context.HideDockWindow(EditToolUI.EditTemplateCtrl);
                    focusMap.Refresh();
                }
            }
            else
            {
                try
                {
                    Editor2.EditWorkspace.StopEditing(true);
                }
                catch (Exception)
                {
                }
                Editor2.EditWorkspace = null;
                Editor2.EditMap = null;
                Editor2.CurrentEditTemplate = null;
                //_context.HideDockWindow(EditToolUI.EditTemplateCtrl);
                focusMap.Refresh();
            }
            // _context.ResetCurrentTool();
            _context.Config.IsInEdit = false;

            // CodeDomainManage.Clear();
            EditorEvent.StopEditing();
        }


        public override void OnClick()
        {
            this.ActiveCommand();
        }
    }
}