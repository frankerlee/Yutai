using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    internal partial class frmAddValues : Form
    {
        private ArrayList arrayList_0 = null;
        private bool bool_0 = false;
        private Container container_0 = null;
        private ILayer ilayer_0 = null;
        private IList ilist_0 = new ArrayList();
        private IUniqueValueRenderer iuniqueValueRenderer_0 = null;
        private string string_0 = "";

        public frmAddValues()
        {
            this.InitializeComponent();
        }

        private void btnAddNewValue_Click(object sender, EventArgs e)
        {
            if (this.txtNewValue.Text.Length > 0)
            {
                this.arrayList_0.Sort();
                object tag = this.txtNewValue.Tag;
                if (this.ValuelistBoxControl.Items.IndexOf(this.txtNewValue.Text) <= -1)
                {
                    try
                    {
                        if (this.iuniqueValueRenderer_0.get_Symbol(this.txtNewValue.Text) != null)
                        {
                            return;
                        }
                    }
                    catch
                    {
                    }
                    ITable displayTable = null;
                    if (this.ilayer_0 is IDisplayTable)
                    {
                        displayTable = (this.ilayer_0 as IDisplayTable).DisplayTable;
                    }
                    else
                    {
                        IAttributeTable table2 = this.ilayer_0 as IAttributeTable;
                        if (table2 != null)
                        {
                            displayTable = table2.AttributeTable;
                        }
                        else
                        {
                            displayTable = this.ilayer_0 as ITable;
                        }
                    }
                    if (displayTable != null)
                    {
                        int index = displayTable.FindField(this.string_0);
                        switch (displayTable.Fields.get_Field(index).Type)
                        {
                            case esriFieldType.esriFieldTypeDouble:
                            case esriFieldType.esriFieldTypeInteger:
                            case esriFieldType.esriFieldTypeSingle:
                            case esriFieldType.esriFieldTypeSmallInteger:
                                try
                                {
                                    double.Parse(this.txtNewValue.Text);
                                }
                                catch
                                {
                                    MessageBox.Show("请输入数字类型数据！");
                                    return;
                                }
                                break;
                        }
                        this.arrayList_0.Add(this.txtNewValue.Text);
                        this.ValuelistBoxControl.Items.Add(this.txtNewValue.Text);
                        this.txtNewValue.Text = null;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnGetAllValues_Click(object sender, EventArgs e)
        {
            this.bool_0 = true;
            this.btnGetAllValues.Enabled = false;
            this.arrayList_0.Clear();
            this.GetUniqueValues(this.ilayer_0, this.string_0, this.arrayList_0);
            this.ValuelistBoxControl.Items.Clear();
            for (int i = 0; i < this.arrayList_0.Count; i++)
            {
                this.ValuelistBoxControl.Items.Add(this.arrayList_0[i]);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int num;
            this.ilist_0.Clear();
            for (num = 0; num < this.ValuelistBoxControl.SelectedIndices.Count; num++)
            {
                this.ilist_0.Add(this.ValuelistBoxControl.Items[this.ValuelistBoxControl.SelectedIndices[num]]);
            }
            for (num = this.ValuelistBoxControl.SelectedIndices.Count - 1; num >= 0; num--)
            {
                this.arrayList_0.RemoveAt(this.ValuelistBoxControl.SelectedIndices[num]);
            }
        }

        private void frmAddValues_Load(object sender, EventArgs e)
        {
            if (this.arrayList_0 != null)
            {
                for (int i = 0; i < this.arrayList_0.Count; i++)
                {
                    this.ValuelistBoxControl.Items.Add(this.arrayList_0[i]);
                }
            }
            if (this.bool_0)
            {
                this.btnGetAllValues.Enabled = false;
            }
            else
            {
                this.btnGetAllValues.Enabled = true;
            }
        }

        public void GetUniqueValues(ILayer ilayer_1, string string_1, IList ilist_1)
        {
            try
            {
                IAttributeTable table = ilayer_1 as IAttributeTable;
                if (table != null)
                {
                    ITable attributeTable = table.AttributeTable;
                    IQueryFilter queryFilter = new QueryFilterClass
                    {
                        WhereClause = "1=1"
                    };
                    (queryFilter as IQueryFilterDefinition).PostfixClause = "Order by " + string_1;
                    ICursor cursor = attributeTable.Search(queryFilter, false);
                    IDataStatistics statistics = new DataStatisticsClass
                    {
                        Field = string_1,
                        Cursor = cursor
                    };
                    IEnumerator uniqueValues = statistics.UniqueValues;
                    uniqueValues.Reset();
                    while (uniqueValues.MoveNext())
                    {
                        this.arrayList_0.Add(uniqueValues.Current);
                    }
                    this.arrayList_0.Sort();
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
        }

        private void method_0(object sender, EventArgs e)
        {
            if (this.ValuelistBoxControl.SelectedIndices.Count > 0)
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void ValuelistBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = true;
        }

        public string FieldName
        {
            set { this.string_0 = value; }
        }

        public bool GetAllValues
        {
            get { return this.bool_0; }
            set { this.bool_0 = value; }
        }

        public ILayer Layer
        {
            set { this.ilayer_0 = value; }
        }

        public ArrayList List
        {
            set { this.arrayList_0 = value; }
        }

        public IList SelectedItems
        {
            get { return this.ilist_0; }
        }

        public IUniqueValueRenderer UniqueValueRenderer
        {
            set { this.iuniqueValueRenderer_0 = value; }
        }
    }
}