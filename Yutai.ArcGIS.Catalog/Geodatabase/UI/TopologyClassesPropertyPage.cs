using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.BaseClasses;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class TopologyClassesPropertyPage : UserControl, IPropertyPage, IPropertyPageEvents
    {
        private bool bool_0 = false;
        private bool bool_1 = false;
        private Container container_0 = null;
        private IList ilist_0 = new ArrayList();
        private ITopology itopology_0 = null;
        private string string_0 = "要素类";

        public event OnValueChangeEventHandler OnValueChange;

        public TopologyClassesPropertyPage()
        {
            this.InitializeComponent();
            this.vertXtraGrid_0 = new VertXtraGrid(this.gridControl1);
            this.gridView1.CellValueChanged += new CellValueChangedEventHandler(this.gridView1_CellValueChanged);
        }

        public void Apply()
        {
            Exception exception;
            try
            {
                int num;
                this.bool_1 = false;
                for (num = 0; num < this.ilist_0.Count; num++)
                {
                    this.itopology_0.RemoveClass(this.ilist_0[num] as IClass);
                }
                this.ilist_0.Clear();
                for (num = 0; num < this.gridView1.RowCount; num++)
                {
                    GridEditorItem row = this.gridView1.GetRow(num) as GridEditorItem;
                    ITopologyClass topoClass = (row.Tag as Class6).TopoClass;
                    int xYRank = (int) row.Value;
                    try
                    {
                        if ((row.Tag as Class6).IsNew)
                        {
                            this.itopology_0.AddClass(topoClass as IClass, 1.0, xYRank, 1, false);
                            (row.Tag as Class6).IsNew = false;
                        }
                        else if (topoClass.XYRank != xYRank)
                        {
                            this.itopology_0.RemoveClass(topoClass as IClass);
                            int zRank = topoClass.ZRank;
                            double weight = topoClass.Weight;
                            bool eventNotificationOnValidate = topoClass.EventNotificationOnValidate;
                            this.itopology_0.AddClass(topoClass as IClass, weight, xYRank, zRank, eventNotificationOnValidate);
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        Logger.Current.Error("",exception, "");
                    }
                }
            }
            catch (Exception exception2)
            {
                exception = exception2;
                Logger.Current.Error("",exception, "");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddClass class2 = new frmAddClass {
                Topology = this.itopology_0
            };
            if (class2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int num2;
                    IList list = class2.List;
                    int num = (int) this.txtValue.Value;
                    IList list2 = new ArrayList();
                    for (num2 = 1; num2 <= num; num2++)
                    {
                        list2.Add(num2);
                    }
                    for (num2 = 0; num2 < list.Count; num2++)
                    {
                        ITopologyClass class3 = list[num2] as ITopologyClass;
                        this.vertXtraGrid_0.AddComBoBox((class3 as IDataset).Name, 1, list2, false, new Class6(class3, true));
                        TopologyEditHelper.m_pList.Add(class3);
                    }
                    this.bool_1 = true;
                    this.gridControl1.MainView.RefreshData();
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("",exception, "");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView1.SelectedRowsCount != 0)
                {
                    int[] selectedRows = this.gridView1.GetSelectedRows();
                    for (int i = 0; i < selectedRows.Length; i++)
                    {
                        GridEditorItem row = this.gridView1.GetRow(selectedRows[i]) as GridEditorItem;
                        if (!(row.Tag as Class6).IsNew)
                        {
                            this.ilist_0.Add((row.Tag as Class6).TopoClass);
                        }
                        int index = 0;
                        while (index < TopologyEditHelper.m_pList.Count)
                        {
                            if (TopologyEditHelper.m_pList.get_Element(index) == (row.Tag as Class6).TopoClass)
                            {
                                goto Label_00A9;
                            }
                            index++;
                        }
                        goto Label_00D3;
                    Label_00A9:
                        TopologyEditHelper.m_pList.Remove(index);
                        TopologyEditHelper.OnDeleteTopolyClass(((row.Tag as Class6).TopoClass as IObjectClass).ObjectClassID);
                    Label_00D3:
                        this.vertXtraGrid_0.DeleteRow(row);
                    }
                    this.bool_1 = true;
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridView1.RowCount != 0)
                {
                    for (int i = 0; i < this.gridView1.RowCount; i++)
                    {
                        GridEditorItem row = this.gridView1.GetRow(i) as GridEditorItem;
                        if (!(row.Tag as Class6).IsNew)
                        {
                            this.ilist_0.Add((row.Tag as Class6).TopoClass);
                        }
                    }
                    if (this.OnValueChange != null)
                    {
                        this.OnValueChange();
                    }
                    this.bool_1 = true;
                    this.vertXtraGrid_0.Clear();
                    TopologyEditHelper.m_pList.RemoveAll();
                    TopologyEditHelper.OnDeleteAllTopolyClass();
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
        }

        public void Cancel()
        {
        }

 private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (this.bool_0 && (this.OnValueChange != null))
            {
                this.OnValueChange();
            }
        }

        private void gridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridView1.SelectedRowsCount > 0)
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                this.btnDelete.Enabled = false;
            }
        }

 public void ResetControl()
        {
        }

        public void SetObjects(object object_0)
        {
            this.itopology_0 = object_0 as ITopology;
        }

        private void TopologyClassesPropertyPage_Load(object sender, EventArgs e)
        {
            try
            {
                int num2;
                TopologyEditHelper.m_pList.RemoveAll();
                this.vertXtraGrid_0.EditorName = "要素类";
                this.vertXtraGrid_0.EditorValue = "优先级";
                int num = 1;
                IEnumFeatureClass classes = (this.itopology_0 as IFeatureClassContainer).Classes;
                classes.Reset();
                ITopologyClass class3 = classes.Next() as ITopologyClass;
                IList list = new ArrayList();
                for (num2 = 1; num2 <= num; num2++)
                {
                    list.Add(num2);
                }
                while (class3 != null)
                {
                    if (class3.IsInTopology)
                    {
                        num = (num < class3.XYRank) ? class3.XYRank : num;
                        this.vertXtraGrid_0.AddComBoBox((class3 as IDataset).Name, class3.XYRank, list, false, new Class6(class3, false));
                        TopologyEditHelper.m_pList.Add(class3);
                    }
                    class3 = classes.Next() as ITopologyClass;
                }
                this.txtValue.Value = num;
                list.Clear();
                for (num2 = 1; num2 <= num; num2++)
                {
                    list.Add(num2);
                }
                for (num2 = 0; num2 < this.gridView1.RowCount; num2++)
                {
                    this.vertXtraGrid_0.ChangeItem(num2, ColumnAttribute.CA_COMBOBOX, list, 0.0, 0.0);
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
            this.bool_0 = true;
        }

        private void txtValue_EditValueChanged(object sender, EventArgs e)
        {
            if (this.bool_0)
            {
                int num2;
                int num = (int) this.txtValue.Value;
                IList list = new ArrayList();
                for (num2 = 1; num2 <= num; num2++)
                {
                    list.Add(num2);
                }
                for (num2 = 0; num2 < this.gridView1.RowCount; num2++)
                {
                    this.vertXtraGrid_0.ChangeItem(num2, ColumnAttribute.CA_COMBOBOX, list, 0.0, 0.0);
                    GridEditorItem row = this.gridView1.GetRow(num2) as GridEditorItem;
                    int num3 = (int) row.Value;
                    if (num3 > num)
                    {
                        row.Value = num;
                    }
                }
            }
        }

        public bool IsPageDirty
        {
            get
            {
                return this.bool_1;
            }
        }

        int IPropertyPage.Height
        {
            get
            {
                return base.Height;
            }
        }

        int IPropertyPage.Width
        {
            get
            {
                return base.Width;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        private partial class Class6
        {
            private bool bool_0 = false;
            private ITopologyClass itopologyClass_0 = null;

            public Class6(ITopologyClass itopologyClass_1, bool bool_1)
            {
                this.itopologyClass_0 = itopologyClass_1;
                this.bool_0 = bool_1;
            }

            public bool IsNew
            {
                get
                {
                    return this.bool_0;
                }
                set
                {
                    this.bool_0 = value;
                }
            }

            public ITopologyClass TopoClass
            {
                get
                {
                    return this.itopologyClass_0;
                }
            }
        }
    }
}

