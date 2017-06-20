using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.TableEditor.Enums;

namespace Yutai.Plugins.TableEditor.Editor
{
    public delegate void ColumnHeaderRightClickEventHandler(object sender, DataGridViewCellMouseEventArgs e);

    public partial class Grid : UserControl, IVirtualGridView
    {
        public event ColumnHeaderRightClickEventHandler ColumnHeaderRightClick;
        public event EventHandler SelectFeatures;
        private string _strGeometry = "";
        private IFeatureLayer _featureLayer;
        private DataTable _dataTable;
        private DataTable _joinDataTable;
        private DataView _dataViewAll;
        private DataView _dataViewSelected;
        private int _recordNum;
        private ICursor _cursor;
        private bool _isLockMap = false; // false:可以对地图进行操作；
        private TableType _tableType;
        private List<int> _selectedRowIdList;

        public Grid()
        {
            InitializeComponent();
            _dataGridView.AllowUserToAddRows = false;
            _dataTable = new DataTable();
        }

        public DataGridView GridView
        {
            get { return _dataGridView; }
        }

        public string StrGeometry
        {
            get { return _strGeometry; }
        }

        public DataTable Table
        {
            get { return _dataTable; }
        }

        public IFeatureLayer FeatureLayer
        {
            get { return _featureLayer; }
            set
            {
                _featureLayer = value;
                ShowTable(null);
            }
        }

        public List<int> SelectedRowIdList
        {
            get { return _selectedRowIdList; }
        }

        public int RecordNum
        {
            get { return _recordNum; }
        }

        public int CurrentOID
        {
            get
            {
                if (this._featureLayer == null)
                    return -1;
                if (this._dataGridView.CurrentRow == null)
                    return -1;
                return Convert.ToInt32(_dataGridView.CurrentRow.Cells[0].Value);
            }
        }

        public TableType TableType
        {
            get { return _tableType; }
        }

        public void AddColumnToGrid(IField pField)
        {
            DataColumn dataColumn = new DataColumn(pField.Name)
            {
                Caption = pField.AliasName
            };
            _dataTable.Columns.Add(dataColumn);
        }

