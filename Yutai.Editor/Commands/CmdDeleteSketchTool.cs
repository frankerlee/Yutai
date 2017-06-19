using System;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdDeleteSketchTool : YutaiCommand
    {
        public CmdDeleteSketchTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_delete;
            this.m_caption = "删除草图";
            this.m_category = "Edit";
            this.m_message = "删除草图";
            this.m_name = "Edit_DeleteSketchTool";
            this._key = "Edit_DeleteSketchTool";
            this.m_toolTip = "删除草图";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.Button;
        }

        public override bool Enabled
        {
            get
            {
                return SketchToolAssist.Feedback != null && SketchShareEx.PointCount >= 1;
            }
        }




        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            SketchShareEx.m_pLastPoint1 = null;
            SketchShareEx.m_pEndPoint1 = null;
            SketchShareEx.m_totalLength = 0.0;
            SketchShareEx.m_bInUse = false;
            SketchShareEx.LastPoint = null;
            SketchToolAssist.Feedback = null;
            SketchShareEx.PointCount = 0;
            SketchShareEx.m_bInUse = false;
            SketchShareEx.m_LastPartGeometry = null;
            (_context.FocusMap as IActiveView).Refresh();
            string string_ = "取消创建要素";
            _context.ShowCommandString(string_, CommandTipsType.CTTInput);
            SketchShareEx.m_pSketchCommandFolw.Reset();
        }

      

    }
}