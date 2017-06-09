// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  ITableEditorView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  15:14
// 更新时间 :  2017/06/06  15:14

using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.TableEditor.Editor;

namespace Yutai.Plugins.TableEditor.Views
{
    public interface ITableEditorView : IMenuProvider
    {
        IMapView MapView { get; }
        Dictionary<int, IGridView> GridViews { get; set; }
        TabControl MainTabControl { get; }
        TabPage CurTabPage { get; set; }
        TabPage CreateTabPage(IFeatureLayer featureLayer);
        TabPage CreateTabPage(IFeatureClass featureClass);
        TabPage CreateTabPage(string name, int handle);
        void ActivatePage(int handle);
        void ClosePage(int handle);
        void ClosePage();
        void OpenTable(IFeatureLayer featureLayer);
        void Clear();
        IGridView CurrentGridView { get; }

    }
}