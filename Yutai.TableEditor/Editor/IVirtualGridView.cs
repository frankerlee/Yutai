// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  IVirtualGridView.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/13  12:00
// 更新时间 :  2017/06/13  12:00

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.TableEditor.Enums;

namespace Yutai.Plugins.TableEditor.Editor
{
    public interface IVirtualGridView
    {
        DataGridView GridView { get; }
        string StrGeometry { get; }
        DataTable Table { get; }
        IFeatureLayer FeatureLayer { get; set; }
        int RecordNum { get; }
        int CurrentOID { get; }
        TableType TableType { get; }
        void AddColumnToGrid(IField pField);
        void ClearSorting();
        void ClearTable();
        DataTable ConvertITableToDataTable(IFeatureClass featureClass, List<string> fields);
        DataTable ConvertITableToDataTable(ITable table, string strGeometry, string name, List<string> fields);
        List<int> GetSelectedRows();
        void HideField(int columnIndex);
        void InvertSelection();
        void RemoveField(int index);
        void SelectAll();
        void SelectionChanged(List<int> oids);
        void SelectNone();
        void ShowAlias();
        void ShowAllFields();
        void ShowName();
        void ShowTable(string whereCaluse);
        void Sort(int columnIndex, ListSortDirection direction);
        void UpdateField(int index, IField field);
        void JoinTable(IFeatureClass featureClass, string parentFieldName, string childFieldName, List<string> fields);
        void StopJoin(string tableName);
    }
}