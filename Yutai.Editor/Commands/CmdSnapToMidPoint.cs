using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    public class CmdSnapToMidPoint : YutaiCommand
    {
        public CmdSnapToMidPoint(IAppContext context)
        {
            OnCreate(context);
        }

        public override void OnCreate(object hook)
        {
            this.m_bitmap = Properties.Resources.icon_snap_midpoint;
            this.m_caption = "捕捉到中点";
            this.m_category = "Edit";
            this.m_message = "捕捉到中点";
            this.m_name = "Edit_Snap_SnapMidPoint";
            this._key = "Edit_Snap_SnapMidPoint";
            this.m_toolTip = "捕捉到中点";
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
                double snapTolerance = _context.Config.SnapEnvironment.SnapTolerance;
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
                    if ((shapeCopy.GeometryType == esriGeometryType.esriGeometryPolygon || shapeCopy.GeometryType == esriGeometryType.esriGeometryPolyline) && (shapeCopy as IHitTest).HitTest(point, 4.0, esriGeometryHitPartType.esriGeometryPartBoundary, point2, ref num, ref index, ref i, ref flag))
                    {
                        ISegmentCollection segmentCollection = (shapeCopy as IGeometryCollection).get_Geometry(index) as ISegmentCollection;
                        ISegment segment = segmentCollection.get_Segment(i);
                        double x = (segment.FromPoint.X + segment.FromPoint.X) / 2.0;
                        double y = (segment.ToPoint.Y + segment.ToPoint.Y) / 2.0;
                        SketchShareEx.m_pAnchorPoint.PutCoords(x, y);
                        SketchShareEx.SketchMouseDown(SketchShareEx.m_pAnchorPoint, _context.ActiveView, Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer);
                    }
                }
            }
        }
    }
}