using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSquareFinishTool : YutaiCommand
    {
        public CmdSquareFinishTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_squarefinish;
            this.m_caption = "直角结束";
            this.m_category = "Edit";
            this.m_message = "直角结束";
            this.m_name = "Edit_SquareFinishTool";
            this._key = "Edit_SquareFinishTool";
            this.m_toolTip = "直角结束";
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
                else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate == null ||
                         Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer == null)
                {
                    result = false;
                }
                else if (SketchToolAssist.Feedback == null)
                {
                    result = false;
                }
                else if (SketchToolAssist.Feedback is INewPolylineFeedback)
                {
                    result = (SketchToolAssist.Feedback as INewPolylineFeedback).CanSquareAndFinish;
                }
                else
                {
                    result = (SketchToolAssist.Feedback is INewPolygonFeedbackEx &&
                              (SketchToolAssist.Feedback as INewPolygonFeedbackEx).CanSquareAndFinish);
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
            EndSketch();
        }

        public void EndSketch()
        {
            ISpatialReference spatialReference =
                (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer as IGeoDataset).SpatialReference;
            IGeometry geometry = null;
            if (SketchToolAssist.Feedback is INewPolylineFeedback)
            {
                geometry = (SketchToolAssist.Feedback as INewPolylineFeedback).SquareAndFinish(spatialReference);
            }
            else if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
            {
                geometry = (SketchToolAssist.Feedback as INewPolygonFeedbackEx).SquareAndFinish(spatialReference);
            }
            if (geometry != null)
            {
                SketchShareEx.EndSketch(geometry, _context.ActiveView,
                    Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                SketchToolAssist.Feedback = null;
                SketchShareEx.PointCount = 0;
                SketchShareEx.m_bInUse = false;
            }
        }
    }
}