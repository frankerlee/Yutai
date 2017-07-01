using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Editor.Helpers;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Commands
{
    internal class SkectchAssist
    {
        internal static bool CheckEnable(IAppContext context)
        {
            bool result;
            if (context.FocusMap == null)
            {
                result = false;
            }
            else if (context.FocusMap.LayerCount == 0)
            {
                result = false;
            }
            else if (Yutai.ArcGIS.Common.Editor.Editor.EditMap != null &&
                     Yutai.ArcGIS.Common.Editor.Editor.EditMap != context.FocusMap)
            {
                result = false;
            }
            else if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate == null)
            {
                result = false;
            }
            else
            {
                if (Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null)
                {
                    if (SketchShareEx.PointCount > 0)
                    {
                        result = true;
                        return result;
                    }
                    if (Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass != null &&
                        Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate.FeatureLayer.FeatureClass.ShapeType ==
                        esriGeometryType.esriGeometryPoint)
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            return result;
        }

        internal static bool CheckEnable2(IAppContext context)
        {
            return context.FocusMap != null && context.FocusMap.LayerCount != 0 &&
                   Yutai.ArcGIS.Common.Editor.Editor.EditMap == context.FocusMap &&
                   Yutai.ArcGIS.Common.Editor.Editor.CurrentEditTemplate != null &&
                   Yutai.ArcGIS.Common.Editor.Editor.EditWorkspace != null;
        }
    }
}