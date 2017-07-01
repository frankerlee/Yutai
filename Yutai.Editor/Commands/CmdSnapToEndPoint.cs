using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSnapToEndPoint : YutaiCommand
    {
        public CmdSnapToEndPoint(IAppContext context)
        {
            OnCreate(context);
        }

        public override bool Checked
        {
            get { return _context.Config.IsSnapEndPoint; }
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_endpoint;
            this.m_caption = "捕捉到终点";
            this.m_category = "Edit";
            this.m_message = "捕捉到终点";
            this.m_name = "Edit_Snap_SnapEndPoint";
            this._key = "Edit_Snap_SnapEndPoint";
            this.m_toolTip = "捕捉到终点";
            _context = hook as IAppContext;
            DisplayStyleYT = DisplayStyleYT.Image;
            base.TextImageRelationYT = TextImageRelationYT.ImageBeforeText;
            base.ToolStripItemImageScalingYT = ToolStripItemImageScalingYT.None;
            _itemType = RibbonItemType.CheckBox;
        }

        public override bool Enabled
        {
            get { return true; }
        }


        public override void OnClick(object sender, EventArgs args)
        {
            OnClick();
        }

        public override void OnClick()
        {
            if (SketchShareEx.m_pAnchorPoint != null)
            {
                IPoint point = new Point();
                point.PutCoords(SketchShareEx.m_pAnchorPoint.X, SketchShareEx.m_pAnchorPoint.Y);
                point.SpatialReference = _context.FocusMap.SpatialReference;
                double snapTolerance = _context.Config.EngineSnapEnvironment.SnapTolerance;
                IFeature feature;
                Yutai.ArcGIS.Common.Editor.Editor.GetClosesFeature(_context.FocusMap, point, snapTolerance, out feature);
                if (feature != null)
                {
                    IGeometry shapeCopy = feature.ShapeCopy;
                    double num = 0.0;
                    int num2 = 0;
                    int num3 = 0;
                    bool flag = false;
                    IPoint point2 = new Point();
                    point2.PutCoords(0.0, 0.0);
                    if (shapeCopy.GeometryType == esriGeometryType.esriGeometryPolyline ||
                        shapeCopy.GeometryType == esriGeometryType.esriGeometryPolygon)
                    {
                        if ((shapeCopy as IHitTest).HitTest(point, 4.0, esriGeometryHitPartType.esriGeometryPartBoundary,
                            point2, ref num, ref num2, ref num3, ref flag))
                        {
                            double num4 = 0.0;
                            ISegmentCollection segmentCollection;
                            for (int i = 0; i < num2; i++)
                            {
                                segmentCollection =
                                    ((shapeCopy as IGeometryCollection).get_Geometry(i) as ISegmentCollection);
                                num4 += (segmentCollection as ICurve).Length;
                            }
                            segmentCollection =
                                ((shapeCopy as IGeometryCollection).get_Geometry(num2) as ISegmentCollection);
                            ISegment segment;
                            for (int i = 0; i < num3; i++)
                            {
                                segment = segmentCollection.get_Segment(i);
                                num4 += segment.Length;
                            }
                            segment = segmentCollection.get_Segment(num3);
                            ILine line = new Line();
                            line.SpatialReference = _context.FocusMap.SpatialReference;
                            line.PutCoords(point2, segment.FromPoint);
                            num4 += line.Length;
                            line.PutCoords(point2, segment.ToPoint);
                            double num5 = line.Length;
                            for (int i = num3 + 1; i < segmentCollection.SegmentCount; i++)
                            {
                                segment = segmentCollection.get_Segment(i);
                                num5 += segment.Length;
                            }
                            for (int i = num2 + 1; i < (shapeCopy as IGeometryCollection).GeometryCount; i++)
                            {
                                segmentCollection =
                                    ((shapeCopy as IGeometryCollection).get_Geometry(i) as ISegmentCollection);
                                num5 += (segmentCollection as ICurve).Length;
                            }
                            IPoint point3;
                            if (num4 < num5)
                            {
                                point3 =
                                    (shapeCopy as IPointCollection).get_Point((shapeCopy as IPointCollection).PointCount);
                            }
                            else
                            {
                                point3 =
                                    (shapeCopy as IPointCollection).get_Point((shapeCopy as IPointCollection).PointCount);
                            }
                            SketchShareEx.m_pAnchorPoint.PutCoords(point3.X, point3.Y);
                            SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, _context.ActiveView,
                                Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                        }
                    }
                    else if ((shapeCopy.GeometryType == esriGeometryType.esriGeometryMultipoint ||
                              shapeCopy.GeometryType == esriGeometryType.esriGeometryPoint) &&
                             (shapeCopy as IHitTest).HitTest(point, 4.0, esriGeometryHitPartType.esriGeometryPartVertex,
                                 point2, ref num, ref num2, ref num3, ref flag))
                    {
                        SketchShareEx.m_pAnchorPoint.PutCoords(point2.X, point2.Y);
                        SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, _context.ActiveView,
                            Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                    }
                }
            }
        }
    }
}