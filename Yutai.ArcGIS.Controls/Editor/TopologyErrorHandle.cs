using System;
using System.Collections;
using System.Reflection;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Controls.Editor.UI;

namespace Yutai.ArcGIS.Controls.Editor
{
    public class TopologyErrorHandle
    {
        public static IFeature CreateFeature(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeature feature;
            IRowSubtypes subtypes;
            IGeometry shape;
            IPolygon polygon;
            IFeatureClass class2 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            if (pTopoErrorFeat.DestinationClassID > 0)
            {
                class3 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
            }
            switch (pTopoErrorFeat.TopologyRuleType)
            {
                case esriTopologyRuleType.esriTRTAreaNoGaps:
                    break;

                case esriTopologyRuleType.esriTRTAreaNoOverlap:
                {
                    IFeature feature2 = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    IFeature feature3 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                    shape = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!shape.IsEmpty)
                    {
                        feature2.Shape = shape;
                        feature2.Store();
                    }
                    else
                    {
                        feature2.Delete();
                    }
                    shape = (feature3.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (shape.IsEmpty)
                    {
                        feature3.Delete();
                    }
                    else
                    {
                        feature3.Shape = shape;
                        feature3.Store();
                    }
                    break;
                }
                case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                    feature = class3.CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    shape = (pTopoErrorFeat as IFeature).Shape;
                    feature.Shape = shape;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                    if (pTopoErrorFeat.OriginOID <= 0)
                    {
                        feature = class2.CreateFeature();
                        subtypes = (IRowSubtypes) feature;
                        shape = (pTopoErrorFeat as IFeature).Shape;
                        feature.Shape = shape;
                        try
                        {
                            subtypes.InitDefaultValues();
                        }
                        catch
                        {
                        }
                        feature.Store();
                        return feature;
                    }
                    feature = class3.CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    shape = (pTopoErrorFeat as IFeature).Shape;
                    feature.Shape = shape;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                    feature = class3.CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    shape = (pTopoErrorFeat as IFeature).Shape;
                    feature.Shape = shape;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTAreaBoundaryCoveredByLine:
                    feature = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID).CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    polygon = (pTopoErrorFeat as IFeature).Shape as IPolygon;
                    shape = new PolylineClass();
                    (shape as IPointCollection).AddPointCollection(polygon as IPointCollection);
                    feature.Shape = shape;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTLineEndpointCoveredByPoint:
                    feature = class3.CreateFeature();
                    feature.Shape = (pTopoErrorFeat as IFeature).Shape;
                    feature.Store();
                    return feature;

                case esriTopologyRuleType.esriTRTAreaContainPoint:
                    feature = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID).CreateFeature();
                    subtypes = (IRowSubtypes) feature;
                    polygon = (pTopoErrorFeat as IFeature).Shape as IPolygon;
                    shape = (polygon as IArea).LabelPoint;
                    feature.Shape = shape;
                    try
                    {
                        subtypes.InitDefaultValues();
                    }
                    catch
                    {
                    }
                    feature.Store();
                    return feature;

