using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Config.Helpers
{
    public class EnumHelper
    {
        public static enumPipelineHeightType ConvertHeightTypeFromStr(string typeStr)
        {
            if (typeStr.Contains("顶")) return enumPipelineHeightType.Top;
            if (typeStr.Contains("底")) return enumPipelineHeightType.Bottom;
            return enumPipelineHeightType.Middle;
        }

        public static string ConvertHeightTypeToStr(enumPipelineHeightType type)
        {
            if (type == enumPipelineHeightType.Top) return "管顶";
            if (type == enumPipelineHeightType.Bottom) return "管底";
            return "管中";
        }

        public static enumPipelineDataType ConvertDataTypeFromString(string typeStr)
        {
            switch (typeStr.ToUpper())
            {
                case "POINT":
                    return enumPipelineDataType.Point;
                case "LINE":
                    return enumPipelineDataType.Line;
                case "NETWORK":
                    return enumPipelineDataType.Network;
                case "JUNCTION":
                    return enumPipelineDataType.Junction;
                case "POINT3D":
                    return enumPipelineDataType.Point3D;
                case "LINE3D":
                    return enumPipelineDataType.Line3D;
                case "ASSPOINT":
                    return enumPipelineDataType.AssPoint;
                case "ASSLINE":
                    return enumPipelineDataType.AssLine;
                case "ANNOPOINT":
                    return enumPipelineDataType.AnnoPoint;
                case "ANNOLINE":
                    return enumPipelineDataType.AnnoLine;
                case "ANNOTATION":
                    return enumPipelineDataType.Annotation;
                case "OTHER":
                default:
                    return enumPipelineDataType.Other;
            }
        }

        public static string ConvertDataTypeToString(enumPipelineDataType type)
        {
            switch (type)
            {
                case enumPipelineDataType.Point:
                    return "POINT";
                case enumPipelineDataType.Line:
                    return "LINE";
                    case enumPipelineDataType.Network:
                    return "NETWORK";
                    case enumPipelineDataType.Junction:
                    return "JUNCTION";
                case enumPipelineDataType.Point3D:
                    return "POINT3D";
                case enumPipelineDataType.Line3D:
                    return "LINE3D";
                case enumPipelineDataType.AssPoint:
                    return "ASSPOINT";
                case enumPipelineDataType.AssLine:
                    return "ASSLINE";
                case enumPipelineDataType.AnnoPoint:
                    return "ANNOPOINT";
                case enumPipelineDataType.AnnoLine:
                    return "ANNOLINE";
                case enumPipelineDataType.Annotation:
                    return "ANNOTATION";
                case enumPipelineDataType.Other:
                default:
                    return "OTHER";
            }
        }

        public static enumFunctionLayerType ConvertFunctionLayerTypeFromString(string typeStr)
        {
            switch (typeStr.ToUpper())
            {
                case "ITEM":
                    return enumFunctionLayerType.Item;
                case "OTHER":
                default:
                    return enumFunctionLayerType.Other;
            }
        }

        public static string ConvertFunctionLayerTypeToString(enumFunctionLayerType type)
        {
            switch (type)
            {
                case enumFunctionLayerType.Item:
                    return "ITEM";
                case enumFunctionLayerType.Other:
                default:
                    return "OTHER";
            }
        }
    }
}
