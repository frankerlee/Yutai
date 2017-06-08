namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog;
    using JLK.Catalog.UI;
    using JLK.ControlExtend;
    using JLK.Editors;
    using JLK.Utility;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class ObjectFieldsPage : UserControl
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private bool bool_2;
        private SimpleButton btnImportStruct;
        private Container container_0;
        private enumUseType enumUseType_0 = enumUseType.enumUTFeatureClass;
        private esriFieldType[] esriFieldType_0;
        private esriFieldType[] esriFieldType_1;
        private esriFieldType[] esriFieldType_2;
        private GroupBox groupBox1;
        private IControlBaseInterface[] icontrolBaseInterface_0;
        private IField ifield_0 = null;
        private IFields ifields_0;
        private IFieldsEdit ifieldsEdit_0 = new FieldsClass();
        private IFieldsEdit ifieldsEdit_1 = new FieldsClass();
        private ITable itable_0 = null;
        private IWorkspace iworkspace_0;
        private EditListView listView2;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private string string_0 = "SHAPE";
        private string[] string_1 = new string[] { "短整形", "长整形", "单精度", "双精度", "字符串", "日期型", "Blob", "Guid", "几何对象", "Raster" };
        private string[] string_2 = new string[] { "短整形", "长整形", "单精度", "双精度", "字符串", "日期型", "Blob", "Guid", "Raster" };
        private string[] string_3;
        private string[] string_4;

        public event JLK.Geodatabase.UI.ValueChangedHandler ValueChanged;

        public ObjectFieldsPage()
        {
            esriFieldType[] typeArray = new esriFieldType[10];
            typeArray[1] = esriFieldType.esriFieldTypeInteger;
            typeArray[2] = esriFieldType.esriFieldTypeSingle;
            typeArray[3] = esriFieldType.esriFieldTypeDouble;
            typeArray[4] = esriFieldType.esriFieldTypeString;
            typeArray[5] = esriFieldType.esriFieldTypeDate;
            typeArray[6] = esriFieldType.esriFieldTypeBlob;
            typeArray[7] = esriFieldType.esriFieldTypeGUID;
            typeArray[8] = esriFieldType.esriFieldTypeGeometry;
            typeArray[9] = esriFieldType.esriFieldTypeRaster;
            this.esriFieldType_0 = typeArray;
            typeArray = new esriFieldType[9];
            typeArray[1] = esriFieldType.esriFieldTypeInteger;
            typeArray[2] = esriFieldType.esriFieldTypeSingle;
            typeArray[3] = esriFieldType.esriFieldTypeDouble;
            typeArray[4] = esriFieldType.esriFieldTypeString;
            typeArray[5] = esriFieldType.esriFieldTypeDate;
            typeArray[6] = esriFieldType.esriFieldTypeBlob;
            typeArray[7] = esriFieldType.esriFieldTypeGUID;
            typeArray[8] = esriFieldType.esriFieldTypeRaster;
            this.esriFieldType_1 = typeArray;
            this.string_3 = new string[] { "短整形", "长整形", "单精度", "双精度", "字符串", "日期型", "几何对象" };
            typeArray = new esriFieldType[7];
            typeArray[1] = esriFieldType.esriFieldTypeInteger;
            typeArray[2] = esriFieldType.esriFieldTypeSingle;
            typeArray[3] = esriFieldType.esriFieldTypeDouble;
            typeArray[4] = esriFieldType.esriFieldTypeString;
            typeArray[5] = esriFieldType.esriFieldTypeDate;
            typeArray[6] = esriFieldType.esriFieldTypeGeometry;
            this.esriFieldType_2 = typeArray;
            this.icontrolBaseInterface_0 = new IControlBaseInterface[] { new FieldTypeIntegerCtrl(), new FieldTypeIntegerCtrl(), new FieldTypeDoubleCtrl(), new FieldTypeDoubleCtrl(), new FieldTypeTextCtrl(), new FieldTypeDateCtrl(), new FieldTypeObjectIDCtrl(), new FieldTypeGeometryCtrl1(), new FieldTypeBlobCtrl(), new FieldTypeRasterCtrl(), new FieldTypeGuidCtrl() };
            this.string_4 = new string[] { "短整形", "长整形", "单精度", "双精度", "字符串", "日期型", "OBJECT ID", "几何对象", "Blob", "Raster", "Guid" };
            this.container_0 = null;
            this.bool_2 = false;
            this.iworkspace_0 = null;
            this.InitializeComponent();
        }

        public bool Apply()
        {
            int num;
            if (this.itable_0 != null)
            {
                Exception exception;
                try
                {
                    try
                    {
                        IField field;
                        for (num = 0; num < this.ifieldsEdit_1.FieldCount; num++)
                        {
                            IFields fields = this.itable_0.Fields;
                            field = this.ifieldsEdit_1.get_Field(num);
                            int index = this.itable_0.FindField(field.Name);
                            if (index != -1)
                            {
                                this.itable_0.DeleteField(fields.get_Field(index));
                            }
                        }
                        for (num = 0; num < this.ifieldsEdit_0.FieldCount; num++)
                        {
                            this.itable_0.AddField(this.ifieldsEdit_0.get_Field(num));
                        }
                        if (!ObjectClassShareData.m_IsShapeFile)
                        {
                            IClassSchemaEdit edit = this.itable_0 as IClassSchemaEdit;
                            num = 0;
                            while (num < this.ifields_0.FieldCount)
                            {
                                field = this.ifields_0.get_Field(num);
                                edit.AlterFieldAliasName(field.Name, field.AliasName);
                                if (((field.Name != "SHAPE.area") && !(field.Name == "SHAPE.len")) && (((((field.Type == esriFieldType.esriFieldTypeDouble) || (field.Type == esriFieldType.esriFieldTypeInteger)) || ((field.Type == esriFieldType.esriFieldTypeSingle) || (field.Type == esriFieldType.esriFieldTypeSmallInteger))) || (field.Type == esriFieldType.esriFieldTypeDate)) || (field.Type == esriFieldType.esriFieldTypeString)))
                                {
                                    edit.AlterDomain(field.Name, field.Domain);
                                }
                                num++;
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        CErrorLog.writeErrorLog(this, exception, "");
                    }
                    this.ifieldsEdit_1.DeleteAllFields();
                    this.ifieldsEdit_0.DeleteAllFields();
                    this.ifields_0 = (this.itable_0.Fields as IClone).Clone() as IFields;
                    this.method_0(this.listView2, this.ifields_0);
                    this.listView2.LockRowCount = this.ifields_0.FieldCount;
                    goto Label_023C;
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    CErrorLog.writeErrorLog(this, exception, "");
                    MessageBox.Show(exception.Message);
                    return false;
                }
            }
            if (this.ifields_0 != null)
            {
                for (num = 0; num < this.ifieldsEdit_0.FieldCount; num++)
                {
                    (this.ifields_0 as IFieldsEdit).AddField(this.ifieldsEdit_0.get_Field(num));
                }
            }
        Label_023C:
            return true;
        }

        private void btnImportStruct_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile();
            file.AddFilter(new MyGxFilterTablesAndFeatureClasses(), true);
            file.AllowMultiSelect = false;
            if ((file.DoModalOpen() == DialogResult.OK) && (file.Items.Count != 0))
            {
                IGxDataset dataset = file.Items.get_Element(0) as IGxDataset;
                if (dataset != null)
                {
                    ITable table = dataset.Dataset as ITable;
                    if (table != null)
                    {
                        int num;
                        IField field;
                        for (num = this.ifields_0.FieldCount - 1; num > 1; num--)
                        {
                            field = this.ifields_0.get_Field(num);
                            if ((field.Type != esriFieldType.esriFieldTypeOID) && (field.Type != esriFieldType.esriFieldTypeGeometry))
                            {
                                (this.ifields_0 as IFieldsEdit).DeleteField(field);
                            }
                        }
                        IFields fields = table.Fields;
                        for (num = 0; num < fields.FieldCount; num++)
                        {
                            field = fields.get_Field(num);
                            if ((field.Type != esriFieldType.esriFieldTypeOID) && (field.Type != esriFieldType.esriFieldTypeGeometry))
                            {
                                (this.ifields_0 as IFieldsEdit).AddField(field);
                            }
                        }
                    }
                    this.method_0(this.listView2, this.ifields_0);
                    this.method_2(this.listView2.Items[0].Tag as IField, true);
                }
            }
        }

        protected override void Dispose(bool bool_3)
        {
            if (bool_3 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_3);
        }

        public void InitControl()
        {
            if (this.bool_2)
            {
                this.method_0(this.listView2, this.ifields_0);
                this.method_2(this.listView2.Items[0].Tag as IField, true);
            }
        }

        private void InitializeComponent()
        {
            this.listView2 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            this.groupBox1 = new GroupBox();
            this.btnImportStruct = new SimpleButton();
            base.SuspendLayout();
            this.listView2.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.listView2.ComboBoxBgColor = Color.GhostWhite;
            this.listView2.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView2.EditBgColor = Color.GhostWhite;
            this.listView2.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(8, 8);
            this.listView2.LockRowCount = 0;
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(0x150, 0xb8);
            this.listView2.TabIndex = 0;
            this.listView2.View = View.Details;
            this.listView2.KeyDown += new KeyEventHandler(this.listView2_KeyDown);
            this.listView2.KeyPress += new KeyPressEventHandler(this.listView2_KeyPress);
            this.listView2.ValueChanged += new JLK.ControlExtend.ValueChangedHandler(this.method_3);
            this.listView2.RowDelete += new RowDeleteHandler(this.method_5);
            this.listView2.Click += new EventHandler(this.listView2_Click);
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_0.Text = "字段名";
            this.lvcolumnHeader_0.Width = 0xcb;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.ComboBox;
            this.lvcolumnHeader_1.Text = "数据类型";
            this.lvcolumnHeader_1.Width = 0x7e;
            this.groupBox1.Location = new System.Drawing.Point(8, 200);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 0xf8);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段属性";
            this.btnImportStruct.Location = new System.Drawing.Point(0x120, 0x1a8);
            this.btnImportStruct.Name = "btnImportStruct";
            this.btnImportStruct.Size = new Size(0x38, 0x18);
            this.btnImportStruct.TabIndex = 2;
            this.btnImportStruct.Text = "导入...";
            this.btnImportStruct.Click += new EventHandler(this.btnImportStruct_Click);
            base.Controls.Add(this.btnImportStruct);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listView2);
            base.Name = "ObjectFieldsPage";
            base.Size = new Size(0x170, 0x1d0);
            base.Load += new EventHandler(this.ObjectFieldsPage_Load);
            base.ResumeLayout(false);
        }

        private void listView2_Click(object sender, EventArgs e)
        {
        }

        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void listView2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                this.groupBox1.Controls.Clear();
                if (this.listView2.SelectedItems[0].Tag != null)
                {
                    IField tag = this.listView2.SelectedItems[0].Tag as IField;
                    this.icontrolBaseInterface_0[(int) tag.Type].Filed = tag;
                    if (this.ifieldsEdit_0.FindField(tag.Name) != -1)
                    {
                        this.icontrolBaseInterface_0[(int) tag.Type].IsEdit = false;
                    }
                    else
                    {
                        this.icontrolBaseInterface_0[(int) tag.Type].IsEdit = this.bool_1;
                    }
                    UserControl control = this.icontrolBaseInterface_0[(int) tag.Type] as UserControl;
                    this.icontrolBaseInterface_0[(int) tag.Type].Workspace = this.iworkspace_0;
                    control.Parent = this.groupBox1;
                    control.Dock = DockStyle.Left;
                    control.Show();
                    this.groupBox1.Controls.Add(control);
                }
            }
        }

        private void method_0(EditListView editListView_0, IFields ifields_1)
        {
            int num;
            ListViewItem item;
            editListView_0.Items.Clear();
            string[] items = new string[2];
            for (num = 0; num < ifields_1.FieldCount; num++)
            {
                IField field = ifields_1.get_Field(num);
                items[0] = field.Name;
                items[1] = this.string_4[(int) field.Type];
                item = new ListViewItem(items) {
                    Tag = field
                };
                editListView_0.Items.Add(item);
            }
            items[0] = "";
            items[1] = "";
            for (num = 0; num < 1; num++)
            {
                item = new ListViewItem(items);
                editListView_0.Items.Add(item);
            }
        }

        private void method_1(int int_0, IFieldsEdit ifieldsEdit_2)
        {
            IFieldEdit field = new FieldClass {
                Name = "OBJECTID",
                AliasName = "OBJECTID",
                IsNullable = false,
                Type = esriFieldType.esriFieldTypeOID
            };
            ifieldsEdit_2.AddField(field);
            if (int_0 == 1)
            {
                field = new FieldClass {
                    Name = "SHAPE",
                    AliasName = "SHAPE",
                    IsNullable = true,
                    Type = esriFieldType.esriFieldTypeGeometry
                };
                IGeometryDefEdit edit2 = new GeometryDefClass {
                    SpatialReference = SpatialReferenctOperator.ConstructCoordinateSystem(this.iworkspace_0 as IGeodatabaseRelease),
                    GridCount = 1
                };
                edit2.set_GridSize(0, 1000.0);
                edit2.GeometryType = esriGeometryType.esriGeometryPolygon;
                edit2.HasZ = false;
                edit2.HasM = false;
                field.GeometryDef = edit2;
                ifieldsEdit_2.AddField(field);
            }
        }

        private void method_2(IField ifield_1, bool bool_3)
        {
            if (this.groupBox1.Controls.Count == 1)
            {
                this.groupBox1.Controls[0].Parent = null;
            }
            if (ifield_1 != null)
            {
                this.icontrolBaseInterface_0[(int) ifield_1.Type].Filed = ifield_1;
                this.icontrolBaseInterface_0[(int) ifield_1.Type].IsEdit = bool_3;
                this.icontrolBaseInterface_0[(int) ifield_1.Type].Workspace = this.iworkspace_0;
                UserControl control = this.icontrolBaseInterface_0[(int) ifield_1.Type] as UserControl;
                control.Parent = this.groupBox1;
                control.Dock = DockStyle.Left;
                control.Show();
                this.groupBox1.Controls.Add(control);
            }
        }

        private void method_3(object sender, ValueChangedEventArgs e)
        {
            IFieldEdit tag;
            ListViewItem item = this.listView2.Items[e.Row];
            if (item.Tag == null)
            {
                if (item.SubItems[1].Text.Length == 0)
                {
                    item.SubItems[1].Text = "字符串";
                }
                if (item.SubItems[1].Text.Length > 0)
                {
                    tag = new FieldClass {
                        Name = item.SubItems[0].Text,
                        Type = this.method_4(item.SubItems[1].Text)
                    };
                    if (tag.Type == esriFieldType.esriFieldTypeString)
                    {
                        tag.Length = 50;
                    }
                    else if (tag.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        IGeometryDefEdit edit2 = new GeometryDefClass {
                            SpatialReference = new UnknownCoordinateSystemClass(),
                            GridCount = 1
                        };
                        edit2.set_GridSize(0, 1000.0);
                        edit2.GeometryType = esriGeometryType.esriGeometryPolygon;
                        edit2.HasZ = false;
                        edit2.HasM = false;
                        tag.GeometryDef = edit2;
                    }
                    this.ifieldsEdit_0.AddField(tag);
                    item.Tag = tag;
                    if (this.valueChangedHandler_0 != null)
                    {
                        this.valueChangedHandler_0(this, new EventArgs());
                    }
                    string[] items = new string[] { "", "" };
                    this.listView2.Items.Add(new ListViewItem(items));
                    if (item == this.listView2.SelectedItems[0])
                    {
                        this.method_2(tag, false);
                    }
                }
            }
            else
            {
                tag = item.Tag as IFieldEdit;
                tag.Name = item.SubItems[0].Text;
                esriFieldType type = this.method_4(item.SubItems[1].Text);
                if (tag.Type != type)
                {
                    tag.Type = type;
                    tag.Domain = null;
                    tag.DefaultValue = "";
                    IControlBaseInterface interface2 = this.icontrolBaseInterface_0[(int) tag.Type];
                    if (interface2 != null)
                    {
                        interface2.Init();
                    }
                }
            }
        }

        private esriFieldType method_4(string string_5)
        {
            int num;
            if (this.enumUseType_0 == enumUseType.enumUTFeatureClass)
            {
                if (ObjectClassShareData.m_IsShapeFile)
                {
                    for (num = 0; num < this.string_3.Length; num++)
                    {
                        if (this.string_3[num] == string_5)
                        {
                            return this.esriFieldType_2[num];
                        }
                    }
                }
                else
                {
                    num = 0;
                    while (num < this.string_1.Length)
                    {
                        if (this.string_1[num] == string_5)
                        {
                            return this.esriFieldType_0[num];
                        }
                        num++;
                    }
                }
            }
            else if (this.enumUseType_0 == enumUseType.enumUTObjectClass)
            {
                for (num = 0; num < this.string_2.Length; num++)
                {
                    if (this.string_2[num] == string_5)
                    {
                        return this.esriFieldType_1[num];
                    }
                }
            }
            return esriFieldType.esriFieldTypeInteger;
        }

        private void method_5(object sender, RowDeleteEventArgs e)
        {
            if (this.listView2.SelectedIndices.Count != 0)
            {
                if ((ObjectClassShareData.m_IsShapeFile && this.bool_1) && (this.ifields_0.FieldCount == 3))
                {
                    MessageBox.Show("除OID字段和Shape字段外，要素类或表至少包含一个字段!");
                }
                else
                {
                    IField tag = this.listView2.Items[e.Row].Tag as IField;
                    switch (tag.Type)
                    {
                        case esriFieldType.esriFieldTypeOID:
                            MessageBox.Show("不能删除该行", "错误");
                            return;

                        case esriFieldType.esriFieldTypeGeometry:
                            if (!(this.string_0 == tag.Name))
                            {
                                break;
                            }
                            MessageBox.Show("不能删除该行", "错误");
                            return;
                    }
                    if (!tag.Editable)
                    {
                        MessageBox.Show("不能删除该行", "错误");
                    }
                    else
                    {
                        if (this.ifieldsEdit_0.FindField(tag.Name) != -1)
                        {
                            this.ifieldsEdit_0.DeleteField(tag);
                        }
                        else
                        {
                            if (!this.bool_0)
                            {
                                MessageBox.Show("不能删除该行", "错误");
                                return;
                            }
                            (this.ifields_0 as IFieldsEdit).DeleteField(tag);
                            this.ifieldsEdit_1.AddField(tag);
                            this.listView2.LockRowCount--;
                        }
                        this.listView2.Items.RemoveAt(e.Row);
                        if (this.valueChangedHandler_0 != null)
                        {
                            this.valueChangedHandler_0(this, new EventArgs());
                        }
                    }
                }
            }
        }

        private void method_6(object sender, EventArgs e)
        {
            if (this.valueChangedHandler_0 != null)
            {
                this.valueChangedHandler_0(this, e);
            }
        }

        private void method_7(object sender, EventArgs e)
        {
            if (this.valueChangedHandler_0 != null)
            {
                this.valueChangedHandler_0(this, e);
            }
        }

        private void ObjectFieldsPage_Load(object sender, EventArgs e)
        {
            this.bool_2 = true;
            for (int i = 0; i < this.icontrolBaseInterface_0.Length; i++)
            {
                this.icontrolBaseInterface_0[i].Workspace = this.iworkspace_0;
            }
            (this.icontrolBaseInterface_0[0] as FieldTypeIntegerCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[1] as FieldTypeIntegerCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[2] as FieldTypeDoubleCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[3] as FieldTypeDoubleCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[4] as FieldTypeTextCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[5] as FieldTypeDateCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[6] as FieldTypeObjectIDCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[7] as FieldTypeGeometryCtrl1).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[8] as FieldTypeBlobCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[9] as FieldTypeRasterCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            (this.icontrolBaseInterface_0[10] as FieldTypeGuidCtrl).ValueChanged += new JLK.Geodatabase.UI.ValueChangedHandler(this.method_6);
            if (this.ifields_0 == null)
            {
                this.ifields_0 = new FieldsClass();
                this.method_1((int) this.enumUseType_0, (IFieldsEdit) this.ifields_0);
            }
            if (this.enumUseType_0 == enumUseType.enumUTFeatureClass)
            {
                if (ObjectClassShareData.m_IsShapeFile)
                {
                    this.listView2.BoundListToColumn(1, this.string_3);
                }
                else
                {
                    this.listView2.BoundListToColumn(1, this.string_1);
                }
            }
            else
            {
                this.listView2.BoundListToColumn(1, this.string_2);
            }
            this.listView2.LockRowCount = this.ifields_0.FieldCount;
            this.method_0(this.listView2, this.ifields_0);
            this.method_2(this.listView2.Items[0].Tag as IField, true);
        }

        public IFields Fields
        {
            get
            {
                return this.ifields_0;
            }
            set
            {
                this.ifields_0 = (value as IClone).Clone() as IFields;
            }
        }

        public bool IsEdit
        {
            set
            {
                this.bool_1 = value;
            }
        }

        public string ShapeFieldName
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

        public ITable Table
        {
            set
            {
                this.itable_0 = value;
            }
        }

        public enumUseType UseType
        {
            set
            {
                this.enumUseType_0 = value;
            }
        }

        public IWorkspace Workspace
        {
            set
            {
                this.iworkspace_0 = value;
            }
        }
    }
}