        public void ClearSorting()
        {
            _dataGridView.SortedColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public void ClearTable()
        {
            _dataTable.Rows.Clear();
        }

        public DataTable ConvertITableToDataTable(IFeatureClass featureClass, List<string> fields)
        {
            string strGeometry = GetShapeString(featureClass);
            ITable table = featureClass as ITable;
            return ConvertITableToDataTable(table, strGeometry, featureClass.AliasName, fields);
        }

        public DataTable ConvertITableToDataTable(ITable table, string strGeometry, string name, List<string> displayFields)
        {
            DataTable dataTable = new DataTable(name);
            try
            {
                IFields fields = table.Fields;
                ICursor cursor = table.Search(null, false);
                AddFilesInfoToListViewColumn(dataTable, fields, displayFields);

                IRow row;
                object[] strValues = new object[fields.FieldCount];
                while ((row = cursor.NextRow()) != null)
                {
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        IField field = fields.Field[i];
                        if (field.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            strValues[i] = strGeometry;
                        }
                        else if (field.Type != esriFieldType.esriFieldTypeBlob)
                        {
                            object value = row.Value[i];
                            strValues[i] = value;
                        }
                        else
                        {
                            strValues[i] = "二进制数据";
                        }
                    }
                    dataTable.Rows.Add(strValues);
                }

                Marshal.ReleaseComObject(cursor);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return dataTable;
        }

        public void HideField(int columnIndex)
        {
            if (columnIndex <= -1)
                return;
            _dataGridView.Columns[columnIndex].Visible = false;
        }

        public void InvertSelection()
        {
            _isLockMap = true;
            for (int i = 0; i < this._dataGridView.RowCount; i++)
            {
                DataGridViewRow pRow = this._dataGridView.Rows[i];
                pRow.Selected = !pRow.Selected;
            }
            this._dataGridView.Refresh();
            _isLockMap = false;
            OnSelectFeaturesHandler();
        }

        public void RemoveField(int index)
        {
            _dataTable.Columns.RemoveAt(index);
        }

        public void SelectAll()
        {
            _isLockMap = true;
            this._dataGridView.SelectAll();
            _isLockMap = false;
            OnSelectFeaturesHandler();
        }

        public void SelectionChanged(List<int> oids)
        {
            if (oids == null)
                return;
            _isLockMap = true;
            this._dataGridView.ClearSelection();
            if (oids.Count > 0)
            {
                for (int i = 0; i < this._dataGridView.Rows.Count; i++)
                {
                    DataGridViewRow pRow = this._dataGridView.Rows[i];
                    int oid = Convert.ToInt32(pRow.Cells[0].Value);
                    if (oids.Contains(oid))
                        pRow.Selected = true;
                }
            }
            this._dataGridView.Refresh();
            _isLockMap = false;
            if (_tableType == TableType.Selected)
                CopyToSubGrid();
            _navigationBar.ShowRecordInfo();
            _navigationBar.ShowCurrentRowInfo();
        }

        public void SelectNone()
        {
            _isLockMap = true;
            this._dataGridView.ClearSelection();
            _isLockMap = false;
            OnSelectFeaturesHandler();
        }

        public void ShowAlias()
        {
            for (int i = 0; i < _dataTable.Columns.Count; i++)
            {
                DataColumn pColumn = _dataTable.Columns[i];
                _dataGridView.Columns[i].HeaderText = pColumn.Caption;
            }
        }

        public void ShowAllFields()
        {
            for (int i = 0; i < _dataGridView.ColumnCount; i++)
            {
                _dataGridView.Columns[i].Visible = true;
            }
        }

        public void ShowName()
        {
            for (int i = 0; i < _dataTable.Columns.Count; i++)
            {
                DataColumn pColumn = _dataTable.Columns[i];
                _dataGridView.Columns[i].HeaderText = pColumn.ColumnName;
            }
        }

        public void ShowTable(string whereCaluse)
        {
            if (_featureLayer == null)
                return;
            _tableType = TableType.All;
            _dataTable = ConvertITableToDataTable(_featureLayer.FeatureClass, null);
            _recordNum = _dataTable.Rows.Count;
            _navigationBar.View = this._dataGridView;
            _dataViewAll = _dataTable.DefaultView;
            this._dataGridView.DataSource = _dataViewAll;
            this._dataGridView.ReadOnly = true;

            this._dataGridView.ClearSelection();
            this._dataGridView.SelectionChanged += _dataGridView_SelectionChanged; ;

        }

        public void Sort(int columnIndex, ListSortDirection direction)
        {
            if (columnIndex <= -1)
                return;
            _dataGridView.Sort(_dataGridView.Columns[columnIndex], direction);
        }

        public void UpdateField(int index, IField field)
        {
            DataColumn pColumn = _dataTable.Columns[index];
            pColumn.ColumnName = field.Name;
            pColumn.Caption = field.AliasName;
        }

        public void JoinTable(IFeatureClass featureClass, string parentFieldName, string childFieldName, List<string> fields)
        {
            DataTable childDataTable = ConvertITableToDataTable(featureClass, fields);
            string childName = featureClass.AliasName;
            for (int i = 0; i < childDataTable.Columns.Count; i++)
            {
                DataColumn dataColumn = childDataTable.Columns[i];
                _dataTable.Columns.Add(new DataColumn()
                {
                    ColumnName = $"{childName}_{dataColumn.ColumnName}",
                    DataType = dataColumn.DataType,
                    Caption = $"{childName}_{dataColumn.Caption}"
                });
            }
            DataView childDataView = childDataTable.DefaultView;
            foreach (DataRow dataTableRow in _dataTable.Rows)
            {
                childDataView.RowFilter =
                    $" {childFieldName} = '{dataTableRow[parentFieldName]}' or {childFieldName} = {dataTableRow[parentFieldName]} ";
                if (childDataView.Count <= 0)
                    continue;
                DataRowView childDataRowView = childDataView[0];
                for (int i = 0; i < childDataTable.Columns.Count; i++)
                {
                    DataColumn childColumn = childDataTable.Columns[i];
                    dataTableRow[$"{childName}_{childColumn.ColumnName}"] = childDataRowView[childColumn.ColumnName];
                }
            }
        }
        
        public void StopJoin(string tableName)
        {
            for (int i = _dataTable.Columns.Count - 1; i >= 0; i--)
            {
                DataColumn dataColumn = _dataTable.Columns[i];
                if (dataColumn.ColumnName.StartsWith($"{tableName}_"))
                    _dataTable.Columns.Remove(dataColumn);
            }
        }

        private string GetShapeString(IFeatureClass pFeatClass)
        {
            string str;
            if (pFeatClass != null)
            {
                string str1 = "";
                switch (pFeatClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        {
                            str1 = "点";
                            break;
                        }
                    case esriGeometryType.esriGeometryMultipoint:
                        {
                            str1 = "多点";
                            break;
                        }
                    case esriGeometryType.esriGeometryPolyline:
                        {
                            str1 = "线";
                            break;
                        }
                    case esriGeometryType.esriGeometryPolygon:
                        {
                            str1 = "多边形";
                            break;
                        }
                    case esriGeometryType.esriGeometryEnvelope:
                    case esriGeometryType.esriGeometryPath:
                    case esriGeometryType.esriGeometryAny:
                    case esriGeometryType.esriGeometryMultiPatch:
                        {
                            str1 = "多面";
                            break;
                        }
                    default:
                        break;
                }
                int num = pFeatClass.Fields.FindField(pFeatClass.ShapeFieldName);
                IGeometryDef geometryDef = pFeatClass.Fields.Field[num].GeometryDef;
                str1 = string.Concat(str1, " ");
                if (geometryDef.HasZ)
                {
                    str1 = string.Concat(str1, "Z");
                }
                if (geometryDef.HasM)
                {
                    str1 = string.Concat(str1, "M");
                }
                str = str1;
            }
            else
            {
                str = "";
            }
            return str;
        }

        private void AddFilesInfoToListViewColumn(DataTable pDataTable, IFields pFields, List<string> fields)
        {
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                try
                {
                    IField field = pFields.Field[i];
                    if (fields != null && !fields.Contains(field.Name))
                        continue;
                    DataColumn dataColumn = new DataColumn(field.Name)
                    {
                        Caption = field.AliasName,
                    };
                    if (!(field.Domain is ICodedValueDomain))
                    {
                        if (field.Type == esriFieldType.esriFieldTypeDouble)
                        {
                            dataColumn.DataType = Type.GetType("System.Double");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeInteger)
                        {
                            dataColumn.DataType = Type.GetType("System.Int32");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeSmallInteger)
                        {
                            dataColumn.DataType = Type.GetType("System.Int16");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeSingle)
                        {
                            dataColumn.DataType = Type.GetType("System.Double");
                        }
                        else if (field.Type == esriFieldType.esriFieldTypeDate)
                        {
                            dataColumn.DataType = Type.GetType("System.DateTime");
                        }
                    }
                    if (field.Type == esriFieldType.esriFieldTypeBlob)
                    {
                        dataColumn.ReadOnly = true;
                    }
                    else if (field.Type != esriFieldType.esriFieldTypeGeometry)
                    {
                        dataColumn.ReadOnly = !field.Editable;
                    }
                    else
                    {
                        dataColumn.ReadOnly = true;
                    }
                    pDataTable.Columns.Add(dataColumn);
                }
                catch (Exception exception)
                {
                }
            }
        }

