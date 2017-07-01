using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.ArcGIS.Common.Editor;
using Yutai.ArcGIS.Common.Helpers;

namespace Yutai.ArcGIS.Controls.Controls
{
    public partial class frmAttributeTable : Form
    {
        private bool m_CanDo = true;
        private bool m_CanDoFeatureLayerSelectChange = true;
        private bool m_CanDoSelectChange = false;
        private bool m_InEditing = false;
        private bool m_IsChange = false;
        private int m_MaxOID = 0;
        private DataTable m_pDataTable = new DataTable();
        private IBasicMap m_pMap = null;
        private ITable m_pTable = null;
        private Common.ControlExtend.XtraGrid m_pXtraGrid = new Common.ControlExtend.XtraGrid();
        private long m_ShowRecNum = 1000;
        private string m_strGeometry = "";
        private string m_strWhere = "";

        public frmAttributeTable()
        {
            this.InitializeComponent();
            this.IsShowAll = true;
            this.m_pDataTable.ColumnChanged += new DataColumnChangeEventHandler(this.m_pDataTable_ColumnChanged);
            EditorEvent.OnStopEditing += new EditorEvent.OnStopEditingHandler(this.EditorEvent_OnStopEditing);
            EditorEvent.OnStartEditing += new EditorEvent.OnStartEditingHandler(this.EditorEvent_OnStartEditing);
            EditorEvent.OnAddFeature += new EditorEvent.OnAddFeatureHandler(this.EditorEvent_OnAddFeature);
            EditorEvent.OnDeleteFeature += new EditorEvent.OnDeleteFeatureHandler(this.EditorEvent_OnDeleteFeature);
        }

        private void AddFilesInfoToListViewColumn(IFields pFields)
        {
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField field = pFields.get_Field(i);
                DataColumn column = new DataColumn(field.AliasName)
                {
                    Caption = field.AliasName
                };
                if (!(field.Domain is ICodedValueDomain))
                {
                    if (field.Type == esriFieldType.esriFieldTypeDouble)
                    {
                        column.DataType = System.Type.GetType("System.Double");
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeInteger)
                    {
                        column.DataType = System.Type.GetType("System.Int32");
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        column.DataType = System.Type.GetType("System.Int16");
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeSingle)
                    {
                        column.DataType = System.Type.GetType("System.Double");
                    }
                    else if (field.Type == esriFieldType.esriFieldTypeDate)
                    {
                        column.DataType = System.Type.GetType("System.DateTime");
                    }
                }
                if (field.Type == esriFieldType.esriFieldTypeBlob)
                {
                    column.ReadOnly = true;
                }
                else if (field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    column.ReadOnly = true;
                }
                else
                {
                    column.ReadOnly = !field.Editable;
                }
                this.m_pDataTable.Columns.Add(column);
            }
        }

