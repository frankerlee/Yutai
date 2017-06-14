// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  ITableView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/07  16:50
// 更新时间 :  2017/06/07  16:50

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Editor
{
    public interface ITableView
    {
        IVirtualGridView VirtualGridView { get; }
        string Name { get; }
        string Text { get; }
        int CurrentOID { get; }
        IFeatureLayer FeatureLayer { get; }
    }
}