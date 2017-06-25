using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;
using Yutai.Pipeline.Config.Concretes;

namespace Yutai.Pipeline.Config.Helpers
{
    public class ConfigHelper
    {
        public static bool ValidateLayerGeometryType(IFeatureLayer pLayer, enumPipelineDataType pType)
        {
            esriGeometryType geometryType=pLayer.FeatureClass.ShapeType;
            switch (pType)
            {
                case enumPipelineDataType.Point:
                case enumPipelineDataType.AssPoint:
                    if (geometryType == esriGeometryType.esriGeometryPoint) return true;
                    return false;
                    break;
                case enumPipelineDataType.Line:
                case enumPipelineDataType.AssLine:
                    if (geometryType == esriGeometryType.esriGeometryPolyline) return true;
                    return false;
                    break;
                case enumPipelineDataType.Point3D:
                case enumPipelineDataType.Line3D:
                    if (geometryType == esriGeometryType.esriGeometryMultiPatch) return true;
                    if (geometryType == esriGeometryType.esriGeometryTriangles) return true;
                    break;
                case enumPipelineDataType.AnnoPoint:
                case enumPipelineDataType.AnnoLine:
                case enumPipelineDataType.Annotation:
                    if (pLayer is IGeoFeatureLayer) return true;
                    return false;
                    break;
                case enumPipelineDataType.Other:
                    return false;
                    break;
                default:
                    return false;
            }
            return false;
        }

        public static string GetBasicClassName(IFeatureClass featureClass)
        {
            string[] strArray = (featureClass as IDataset).Name.Split(new char[] { '.' });
            string str = strArray[strArray.Length - 1];
            return str;
        }

        public static string GetClassOwnerName(string pDSName)
        {
            int index = pDSName.IndexOf(".");
            if (index >= 0)
            {
                pDSName = pDSName.Substring(0, index);
            }
            else
            {
                pDSName = "";
            }
            pDSName = pDSName.ToUpper();
            return pDSName;
        }
        public static string GetClassShortName(IDataset paramDS)
        {
            if (paramDS == null)
            {
                return "";
            }
            return GetClassShortName(paramDS.Name.ToUpper());
        }

        public static string GetClassShortName(IFeatureClass fc)
        {
            try
            {
                string str = "";
                str = (fc as IDataset).Name;
                int num = str.LastIndexOf(".");
                if (num >= 0)
                {
                    str = str.Substring(num + 1);
                }
                return str;
            }
            catch
            {
                return "";
            }
        }
        public static string GetClassShortName(string paramName)
        {
            string str = paramName;
            int num = paramName.LastIndexOf(".");
            if (num >= 0)
            {
                str = paramName.Substring(num + 1);
            }
            return str;
        }

        public static IArray OrganizeMapWorkspaceAndLayer(IMap pMap)
        {
            IArray arrayClass = new ESRI.ArcGIS.esriSystem.ArrayClass();
            IPropertySet propertySetClass = new PropertySetClass();
            IFeatureLayer featureLayer = null;


            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            IEnumLayer layers = pMap.Layers[uid, true];
            layers.Reset();
            ILayer pLayer = layers.Next();
            while (pLayer != null)
            {
                if (pLayer is IFeatureLayer)
                {
                    featureLayer = pLayer as IFeatureLayer;
                    IDataset featureClass = featureLayer.FeatureClass as IDataset;
                    if (featureClass != null && !(featureClass is ICoverageFeatureClass))
                    {
                        IWorkspace workspace = featureClass.Workspace;
                        if (workspace is IFeatureWorkspace)
                        {
                            IPropertySet connectionProperties = workspace.ConnectionProperties;
                            bool hasFindWorkspace = false;
                            for (int i = 0; i < arrayClass.Count; i++)
                            {
                                PipeWorkspaceInfo workspaceInfo = arrayClass.Element[i] as PipeWorkspaceInfo;
                                if (workspaceInfo.Workspace.ConnectionProperties.Equals(connectionProperties))
                                {
                                    workspaceInfo.AddClass(featureLayer.FeatureClass);
                                    hasFindWorkspace = true;
                                    break;
                                }
                            }
                            if (hasFindWorkspace == false)
                            {
                                PipeWorkspaceInfo workspaceInfo = new PipeWorkspaceInfo(workspace);
                                workspaceInfo.AddClass(featureLayer.FeatureClass);
                                arrayClass.Add(workspaceInfo);
                            }
                        }
                    }
                }
                pLayer = layers.Next();
            }
            return arrayClass;
        }
    }
}
