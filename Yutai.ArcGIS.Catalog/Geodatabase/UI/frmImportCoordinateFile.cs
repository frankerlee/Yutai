using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Catalog.UI;
using Yutai.ArcGIS.Common;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    public partial class frmImportCoordinateFile : Form
    {
        private IContainer icontainer_0 = null;
        private IGxObject igxObject_0 = null;
        private IName iname_0 = null;

        public frmImportCoordinateFile()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtCoordinateFile.Text.Length == 0)
            {
                MessageBox.Show("请选择坐标数据文件");
            }
            else if (this.iname_0 == null)
            {
                MessageBox.Show("请选择坐标数据导入位置");
            }
            else if (this.txtFeatureClassName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入要创建的要素类名");
            }
            else
            {
                IFeatureClass class2 = this.method_1();
                if (class2 != null)
                {
                    StreamReader reader = new StreamReader(this.txtCoordinateFile.Text, Encoding.Default, true);
                    System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
                    string str = this.method_4(reader, class2);
                    reader.Close();
                    System.Windows.Forms.Cursor.Current = Cursors.Default;
                    if (str.Length > 0)
                    {
                        MessageBox.Show("以下行数据有问题：\r\n" + str);
                    }
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
        }

        private void btnOpenCoordinateFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "*.txt|*.txt"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtCoordinateFile.Text = dialog.FileName;
            }
        }

        private void btnSelectCoordinate_Click(object sender, EventArgs e)
        {
            ISpatialReference tag = this.btnSelectCoordinate.Tag as ISpatialReference;
            if (tag == null)
            {
                tag = new UnknownCoordinateSystemClass();
            }
            frmSpatialReference reference2 = new frmSpatialReference {
                SpatialRefrence = tag
            };
            if (reference2.ShowDialog() == DialogResult.OK)
            {
                this.btnSelectCoordinate.Tag = tag;
                this.btnSelectCoordinate.Text = tag.Name;
            }
        }

        private void btnSelectOutLocation_Click(object sender, EventArgs e)
        {
            frmOpenFile file = new frmOpenFile {
                Text = "保存位置"
            };
            file.RemoveAllFilters();
            file.AddFilter(new MyGxFilterWorkspaces(), true);
            file.AddFilter(new MyGxFilterFeatureDatasets(), false);
            if (file.DoModalSaveLocation() == DialogResult.OK)
            {
                IArray items = file.Items;
                if (items.Count != 0)
                {
                    this.igxObject_0 = items.get_Element(0) as IGxObject;
                    this.iname_0 = this.igxObject_0.InternalObjectName;
                    if (this.igxObject_0 is IGxDatabase)
                    {
                        this.iname_0 = this.igxObject_0.InternalObjectName;
                    }
                    else if (this.igxObject_0 is IGxFolder)
                    {
                        IWorkspaceName name = new WorkspaceNameClass {
                            WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory",
                            PathName = (this.igxObject_0.InternalObjectName as IFileName).Path
                        };
                        this.iname_0 = name as IName;
                    }
                    else if (this.igxObject_0 is IGxDataset)
                    {
                        IDatasetName internalObjectName = this.igxObject_0.InternalObjectName as IDatasetName;
                        if (internalObjectName.Type != esriDatasetType.esriDTFeatureDataset)
                        {
                            return;
                        }
                        this.iname_0 = internalObjectName as IName;
                    }
                    this.txtOutLocation.Text = this.igxObject_0.FullName;
                }
            }
        }

 private string method_0(IWorkspace iworkspace_0, string string_0)
        {
            string str2;
            int num;
            string str = string_0;
            if ((iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace) || (iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace))
            {
                IWorkspace2 o = iworkspace_0 as IWorkspace2;
                str2 = str;
                for (num = 1; o.get_NameExists(esriDatasetType.esriDTFeatureClass, str2); num++)
                {
                    str2 = str + "_" + num.ToString();
                }
                Marshal.ReleaseComObject(o);
                o = null;
                return str2;
            }
            string str3 = System.IO.Path.Combine(iworkspace_0.PathName, str);
            str2 = str3 + ".shp";
            for (num = 1; File.Exists(str2); num++)
            {
                str2 = str3 + "_" + num.ToString() + ".shp";
            }
            return str2;
        }

        private IFeatureClass method_1()
        {
            IFeatureClass class2 = null;
            if (this.iname_0 == null)
            {
                return null;
            }
            object obj2 = this.iname_0.Open();
            IWorkspace workspace = null;
            IFeatureDataset dataset = null;
            if (obj2 is IWorkspace)
            {
                workspace = obj2 as IWorkspace;
            }
            else if (obj2 is IFeatureDataset)
            {
                dataset = obj2 as IFeatureDataset;
                workspace = dataset.Workspace;
            }
            IObjectClassDescription description = null;
            description = new FeatureClassDescriptionClass();
            IFieldsEdit requiredFields = description.RequiredFields as IFieldsEdit;
            IFieldEdit field = null;
            IFieldEdit edit3 = null;
            int index = requiredFields.FindField((description as IFeatureClassDescription).ShapeFieldName);
            edit3 = requiredFields.get_Field(index) as IFieldEdit;
            IGeometryDefEdit geometryDef = edit3.GeometryDef as IGeometryDefEdit;
            if (this.cboGeometryType.SelectedIndex == 0)
            {
                geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPoint;
            }
            else if (this.cboGeometryType.SelectedIndex == 1)
            {
                geometryDef.GeometryType_2 = esriGeometryType.esriGeometryMultipoint;
            }
            else if (this.cboGeometryType.SelectedIndex == 2)
            {
                geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
            }
            else if (this.cboGeometryType.SelectedIndex == 3)
            {
                geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
            }
            ISpatialReference spatialReference = geometryDef.SpatialReference;
            if (this.txtCoordinate.Tag is ISpatialReference)
            {
                spatialReference = this.txtCoordinate.Tag as ISpatialReference;
            }
            geometryDef.HasZ_2 = this.chkHasZ.Checked;
            geometryDef.HasM_2 = this.chkHasM.Checked;
            SpatialReferenctOperator.ChangeCoordinateSystem(workspace as IGeodatabaseRelease, spatialReference, false);
            geometryDef.SpatialReference_2 = spatialReference;
            esriFeatureType esriFTSimple = esriFeatureType.esriFTSimple;
            field = new FieldClass();
            field.Name_2 = "Code";
            field.AliasName_2 = "要素编号";
            field.Type_2 = esriFieldType.esriFieldTypeInteger;
            requiredFields.AddField(field);
            string name = this.method_0(workspace, this.txtFeatureClassName.Text);
            try
            {
                if (dataset == null)
                {
                    class2 = (workspace as IFeatureWorkspace).CreateFeatureClass(name, requiredFields, null, null, esriFTSimple, "Shape", "");
                }
                else
                {
                    class2 = dataset.CreateFeatureClass(name, requiredFields, null, null, esriFTSimple, "Shape", "");
                }
            }
            catch (Exception exception)
            {
                Logger.Current.Error("",exception, "");
            }
            return class2;
        }

        private IPoint method_2(string string_0, bool bool_0, out int int_0)
        {
            int_0 = -1;
            string[] strArray = string_0.Split(new char[] { ',' });
            if (bool_0 && (strArray.Length < 3))
            {
                return null;
            }
            if (!(bool_0 || (strArray.Length >= 2)))
            {
                return null;
            }
            int index = 0;
            if (bool_0)
            {
                index = 1;
                try
                {
                    int_0 = int.Parse(strArray[0].Trim());
                }
                catch
                {
                }
            }
            IPoint point2 = new PointClass();
            try
            {
                point2.X = double.Parse(strArray[index]);
                index++;
                point2.Y = double.Parse(strArray[index]);
                if (this.chkHasZ.Checked)
                {
                    point2.Z = 0.0;
                }
                if (this.chkHasM.Checked)
                {
                    point2.M = 0.0;
                }
                index++;
                if ((strArray.Length > index) && (strArray[index].Trim().Length > 0))
                {
                    try
                    {
                        point2.Z = double.Parse(strArray[index]);
                    }
                    catch
                    {
                    }
                }
                index++;
                if ((strArray.Length > index) && (strArray[index].Trim().Length > 0))
                {
                    try
                    {
                        point2.M = double.Parse(strArray[index]);
                    }
                    catch
                    {
                    }
                }
                return point2;
            }
            catch
            {
                return null;
            }
        }

        private string method_3(StreamReader streamReader_0, IFeatureClass ifeatureClass_0)
        {
            StringBuilder builder = new StringBuilder();
            int index = ifeatureClass_0.FindField("Code");
            int num2 = 0;
            while (!streamReader_0.EndOfStream)
            {
                string str = streamReader_0.ReadLine();
                num2++;
                if (str.Length != 0)
                {
                    int num3;
                    IPoint point = this.method_2(str, true, out num3);
                    if ((num3 == -1) || (point == null))
                    {
                        builder.Append(num2);
                        builder.Append(",");
                    }
                    IFeature o = ifeatureClass_0.CreateFeature();
                    o.set_Value(index, num3);
                    try
                    {
                        if (this.chkHasZ.Checked)
                        {
                            (point as IZAware).ZAware = true;
                        }
                        if (this.chkHasM.Checked)
                        {
                            (point as IMAware).MAware = true;
                        }
                        o.Shape = point;
                        o.Store();
                    }
                    catch
                    {
                        o.Delete();
                        builder.Append(num2);
                        builder.Append(",");
                    }
                    Marshal.ReleaseComObject(o);
                    o = null;
                }
            }
            return builder.ToString();
        }

        private string method_4(StreamReader streamReader_0, IFeatureClass ifeatureClass_0)
        {
            IFeature feature;
            if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPoint)
            {
                return this.method_3(streamReader_0, ifeatureClass_0);
            }
            StringBuilder builder = new StringBuilder();
            int index = ifeatureClass_0.FindField("Code");
            int num2 = 0;
            int num3 = -1;
            IPointCollection o = null;
            while (!streamReader_0.EndOfStream)
            {
                string str2 = streamReader_0.ReadLine();
                num2++;
                if (str2.Length != 0)
                {
                    if (str2.IndexOf(",") == -1)
                    {
                        if (o == null)
                        {
                            try
                            {
                                num3 = int.Parse(str2.Trim());
                                if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryMultipoint)
                                {
                                    o = new MultipointClass();
                                }
                                else if (ifeatureClass_0.ShapeType == esriGeometryType.esriGeometryPolyline)
                                {
                                    o = new PolylineClass();
                                }
                                else
                                {
                                    o = new PolygonClass();
                                }
                            }
                            catch
                            {
                                builder.Append(num2);
                                builder.Append(",");
                            }
                        }
                        else
                        {
                            feature = ifeatureClass_0.CreateFeature();
                            feature.set_Value(index, num3);
                            try
                            {
                                if (this.chkHasZ.Checked)
                                {
                                    (o as IZAware).ZAware = true;
                                }
                                if (this.chkHasM.Checked)
                                {
                                    (o as IMAware).MAware = true;
                                }
                                feature.Shape = o as IGeometry;
                                feature.Store();
                            }
                            catch
                            {
                                feature.Delete();
                                builder.Append(num2);
                                builder.Append(",");
                            }
                            Marshal.ReleaseComObject(o);
                            o = null;
                            Marshal.ReleaseComObject(feature);
                            feature = null;
                        }
                    }
                    else if (o != null)
                    {
                        int num4;
                        IPoint inPoint = this.method_2(str2, false, out num4);
                        if (inPoint == null)
                        {
                            builder.Append(num2);
                            builder.Append(",");
                            continue;
                        }
                        object before = Missing.Value;
                        o.AddPoint(inPoint, ref before, ref before);
                    }
                }
            }
            if (o != null)
            {
                feature = ifeatureClass_0.CreateFeature();
                feature.set_Value(index, num3);
                try
                {
                    if (this.chkHasZ.Checked)
                    {
                        (o as IZAware).ZAware = true;
                    }
                    if (this.chkHasM.Checked)
                    {
                        (o as IMAware).MAware = true;
                    }
                    feature.Shape = o as IGeometry;
                    feature.Store();
                }
                catch
                {
                    feature.Delete();
                    builder.Append(num2);
                    builder.Append(",");
                }
                Marshal.ReleaseComObject(o);
                o = null;
                Marshal.ReleaseComObject(feature);
                feature = null;
            }
            return builder.ToString();
        }

        private string method_5(StreamReader streamReader_0, IFeatureClass ifeatureClass_0)
        {
            IFeature feature;
            StringBuilder builder = new StringBuilder();
            int index = ifeatureClass_0.FindField("Code");
            int num2 = 0;
            int num3 = -1;
            IPointCollection o = null;
            while (!streamReader_0.EndOfStream)
            {
                string str = streamReader_0.ReadLine();
                num2++;
                if (str.Length != 0)
                {
                    if (str.IndexOf(",") == -1)
                    {
                        if (o == null)
                        {
                            try
                            {
                                num3 = int.Parse(str.Trim());
                                o = new MultipointClass();
                            }
                            catch
                            {
                                builder.Append(num2);
                                builder.Append(",");
                            }
                        }
                        else
                        {
                            if (o is IPolygon)
                            {
                                (o as IPolygon).Close();
                            }
                            feature = ifeatureClass_0.CreateFeature();
                            feature.set_Value(index, num3);
                            try
                            {
                                feature.Shape = o as IGeometry;
                                feature.Store();
                            }
                            catch
                            {
                                feature.Delete();
                                builder.Append(num2);
                                builder.Append(",");
                            }
                            Marshal.ReleaseComObject(o);
                            o = null;
                            Marshal.ReleaseComObject(feature);
                            feature = null;
                        }
                    }
                    else if (o != null)
                    {
                        int num4;
                        IPoint inPoint = this.method_2(str, true, out num4);
                        if ((num4 == -1) || (inPoint == null))
                        {
                            builder.Append(num2);
                            builder.Append(",");
                            continue;
                        }
                        object before = Missing.Value;
                        o.AddPoint(inPoint, ref before, ref before);
                    }
                }
            }
            if (o != null)
            {
                if (o is IPolygon)
                {
                    (o as IPolygon).Close();
                }
                feature = ifeatureClass_0.CreateFeature();
                feature.set_Value(index, num3);
                try
                {
                    feature.Shape = o as IGeometry;
                    feature.Store();
                }
                catch
                {
                    feature.Delete();
                    builder.Append(num2);
                    builder.Append(",");
                }
                Marshal.ReleaseComObject(o);
                o = null;
                Marshal.ReleaseComObject(feature);
                feature = null;
            }
            return builder.ToString();
        }
    }
}

