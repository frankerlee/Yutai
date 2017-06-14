// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IVirtualGridView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/13  12:00
// 更新时间 :  2017/06/13  12:00

using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.Plugins.TableEditor.Editor
{
    public interface IVirtualGridView
    {
        DataGridView GridView { get; }
        string StrGeometry { get; }
        DataTable Table { get; }
        void AddColumnToGrid(IField pField);
        void ClearTable();
        void HideField(int columnIndex);
        void InvertSelection();
        void RemoveField(int index);
        void SelectAll();
        void SelectNone();
        void ShowAlias();
        void ShowAllFields();
        void ShowName();
        void ShowTable(string whereCaluse);
        void Sort(int columnIndex, ListSortDirection direction);
        void UpdateField(int index, IField field);
    }
}