// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  MapHelper.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/02  15:28
// 更新时间 :  2017/06/02  15:28

using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class MapHelper
    {
        public static IEnvelope GetSelectFeatureEnvelop(IFeatureLayer featureLayer)
        {
            IEnvelope envelope;
            ICursor cursor;
            IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
            if (featureSelection.SelectionSet.Count != 0)
            {
                IEnvelope extent = null;
                IEnvelope extent1 = null;
                IFeature feature = null;
                double num = 5;
                featureSelection.SelectionSet.Search(null, false, out cursor);
                for (IRow i = cursor.NextRow(); i != null; i = cursor.NextRow())
                {
                    feature = i as IFeature;
                    if ((feature == null ? false : feature.Shape != null))
                    {
                        try
                        {
                            if (extent != null)
                            {
                                extent1 = feature.Extent;
                                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                                {
                                    extent1.Expand(num, num, false);
                                }
                                extent.Union(extent1);
                            }
                            else
                            {
                                extent = feature.Extent;
                                if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
                                {
                                    extent.Expand(num, num, false);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                Marshal.ReleaseComObject(cursor);
                envelope = extent;
            }
            else
            {
                envelope = null;
            }
            return envelope;
        }

        public static void Zoom2SelectedFeature(IActiveView activeView, IFeatureLayer featureLayer)
        {
            try
            {
                double num = 5;
                IEnvelope selectFeatureEnvelop = GetSelectFeatureEnvelop(featureLayer);
                if (selectFeatureEnvelop != null)
                {
                    selectFeatureEnvelop.Expand(num, num, false);
                    activeView.Extent = selectFeatureEnvelop;
                }
                activeView.Refresh();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}