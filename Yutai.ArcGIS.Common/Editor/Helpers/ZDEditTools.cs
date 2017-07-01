using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.Display;
using Yutai.ArcGIS.Common.Framework;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.ArcGIS.Common.Helpers;
using Yutai.ArcGIS.Common.Symbol;
using Yutai.ArcGIS.Common.ZD;
using Yutai.Plugins.Interfaces;
using Yutai.Shared;
using WorkspaceOperator = Yutai.Shared.WorkspaceOperator;

namespace Yutai.ArcGIS.Common.Editor.Helpers
{
    public class ZDEditTools
    {
        public static List<int> Oids;

        private static ITable _ZDChangeHisInfoTable;

        public static DateTime StartEditDateTime { get; set; }

        public static ITable ZDChangeHisInfoTable
        {
            get
            {
                if (ZDEditTools._ZDChangeHisInfoTable == null)
                {
                    ZDEditTools._ZDChangeHisInfoTable = AppConfigInfo.OpenTable("ZDHistory");
                }
                return ZDEditTools._ZDChangeHisInfoTable;
            }
        }

        private static IFeatureClass ZDFeatureClass { get; set; }

        public static IFeatureLayer ZDFeatureLayer { get; set; }

        public static IFeatureClass ZDHisFeatureClass { get; set; }

        static ZDEditTools()
        {
            ZDEditTools.old_acctor_mc();
        }

        public ZDEditTools()
        {
        }

        public static void ChangeAttribute(IFeature ifeature_0, SortedList<string, object> sortedList_0)
        {
            ZDEditTools.StartEditDateTime = DateTime.Now;
            if (ZDEditTools.Oids.IndexOf(ifeature_0.OID) == -1)
            {
                ZDEditTools.Oids.Add(ifeature_0.OID);
                IFeature feature = ZDEditTools.WriteHistory(ifeature_0);
                ZDEditTools.WriteHistoryLine(ifeature_0, feature, 5, ZDEditTools.StartEditDateTime);
            }
            foreach (KeyValuePair<string, object> sortedList0 in sortedList_0)
            {
                RowOperator.SetFieldValue(ifeature_0, sortedList0.Key, sortedList0.Value);
            }
            ifeature_0.Store();
        }

        private static void CheckGroupLayer(List<IFeatureLayer> list_0, ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.Layer[i];
                if (layer is IGroupLayer)
                {
                    ZDEditTools.CheckGroupLayer(list_0, layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    if (ZDRegister.IsZDFeatureClass((layer as IFeatureLayer).FeatureClass))
                    {
                        list_0.Add(featureLayer);
                    }
                }
            }
        }

        private static void CheckGroupLayerEdit(List<IFeatureLayer> list_0, ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.Layer[i];
                if (layer is IGroupLayer)
                {
                    ZDEditTools.CheckGroupLayerEdit(list_0, layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    if (ZDEditTools.CheckLayerIsCanEditZD(featureLayer))
                    {
                        list_0.Add(featureLayer);
                    }
                }
            }
        }

        private static bool CheckLayerIsCanEditZD(IFeatureLayer ifeatureLayer_0)
        {
            bool flag = ZDEditTools.LayerCanEdit(ifeatureLayer_0);
            bool flag1 = flag;
            if (flag)
            {
                flag1 = ZDRegister.IsZDFeatureClass(ifeatureLayer_0.FeatureClass);
            }
            return flag1;
        }

        public static IFeature CreateZD(IGeometry igeometry_0)
        {
            ZDEditTools.StartEditDateTime = DateTime.Now;
            IFeature igeometry0 = ZDEditTools.ZDFeatureClass.CreateFeature();
            igeometry0.Shape = igeometry_0;
            igeometry0.Store();
            ZDEditTools.WriteHistoryLine(igeometry0, null, 0, ZDEditTools.StartEditDateTime);
            ZDEditTools.Oids.Add(igeometry0.OID);
            return igeometry0;
        }

        public static void Delete(IFeature ifeature_0)
        {
            ZDEditTools.DeleteZD(ifeature_0);
        }

