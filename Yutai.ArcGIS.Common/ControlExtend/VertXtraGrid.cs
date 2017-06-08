using System;
using System.Collections;
using System.Data;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using Yutai.ArcGIS.Common.Data;
using Yutai.ArcGIS.Common.Wrapper;

namespace Yutai.ArcGIS.Common.ControlExtend
{
    public class VertXtraGrid
    {
        private GridColumn gridColumn_0;
        private GridColumn gridColumn_1;
        private GridControl gridControl_0;
        private GridEditorCollection gridEditorCollection_0;
        private GridView gridView_0;
        private RepositoryItemButtonEdit repositoryItemButtonEdit_0;
        private RepositoryItemCalcEdit repositoryItemCalcEdit_0;
        private RepositoryItemCheckEdit repositoryItemCheckEdit_0;
        private RepositoryItemColorEdit repositoryItemColorEdit_0;
        private RepositoryItemComboBox repositoryItemComboBox_0;
        private RepositoryItemDateEdit repositoryItemDateEdit_0;
        private RepositoryItemImageEdit repositoryItemImageEdit_0;
        private RepositoryItemLookUpEdit repositoryItemLookUpEdit_0;
        private RepositoryItemMemoEdit repositoryItemMemoEdit_0;
        private RepositoryItemMemoExEdit repositoryItemMemoExEdit_0;
        private RepositoryItemSpinEdit repositoryItemSpinEdit_0;
        private RepositoryItemTextEdit repositoryItemTextEdit_0;
        private RepositoryItemTimeEdit repositoryItemTimeEdit_0;
        private RepositoryItemTreeViewComboBoxEdit repositoryItemTreeViewComboBoxEdit_0;
        private string string_0;
        private string string_1;

        public VertXtraGrid(GridControl gridControl_1)
        {
            this.gridControl_0 = gridControl_1;
            this.gridView_0 = (GridView) this.gridControl_0.MainView;
            this.gridColumn_0 = new GridColumn();
            this.gridColumn_1 = new GridColumn();
            this.method_1();
            this.gridEditorCollection_0 = new GridEditorCollection();
            this.gridControl_0.DataSource = this.gridEditorCollection_0;
        }

