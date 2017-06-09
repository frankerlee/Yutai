using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Enums;
using Yutai.Plugins.TableEditor.Views;

namespace Yutai.Plugins.TableEditor.Editor
{
    public partial class VirtualGrid : UserControl
    {
        public event EventHandler SelectFeatures;
        private bool _isClone = true;
        private int m_ShowMaxRecNum = 300;
        private string m_strGeometry = "";
        private DataTable m_pDataTable;
        private int m_MaxOID = 0;
        private IFeatureLayer _featureLayer;
        private ICursor _cursor;
        private bool _isLockMap = false;    // false:可以对地图进行操作；
        private TableType _tableType;

        public VirtualGrid()
        {
            InitializeComponent();
            m_pDataTable = new DataTable();
        }

        public int CurrentOID
        {
            get
            {
                if (this.FeatureClass == null)
                    return -1;
                if (this.dataGridViewAll.CurrentRow == null)
                    return -1;
                return Convert.ToInt32(this.dataGridViewAll.CurrentRow.Cells[0].Value);
            }
        }

        public List<int> SelectedOIDs
        {
            get
            {
                List<int> list = new List<int>();
                for (int i = this.dataGridViewAll.SelectedRows.Count - 1; i >= 0; i--)
                {
                    list.Add(Convert.ToInt32(this.dataGridViewAll.SelectedRows[i].Cells[0].Value));
                }

                return list;
            }
        }

