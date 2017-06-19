using System;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdCompletePartTool : YutaiCommand
    {
        public CmdCompletePartTool(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_sketch_complete;
            this.m_caption = "完成部分";
            this.m_category = "Edit";
            this.m_message = "完成部分";
            this.m_name = "Edit_CompletePartTool";
            this._key = "Edit_CompletePartTool";
            this.m_toolTip = "完成部分";
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
                return SketchToolAssist.Feedback != null && ((SketchToolAssist.Feedback is INewLineFeedback && SketchShareEx.PointCount > 1) || (SketchToolAssist.Feedback is INewPolygonFeedback && SketchShareEx.PointCount > 2));
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
            SketchShareEx.LastPoint = null;
            if (SketchToolAssist.Feedback is INewLineFeedback)
            {
                (SketchToolAssist.Feedback as INewPolylineFeedback).CompletePart();
            }
            else if (SketchToolAssist.Feedback is INewPolygonFeedbackEx)
            {
                (SketchToolAssist.Feedback as INewPolygonFeedbackEx).CompletePart();
            }
            else
            {
                if (!(SketchToolAssist.Feedback is INewMultiPointFeedback))
                {
                    if (SketchToolAssist.Feedback is INewLineFeedback)
                    {
                        INewLineFeedback newLineFeedback = (INewLineFeedback)SketchToolAssist.Feedback;
                        IPolyline polyline = newLineFeedback.Stop();
                        if (polyline != null)
                        {
                            IPointCollection pointCollection = (IPointCollection)polyline;
                            if (pointCollection.PointCount >= 2)
                            {
                                IGeometry geometry = (IGeometry)pointCollection;
                                if (SketchShareEx.m_LastPartGeometry != null && SketchShareEx.m_LastPartGeometry is IPolyline)
                                {
                                    if ((SketchShareEx.m_LastPartGeometry as ITopologicalOperator).IsSimple)
                                    {
                                        (SketchShareEx.m_LastPartGeometry as ITopologicalOperator).Simplify();
                                    }
                                    if ((geometry as ITopologicalOperator).IsSimple)
                                    {
                                        (geometry as ITopologicalOperator).Simplify();
                                    }
                                    geometry = (SketchShareEx.m_LastPartGeometry as ITopologicalOperator).Union((IGeometry)pointCollection);
                                }
                                SketchShareEx.m_LastPartGeometry = geometry;
                            }
                        }
                    }
                    else if (SketchToolAssist.Feedback is INewPolygonFeedback)
                    {
                        INewPolygonFeedback newPolygonFeedback = (INewPolygonFeedback)SketchToolAssist.Feedback;
                        IPolygon polygon = newPolygonFeedback.Stop();
                        if (polygon != null)
                        {
                            IPointCollection pointCollection = (IPointCollection)polygon;
                            if (pointCollection.PointCount >= 3)
                            {
                                IGeometry geometry = (IGeometry)pointCollection;
                                if (SketchShareEx.m_LastPartGeometry != null)
                                {
                                    if (SketchShareEx.m_LastPartGeometry is IPolygon)
                                    {
                                        if ((SketchShareEx.m_LastPartGeometry as ITopologicalOperator).IsSimple)
                                        {
                                            (SketchShareEx.m_LastPartGeometry as ITopologicalOperator).Simplify();
                                        }
                                        if ((geometry as ITopologicalOperator).IsSimple)
                                        {
                                            (geometry as ITopologicalOperator).Simplify();
                                        }
                                        IGeometryCollection geometryCollection = SketchShareEx.m_LastPartGeometry as IGeometryCollection;
                                        geometryCollection.AddGeometryCollection(polygon as IGeometryCollection);
                                    }
                                }
                                else
                                {
                                    SketchShareEx.m_LastPartGeometry = geometry;
                                }
                            }
                        }
                    }
                }
                SketchToolAssist.Feedback = null;
                SketchShareEx.PointCount = 0;
                SketchShareEx.m_bInUse = false;
                _context.ActiveView.Refresh();
            }
        }
    
    }
}