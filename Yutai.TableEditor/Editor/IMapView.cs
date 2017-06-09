// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IMapView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/07  17:40
// 更新时间 :  2017/06/07  17:40

using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Editor
{
    public interface IMapView
    {
        IFeature SelectFeature(IFeatureLayer featureLayer, int oid);
        void SelectFeatures(IFeatureLayer featureLayer, List<int> oids);
        void ZoomToFeature(IFeatureLayer featureLayer, int oid);
        void ZoomToSelectedFeatures(IFeatureLayer featureLayer);
        void PanToFeature(IFeatureLayer featureLayer, int oid);
    }
}