                default:
                    return null;
            }
            if (pTopoErrorFeat.ShapeType == esriGeometryType.esriGeometryPolyline)
            {
                shape = new PolygonClass();
                IPolyline shapeCopy = (pTopoErrorFeat as IFeature).ShapeCopy as IPolyline;
                shape.SpatialReference = shapeCopy.SpatialReference;
                object before = Missing.Value;
                for (int i = 0; i < (shapeCopy as ISegmentCollection).SegmentCount; i++)
                {
                    (shape as ISegmentCollection).AddSegment((shapeCopy as ISegmentCollection).get_Segment(i), ref before, ref before);
                }
                if (!(shape as IPolygon).IsClosed)
                {
                    (shape as IPolygon).Close();
                }
            }
            else
            {
                shape = (pTopoErrorFeat as IFeature).ShapeCopy;
            }
            feature = class2.CreateFeature();
            feature.Shape = shape;
            subtypes = (IRowSubtypes) feature;
            try
            {
                subtypes.InitDefaultValues();
            }
            catch
            {
            }
            feature.Store();
            return feature;
        }

        private static void DeComposePolyline(IFeature pFeature, IGeometryCollection pGeometryColn)
        {
            ((pFeature.Class as IDataset).Workspace as IWorkspaceEdit).StartEditOperation();
            object before = Missing.Value;
            bool zAware = false;
            bool mAware = false;
            double zLevel = 0.0;
            try
            {
                zAware = (pGeometryColn as IZAware).ZAware;
                zLevel = (pGeometryColn as IZ).ZMin;
            }
            catch
            {
            }
            try
            {
                mAware = (pGeometryColn as IMAware).MAware;
            }
            catch
            {
            }
            for (int i = 0; i < pGeometryColn.GeometryCount; i++)
            {
                IGeometry inGeometry = pGeometryColn.get_Geometry(i);
                IGeometryCollection geometrys = new PolylineClass();
                (geometrys as IZAware).ZAware = zAware;
                (geometrys as IMAware).MAware = mAware;
                geometrys.AddGeometry(inGeometry, ref before, ref before);
                if (zAware)
                {
                    (geometrys as IZ).SetConstantZ(zLevel);
                }
                (geometrys as ITopologicalOperator).Simplify();
            }
            pFeature.Delete();
        }

        public static bool DeleteError(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeatureClass class2 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            esriTopologyRuleType topologyRuleType = pTopoErrorFeat.TopologyRuleType;
            if (topologyRuleType != esriTopologyRuleType.esriTRTPointProperlyInsideArea)
            {
                if (topologyRuleType != esriTopologyRuleType.esriTRTPointCoveredByLineEndpoint)
                {
                    return false;
                }
            }
            else
            {
                class2.GetFeature(pTopoErrorFeat.OriginOID).Delete();
                return true;
            }
            class2.GetFeature(pTopoErrorFeat.OriginOID).Delete();
            return true;
        }

        public static void DoExplode(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            ITopologicalOperator shapeCopy = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID).GetFeature(pTopoErrorFeat.OriginOID).ShapeCopy as ITopologicalOperator;
            if (pTopoErrorFeat.TopologyRuleType == esriTopologyRuleType.esriTRTLineNoMultipart)
            {
            }
        }

        public static void DoSplit(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeatureClass class2 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            esriTopologyRuleType topologyRuleType = pTopoErrorFeat.TopologyRuleType;
            if (((topologyRuleType == esriTopologyRuleType.esriTRTLineNoIntersection) || (topologyRuleType == esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch)) && ((pTopoErrorFeat as IFeature).Shape is IPoint))
            {
                int num;
                IFeature feature2;
                IFeature feature3;
                IFeature feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                IPolycurve shapeCopy = feature.ShapeCopy as IPolycurve;
                IPoint shape = (pTopoErrorFeat as IFeature).Shape as IPoint;
                IList list = PointSplitLine(shapeCopy, shape);
                for (num = 0; num < list.Count; num++)
                {
                    if (num == 0)
                    {
                        feature.Shape = list[num] as IGeometry;
                        feature.Store();
                    }
                    else
                    {
                        feature2 = RowOperator.CreatRowByRow(feature) as IFeature;
                        feature2.Shape = list[num] as IGeometry;
                    }
                }
                if (class3 == null)
                {
                    feature3 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                }
                else
                {
                    feature3 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                }
                shapeCopy = feature3.ShapeCopy as IPolycurve;
                list = PointSplitLine(shapeCopy, shape);
                for (num = 0; num < list.Count; num++)
                {
                    if (num == 0)
                    {
                        feature3.Shape = list[num] as IGeometry;
                        feature3.Store();
                    }
                    else
                    {
                        feature2 = RowOperator.CreatRowByRow(feature3) as IFeature;
                        feature2.Shape = list[num] as IGeometry;
                        feature2.Store();
                    }
                }
            }
        }

        public static bool Extend(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeatureClass class2 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            IFeature feature = null;
            if (pTopoErrorFeat.TopologyRuleType == esriTopologyRuleType.esriTRTLineNoDangles)
            {
                feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                IPoint shape = (pTopoErrorFeat as IFeature).Shape as IPoint;
                IPolyline polyline = feature.Shape as IPolyline;
                double num = CommonHelper.distance(polyline.FromPoint, shape);
                double num2 = CommonHelper.distance(polyline.ToPoint, shape);
                ISegmentCollection segments = polyline as ISegmentCollection;
                ILine inLine = null;
                IConstructLine line2 = new LineClass();
                if (num < num2)
                {
                    inLine = segments.get_Segment(0) as ILine;
                    line2.ConstructExtended(inLine, esriSegmentExtension.esriExtendAtFrom);
                }
                else
                {
                    inLine = segments.get_Segment(segments.SegmentCount - 1) as ILine;
                    line2.ConstructExtended(inLine, esriSegmentExtension.esriExtendAtTo);
                }
                IPolyline polyline2 = new PolylineClass();
                object before = Missing.Value;
                (polyline2 as ISegmentCollection).AddSegment(line2 as ISegment, ref before, ref before);
                ISpatialFilter filter = new SpatialFilterClass {
                    Geometry = polyline2,
                    SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                };
                IFeatureCursor o = class2.Search(filter, false);
                ITopologicalOperator operator2 = polyline2 as ITopologicalOperator;
                IFeature feature3 = o.NextFeature();
                bool flag = true;
                double num3 = 0.0;
                ICurve toCurve = null;
                IPoint splitPoint = null;
                IFeature feature4 = null;
                while (feature3 != null)
                {
                    if (feature3.OID != feature.OID)
                    {
                        double num4;
                        IGeometry geometry2 = operator2.Intersect(feature3.Shape, esriGeometryDimension.esriGeometry0Dimension);
                        if (geometry2 is IPoint)
                        {
                            num4 = CommonHelper.distance(geometry2 as IPoint, shape);
                            if (flag)
                            {
                                flag = false;
                                num3 = num4;
                                splitPoint = geometry2 as IPoint;
                                toCurve = feature3.Shape as ICurve;
                                feature4 = feature3;
                            }
                            else if (num3 > num4)
                            {
                                num3 = num4;
                                splitPoint = geometry2 as IPoint;
                                toCurve = feature3.Shape as ICurve;
                                feature4 = feature3;
                            }
                        }
                        if (geometry2 is IPointCollection)
                        {
                            IPointCollection points = geometry2 as IPointCollection;
                            for (int i = 0; i < points.PointCount; i++)
                            {
                                IPoint point3 = points.get_Point(i);
                                num4 = CommonHelper.distance(point3, shape);
                                if (flag)
                                {
                                    flag = false;
                                    num3 = num4;
                                    splitPoint = point3;
                                    toCurve = feature3.Shape as ICurve;
                                    feature4 = feature3;
                                }
                                else if (num3 > num4)
                                {
                                    num3 = num4;
                                    splitPoint = point3;
                                    toCurve = feature3.Shape as ICurve;
                                    feature4 = feature3;
                                }
                            }
                        }
                    }
                    feature3 = o.NextFeature();
                }
                ComReleaser.ReleaseCOMObject(o);
                if (toCurve != null)
                {
                    bool flag3;
                    int num6;
                    int num7;
                    IConstructCurve curve2 = new PolylineClass();
                    ICurve fromCurve = feature.Shape as ICurve;
                    bool extensionsPerformed = true;
                    if (num < num2)
                    {
                        curve2.ConstructExtended(fromCurve, toCurve, 0x10, ref extensionsPerformed);
                    }
                    else
                    {
                        curve2.ConstructExtended(fromCurve, toCurve, 8, ref extensionsPerformed);
                    }
                    if (!(curve2 as IGeometry).IsEmpty)
                    {
                        feature.Shape = curve2 as IGeometry;
                        feature.Store();
                    }
                    (toCurve as IPolycurve).SplitAtPoint(splitPoint, true, false, out flag3, out num6, out num7);
                    feature4.Shape = toCurve;
                    feature4.Store();
                    return true;
                }
            }
            return false;
        }

        public static void Merge(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            frmSelectMergeFeature feature;
            IGeometry geometry;
            IFeature feature2;
            IFeature feature3;
            ITopologicalOperator shapeCopy;
            IFeatureClass class2 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            string[] strArray = new string[2];
            switch (pTopoErrorFeat.TopologyRuleType)
            {
                case esriTopologyRuleType.esriTRTAreaNoOverlap:
                    strArray[0] = class2.AliasName + "-" + pTopoErrorFeat.OriginOID;
                    strArray[1] = class2.AliasName + "-" + pTopoErrorFeat.DestinationOID;
                    feature = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature.ShowDialog() == DialogResult.OK)
                    {
                        feature2 = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature3 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature.SelectedIndex == 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature3.Shape);
                            feature2.Shape = geometry;
                            feature2.Store();
                            feature3.Delete();
                        }
                        else
                        {
                            shapeCopy = feature3.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature2.Shape);
                            feature3.Shape = geometry;
                            feature3.Store();
                            feature2.Delete();
                        }
                    }
                    break;

                case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                    strArray[0] = class2.AliasName + "-" + pTopoErrorFeat.OriginOID;
                    strArray[1] = class3.AliasName + "-" + pTopoErrorFeat.DestinationOID;
                    feature = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature.ShowDialog() == DialogResult.OK)
                    {
                        feature2 = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature3 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature.SelectedIndex == 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature3.Shape);
                            feature2.Shape = geometry;
                            feature2.Store();
                            feature3.Delete();
                        }
                        else
                        {
                            shapeCopy = feature3.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature2.Shape);
                            feature3.Shape = geometry;
                            feature3.Store();
                            feature2.Delete();
                        }
                    }
                    break;

                case esriTopologyRuleType.esriTRTLineNoPseudos:
                    strArray[0] = class2.AliasName + "-" + pTopoErrorFeat.OriginOID;
                    strArray[1] = class3.AliasName + "-" + pTopoErrorFeat.DestinationOID;
                    feature = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature.ShowDialog() == DialogResult.OK)
                    {
                        feature2 = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature3 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature.SelectedIndex == 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Union(feature3.Shape);
                            feature2.Shape = geometry;
                            feature2.Store();
                            feature3.Delete();
                        }
                        else
                        {
                            geometry = (feature3.ShapeCopy as ITopologicalOperator).Union(feature2.Shape);
                            feature3.Shape = geometry;
                            feature3.Store();
                            feature2.Delete();
                        }
                    }
                    break;
            }
        }

        private static IList PointSplitLine(IPolycurve pPolycurve, IPoint pt)
        {
            int num;
            int num2;
            bool flag;
            IList list = new ArrayList();
            pPolycurve.SplitAtPoint(pt, true, true, out flag, out num2, out num);
            if (flag)
            {
                object before = Missing.Value;
                try
                {
                    int num3;
                    IGeometryCollection geometrys = new PolylineClass();
                    for (num3 = 0; num3 < num2; num3++)
                    {
                        geometrys.AddGeometry((pPolycurve as IGeometryCollection).get_Geometry(num3), ref before, ref before);
                    }
                    if ((geometrys as IPointCollection).PointCount > 1)
                    {
                        list.Add(geometrys);
                    }
                    geometrys = new PolylineClass();
                    for (num3 = num2; num3 < (pPolycurve as IGeometryCollection).GeometryCount; num3++)
                    {
                        geometrys.AddGeometry((pPolycurve as IGeometryCollection).get_Geometry(num3), ref before, ref before);
                    }
                    if ((geometrys as IPointCollection).PointCount > 1)
                    {
                        list.Add(geometrys);
                    }
                }
                catch (Exception exception)
                {
                    CErrorLog.writeErrorLog(null, exception, "");
                }
            }
            return list;
        }

        public static void Simply(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeature feature = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID).GetFeature(pTopoErrorFeat.OriginOID);
            ITopologicalOperator shapeCopy = feature.ShapeCopy as ITopologicalOperator;
            switch (pTopoErrorFeat.TopologyRuleType)
            {
                case esriTopologyRuleType.esriTRTLineNoSelfOverlap:
                case esriTopologyRuleType.esriTRTLineNoSelfIntersect:
                    shapeCopy.Simplify();
                    feature.Shape = shapeCopy as IGeometry;
                    feature.Store();
                    break;
            }
        }

        public static void Subtract(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            IGeometry geometry;
            string[] strArray;
            frmSelectMergeFeature feature3;
            IFeatureClass class2 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            IFeature feature = null;
            IFeature feature2 = null;
            ITopologicalOperator shapeCopy = null;
            switch (pTopoErrorFeat.TopologyRuleType)
            {
                case esriTopologyRuleType.esriTRTAreaNoOverlap:
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    feature2 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                    geometry = (feature.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        break;
                    }
                    feature.Delete();
                    break;

                case esriTopologyRuleType.esriTRTAreaCoveredByAreaClass:
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                    geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        return;
                    }
                    feature.Delete();
                    return;

                case esriTopologyRuleType.esriTRTAreaAreaCoverEachOther:
                    strArray = new string[] { class2.AliasName + "-" + pTopoErrorFeat.OriginOID, class3.AliasName + "-" + pTopoErrorFeat.DestinationOID };
                    feature3 = new frmSelectMergeFeature {
                        FeatureInfos = strArray,
                        Text = "删除"
                    };
                    if (feature3.ShowDialog() == DialogResult.OK)
                    {
                        feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature2 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature3.SelectedIndex != 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                            if (geometry.IsEmpty)
                            {
                                feature2.Delete();
                            }
                            else
                            {
                                feature2.Shape = geometry;
                                feature2.Store();
                            }
                            return;
                        }
                        shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                        geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                        if (!geometry.IsEmpty)
                        {
                            feature.Shape = geometry;
                            feature.Store();
                            return;
                        }
                        feature.Delete();
                    }
                    return;

                case ((esriTopologyRuleType) 6):
                case (esriTopologyRuleType.esriTRTAreaNoOverlapArea | esriTopologyRuleType.esriTRTAreaNoGaps):
                    return;

                case esriTopologyRuleType.esriTRTAreaCoveredByArea:
                    if (pTopoErrorFeat.OriginOID <= 0)
                    {
                        feature2 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                        geometry = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                        if (geometry.IsEmpty)
                        {
                            feature2.Delete();
                        }
                        else
                        {
                            feature2.Shape = geometry;
                            feature2.Store();
                        }
                        return;
                    }
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    geometry = (feature.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        return;
                    }
                    feature.Delete();
                    return;

                case esriTopologyRuleType.esriTRTAreaNoOverlapArea:
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    feature2 = class3.GetFeature(pTopoErrorFeat.DestinationOID);
                    geometry = (feature.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                    }
                    else
                    {
                        feature.Delete();
                    }
                    geometry = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                    if (geometry.IsEmpty)
                    {
                        feature2.Delete();
                    }
                    else
                    {
                        feature2.Shape = geometry;
                        feature2.Store();
                    }
                    return;

                case esriTopologyRuleType.esriTRTLineCoveredByAreaBoundary:
                    feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                    shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                    geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                    if (!geometry.IsEmpty)
                    {
                        feature.Shape = geometry;
                        feature.Store();
                        return;
                    }
                    feature.Delete();
                    return;

                case esriTopologyRuleType.esriTRTLineNoOverlap:
                    strArray = new string[] { class2.AliasName + "-" + pTopoErrorFeat.OriginOID, class2.AliasName + "-" + pTopoErrorFeat.DestinationOID };
                    feature3 = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature3.ShowDialog() == DialogResult.OK)
                    {
                        feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature2 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature3.SelectedIndex != 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                            if (geometry.IsEmpty)
                            {
                                feature2.Delete();
                            }
                            else
                            {
                                feature2.Shape = geometry;
                                feature2.Store();
                            }
                            return;
                        }
                        shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                        geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                        if (!geometry.IsEmpty)
                        {
                            feature.Shape = geometry;
                            feature.Store();
                            return;
                        }
                        feature.Delete();
                    }
                    return;

                case esriTopologyRuleType.esriTRTLineNoIntersection:
                    strArray = new string[] { class2.AliasName + "-" + pTopoErrorFeat.OriginOID, class2.AliasName + "-" + pTopoErrorFeat.DestinationOID };
                    feature3 = new frmSelectMergeFeature {
                        FeatureInfos = strArray
                    };
                    if (feature3.ShowDialog() == DialogResult.OK)
                    {
                        feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature2 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature3.SelectedIndex != 0)
                        {
                            shapeCopy = feature2.ShapeCopy as ITopologicalOperator;
                            geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                            if (geometry.IsEmpty)
                            {
                                feature2.Delete();
                            }
                            else
                            {
                                feature2.Shape = geometry;
                                feature2.Store();
                            }
                            return;
                        }
                        shapeCopy = feature.ShapeCopy as ITopologicalOperator;
                        geometry = shapeCopy.Difference((pTopoErrorFeat as IFeature).Shape);
                        if (!geometry.IsEmpty)
                        {
                            feature.Shape = geometry;
                            feature.Store();
                            return;
                        }
                        feature.Delete();
                    }
                    return;

                case esriTopologyRuleType.esriTRTLineNoIntersectOrInteriorTouch:
                    strArray = new string[] { class2.AliasName + "-" + pTopoErrorFeat.OriginOID, class2.AliasName + "-" + pTopoErrorFeat.DestinationOID };
                    feature3 = new frmSelectMergeFeature {
                        FeatureInfos = strArray,
                        Text = "删除"
                    };
                    if (feature3.ShowDialog() == DialogResult.OK)
                    {
                        feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                        feature2 = class2.GetFeature(pTopoErrorFeat.DestinationOID);
                        if (feature3.SelectedIndex != 0)
                        {
                            geometry = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                            if (geometry.IsEmpty)
                            {
                                feature2.Delete();
                            }
                            else
                            {
                                feature2.Shape = geometry;
                                feature2.Store();
                            }
                            return;
                        }
                        geometry = (feature.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
                        if (geometry.IsEmpty)
                        {
                            feature.Delete();
                        }
                        else
                        {
                            feature.Shape = geometry;
                            feature.Store();
                        }
                    }
                    return;

                default:
                    return;
            }
            geometry = (feature2.ShapeCopy as ITopologicalOperator).Difference((pTopoErrorFeat as IFeature).Shape);
            if (geometry.IsEmpty)
            {
                feature2.Delete();
            }
            else
            {
                feature2.Shape = geometry;
                feature2.Store();
            }
        }

        public static bool Trim(ITopology pTopology, ITopologyErrorFeature pTopoErrorFeat)
        {
            IFeatureClass class2 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.OriginClassID);
            IFeatureClass class3 = null;
            try
            {
                if (pTopoErrorFeat.DestinationClassID != 0)
                {
                    class3 = (pTopology as IFeatureClassContainer).get_ClassByID(pTopoErrorFeat.DestinationClassID);
                }
            }
            catch
            {
            }
            IFeature feature = null;
            if (pTopoErrorFeat.TopologyRuleType == esriTopologyRuleType.esriTRTLineNoDangles)
            {
                feature = class2.GetFeature(pTopoErrorFeat.OriginOID);
                IPoint shape = (pTopoErrorFeat as IFeature).Shape as IPoint;
                IPolyline polyline = feature.Shape as IPolyline;
                double num = CommonHelper.distance(polyline.FromPoint, shape);
                double num2 = CommonHelper.distance(polyline.ToPoint, shape);
                ISegmentCollection segments = polyline as ISegmentCollection;
                ILine line = null;
                IConstructLine line2 = new LineClass();
                if (num < num2)
                {
                    line = segments.get_Segment(0) as ILine;
                }
                else
                {
                    line = segments.get_Segment(segments.SegmentCount - 1) as ILine;
                }
                IPolyline polyline2 = new PolylineClass();
                object before = Missing.Value;
                (polyline2 as ISegmentCollection).AddSegment(line as ISegment, ref before, ref before);
                ISpatialFilter filter = new SpatialFilterClass {
                    Geometry = polyline2,
                    SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                };
                IFeatureCursor o = class2.Search(filter, false);
                ITopologicalOperator operator2 = polyline2 as ITopologicalOperator;
                IFeature feature3 = o.NextFeature();
                bool flag = true;
                double num3 = 0.0;
                ICurve curve = null;
                IPoint splitPoint = null;
                IFeature feature4 = null;
                while (feature3 != null)
                {
                    if (feature3.OID != feature.OID)
                    {
                        double num4;
                        IGeometry geometry2 = operator2.Intersect(feature3.Shape, esriGeometryDimension.esriGeometry0Dimension);
                        if (geometry2 is IPoint)
                        {
                            num4 = CommonHelper.distance(geometry2 as IPoint, shape);
                            if (flag)
                            {
                                flag = false;
                                num3 = num4;
                                splitPoint = geometry2 as IPoint;
                                curve = feature3.Shape as ICurve;
                                feature4 = feature3;
                            }
                            else if (num3 > num4)
                            {
                                num3 = num4;
                                splitPoint = geometry2 as IPoint;
                                curve = feature3.Shape as ICurve;
                                feature4 = feature3;
                            }
                        }
                        if (geometry2 is IPointCollection)
                        {
                            IPointCollection points = geometry2 as IPointCollection;
                            for (int i = 0; i < points.PointCount; i++)
                            {
                                IPoint point3 = points.get_Point(i);
                                num4 = CommonHelper.distance(point3, shape);
                                if (flag)
                                {
                                    flag = false;
                                    num3 = num4;
                                    splitPoint = point3;
                                    curve = feature3.Shape as ICurve;
                                    feature4 = feature3;
                                }
                                else if (num3 > num4)
                                {
                                    num3 = num4;
                                    splitPoint = point3;
                                    curve = feature3.Shape as ICurve;
                                    feature4 = feature3;
                                }
                            }
                        }
                    }
                    feature3 = o.NextFeature();
                }
                ComReleaser.ReleaseCOMObject(o);
                if (curve != null)
                {
                    bool flag2;
                    int num6;
                    int num7;
                    polyline.SplitAtPoint(splitPoint, true, false, out flag2, out num6, out num7);
                    if (num < num2)
                    {
                        if (num6 > 0)
                        {
                            (polyline as IGeometryCollection).RemoveGeometries(0, num6);
                        }
                        if (num7 > 0)
                        {
                            (polyline as ISegmentCollection).RemoveSegments(0, num7, false);
                        }
                    }
                    else
                    {
                        if (num6 > 0)
                        {
                            (polyline as IGeometryCollection).RemoveGeometries(num6 + 1, (polyline as IGeometryCollection).GeometryCount - num6);
                        }
                        if (num7 > 0)
                        {
                            (polyline as ISegmentCollection).RemoveSegments(num7 + 1, (polyline as ISegmentCollection).SegmentCount - num7, false);
                        }
                    }
                    if (polyline.IsEmpty)
                    {
                        feature.Delete();
                    }
                    else
                    {
                        feature.Shape = polyline;
                        feature.Store();
                    }
                    (curve as IPolycurve).SplitAtPoint(splitPoint, true, false, out flag2, out num6, out num7);
                    feature4.Shape = curve;
                    feature4.Store();
                    return true;
                }
            }
            return false;
        }
    }
}

