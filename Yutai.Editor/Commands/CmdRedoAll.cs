using System;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    class CmdRedoAll : YutaiCommand
    {


        public CmdRedoAll(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_edit_redoall;
            this.m_caption = "重做所有编辑";
            this.m_category = "Edit";
            this.m_message = "重做所有编辑";
            this.m_name = "Edit_RedoAll";
            this._key = "Edit_RedoAll";
            this.m_toolTip = "重做所有编辑";
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
                else if ( Yutai.ArcGIS.Common.Editor.Editor.EditMap != null && Yutai.ArcGIS.Common.Editor.Editor.EditMap != _context.FocusMap)
                {
                    result = false;
                }
                else if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                {
                    if (SketchToolAssist.Feedback != null)
                    {
                        result = false;
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
            bool flag = false;
            Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.HasRedos(ref flag);
            while (flag)
            {
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.RedoEditOperation();
                if (Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace != null)
                {
                    bool flag2 = false;
                    Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace.HasRedos(ref flag2);
                    if (flag2)
                    {
                        Yutai.ArcGIS.Common.Editor.Editor.SecondEditWorkspace.RedoEditOperation();
                    }
                }
                Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace.HasRedos(ref flag);
            }
            (_context.FocusMap as IActiveView).Refresh();
        }
    }
}