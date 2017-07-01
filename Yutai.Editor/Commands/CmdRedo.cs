using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdRedo : YutaiCommand
    {
        public CmdRedo(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_redo;
            this.m_caption = "重做";
            this.m_category = "Edit";
            this.m_message = "重做";
            this.m_name = "Edit_Redo";
            this._key = "Edit_Redo";
            this.m_toolTip = "重做";
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
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                         Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (!Yutai.ArcGIS.Common.Editor.Editor.EnableUndoRedo)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                {
                    if (SketchToolAssist.Feedback != null && SketchToolAssist.Feedback is IOperation &&
                        (SketchToolAssist.Feedback as IOperation).CanRedo)
                    {
                        result = true;
                    }
                    else
                    {
                        bool flag = false;
                        Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.HasRedos(ref flag);
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
            if (SketchToolAssist.Feedback != null && SketchToolAssist.Feedback is IOperation &&
                (SketchToolAssist.Feedback as IOperation).CanRedo)
            {
                (SketchToolAssist.Feedback as IOperation).Redo();
            }
            else
            {
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.RedoEditOperation();
                if (Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace != null)
                {
                    bool flag = false;
                    Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace.HasRedos(ref flag);
                    if (flag)
                    {
                        Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace.RedoEditOperation();
                    }
                }
                (_context.FocusMap as IActiveView).Refresh();
            }
        }
    }
}