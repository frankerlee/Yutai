// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IFieldSetting.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/20  10:56
// 更新时间 :  2017/07/20  10:56

using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Pipeline.Editor.Classes
{
    public interface IFieldSetting
    {
        int Index { get; set; }
        string FieldName { get; }
        IField Field { get; }
        string Expression { get; }
        int Length { get; }
    }
}