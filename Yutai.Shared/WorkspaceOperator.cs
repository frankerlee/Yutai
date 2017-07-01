using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.Shared
{
    public class WorkspaceOperator
    {
        // Methods
        public static IFeatureClass CreateAnnoFeatureClass(string string_0, IFeatureDataset ifeatureDataset_0,
            double double_0)
        {
            IObjectClassDescription description = new AnnotationFeatureClassDescription();
            IFeatureClassDescription description2 = (IFeatureClassDescription) description;
            IFields requiredFields = description.RequiredFields;
            IFeatureWorkspaceAnno workspace = (IFeatureWorkspaceAnno) ifeatureDataset_0.Workspace;
            IGraphicsLayerScale referenceScale = new GraphicsLayerScale
            {
                ReferenceScale = double_0,
                Units = esriUnits.esriMeters
            };
            UID instanceCLSID = description.InstanceCLSID;
            UID classExtensionCLSID = description.ClassExtensionCLSID;
            ISymbolCollection symbolCollection = new SymbolCollection();
            symbolCollection.set_Symbol(0, (ISymbol) MakeTextSymbol("宋体", 3.0));
            symbolCollection.set_Symbol(1, (ISymbol) MakeTextSymbol("宋体", 3.5));
            symbolCollection.set_Symbol(2, (ISymbol) MakeTextSymbol("宋体", 3.0));
            symbolCollection.set_Symbol(3, (ISymbol) MakeTextSymbol("宋体", 2.5));
            symbolCollection.set_Symbol(4, (ISymbol) MakeTextSymbol("黑体", 2.0));
            symbolCollection.set_Symbol(5, (ISymbol) MakeTextSymbol("黑体", 1.5));
            symbolCollection.set_Symbol(6, (ISymbol) MakeTextSymbol("宋体", 1.0));
            symbolCollection.set_Symbol(7, (ISymbol) MakeTextSymbol("宋体", 0.4));
            symbolCollection.set_Symbol(8, (ISymbol) MakeTextSymbol("宋体", 0.3));
            symbolCollection.set_Symbol(9, (ISymbol) MakeTextSymbol("黑体", 1.0));
            symbolCollection.set_Symbol(10, (ISymbol) MakeTextSymbol("宋体", 0.5));
            symbolCollection.set_Symbol(11, (ISymbol) MakeTextSymbol("宋体", 0.6));
            symbolCollection.set_Symbol(12, (ISymbol) MakeTextSymbol("宋体", 0.7));
            symbolCollection.set_Symbol(13, (ISymbol) MakeTextSymbol("宋体", 0.8));
            symbolCollection.set_Symbol(14, (ISymbol) MakeTextSymbol("宋体", 0.9));
            symbolCollection.set_Symbol(15, (ISymbol) MakeTextSymbol("宋体", 1.2));
            return workspace.CreateAnnotationClass(string_0, requiredFields, instanceCLSID, classExtensionCLSID,
                description2.ShapeFieldName, "", ifeatureDataset_0, null, null, referenceScale, symbolCollection, true);
        }

        public static IFeatureClass CreateFeatureClass(object object_0, string string_0,
            ISpatialReference ispatialReference_0, esriFeatureType esriFeatureType_0,
            esriGeometryType esriGeometryType_0, IFields ifields_0, UID uid_0, UID uid_1, string string_1)
        {
            if (object_0 == null)
            {
                throw new Exception("[objectWorkspace] 不能为空");
            }
            if (!((object_0 is IWorkspace) || (object_0 is IFeatureDataset)))
            {
                throw new Exception("[objectWorkspace] 必须是IWorkspace或IFeatureDataset");
            }
            if (string_0 == "")
            {
                throw new Exception("[name] cannot be empty");
            }
            if ((object_0 is IWorkspace) && (ispatialReference_0 == null))
            {
                throw new Exception("[spatialReference] cannot be null for StandAlong FeatureClasses");
            }
            if (uid_0 == null)
            {
                uid_0 = new UID();
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
                        uid_1 = new UID();
                        uid_1.Value = "{24429589-D711-11D2-9F41-00C04F6BC6A5}";
                        break;

                    case esriFeatureType.esriFTDimension:
                        uid_1 = new UID();
                        uid_1.Value = "{48F935E2-DA66-11D3-80CE-00C04F601565}";
                        break;
                }
            }
            if (ifields_0 == null)
            {
                ifields_0 = new Fields();
                IFieldsEdit edit = (IFieldsEdit) ifields_0;
                IGeometryDef def = new GeometryDef();
                IGeometryDefEdit edit2 = (IGeometryDefEdit) def;
                edit2.GeometryType_2 = esriGeometryType_0;
                edit2.GridCount_2 = 1;
                edit2.set_GridSize(0, 8555.04939799);
                edit2.AvgNumPoints_2 = 2;
                edit2.HasM_2 = false;
                edit2.HasZ_2 = false;
                if (object_0 is IWorkspace)
                {
                    edit2.SpatialReference_2 = ispatialReference_0;
                }
                IField field = new Field();
                IFieldEdit edit3 = (IFieldEdit) field;
                edit3.Name_2 = "OBJECTID";
                edit3.AliasName_2 = "OBJECTID";
                edit3.Type_2 = esriFieldType.esriFieldTypeOID;
                edit.AddField(field);
                IField field2 = new Field();
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
                return workspace2.CreateFeatureClass(string_0, ifields_0, uid_0, uid_1, esriFeatureType_0,
                    shapeFieldName, string_1);
            }
            if (object_0 is IFeatureDataset)
            {
                class2 = ((IFeatureDataset) object_0).CreateFeatureClass(string_0, ifields_0, uid_0, uid_1,
                    esriFeatureType_0, shapeFieldName, string_1);
            }
            return class2;
        }

        public static IDataset CreateFeatureDataSet(IWorkspace iworkspace_0, string string_0,
            ISpatialReference ispatialReference_0)
        {
            if (iworkspace_0 == null)
            {
                return null;
            }
            if (string_0 == null)
            {
                return null;
            }
            if (string_0 == "")
            {
                return null;
            }
            if (ispatialReference_0 == null)
            {
                return null;
            }
            IWorkspace workspace = iworkspace_0;
            IFeatureWorkspace workspace2 = (IFeatureWorkspace) workspace;
            IDataset dataset2 = null;
            try
            {
                dataset2 = workspace2.CreateFeatureDataset(string_0, ispatialReference_0);
            }
            catch (Exception)
            {
            }
            return dataset2;
        }

        public static IWorkspace CreateFileGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory o = new FileGDBWorkspaceFactory();
            try
            {
                workspace =
                (o.Create(System.IO.Path.GetDirectoryName(string_0),
                    System.IO.Path.GetFileNameWithoutExtension(string_0), null, 0) as IName).Open() as IWorkspace;
            }
            catch (COMException exception)
            {
                MessageBox.Show("错误代码:" + exception.ErrorCode.ToString() + "\r\n" + exception.Message);
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(o);
            o = null;
            return workspace;
        }

        public static IWorkspace CreateLocalGDB(string string_0)
        {
            if (System.IO.Path.GetExtension(string_0).ToLower() == ".mdb")
            {
                return CreatePersonGDB(string_0);
            }
            return CreateFileGDB(string_0);
        }

        public static IWorkspace CreatePersonGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory o = new AccessWorkspaceFactory();
            try
            {
                workspace =
                (o.Create(System.IO.Path.GetDirectoryName(string_0),
                    System.IO.Path.GetFileNameWithoutExtension(string_0), null, 0) as IName).Open() as IWorkspace;
            }
            catch (COMException exception)
            {
                MessageBox.Show("错误代码:" + exception.ErrorCode.ToString() + "\r\n" + exception.Message);
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(o);
            o = null;
            return workspace;
        }

        public static string GetFinalName(IWorkspace iworkspace_0, esriDatasetType esriDatasetType_0, string string_0)
        {
            string str2;
            string name = string_0;
            int num = 1;
            if (esriDatasetType_0 == esriDatasetType.esriDTFeatureDataset)
            {
                try
                {
                    while ((iworkspace_0 as IFeatureWorkspace).OpenFeatureDataset(name) != null)
                    {
                        name = string_0 + "_" + num.ToString();
                        num++;
                    }
                }
                catch
                {
                }
                return name;
            }
            IFieldChecker checker = new FieldChecker
            {
                ValidateWorkspace = iworkspace_0
            };
            checker.ValidateTableName(string_0, out str2);
            name = str2;
            if ((iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) ||
                (iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
            {
                while (((IWorkspace2) iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, name))
                {
                    name = str2 + "_" + num.ToString();
                    num++;
                }
                return name;
            }
            if (iworkspace_0.Type != esriWorkspaceType.esriFileSystemWorkspace)
            {
            }
            return name;
        }

        public static string GetFinalName2(IWorkspace iworkspace_0, esriDatasetType esriDatasetType_0, string string_0,
            string string_1, string string_2)
        {
            string str2;
            string name = string_0 + string_1 + string_2;
            int num = 1;
            if (esriDatasetType_0 == esriDatasetType.esriDTFeatureDataset)
            {
                try
                {
                    while ((iworkspace_0 as IFeatureWorkspace).OpenFeatureDataset(name) != null)
                    {
                        name = string_0 + string_1 + string_2 + "_" + num.ToString();
                        num++;
                    }
                }
                catch
                {
                }
                return name;
            }
            IFieldChecker checker = new FieldChecker
            {
                ValidateWorkspace = iworkspace_0
            };
            checker.ValidateTableName(string_1, out str2);
            name = string_0 + str2 + string_2;
            if ((iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) ||
                (iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
            {
                while (((IWorkspace2) iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, name))
                {
                    name = string_0 + str2 + string_2 + "_" + num.ToString();
                    num++;
                }
                return name;
            }
            if (iworkspace_0.Type != esriWorkspaceType.esriFileSystemWorkspace)
            {
            }
            return name;
        }

        public static string GetWorkspaceConnectInfo(IPropertySet ipropertySet_0)
        {
            string str2;
            if (ipropertySet_0 == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            if (ipropertySet_0.Count < 5)
            {
                str2 = Convert.ToString(ipropertySet_0.GetProperty("Database")).ToLower();
                builder.Append(str2);
            }
            else
            {
                str2 = Convert.ToString(ipropertySet_0.GetProperty("DBCLIENT")).ToLower();
                builder.Append(str2);
                builder.Append(",");
                str2 = Convert.ToString(ipropertySet_0.GetProperty("DB_CONNECTION_PROPERTIES")).ToLower();
                builder.Append(str2);
                builder.Append(",");
                str2 = Convert.ToString(ipropertySet_0.GetProperty("Database")).ToLower();
                builder.Append(str2);
            }
            return builder.ToString();
        }

        public static string GetWorkspaceConnectInfo(IWorkspace iworkspace_0)
        {
            if (iworkspace_0 == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            if ((iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace) ||
                (iworkspace_0.Type == esriWorkspaceType.esriFileSystemWorkspace))
            {
                builder.Append(iworkspace_0.PathName);
            }
            else
            {
                string str2 = Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("DBCLIENT")).ToLower();
                builder.Append(str2);
                builder.Append(",");
                str2 =
                    Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("DB_CONNECTION_PROPERTIES"))
                        .ToLower();
                builder.Append(str2);
                builder.Append(",");
                str2 = Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("Database")).ToLower();
                builder.Append(str2);
            }
            return builder.ToString();
        }

        public static bool IsConnectedToGeodatabase(IWorkspace iworkspace_0)
        {
            IWorkspaceProperties properties = (IWorkspaceProperties) iworkspace_0;
            return
                Convert.ToBoolean(
                    properties.get_Property(esriWorkspacePropertyGroupType.esriWorkspacePropertyGroup, 7).PropertyValue);
        }

        public static bool IsConnectedToGeodatabase(IWorkspaceName iworkspaceName_0)
        {
            bool flag = false;
            try
            {
                object property = iworkspaceName_0.ConnectionProperties.GetProperty("IS_GEODATABASE");
                if (property == null)
                {
                    IWorkspace workspace = (iworkspaceName_0 as IName).Open() as IWorkspace;
                    return IsConnectedToGeodatabase(workspace);
                }
                flag = Convert.ToBoolean(property);
            }
            catch (Exception)
            {
            }
            return flag;
        }

        public static ITextSymbol MakeTextSymbol(string string_0, double double_0)
        {
            try
            {
                ISimpleTextSymbol symbol = new TextSymbol() as ISimpleTextSymbol;
                IFontDisp disp = (IFontDisp) new StdFont();
                disp.Name = string_0;
                symbol.Font = disp;
                symbol.Angle = 0.0;
                symbol.RightToLeft = false;
                symbol.Size = double_0;
                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                return symbol;
            }
            catch
            {
                return null;
            }
        }

        public static IWorkspace OpenFileGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory o = new FileGDBWorkspaceFactory() as IWorkspaceFactory;
            try
            {
                workspace = o.OpenFromFile(string_0, 0);
            }
            catch (COMException exception)
            {
                MessageBox.Show("错误代码:" + exception.ErrorCode.ToString() + "\r\n" + exception.Message);
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(o);
            o = null;
            return workspace;
        }

        public static IWorkspace OpenPersonGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory o = new AccessWorkspaceFactory();
            try
            {
                workspace = o.OpenFromFile(string_0, 0);
            }
            catch (COMException exception)
            {
                MessageBox.Show("错误代码:" + exception.ErrorCode.ToString() + "\r\n" + exception.Message);
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(o);
            o = null;
            return workspace;
        }

        public static IWorkspace OpenSDEGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory o = new SdeWorkspaceFactory();
            try
            {
                workspace = o.OpenFromFile(string_0, 0);
            }
            catch (COMException exception)
            {
                MessageBox.Show("错误代码:" + exception.ErrorCode.ToString() + "\r\n" + exception.Message);
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(o);
            o = null;
            return workspace;
        }

        public static IWorkspace OpenWorkspace(IPropertySet ipropertySet_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory factory;
            Exception exception;
            if (ipropertySet_0.Count == 1)
            {
                object obj2;
                object obj3;
                string str2 = "";
                ipropertySet_0.GetAllProperties(out obj2, out obj3);
                if (((string[]) obj2)[0] == "DATABASE")
                {
                    str2 = System.IO.Path.GetExtension(((object[]) obj3)[0].ToString()).ToLower();
                }
                if (str2 == ".mdb")
                {
                    factory = new AccessWorkspaceFactory();
                    try
                    {
                        workspace = factory.Open(ipropertySet_0, 0);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        MessageBox.Show(exception.Message);
                    }
                    return workspace;
                }
                if (str2 == ".gdb")
                {
                    factory = new FileGDBWorkspaceFactory();
                    try
                    {
                        workspace = factory.Open(ipropertySet_0, 0);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        MessageBox.Show(exception.Message);
                    }
                }
                return workspace;
            }
            factory = new SdeWorkspaceFactory();
            try
            {
                workspace = factory.Open(ipropertySet_0, 0);
            }
            catch (Exception exception3)
            {
                exception = exception3;
                MessageBox.Show(exception.Message);
            }
            return workspace;
        }

        public static IWorkspace OpenWorkspace(string string_0)
        {
            string str = System.IO.Path.GetExtension(string_0).ToLower();
            switch (str)
            {
                case ".mdb":
                    return OpenPersonGDB(string_0);

                case ".sde":
                    return OpenSDEGDB(string_0);
            }
            if ((str == null) || (str.Length == 0))
            {
                return OpenFileGDB(string_0);
            }
            return null;
        }

        public static bool WorkspaceIsSame(IWorkspace iworkspace_0, IWorkspace iworkspace_1)
        {
            if ((iworkspace_0 == null) || (iworkspace_1 == null))
            {
                return true;
            }
            if (iworkspace_0.Type != iworkspace_1.Type)
            {
                return false;
            }
            bool flag2 = false;
            if ((iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace) ||
                (iworkspace_0.Type == esriWorkspaceType.esriFileSystemWorkspace))
            {
                flag2 = string.Compare(iworkspace_0.PathName, iworkspace_1.PathName, true) == 0;
            }
            else
            {
                string str = Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("DBCLIENT")).ToLower();
                string str2 = Convert.ToString(iworkspace_1.ConnectionProperties.GetProperty("DBCLIENT")).ToLower();
                if (str == str2)
                {
                    string str3 = str;
                    str =
                        Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("DB_CONNECTION_PROPERTIES"))
                            .ToLower();
                    str2 =
                        Convert.ToString(iworkspace_1.ConnectionProperties.GetProperty("DB_CONNECTION_PROPERTIES"))
                            .ToLower();
                    if (str == str2)
                    {
                        if (str3 == "sqlserver")
                        {
                            str = Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("Database")).ToLower();
                            str2 = Convert.ToString(iworkspace_1.ConnectionProperties.GetProperty("Database")).ToLower();
                            flag2 = str == str2;
                        }
                        else
                        {
                            flag2 = true;
                        }
                    }
                }
            }
            return flag2;
        }
    }
}