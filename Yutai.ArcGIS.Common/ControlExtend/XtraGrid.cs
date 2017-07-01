using System.Collections;
using System.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Yutai.ArcGIS.Common.Data;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class XtraGrid
    {
        private bool bool_0 = false;
        private DataTable dataTable_0;
        private GridColumn[] gridColumn_0;
        private GridControl gridControl_0;
        private GridView gridView_0;
        private int int_0 = -1;

        private void method_0(bool bool_1)
        {
            if (this.gridControl_0 != null)
            {
                if (this.gridControl_0.MainView != null)
                {
                    ((GridView) this.gridControl_0.MainView).OptionsBehavior.Editable = !bool_1;
                }
                else if (this.gridView_0 != null)
                {
                    this.gridView_0.OptionsBehavior.Editable = !bool_1;
                }
            }
        }

        private void method_1(int int_1, ColumnAttribute columnAttribute_0, string string_0, string string_1,
            object object_0, string string_2, double double_0, double double_1)
        {
            int num;
            if (int_1 < 0)
            {
                for (num = 0; num < this.gridColumn_0.Length; num++)
                {
                    if (this.gridColumn_0[num].FieldName.ToUpper() == string_2.ToUpper())
                    {
                        int_1 = num;
                    }
                }
            }
            switch (columnAttribute_0)
            {
                case ColumnAttribute.CA_LOOKUPEDIT:
                {
                    RepositoryItemLookUpEdit edit2 = new RepositoryItemLookUpEdit();
                    if (object_0 is DataTable)
                    {
                        edit2.DataSource = object_0;
                        edit2.DisplayMember = string_1;
                        edit2.ValueMember = string_0;
                    }
                    this.gridColumn_0[int_1].ColumnEdit = edit2;
                    break;
                }
                case ColumnAttribute.CA_COMBOBOX:
                {
                    RepositoryItemComboBox box = new RepositoryItemComboBox();
                    for (num = 0; num < ((IList) object_0).Count; num++)
                    {
                        box.Items.Add(((IList) object_0)[num].ToString());
                    }
                    this.gridColumn_0[int_1].ColumnEdit = box;
                    break;
                }
                case ColumnAttribute.CA_CHECKEDIT:
                    this.gridColumn_0[int_1].ColumnEdit = new RepositoryItemCheckEdit();
                    break;

                case ColumnAttribute.CA_SPINEDIT:
                    this.gridColumn_0[int_1].ColumnEdit = new RepositoryItemSpinEdit();
                    ((RepositoryItemSpinEdit) ((GridView) this.xGrid.MainView).Columns[int_1].ColumnEdit).MaxValue =
                        (decimal) double_1;
                    ((RepositoryItemSpinEdit) ((GridView) this.xGrid.MainView).Columns[int_1].ColumnEdit).MinValue =
                        (decimal) double_0;
                    break;

                case ColumnAttribute.CA_TIMEEDIT:
                    this.gridColumn_0[int_1].ColumnEdit = new RepositoryItemTimeEdit();
                    break;

                case ColumnAttribute.CA_TREEVIEWCOMBOX:
                {
                    RepositoryItemTreeViewComboBoxEdit edit = new RepositoryItemTreeViewComboBoxEdit();
                    CodeDomainEx.CodeDomainEx ex = object_0 as CodeDomainEx.CodeDomainEx;
                    DataAccessLayerBaseClass dataAccessLayer =
                        DataAccessLayerFactory.GetDataAccessLayer(DataProviderType.OleDb, ex.ConnectionStr);
                    edit.DataAccessLayerBaseClass = dataAccessLayer;
                    edit.TableName = ex.TableFieldName;
                    edit.CodeFieldName = ex.CodeFieldName;
                    edit.NameFieldName = ex.NameFieldName;
                    edit.ParentIDFieldName = ex.ParentIDFieldName;
                    edit.IDFieldName = ex.IDFieldName;
                    this.gridColumn_0[int_1].ColumnEdit = edit;
                    break;
                }
            }
        }

        public void SetColumnAttr(int int_1, ColumnAttribute columnAttribute_0)
        {
            this.method_1(int_1, columnAttribute_0, "", "", null, "", 0.0, 0.0);
        }

        public void SetColumnAttr(string string_0, ColumnAttribute columnAttribute_0)
        {
            this.method_1(-1, columnAttribute_0, "", "", null, string_0, 0.0, 0.0);
        }

        public void SetColumnAttr(int int_1, ColumnAttribute columnAttribute_0, object object_0)
        {
            this.method_1(int_1, columnAttribute_0, "", "", object_0, "", 0.0, 0.0);
        }

        public void SetColumnAttr(string string_0, ColumnAttribute columnAttribute_0, object object_0)
        {
            this.method_1(-1, columnAttribute_0, "", "", object_0, string_0, 0.0, 0.0);
        }

        public void SetColumnAttr(int int_1, ColumnAttribute columnAttribute_0, double double_0, double double_1)
        {
            this.method_1(int_1, columnAttribute_0, "", "", null, "", double_0, double_1);
        }

        public void SetColumnAttr(string string_0, ColumnAttribute columnAttribute_0, double double_0, double double_1)
        {
            this.method_1(-1, columnAttribute_0, "", "", null, string_0, double_0, double_1);
        }

        public void SetColumnAttr(int int_1, ColumnAttribute columnAttribute_0, string string_0, string string_1,
            object object_0)
        {
            this.method_1(int_1, columnAttribute_0, string_0, string_1, object_0, "", 0.0, 0.0);
        }

        public void SetColumnAttr(string string_0, ColumnAttribute columnAttribute_0, string string_1, string string_2,
            object object_0)
        {
            this.method_1(-1, columnAttribute_0, string_1, string_2, object_0, string_0, 0.0, 0.0);
        }

        public void SetColumnCaption(string[] string_0)
        {
            for (int i = 0; i < string_0.Length; i++)
            {
                this.gridView_0.Columns[i].Caption = string_0.GetValue(i).ToString();
            }
        }

        public void SetDataBinding()
        {
            this.SetDataBinding(null, null);
        }

        public void SetDataBinding(GridControl gridControl_1, DataTable dataTable_1)
        {
            if ((gridControl_1 != null) && (dataTable_1 != null))
            {
                this.gridControl_0 = gridControl_1;
                this.dataTable_0 = dataTable_1;
                this.gridView_0 = (GridView) this.gridControl_0.MainView;
                this.gridView_0.Columns.Clear();
                this.int_0 = this.dataTable_0.Columns.Count;
                this.gridColumn_0 = new GridColumn[this.int_0];
                for (int i = 0; i < this.int_0; i++)
                {
                    this.gridColumn_0[i] = new GridColumn();
                    this.gridColumn_0[i].Caption = this.dataTable_0.Columns[i].ColumnName;
                    this.gridColumn_0[i].FieldName = this.dataTable_0.Columns[i].ColumnName;
                    this.gridColumn_0[i].VisibleIndex = i;
                    this.gridColumn_0[i].OptionsColumn.ReadOnly = this.dataTable_0.Columns[i].ReadOnly;
                }
                this.gridView_0.Columns.AddRange(this.gridColumn_0);
                this.gridControl_0.DataSource = this.dataTable_0;
            }
        }

        public DataTable mainDataTable
        {
            get { return this.dataTable_0; }
            set { this.dataTable_0 = value; }
        }

        public bool ReadOnly
        {
            get { return this.bool_0; }
            set
            {
                this.bool_0 = value;
                this.method_0(value);
            }
        }

        public GridControl xGrid
        {
            get { return this.gridControl_0; }
            set { this.gridControl_0 = value; }
        }
    }
}