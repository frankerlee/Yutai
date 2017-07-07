using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Concretes;
using Yutai.Pipeline.Config.Interfaces;

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

        public static IFeatureLayer GetLayerByFeatureClassAliasName(ICompositeLayer compositeLayer, string featureClassName)
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
                        string[] strArray = dataset.Name.Split(new char[] { '.' });
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
                            string[] strArray = dataset.Name.Split(new char[] { '.' });
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
            return Math.Sqrt((point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y));
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
    }
}