        public static void DeletedSelectedZD(IFeatureSelection ifeatureSelection_0)
        {
            IEnumIDs ds = ifeatureSelection_0.SelectionSet.IDs;
            ds.Reset();
            int num = ds.Next();
            List<IFeature> features = new List<IFeature>();
            IFeatureClass featureClass = (ifeatureSelection_0 as IFeatureLayer).FeatureClass;
            while (num > 0)
            {
                features.Add(featureClass.GetFeature(num));
                num = ds.Next();
            }
            foreach (IFeature feature in features)
            {
                ZDEditTools.DeleteZD(feature);
            }
        }

        public static void DeletedSelectedZD(ICursor icursor_0)
        {
            List<IFeature> features = new List<IFeature>();
            for (IRow i = icursor_0.NextRow(); i != null; i = icursor_0.NextRow())
            {
                ZDEditTools.DeleteZD(i as IFeature);
            }
        }

        public static void DeletedSelectedZD(IMap imap_0, IFeatureLayer ifeatureLayer_0)
        {
            Editor.StartEditOperation();
            IWorkspace workspace = AppConfigInfo.GetWorkspace();
            if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.EditWorkspace as IWorkspace))
            {
                (workspace as IWorkspaceEdit).StartEditOperation();
            }
            ZDEditTools.DeletedSelectedZD(ifeatureLayer_0 as IFeatureSelection);
            if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.EditWorkspace as IWorkspace))
            {
                (workspace as IWorkspaceEdit).StopEditOperation();
            }
            Editor.StopEditOperation();
            imap_0.ClearSelection();
            (imap_0 as IActiveView).Refresh();
        }

        private static void DeleteZD(IFeature ifeature_0)
        {
            ZDEditTools.StartEditDateTime = DateTime.Now;
            IFeature feature = ZDEditTools.WriteHistory(ifeature_0);
            ZDEditTools.WriteDelHistoryLine(ifeature_0, feature, 4, ZDEditTools.StartEditDateTime);
            ifeature_0.Delete();
        }

        public static void FireAfterCreateZD(IRow irow_0)
        {
            if (ZDEditTools.OnAfterCreateZD != null)
            {
                ZDEditTools.OnAfterCreateZD(irow_0);
            }
        }

        public static void FireEndZDEdit()
        {
            if (ZDEditTools.OnEndZDEdit != null)
            {
                ZDEditTools.OnEndZDEdit();
            }
        }

        public static void FireStartZDEdit()
        {
            if (ZDEditTools.OnStartZDEdit != null)
            {
                ZDEditTools.OnStartZDEdit();
            }
        }

        public static IFeatureLayer GetFirstZDLayer(IMap imap_0)
        {
            IFeatureLayer featureLayer;
            IEnumLayer layers = imap_0.Layers[null, true];
            layers.Reset();
            ILayer layer = layers.Next();
            while (true)
            {
                if (layer == null)
                {
                    featureLayer = null;
                    break;
                }
                else if (!(layer is IFeatureLayer) || !ZDRegister.IsZDFeatureClass((layer as IFeatureLayer).FeatureClass))
                {
                    layer = layers.Next();
                }
                else
                {
                    featureLayer = layer as IFeatureLayer;
                    break;
                }
            }
            return featureLayer;
        }

        public static List<HitFeatureInfo> GetHitInfo(IFeature ifeature_0, IPoint ipoint_0)
        {
            List<HitFeatureInfo> hitFeatureInfos = new List<HitFeatureInfo>();
            List<IFeature> features = new List<IFeature>();
            IEnvelope envelope = ipoint_0.Envelope;
            envelope.Expand(0.1, 0.1, false);
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            IFeatureCursor featureCursor = (ifeature_0.Class as IFeatureClass).Search(spatialFilterClass, false);
            for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
            {
                if (i.OID != ifeature_0.OID)
                {
                    features.Add(i);
                }
            }
            ComReleaser.ReleaseCOMObject(featureCursor);
            foreach (IFeature feature in features)
            {
                IGeometry shapeCopy = feature.ShapeCopy;
                IPoint pointClass = new Point();
                double num = 0;
                int num1 = 0;
                int num2 = 0;
                bool flag = false;
                if (
                    !(shapeCopy as IHitTest).HitTest(ipoint_0, 1E-05, esriGeometryHitPartType.esriGeometryPartVertex,
                        pointClass, ref num, ref num1, ref num2, ref flag))
                {
                    if (
                        !(shapeCopy as IHitTest).HitTest(ipoint_0, 1E-05,
                            esriGeometryHitPartType.esriGeometryPartBoundary, pointClass, ref num, ref num1, ref num2,
                            ref flag) || num >= 0.0001)
                    {
                        continue;
                    }
                    HitFeatureInfo hitFeatureInfo = new HitFeatureInfo()
                    {
                        Feature = feature,
                        PartIndex = num1,
                        SegmentIndex = num2
                    };
                    hitFeatureInfos.Add(hitFeatureInfo);
                }
                else
                {
                    HitFeatureInfo hitFeatureInfo1 = new HitFeatureInfo()
                    {
                        Feature = feature,
                        PartIndex = num1,
                        VertIndex = num2
                    };
                    hitFeatureInfos.Add(hitFeatureInfo1);
                }
            }
            return hitFeatureInfos;
        }

        private static void GetHitZD(IFeatureClass ifeatureClass_0, IPolyline ipolyline_0)
        {
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = ipolyline_0,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            IFeatureCursor featureCursor = ifeatureClass_0.Search(spatialFilterClass, false);
            IFeature feature = featureCursor.NextFeature();
            while (feature != null)
            {
                feature = featureCursor.NextFeature();
            }
        }

        public static List<IFeatureLayer> GetZDLayers(IMap imap_0)
        {
            List<IFeatureLayer> featureLayers = new List<IFeatureLayer>();
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                ILayer layer = imap_0.Layer[i];
                if (layer is IGroupLayer)
                {
                    ZDEditTools.CheckGroupLayer(featureLayers, layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    if (ZDRegister.IsZDFeatureClass((layer as IFeatureLayer).FeatureClass))
                    {
                        featureLayers.Add(featureLayer);
                    }
                }
            }
            return featureLayers;
        }

        public static bool HasZDLayer(IMap imap_0)
        {
            bool flag = false;
            IEnumLayer layers = imap_0.Layers[null, true];
            layers.Reset();
            for (ILayer i = layers.Next(); i != null; i = layers.Next())
            {
                if (i is IFeatureLayer)
                {
                    bool flag1 = ZDRegister.IsZDFeatureClass((i as IFeatureLayer).FeatureClass);
                    flag = flag1;
                    if (flag1)
                    {
                        break;
                    }
                }
            }
            return flag;
        }

        private static void HitTest(IFeature ifeature_0, IPoint ipoint_0)
        {
            IGeometry shapeCopy = ifeature_0.ShapeCopy;
            IPoint pointClass = new Point();
            double num = 0;
            int num1 = 0;
            int num2 = 0;
            bool flag = false;
            if (
                (!(shapeCopy as IHitTest).HitTest(ipoint_0, 0.0001, esriGeometryHitPartType.esriGeometryPartVertex,
                     pointClass, ref num, ref num1, ref num2, ref flag) || num >= 0.0001) &&
                (shapeCopy as IHitTest).HitTest(ipoint_0, 0.0001, esriGeometryHitPartType.esriGeometryPartBoundary,
                    pointClass, ref num, ref num1, ref num2, ref flag) && num < 0.0001)
            {
                ISegmentCollection geometry = (shapeCopy as IGeometryCollection).Geometry[num1] as ISegmentCollection;
                ISegment segment = geometry.Segment[num2];
                if (segment is ILine)
                {
                    ILine lineClass = new Line();
                    lineClass.PutCoords((segment as ILine).FromPoint, pointClass);
                    ILine line = new Line();
                    line.PutCoords(pointClass, (segment as ILine).ToPoint);
                    ISegmentCollection polylineClass = new Polyline() as ISegmentCollection;
                    object value = Missing.Value;
                    object obj = Missing.Value;
                    polylineClass.AddSegment(lineClass as ISegment, ref value, ref obj);
                    geometry.ReplaceSegmentCollection(num2, 1, polylineClass);
                    polylineClass = new Polyline() as ISegmentCollection;
                    object value1 = Missing.Value;
                    object obj1 = Missing.Value;
                    polylineClass.AddSegment(line as ISegment, ref value1, ref obj1);
                    geometry.InsertSegmentCollection(num2 + 1, polylineClass);
                    geometry.SegmentsChanged();
                    (shapeCopy as IGeometryCollection).GeometriesChanged();
                    ifeature_0.Shape = shapeCopy;
                    ifeature_0.Store();
                }
            }
        }

        private static bool LayerCanEdit(IFeatureLayer ifeatureLayer_0)
        {
            bool flag;
            if (Editor.EditWorkspace != null)
            {
                IDataset featureClass = ifeatureLayer_0.FeatureClass as IDataset;
                if (featureClass == null)
                {
                    flag = false;
                }
                else if (!(featureClass is ICoverageFeatureClass))
                {
                    IPropertySet connectionProperties = featureClass.Workspace.ConnectionProperties;
                    IWorkspace editWorkspace = Editor.EditWorkspace as IWorkspace;
                    if (connectionProperties.IsEqual(editWorkspace.ConnectionProperties))
                    {
                        if (editWorkspace.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace)
                        {
                            if ((AppConfigInfo.UserID.Length == 0 ? false : !(AppConfigInfo.UserID.ToLower() == "admin")))
                            {
                                if (!Editor.LayerIsHasEditprivilege(ifeatureLayer_0))
                                {
                                    goto Label1;
                                }
                                flag = true;
                                return flag;
                            }
                            else
                            {
                                flag = true;
                                return flag;
                            }
                        }
                        else if (editWorkspace is IVersionedWorkspace)
                        {
                            if ((featureClass as IVersionedObject).IsRegisteredAsVersioned)
                            {
                                if ((AppConfigInfo.UserID.Length == 0
                                    ? false
                                    : !(AppConfigInfo.UserID.ToLower() == "admin")))
                                {
                                    if (
                                        !Editor.m_SysGrants.GetStaffAndRolesDatasetPri(AppConfigInfo.UserID, 2,
                                            featureClass.Name))
                                    {
                                        goto Label1;
                                    }
                                    flag = true;
                                    return flag;
                                }
                                else
                                {
                                    flag = true;
                                    return flag;
                                }
                            }
                        }
                        else if ((AppConfigInfo.UserID.Length == 0 ? false : !(AppConfigInfo.UserID.ToLower() == "admin")))
                        {
                            if (!Editor.LayerIsHasEditprivilege(ifeatureLayer_0))
                            {
                                goto Label1;
                            }
                            flag = true;
                            return flag;
                        }
                        else
                        {
                            flag = true;
                            return flag;
                        }
                    }
                    Label1:
                    flag = false;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private static void old_acctor_mc()
        {
            ZDEditTools.Oids = new List<int>();
            ZDEditTools._ZDChangeHisInfoTable = null;
        }

        public static List<IFeature> Split(IFeatureClass ifeatureClass_0, IPolyline ipolyline_0)
        {
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = ipolyline_0,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            IFeatureCursor featureCursor = ifeatureClass_0.Search(spatialFilterClass, false);
            IFeature feature = featureCursor.NextFeature();
            List<IFeature> features = new List<IFeature>();
            while (feature != null)
            {
                List<IFeature> features1 = ZDEditTools.Split(feature, ipolyline_0);
                if (features1.Count > 0)
                {
                    features.AddRange(features1);
                }
                feature = featureCursor.NextFeature();
            }
            ComReleaser.ReleaseCOMObject(featureCursor);
            return features;
        }

        public static List<IFeature> Split(IFeatureSelection ifeatureSelection_0, IPolyline ipolyline_0)
        {
            ICursor cursor;
            ifeatureSelection_0.SelectionSet.Search(null, false, out cursor);
            IFeature feature = cursor.NextRow() as IFeature;
            ComReleaser.ReleaseCOMObject(cursor);
            List<IFeature> features = new List<IFeature>();
            if (feature != null)
            {
                features = ZDEditTools.Split(feature, ipolyline_0);
            }
            return features;
        }

        public static List<IFeature> Split(IFeature ifeature_0, IPolyline ipolyline_0)
        {
            IGeometry geometry;
            IGeometry geometry1;
            IFeature feature;
            IFeature feature1;
            List<IFeature> features = new List<IFeature>();
            ITopologicalOperator4 shapeCopy = ifeature_0.ShapeCopy as ITopologicalOperator4;
            IGeometry geometry2 = shapeCopy.Intersect(ipolyline_0, esriGeometryDimension.esriGeometry0Dimension);
            List<IFeature> features1 = new List<IFeature>();
            ZDEditTools.StartEditDateTime = DateTime.Now;
            if (geometry2.IsEmpty)
            {
                shapeCopy.Cut(ipolyline_0, out geometry, out geometry1);
                if ((geometry == null ? false : geometry1 != null))
                {
                    feature = ZDEditTools.WriteHistory(ifeature_0);
                    feature1 = ZDEditTools.ZDFeatureClass.CreateFeature();
                    features.Add(feature1);
                    feature1.Shape = geometry;
                    RowOperator.CopyFeatureAttributeToFeature(ifeature_0, feature1);
                    feature1.Store();
                    ZDEditTools.Oids.Add(feature1.OID);
                    ZDEditTools.WriteHistoryLine(ifeature_0, feature1, feature, 2, ZDEditTools.StartEditDateTime);
                    feature1 = ZDEditTools.ZDFeatureClass.CreateFeature();
                    features.Add(feature1);
                    feature1.Shape = geometry1;
                    RowOperator.CopyFeatureAttributeToFeature(ifeature_0, feature1);
                    feature1.Store();
                    ZDEditTools.Oids.Add(feature1.OID);
                    ZDEditTools.WriteHistoryLine(ifeature_0, feature1, feature, 2, ZDEditTools.StartEditDateTime);
                    ifeature_0.Delete();
                }
            }
            else if ((!(geometry2 is IMultipoint) ? false : (geometry2 as IPointCollection).PointCount > 1))
            {
                ISpatialFilter spatialFilterClass = new SpatialFilter()
                {
                    Geometry = ipolyline_0,
                    SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                };
                IFeatureCursor featureCursor = (ifeature_0.Class as IFeatureClass).Search(spatialFilterClass, false);
                for (IFeature i = featureCursor.NextFeature(); i != null; i = featureCursor.NextFeature())
                {
                    if (i.OID != ifeature_0.OID)
                    {
                        features1.Add(i);
                    }
                }
                ComReleaser.ReleaseCOMObject(featureCursor);
                int pointCount = (geometry2 as IPointCollection).PointCount;
                IPoint point = (geometry2 as IPointCollection).Point[0];
                IPoint point1 = (geometry2 as IPointCollection).Point[pointCount - 1];
                foreach (IFeature feature2 in features1)
                {
                    ZDEditTools.HitTest(feature2, point);
                    ZDEditTools.HitTest(feature2, point1);
                }
                IGeometryCollection geometryCollection = shapeCopy.Cut2(ipolyline_0);
                DateTime startEditDateTime = ZDEditTools.StartEditDateTime;
                feature = ZDEditTools.WriteHistory(ifeature_0);
                for (int j = 0; j < geometryCollection.GeometryCount; j++)
                {
                    IGeometry geometry3 = geometryCollection.Geometry[j];
                    feature1 = ZDEditTools.ZDFeatureClass.CreateFeature();
                    features.Add(feature1);
                    feature1.Shape = geometry3;
                    RowOperator.CopyFeatureAttributeToFeature(ifeature_0, feature1);
                    feature1.Store();
                    ZDEditTools.Oids.Add(feature1.OID);
                    ZDEditTools.WriteHistoryLine(ifeature_0, feature1, feature, 2, startEditDateTime);
                }
                ifeature_0.Delete();
            }
            return features;
        }

        public static void StartZDEdit()
        {
            ZDEditTools.StartEditDateTime = DateTime.Now;
            IMap editMap = Editor.EditMap;
            List<IFeatureLayer> featureLayers = new List<IFeatureLayer>();
            for (int i = 0; i < editMap.LayerCount; i++)
            {
                ILayer layer = editMap.Layer[i];
                if (layer is IGroupLayer)
                {
                    ZDEditTools.CheckGroupLayerEdit(featureLayers, layer as ICompositeLayer);
                }
                else if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    if (ZDEditTools.CheckLayerIsCanEditZD(featureLayer))
                    {
                        featureLayers.Add(featureLayer);
                    }
                }
            }
            if (featureLayers.Count == 0)
            {
                MessageBox.Show("当前没有可编辑的宗地图层!");
            }
            else if (featureLayers.Count != 1)
            {
                frmSelectEditZD _frmSelectEditZD = new frmSelectEditZD()
                {
                    FeatureLayers = featureLayers
                };
                if (_frmSelectEditZD.ShowDialog() == DialogResult.OK)
                {
                    ZDEditTools.ZDFeatureLayer = _frmSelectEditZD.SelectFeatureLayer;
                    ZDEditTools.ZDFeatureClass = ZDEditTools.ZDFeatureLayer.FeatureClass;
                    ZDEditTools.ZDHisFeatureClass = ZDRegister.GetHistoryFeatureClass(ZDEditTools.ZDFeatureClass);
                    ZDEditTools.FireStartZDEdit();
                }
            }
            else
            {
                ZDEditTools.ZDFeatureLayer = featureLayers[0];
                ZDEditTools.ZDFeatureClass = ZDEditTools.ZDFeatureLayer.FeatureClass;
                ZDEditTools.ZDHisFeatureClass = ZDRegister.GetHistoryFeatureClass(ZDEditTools.ZDFeatureClass);
                ZDEditTools.FireStartZDEdit();
            }
        }

        public static void StopZDEdit()
        {
            ZDEditTools.Oids.Clear();
            Editor.SaveEditing();
            ZDEditTools.ZDFeatureLayer = null;
            ZDEditTools.ZDFeatureClass = null;
            ZDEditTools.ZDHisFeatureClass = null;
            ZDEditTools.FireEndZDEdit();
        }

        public static IFeature Union(IFeatureSelection ifeatureSelection_0)
        {
            ICursor cursor = null;
            ifeatureSelection_0.SelectionSet.Search(null, false, out cursor);
            IFeature feature = ZDEditTools.Union(cursor);
            ComReleaser.ReleaseCOMObject(cursor);
            cursor = null;
            return feature;
        }

        public static IFeature Union(ICursor icursor_0)
        {
            List<IFeature> features = new List<IFeature>();
            for (IRow i = icursor_0.NextRow(); i != null; i = icursor_0.NextRow())
            {
                features.Add(i as IFeature);
            }
            return ZDEditTools.Union(features);
        }

        public static IFeature Union(List<IFeature> list_0)
        {
            ITopologicalOperator4 shapeCopy = null;
            IGeometry geometry = null;
            List<IFeature> features = new List<IFeature>();
            foreach (IFeature list0 in list_0)
            {
                features.Add(ZDEditTools.WriteHistory(list0));
                if (shapeCopy != null)
                {
                    geometry = shapeCopy.Union(list0.ShapeCopy);
                    shapeCopy = geometry as ITopologicalOperator4;
                }
                else
                {
                    shapeCopy = list0.ShapeCopy as ITopologicalOperator4;
                }
            }
            IFeature feature = ZDEditTools.ZDFeatureClass.CreateFeature();
            feature.Shape = geometry;
            RowOperator.CopyFeatureAttributeToFeature(list_0[0], feature);
            feature.Store();
            (ZDEditTools.ZDFeatureClass as IDataset).Name.Split(new char[] {'.'});
            ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
            ZDEditTools.StartEditDateTime = DateTime.Now;
            int num = 0;
            foreach (IFeature list01 in list_0)
            {
                int num1 = num;
                num = num1 + 1;
                ZDEditTools.WriteHistoryLine(list01, feature, features[num1], 1, ZDEditTools.StartEditDateTime);
                list01.Delete();
            }
            ZDEditTools.Oids.Add(feature.OID);
            return feature;
        }

        public static void UpdateFeatureGeometry(IFeature ifeature_0, IGeometry igeometry_0)
        {
            ZDEditTools.StartEditDateTime = DateTime.Now;
            IFeature feature = null;
            bool flag = ZDEditTools.Oids.IndexOf(ifeature_0.OID) == -1;
            bool flag1 = flag;
            if (flag)
            {
                feature = ZDEditTools.WriteHistory(ifeature_0);
            }
            try
            {
                ifeature_0.Shape = igeometry_0;
                ifeature_0.Store();
                if (flag1)
                {
                    IFeature feature1 = ZDEditTools.ZDFeatureClass.CreateFeature();
                    RowOperator.CopyFeatureToFeature(ifeature_0, feature1);
                    ZDEditTools.Oids.Add(feature1.OID);
                    ZDEditTools.WriteHistoryLine(ifeature_0, feature1, feature, 3, ZDEditTools.StartEditDateTime);
                    ifeature_0.Delete();
                }
            }
            catch
            {
                if (feature != null)
                {
                    feature.Delete();
                }
            }
        }

        public static bool UpdateZDAttribute(IFeature ifeature_0, string string_0, object object_0,
            out IFeature ifeature_1)
        {
            ZDEditTools.StartEditDateTime = DateTime.Now;
            Editor.StartEditOperation();
            IWorkspace workspace = AppConfigInfo.GetWorkspace();
            if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.EditWorkspace as IWorkspace))
            {
                (workspace as IWorkspaceEdit).StartEditOperation();
            }
            ifeature_1 = null;
            bool flag = ZDEditTools.Oids.IndexOf(ifeature_0.OID) == -1;
            IFeature feature = null;
            if (flag)
            {
                feature = ZDEditTools.WriteHistory(ifeature_0);
            }
            bool flag1 = true;
            try
            {
                ifeature_0.Value[ifeature_0.Fields.FindField(string_0)] = object_0;
                ifeature_0.Store();
                if (flag)
                {
                    IFeature feature1 = ZDEditTools.ZDFeatureClass.CreateFeature();
                    RowOperator.CopyFeatureToFeature(ifeature_0, feature1);
                    ZDEditTools.Oids.Add(feature1.OID);
                    ifeature_1 = feature1;
                    ZDEditTools.WriteHistoryLine(ifeature_0, feature1, feature, 5, ZDEditTools.StartEditDateTime);
                    ifeature_0.Delete();
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                if (feature != null)
                {
                    feature.Delete();
                }
                flag1 = false;
                MessageBox.Show("输入数据格式错误");
                // CErrorLog.writeErrorLog(null, exception, "");
            }
            if (!WorkspaceOperator.WorkspaceIsSame(workspace, Editor.EditWorkspace as IWorkspace))
            {
                (workspace as IWorkspaceEdit).StopEditOperation();
            }
            Editor.StopEditOperation();
            return flag1;
        }

        private static void WriteDelHistoryLine(IFeature ifeature_0, IFeature ifeature_1, int int_0, DateTime dateTime_0)
        {
            string[] strArrays = (ZDEditTools.ZDFeatureClass as IDataset).Name.Split(new char[] {'.'});
            ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
            IRow row = ZDEditTools.ZDChangeHisInfoTable.CreateRow();
            RowOperator.SetFieldValue(row, zDHistoryTable.ChageDateFieldName, dateTime_0);
            RowOperator.SetFieldValue(row, zDHistoryTable.ChangeTypeFieldName, int_0);
            RowOperator.SetFieldValue(row, zDHistoryTable.OrigineZDOIDName, ifeature_0.OID);
            RowOperator.SetFieldValue(row, zDHistoryTable.NewZDOIDName, -1);
            if (ifeature_1 == null)
            {
                RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, -1);
            }
            else
            {
                RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, ifeature_1.OID);
            }
            RowOperator.SetFieldValue(row, zDHistoryTable.ZDFeatureClassName, strArrays[(int) strArrays.Length - 1]);
            RowOperator.SetFieldValue(row, zDHistoryTable.ZDRegisterGuidName,
                ZDRegister.GetRegisterZDGuid(ZDEditTools.ZDFeatureClass));
            row.Store();
        }

        public static IFeature WriteHistory(IFeature ifeature_0)
        {
            IFeature shapeCopy = ZDEditTools.ZDHisFeatureClass.CreateFeature();
            RowOperator.SetFieldValue(shapeCopy, "OriginOID_", ifeature_0.OID);
            shapeCopy.Shape = ifeature_0.ShapeCopy;
            RowOperator.CopyRowToRow(ifeature_0, shapeCopy);
            return shapeCopy;
        }

        private static void WriteHistoryLine(IFeature ifeature_0, IFeature ifeature_1, int int_0, DateTime dateTime_0)
        {
            string[] strArrays = (ZDEditTools.ZDFeatureClass as IDataset).Name.Split(new char[] {'.'});
            ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
            IRow row = ZDEditTools.ZDChangeHisInfoTable.CreateRow();
            RowOperator.SetFieldValue(row, zDHistoryTable.ChageDateFieldName, dateTime_0);
            RowOperator.SetFieldValue(row, zDHistoryTable.ChangeTypeFieldName, int_0);
            RowOperator.SetFieldValue(row, zDHistoryTable.OrigineZDOIDName, -1);
            RowOperator.SetFieldValue(row, zDHistoryTable.NewZDOIDName, ifeature_0.OID);
            if (ifeature_1 == null)
            {
                RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, -1);
            }
            else
            {
                RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, ifeature_1.OID);
            }
            RowOperator.SetFieldValue(row, zDHistoryTable.ZDFeatureClassName, strArrays[(int) strArrays.Length - 1]);
            RowOperator.SetFieldValue(row, zDHistoryTable.ZDRegisterGuidName,
                ZDRegister.GetRegisterZDGuid(ZDEditTools.ZDFeatureClass));
            row.Store();
        }

        private static void WriteHistoryLine(IFeature ifeature_0, IFeature ifeature_1, IFeature ifeature_2, int int_0,
            DateTime dateTime_0)
        {
            string[] strArrays = (ZDEditTools.ZDFeatureClass as IDataset).Name.Split(new char[] {'.'});
            ZDHistoryTable zDHistoryTable = new ZDHistoryTable();
            IRow row = ZDEditTools.ZDChangeHisInfoTable.CreateRow();
            RowOperator.SetFieldValue(row, zDHistoryTable.ChageDateFieldName, dateTime_0);
            RowOperator.SetFieldValue(row, zDHistoryTable.ChangeTypeFieldName, int_0);
            RowOperator.SetFieldValue(row, zDHistoryTable.OrigineZDOIDName, ifeature_0.OID);
            RowOperator.SetFieldValue(row, zDHistoryTable.NewZDOIDName, ifeature_1.OID);
            if (ifeature_2 != null)
            {
                RowOperator.SetFieldValue(row, zDHistoryTable.HisZDOIDName, ifeature_2.OID);
            }
            RowOperator.SetFieldValue(row, zDHistoryTable.ZDFeatureClassName, strArrays[(int) strArrays.Length - 1]);
            RowOperator.SetFieldValue(row, zDHistoryTable.ZDRegisterGuidName,
                ZDRegister.GetRegisterZDGuid(ZDEditTools.ZDFeatureClass));
            row.Store();
        }

        private static void WriteHistoryLine(List<IFeature> list_0, IFeature ifeature_0, List<IFeature> list_1,
            int int_0, DateTime dateTime_0)
        {
            int num = 0;
            foreach (IFeature list0 in list_0)
            {
                int num1 = num;
                num = num1 + 1;
                ZDEditTools.WriteHistoryLine(list0, ifeature_0, list_1[num1], int_0, dateTime_0);
            }
        }

        private static void WriteHistoryLine(IFeature ifeature_0, List<IFeature> list_0, IFeature ifeature_1, int int_0,
            DateTime dateTime_0)
        {
            foreach (IFeature list0 in list_0)
            {
                ZDEditTools.WriteHistoryLine(ifeature_0, list0, ifeature_1, int_0, dateTime_0);
            }
        }

        public static event OnAfterCreateZDHandler OnAfterCreateZD;

        public static event OnEndZDEditHandler OnEndZDEdit;

        public static event OnStartZDEditHandler OnStartZDEdit;
    }
}