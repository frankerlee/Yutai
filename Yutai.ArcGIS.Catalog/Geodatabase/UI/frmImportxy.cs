using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmImportxy : Form
    {
        private ArrayList arrayList_0 = new ArrayList();
        private Container container_0 = null;
        private IDataset idataset_0 = null;

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
                Logger.Current.Error("", exception, "");
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

        public IFeatureClass CreateFeatureClass(object object_0, string string_2, ISpatialReference ispatialReference_0,
            esriFeatureType esriFeatureType_0, esriGeometryType esriGeometryType_0, IFields ifields_0, UID uid_0,
            UID uid_1, string string_3)
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
                edit2.GeometryType_2 = esriGeometryType_0;
                edit2.GridCount_2 = 1;
                edit2.set_GridSize(0, 0.5);
                edit2.AvgNumPoints_2 = 2;
                edit2.HasM_2 = false;
                edit2.HasZ_2 = true;
                if (object_0 is IWorkspace)
                {
                    edit2.SpatialReference_2 = ispatialReference_0;
                }
                IField field = new FieldClass();
                IFieldEdit edit3 = (IFieldEdit) field;
                edit3.Name_2 = "OBJECTID";
                edit3.AliasName_2 = "OBJECTID";
                edit3.Type_2 = esriFieldType.esriFieldTypeOID;
                edit.AddField(field);
                IField field2 = new FieldClass();
                IFieldEdit edit4 = (IFieldEdit) field2;
                edit4.Name_2 = "SHAPE";
                edit4.AliasName_2 = "SHAPE";
                edit4.Type_2 = esriFieldType.esriFieldTypeGeometry;
                edit4.GeometryDef_2 = def;
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
                return workspace2.CreateFeatureClass(string_2, ifields_0, uid_0, uid_1, esriFeatureType_0,
                    shapeFieldName, string_3);
            }
            if (object_0 is IFeatureDataset)
            {
                class2 = ((IFeatureDataset) object_0).CreateFeatureClass(string_2, ifields_0, uid_0, uid_1,
                    esriFeatureType_0, shapeFieldName, string_3);
            }
            return class2;
        }

        private void method_0()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.string_0 + @"\" +
                                      this.string_1 + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2'";
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
                        edit.Name_2 = column.ColumnName + "_";
                    }
                    else
                    {
                        edit.Name_2 = column.ColumnName;
                    }
                    System.Type dataType = column.DataType;
                    if (dataType.Name == "String")
                    {
                        edit.Type_2 = esriFieldType.esriFieldTypeString;
                        edit.Length_2 = column.MaxLength;
                    }
                    else if (dataType.Name == "Double")
                    {
                        edit.Type_2 = esriFieldType.esriFieldTypeDouble;
                        edit.Precision_2 = 10;
                    }
                    else
                    {
                        edit.Type_2 = esriFieldType.esriFieldTypeString;
                        edit.Length_2 = column.MaxLength;
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
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + this.string_0 + @"\" +
                                      this.string_1 + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=2'";
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
                point = new PointClass
                {
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
                    Logger.Current.Error("", exception, "");
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
            edit2.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            edit2.GridCount_2 = 1;
            edit2.set_GridSize(0, 200.0);
            edit2.AvgNumPoints_2 = 1;
            edit2.HasM_2 = false;
            edit2.HasZ_2 = false;
            ISpatialReference reference = new UnknownCoordinateSystemClass();
            ISpatialReferenceResolution resolution = reference as ISpatialReferenceResolution;
            resolution.ConstructFromHorizon();
            resolution.SetDefaultXYResolution();
            (reference as ISpatialReferenceTolerance).SetDefaultXYTolerance();
            edit2.SpatialReference_2 = reference;
            IField field = new FieldClass();
            IFieldEdit edit3 = (IFieldEdit) field;
            edit3.Name_2 = "OBJECTID";
            edit3.AliasName_2 = "OBJECTID";
            edit3.Type_2 = esriFieldType.esriFieldTypeOID;
            edit.AddField(field);
            IField field2 = new FieldClass();
            IFieldEdit edit4 = (IFieldEdit) field2;
            edit4.Name_2 = "SHAPE";
            edit4.AliasName_2 = "SHAPE";
            edit4.Type_2 = esriFieldType.esriFieldTypeGeometry;
            edit4.GeometryDef_2 = def;
            edit.AddField(field2);
            return this.CreateFeatureClass(idataset_1, string_2, new UnknownCoordinateSystemClass(),
                esriFeatureType.esriFTSimple, esriGeometryType.esriGeometryPoint, fields, null, null, null);
        }
    }
}