using System.Collections;
using System.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace Yutai.ArcGIS.Controls.XtraGrid
{
    internal class XtraGrid
    {
        private int _ColumnCount = -1;
        private DataTable _mainDataTable;
        private GridControl _xGrid;
        private GridView _xGridView;
        private bool m_ReadOnly = false;
        private GridColumn[] xGridColumns;

        public void SetColumnAttr(int iColumnIndex, ColumnAttribute ColumnAttribute)
        {
            this.SetColumnAttr(iColumnIndex, ColumnAttribute, "", "", null, "", 0.0, 0.0);
        }

        public void SetColumnAttr(string sColumnName, ColumnAttribute ColumnAttribute)
        {
            this.SetColumnAttr(-1, ColumnAttribute, "", "", null, sColumnName, 0.0, 0.0);
        }

        public void SetColumnAttr(int iColumnIndex, ColumnAttribute ColumnAttribute, object columnDataTable)
        {
            this.SetColumnAttr(iColumnIndex, ColumnAttribute, "", "", columnDataTable, "", 0.0, 0.0);
        }

        public void SetColumnAttr(string sColumnName, ColumnAttribute ColumnAttribute, object columnDataTable)
        {
            this.SetColumnAttr(-1, ColumnAttribute, "", "", columnDataTable, sColumnName, 0.0, 0.0);
        }

        public void SetColumnAttr(int iColumnIndex, ColumnAttribute ColumnAttribute, double iMinValue, double iMaxValue)
        {
            this.SetColumnAttr(iColumnIndex, ColumnAttribute, "", "", null, "", iMinValue, iMaxValue);
        }

        public void SetColumnAttr(string sColumnName, ColumnAttribute ColumnAttribute, double iMinValue,
            double iMaxValue)
        {
            this.SetColumnAttr(-1, ColumnAttribute, "", "", null, sColumnName, iMinValue, iMaxValue);
        }

        public void SetColumnAttr(int iColumnIndex, ColumnAttribute ColumnAttribute, string ColumnValueFieldName,
            string ColumnDisplayFieldName, object columnDataTable)
        {
            this.SetColumnAttr(iColumnIndex, ColumnAttribute, ColumnValueFieldName, ColumnDisplayFieldName,
                columnDataTable, "", 0.0, 0.0);
        }

        public void SetColumnAttr(string sColumnName, ColumnAttribute ColumnAttribute, string ColumnValueFieldName,
            string ColumnDisplayFieldName, object columnDataTable)
        {
            this.SetColumnAttr(-1, ColumnAttribute, ColumnValueFieldName, ColumnDisplayFieldName, columnDataTable,
                sColumnName, 0.0, 0.0);
        }

        private void SetColumnAttr(int iColumnIndex, ColumnAttribute ColumnAttribute, string ColumnValueFieldName,
            string ColumnDisplayFieldName, object columnDataTable, string sColumnName, double iMinValue,
            double iMaxValue)
        {
            int num;
            if (iColumnIndex < 0)
            {
                for (num = 0; num < this.xGridColumns.Length; num++)
                {
                    if (this.xGridColumns[num].FieldName.ToUpper() == sColumnName.ToUpper())
                    {
                        iColumnIndex = num;
                    }
                }
            }
            switch (ColumnAttribute)
            {
                case ColumnAttribute.CA_LOOKUPEDIT:
                {
                    RepositoryItemLookUpEdit edit = new RepositoryItemLookUpEdit();
                    if (columnDataTable is DataTable)
                    {
                        edit.DataSource = columnDataTable;
                        edit.DisplayMember = ColumnDisplayFieldName;
                        edit.ValueMember = ColumnValueFieldName;
                    }
                    this.xGridColumns[iColumnIndex].ColumnEdit = edit;
                    break;
                }
                case ColumnAttribute.CA_COMBOBOX:
                {
                    RepositoryItemComboBox box = new RepositoryItemComboBox();
                    for (num = 0; num < ((IList) columnDataTable).Count; num++)
                    {
                        box.Items.Add(((IList) columnDataTable)[num].ToString());
                    }
                    this.xGridColumns[iColumnIndex].ColumnEdit = box;
                    break;
                }
                case ColumnAttribute.CA_CHECKEDIT:
                    this.xGridColumns[iColumnIndex].ColumnEdit = new RepositoryItemCheckEdit();
                    break;

                case ColumnAttribute.CA_SPINEDIT:
                    this.xGridColumns[iColumnIndex].ColumnEdit = new RepositoryItemSpinEdit();
                    ((RepositoryItemSpinEdit) ((GridView) this.xGrid.MainView).Columns[iColumnIndex].ColumnEdit)
                        .MaxValue = (decimal) iMaxValue;
                    ((RepositoryItemSpinEdit) ((GridView) this.xGrid.MainView).Columns[iColumnIndex].ColumnEdit)
                        .MinValue = (decimal) iMinValue;
                    break;

                case ColumnAttribute.CA_TIMEEDIT:
                    this.xGridColumns[iColumnIndex].ColumnEdit = new RepositoryItemTimeEdit();
                    break;
            }
        }

        public void SetColumnCaption(string[] strCaption)
        {
            for (int i = 0; i < strCaption.Length; i++)
            {
                this._xGridView.Columns[i].Caption = strCaption.GetValue(i).ToString();
            }
        }

        public void SetDataBinding()
        {
            this.SetDataBinding(null, null);
        }

        public void SetDataBinding(GridControl xGrid, DataTable mainDataTable)
        {
            if ((xGrid != null) && (mainDataTable != null))
            {
                this._xGrid = xGrid;
                this._mainDataTable = mainDataTable;
                this._xGridView = (GridView) this._xGrid.MainView;
                this._xGridView.Columns.Clear();
                this._ColumnCount = this._mainDataTable.Columns.Count;
                this.xGridColumns = new GridColumn[this._ColumnCount];
                for (int i = 0; i < this._ColumnCount; i++)
                {
                    this.xGridColumns[i] = new GridColumn();
                    this.xGridColumns[i].Caption = this._mainDataTable.Columns[i].ColumnName;
                    this.xGridColumns[i].FieldName = this._mainDataTable.Columns[i].ColumnName;
                    this.xGridColumns[i].VisibleIndex = i;
                    this.xGridColumns[i].OptionsColumn.ReadOnly = this._mainDataTable.Columns[i].ReadOnly;
                }
                this._xGridView.Columns.AddRange(this.xGridColumns);
                this._xGrid.MainView = this._xGridView;
                this._xGrid.DataSource = this._mainDataTable;
            }
        }

        private void SetGridReadOnlyStatus(bool ReadOnly)
        {
            ((GridView) this._xGrid.MainView).OptionsBehavior.Editable = !ReadOnly;
        }

        public DataTable mainDataTable
        {
            get { return this._mainDataTable; }
            set { this._mainDataTable = value; }
        }

        public bool ReadOnly
        {
            get { return this.m_ReadOnly; }
            set
            {
                this.m_ReadOnly = value;
                this.SetGridReadOnlyStatus(value);
            }
        }

        public GridControl xGrid
        {
            get { return this._xGrid; }
            set { this._xGrid = value; }
        }
    }
}