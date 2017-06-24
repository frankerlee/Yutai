using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Config.Interfaces;

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
    }
}
