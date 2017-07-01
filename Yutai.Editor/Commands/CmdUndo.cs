using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdUndo : YutaiCommand
    {
        public CmdUndo(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_undo;
            this.m_caption = "撤销";
            this.m_category = "Edit";
            this.m_message = "撤销";
            this.m_name = "Edit_Undo";
            this._key = "Edit_Undo";
            this.m_toolTip = "撤销";
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
                    result = false;
                }
                else if (!Yutai.ArcGIS.Common.Editor.Editor.EnableUndoRedo)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                         Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                {
                    if (SketchToolAssist.Feedback != null && SketchToolAssist.Feedback is IOperation &&
                        (SketchToolAssist.Feedback as IOperation).CanUndo)
                    {
                        result = true;
                    }
                    else
                    {
                        bool flag = false;
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.HasUndos(ref flag);
                        result = flag;
                    }
                }
                else
                {
                    result = false;
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
            try
            {
                if (SketchToolAssist.Feedback != null && SketchToolAssist.Feedback is IOperation)
                {
                    (SketchToolAssist.Feedback as IOperation).Undo();
                }
                else
                {
                    Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.UndoEditOperation();
                    if (Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace != null)
                    {
                        bool flag = false;
                        Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace.HasUndos(ref flag);
                        if (flag)
                        {
                            Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace.UndoEditOperation();
                        }
                    }
                    if (Yutai.ArcGIS.Common.Editor.Editor.EditFeature != null)
                    {
                        EditorEvent.FeatureGeometryChanged(Yutai.ArcGIS.Common.Editor.Editor.EditFeature);
                    }
                    (_context.FocusMap as IActiveView).Refresh();
                }
            }
            catch (Exception exception_)
            {
                //CErrorLog.writeErrorLog(this, exception_, "");
            }
        }
    }
}