        public void AddNextToTable(bool bAll)
        {
            if (this.m_pTable != null)
            {
                IQueryFilter queryFilter = null;
                queryFilter = new QueryFilterClass();
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                if (this.m_strWhere.Length > 1)
                {
                    queryFilter.WhereClause = this.m_strWhere + " and " + this.m_pTable.OIDFieldName + " > " +
                                              this.m_MaxOID.ToString();
                }
                else
                {
                    queryFilter.WhereClause = this.m_pTable.OIDFieldName + " > " + this.m_MaxOID.ToString();
                }
                this.m_pCursor = this.m_pTable.Search(queryFilter, false);
                this.AddRecordToListView(bAll);
                this.m_pCursor = null;
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private void AddRecordToListView(bool bAll)
        {
            try
            {
                if (bAll)
                {
                    this.m_ShowRecNum = this.m_RecordNum - this.m_pDataTable.Rows.Count;
                }
                else
                {
                    this.m_ShowRecNum = ((this.m_RecordNum - this.m_pDataTable.Rows.Count) > 300)
                        ? ((long) 300)
                        : ((long) this.m_RecordNum);
                }
                IFields fields = this.m_pCursor.Fields;
                int num = 0;
                IRow row = this.m_pCursor.NextRow();
                object[] values = new object[fields.FieldCount];
                while (row != null)
                {
                    for (int i = 0; i < fields.FieldCount; i++)
                    {
                        IField field = fields.get_Field(i);
                        if (field.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            values[i] = this.m_strGeometry;
                            continue;
                        }
                        if (field.Type == esriFieldType.esriFieldTypeBlob)
                        {
                            values[i] = "二进制数据";
                            continue;
                        }
                        object obj2 = row.get_Value(i);
                        IDomain domain = field.Domain;
                        if (domain != null)
                        {
                            if (domain is ICodedValueDomain)
                            {
                                for (int j = 0; j < (field.Domain as ICodedValueDomain).CodeCount; j++)
                                {
                                    if ((domain as ICodedValueDomain).get_Value(j).ToString() == obj2.ToString())
                                    {
                                        obj2 = (domain as ICodedValueDomain).get_Name(j);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            string name = (row.Table as IDataset).Name;
                            CodeDomainEx codeDomainEx = CodeDomainManage.GetCodeDomainEx(field.Name, name);
                            if (codeDomainEx != null)
                            {
                                obj2 = codeDomainEx.FindName(obj2.ToString());
                            }
                        }
                        values[i] = obj2;
                    }
                    this.m_pDataTable.Rows.Add(values);
                    num++;
                    if (row.HasOID)
                    {
                        this.m_MaxOID = row.OID;
                    }
                    if (num >= this.m_ShowRecNum)
                    {
                        return;
                    }
                    row = this.m_pCursor.NextRow();
                }
            }
            catch
            {
            }
        }

        private void dataGrid1_Click(object sender, EventArgs e)
        {
        }

        private void dataGrid1_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                if ((this.m_InEditing && this.m_CanDo) && (((GridView) this.dataGrid1.MainView).SelectedRowsCount > 0))
                {
                    IFeatureLayer pTable = this.m_pTable as IFeatureLayer;
                    if (pTable.FeatureClass != null)
                    {
                        string oIDFieldName = pTable.FeatureClass.OIDFieldName;
                        int[] selectedRows = ((GridView) this.dataGrid1.MainView).GetSelectedRows();
                        this.m_CanDoFeatureLayerSelectChange = false;
                        IWorkspaceEdit workspace = (this.m_pTable as IDataset).Workspace as IWorkspaceEdit;
                        for (int i = 0; i < selectedRows.Length; i++)
                        {
                            object obj2 = ((GridView) this.dataGrid1.MainView).GetRow(selectedRows[i]);
                            if (obj2 is DataRowView)
                            {
                                int num2 = Convert.ToInt32((obj2 as DataRowView).Row[oIDFieldName]);
                                IQueryFilter queryFilter = new QueryFilterClass
                                {
                                    WhereClause = this.m_pTable.OIDFieldName + " = " + num2.ToString()
                                };
                                ICursor o = this.m_pTable.Search(queryFilter, false);
                                IRow row = o.NextRow();
                                if (row != null)
                                {
                                    workspace.StartEditOperation();
                                    row.Delete();
                                    workspace.StopEditOperation();
                                }
                                row = null;
                                ComReleaser.ReleaseCOMObject(o);
                                o = null;
                            }
                        }
                        this.m_CanDoFeatureLayerSelectChange = true;
                        if (this.m_pTable is IFeatureLayer)
                        {
                            try
                            {
                                (this.m_pMap as IActiveView).Refresh();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            else if (e.Button.ButtonType == NavigatorButtonType.Last)
            {
                if (this.m_pDataTable.Rows.Count < this.m_RecordNum)
                {
                    this.AddRecordToListView(true);
                }
            }
            else if (((e.Button.ButtonType == NavigatorButtonType.Next) &&
                      (((GridView) this.dataGrid1.MainView).GetSelectedRows()[0] == (this.m_pDataTable.Rows.Count - 2))) &&
                     (this.m_pDataTable.Rows.Count < this.m_RecordNum))
            {
                this.AddRecordToListView(false);
            }
        }

        private void dataGrid1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
            }
        }

        private void dataGrid1_LocationChanged(object sender, EventArgs e)
        {
        }

        private void EditorEvent_OnAddFeature(ILayer pLayer, IFeature pFeature)
        {
            if (pLayer == this.m_pTable)
            {
                this.m_CanDo = false;
                IFields fields = pFeature.Fields;
                object[] values = new object[fields.FieldCount];
                for (int i = 0; i < fields.FieldCount; i++)
                {
                    IField field = fields.get_Field(i);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        values[i] = this.m_strGeometry;
                        continue;
                    }
                    if (field.Type == esriFieldType.esriFieldTypeBlob)
                    {
                        values[i] = "二进制数据";
                        continue;
                    }
                    object obj2 = pFeature.get_Value(i);
                    if (field.Domain is ICodedValueDomain)
                    {
                        for (int j = 0; j < (field.Domain as ICodedValueDomain).CodeCount; j++)
                        {
                            if ((field.Domain as ICodedValueDomain).get_Value(j).ToString() == obj2.ToString())
                            {
                                obj2 = (field.Domain as ICodedValueDomain).get_Name(j);
                                break;
                            }
                        }
                    }
                    values[i] = obj2;
                }
                this.m_pDataTable.Rows.Add(values);
                this.m_CanDo = true;
            }
        }

        private void EditorEvent_OnDeleteFeature(ILayer pLayer, int OID)
        {
            if (this.m_pTable == pLayer)
            {
                this.m_CanDo = false;
                string oIDFieldName = (this.m_pTable as IFeatureLayer).FeatureClass.OIDFieldName;
                for (int i = 0; i < ((GridView) this.dataGrid1.MainView).RowCount; i++)
                {
                    object row = ((GridView) this.dataGrid1.MainView).GetRow(i);
                    if (row is DataRowView)
                    {
                        int num2 = Convert.ToInt32((row as DataRowView).Row[oIDFieldName]);
                        if (OID == num2)
                        {
                            ((GridView) this.dataGrid1.MainView).DeleteRow(i);
                            break;
                        }
                    }
                }
                this.m_CanDo = true;
            }
        }

        private void EditorEvent_OnStartEditing()
        {
            if (this.m_pTable is IFeatureLayer)
            {
                IFeatureClass featureClass = (this.m_pTable as IFeatureLayer).FeatureClass;
                if ((featureClass == null) || !((featureClass as IDataset).Workspace as IWorkspaceEdit).IsBeingEdited())
                {
                    return;
                }
            }
            if (((this.m_pTable as IDataset).Workspace is IWorkspaceEdit) &&
                ((this.m_pTable as IDataset).Workspace as IWorkspaceEdit).IsBeingEdited())
            {
                this.m_pXtraGrid.ReadOnly = false;
                if ((this.m_pTable is IFeatureClass) || (this.m_pTable is IFeatureLayer))
                {
                    this.dataGrid1.EmbeddedNavigator.Buttons.Append.Enabled = false;
                }
                else
                {
                    this.dataGrid1.EmbeddedNavigator.Buttons.Append.Enabled = true;
                }
                this.dataGrid1.EmbeddedNavigator.Buttons.Remove.Enabled = true;
                this.m_InEditing = true;
            }
        }

        private void EditorEvent_OnStopEditing()
        {
            this.m_pXtraGrid.ReadOnly = true;
            this.dataGrid1.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.dataGrid1.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.m_InEditing = false;
        }

        private void frmAttributeTable_FeatureLayerSelectionChanged()
        {
            if ((this.m_pTable is IFeatureSelection) && this.m_CanDoFeatureLayerSelectChange)
            {
                this.m_CanDoSelectChange = false;
                IFeatureSelection pTable = this.m_pTable as IFeatureSelection;
                IEnumIDs iDs = pTable.SelectionSet.IDs;
                iDs.Reset();
                int num = iDs.Next();
                string oIDFieldName = (this.m_pTable as IFeatureLayer).FeatureClass.OIDFieldName;
                int num2 = 0;
                while (num != -1)
                {
                    ((GridView) this.dataGrid1.MainView).ClearSelection();
                    for (int i = num2; i < ((GridView) this.dataGrid1.MainView).RowCount; i++)
                    {
                        object row = ((GridView) this.dataGrid1.MainView).GetRow(i);
                        if ((row is DataRowView) && (Convert.ToInt32((row as DataRowView).Row[oIDFieldName]) == num))
                        {
                            ((GridView) this.dataGrid1.MainView).SelectRow(i);
                            num2 = i + 1;
                            break;
                        }
                    }
                    num = iDs.Next();
                }
                this.m_CanDoSelectChange = true;
            }
        }

        private void frmAttributeTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.m_CanDoSelectChange && (this.m_pTable is IFeatureLayer))
                {
                    if (this.m_pMap.SelectionCount > 0)
                    {
                        (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        this.m_pMap.ClearSelection();
                        (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                    }
                    if (((GridView) this.dataGrid1.MainView).SelectedRowsCount > 0)
                    {
                        IFeatureLayer pTable = this.m_pTable as IFeatureLayer;
                        if (pTable.FeatureClass != null)
                        {
                            string oIDFieldName = pTable.FeatureClass.OIDFieldName;
                            int[] selectedRows = ((GridView) this.dataGrid1.MainView).GetSelectedRows();
                            this.m_CanDoFeatureLayerSelectChange = false;
                            IFeature feature = null;
                            for (int i = 0; i < selectedRows.Length; i++)
                            {
                                object row = ((GridView) this.dataGrid1.MainView).GetRow(selectedRows[i]);
                                if (row is DataRowView)
                                {
                                    try
                                    {
                                        int iD = Convert.ToInt32((row as DataRowView).Row[oIDFieldName]);
                                        feature = pTable.FeatureClass.GetFeature(iD);
                                        if (this.m_pMap is IMap)
                                        {
                                            (this.m_pMap as IMap).SelectFeature(this.m_pTable as ILayer, feature);
                                        }
                                        else if (this.m_pMap is IScene)
                                        {
                                            (this.m_pMap as IScene).SelectFeature(this.m_pTable as ILayer, feature);
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            this.m_CanDoFeatureLayerSelectChange = true;
                            if (this.m_pMap is IMap)
                            {
                                CommonHelper.Zoom2SelectedFeature(this.m_pMap as IActiveView);
                            }
                            else
                            {
                                CommonHelper.Zoom2SelectedFeature(this.m_pMap as IScene);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private string GetShapeString(IFeatureClass pFeatClass)
        {
            if (pFeatClass == null)
            {
                return "";
            }
            string str = "";
            switch (pFeatClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    str = "点";
                    break;

                case esriGeometryType.esriGeometryMultipoint:
                    str = "多点";
                    break;

                case esriGeometryType.esriGeometryPolyline:
                    str = "线";
                    break;

                case esriGeometryType.esriGeometryPolygon:
                    str = "多边形";
                    break;

                case esriGeometryType.esriGeometryMultiPatch:
                    str = "多面";
                    break;
            }
            int index = pFeatClass.Fields.FindField(pFeatClass.ShapeFieldName);
            IGeometryDef geometryDef = pFeatClass.Fields.get_Field(index).GeometryDef;
            str = str + " ";
            if (geometryDef.HasZ)
            {
                str = str + "Z";
            }
            if (geometryDef.HasM)
            {
                str = str + "M";
            }
            return str;
        }

        private void LayoutControl()
        {
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Size = new Size(base.Width, base.Height - this.panel1.Height);
        }

        private void m_pDataTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (this.m_InEditing && this.m_CanDo)
            {
                object[] itemArray = e.Row.ItemArray;
                try
                {
                    IRow row;
                    int num;
                    IField field;
                    int num2;
                    IWorkspaceEdit workspace = (this.m_pTable as IDataset).Workspace as IWorkspaceEdit;
                    if (itemArray[0] is DBNull)
                    {
                        if (!(this.m_pTable is IFeatureClass))
                        {
                            workspace.StartEditOperation();
                            row = this.m_pTable.CreateRow();
                            num = row.Fields.FindFieldByAliasName(e.Column.ColumnName);
                            if (num != -1)
                            {
                                field = row.Fields.get_Field(num);
                                if (field.Domain is ICodedValueDomain)
                                {
                                    for (num2 = 0; num2 < (field.Domain as ICodedValueDomain).CodeCount; num2++)
                                    {
                                        if (e.ProposedValue.ToString() ==
                                            (field.Domain as ICodedValueDomain).get_Name(num2))
                                        {
                                            row.set_Value(num, (field.Domain as ICodedValueDomain).get_Value(num2));
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    string name = (row.Table as IDataset).Name;
                                    CodeDomainEx codeDomainEx = CodeDomainManage.GetCodeDomainEx(field.Name, name);
                                    if (codeDomainEx != null)
                                    {
                                        row.set_Value(num, codeDomainEx.GetCodeByName(e.ProposedValue.ToString()));
                                    }
                                    else
                                    {
                                        row.set_Value(num, e.ProposedValue);
                                    }
                                }
                                row.Store();
                            }
                            workspace.StopEditOperation();
                            this.m_CanDo = false;
                            e.Row[this.m_pTable.OIDFieldName] = row.OID;
                            this.m_CanDo = true;
                        }
                    }
                    else
                    {
                        int num3 = Convert.ToInt32(itemArray[0]);
                        IQueryFilter queryFilter = new QueryFilterClass
                        {
                            WhereClause = this.m_pTable.OIDFieldName + " = " + num3.ToString()
                        };
                        ICursor o = this.m_pTable.Search(queryFilter, false);
                        row = o.NextRow();
                        if (row != null)
                        {
                            workspace.StartEditOperation();
                            num = row.Fields.FindFieldByAliasName(e.Column.ColumnName);
                            if (num != -1)
                            {
                                field = row.Fields.get_Field(num);
                                if (field.Domain is ICodedValueDomain)
                                {
                                    for (num2 = 0; num2 < (field.Domain as ICodedValueDomain).CodeCount; num2++)
                                    {
                                        if (e.ProposedValue.ToString() ==
                                            (field.Domain as ICodedValueDomain).get_Name(num2))
                                        {
                                            row.set_Value(num, (field.Domain as ICodedValueDomain).get_Value(num2));
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    row.set_Value(num, e.ProposedValue);
                                }
                            }
                            row.Store();
                            workspace.StopEditOperation();
                        }
                        row = null;
                        ComReleaser.ReleaseCOMObject(o);
                        o = null;
                    }
                }
                catch (COMException exception)
                {
                    if (exception.ErrorCode == -2147467259)
                    {
                        MessageBox.Show("输入数据大于字段长度");
                    }
                    else
                    {
                        MessageBox.Show(exception.Message);
                    }
                    e.Row.CancelEdit();
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message);
                    e.Row.CancelEdit();
                }
            }
        }

        private void m_pDataTable_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            if (this.m_InEditing && this.m_CanDo)
            {
                try
                {
                    object[] itemArray = e.Row.ItemArray;
                    IWorkspaceEdit workspace = (this.m_pTable as IDataset).Workspace as IWorkspaceEdit;
                    if (!(itemArray[0] is DBNull))
                    {
                        int num = Convert.ToInt32(itemArray[0]);
                        IQueryFilter queryFilter = new QueryFilterClass
                        {
                            WhereClause = this.m_pTable.OIDFieldName + " = " + num.ToString()
                        };
                        IRow row = this.m_pTable.Search(queryFilter, false).NextRow();
                        if (row != null)
                        {
                            workspace.StartEditOperation();
                            row.Delete();
                            workspace.StopEditOperation();
                        }
                        row = null;
                        if (this.m_pTable is IFeatureLayer)
                        {
                            (this.m_pMap as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography,
                                this.m_pTable, null);
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public void Reset()
        {
            this.m_pTable = null;
            this.m_pDataTable.Rows.Clear();
            this.m_pDataTable.Columns.Clear();
        }

        public void ShowTable()
        {
            this.m_CanDo = false;
            this.m_pDataTable.Rows.Clear();
            this.m_pDataTable.Columns.Clear();
            this.m_CanDo = true;
            if (this.m_pCursor != null)
            {
                ComReleaser.ReleaseCOMObject(this.m_pCursor);
                this.m_pCursor = null;
            }
            if (this.m_pTable != null)
            {
                IQueryFilter queryFilter = null;
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                if (this.m_strWhere.Length > 1)
                {
                    queryFilter = new QueryFilterClass
                    {
                        WhereClause = this.m_strWhere
                    };
                }
                try
                {
                    if (this.IsShowAll)
                    {
                        this.m_RecordNum = this.m_pTable.RowCount(queryFilter);
                        this.m_pCursor = this.m_pTable.Search(queryFilter, false);
                    }
                    else
                    {
                        ComReleaser.ReleaseCOMObject(this.m_pCursor);
                        if (this.m_pTable is IFeatureLayer)
                        {
                        }
                    }
                    try
                    {
                        this.AddFilesInfoToListViewColumn(this.m_pTable.Fields);
                        this.AddRecordToListView(false);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                    if (this.m_pTable.HasOID)
                    {
                    }
                    this.m_pXtraGrid.SetDataBinding(this.dataGrid1, this.m_pDataTable);
                    this.m_pXtraGrid.ReadOnly = true;
                    for (int i = 0; i < this.m_pTable.Fields.FieldCount; i++)
                    {
                        IList list;
                        int num2;
                        IField field = this.m_pTable.Fields.get_Field(i);
                        if (field.Domain != null)
                        {
                            if (field.Domain is ICodedValueDomain)
                            {
                                list = new ArrayList();
                                num2 = 0;
                                while (num2 < (field.Domain as ICodedValueDomain).CodeCount)
                                {
                                    list.Add((field.Domain as ICodedValueDomain).get_Name(num2));
                                    num2++;
                                }
                                this.m_pXtraGrid.SetColumnAttr(i, ColumnAttribute.CA_COMBOBOX, list);
                            }
                            else if ((field.Domain is IRangeDomain) &&
                                     ((((field.Type == esriFieldType.esriFieldTypeDouble) ||
                                        (field.Type == esriFieldType.esriFieldTypeSingle)) ||
                                       (field.Type == esriFieldType.esriFieldTypeSmallInteger)) ||
                                      (field.Type == esriFieldType.esriFieldTypeInteger)))
                            {
                                this.m_pXtraGrid.SetColumnAttr(i, ColumnAttribute.CA_SPINEDIT,
                                    (double) (field.Domain as IRangeDomain).MinValue,
                                    (double) (field.Domain as IRangeDomain).MaxValue);
                            }
                        }
                        else
                        {
                            list = new ArrayList();
                            string name = (this.m_pTable as IDataset).Name;
                            CodeDomainEx codeDomainEx = CodeDomainManage.GetCodeDomainEx(field.Name, name);
                            if (codeDomainEx != null)
                            {
                                if ((codeDomainEx.ParentIDFieldName == null) ||
                                    (codeDomainEx.ParentIDFieldName.Length == 0))
                                {
                                    NameValueCollection codeDomain = codeDomainEx.GetCodeDomain();
                                    if (field.IsNullable)
                                    {
                                        list.Add("<空>");
                                    }
                                    for (num2 = 0; num2 < codeDomain.Count; num2++)
                                    {
                                        string str2 = codeDomain.Keys[num2];
                                        list.Add(str2);
                                    }
                                    this.m_pXtraGrid.SetColumnAttr(i, ColumnAttribute.CA_COMBOBOX, list);
                                }
                                else
                                {
                                    this.m_pXtraGrid.SetColumnAttr(i, ColumnAttribute.CA_TREEVIEWCOMBOX, codeDomainEx);
                                }
                            }
                            else if ((((field.Type == esriFieldType.esriFieldTypeDouble) ||
                                       (field.Type == esriFieldType.esriFieldTypeSingle)) ||
                                      (field.Type == esriFieldType.esriFieldTypeSmallInteger)) ||
                                     (field.Type == esriFieldType.esriFieldTypeInteger))
                            {
                                this.m_pXtraGrid.SetColumnAttr(i, ColumnAttribute.CA_SPINEDIT);
                            }
                        }
                    }
                }
                catch
                {
                }
                System.Windows.Forms.Cursor.Current = Cursors.Default;
            }
        }

        private void TableControl_Load(object sender, EventArgs e)
        {
            ((GridView) this.dataGrid1.MainView).SelectionChanged +=
                new SelectionChangedEventHandler(this.frmAttributeTable_SelectionChanged);
            this.ShowTable();
            if (this.m_pTable is IDataset)
            {
                this.Text = (this.m_pTable as IDataset).Name;
            }
            this.frmAttributeTable_FeatureLayerSelectionChanged();
            this.m_CanDoSelectChange = true;
            IWorkspaceEdit workspace = (this.m_pTable as IDataset).Workspace as IWorkspaceEdit;
            if ((workspace != null) && workspace.IsBeingEdited())
            {
                this.EditorEvent_OnStartEditing();
            }
        }

        private void TableControl_SizeChanged(object sender, EventArgs e)
        {
            this.LayoutControl();
        }

        public bool IsShowAll { get; set; }

        public IBasicMap Map
        {
            set { this.m_pMap = value; }
        }

        public ITable Table
        {
            get { return this.m_pTable; }
            set
            {
                this.m_pTable = value;
                if (this.m_pTable is IFeatureClass)
                {
                    this.m_strGeometry = this.GetShapeString(this.m_pTable as IFeatureClass);
                }
                else if (this.m_pTable is IFeatureLayer)
                {
                    this.m_strGeometry = this.GetShapeString((this.m_pTable as IFeatureLayer).FeatureClass);
                }
                if (this.m_pTable is IFeatureLayerSelectionEvents_Event)
                {
                    (this.m_pTable as IFeatureLayerSelectionEvents_Event).FeatureLayerSelectionChanged +=
                    (new IFeatureLayerSelectionEvents_FeatureLayerSelectionChangedEventHandler(
                        this.frmAttributeTable_FeatureLayerSelectionChanged));
                }
            }
        }
    }
}