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
    public class frmImportCoordinateFile : Form
    {
        private SimpleButton btnOK;
        private SimpleButton btnOpenCoordinateFile;
        private SimpleButton btnSelectCoordinate;
        private SimpleButton btnSelectOutLocation;
        private ComboBoxEdit cboGeometryType;
        private CheckEdit chkHasM;
        private CheckEdit chkHasZ;
        private IContainer icontainer_0 = null;
        private IGxObject igxObject_0 = null;
        private IName iname_0 = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private SimpleButton simpleButton2;
        private TextEdit txtCoordinate;
        private TextEdit txtCoordinateFile;
        private TextEdit txtFeatureClassName;
        private TextEdit txtOutLocation;

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

        protected override void Dispose(bool bool_0)
        {
            if (bool_0 && (this.icontainer_0 != null))
            {
                this.icontainer_0.Dispose();
            }
            base.Dispose(bool_0);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportCoordinateFile));
            this.simpleButton2 = new SimpleButton();
            this.btnOK = new SimpleButton();
            this.btnSelectOutLocation = new SimpleButton();
            this.label2 = new Label();
            this.txtOutLocation = new TextEdit();
            this.label1 = new Label();
            this.txtCoordinateFile = new TextEdit();
            this.btnOpenCoordinateFile = new SimpleButton();
            this.label3 = new Label();
            this.cboGeometryType = new ComboBoxEdit();
            this.label4 = new Label();
            this.btnSelectCoordinate = new SimpleButton();
            this.txtCoordinate = new TextEdit();
            this.txtFeatureClassName = new TextEdit();
            this.label5 = new Label();
            this.chkHasZ = new CheckEdit();
            this.chkHasM = new CheckEdit();
            this.txtOutLocation.Properties.BeginInit();
            this.txtCoordinateFile.Properties.BeginInit();
            this.cboGeometryType.Properties.BeginInit();
            this.txtCoordinate.Properties.BeginInit();
            this.txtFeatureClassName.Properties.BeginInit();
            this.chkHasZ.Properties.BeginInit();
            this.chkHasM.Properties.BeginInit();
            base.SuspendLayout();
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(0x123, 250);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new Size(80, 0x18);
            this.simpleButton2.TabIndex = 0x17;
            this.simpleButton2.Text = "取消";
            this.btnOK.Location = new System.Drawing.Point(0xd3, 250);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x48, 0x18);
            this.btnOK.TabIndex = 0x16;
            this.btnOK.Text = "导入";
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnSelectOutLocation.Image = (Image) resources.GetObject("btnSelectOutLocation.Image");
            this.btnSelectOutLocation.Location = new System.Drawing.Point(0x161, 0x7e);
            this.btnSelectOutLocation.Name = "btnSelectOutLocation";
            this.btnSelectOutLocation.Size = new Size(0x18, 0x18);
            this.btnSelectOutLocation.TabIndex = 0x15;
            this.btnSelectOutLocation.Click += new EventHandler(this.btnSelectOutLocation_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 0x83);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "输出位置";
            this.txtOutLocation.EditValue = "";
            this.txtOutLocation.Location = new System.Drawing.Point(0x6a, 0x7e);
            this.txtOutLocation.Name = "txtOutLocation";
            this.txtOutLocation.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtOutLocation.Properties.Appearance.Options.UseBackColor = true;
            this.txtOutLocation.Properties.ReadOnly = true;
            this.txtOutLocation.Size = new Size(0xe7, 0x15);
            this.txtOutLocation.TabIndex = 0x13;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 0x18;
            this.label1.Text = "坐标文件";
            this.txtCoordinateFile.EditValue = "";
            this.txtCoordinateFile.Location = new System.Drawing.Point(0x6a, 12);
            this.txtCoordinateFile.Name = "txtCoordinateFile";
            this.txtCoordinateFile.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtCoordinateFile.Properties.Appearance.Options.UseBackColor = true;
            this.txtCoordinateFile.Properties.ReadOnly = true;
            this.txtCoordinateFile.Size = new Size(0xe7, 0x15);
            this.txtCoordinateFile.TabIndex = 0x19;
            this.btnOpenCoordinateFile.Image = (Image) resources.GetObject("btnOpenCoordinateFile.Image");
            this.btnOpenCoordinateFile.Location = new System.Drawing.Point(0x161, 12);
            this.btnOpenCoordinateFile.Name = "btnOpenCoordinateFile";
            this.btnOpenCoordinateFile.Size = new Size(0x18, 0x18);
            this.btnOpenCoordinateFile.TabIndex = 0x1a;
            this.btnOpenCoordinateFile.Click += new EventHandler(this.btnOpenCoordinateFile_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0x37);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x4d, 12);
            this.label3.TabIndex = 0x1b;
            this.label3.Text = "几何数据类型";
            this.cboGeometryType.EditValue = "多义线";
            this.cboGeometryType.Location = new System.Drawing.Point(0x6a, 50);
            this.cboGeometryType.Name = "cboGeometryType";
            this.cboGeometryType.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.cboGeometryType.Properties.Items.AddRange(new object[] { "点", "多点", "多义线", "多边形" });
            this.cboGeometryType.Size = new Size(0xb1, 0x15);
            this.cboGeometryType.TabIndex = 0x1c;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 0x5d);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x59, 12);
            this.label4.TabIndex = 0x1d;
            this.label4.Text = "坐标系统(可选)";
            this.btnSelectCoordinate.Image = (Image) resources.GetObject("btnSelectCoordinate.Image");
            this.btnSelectCoordinate.Location = new System.Drawing.Point(0x161, 0x58);
            this.btnSelectCoordinate.Name = "btnSelectCoordinate";
            this.btnSelectCoordinate.Size = new Size(0x18, 0x18);
            this.btnSelectCoordinate.TabIndex = 0x1f;
            this.btnSelectCoordinate.Click += new EventHandler(this.btnSelectCoordinate_Click);
            this.txtCoordinate.EditValue = "";
            this.txtCoordinate.Location = new System.Drawing.Point(0x6a, 0x58);
            this.txtCoordinate.Name = "txtCoordinate";
            this.txtCoordinate.Properties.Appearance.BackColor = SystemColors.Control;
            this.txtCoordinate.Properties.Appearance.Options.UseBackColor = true;
            this.txtCoordinate.Properties.ReadOnly = true;
            this.txtCoordinate.Size = new Size(0xe7, 0x15);
            this.txtCoordinate.TabIndex = 30;
            this.txtFeatureClassName.EditValue = "";
            this.txtFeatureClassName.Location = new System.Drawing.Point(0x6a, 0xa4);
            this.txtFeatureClassName.Name = "txtFeatureClassName";
            this.txtFeatureClassName.Properties.Appearance.BackColor = SystemColors.ControlLightLight;
            this.txtFeatureClassName.Properties.Appearance.Options.UseBackColor = true;
            this.txtFeatureClassName.Size = new Size(0xe7, 0x15);
            this.txtFeatureClassName.TabIndex = 0x21;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 0xa9);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x29, 12);
            this.label5.TabIndex = 0x20;
            this.label5.Text = "要素名";
            this.chkHasZ.Location = new System.Drawing.Point(0xaf, 200);
            this.chkHasZ.Name = "chkHasZ";
            this.chkHasZ.Properties.Caption = "包含Z值";
            this.chkHasZ.Size = new Size(0x4b, 0x13);
            this.chkHasZ.TabIndex = 0x22;
            this.chkHasM.Location = new System.Drawing.Point(0x114, 200);
            this.chkHasM.Name = "chkHasM";
            this.chkHasM.Properties.Caption = "包含M值";
            this.chkHasM.Size = new Size(0x4b, 0x13);
            this.chkHasM.TabIndex = 0x23;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x18e, 0x129);
            base.Controls.Add(this.chkHasM);
            base.Controls.Add(this.chkHasZ);
            base.Controls.Add(this.txtFeatureClassName);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.btnSelectCoordinate);
            base.Controls.Add(this.txtCoordinate);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cboGeometryType);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.btnOpenCoordinateFile);
            base.Controls.Add(this.txtCoordinateFile);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.simpleButton2);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnSelectOutLocation);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtOutLocation);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.Icon = (Icon) resources.GetObject("$Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmImportCoordinateFile";
            this.Text = "导入坐标数据";
            this.txtOutLocation.Properties.EndInit();
            this.txtCoordinateFile.Properties.EndInit();
            this.cboGeometryType.Properties.EndInit();
            this.txtCoordinate.Properties.EndInit();
            this.txtFeatureClassName.Properties.EndInit();
            this.chkHasZ.Properties.EndInit();
            this.chkHasM.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
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

