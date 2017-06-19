using System;
using ESRI.ArcGIS.Carto;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdCompleteSketchTool : YutaiCommand
    {
        public CmdCompleteSketchTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_complete;
            this.m_caption = "完成草图";
            this.m_category = "Edit";
            this.m_message = "完成草图";
            this.m_name = "Edit_CompleteSketchTool";
            this._key = "Edit_CompleteSketchTool";
            this.m_toolTip = "完成草图";
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
            SketchShareEx.EndSketch(false, _context.FocusMap as IActiveView, Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
            SketchToolAssist.Feedback = null;
            SketchShareEx.PointCount = 0;
            SketchShareEx.m_bInUse = false;
            SketchShareEx.m_pSketchCommandFolw.Reset();
        }



    }
}