        public void AddButtonEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemButtonEdit_0 = new RepositoryItemButtonEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemButtonEdit_0, string_2, object_0);
            this.repositoryItemButtonEdit_0.AutoHeight = false;
            this.repositoryItemButtonEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemButtonEdit_0.Name = "repositoryItemButtonEdit";
            this.repositoryItemButtonEdit_0.ReadOnly = bool_0;
        }

        public void AddButtonEdit(string string_2, object object_0, bool bool_0, ButtonPressedEventHandler buttonPressedEventHandler_0, object object_1)
        {
            this.repositoryItemButtonEdit_0 = new RepositoryItemButtonEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemButtonEdit_0, string_2, object_0);
            this.repositoryItemButtonEdit_0.AutoHeight = false;
            this.repositoryItemButtonEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemButtonEdit_0.Name = "repositoryItemButtonEdit";
            this.repositoryItemButtonEdit_0.ReadOnly = bool_0;
            if (!bool_0)
            {
            }
            this.repositoryItemButtonEdit_0.Tag = object_1;
            this.repositoryItemButtonEdit_0.ButtonClick += buttonPressedEventHandler_0;
        }

        public void AddCalcEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemCalcEdit_0 = new RepositoryItemCalcEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemCalcEdit_0, string_2, object_0);
            this.repositoryItemCalcEdit_0.AutoHeight = false;
            this.repositoryItemCalcEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemCalcEdit_0.Mask.MaskType = MaskType.Numeric;
            this.repositoryItemCalcEdit_0.Name = "repositoryItemCalcEdit";
        }

        public void AddCheckEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemCheckEdit_0 = new RepositoryItemCheckEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemCheckEdit_0, string_2, false);
            this.repositoryItemCheckEdit_0.AutoHeight = false;
            this.repositoryItemCheckEdit_0.Name = "repositoryItemCheckEdit";
            this.repositoryItemCheckEdit_0.ReadOnly = bool_0;
        }

        public void AddColorEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemColorEdit_0 = new RepositoryItemColorEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemColorEdit_0, string_2, object_0);
            this.repositoryItemColorEdit_0.AutoHeight = false;
            this.repositoryItemColorEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemColorEdit_0.Name = "repositoryItemColorEdit";
            this.repositoryItemColorEdit_0.ReadOnly = bool_0;
        }

        public void AddComBoBox(string string_2, object object_0, object object_1, bool bool_0)
        {
            this.repositoryItemComboBox_0 = new RepositoryItemComboBox();
            this.gridEditorCollection_0.Add(this.repositoryItemComboBox_0, string_2, object_0);
            this.repositoryItemComboBox_0.AutoHeight = false;
            this.repositoryItemComboBox_0.BorderStyle = BorderStyles.NoBorder;
            for (int i = 0; i < ((IList) object_1).Count; i++)
            {
                this.repositoryItemComboBox_0.Items.Add(((IList) object_1)[i]);
            }
            this.repositoryItemComboBox_0.Name = "repositoryItemComboBox";
            this.repositoryItemComboBox_0.ReadOnly = bool_0;
        }

        public void AddComBoBox(string string_2, object object_0, object object_1, bool bool_0, object object_2)
        {
            this.repositoryItemComboBox_0 = new RepositoryItemComboBox();
            this.gridEditorCollection_0.Add(this.repositoryItemComboBox_0, string_2, object_0, object_2);
            this.repositoryItemComboBox_0.AutoHeight = false;
            this.repositoryItemComboBox_0.BorderStyle = BorderStyles.NoBorder;
            for (int i = 0; i < ((IList) object_1).Count; i++)
            {
                this.repositoryItemComboBox_0.Items.Add(((IList) object_1)[i]);
            }
            if (!(object_0 is FieldObject))
            {
            }
            this.repositoryItemComboBox_0.Name = "repositoryItemComboBox";
            this.repositoryItemComboBox_0.ReadOnly = bool_0;
        }

        public void AddDateEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemDateEdit_0 = new RepositoryItemDateEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemDateEdit_0, string_2, object_0);
            this.repositoryItemDateEdit_0.AutoHeight = false;
            this.repositoryItemDateEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemDateEdit_0.Mask.EditMask = "d";
            this.repositoryItemDateEdit_0.Mask.MaskType = MaskType.DateTime;
            this.repositoryItemDateEdit_0.Name = "repositoryItemDateEdit";
            this.repositoryItemDateEdit_0.ReadOnly = bool_0;
        }

        public void AddImageEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemImageEdit_0 = new RepositoryItemImageEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemImageEdit_0, string_2, object_0);
            this.repositoryItemImageEdit_0.AutoHeight = false;
            this.repositoryItemImageEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemImageEdit_0.Name = "repositoryItemImageEdit";
            this.repositoryItemImageEdit_0.PopupStartSize = new Size(250, 300);
        }

        public void AddLookUpEdit(DataTable dataTable_0, string string_2, object object_0, object object_1, object object_2, object object_3, bool bool_0)
        {
            this.repositoryItemLookUpEdit_0 = new RepositoryItemLookUpEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemLookUpEdit_0, string_2, object_0);
            this.repositoryItemLookUpEdit_0.AutoHeight = false;
            this.repositoryItemLookUpEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemLookUpEdit_0.Columns.AddRange(new LookUpColumnInfo[] { new LookUpColumnInfo((string) object_2, (string) object_2, 160, FormatType.None, "", true, HorzAlignment.Near), new LookUpColumnInfo((string) object_0, (string) object_1, 140, FormatType.None, "", true, HorzAlignment.Near) });
            this.repositoryItemLookUpEdit_0.DataSource = dataTable_0;
            this.repositoryItemLookUpEdit_0.DisplayMember = (string) object_2;
            this.repositoryItemLookUpEdit_0.Name = "repositoryItemLookUpEdit";
            this.repositoryItemLookUpEdit_0.PopupWidth = 290;
            this.repositoryItemLookUpEdit_0.ValueMember = (string) object_3;
        }

        public void AddMemoEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemMemoEdit_0 = new RepositoryItemMemoEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemMemoEdit_0, string_2, object_0);
            this.repositoryItemMemoEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemMemoEdit_0.Name = "repositoryItemMemoEdit";
            this.repositoryItemMemoEdit_0.WordWrap = true;
            this.repositoryItemMemoEdit_0.ReadOnly = bool_0;
        }

        public void AddMemoExEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemMemoExEdit_0 = new RepositoryItemMemoExEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemMemoExEdit_0, string_2, object_0);
            this.repositoryItemMemoExEdit_0.AutoHeight = false;
            this.repositoryItemMemoExEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemMemoExEdit_0.Name = "repositoryItemMemoExEdit";
            this.repositoryItemMemoExEdit_0.PopupStartSize = new Size(250, 150);
            this.repositoryItemMemoExEdit_0.ReadOnly = bool_0;
        }

        public void AddSpinEdit(string string_2, object object_0, bool bool_0, double double_0, double double_1)
        {
            this.repositoryItemSpinEdit_0 = new RepositoryItemSpinEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemSpinEdit_0, string_2, object_0);
            this.repositoryItemSpinEdit_0.AutoHeight = false;
            this.repositoryItemSpinEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemSpinEdit_0.Mask.MaskType = MaskType.Numeric;
            this.repositoryItemSpinEdit_0.Name = "repositoryItemSpinEdit";
            this.repositoryItemSpinEdit_0.ReadOnly = bool_0;
            this.repositoryItemSpinEdit_0.MaxValue = (decimal) double_1;
            this.repositoryItemSpinEdit_0.MinValue = (decimal) double_0;
        }

        public void AddTextEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemTextEdit_0 = new RepositoryItemTextEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemTextEdit_0, string_2, object_0);
            this.repositoryItemTextEdit_0.AutoHeight = false;
            this.repositoryItemTextEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemTextEdit_0.Name = "repositoryItemTextEdit";
            this.repositoryItemTextEdit_0.ReadOnly = bool_0;
        }

        public void AddTimeEdit(string string_2, object object_0, bool bool_0)
        {
            this.repositoryItemTimeEdit_0 = new RepositoryItemTimeEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemTimeEdit_0, string_2, object_0);
            this.repositoryItemTimeEdit_0.AutoHeight = false;
            this.repositoryItemTimeEdit_0.BorderStyle = BorderStyles.NoBorder;
            this.repositoryItemTimeEdit_0.Mask.EditMask = "T";
            this.repositoryItemTimeEdit_0.Mask.MaskType = MaskType.DateTime;
            this.repositoryItemTimeEdit_0.Name = "repositoryItemTimeEdit";
        }

        public void AddTreeviewComBoBox(string string_2, object object_0, object object_1, bool bool_0)
        {
            this.repositoryItemTreeViewComboBoxEdit_0 = new RepositoryItemTreeViewComboBoxEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemTreeViewComboBoxEdit_0, string_2, object_0);
            if (object_1 is CodeDomainEx.CodeDomainEx)
            {
                CodeDomainEx.CodeDomainEx ex = object_1 as CodeDomainEx.CodeDomainEx;
                DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(DataProviderType.OleDb, ex.ConnectionStr);
                this.repositoryItemTreeViewComboBoxEdit_0.DataAccessLayerBaseClass = dataAccessLayer;
                this.repositoryItemTreeViewComboBoxEdit_0.TableName = ex.TableFieldName;
                this.repositoryItemTreeViewComboBoxEdit_0.CodeFieldName = ex.CodeFieldName;
                this.repositoryItemTreeViewComboBoxEdit_0.NameFieldName = ex.NameFieldName;
                this.repositoryItemTreeViewComboBoxEdit_0.ParentIDFieldName = ex.ParentIDFieldName;
                this.repositoryItemTreeViewComboBoxEdit_0.IDFieldName = ex.IDFieldName;
            }
            this.repositoryItemTreeViewComboBoxEdit_0.ReadOnly = bool_0;
        }

        public void AddTreeviewComBoBox(string string_2, object object_0, object object_1, bool bool_0, object object_2)
        {
            this.repositoryItemTreeViewComboBoxEdit_0 = new RepositoryItemTreeViewComboBoxEdit();
            this.gridEditorCollection_0.Add(this.repositoryItemTreeViewComboBoxEdit_0, string_2, object_0, object_2);
            if (object_1 is CodeDomainEx.CodeDomainEx)
            {
                CodeDomainEx.CodeDomainEx ex = object_1 as CodeDomainEx.CodeDomainEx;
                DataAccessLayerBaseClass dataAccessLayer = DataAccessLayerFactory.GetDataAccessLayer(DataProviderType.OleDb, ex.ConnectionStr);
                this.repositoryItemTreeViewComboBoxEdit_0.DataAccessLayerBaseClass = dataAccessLayer;
                this.repositoryItemTreeViewComboBoxEdit_0.TableName = ex.TableFieldName;
                this.repositoryItemTreeViewComboBoxEdit_0.CodeFieldName = ex.CodeFieldName;
                this.repositoryItemTreeViewComboBoxEdit_0.NameFieldName = ex.NameFieldName;
                this.repositoryItemTreeViewComboBoxEdit_0.ParentIDFieldName = ex.ParentIDFieldName;
                this.repositoryItemTreeViewComboBoxEdit_0.IDFieldName = ex.IDFieldName;
            }
            this.repositoryItemTreeViewComboBoxEdit_0.Name = "repositoryItemtREEVIEWComboBox";
            this.repositoryItemTreeViewComboBoxEdit_0.ReadOnly = bool_0;
        }

        public void ChangeItem(int int_0, ColumnAttribute columnAttribute_0, object object_0, double double_0, double double_1)
        {
            GridEditorItem item = this.gridEditorCollection_0[int_0];
            RepositoryItem repositoryItem = item.RepositoryItem;
            if (columnAttribute_0 == ColumnAttribute.CA_COMBOBOX)
            {
                this.repositoryItemComboBox_0 = new RepositoryItemComboBox();
                this.repositoryItemComboBox_0.AutoHeight = false;
                this.repositoryItemComboBox_0.BorderStyle = BorderStyles.NoBorder;
                for (int i = 0; i < ((IList) object_0).Count; i++)
                {
                    this.repositoryItemComboBox_0.Items.Add(((IList) object_0)[i]);
                }
                this.repositoryItemComboBox_0.Name = "repositoryItemComboBox";
                this.repositoryItemComboBox_0.ReadOnly = repositoryItem.ReadOnly;
                item.RepositoryItem = this.repositoryItemComboBox_0;
            }
            else if (repositoryItem is RepositoryItemComboBox)
            {
                if (columnAttribute_0 == ColumnAttribute.CA_TEXTEDIT)
                {
                    this.repositoryItemTextEdit_0 = new RepositoryItemTextEdit();
                    this.repositoryItemTextEdit_0.AutoHeight = false;
                    this.repositoryItemTextEdit_0.BorderStyle = BorderStyles.NoBorder;
                    this.repositoryItemTextEdit_0.Name = "repositoryItemTextEdit";
                    this.repositoryItemTextEdit_0.ReadOnly = repositoryItem.ReadOnly;
                    item.RepositoryItem = this.repositoryItemTextEdit_0;
                }
                else if (columnAttribute_0 == ColumnAttribute.CA_SPINEDIT)
                {
                    this.repositoryItemSpinEdit_0 = new RepositoryItemSpinEdit();
                    this.repositoryItemSpinEdit_0.AutoHeight = false;
                    this.repositoryItemSpinEdit_0.BorderStyle = BorderStyles.NoBorder;
                    this.repositoryItemSpinEdit_0.Mask.MaskType = MaskType.Numeric;
                    this.repositoryItemSpinEdit_0.Name = "repositoryItemSpinEdit";
                    this.repositoryItemSpinEdit_0.ReadOnly = repositoryItem.ReadOnly;
                    this.repositoryItemSpinEdit_0.MaxValue = (decimal) double_1;
                    this.repositoryItemSpinEdit_0.MinValue = (decimal) double_0;
                    item.RepositoryItem = this.repositoryItemSpinEdit_0;
                }
            }
        }

        public void Clear()
        {
            this.gridEditorCollection_0.Clear();
            this.gridControl_0.RefreshDataSource();
        }

        public void DeleteRow(object object_0)
        {
            this.gridEditorCollection_0.Remove(object_0);
            this.gridControl_0.RefreshDataSource();
        }

        private void gridView_0_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column == this.gridColumn_1)
            {
                GridEditorItem row = this.gridView_0.GetRow(e.RowHandle) as GridEditorItem;
                if (row != null)
                {
                    e.RepositoryItem = row.RepositoryItem;
                }
            }
        }

        private void gridView_0_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridEditorItem row = this.gridView_0.GetRow(e.FocusedRowHandle) as GridEditorItem;
        }

        private void method_0(object sender, EventArgs e)
        {
        }

        private void method_1()
        {
            this.gridView_0.Columns.AddRange(new GridColumn[] { this.gridColumn_0, this.gridColumn_1 });
            this.gridView_0.FocusedRowChanged += new FocusedRowChangedEventHandler(this.gridView_0_FocusedRowChanged);
            this.gridView_0.CustomRowCellEdit += new CustomRowCellEditEventHandler(this.gridView_0_CustomRowCellEdit);
            if ((this.string_1 == null) || (this.string_1.Trim() == ""))
            {
                this.string_1 = "字段";
            }
            this.gridColumn_0.Caption = this.string_1;
            this.gridColumn_0.FieldName = "Name";
            this.gridColumn_0.Name = "gridEditorName";
            this.gridColumn_0.OptionsColumn.AllowEdit = false;
            this.gridColumn_0.OptionsColumn.AllowGroup = DefaultBoolean.False;
            this.gridColumn_0.OptionsColumn.ReadOnly = true;
            this.gridColumn_0.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn_0.OptionsFilter.AllowFilter = false;
            this.gridColumn_0.Visible = true;
            this.gridColumn_0.VisibleIndex = 0;
            this.gridColumn_0.Width = 150;
            if ((this.string_0 == null) || (this.string_0.Trim() == ""))
            {
                this.string_0 = "值";
            }
            this.gridColumn_1.Caption = this.string_0;
            this.gridColumn_1.FieldName = "Value";
            this.gridColumn_1.Name = "gridEditorValue";
            this.gridColumn_1.OptionsColumn.AllowGroup = DefaultBoolean.False;
            this.gridColumn_1.OptionsColumn.AllowSort = DefaultBoolean.False;
            this.gridColumn_1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn_1.OptionsFilter.AllowFilter = false;
            this.gridColumn_1.Visible = true;
            this.gridColumn_1.VisibleIndex = 1;
            this.gridColumn_1.Width = 0xea;
        }

        public string EditorName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
                if (this.gridView_0.Columns.Count > 0)
                {
                    this.gridView_0.Columns[0].Caption = value;
                }
            }
        }

        public string EditorValue
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
                if (this.gridView_0.Columns.Count > 1)
                {
                    this.gridView_0.Columns[1].Caption = value;
                }
            }
        }
    }
}

