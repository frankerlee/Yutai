using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common.CodeDomainEx;
using Yutai.ArcGIS.Common.ControlExtend;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public class NewObjectClassFieldsPage : UserControl
    {
        private bool bool_0 = true;
        private bool bool_1 = false;
        private bool bool_2 = false;
        private bool bool_3;
        private SimpleButton btnImportStruct;
        private enumUseType enumUseType_0 = enumUseType.enumUTFeatureClass;
        private esriFieldType[] esriFieldType_0;
        private esriFieldType[] esriFieldType_1;
        private esriFieldType[] esriFieldType_2;
        private GroupBox groupBox1;
        private IContainer icontainer_0 = null;
        private IControlBaseInterface[] icontrolBaseInterface_0;
        private IField ifield_0 = null;
        private IFields ifields_0;
        private IFieldsEdit ifieldsEdit_0 = new FieldsClass();
        private IFieldsEdit ifieldsEdit_1 = new FieldsClass();
        private IList<IField> ilist_0 = new List<IField>();
        private IList<FieldChangeType> ilist_1 = new List<FieldChangeType>();
        private ITable itable_0 = null;
        private IWorkspace iworkspace_0 = null;
        private EditListView listView2;
        private LVColumnHeader lvcolumnHeader_0;
        private LVColumnHeader lvcolumnHeader_1;
        private string string_0 = "SHAPE";
        private string[] string_1 = new string[] { "短整形", "长整形", "单精度", "双精度", "字符串", "日期型", "Blob", "Guid", "几何对象", "Raster" };
        private string[] string_2 = new string[] { "短整形", "长整形", "单精度", "双精度", "字符串", "日期型", "Blob", "Guid", "Raster" };
        private string[] string_3;
        private string[] string_4;

        public event ValueChangedHandler ValueChanged;

        public NewObjectClassFieldsPage()
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
            this.icontrolBaseInterface_0 = new IControlBaseInterface[] { new FieldTypeIntegerCtrl(), new FieldTypeIntegerCtrl(), new FieldTypeDoubleCtrl(), new FieldTypeDoubleCtrl(), new FieldTypeTextCtrl(), new FieldTypeDateCtrl(), new FieldTypeObjectIDCtrl(), new FieldTypeGeometryCtrl(), new FieldTypeBlobCtrl(), new FieldTypeRasterCtrl(), new FieldTypeGuidCtrl() };
            this.string_4 = new string[] { "短整形", "长整形", "单精度", "双精度", "字符串", "日期型", "OBJECT ID", "几何对象", "Blob", "Raster", "Guid" };
            this.bool_3 = false;
            this.InitializeComponent();
        }

        public bool Apply()
        {
            int num;
            if (!(!this.bool_2 || this.bool_3))
            {
                return true;
            }
            this.bool_3 = false;
            if (NewObjectClassHelper.m_pObjectClassHelper.ObjectClass != null)
            {
                COMException exception;
                Exception exception2;
                this.itable_0 = NewObjectClassHelper.m_pObjectClassHelper.ObjectClass as ITable;
                string name = (this.itable_0 as IDataset).Name;
                try
                {
                    IField field;
                   CodeDomainEx ex;
                    try
                    {
                        for (num = 0; num < this.ifieldsEdit_1.FieldCount; num++)
                        {
                            IFields fields = this.itable_0.Fields;
                            field = this.ifieldsEdit_1.get_Field(num);
                            int index = this.itable_0.FindField(field.Name);
                            if (index != -1)
                            {
                                this.itable_0.DeleteField(fields.get_Field(index));
                                if ((NewObjectClassHelper.m_pObjectClassHelper != null) && NewObjectClassHelper.m_pObjectClassHelper.FieldDomains.ContainsKey(field))
                                {
                                    ex = NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[field];
                                    if (ex != null)
                                    {
                                        CodeDomainManage.DeleteCodeDoaminMap(name, field.Name);
                                    }
                                }
                            }
                        }
                    }
                    catch (COMException exception1)
                    {
                        exception = exception1;
                        Logger.Current.Error("",exception, "");
                        MessageBox.Show(exception.Message);
                        return false;
                    }
                    catch (Exception exception3)
                    {
                        exception2 = exception3;
                        Logger.Current.Error("",exception2, "");
                    }
                    try
                    {
                        for (num = 0; num < this.ifieldsEdit_0.FieldCount; num++)
                        {
                            IField key = this.ifieldsEdit_0.get_Field(num);
                            this.itable_0.AddField(this.ifieldsEdit_0.get_Field(num));
                            if ((NewObjectClassHelper.m_pObjectClassHelper != null) && NewObjectClassHelper.m_pObjectClassHelper.FieldDomains.ContainsKey(key))
                            {
                                ex = NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[key];
                                if (ex != null)
                                {
                                    CodeDomainManage.AddFieldCodeDoaminMap(name, key.Name, ex.DomainID);
                                }
                            }
                        }
                    }
                    catch (COMException exception4)
                    {
                        exception = exception4;
                        if (exception.ErrorCode == -2147220969)
                        {
                            MessageBox.Show("不是对象的所有者，无法修改对象!");
                        }
                        else if (exception.ErrorCode == -2147219887)
                        {
                            MessageBox.Show("无法添加字段!");
                        }
                        else
                        {
                            Logger.Current.Error("",exception, "");
                            MessageBox.Show(exception.Message);
                        }
                    }
                    catch (Exception exception5)
                    {
                        exception2 = exception5;
                        Logger.Current.Error("",exception2, "");
                    }
                    if (!ObjectClassShareData.m_IsShapeFile)
                    {
                        IClassSchemaEdit edit = this.itable_0 as IClassSchemaEdit;
                        num = 0;
                        while (num < this.ilist_0.Count)
                        {
                            field = this.ilist_0[num];
                            try
                            {
                                if (((FieldChangeType) this.ilist_1[num]) == FieldChangeType.FCTAlias)
                                {
                                    edit.AlterFieldAliasName(field.Name, field.AliasName);
                                }
                                else if (((FieldChangeType) this.ilist_1[num]) == FieldChangeType.FCTDomain)
                                {
                                    edit.AlterDomain(field.Name, field.Domain);
                                    if ((NewObjectClassHelper.m_pObjectClassHelper != null) && NewObjectClassHelper.m_pObjectClassHelper.FieldDomains.ContainsKey(field))
                                    {
                                        ex = NewObjectClassHelper.m_pObjectClassHelper.FieldDomains[field];
                                        if (ex != null)
                                        {
                                            CodeDomainManage.AddFieldCodeDoaminMap(name, field.Name, ex.DomainID);
                                        }
                                        else
                                        {
                                            CodeDomainManage.DeleteCodeDoaminMap(name, field.Name);
                                        }
                                    }
                                }
                            }
                            catch (COMException exception6)
                            {
                                exception = exception6;
                                (field as IFieldEdit).AliasName_2 = field.Name;
                                if (exception.ErrorCode == -2147220969)
                                {
                                    MessageBox.Show("不是对象的所有者，无法修改字段[" + field.Name + "]!");
                                }
                                else if (exception.ErrorCode == -2147219887)
                                {
                                    MessageBox.Show("无法修改字段[" + field.Name + "]!");
                                }
                                else
                                {
                                    Logger.Current.Error("",exception, "");
                                    MessageBox.Show(exception.Message);
                                }
                                return false;
                            }
                            catch (Exception exception7)
                            {
                                exception2 = exception7;
                                Logger.Current.Error("",exception2, "");
                            }
                            num++;
                        }
                    }
                    if (!ObjectClassShareData.m_IsShapeFile)
                    {
                        (this.itable_0 as ISchemaLock).ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                    }
                    this.ifieldsEdit_1.DeleteAllFields();
                    this.ifieldsEdit_0.DeleteAllFields();
                    this.ilist_0.Clear();
                    this.ilist_1.Clear();
                    this.ifields_0 = (this.itable_0.Fields as IClone).Clone() as IFields;
                    this.method_2(this.listView2, this.ifields_0);
                    this.listView2.LockRowCount = this.ifields_0.FieldCount;
                    goto Label_0512;
                }
                catch (COMException exception8)
                {
                    exception = exception8;
                    if (exception.ErrorCode == -2147220969)
                    {
                        MessageBox.Show("不是对象的所有者，无法修改对象!");
                    }
                    else
                    {
                        Logger.Current.Error("",exception, "");
                        MessageBox.Show(exception.Message);
                    }
                    return false;
                }
                catch (Exception exception9)
                {
                    exception2 = exception9;
                    Logger.Current.Error("",exception2, "");
                    MessageBox.Show(exception2.Message);
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
        Label_0512:
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
                                if (this.bool_2)
                                {
                                    this.ifieldsEdit_1.AddField(field);
                                }
                            }
                        }
                        IFields fields = table.Fields;
                        for (num = 0; num < fields.FieldCount; num++)
                        {
                            field = fields.get_Field(num);
                            if ((field.Type != esriFieldType.esriFieldTypeOID) && (field.Type != esriFieldType.esriFieldTypeGeometry))
                            {
                                (this.ifields_0 as IFieldsEdit).AddField(field);
                                if (this.bool_2)
                                {
                                    this.ifieldsEdit_0.AddField(field);
                                }
                            }
                        }
                    }
                    this.method_2(this.listView2, this.ifields_0);
                    this.method_5(this.listView2.Items[0].Tag as IField, true);
                }
            }
        }

        protected override void Dispose(bool bool_4)
        {
            if (bool_4 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_4);
        }

        public void InitControl()
        {
            NewObjectClassHelper.m_pObjectClassHelper.InitFields();
            this.ifields_0 = NewObjectClassHelper.m_pObjectClassHelper.Fields;
            if (this.bool_1)
            {
                this.method_2(this.listView2, this.ifields_0);
                this.method_5(this.listView2.Items[0].Tag as IField, true);
            }
        }

        private void InitializeComponent()
        {
            this.btnImportStruct = new SimpleButton();
            this.groupBox1 = new GroupBox();
            this.listView2 = new EditListView();
            this.lvcolumnHeader_0 = new LVColumnHeader();
            this.lvcolumnHeader_1 = new LVColumnHeader();
            base.SuspendLayout();
            this.btnImportStruct.Location = new System.Drawing.Point(0x128, 0x187);
            this.btnImportStruct.Name = "btnImportStruct";
            this.btnImportStruct.Size = new Size(0x38, 0x18);
            this.btnImportStruct.TabIndex = 5;
            this.btnImportStruct.Text = "导入...";
            this.btnImportStruct.Click += new EventHandler(this.btnImportStruct_Click);
            this.groupBox1.Location = new System.Drawing.Point(0x10, 0xbf);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x110, 0xe0);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段属性";
            this.listView2.Columns.AddRange(new ColumnHeader[] { this.lvcolumnHeader_0, this.lvcolumnHeader_1 });
            this.listView2.ComboBoxBgColor = Color.GhostWhite;
            this.listView2.ComboBoxFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView2.EditBgColor = Color.GhostWhite;
            this.listView2.EditFont = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0x10, 13);
            this.listView2.LockRowCount = 0;
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new Size(0x150, 0xa7);
            this.listView2.TabIndex = 3;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = View.Details;
            this.listView2.SelectedIndexChanged += new EventHandler(this.listView2_SelectedIndexChanged);
            this.listView2.ValueChanged += new Common.ControlExtend.ValueChangedHandler(this.method_6);
            this.listView2.RowDelete += new RowDeleteHandler(this.method_7);
            this.lvcolumnHeader_0.ColumnStyle = ListViewColumnStyle.EditBox;
            this.lvcolumnHeader_0.Text = "字段名";
            this.lvcolumnHeader_0.Width = 0xcb;
            this.lvcolumnHeader_1.ColumnStyle = ListViewColumnStyle.ComboBox;
            this.lvcolumnHeader_1.Text = "数据类型";
            this.lvcolumnHeader_1.Width = 0x7e;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.btnImportStruct);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.listView2);
            base.Name = "NewObjectClassFieldsPage";
            base.Size = new Size(0x170, 0x1a8);
            base.Load += new EventHandler(this.NewObjectClassFieldsPage_Load);
            base.ResumeLayout(false);
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
                        this.icontrolBaseInterface_0[(int) tag.Type].IsEdit = this.bool_2;
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

        private void method_0()
        {
            this.bool_3 = true;
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
        }

        private IFields method_1()
        {
            if (NewObjectClassHelper.m_pObjectClassHelper.IsFeatureClass)
            {
                IFeatureClassDescription description;
                IFields requiredFields;
                IGeometryDef geometryDef;
                if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTSimple)
                {
                    description = new FeatureClassDescriptionClass();
                    requiredFields = (description as IObjectClassDescription).RequiredFields;
                    IField field = requiredFields.get_Field(requiredFields.FindField(description.ShapeFieldName));
                    geometryDef = (field as IFieldEdit2).GeometryDef;
                    (geometryDef as IGeometryDefEdit).GeometryType_2 = NewObjectClassHelper.m_pObjectClassHelper.ShapeType;
                    (field as IFieldEdit).GeometryDef_2 = geometryDef as IGeometryDefEdit;
                    return requiredFields;
                }
                if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    description = new AnnotationFeatureClassDescriptionClass();
                    requiredFields = (description as IObjectClassDescription).RequiredFields;
                    geometryDef = (requiredFields.get_Field(requiredFields.FindField(description.ShapeFieldName)) as IFieldEdit2).GeometryDef;
                    return requiredFields;
                }
                if (NewObjectClassHelper.m_pObjectClassHelper.FeatureType == esriFeatureType.esriFTDimension)
                {
                    description = new DimensionClassDescriptionClass();
                    requiredFields = (description as IObjectClassDescription).RequiredFields;
                    geometryDef = (requiredFields.get_Field(requiredFields.FindField(description.ShapeFieldName)) as IFieldEdit2).GeometryDef;
                    return requiredFields;
                }
                return null;
            }
            IObjectClassDescription description2 = new FeatureClassDescriptionClass();
            return description2.RequiredFields;
        }

        private void method_2(EditListView editListView_0, IFields ifields_1)
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

        private void method_3(IField ifield_1, FieldChangeType fieldChangeType_0)
        {
            this.bool_3 = true;
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, new EventArgs());
            }
            if (this.ifieldsEdit_0.FindField(ifield_1.Name) == -1)
            {
                for (int i = 0; i < this.ilist_0.Count; i++)
                {
                    if ((this.ilist_0[i].Name == ifield_1.Name) && (((FieldChangeType) this.ilist_1[i]) == fieldChangeType_0))
                    {
                        return;
                    }
                }
                this.ilist_0.Add(ifield_1);
                this.ilist_1.Add(fieldChangeType_0);
            }
        }

        private void method_4(object sender, EventArgs e)
        {
            this.bool_3 = true;
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        private void method_5(IField ifield_1, bool bool_4)
        {
            if (this.groupBox1.Controls.Count == 1)
            {
                this.groupBox1.Controls[0].Parent = null;
            }
            if (ifield_1 != null)
            {
                this.icontrolBaseInterface_0[(int) ifield_1.Type].Filed = ifield_1;
                this.icontrolBaseInterface_0[(int) ifield_1.Type].IsEdit = bool_4;
                this.icontrolBaseInterface_0[(int) ifield_1.Type].Workspace = this.iworkspace_0;
                UserControl control = this.icontrolBaseInterface_0[(int) ifield_1.Type] as UserControl;
                control.Parent = this.groupBox1;
                control.Dock = DockStyle.Left;
                control.Show();
                this.groupBox1.Controls.Add(control);
            }
        }

        private void method_6(object sender, ValueChangedEventArgs e)
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
                    IFieldEdit field = new Field() as IFieldEdit;
                    field.Name_2 = item.SubItems[0].Text;
                    field.Type_2 = this.method_8(item.SubItems[1].Text);
                    tag = field as IFieldEdit;
                    if (tag.Type == esriFieldType.esriFieldTypeString)
                    {
                        tag.Length_2 = 50;
                    }
                    else if (tag.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        IGeometryDefEdit edit2 = new GeometryDefClass();
                        edit2.SpatialReference_2 = new UnknownCoordinateSystemClass();
                        edit2.GridCount_2 = 1;
                        edit2.set_GridSize(0, 1000.0);
                        edit2.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                        edit2.HasZ_2 = false;
                        edit2.HasM_2 = false;
                        tag.GeometryDef_2 = edit2;
                    }
                    this.ifieldsEdit_0.AddField(tag);
                    item.Tag = tag;
                    this.method_0();
                    string[] items = new string[] { "", "" };
                    this.listView2.Items.Add(new ListViewItem(items));
                    if (item == this.listView2.SelectedItems[0])
                    {
                        this.method_5(tag, false);
                    }
                }
            }
            else
            {
                tag = item.Tag as IFieldEdit;
                tag.Name_2 = item.SubItems[0].Text;
                esriFieldType type = this.method_8(item.SubItems[1].Text);
                if (tag.Type != type)
                {
                    tag.Type_2 = type;
                    if (tag.Domain != null)
                    {
                        tag.Domain_2 = null;
                    }
                    else if ((NewObjectClassHelper.m_pObjectClassHelper != null) && NewObjectClassHelper.m_pObjectClassHelper.FieldDomains.ContainsKey(tag))
                    {
                        NewObjectClassHelper.m_pObjectClassHelper.FieldDomains.Remove(tag);
                    }
                    tag.DefaultValue_2 = "";
                    this.icontrolBaseInterface_0[(int) tag.Type].Filed = tag;
                    if (this.ifieldsEdit_0.FindField(tag.Name) != -1)
                    {
                        this.icontrolBaseInterface_0[(int) tag.Type].IsEdit = false;
                    }
                    else
                    {
                        this.icontrolBaseInterface_0[(int) tag.Type].IsEdit = this.bool_2;
                    }
                    UserControl control = this.icontrolBaseInterface_0[(int) tag.Type] as UserControl;
                    this.groupBox1.Controls.Clear();
                    this.icontrolBaseInterface_0[(int) tag.Type].Workspace = this.iworkspace_0;
                    try
                    {
                        control.Dock = DockStyle.Left;
                        this.groupBox1.Controls.Add(control);
                    }
                    catch (Exception)
                    {
                    }
                    (control as IControlBaseInterface).Init();
                }
                this.method_0();
            }
        }

        private void method_7(object sender, RowDeleteEventArgs e)
        {
            if (this.listView2.SelectedIndices.Count != 0)
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
                    this.method_0();
                }
            }
        }

        private esriFieldType method_8(string string_5)
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

        private void NewObjectClassFieldsPage_Load(object sender, EventArgs e)
        {
            this.bool_2 = NewObjectClassHelper.m_pObjectClassHelper.IsEdit;
            this.bool_1 = true;
            for (int i = 0; i < this.icontrolBaseInterface_0.Length; i++)
            {
                this.icontrolBaseInterface_0[i].Workspace = this.iworkspace_0;
            }
            (this.icontrolBaseInterface_0[0] as FieldTypeIntegerCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[1] as FieldTypeIntegerCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[2] as FieldTypeDoubleCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[3] as FieldTypeDoubleCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[4] as FieldTypeTextCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[5] as FieldTypeDateCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[6] as FieldTypeObjectIDCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[7] as FieldTypeGeometryCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[8] as FieldTypeBlobCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[9] as FieldTypeRasterCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[10] as FieldTypeGuidCtrl).ValueChanged += new ValueChangedHandler(this.method_4);
            (this.icontrolBaseInterface_0[0] as FieldTypeIntegerCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[1] as FieldTypeIntegerCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[2] as FieldTypeDoubleCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[3] as FieldTypeDoubleCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[4] as FieldTypeTextCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[5] as FieldTypeDateCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[6] as FieldTypeObjectIDCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[7] as FieldTypeGeometryCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[8] as FieldTypeBlobCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[9] as FieldTypeRasterCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            (this.icontrolBaseInterface_0[10] as FieldTypeGuidCtrl).FieldChanged += new FieldChangedHandler(this.method_3);
            this.ifields_0 = NewObjectClassHelper.m_pObjectClassHelper.Fields;
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
            this.method_2(this.listView2, this.ifields_0);
            this.method_5(this.listView2.Items[0].Tag as IField, true);
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
                this.bool_2 = value;
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

