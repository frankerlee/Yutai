using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Axf
{
    public class AxfInfo
    {
        public bool IsSelect { get; set; }      // 是否选择导入/签入
        public int AddCount { get; set; }           // 增加个数
        public int ModifyCount { get; set; }        // 修改个数
        public int DeleteCount { get; set; }        // 删除个数
        public int NoEditCount { get; set; }        // 不变个数
        public string SourceLayerName { get; set; } // AXF图层名称，源图层名称
        public IFeatureClass SourceFeatureClass { get; set; }  
        public string TargetLayerName { get; set; } // 要导入的图层名称，目标图层名称
        public IFeatureLayer TargetFeatureLayer { get; set; }
        public string AxfFilePath { get; set; } // axf路径
        public List<AxfField> AxfFieldList { get; set; }
    }
}
