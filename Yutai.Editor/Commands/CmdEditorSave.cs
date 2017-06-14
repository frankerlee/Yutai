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
    public class CmdEditorSave : YutaiCommand
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
                if ((Editor2.EditMap != null) && (Editor2.EditMap != _context.MapControl.Map))
                {
                    return false;
                }
                return (Editor2.EditWorkspace != null);
            }
        }


        public CmdEditorSave(IAppContext context, BasePlugin plugin)
        {
            OnCreate(context);
            _plugin = plugin as EditorPlugin;
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_save;
            this.m_caption = "保存";
            this.m_category = "Edit";
            this.m_message = "保存编辑";
            this.m_name = "Edit_Common_SaveEdit";
            this._key = "Edit_Common_SaveEdit";
            this.m_toolTip = "保存编辑";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.ImageAndText;
            base.TextImageRelationYT = TextImageRelationYT.ImageAboveText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }


        public override void OnClick()
        {
            EditorEvent.BeginSaveEditing();
            bool hasEdits = false;
            Editor2.EditWorkspace.HasEdits(ref hasEdits);
            if (hasEdits)
            {
                Editor2.EditWorkspace.StopEditing(true);
                Editor2.EditWorkspace.StartEditing(true);
                ((IActiveView) _context.MapControl.Map).Refresh();
                EditorEvent.SaveEditing();
            }
        }
    }
}