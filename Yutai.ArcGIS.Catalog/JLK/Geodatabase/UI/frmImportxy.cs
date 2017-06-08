namespace JLK.Geodatabase.UI
{
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Editors;
    using JLK.Editors.Controls;
    using JLK.Utility;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class frmImportxy : Form
    {
        private ArrayList arrayList_0 = new ArrayList();
        private SimpleButton btnExport;
        private ComboBoxEdit cboXField;
        private ComboBoxEdit cboYField;
        private ComboBoxEdit cboZField;
        private SimpleButton cmdFind;
        private Container container_0 = null;
        private TextEdit ExcelFullPath;
        private IDataset idataset_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private OpenFileDialog openFileDialog_0;
        private SimpleButton simpleButton2;
        private string string_0;
        private string string_1;

        public frmImportxy(IDataset idataset_1)
        {
            this.InitializeComponent();
            this.idataset_0 = idataset_1;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cboXField.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择X坐标字段!");
                }
                else if (this.cboYField.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择Y坐标字段!");
                }
                else if ((this.cboXField.Text == this.cboZField.Text) && (this.cboXField.Text == this.cboYField.Text))
                {
                    MessageBox.Show("X、Y、Z坐标字段名相同，请重新选择!");
                }
                else if (this.cboXField.Text == this.cboYField.Text)
                {
                    MessageBox.Show("X和Y坐标字段名相同，请重新选择!");
                }
                else if (this.cboXField.Text == this.cboZField.Text)
                {
                    MessageBox.Show("X和Z坐标字段名相同，请重新选择!");
                }
                else if (this.cboYField.Text == this.cboZField.Text)
                {
                    MessageBox.Show("Y和Z坐标字段名相同，请重新选择!");
                }
                else
                {
                    if (this.idataset_0 is IFeatureClass)
                    {
                        if ((this.idataset_0 as IFeatureClass).ShapeType == esriGeometryType.esriGeometryPoint)
                        {
                            this.method_1(this.idataset_0 as IFeatureClass);
                        }
                        else
                        {
                            MessageBox.Show("只能将坐标导入点要素中!");
                        }
                    }
                    else
                    {
                        string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(this.string_1);
                        IFeatureClass class2 = this.method_2(this.idataset_0, fileNameWithoutExtension);
                        if (class2 != null)
                        {
                            this.method_1(class2);
                        }
                    }
                    base.Close();
                }
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
                MessageBox.Show(exception.ToString());
            }
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            this.openFileDialog_0.ShowDialog();
            string fileName = this.openFileDialog_0.FileName;
            this.ExcelFullPath.Text = fileName;
            string str2 = fileName;
            for (int i = fileName.IndexOf(@"\"); i != -1; i = str2.IndexOf(@"\"))
            {
                str2 = str2.Substring(i + 1);
            }
            this.string_1 = str2;
            int index = fileName.IndexOf(this.string_1);
            this.string_0 = fileName.Substring(0, index - 1);
            try
            {
                this.method_0();
            }
            catch
            {
                MessageBox.Show("无法从Excel文件中找到[XYDATA]页!");
            }
        }

        public IFeatureClass CreateFeatureClass(object object_0, string string_2, ISpatialReference ispatialReference_0, esriFeatureType esriFeatureType_0, esriGeometryType esriGeometryType_0, IFields ifields_0, UID uid_0, UID uid_1, string string_3)
        {
            if (object_0 == null)
            {
                throw new Exception("[objectWorkspace] cannot be null");
            }
            if (!((object_0 is IWorkspace) || (object_0 is IFeatureDataset)))
            {
                throw new Exception("[objectWorkspace] must be IWorkspace or IFeatureDataset");
            }
            if (string_2 == "")
            {
                throw new Exception("[name] cannot be empty");
            }
            if ((object_0 is IWorkspace) && (ispatialReference_0 == null))
            {
                throw new Exception("[spatialReference] cannot be null for StandAlong FeatureClasses");
            }
            if (uid_0 == null)
            {
                uid_0 = new UIDClass();
                switch (esriFeatureType_0)
                {
                    case esriFeatureType.esriFTSimple:
                        uid_0.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
                        break;

                    case esriFeatureType.esriFTSimpleJunction:
                        esriGeometryType_0 = esriGeometryType.esriGeometryPoint;
                        uid_0.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}";
                        break;

                    case esriFeatureType.esriFTSimpleEdge:
                        esriGeometryType_0 = esriGeometryType.esriGeometryPolyline;
                        uid_0.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}";
                        break;

                    case esriFeatureType.esriFTComplexJunction:
                        uid_0.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}";
                        break;

                    case esriFeatureType.esriFTComplexEdge:
                        esriGeometryType_0 = esriGeometryType.esriGeometryPolyline;
                        uid_0.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}";
                        break;

                    case esriFeatureType.esriFTAnnotation:
                        esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;
                        uid_0.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}";
                        break;

                    case esriFeatureType.esriFTDimension:
                        esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;
                        uid_0.Value = "{496764FC-E0C9-11D3-80CE-00C04F601565}";
                        break;
                }
            }
            if (uid_1 == null)
            {
                switch (esriFeatureType_0)
                {
                    case esriFeatureType.esriFTAnnotation:
                        uid_1 = new UIDClass();
                        uid_1.Value = "{24429589-D711-11D2-9F41-00C04F6BC6A5}";
                        break;

                    case esriFeatureType.esriFTDimension:
                        uid_1 = new UIDClass();
                        uid_1.Value = "{48F935E2-DA66-11D3-80CE-00C04F601565}";
                        break;
                }
            }
            if (ifields_0 == null)
            {
                ifields_0 = new FieldsClass();
                IFieldsEdit edit = (IFieldsEdit) ifields_0;
                IGeometryDef def = new GeometryDefClass();
                IGeometryDefEdit edit2 = (IGeometryDefEdit) def;
                edit2.GeometryType = esriGeometryType_0;
                edit2.GridCount = 1;
                edit2.set_GridSize(0, 0.5);
                edit2.AvgNumPoints = 2;
                edit2.HasM = false;
                edit2.HasZ = true;
                if (object_0 is IWorkspace)
                {
                    edit2.SpatialReference = ispatialReference_0;
                }
                IField field = new FieldClass();
                IFieldEdit edit3 = (IFieldEdit) field;
                edit3.Name = "OBJECTID";
                edit3.AliasName = "OBJECTID";
                edit3.Type = esriFieldType.esriFieldTypeOID;
                edit.AddField(field);
                IField field2 = new FieldClass();
                IFieldEdit edit4 = (IFieldEdit) field2;
                edit4.Name = "SHAPE";
                edit4.AliasName = "SHAPE";
                edit4.Type = esriFieldType.esriFieldTypeGeometry;
                edit4.GeometryDef = def;
                edit.AddField(field2);
            }
            string shapeFieldName = "";
            for (int i = 0; i <= (ifields_0.FieldCount - 1); i++)
            {
                if (ifields_0.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    shapeFieldName = ifields_0.get_Field(i).Name;
                    break;
                }
            }
            if (shapeFieldName == "")
            {
                throw new Exception("Cannot locate geometry field in FIELDS");
            }
            IFeatureClass class2 = null;
            if (object_0 is IWorkspace)
            {
                IWorkspace workspace = (IWorkspace) object_0;
                IFeatureWorkspace workspace2 = (IFeatureWorkspace) workspace;
                return workspace2.CreateFeatureClass(string_2, ifields_0, uid_0, uid_1, esriFeatureType_0, shapeFieldName, string_3);
            }
            if (object_0 is IFeatureDataset)
            {
                class2 = ((IFeatureDataset) object_0).CreateFeatureClass(string_2, ifields_0, uid_0, uid_1, esriFeatureType_0, shapeFieldName, string_3);
            }
            return class2;
        }

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.container_0 != null))
            {
                this.container_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            this.label2 = new Label();
            this.cmdFind = new SimpleButton();
            this.label1 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.cboXField = new ComboBoxEdit();
            this.cboYField = new ComboBoxEdit();
            this.cboZField = new ComboBoxEdit();
            this.ExcelFullPath = new TextEdit();
            this.btnExport = new SimpleButton();
            this.simpleButton2 = new SimpleButton();
            this.openFileDialog_0 = new OpenFileDialog();
            this.cboXField.Properties.BeginInit();
            this.cboYField.Properties.BeginInit();
            this.cboZField.Properties.BeginInit();
            this.ExcelFullPath.Properties.BeginInit();
            base.SuspendLayout();
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x10, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x55, 0x11);
            this.label2.TabIndex = 5;
            this.label2.Text = "输入Excel文件";
            this.cmdFind.Location = new System.Drawing.Point(0x130, 40);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new Size(0x20, 0x17);
            this.cmdFind.TabIndex = 8;
            this.cmdFind.Text = "...";
            this.cmdFind.Click += new EventHandler(this.cmdFind_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x10, 80);
            this.label1.Name = "label1";
            this.label1.Size = new Size(60, 0x11);
            this.label1.TabIndex = 9;
            this.label1.Text = "X坐标字段";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x10, 0x70);
            this.label3.Name = "label3";
            this.label3.Size = new Size(60, 0x11);
            this.label3.TabIndex = 10;
            this.label3.Text = "Y坐标字段";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x10, 0x90);
            this.label4.Name = "label4";
            this.label4.Size = new Size(60, 0x11);
            this.label4.TabIndex = 11;
            this.label4.Text = "Z坐标字段";
            this.cboXField.EditValue = "";
            this.cboXField.Location = new System.Drawing.Point(0x60, 0x48);
            this.cboXField.Name = "cboXField";
            this.cboXField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboXField.Size = new Size(200, 0x17);
            this.cboXField.TabIndex = 12;
            this.cboYField.EditValue = "";
            this.cboYField.Location = new System.Drawing.Point(0x60, 0x68);
            this.cboYField.Name = "cboYField";
            this.cboYField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboYField.Size = new Size(200, 0x17);
            this.cboYField.TabIndex = 13;
            this.cboZField.EditValue = "";
            this.cboZField.Location = new System.Drawing.Point(0x60, 0x88);
            this.cboZField.Name = "cboZField";
            this.cboZField.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboZField.Size = new Size(200, 0x17);
            this.cboZField.TabIndex = 14;
            this.ExcelFullPath.EditValue = "";
            this.ExcelFullPath.Location = new System.Drawing.Point(0x10, 40);
            this.ExcelFullPath.Name = "ExcelFullPath";
            this.ExcelFullPath.Properties.ReadOnly = true;
            this.ExcelFullPath.Size = new Size(280, 0x17);
            this.ExcelFullPath.TabIndex = 15;
            this.btnExport.Location = new System.Drawing.Point(0xb8, 0xb0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new Size(0x30, 0x17);
            this.btnExport.TabIndex = 0x10;
            this.btnExport.Text = "导入";
            this.btnExport.Click += new EventHandler(this.btnExport_Click);
            this.simpleButton2.DialogResult = DialogResult.OK;
            this.simpleButton2.Location = new System.Drawing.Point(0x100, 0xb0);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(0x30, 0x17);
            this.simpleButton2.TabIndex = 0x11;
            this.simpleButton2.Text = "取消";
            this.openFileDialog_0.DefaultExt = "xls";
            this.openFileDialog_0.Filter = "Excel (*.xls)|*.xls";
            this.AutoScaleBaseSize = new Size(6, 14);
            base.ClientSize = new Size(0x158, 0xd5);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnExport);
            base.Controls.Add(this.ExcelFullPath);
            base.Controls.Add(this.cboZField);
            base.Controls.Add(this.cboYField);
            base.Controls.Add(this.cboXField);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.cmdFind);
            base.Controls.Add(this.label2);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmImportxy";
            this.Text = "从excel导入xy坐标点";
            this.cboXField.Properties.EndInit();
            this.cboYField.Properties.EndInit();
            this.cboZField.Properties.EndInit();
            this.ExcelFullPath.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void method_0()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.string_0 + @"\" + this.string_1 + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2'";
            string cmdText = "SELECT * FROM [XYDATA$]";
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("XYDATA");
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
            connection.Open();
            try
            {
                adapter.Fill(dataSet, "XYDATA");
            }
            finally
            {
                connection.Close();
            }
            try
            {
                DataColumnCollection columns = dataSet.Tables["XYDATA"].Columns;
                this.arrayList_0.Clear();
                foreach (DataColumn column in columns)
                {
                    IFieldEdit edit = new FieldClass();
                    if (column.ColumnName.ToLower() == "as")
                    {
                        edit.Name = column.ColumnName + "_";
                    }
                    else
                    {
                        edit.Name = column.ColumnName;
                    }
                    System.Type dataType = column.DataType;
                    if (dataType.Name == "String")
                    {
                        edit.Type = esriFieldType.esriFieldTypeString;
                        edit.Length = column.MaxLength;
                    }
                    else if (dataType.Name == "Double")
                    {
                        edit.Type = esriFieldType.esriFieldTypeDouble;
                        edit.Precision = 10;
                    }
                    else
                    {
                        edit.Type = esriFieldType.esriFieldTypeString;
                        edit.Length = column.MaxLength;
                    }
                    this.arrayList_0.Add(edit);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:\n{0}", exception.Message);
            }
            this.cboXField.Properties.Items.Clear();
            this.cboYField.Properties.Items.Clear();
            this.cboZField.Properties.Items.Clear();
            this.cboZField.Properties.Items.Add("<无>");
            for (int i = 0; i < this.arrayList_0.Count; i++)
            {
                this.cboXField.Properties.Items.Add((this.arrayList_0[i] as IField).Name);
                this.cboYField.Properties.Items.Add((this.arrayList_0[i] as IField).Name);
                this.cboZField.Properties.Items.Add((this.arrayList_0[i] as IField).Name);
            }
        }

        private void method_1(IFeatureClass ifeatureClass_0)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.string_0 + @"\" + this.string_1 + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2'";
            string cmdText = "SELECT * FROM [XYDATA$]";
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("XYDATA");
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand selectCommand = new OleDbCommand(cmdText, connection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommand);
            connection.Open();
            try
            {
                adapter.Fill(dataSet, "XYDATA");
            }
            finally
            {
                connection.Close();
            }
            DataRowCollection rows = dataSet.Tables["XYDATA"].Rows;
            IDataset dataset = (IDataset) ifeatureClass_0;
            IWorkspaceEdit workspace = (IWorkspaceEdit) dataset.Workspace;
            workspace.StartEditing(true);
            int index = ifeatureClass_0.FindField(ifeatureClass_0.ShapeFieldName);
            IGeometryDef geometryDef = ifeatureClass_0.Fields.get_Field(index).GeometryDef;
            foreach (DataRow row in rows)
            {
                IPoint point = null;
                point = new PointClass {
                    X = Convert.ToDouble(row[this.cboXField.Text].ToString()),
                    Y = Convert.ToDouble(row[this.cboYField.Text].ToString())
                };
                if (this.cboZField.SelectedIndex > 0)
                {
                    point.Z = Convert.ToDouble(row[this.cboZField.Text].ToString());
                }
                if (geometryDef.HasZ)
                {
                    IZAware aware = (IZAware) point;
                    aware.ZAware = true;
                }
                if (geometryDef.HasM)
                {
                    IMAware aware2 = (IMAware) point;
                    aware2.MAware = true;
                }
                try
                {
                    ITable table = (ITable) ifeatureClass_0;
                    IRowBuffer buffer = table.CreateRowBuffer();
                    int num2 = buffer.Fields.FindField("SHAPE");
                    buffer.set_Value(num2, point);
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        string name = row.Table.Columns[i].ToString();
                        if (name.ToLower() == "as")
                        {
                            name = name + "_";
                        }
                        num2 = buffer.Fields.FindField(name);
                        buffer.set_Value(num2, row[i].ToString());
                    }
                    table.Insert(true).InsertRow(buffer);
                }
                catch (Exception exception)
                {
                    CErrorLog.writeErrorLog(this, exception, "");
                }
            }
            workspace.StopEditing(true);
        }

        private IFeatureClass method_2(IDataset idataset_1, string string_2)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit edit = (IFieldsEdit) fields;
            for (int i = 0; i < this.arrayList_0.Count; i++)
            {
                edit.AddField(this.arrayList_0[i] as IField);
            }
            IGeometryDef def = new GeometryDefClass();
            IGeometryDefEdit edit2 = (IGeometryDefEdit) def;
            edit2.GeometryType = esriGeometryType.esriGeometryPoint;
            edit2.GridCount = 1;
            edit2.set_GridSize(0, 200.0);
            edit2.AvgNumPoints = 1;
            edit2.HasM = false;
            edit2.HasZ = false;
            ISpatialReference reference = new UnknownCoordinateSystemClass();
            ISpatialReferenceResolution resolution = reference as ISpatialReferenceResolution;
            resolution.ConstructFromHorizon();
            resolution.SetDefaultXYResolution();
            (reference as ISpatialReferenceTolerance).SetDefaultXYTolerance();
            edit2.SpatialReference = reference;
            IField field = new FieldClass();
            IFieldEdit edit3 = (IFieldEdit) field;
            edit3.Name = "OBJECTID";
            edit3.AliasName = "OBJECTID";
            edit3.Type = esriFieldType.esriFieldTypeOID;
            edit.AddField(field);
            IField field2 = new FieldClass();
            IFieldEdit edit4 = (IFieldEdit) field2;
            edit4.Name = "SHAPE";
            edit4.AliasName = "SHAPE";
            edit4.Type = esriFieldType.esriFieldTypeGeometry;
            edit4.GeometryDef = def;
            edit.AddField(field2);
            return this.CreateFeatureClass(idataset_1, string_2, new UnknownCoordinateSystemClass(), esriFeatureType.esriFTSimple, esriGeometryType.esriGeometryPoint, fields, null, null, null);
        }
    }
}

