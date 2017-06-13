// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IFieldPropertiesView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/12  12:00
// 更新时间 :  2017/06/12  12:00

using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Views
{
    public interface IFieldPropertiesView
    {
        IField NewField { get; }
    }
}