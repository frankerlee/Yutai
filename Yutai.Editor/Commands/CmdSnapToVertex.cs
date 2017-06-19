using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSnapToVertex : YutaiCommand
    {
        public CmdSnapToVertex(IAppContext context)
        {
            OnCreate(context);
        }
        public override bool Checked { get { return _context.Config.IsSnapVertexPoint; } }
        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_vertex;
            this.m_caption = "捕捉到节点";
            this.m_category = "Edit";
            this.m_message = "捕捉到节点";
            this.m_name = "Edit_Snap_SnapVertex";
            this._key = "Edit_Snap_SnapVertex";
            this.m_toolTip = "捕捉到节点";
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
                    int index = 0;
                    int i = 0;
                    bool flag = false;
                    IPoint point2 = new Point();
                    point2.PutCoords(0.0, 0.0);
                    if (shapeCopy.GeometryType == esriGeometryType.esriGeometryPolygon || shapeCopy.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        if ((shapeCopy as IHitTest).HitTest(point, 4.0, esriGeometryHitPartType.esriGeometryPartBoundary, point2, ref num, ref index, ref i, ref flag))
                        {
                            ISegmentCollection segmentCollection = (shapeCopy as IGeometryCollection).get_Geometry(index) as ISegmentCollection;
                            ISegment segment = segmentCollection.get_Segment(i);
                            ILine line = new Line();
                            line.SpatialReference = _context.FocusMap.SpatialReference;
                            line.PutCoords(point2, segment.FromPoint);
                            double length = line.Length;
                            line.PutCoords(point2, segment.ToPoint);
                            double length2 = line.Length;
                            if (length < length2)
                            {
                                SketchShareEx.m_pAnchorPoint.PutCoords(segment.FromPoint.X, segment.FromPoint.Y);
                            }
                            else
                            {
                                SketchShareEx.m_pAnchorPoint.PutCoords(segment.ToPoint.X, segment.ToPoint.Y);
                            }
                            SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, _context.ActiveView, Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                        }
                    }
                    else if ((shapeCopy.GeometryType == esriGeometryType.esriGeometryMultipoint || shapeCopy.GeometryType == esriGeometryType.esriGeometryPoint) && (shapeCopy as IHitTest).HitTest(point, 4.0, esriGeometryHitPartType.esriGeometryPartVertex, point2, ref num, ref index, ref i, ref flag))
                    {
                        SketchShareEx.m_pAnchorPoint.PutCoords(point2.X, point2.Y);
                        SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, _context.ActiveView, Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                    }
                }
            }
        }
    }
}