using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using stdole;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;
using Yutai.Pipeline.Editor.Classes;
using Yutai.Pipeline.Editor.Views;
using Yutai.Shared;

namespace Yutai.Pipeline.Editor.Helper
{
    class CommonHelper
    {
        public static ILayer GetLayerByName(IMap map, string layerName, bool isFeatureLayer = false)
        {
            try
            {
                IEnumLayer enumLayer = map.Layers[null, false];
                enumLayer.Reset();
                ILayer layer;
                while ((layer = enumLayer.Next()) != null)
                {
                    if (layer is IGroupLayer)
                        return GetLayerByName(layer as ICompositeLayer, layerName, isFeatureLayer);
                    if (isFeatureLayer && (layer is IFeatureLayer) == false)
                        continue;
                    if (layer.Name == layerName)
                    {
                        return layer;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public static ILayer GetLayerByName(ICompositeLayer compositeLayer, string layerName,
            bool isFeatureLayer = false)
        {
            try
            {
                for (int i = 0; i < compositeLayer.Count; i++)
                {
                    ILayer layer = compositeLayer.Layer[i];
                    if (layer is IGroupLayer)
                        return GetLayerByName(layer as ICompositeLayer, layerName, isFeatureLayer);
                    if (isFeatureLayer && (layer is IFeatureLayer) == false)
                        continue;
                    if (layer.Name == layerName)
                    {
                        return layer;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public static IFeatureLayer GetLayerByFeatureClassAliasName(IMap map, string featureClassName)
        {
            try
            {
                IEnumLayer enumLayer = map.Layers[null, false];
                enumLayer.Reset();
                ILayer layer;
                while ((layer = enumLayer.Next()) != null)
                {
                    if (layer is IGroupLayer)
                        return GetLayerByFeatureClassAliasName(layer as ICompositeLayer, featureClassName);
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as IFeatureLayer;
                        if (featureLayer.FeatureClass.AliasName == featureClassName)
                            return featureLayer;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public static IFeatureLayer GetLayerByFeatureClassAliasName(ICompositeLayer compositeLayer,
            string featureClassName)
        {
            try
            {
                for (int i = 0; i < compositeLayer.Count; i++)
                {
                    ILayer layer = compositeLayer.Layer[i];
                    if (layer is IGroupLayer)
                        return GetLayerByFeatureClassAliasName(layer as ICompositeLayer, featureClassName);
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as IFeatureLayer;
                        if (featureLayer.FeatureClass.AliasName == featureClassName)
                            return featureLayer;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public static IFeatureLayer GetLayerByFeatureClassName(IMap map, string featureClassName)
        {
            IEnumLayer enumLayer = map.Layers[null, true];

            enumLayer.Reset();
            ILayer layer;
            while ((layer = enumLayer.Next()) != null)
            {
                if (layer is IGroupLayer)
                    return GetLayerByFeatureClassName(layer as ICompositeLayer, featureClassName);
                if (layer is IFeatureLayer)
                {
                    IFeatureLayer featureLayer = layer as IFeatureLayer;
                    IFeatureClass featureClass = featureLayer.FeatureClass;
                    IDataset dataset = featureClass as IDataset;
                    if (dataset != null)
                    {
                        string[] strArray = dataset.Name.Split(new char[] {'.'});
                        string str = strArray[strArray.Length - 1];
                        if (str == featureClassName)
                        {
                            return layer as IFeatureLayer;
                        }
                    }
                }
            }

            return null;
        }

        public static IFeatureLayer GetLayerByFeatureClassName(ICompositeLayer compositeLayer, string featureClassName)
        {
            try
            {
                for (int i = 0; i < compositeLayer.Count; i++)
                {
                    ILayer layer = compositeLayer.Layer[i];
                    if (layer is IGroupLayer)
                        return GetLayerByFeatureClassName(layer as ICompositeLayer, featureClassName);
                    if (layer is IFeatureLayer)
                    {
                        IFeatureLayer featureLayer = layer as IFeatureLayer;
                        IFeatureClass featureClass = featureLayer.FeatureClass;
                        IDataset dataset = featureClass as IDataset;
                        if (dataset != null)
                        {
                            string[] strArray = dataset.Name.Split(new char[] {'.'});
                            string str = strArray[strArray.Length - 1];
                            if (str == featureClassName)
                            {
                                return layer as IFeatureLayer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public static int IsFromPoint(IPoint point, IPolyline polyline)
        {
            double dis = GetDistance(point, polyline.FromPoint);
            if (dis < 0.001)
                return 1;
            dis = GetDistance(point, polyline.ToPoint);
            if (dis < 0.001)
                return -1;
            return 0;
        }

        public static double GetDistance(IPoint point1, IPoint point2)
        {
            return Math.Sqrt((point1.X - point2.X)*(point1.X - point2.X) + (point1.Y - point2.Y)*(point1.Y - point2.Y));
        }

        public static FileInfo[] GetLabelDefinitions()
        {
            string path = $"{Application.StartupPath}\\Plugins\\configs";
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory.GetFiles("*.lab");
        }

        public static bool Exist(IWorkspace workspace, string featureClassName)
        {
            IWorkspace2 pWorkspace2 = workspace as IWorkspace2;
            return pWorkspace2 != null && pWorkspace2.NameExists[esriDatasetType.esriDTFeatureClass, featureClassName];
        }

        public static bool DeleteFeatureDataset(IWorkspace ws, string name)
        {
            if (ws == null || string.IsNullOrEmpty(name))
            {
                return false;
            }
            try
            {
                var pFeaWorkspace = ws as IFeatureWorkspace;
                var pEnumDatasetName = ws.DatasetNames[esriDatasetType.esriDTAny];
                pEnumDatasetName.Reset();
                var pDatasetName = pEnumDatasetName.Next();
                while (pDatasetName != null)
                {
                    if (pDatasetName.Type == esriDatasetType.esriDTFeatureDataset)
                    {
                        //如果是要素集，则对要素集内的要素类进行查找
                        IFeatureDatasetName featureDatasetName = pDatasetName as IFeatureDatasetName;
                        if (featureDatasetName != null)
                        {
                            IEnumDatasetName pEnumFcName = featureDatasetName.FeatureClassNames;
                            IDatasetName pFcName;
                            while ((pFcName = pEnumFcName.Next()) != null)
                            {
                                if (pFcName.Name.IndexOf(name, StringComparison.Ordinal) >= 0)
                                {
                                    DeleteByName(pFeaWorkspace, pFcName);
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (pDatasetName.Name.IndexOf(name, StringComparison.Ordinal) >= 0)
                        {
                            DeleteByName(pFeaWorkspace, pDatasetName);
                            return true;
                        }
                    }
                    pDatasetName = pEnumDatasetName.Next();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //删除名称对象
        public static void DeleteByName(IFeatureWorkspace pFeaWorkspace, IDatasetName pDatasetName)
        {
            IFeatureWorkspaceManage pWorkspaceManager = pFeaWorkspace as IFeatureWorkspaceManage;
            pWorkspaceManager?.DeleteByName(pDatasetName);
        }

        public static void SetReferenceScale(IAnnotationLayer annotationLayer, double scale)
        {
            IFeatureLayer featureLayer = annotationLayer as IFeatureLayer;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IAnnoClass annoClass = featureClass.Extension as IAnnoClass;
            IAnnoClassAdmin3 annoClassAdmin3 = annoClass as IAnnoClassAdmin3;
            annoClassAdmin3.ReferenceScale = scale;
        }

        #region 管线注记

        public static string GetIntersectInformationFlagLineOnlyOne(IGeometry pGeometry, ICheQiConfig cheQiConfig,
            IFeature pFeature)
        {
            string pFlagInfo = "";
            pFlagInfo = "类别:" + cheQiConfig.FlagLayer.FeatureClass.AliasName + "  " +
                        GetFlagInformationInFeature(pFeature, cheQiConfig.Expression);
            return pFlagInfo;
        }

        /// <summary>
        /// Gets the flag information in feature.
        /// </summary>
        /// <param name="polylinefeature">The polylinefeature.</param>
        /// <param name="flagfieldlist">The flagfieldlist.</param>
        /// <returns></returns>
        public static string GetFlagInformationInFeature(IFeature polylinefeature, string expression)
        {
            try
            {
                List<IField> fields = GetFields(expression, polylinefeature.Fields);
                for (int i = 0; i < fields.Count; i++)
                {
                    string fieldname = fields[i].Name;
                    string value = polylinefeature.Value[polylinefeature.Fields.FindField(fieldname)].ToString();
                    expression = expression.Replace($"[{fieldname}]", value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return expression;
        }


        public static List<IField> GetFields(string expression, IFields fields)
        {
            List<IField> list = new List<IField>();

            expression = expression.Replace("[#####]", "");
            while (expression.Contains("[") && expression.Contains("]"))
            {
                int sIdx = expression.IndexOf("[", StringComparison.Ordinal);
                int eIdx = expression.IndexOf("]", StringComparison.Ordinal);
                string fieldName = expression.Substring(sIdx + 1, eIdx - sIdx - 1);

                int idx = fields.FindField(fieldName);
                if (idx >= 0)
                    list.Add(fields.Field[idx]);

                expression = expression.Remove(0, eIdx + 1);
            }
            return list;
        }

        /// <summary>
        /// Gets the depth.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        /// <param name="point">The point.</param>
        /// <param name="fromdepth">The fromdepth.</param>
        /// <param name="todepth">The todepth.</param>
        /// <returns></returns>
        public static double GetDepth(IPolyline polyline, IPoint point, double fromdepth, double todepth)
        {
            if (fromdepth == todepth) return fromdepth;
            double fromX = polyline.FromPoint.X;
            double fromY = polyline.FromPoint.Y;
            double toX = polyline.ToPoint.X;
            double toY = polyline.ToPoint.Y;
            double depth = -999;
            ;
            double ratiolenth = 0;
            double length = Math.Sqrt((fromX - toX)*(fromX - toX) + (fromY - toY)*(fromY - toY));
            if (fromdepth > todepth)
            {
                ratiolenth = Math.Sqrt((point.X - toX)*(point.X - toX) + (point.Y - toY)*(point.Y - toY));
                depth = ratiolenth*(fromdepth - todepth)/length + todepth;

            }
            if (fromdepth < todepth)
            {
                ratiolenth = Math.Sqrt((point.X - fromX)*(point.X - fromX) + (point.Y - fromY)*(point.Y - fromY));
                depth = ratiolenth*(todepth - fromdepth)/length + fromdepth;

            }
            return Math.Round(depth, 2);
        }

        /// <summary>
        /// 返回数组中最大的那个所在的位置，从0开始计算
        /// </summary>
        /// <param name="strlist"></param>
        /// <returns></returns>
        public static int MaxLengthInList(List<string> strlist)
        {
            int count = 0;
            for (int i = 0; i < strlist.Count; i++)
            {
                string item = strlist[i];
                int length = item.Length;
                if (length > count)
                {
                    count = length;
                }
            }
            return count;
        }

        /// <summary>
        /// Deletes the anno features in feature layer.
        /// </summary>
        /// <param name="featureLayer">The feature layer.</param>
        public static void DeleteAnnoFeaturesInFeatureLayer(IFeatureLayer featureLayer)
        {
            if (featureLayer == null) return;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IDataset dataset = featureClass as IDataset;
            IWorkspace pWorkspace = dataset.Workspace;
            IWorkspaceEdit pWorkspaceEdit = pWorkspace as IWorkspaceEdit;
            pWorkspaceEdit.StartEditing(true);
            pWorkspaceEdit.StartEditOperation();

            IWorkspaceProperties2 workspaceProperties2 = (IWorkspaceProperties2) pWorkspace;
            //判断workspace是否可以执行SQL语句
            IWorkspaceProperty canExecuteSqlProperty =
                workspaceProperties2.get_Property(esriWorkspacePropertyGroupType.esriWorkspacePropertyGroup,
                    (int) esriWorkspacePropertyType.esriWorkspacePropCanExecuteSQL);
            if (canExecuteSqlProperty.IsSupported)
            {
                //ExecuteSQL删除feature
                pWorkspace.ExecuteSQL("delete  from " + featureClass.AliasName + " where objectid >=0");
            }
            pWorkspaceEdit.StopEditOperation();
            pWorkspaceEdit.StopEditing(true);
        }

        public static void ClearFeaturesInFeatureLayer(IFeatureLayer featureLayer)
        {
            if (featureLayer == null) return;
            IFeatureClass featureClass = featureLayer.FeatureClass;
            IDataset dataset = featureClass as IDataset;
            IWorkspace pWorkspace = dataset.Workspace;
            IWorkspaceProperties2 workspaceProperties2 = pWorkspace as IWorkspaceProperties2;
            //判断workspace是否可以执行SQL语句
            IWorkspaceProperty canExecuteSqlProperty =
                workspaceProperties2.Property[
                    esriWorkspacePropertyGroupType.esriWorkspacePropertyGroup,
                    (int) esriWorkspacePropertyType.esriWorkspacePropCanExecuteSQL];
            if (canExecuteSqlProperty.IsSupported)
            {
                //ExecuteSQL删除feature
                pWorkspace.ExecuteSQL("delete  from " + featureClass.AliasName + " where objectid >=0");
            }
        }

        #endregion

        public static List<IElement> CreateAnnoElementList(IMultiCheQiConfig multiCheQiConfig,
            List<MultiCheQiModel> modelList, IPoint point)
        {
            List<IElement> list = new List<IElement>();

            IElement element = CreateHeaderElements(multiCheQiConfig, point);
            list.Add(element);
            list.AddRange(CreateContentElements(multiCheQiConfig, modelList, element.Geometry as IPoint,
                element.Geometry.Envelope.Width, element.Geometry.Envelope.Height));

            return list;
        }

        public static List<IElement> CreateContentElements(IMultiCheQiConfig multiCheQiConfig,
            List<MultiCheQiModel> models, IPoint point, double xLength, double yLength)
        {
            List<IElement> list = new List<IElement>();
            IPoint originPoint = new PointClass
            {
                X = point.X,
                Y = point.Y
            };
            for (int i = 0; i < models.Count; i++)
            {
                MultiCheQiModel multiCheQiModel = models[i];
                IPoint tempPoint = new PointClass();
                tempPoint.X = originPoint.X;
                tempPoint.Y = originPoint.Y - yLength*(i + 1);
                string strContent = null;
                for (int j = 0; j < multiCheQiModel.FieldMappingList.Count; j++)
                {
                    CheQiFieldMapping mapping = multiCheQiModel.FieldMappingList[j];
                    strContent += mapping.FieldValue.PadRight(mapping.FieldSetting.Length);
                }
                IElement element = CreateTextElement(tempPoint, multiCheQiModel.Color, strContent,
                    multiCheQiConfig.ContentFontConfig);
                list.Add(element);
            }

            return list;
        }

        public static IElement CreateHeaderElements(IMultiCheQiConfig multiCheQiConfig, IPoint point)
        {
            IPoint originPoint = new PointClass
            {
                X = point.X,
                Y = point.Y
            };
            string strHeader = null;
            for (int i = 0; i < multiCheQiConfig.FieldSettingList.Count; i++)
            {
                IFieldSetting fieldSetting = multiCheQiConfig.FieldSettingList[i];
                strHeader += fieldSetting.FieldName.PadRight(fieldSetting.Length);
            }
            IElement element = CreateTextElement(originPoint, ConvertToRgbColor(multiCheQiConfig.HeaderFontConfig.Color),
                strHeader, multiCheQiConfig.HeaderFontConfig);

            return element;
        }

        public static IElement CreateTextElement(IPoint point, IColor color, string text, IFontConfig fontConfig)
        {

            stdole.IFontDisp fontDisp = new StdFontClass() as IFontDisp;
            fontDisp.Name = fontConfig.Font.Name;
            fontDisp.Size = (decimal) fontConfig.Font.Size;
            fontDisp.Italic = fontConfig.Font.Italic;
            fontDisp.Underline = fontConfig.Font.Underline;
            fontDisp.Bold = fontConfig.Font.Bold;
            fontDisp.Strikethrough = fontConfig.Font.Strikeout;

            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Font = fontDisp;
            textSymbol.Color = color;
            textSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
            textSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;

            ITextElement textElement = new TextElementClass();
            textElement.Symbol = textSymbol;
            textElement.ScaleText = true;
            textElement.Text = text;

            IElement element = textElement as IElement;
            element.Geometry = point;
            return element;
        }

        public static IRgbColor ConvertToRgbColor(Color color)
        {
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = color.R;
            pColor.Green = color.G;
            pColor.Blue = color.B;
            return pColor;
        }

        public static List<IFeature> GetSelectedFeatures(IMap map)
        {
            List<IFeature> features = new List<IFeature>();
            IEnumFeature enumFeature = map.FeatureSelection as IEnumFeature;
            IFeature feature;
            while ((feature = enumFeature.Next()) != null)
            {
                features.Add(feature);
            }
            return features;
        }

        public static List<IFeature> GetSelectedFeatures(IFeatureLayer featureLayer)
        {
            List<IFeature> features = new List<IFeature>();
            IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
            if (featureSelection?.SelectionSet == null || featureSelection.SelectionSet.Count <= 0)
                return features;
            ICursor cursor;
            featureSelection.SelectionSet.Search(null, false, out cursor);
            if (cursor == null)
                return features;
            IFeature feature;
            while ((feature = cursor.NextRow() as IFeature) != null)
            {
                features.Add(feature);
            }
            Marshal.ReleaseComObject(cursor);
            return features;
        }

        public static List<string> ToAnnotations(List<IFeature> features)
        {
            List<string> list = new List<string>();
            foreach (IFeature feature in features)
            {
                IAnnotationFeature pAnnotateFeature = feature as IAnnotationFeature;
                if (pAnnotateFeature == null)
                    continue;
                IElement pElement = pAnnotateFeature.Annotation as IElement;
                ITextElement pTextElement = pElement as ITextElement;
                if (pTextElement == null)
                    continue;
                list.Add(pTextElement.Text);
            }
            return list;
        }

        public static List<IPolygon> GetMovedPoints(List<IFeature> features, IPoint point,
            enumAnnotationDirection direction)
        {
            List<IPolygon> list = new List<IPolygon>();
            if (features.Count <= 0)
                return list;
            IPoint originPoint = new PointClass
            {
                X = point.X,
                Y = point.Y - features[0].Extent.Height/2
            };
            for (int i = 0; i < features.Count; i++)
            {
                IFeature pFeature = features[i];
                IPolygon tempPolygon = new PolygonClass();
                IPointCollection pointCollection = tempPolygon as IPointCollection;
                IPoint point1 = new PointClass();
                IPoint point2 = new PointClass();
                IPoint point3 = new PointClass();
                IPoint point4 = new PointClass();
                switch (direction)
                {
                    case enumAnnotationDirection.LeftUp:
                    {
                        point1 = new PointClass
                        {
                            X = originPoint.X - pFeature.Extent.Width,
                            Y = originPoint.Y + pFeature.Extent.Height*(features.Count - 1 - i)
                        };
                        point2 = new PointClass
                        {
                            X = originPoint.X - pFeature.Extent.Width,
                            Y = originPoint.Y + pFeature.Extent.Height*(features.Count - i)
                        };
                        point3 = new PointClass
                        {
                            X = originPoint.X,
                            Y = originPoint.Y + pFeature.Extent.Height*(features.Count - i)
                        };
                        point4 = new PointClass
                        {
                            X = originPoint.X,
                            Y = originPoint.Y + pFeature.Extent.Height*(features.Count - 1 - i)
                        };
                    }
                        break;
                    case enumAnnotationDirection.RightUp:
                    {
                        point1 = new PointClass
                        {
                            X = originPoint.X,
                            Y = originPoint.Y + pFeature.Extent.Height*(features.Count - 1 - i)
                        };
                        point2 = new PointClass
                        {
                            X = originPoint.X,
                            Y = originPoint.Y + pFeature.Extent.Height*(features.Count - i)
                        };
                        point3 = new PointClass
                        {
                            X = originPoint.X + pFeature.Extent.Width,
                            Y = originPoint.Y + pFeature.Extent.Height*(features.Count - i)
                        };
                        point4 = new PointClass
                        {
                            X = originPoint.X + pFeature.Extent.Width,
                            Y = originPoint.Y + pFeature.Extent.Height*(features.Count - 1 - i)
                        };
                    }
                        break;
                    case enumAnnotationDirection.RightDown:
                    {
                        point1 = new PointClass
                        {
                            X = originPoint.X,
                            Y = originPoint.Y - pFeature.Extent.Height*(i + 1)
                        };
                        point2 = new PointClass
                        {
                            X = originPoint.X,
                            Y = originPoint.Y - pFeature.Extent.Height*(i)
                        };
                        point3 = new PointClass
                        {
                            X = originPoint.X + pFeature.Extent.Width,
                            Y = originPoint.Y - pFeature.Extent.Height*(i)
                        };
                        point4 = new PointClass
                        {
                            X = originPoint.X + pFeature.Extent.Width,
                            Y = originPoint.Y - pFeature.Extent.Height*(i + 1)
                        };
                    }
                        break;
                    case enumAnnotationDirection.LeftDown:
                    {
                        point1 = new PointClass
                        {
                            X = originPoint.X - pFeature.Extent.Width,
                            Y = originPoint.Y - pFeature.Extent.Height*(i + 1)
                        };
                        point2 = new PointClass
                        {
                            X = originPoint.X - pFeature.Extent.Width,
                            Y = originPoint.Y - pFeature.Extent.Height*(i)
                        };
                        point3 = new PointClass
                        {
                            X = originPoint.X,
                            Y = originPoint.Y - pFeature.Extent.Height*(i)
                        };
                        point4 = new PointClass
                        {
                            X = originPoint.X,
                            Y = originPoint.Y - pFeature.Extent.Height*(i + 1)
                        };
                    }
                        break;
                }
                pointCollection.AddPoint(point1);
                pointCollection.AddPoint(point2);
                pointCollection.AddPoint(point3);
                pointCollection.AddPoint(point4);
                pointCollection.AddPoint(point1);
                list.Add(tempPolygon);
            }

            return list;
        }

        public static void CreatePointFeatures(IFeatureLayer featureLayer, IPolyline polyline, bool hasFromPoint, bool hasTurningPoint, bool hasToPoint)
        {
            try
            {
                bool hasZ = FeatureClassUtil.CheckHasZ(featureLayer.FeatureClass);
                bool hasM = FeatureClassUtil.CheckHasM(featureLayer.FeatureClass);
                IPointCollection pointCollection = polyline as IPointCollection;
                if (pointCollection == null)
                    return;
                for (int i = 0; i < pointCollection.PointCount; i++)
                {
                    if (hasFromPoint == false && i == 0)
                        continue;
                    if (hasTurningPoint == false && i > 0 && i < pointCollection.PointCount-1)
                        continue;
                    if (hasToPoint == false && i == pointCollection.PointCount-1)
                        continue;
                    IPoint point = pointCollection.Point[i];
                    IFeature feature = featureLayer.FeatureClass.CreateFeature();
                    feature.Shape = GeometryHelper.CreatePoint(point.X, point.Y,
                        point.Z, point.M, hasZ, hasM);
                    feature.Store();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static void CreateLineFeature(IFeatureLayer featureLayer, IPolyline polyline)
        {
            try
            {
                bool hasZ = FeatureClassUtil.CheckHasZ(featureLayer.FeatureClass);
                bool hasM = FeatureClassUtil.CheckHasM(featureLayer.FeatureClass);
                IPointCollection pointCollection = polyline as IPointCollection;
                if (pointCollection == null)
                    return;
                for (int i = 1; i < pointCollection.PointCount; i++)
                {
                    IPoint fromPoint = pointCollection.Point[i - 1];
                    IPoint toPoint = pointCollection.Point[i];
                    IFeature feature = featureLayer.FeatureClass.CreateFeature();
                    feature.Shape = GeometryHelper.CreatePointCollection(fromPoint, toPoint, hasZ, hasM) as IPolyline;
                    feature.Store();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static IPoint GetFarthestPoint(IPolyline polyline, IPoint point)
        {
            if (GetDistance(polyline.FromPoint, point) <= GetDistance(polyline.ToPoint, point))
                return polyline.ToPoint;
            else
                return polyline.FromPoint;
        }
    }
}