        public IFeatureClass FeatureClass
        {
            get { return _featureLayer.FeatureClass; }
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
        
        public int RecordNum { get; set; }

        public void ShowTable(string whereCaluse)
        {
            if (_featureLayer == null)
                return;
            this.m_strGeometry = this.GetShapeString((this._featureLayer as IFeatureLayer).FeatureClass);
            IQueryFilter pQueryFilter = null;
            if (string.IsNullOrWhiteSpace(whereCaluse) == false)
            {
                pQueryFilter = new QueryFilterClass()
                {
                    WhereClause = whereCaluse
                };
            }
            RecordNum = FeatureClass.FeatureCount(pQueryFilter);
            _cursor = FeatureClass.Search(pQueryFilter, false) as ICursor;

            AddFilesInfoToListViewColumn(m_pDataTable, _cursor.Fields);
            LoadToDataTable(_cursor, m_pDataTable, true);
            Marshal.ReleaseComObject(_cursor);
            _cursor = null;
            navigationBar.View = this.dataGridViewAll;
            this.dataGridViewAll.DataSource = m_pDataTable;
            this.dataGridViewAll.ReadOnly = true;

            this.dataGridViewAll.ClearSelection();
            this.dataGridViewAll.SelectionChanged += dataGridView1_SelectionChanged;
        }

        public void ClearTable()
        { 
            m_pDataTable?.Rows.Clear();
        }

        private void AddFilesInfoToListViewColumn(DataTable pDataTable, IFields pFields)
        {
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                try
                {
                    IField field = pFields.Field[i];
                    DataColumn dataColumn = new DataColumn(field.Name)
                    {
                        Caption = field.AliasName
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

        private void LoadToDataTable(ICursor pCursor, DataTable pDataTable, bool bAll)
        {
            try
            {
                int reocrdNum = RecordNum - pDataTable.Rows.Count;
                if (reocrdNum > 0)
                {
                    if (!bAll)
                    {
                        reocrdNum = (reocrdNum > this.m_ShowMaxRecNum ? this.m_ShowMaxRecNum : reocrdNum);
                    }
                    IFields fields = pCursor.Fields;
                    int num = 0;
                    IRow row = pCursor.NextRow();
                    object[] mStrGeometry = new object[fields.FieldCount];
                    while (row != null)
                    {
                        for (int i = 0; i < fields.FieldCount; i++)
                        {
                            IField field = fields.Field[i];
                            if (field.Type == esriFieldType.esriFieldTypeGeometry)
                            {
                                mStrGeometry[i] = this.m_strGeometry;
                            }
                            else if (field.Type != esriFieldType.esriFieldTypeBlob)
                            {
                                object value = row.Value[i];
                                mStrGeometry[i] = value;
                            }
                            else
                            {
                                mStrGeometry[i] = "二进制数据";
                            }
                        }
                        pDataTable.Rows.Add(mStrGeometry);
                        num++;
                        if (row.HasOID)
                        {
                            this.m_MaxOID = row.OID;
                        }
                        if (num < reocrdNum)
                        {
                            row = pCursor.NextRow();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch
            {
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.navigationBar.ShowRecordInfo();
            this.navigationBar.ShowCurrentRowInfo();
            if (_isLockMap)
                return;
            OnSelectFeaturesHandler();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            this.navigationBar.ShowRecordInfo();
        }

        /// <summary>
        /// 表格1当前行发生变化时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewSelected_CurrentCellChanged(object sender, EventArgs e)
        {
            this.navigationBar.ShowCurrentRowInfo();
        }

        /// <summary>
        /// 表格1绑定之后执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewAll_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.Reset)
            {
                if (_isClone)
                {
                    CopyDataGridViewStructure();
                    _isClone = false;
                }
                this.navigationBar.ShowRecordInfo();
                this.navigationBar.ShowRecordInfo();
            }
        }
        /// <summary>
        /// 将表格1的结构拷贝到表格2
        /// </summary>
        private void CopyDataGridViewStructure()
        {
            this.dataGridViewSelected.Columns.Clear();
            for (int i = 0; i < this.dataGridViewAll.Columns.Count; i++)
            {
                this.dataGridViewSelected.Columns.Add((DataGridViewColumn)this.dataGridViewAll.Columns[i].Clone());
            }
        }

        private void navigationBar_SwitchTableEventHandler(object sender, TableType args)
        {
            _tableType = args;
            switch (args)
            {
                case TableType.All:
                    navigationBar.View = dataGridViewAll;
                    dataGridViewAll.Visible = true;
                    dataGridViewSelected.Visible = false;
                    break;
                case TableType.Selected:
                    CopyToSubGrid();
                    navigationBar.View = dataGridViewSelected;
                    dataGridViewAll.Visible = false;
                    dataGridViewSelected.Visible = true;
                    break;
            }
            navigationBar.ShowRecordInfo();
            navigationBar.ShowCurrentRowInfo();
        }

        private void dataGridViewSelected_SelectionChanged(object sender, EventArgs e)
        {
            this.navigationBar.ShowRecordInfo();
            this.navigationBar.ShowCurrentRowInfo();
        }

        protected virtual void OnSelectFeaturesHandler()
        {
            SelectFeatures?.Invoke(this, EventArgs.Empty);
        }

        public void SelectionChanged(List<int> oids)
        {
            _isLockMap = true;
            this.dataGridViewAll.ClearSelection();
            if (oids.Count > 0)
            {
                for (int i = 0; i < this.dataGridViewAll.Rows.Count; i++)
                {
                    DataGridViewRow pRow = this.dataGridViewAll.Rows[i];
                    int oid = Convert.ToInt32(pRow.Cells[0].Value);
                    if (oids.Contains(oid))
                        pRow.Selected = true;
                }
            }
            this.dataGridViewAll.Refresh();
            _isLockMap = false;
            if (_tableType == TableType.Selected)
                CopyToSubGrid();
            navigationBar.ShowRecordInfo();
            navigationBar.ShowCurrentRowInfo();
        }

        public void CopyToSubGrid()
        {
            this.dataGridViewSelected.Rows.Clear();

            for (int i = this.dataGridViewAll.SelectedRows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow dataGridViewRow = this.dataGridViewAll.SelectedRows[i];
                this.dataGridViewSelected.Rows.Add((dataGridViewRow.DataBoundItem as DataRowView).Row.ItemArray);
            }
            this.dataGridViewSelected.Refresh();
        }

        public void SelectAll()
        {
            _isLockMap = true;
            this.dataGridViewAll.SelectAll();
            _isLockMap = false;
            OnSelectFeaturesHandler();
        }

        public void SelectNone()
        {
            _isLockMap = true;
            this.dataGridViewAll.ClearSelection();
            _isLockMap = false;
            OnSelectFeaturesHandler();
        }

        public void InvertSelection()
        {
            _isLockMap = true;
            for (int i = 0; i < this.dataGridViewAll.RowCount; i++)
            {
                DataGridViewRow pRow = this.dataGridViewAll.Rows[i];
                pRow.Selected = !pRow.Selected;
            }
            this.dataGridViewAll.Refresh();
            _isLockMap = false;
            OnSelectFeaturesHandler();
        }
    }
}

