using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Plugins.Identifer.Helpers
{
    public class QueryHelper
    {
        public QueryHelper()
        {
        }

        public static void GetClosestSelectedFeature(IPoint inPoint, IMap pMap, out IFeature pFeature)
        {
            pFeature = null;
            IProximityOperator ipoint0 = (IProximityOperator)inPoint;
            IEnumFeature featureSelection = (IEnumFeature)pMap.FeatureSelection;
            featureSelection.Reset();
            IFeature i = featureSelection.Next();
            if (i != null)
            {
                double num = ipoint0.ReturnDistance(i.ShapeCopy);
                pFeature = i;
                for (i = featureSelection.Next(); i != null; i = featureSelection.Next())
                {
                    double num1 = ipoint0.ReturnDistance(i.Shape);
                    if (num1 < num)
                    {
                        num = num1;
                        pFeature = i;
                    }
                }
            }
        }

        public static void GetClosestSelectedFeature(IPoint inPoint, IArray inFeatures, out IFeature pFeature)
        {
            pFeature = null;
            if (inFeatures.Count != 0)
            {
                IProximityOperator ipoint0 = (IProximityOperator)inPoint;
                double num = -1;
                for (int i = 0; i < inFeatures.Count; i++)
                {
                    IFeature element = inFeatures.Element[i] as IFeature;
                    double num1 = ipoint0.ReturnDistance(element.Shape);
                    if (num < 0)
                    {
                        num = num1 + 1;
                    }
                    if (num1 < num)
                    {
                        num = num1;
                        pFeature = element;
                    }
                }
            }
        }

        public static void GetClosestSelectedFeature(IPoint inPoint, IFeatureCursor pCursor, out IFeature pFeature)
        {
            pFeature = null;
            IProximityOperator ipoint0 = (IProximityOperator)inPoint;
            IFeature feature = pCursor.NextFeature();
            if (feature != null)
            {
                if (pFeature == null)
                {
                    pFeature = feature;
                    feature = pCursor.NextFeature();
                }
                double num = ipoint0.ReturnDistance(pFeature.ShapeCopy);
                while (feature != null)
                {
                    double num1 = ipoint0.ReturnDistance(feature.Shape);
                    if (num1 < num)
                    {
                        num = num1;
                        pFeature = feature;
                    }
                    feature = pCursor.NextFeature();
                }
            }
        }

        public static IFeature GetHitLineFeature(IPoint inPoint, IMap pMap, double torelance)
        {
            IFeature feature = null;
            double mapUnits = CommonHelper.ConvertPixelsToMapUnits(pMap as IActiveView, torelance);
            IEnvelope envelope = inPoint.Envelope;
            envelope.Height = mapUnits;
            envelope.Width = mapUnits;
            envelope.CenterAt(inPoint);
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                Geometry = envelope,
                SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                IFeatureLayer layer = pMap.Layer[i] as IFeatureLayer;
                if (layer != null && layer.Visible && layer.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    spatialFilterClass.GeometryField = layer.FeatureClass.ShapeFieldName;
                    IFeatureCursor featureCursor = layer.Search(spatialFilterClass, false);
                    QueryHelper.GetClosestSelectedFeature(inPoint, featureCursor, out feature);
                    ComReleaser.ReleaseCOMObject(featureCursor);
                }
            }
            return feature;
        }

        public IQueryFilter Quilter(string whereClause)
        {
            return new QueryFilter()
            {
                WhereClause = whereClause
            };
        }

        public ICursor Search(ITable pTable, string whereClause)
        {
            return pTable.Search(this.Quilter(whereClause), false);
        }

        public IFeatureCursor Search(IMap pMap, string lyrName, string whereClause)
        {
            IFeatureCursor featureCursor;
            IFeatureLayer featureLayer = MapHelper.FindLayer(pMap as IBasicMap, lyrName) as IFeatureLayer;
            if (featureLayer == null)
            {
                featureCursor = null;
            }
            else
            {
                featureCursor = this.Search(featureLayer.FeatureClass as ITable, whereClause) as IFeatureCursor;
            }
            return featureCursor;
        }

        public IFeatureCursor Search(IMap pMap, string lyrName, string whereClause, IGeometry pGeometry, esriSpatialRelEnum pSpatialRelEnum)
        {
            IFeatureCursor featureCursor;
            IFeatureLayer featureLayer = MapHelper.FindLayer(pMap as IBasicMap, lyrName) as IFeatureLayer;
            if (featureLayer == null)
            {
                featureCursor = null;
            }
            else
            {
                featureCursor = this.SearchShape(pGeometry, featureLayer.FeatureClass, whereClause, pSpatialRelEnum);
            }
            return featureCursor;
        }

        public IFeatureCursor SearchShape(IGeometry pGeometry, IFeatureClass pFeatureClass, string string_0)
        {
            return this.SearchShape(pGeometry, pFeatureClass, string_0, esriSpatialRelEnum.esriSpatialRelIntersects);
        }

        public IFeatureCursor SearchShape(IGeometry pGeometry, IFeatureClass pFeatureClass, string whereClause, esriSpatialRelEnum pSpatialRelEnum)
        {
            ISpatialFilter spatialFilterClass = new SpatialFilter()
            {
                GeometryField = pFeatureClass.ShapeFieldName,
                Geometry = pGeometry
            };
            if (whereClause.Length > 0)
            {
                spatialFilterClass.WhereClause = whereClause;
            }
            spatialFilterClass.SpatialRel = pSpatialRelEnum;
            return pFeatureClass.Search(spatialFilterClass, false);
        }

        public IFeatureCursor Select(object object0, IGeometry pGeometry, double torelance)
        {
            IFeatureCursor featureCursor;
            if (object0 is IFeatureLayer)
            {
                if (pGeometry is IPoint)
                {
                    featureCursor = this.SelectByPoint((IFeatureLayer)object0, (IPoint)pGeometry, torelance);
                }
                else if (!(pGeometry is IPolyline))
                {
                    if (!(pGeometry is IPolygon))
                    {
                        featureCursor = null;
                        return featureCursor;
                    }
                    featureCursor = null;
                }
                else
                {
                    featureCursor = this.SelectByLine((IFeatureLayer)object0, (IPolyline)pGeometry, torelance);
                }
            }
            else if (!(object0 is IFeatureClass))
            {
                featureCursor = null;
                return featureCursor;
            }
            else if (pGeometry is IPoint)
            {
                featureCursor = this.SelectByPoint((IFeatureClass)object0, (IPoint)pGeometry, torelance);
            }
            else if (!(pGeometry is IPolyline))
            {
                if (!(pGeometry is IPolygon))
                {
                    featureCursor = null;
                    return featureCursor;
                }
                featureCursor = null;
            }
            else
            {
                featureCursor = this.SelectByLine((IFeatureClass)object0, (IPolyline)pGeometry, torelance);
            }
            return featureCursor;
        }

        public IEnumFeature Select(IMap pMap, IGeometry pGeometry, double torelance)
        {
            IEnumFeature enumFeature;
            if (pGeometry is IPoint)
            {
                enumFeature = this.SelectByPoint(pMap, (IPoint)pGeometry, torelance);
            }
            else if ((pGeometry is IPolyline))
            {
                enumFeature = this.SelectByLine(pMap, (IPolyline)pGeometry, torelance);
                
            }
            else
            {
                return null;
            }
            return enumFeature;
        }

        public IFeatureCursor SelectByLine(IFeatureClass pFeatureClass, IPolyline pLine, double pDist, string whereClause)
        {
            IGeometry ipolyline0;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            if (pDist <= 0)
            {
                ipolyline0 = pLine;
                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipolyline0 = ((ITopologicalOperator)pLine).Buffer(pDist);
                if (pFeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            spatialFilterClass.GeometryField = "shape";
            spatialFilterClass.Geometry = ipolyline0;
            spatialFilterClass.WhereClause = whereClause;
            return pFeatureClass.Search(spatialFilterClass, false);
        }

        public IFeatureCursor SelectByLine(IFeatureClass pFeatureClass, IPolyline pLine, double pDist)
        {
            IGeometry ipolyline0;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            if (pDist <= 0)
            {
                ipolyline0 = pLine;
                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipolyline0 = ((ITopologicalOperator)pLine).Buffer(pDist);
                if (pFeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            spatialFilterClass.GeometryField = "shape";
            spatialFilterClass.Geometry = ipolyline0;
            return pFeatureClass.Search(spatialFilterClass, false);
        }

        public IFeatureCursor SelectByLine(IFeatureLayer pFLayer, IPolyline pLine, double pDist, string whereClause)
        {
            IGeometry ipolyline0;
            ICursor cursor;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            if (pDist <= 0)
            {
                ipolyline0 = pLine;
                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipolyline0 = ((ITopologicalOperator)pLine).Buffer(pDist);
                if (pFLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            spatialFilterClass.GeometryField = "shape";
            spatialFilterClass.Geometry = ipolyline0;
            spatialFilterClass.WhereClause = whereClause;
            IFeatureSelection ifeatureLayer0 = (IFeatureSelection)pFLayer;
            ifeatureLayer0.SelectFeatures(spatialFilterClass, esriSelectionResultEnum.esriSelectionResultNew, false);
            ifeatureLayer0.SelectionSet.Search(null, false, out cursor);
            return (IFeatureCursor)cursor;
        }

        public IFeatureCursor SelectByLine(IFeatureLayer pFLayer, IPolyline pLine, double pDist)
        {
            IGeometry ipolyline0;
            ICursor cursor;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            if (pDist <= 0)
            {
                ipolyline0 = pLine;
                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipolyline0 = ((ITopologicalOperator)pLine).Buffer(pDist);
                if (pFLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            spatialFilterClass.GeometryField = "shape";
            spatialFilterClass.Geometry = ipolyline0;
            IFeatureSelection ifeatureLayer0 = (IFeatureSelection)pFLayer;
            ifeatureLayer0.SelectFeatures(spatialFilterClass, esriSelectionResultEnum.esriSelectionResultNew, false);
            ifeatureLayer0.SelectionSet.Search(null, false, out cursor);
            return (IFeatureCursor)cursor;
        }

        public IEnumFeature SelectByLine(IMap pMap, IPolyline pLine, double pDist)
        {
            IGeometry ipolyline0;
            ISelectionEnvironment selectionEnvironmentClass = new SelectionEnvironmentClass()
            {
                AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects,
                LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            if (pDist <= 0)
            {
                ipolyline0 = pLine;
                selectionEnvironmentClass.PointSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipolyline0 = ((ITopologicalOperator)pLine).Buffer(pDist);
                selectionEnvironmentClass.PointSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
            }
            selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            try
            {
                pMap.SelectByShape(ipolyline0, selectionEnvironmentClass, false);
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode != -2147467259)
                {
                    
                }
                else
                {
                    MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
                }
            }
            catch (Exception exception)
            {
               
            }
            return (IEnumFeature)pMap.FeatureSelection;
        }

        public IFeatureCursor SelectByPoint(IFeatureLayer pFLayer, IPoint inPoint, double pDist)
        {
            IGeometry ipoint0;
            ICursor cursor;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            if (pDist <= 0)
            {
                ipoint0 = inPoint;
                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipoint0 = ((ITopologicalOperator)inPoint).Buffer(pDist);
                if (pFLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            spatialFilterClass.GeometryField = "shape";
            spatialFilterClass.Geometry = ipoint0;
            IFeatureSelection ifeatureLayer0 = (IFeatureSelection)pFLayer;
            ifeatureLayer0.SelectFeatures(spatialFilterClass, esriSelectionResultEnum.esriSelectionResultNew, false);
            ifeatureLayer0.SelectionSet.Search(null, false, out cursor);
            return (IFeatureCursor)cursor;
        }

        public IFeatureCursor SelectByPoint(IFeatureClass pFeatureClass, IPoint inPoint, double pDist)
        {
            IGeometry ipoint0;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            if (pDist <= 0)
            {
                ipoint0 = inPoint;
                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipoint0 = ((ITopologicalOperator)inPoint).Buffer(pDist);
                if (pFeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            spatialFilterClass.GeometryField = "shape";
            spatialFilterClass.Geometry = ipoint0;
            return pFeatureClass.Search(spatialFilterClass, false);
        }

        public IFeatureCursor SelectByPoint(IFeatureLayer pFLayer, IPoint inPoint, double pDist, string whereClause)
        {
            IGeometry ipoint0;
            ICursor cursor;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            if (pDist <= 0)
            {
                ipoint0 = inPoint;
                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipoint0 = ((ITopologicalOperator)inPoint).Buffer(pDist);
                if (pFLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            spatialFilterClass.GeometryField = "shape";
            spatialFilterClass.Geometry = ipoint0;
            spatialFilterClass.WhereClause = whereClause;
            IFeatureSelection ifeatureLayer0 = (IFeatureSelection)pFLayer;
            ifeatureLayer0.SelectFeatures(spatialFilterClass, esriSelectionResultEnum.esriSelectionResultNew, false);
            ifeatureLayer0.SelectionSet.Search(null, false, out cursor);
            return (IFeatureCursor)cursor;
        }

        public IFeatureCursor SelectByPoint(IFeatureClass pFeatureClass, IPoint inPoint, double pDist, string whereClause)
        {
            IGeometry ipoint0;
            ISpatialFilter spatialFilterClass = new SpatialFilter();
            if (pDist <= 0)
            {
                ipoint0 = inPoint;
                spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipoint0 = ((ITopologicalOperator)inPoint).Buffer(pDist);
                if (pFeatureClass.ShapeType != esriGeometryType.esriGeometryPoint)
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                }
                else
                {
                    spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                }
            }
            spatialFilterClass.GeometryField = "shape";
            spatialFilterClass.Geometry = ipoint0;
            spatialFilterClass.WhereClause = whereClause;
            return pFeatureClass.Search(spatialFilterClass, false);
        }

        public IEnumFeature SelectByPoint(IMap imap_0, IPoint inPoint, double pDist)
        {
            IGeometry ipoint0;
            ISelectionEnvironment selectionEnvironmentClass = new SelectionEnvironmentClass()
            {
                AreaSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects,
                LinearSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects
            };
            if (pDist <= 0)
            {
                ipoint0 = inPoint;
                selectionEnvironmentClass.PointSelectionMethod = esriSpatialRelEnum.esriSpatialRelIntersects;
            }
            else
            {
                ipoint0 = ((ITopologicalOperator)inPoint).Buffer(pDist);
                selectionEnvironmentClass.PointSelectionMethod = esriSpatialRelEnum.esriSpatialRelContains;
            }
            selectionEnvironmentClass.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            try
            {
                imap_0.SelectByShape(ipoint0, selectionEnvironmentClass, false);
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode != -2147467259)
                {
                   // CErrorLog.writeErrorLog(this, cOMException, "");
                }
                else
                {
                    MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
                }
            }
            catch (Exception exception)
            {
                //CErrorLog.writeErrorLog(this, exception, "");
            }
            return (IEnumFeature)imap_0.FeatureSelection;
        }

        public  static IEnumFeature SelectByPolygon(IMap pMap, IPolygon pPolygon, esriSpatialRelEnum pSpatialRelEnum)
        {
            ISelectionEnvironment selectionEnvironmentClass = new SelectionEnvironmentClass()
            {
                AreaSelectionMethod = pSpatialRelEnum,
                LinearSelectionMethod = pSpatialRelEnum,
                PointSelectionMethod = pSpatialRelEnum,
                CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew
            };
            try
            {
                pMap.SelectByShape(pPolygon, selectionEnvironmentClass, false);
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                if (cOMException.ErrorCode != -2147467259)
                {
                    //CErrorLog.writeErrorLog(null, cOMException, "");
                }
                else
                {
                    MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
                }
            }
            catch (Exception exception)
            {
                //CErrorLog.writeErrorLog(null, exception, "");
            }
            return (IEnumFeature)pMap.FeatureSelection;
        }
    }
}