        private void _dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            _navigationBar.ShowRecordInfo();
            _navigationBar.ShowCurrentRowInfo();
            if (_isLockMap)
                return;
            OnSelectFeaturesHandler();
        }

        protected virtual void OnSelectFeaturesHandler()
        {
            SelectFeatures?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnColumnHeaderRightClick(DataGridViewCellMouseEventArgs e)
        {
            ColumnHeaderRightClick?.Invoke(this, e);
        }

        private void _dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                OnColumnHeaderRightClick(e);
            }

        }

        private void _dataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            this._navigationBar.ShowRecordInfo();
        }

        private void _navigationBar_SwitchTableEventHandler(object sender, Enums.TableType args)
        {
            _tableType = args;
            switch (args)
            {
                case TableType.All:
                    SetSelectedRows();
                    break;
                case TableType.Selected:
                    _selectedRowIdList = GetSelectedRows();
                    CopyToSubGrid();
                    break;
            }
            _navigationBar.ShowRecordInfo();
            _navigationBar.ShowCurrentRowInfo();
        }

        public List<int> GetSelectedRows()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < this._dataGridView.SelectedRows.Count; i++)
            {
                DataGridViewRow pRow = this._dataGridView.SelectedRows[i];
                list.Add(Convert.ToInt32(pRow.Cells[0].Value));
            }
            return list;
        }

        private void SetSelectedRows()
        {
            _dataViewAll = _dataTable.DefaultView;
            _dataGridView.DataSource = _dataViewAll;
            SelectionChanged(_selectedRowIdList);
        }

        public void CopyToSubGrid()
        {
            _dataViewSelected = _dataTable.AsDataView();
            if (_selectedRowIdList.Count <= 0)
            {
                _dataGridView.DataSource = null;
            }
            else
            {

                string values = $"({string.Join(",", _selectedRowIdList)})";
                _dataViewSelected.RowFilter = $" {_dataTable.Columns[0].ColumnName} IN {values} ";
                _dataGridView.DataSource = _dataViewSelected;
            }
        }
    }
}
