using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class MiTab2DataConvert : IFeatureProgress_Event, IFeatureDataConverter, IFeatureDataConverter2
    {
        private ISpatialReference ispatialReference_0 = null;

        private IFeatureProgress_StepEventHandler ifeatureProgress_StepEventHandler_0;

        public IFeatureClass FeatureClass { get; protected set; }

        public MiTab2DataConvert()
        {
        }

        public bool _tab2(string string_0, IFeatureWorkspace ifeatureWorkspace_0)
        {
            bool flag;
            bool flag1;
            IFeatureClass featureClass = null;
            IFeatureClass featureClass1 = null;
            IFeatureClass featureClass2 = null;
            IFeatureClass featureClass3 = null;
            IFeatureClass featureClass4 = null;
            string string0 = string_0;
            if (File.Exists(string0))
            {
                IntPtr intPtr = TabRead._mitab_c_open(string0);
                if (intPtr.ToInt32() != 0)
                {
                    try
                    {
                        try
                        {
                            int num = TabRead._mitab_c_next_feature_id(intPtr, -1);
                            this.FeatureClass = null;
                            while (num != -1)
                            {
                                IntPtr intPtr1 = TabRead._mitab_c_read_feature(intPtr, num);
                                int num1 = TabRead._mitab_c_get_type(intPtr1);
                                switch (num1)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    {
                                        if (featureClass == null)
                                        {
                                            featureClass = this.method_1(string0, num1, intPtr, ifeatureWorkspace_0,
                                                out flag1);
                                            if (flag1)
                                            {
                                                flag = false;
                                                return flag;
                                            }
                                        }
                                        this.FeatureClass = featureClass;
                                        goto case 9;
                                    }
                                    case 4:
                                    {
                                        if (featureClass4 == null)
                                        {
                                            featureClass4 = this.method_1(string0, num1, intPtr, ifeatureWorkspace_0,
                                                out flag1);
                                            if (flag1)
                                            {
                                                flag = false;
                                                return flag;
                                            }
                                        }
                                        this.FeatureClass = featureClass4;
                                        goto case 9;
                                    }
                                    case 5:
                                    case 6:
                                    {
                                        if (featureClass2 == null)
                                        {
                                            featureClass2 = this.method_1(string0, num1, intPtr, ifeatureWorkspace_0,
                                                out flag1);
                                            if (flag1)
                                            {
                                                flag = false;
                                                return flag;
                                            }
                                        }
                                        this.FeatureClass = featureClass2;
                                        goto case 9;
                                    }
                                    case 7:
                                    {
                                        if (featureClass3 == null)
                                        {
                                            featureClass3 = this.method_1(string0, num1, intPtr, ifeatureWorkspace_0,
                                                out flag1);
                                            if (flag1)
                                            {
                                                flag = false;
                                                return flag;
                                            }
                                        }
                                        this.FeatureClass = featureClass3;
                                        goto case 9;
                                    }
                                    case 8:
                                    case 9:
                                    {
                                        if (this.FeatureClass != null)
                                        {
                                            try
                                            {
                                                IFeature feature = this.FeatureClass.CreateFeature();
                                                IGeometry geometry = this.CreateGeometry(num1, intPtr1);
                                                if (geometry is ITopologicalOperator)
                                                {
                                                    (geometry as ITopologicalOperator).Simplify();
                                                }
                                                try
                                                {
                                                    this.method_2(intPtr1, feature);
                                                }
                                                catch (Exception exception)
                                                {
                                                    Logger.Current.Error("", exception, "");
                                                }
                                                int num2 = TabRead._mitab_c_get_field_count(intPtr);
                                                feature.Fields.FindField("Shape");
                                                for (int i = 0; i < num2; i++)
                                                {
                                                    string str = TabRead._mitab_c_get_field_name(intPtr, i);
                                                    int num3 = TabRead._mitab_c_get_field_type(intPtr, i);
                                                    string str1 = TabRead._mitab_c_get_field_as_string(intPtr1, i);
                                                    int num4 = feature.Fields.FindField(str);
                                                    if (num4 == -1)
                                                    {
                                                        try
                                                        {
                                                            if (num3 != 6)
                                                            {
                                                                feature.Value[num4] = str1;
                                                            }
                                                            else if (str1.Length == 8)
                                                            {
                                                                str1 = str1.Insert(6, "-");
                                                                str1 = str1.Insert(4, "-");
                                                                feature.Value[num4] = DateTime.Parse(str1);
                                                            }
                                                        }
                                                        catch
                                                        {
                                                        }
                                                    }
                                                    feature.Shape = geometry;
                                                }
                                                feature.Store();
                                            }
                                            catch (Exception exception1)
                                            {
                                                Logger.Current.Error("", exception1, "");
                                            }
                                            if (this.ifeatureProgress_StepEventHandler_0 != null)
                                            {
                                                this.ifeatureProgress_StepEventHandler_0();
                                            }
                                        }
                                        TabRead._mitab_c_destroy_feature(intPtr1);
                                        num = TabRead._mitab_c_next_feature_id(intPtr, num);
                                        continue;
                                    }
                                    case 10:
                                    {
                                        if (featureClass1 == null)
                                        {
                                            featureClass1 = this.method_1(string0, num1, intPtr, ifeatureWorkspace_0,
                                                out flag1);
                                            if (flag1)
                                            {
                                                flag = false;
                                                return flag;
                                            }
                                        }
                                        this.FeatureClass = featureClass1;
                                        goto case 9;
                                    }
                                    default:
                                    {
                                        goto case 9;
                                    }
                                }
                            }
                        }
                        catch (Exception exception2)
                        {
                            Logger.Current.Error("", exception2, "");
                        }
                    }
                    finally
                    {
                    }
                    TabRead._mitab_c_close(intPtr);
                    flag = true;
                }
                else
                {
                    MessageBox.Show(string.Concat("无法打开 -", string0, "- 文件"));
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }
            return flag;
        }


        public void AddTabField(IFieldsEdit ifieldsEdit_0, IntPtr intptr_0)
        {
            int num = TabRead._mitab_c_get_field_count(intptr_0);
            for (int i = 0; i < num; i++)
            {
                IFieldEdit field = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                field.Name_2 = TabRead._mitab_c_get_field_name(intptr_0, i);
                int num3 = TabRead._mitab_c_get_field_type(intptr_0, i);
                int num4 = TabRead._mitab_c_get_field_width(intptr_0, i);
                int num5 = TabRead._mitab_c_get_field_precision(intptr_0, i);
                switch (num3)
                {
                    case 1:
                    case 7:
                        field.Type_2 = esriFieldType.esriFieldTypeString;
                        field.Length_2 = num4;
                        break;

                    case 2:
                        field.Type_2 = esriFieldType.esriFieldTypeInteger;
                        field.Precision_2 = num4;
                        break;

                    case 3:
                        field.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                        field.Precision_2 = num4;
                        break;

                    case 4:
                        field.Type_2 = esriFieldType.esriFieldTypeDouble;
                        field.Precision_2 = num4;
                        field.Scale_2 = num5;
                        break;

                    case 5:
                        field.Type_2 = esriFieldType.esriFieldTypeSingle;
                        field.Precision_2 = num4;
                        break;

                    case 6:
                        field.Type_2 = esriFieldType.esriFieldTypeDate;
                        break;

                    default:
                    {
                        continue;
                    }
                }
                ifieldsEdit_0.AddField(field);
            }
        }


        public void AddTabField(int int_0, IFieldsEdit ifieldsEdit_0)
        {
            IFieldEdit fieldClass;
            switch (int_0)
            {
                case 1:
                case 2:
                case 3:
                case 10:
                {
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "S_SID";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "S_Size";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "S_Color";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    return;
                }
                case 4:
                {
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_Text";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
                    fieldClass.Length_2 = 100;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_Font";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
                    fieldClass.Length_2 = 100;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_Height";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeDouble;
                    fieldClass.Precision_2 = 5;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_Weight";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeDouble;
                    fieldClass.Precision_2 = 5;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_BKC";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_FKC";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_Angle";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeDouble;
                    fieldClass.Precision_2 = 5;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_Justi";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_LT";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "T_Space";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    return;
                }
                case 5:
                case 6:
                {
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "L_PID";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "L_Width";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "L_Color";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    return;
                }
                case 7:
                {
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "P_BID";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "P_BBkC";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "P_BFRC";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "P_BTTP";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "P_PID";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "P_PC";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
                    fieldClass.Name_2 = "P_PW";
                    fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
                    ifieldsEdit_0.AddField(fieldClass);
                    return;
                }
                case 8:
                case 9:
                {
                    return;
                }
                default:
                {
                    return;
                }
            }
        }

        public IEnumInvalidObject ConvertFeatureClass(IFeatureClassName ifeatureClassName_0, IQueryFilter iqueryFilter_0,
            IFeatureDatasetName ifeatureDatasetName_0, IFeatureClassName ifeatureClassName_1,
            IGeometryDef igeometryDef_0, IFields ifields_0, string string_0, int int_0, int int_1)
        {
            return null;
        }

        public void ConvertFeatureDataset(IFeatureDatasetName ifeatureDatasetName_0,
            IFeatureDatasetName ifeatureDatasetName_1, IGeometryDef igeometryDef_0, string string_0, int int_0,
            int int_1)
        {
        }

        public IEnumInvalidObject ConvertTable(IDatasetName idatasetName_0, IQueryFilter iqueryFilter_0,
            IDatasetName idatasetName_1, IFields ifields_0, string string_0, int int_0, int int_1)
        {
            return null;
        }

        public IGeometry CreateGeometry(int int_0, IntPtr intptr_0)
        {
            IGeometry geometry;
            switch (int_0)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                {
                    geometry = this.CreatePoint(intptr_0);
                    break;
                }
                case 5:
                case 6:
                {
                    geometry = this.CreatePolyline(intptr_0);
                    break;
                }
                case 7:
                {
                    geometry = this.CreatePolygon(intptr_0);
                    break;
                }
                case 8:
                case 9:
                {
                    geometry = null;
                    break;
                }
                case 10:
                {
                    geometry = this.CreateMultipoint(intptr_0);
                    break;
                }
                default:
                {
                    goto case 9;
                }
            }
            return geometry;
        }

        public IMultipoint CreateMultipoint(IntPtr intptr_0)
        {
            IMultipoint multipointClass = new Multipoint() as IMultipoint;
            int num = TabRead._mitab_c_get_parts(intptr_0);
            object value = Missing.Value;
            for (int i = 0; i < num; i++)
            {
                int num1 = TabRead._mitab_c_get_vertex_count(intptr_0, i);
                for (int j = 0; j < num1; j++)
                {
                    IPoint pointClass = new Point()
                    {
                        X = TabRead._mitab_c_get_vertex_x(intptr_0, i, j),
                        Y = TabRead._mitab_c_get_vertex_y(intptr_0, i, j)
                    };
                    (multipointClass as IPointCollection).AddPoint(pointClass, ref value, ref value);
                }
            }
            return multipointClass;
        }

        public IPoint CreatePoint(IntPtr intptr_0)
        {
            TabRead._mitab_c_get_parts(intptr_0);
            IPoint pointClass = new Point()
            {
                X = TabRead._mitab_c_get_vertex_x(intptr_0, 0, 0),
                Y = TabRead._mitab_c_get_vertex_y(intptr_0, 0, 0)
            };
            return pointClass;
        }

        public IPolygon CreatePolygon(IntPtr intptr_0)
        {
            IPolygon polygonClass = new Polygon() as IPolygon;
            int num = TabRead._mitab_c_get_parts(intptr_0);
            object value = Missing.Value;
            for (int i = 0; i < num; i++)
            {
                int num1 = TabRead._mitab_c_get_vertex_count(intptr_0, i);
                IRing ringClass = new ESRI.ArcGIS.Geometry.Ring() as IRing;
                for (int j = 0; j < num1; j++)
                {
                    IPoint pointClass = new Point()
                    {
                        X = TabRead._mitab_c_get_vertex_x(intptr_0, i, j),
                        Y = TabRead._mitab_c_get_vertex_y(intptr_0, i, j)
                    };
                    (ringClass as IPointCollection).AddPoint(pointClass, ref value, ref value);
                }
                ringClass.Close();
                (polygonClass as IGeometryCollection).AddGeometry(ringClass, ref value, ref value);
            }
            return polygonClass;
        }

        public IPolyline CreatePolyline(IntPtr intptr_0)
        {
            IPolyline polylineClass = new Polyline() as IPolyline;
            int num = TabRead._mitab_c_get_parts(intptr_0);
            object value = Missing.Value;
            for (int i = 0; i < num; i++)
            {
                int num1 = TabRead._mitab_c_get_vertex_count(intptr_0, i);
                IPath pathClass = new ESRI.ArcGIS.Geometry.Path() as IPath;
                for (int j = 0; j < num1; j++)
                {
                    IPoint pointClass = new Point()
                    {
                        X = TabRead._mitab_c_get_vertex_x(intptr_0, i, j),
                        Y = TabRead._mitab_c_get_vertex_y(intptr_0, i, j)
                    };
                    (pathClass as IPointCollection).AddPoint(pointClass, ref value, ref value);
                }
                (polylineClass as IGeometryCollection).AddGeometry(pathClass, ref value, ref value);
            }
            return polylineClass;
        }

        IEnumInvalidObject ESRI.ArcGIS.Geodatabase.IFeatureDataConverter2.ConvertFeatureClass(
            IDatasetName idatasetName_0, IQueryFilter iqueryFilter_0, ISelectionSet iselectionSet_0,
            IFeatureDatasetName ifeatureDatasetName_0, IFeatureClassName ifeatureClassName_0,
            IGeometryDef igeometryDef_0, IFields ifields_0, string string_0, int int_0, int int_1)
        {
            return null;
        }

        IEnumInvalidObject ESRI.ArcGIS.Geodatabase.IFeatureDataConverter2.ConvertTable(IDatasetName idatasetName_0,
            IQueryFilter iqueryFilter_0, ISelectionSet iselectionSet_0, IDatasetName idatasetName_1, IFields ifields_0,
            string string_0, int int_0, int int_1)
        {
            return null;
        }

        private string method_0(IWorkspace iworkspace_0, string string_0)
        {
            string string0;
            int num;
            string fileNameWithoutExtension;
            if ((iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace
                ? false
                : iworkspace_0.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace))
            {
                string str = string.Concat(iworkspace_0.PathName, "\\", string_0);
                string0 = string.Concat(str, ".shp");
                num = 1;
                while (File.Exists(string0))
                {
                    string0 = string.Concat(str, "_", num.ToString(), ".shp");
                    num++;
                }
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(string0);
            }
            else
            {
                IWorkspace2 iworkspace0 = iworkspace_0 as IWorkspace2;
                string0 = string_0;
                num = 1;
                while (iworkspace0.NameExists[esriDatasetType.esriDTFeatureClass, string0])
                {
                    string0 = string.Concat(string_0, "_", num.ToString());
                    num++;
                }
                fileNameWithoutExtension = string0;
            }
            return fileNameWithoutExtension;
        }

        private IFeatureClass method_1(string string_0, int int_0, IntPtr intptr_0,
            IFeatureWorkspace ifeatureWorkspace_0, out bool bool_0)
        {
            IEnumFieldError enumFieldError;
            IFields field;
            IFeatureClass featureClass;
            bool_0 = false;
            try
            {
                IFeatureClass featureClass1 = null;
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(string_0);
                IFeatureClassDescription featureClassDescriptionClass =
                    new FeatureClassDescription() as IFeatureClassDescription;
                string shapeFieldName = featureClassDescriptionClass.ShapeFieldName;
                IFields requiredFields = (featureClassDescriptionClass as IObjectClassDescription).RequiredFields;
                this.AddTabField(requiredFields as IFieldsEdit, intptr_0);
                this.AddTabField(int_0, requiredFields as IFieldsEdit);
                IFields field1 = requiredFields;
                IFieldChecker fieldCheckerClass = new FieldChecker()
                {
                    ValidateWorkspace = ifeatureWorkspace_0 as IWorkspace
                };
                fieldCheckerClass.Validate(field1, out enumFieldError, out field);
                IFieldEdit fieldEdit = field.Field[field.FindField(shapeFieldName)] as IFieldEdit;
                IGeometryDefEdit geometryDef = fieldEdit.GeometryDef as IGeometryDefEdit;
                if (this.ispatialReference_0 == null)
                {
                    string str = TabRead._mitab_c_get_coordsys_xml(intptr_0);
                    if (str == null)
                    {
                        this.ispatialReference_0 = geometryDef.SpatialReference;
                    }
                    else if (str.Length <= 0)
                    {
                        this.ispatialReference_0 = geometryDef.SpatialReference;
                    }
                    else
                    {
                        string str1 = string.Concat(System.IO.Path.GetTempPath(), "\\temp.prj");
                        StreamWriter streamWriter = new StreamWriter(str1);
                        try
                        {
                            streamWriter.Write(str);
                        }
                        finally
                        {
                            if (streamWriter != null)
                            {
                                ((IDisposable) streamWriter).Dispose();
                            }
                        }
                        try
                        {
                            this.ispatialReference_0 =
                                (new SpatialReferenceEnvironment()).CreateESRISpatialReferenceFromPRJFile(str1);
                        }
                        catch
                        {
                            this.ispatialReference_0 = geometryDef.SpatialReference;
                        }
                        try
                        {
                            File.Delete(str1);
                        }
                        catch
                        {
                        }
                    }
                    double num = 0;
                    double num1 = 0;
                    double num2 = 0;
                    double num3 = 0;
                    TabRead._mitab_c_get_mif_bounds(intptr_0, ref num, ref num1, ref num2, ref num3);
                    SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspace_0 as IGeodatabaseRelease,
                        this.ispatialReference_0, false);
                    num = num - 10;
                    num1 = num1 + 10;
                    num2 = num2 - 10;
                    num3 = num3 + 10;
                    this.ispatialReference_0.SetDomain(num, num1, num2, num3);
                }
                geometryDef.SpatialReference_2 = this.ispatialReference_0;
                TabRead._mitab_c_get_feature_count_bytype(intptr_0, int_0);
                switch (int_0)
                {
                    case 1:
                    case 2:
                    case 3:
                    {
                        fileNameWithoutExtension = string.Concat(fileNameWithoutExtension, "_Point");
                        fileNameWithoutExtension = this.method_0(ifeatureWorkspace_0 as IWorkspace,
                            fileNameWithoutExtension);
                        geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                        fieldEdit.GeometryDef_2 = geometryDef;
                        featureClass1 = ifeatureWorkspace_0.CreateFeatureClass(fileNameWithoutExtension, field, null,
                            null, esriFeatureType.esriFTSimple, shapeFieldName, "");
                        break;
                    }
                    case 4:
                    {
                        fileNameWithoutExtension = string.Concat(fileNameWithoutExtension, "_Anno_Point");
                        fileNameWithoutExtension = this.method_0(ifeatureWorkspace_0 as IWorkspace,
                            fileNameWithoutExtension);
                        geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                        fieldEdit.GeometryDef_2 = geometryDef;
                        featureClass1 = ifeatureWorkspace_0.CreateFeatureClass(fileNameWithoutExtension, field, null,
                            null, esriFeatureType.esriFTSimple, shapeFieldName, "");
                        break;
                    }
                    case 5:
                    case 6:
                    {
                        fileNameWithoutExtension = string.Concat(fileNameWithoutExtension, "_Line");
                        fileNameWithoutExtension = this.method_0(ifeatureWorkspace_0 as IWorkspace,
                            fileNameWithoutExtension);
                        geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPolyline;
                        fieldEdit.GeometryDef_2 = geometryDef;
                        featureClass1 = ifeatureWorkspace_0.CreateFeatureClass(fileNameWithoutExtension, field, null,
                            null, esriFeatureType.esriFTSimple, shapeFieldName, "");
                        break;
                    }
                    case 7:
                    {
                        fileNameWithoutExtension = string.Concat(fileNameWithoutExtension, "_Polygon");
                        fileNameWithoutExtension = this.method_0(ifeatureWorkspace_0 as IWorkspace,
                            fileNameWithoutExtension);
                        geometryDef.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                        fieldEdit.GeometryDef_2 = geometryDef;
                        featureClass1 = ifeatureWorkspace_0.CreateFeatureClass(fileNameWithoutExtension, field, null,
                            null, esriFeatureType.esriFTSimple, shapeFieldName, "");
                        break;
                    }
                    case 8:
                    case 9:
                    {
                        featureClass = null;
                        return featureClass;
                    }
                    case 10:
                    {
                        fileNameWithoutExtension = string.Concat(fileNameWithoutExtension, "_MultiPoint");
                        fileNameWithoutExtension = this.method_0(ifeatureWorkspace_0 as IWorkspace,
                            fileNameWithoutExtension);
                        geometryDef.GeometryType_2 = esriGeometryType.esriGeometryMultipoint;
                        fieldEdit.GeometryDef_2 = geometryDef;
                        featureClass1 = ifeatureWorkspace_0.CreateFeatureClass(fileNameWithoutExtension, field, null,
                            null, esriFeatureType.esriFTSimple, shapeFieldName, "");
                        break;
                    }
                    default:
                    {
                        featureClass = null;
                        return featureClass;
                    }
                }
                featureClass = featureClass1;
                return featureClass;
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
                bool_0 = true;
            }
            featureClass = null;
            return featureClass;
        }

        private void method_2(IntPtr intptr_0, IFeature ifeature_0)
        {
            int num = TabRead._mitab_c_get_type(intptr_0);
            IFields fields = ifeature_0.Fields;
            int num1 = -1;
            switch (num)
            {
                case 1:
                case 2:
                case 3:
                case 10:
                {
                    num1 = fields.FindField("S_SID");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_symbol_no(intptr_0);
                    num1 = fields.FindField("S_Size");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_symbol_size(intptr_0);
                    num1 = fields.FindField("S_Color");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_symbol_color(intptr_0);
                    return;
                }
                case 4:
                {
                    num1 = fields.FindField("T_Text");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text(intptr_0);
                    num1 = fields.FindField("T_Font");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_font(intptr_0);
                    num1 = fields.FindField("T_Height");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text_height(intptr_0);
                    num1 = fields.FindField("T_Weight");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text_width(intptr_0);
                    num1 = fields.FindField("T_BKC");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text_bgcolor(intptr_0);
                    num1 = fields.FindField("T_FKC");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text_fgcolor(intptr_0);
                    num1 = fields.FindField("T_Angle");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text_angle(intptr_0);
                    num1 = fields.FindField("T_Justi");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text_justification(intptr_0);
                    num1 = fields.FindField("T_LT");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text_linetype(intptr_0);
                    num1 = fields.FindField("T_Space");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_text_spacing(intptr_0);
                    return;
                }
                case 5:
                case 6:
                {
                    num1 = fields.FindField("L_PID");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_pen_pattern(intptr_0);
                    num1 = fields.FindField("L_Width");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_pen_width(intptr_0);
                    num1 = fields.FindField("L_Color");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_pen_color(intptr_0);
                    return;
                }
                case 7:
                {
                    num1 = fields.FindField("P_BID");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_brush_pattern(intptr_0);
                    num1 = fields.FindField("P_BBkC");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_brush_bgcolor(intptr_0);
                    num1 = fields.FindField("P_BFRC");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_brush_fgcolor(intptr_0);
                    num1 = fields.FindField("P_BTTP");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_brush_transparent(intptr_0);
                    num1 = fields.FindField("P_PID");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_pen_pattern(intptr_0);
                    num1 = fields.FindField("P_PC");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_pen_color(intptr_0);
                    num1 = fields.FindField("P_PW");
                    ifeature_0.Value[num1] = TabRead._mitab_c_get_pen_width(intptr_0);
                    return;
                }
                case 8:
                case 9:
                {
                    return;
                }
                default:
                {
                    return;
                }
            }
        }

        private string method_3(string string_0)
        {
            string str;
            int num = string_0.LastIndexOf("\\");
            if (num >= 0)
            {
                string_0 = string_0.Substring(num + 1, string_0.Length - num - 1);
                num = string_0.LastIndexOf(".");
                str = (num < 0 ? string_0 : string_0.Substring(0, num));
            }
            else
            {
                str = "";
            }
            return str;
        }

        public event IFeatureProgress_StepEventHandler Step
        {
            add
            {
                IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
                IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 =
                    this.ifeatureProgress_StepEventHandler_0;
                do
                {
                    featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
                    IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 =
                        (IFeatureProgress_StepEventHandler) Delegate.Combine(featureProgressStepEventHandler, value);
                    ifeatureProgressStepEventHandler0 =
                        Interlocked.CompareExchange<IFeatureProgress_StepEventHandler>(
                            ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1,
                            featureProgressStepEventHandler);
                } while ((object) ifeatureProgressStepEventHandler0 != (object) featureProgressStepEventHandler);
            }
            remove
            {
                IFeatureProgress_StepEventHandler featureProgressStepEventHandler;
                IFeatureProgress_StepEventHandler ifeatureProgressStepEventHandler0 =
                    this.ifeatureProgress_StepEventHandler_0;
                do
                {
                    featureProgressStepEventHandler = ifeatureProgressStepEventHandler0;
                    IFeatureProgress_StepEventHandler featureProgressStepEventHandler1 =
                        (IFeatureProgress_StepEventHandler) Delegate.Remove(featureProgressStepEventHandler, value);
                    ifeatureProgressStepEventHandler0 =
                        Interlocked.CompareExchange<IFeatureProgress_StepEventHandler>(
                            ref this.ifeatureProgress_StepEventHandler_0, featureProgressStepEventHandler1,
                            featureProgressStepEventHandler);
                } while ((object) ifeatureProgressStepEventHandler0 != (object) featureProgressStepEventHandler);
            }
        }
    }
}