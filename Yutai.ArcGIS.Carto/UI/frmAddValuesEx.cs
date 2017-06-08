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
    internal class frmAddValuesEx : Form
    {
        private ArrayList arrayList_0 = null;
        private ArrayList arrayList_1 = null;
        private bool bool_0 = false;
        private SimpleButton btnAddNewValue;
        private SimpleButton btnCancel;
        private SimpleButton btnGetAllValues;
        private SimpleButton btnOK;
        private Container container_0 = null;
        private GroupBox groupBox1;
        private ILayer ilayer_0 = null;
        private IList ilist_0 = new ArrayList();
        private IUniqueValueRenderer iuniqueValueRenderer_0 = null;
        private Label label1;
        private string[] string_0 = null;
        private string string_1 = "";
        private TextEdit txtNewValue;
        private ListBoxControl ValuelistBoxControl;

        public frmAddValuesEx()
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
                    this.arrayList_0.Add(this.txtNewValue.Text);
                    this.ValuelistBoxControl.Items.Add(this.txtNewValue.Text);
                    this.txtNewValue.Text = null;
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
            if (this.arrayList_1 != null)
            {
                this.arrayList_1.Clear();
            }
            if (this.string_0.Length == 1)
            {
                this.GetUniqueValues(this.ilayer_0, this.string_0[0], this.arrayList_0);
            }
            else if (this.string_0.Length > 1)
            {
                this.GetUniqueValues(this.ilayer_0, this.string_0, this.arrayList_0, this.arrayList_1);
            }
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

        protected override void Dispose(bool bool_1)
        {
            if (bool_1 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_1);
        }

        private void frmAddValuesEx_Load(object sender, EventArgs e)
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

        public void GetUniqueValues(ILayer ilayer_1, string string_2, IList ilist_1)
        {
            try
            {
                IAttributeTable table = ilayer_1 as IAttributeTable;
                if (table != null)
                {
                    ITable attributeTable = table.AttributeTable;
                    IQueryFilter queryFilter = new QueryFilterClass {
                        WhereClause = "1=1"
                    };
                    (queryFilter as IQueryFilterDefinition).PostfixClause = "Order by " + string_2;
                    ICursor cursor = attributeTable.Search(queryFilter, false);
                    IDataStatistics statistics = new DataStatisticsClass {
                        Field = string_2,
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

        public bool GetUniqueValues(ILayer ilayer_1, string[] string_2, IList ilist_1, IList ilist_2)
        {
            try
            {
                IAttributeTable table = ilayer_1 as IAttributeTable;
                ITable attributeTable = null;
                if (table != null)
                {
                    attributeTable = table.AttributeTable;
                }
                else
                {
                    attributeTable = ilayer_1 as ITable;
                }
                if (attributeTable == null)
                {
                    return false;
                }
                int[] numArray = new int[string_2.Length];
                string str = "";
                int index = 0;
                while (index < string_2.Length)
                {
                    numArray[index] = attributeTable.Fields.FindField(string_2[index]);
                    if (index == 0)
                    {
                        str = string_2[index];
                    }
                    else
                    {
                        str = str + "," + string_2[index];
                    }
                    index++;
                }
                IQueryFilter queryFilter = new QueryFilterClass {
                    WhereClause = "1=1"
                };
                (queryFilter as IQueryFilterDefinition).PostfixClause = "Order by " + str;
                ICursor cursor = attributeTable.Search(queryFilter, false);
                IRow row = cursor.NextRow();
                ilist_1.Clear();
                while (row != null)
                {
                    string str2 = "";
                    for (index = 0; index < numArray.Length; index++)
                    {
                        object obj2 = row.get_Value(numArray[index]);
                        if (obj2 is DBNull)
                        {
                            if (index == 0)
                            {
                                str2 = "";
                            }
                            else
                            {
                                str2 = str2 + this.string_1;
                            }
                        }
                        else if (index == 0)
                        {
                            str2 = obj2.ToString();
                        }
                        else
                        {
                            str2 = str2 + this.string_1 + obj2.ToString();
                        }
                    }
                    int num2 = ilist_1.IndexOf(str2);
                    if (num2 != -1)
                    {
                        if (ilist_2 != null)
                        {
                            int num3 = (int) ilist_2[num2];
                            num3++;
                            ilist_2[num2] = num3;
                        }
                    }
                    else
                    {
                        ilist_1.Add(str2);
                        if (ilist_2 != null)
                        {
                            ilist_2.Add(1);
                        }
                    }
                    row = cursor.NextRow();
                }
                (ilist_1 as ArrayList).Sort();
                return true;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            return false;
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.btnGetAllValues = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.btnAddNewValue = new SimpleButton();
            this.txtNewValue = new TextEdit();
            this.btnOK = new SimpleButton();
            this.btnCancel = new SimpleButton();
            this.ValuelistBoxControl = new ListBoxControl();
            this.groupBox1.SuspendLayout();
            this.txtNewValue.Properties.BeginInit();
            ((ISupportInitialize) this.ValuelistBoxControl).BeginInit();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4f, 0x11);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择增加的值";
            this.btnGetAllValues.Location = new Point(0x18, 0xb0);
            this.btnGetAllValues.Name = "btnGetAllValues";
            this.btnGetAllValues.Size = new Size(0x70, 0x18);
            this.btnGetAllValues.TabIndex = 2;
            this.btnGetAllValues.Text = "完成列表";
            this.btnGetAllValues.Click += new EventHandler(this.btnGetAllValues_Click);
            this.groupBox1.Controls.Add(this.btnAddNewValue);
            this.groupBox1.Controls.Add(this.txtNewValue);
            this.groupBox1.Location = new Point(8, 0xd8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0xe8, 0x40);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新值";
            this.btnAddNewValue.Location = new Point(0x98, 0x18);
            this.btnAddNewValue.Name = "btnAddNewValue";
            this.btnAddNewValue.Size = new Size(0x48, 0x18);
            this.btnAddNewValue.TabIndex = 1;
            this.btnAddNewValue.Text = "添加到列表";
            this.btnAddNewValue.Click += new EventHandler(this.btnAddNewValue_Click);
            this.txtNewValue.EditValue = "";
            this.txtNewValue.Location = new Point(0x10, 0x18);
            this.txtNewValue.Name = "txtNewValue";
            this.txtNewValue.Size = new Size(120, 0x17);
            this.txtNewValue.TabIndex = 0;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new Point(0xa8, 40);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x38, 0x18);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xa8, 0x48);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x38, 0x18);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.ValuelistBoxControl.ItemHeight = 15;
            this.ValuelistBoxControl.Location = new Point(0x10, 40);
            this.ValuelistBoxControl.Name = "ValuelistBoxControl";
            this.ValuelistBoxControl.SelectionMode = SelectionMode.MultiExtended;
            this.ValuelistBoxControl.Size = new Size(0x88, 120);
            this.ValuelistBoxControl.SortOrder = SortOrder.Ascending;
            this.ValuelistBoxControl.TabIndex = 6;
            this.ValuelistBoxControl.SelectedIndexChanged += new EventHandler(this.ValuelistBoxControl_SelectedIndexChanged);
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0xf8, 0x125);
            base.Controls.Add(this.ValuelistBoxControl);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnGetAllValues);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAddValuesEx";
            this.Text = "新值";
            base.Load += new EventHandler(this.frmAddValuesEx_Load);
            this.groupBox1.ResumeLayout(false);
            this.txtNewValue.Properties.EndInit();
            ((ISupportInitialize) this.ValuelistBoxControl).EndInit();
            base.ResumeLayout(false);
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

        public ArrayList CountList
        {
            set
            {
                this.arrayList_1 = value;
            }
        }

        public string FieldDelimiter
        {
            set
            {
                this.string_1 = value;
            }
        }

        public string[] FieldNames
        {
            set
            {
                this.string_0 = value;
            }
        }

        public bool GetAllValues
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

        public ILayer Layer
        {
            set
            {
                this.ilayer_0 = value;
            }
        }

        public ArrayList List
        {
            set
            {
                this.arrayList_0 = value;
            }
        }

        public IList SelectedItems
        {
            get
            {
                return this.ilist_0;
            }
        }

        public IUniqueValueRenderer UniqueValueRenderer
        {
            set
            {
                this.iuniqueValueRenderer_0 = value;
            }
        }
    }
}

