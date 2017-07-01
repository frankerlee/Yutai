using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using stdole;

namespace Yutai.ArcGIS.Common.BaseClasses
{
    public class WorkspaceOperator
    {
        public WorkspaceOperator()
        {
        }

        public static IFeatureClass CreateAnnoFeatureClass(string string_0, IFeatureDataset ifeatureDataset_0,
            double double_0)
        {
            IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescription();
            IFeatureClassDescription featureClassDescription =
                (IFeatureClassDescription) annotationFeatureClassDescriptionClass;
            IFields requiredFields = annotationFeatureClassDescriptionClass.RequiredFields;
            IFeatureWorkspaceAnno workspace = (IFeatureWorkspaceAnno) ifeatureDataset_0.Workspace;
            IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale()
            {
                ReferenceScale = double_0,
                Units = esriUnits.esriMeters
            };
            UID instanceCLSID = annotationFeatureClassDescriptionClass.InstanceCLSID;
            UID classExtensionCLSID = annotationFeatureClassDescriptionClass.ClassExtensionCLSID;
            ISymbolCollection symbolCollectionClass = new SymbolCollection();
            symbolCollectionClass.Symbol[0] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 3);
            symbolCollectionClass.Symbol[1] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 3.5);
            symbolCollectionClass.Symbol[2] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 3);
            symbolCollectionClass.Symbol[3] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 2.5);
            symbolCollectionClass.Symbol[4] = (ISymbol) WorkspaceOperator.MakeTextSymbol("黑体", 2);
            symbolCollectionClass.Symbol[5] = (ISymbol) WorkspaceOperator.MakeTextSymbol("黑体", 1.5);
            symbolCollectionClass.Symbol[6] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 1);
            symbolCollectionClass.Symbol[7] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 0.4);
            symbolCollectionClass.Symbol[8] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 0.3);
            symbolCollectionClass.Symbol[9] = (ISymbol) WorkspaceOperator.MakeTextSymbol("黑体", 1);
            symbolCollectionClass.Symbol[10] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 0.5);
            symbolCollectionClass.Symbol[11] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 0.6);
            symbolCollectionClass.Symbol[12] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 0.7);
            symbolCollectionClass.Symbol[13] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 0.8);
            symbolCollectionClass.Symbol[14] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 0.9);
            symbolCollectionClass.Symbol[15] = (ISymbol) WorkspaceOperator.MakeTextSymbol("宋体", 1.2);
            IFeatureClass featureClass = workspace.CreateAnnotationClass(string_0, requiredFields, instanceCLSID,
                classExtensionCLSID, featureClassDescription.ShapeFieldName, "", ifeatureDataset_0, null, null,
                graphicsLayerScaleClass, symbolCollectionClass, true);
            return featureClass;
        }

        public static IFeatureClass CreateFeatureClass(object object_0, string string_0,
            ISpatialReference ispatialReference_0, esriFeatureType esriFeatureType_0,
            esriGeometryType esriGeometryType_0, IFields ifields_0, UID uid_0, UID uid_1, string string_1)
        {
            if (object_0 == null)
            {
                throw new Exception("[objectWorkspace] 不能为空");
            }
            if ((object_0 is IWorkspace ? false : !(object_0 is IFeatureDataset)))
            {
                throw new Exception("[objectWorkspace] 必须是IWorkspace或IFeatureDataset");
            }
            if (string_0 == "")
            {
                throw new Exception("[name] cannot be empty");
            }
            if ((!(object_0 is IWorkspace) ? false : ispatialReference_0 == null))
            {
                throw new Exception("[spatialReference] cannot be null for StandAlong FeatureClasses");
            }
            if (uid_0 == null)
            {
                uid_0 = new UID();
                switch (esriFeatureType_0)
                {
                    case esriFeatureType.esriFTSimple:
                    {
                        uid_0.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
                        break;
                    }
                    case esriFeatureType.esriFTSimpleJunction:
                    {
                        esriGeometryType_0 = esriGeometryType.esriGeometryPoint;
                        uid_0.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    }
                    case esriFeatureType.esriFTSimpleEdge:
                    {
                        esriGeometryType_0 = esriGeometryType.esriGeometryPolyline;
                        uid_0.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    }
                    case esriFeatureType.esriFTComplexJunction:
                    {
                        uid_0.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}";
                        break;
                    }
                    case esriFeatureType.esriFTComplexEdge:
                    {
                        esriGeometryType_0 = esriGeometryType.esriGeometryPolyline;
                        uid_0.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}";
                        break;
                    }
                    case esriFeatureType.esriFTAnnotation:
                    {
                        esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;
                        uid_0.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}";
                        break;
                    }
                    case esriFeatureType.esriFTDimension:
                    {
                        esriGeometryType_0 = esriGeometryType.esriGeometryPolygon;
                        uid_0.Value = "{496764FC-E0C9-11D3-80CE-00C04F601565}";
                        break;
                    }
                }
            }
            if (uid_1 == null)
            {
                switch (esriFeatureType_0)
                {
                    case esriFeatureType.esriFTAnnotation:
                    {
                        uid_1 = new UID()
                        {
                            Value = "{24429589-D711-11D2-9F41-00C04F6BC6A5}"
                        };
                        break;
                    }
                    case esriFeatureType.esriFTDimension:
                    {
                        uid_1 = new UID()
                        {
                            Value = "{48F935E2-DA66-11D3-80CE-00C04F601565}"
                        };
                        break;
                    }
                }
            }
            if (ifields_0 == null)
            {
                ifields_0 = new Fields();
                IFieldsEdit ifields0 = (IFieldsEdit) ifields_0;
                IGeometryDef geometryDefClass = new GeometryDef();
                IGeometryDefEdit esriGeometryType0 = (IGeometryDefEdit) geometryDefClass;
                esriGeometryType0.GeometryType_2 = esriGeometryType_0;
                esriGeometryType0.GridCount_2 = 1;
                esriGeometryType0.GridSize_2[0] = 8555.04939799;
                esriGeometryType0.AvgNumPoints_2 = 2;
                esriGeometryType0.HasM_2 = false;
                esriGeometryType0.HasZ_2 = false;
                if (object_0 is IWorkspace)
                {
                    esriGeometryType0.SpatialReference_2 = ispatialReference_0;
                }
                IField fieldClass = new Field();
                IFieldEdit fieldEdit = (IFieldEdit) fieldClass;
                fieldEdit.Name_2 = "OBJECTID";
                fieldEdit.AliasName_2 = "OBJECTID";
                fieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                ifields0.AddField(fieldClass);
                IField field = new Field();
                IFieldEdit fieldEdit1 = (IFieldEdit) field;
                fieldEdit1.Name_2 = "SHAPE";
                fieldEdit1.AliasName_2 = "SHAPE";
                fieldEdit1.Type_2 = esriFieldType.esriFieldTypeGeometry;
                fieldEdit1.GeometryDef_2 = geometryDefClass;
                ifields0.AddField(field);
            }
            string name = "";
            int num = 0;
            while (true)
            {
                if (num > ifields_0.FieldCount - 1)
                {
                    break;
                }
                else if (ifields_0.Field[num].Type == esriFieldType.esriFieldTypeGeometry)
                {
                    name = ifields_0.Field[num].Name;
                    break;
                }
                else
                {
                    num++;
                }
            }
            if (name == "")
            {
                throw new Exception("Cannot locate geometry field in FIELDS");
            }
            IFeatureClass featureClass = null;
            if (object_0 is IWorkspace)
            {
                IFeatureWorkspace object0 = (IFeatureWorkspace) ((IWorkspace) object_0);
                featureClass = object0.CreateFeatureClass(string_0, ifields_0, uid_0, uid_1, esriFeatureType_0, name,
                    string_1);
            }
            else if (object_0 is IFeatureDataset)
            {
                IFeatureDataset featureDataset = (IFeatureDataset) object_0;
                featureClass = featureDataset.CreateFeatureClass(string_0, ifields_0, uid_0, uid_1, esriFeatureType_0,
                    name, string_1);
            }
            return featureClass;
        }

        public static IDataset CreateFeatureDataSet(IWorkspace iworkspace_0, string string_0,
            ISpatialReference ispatialReference_0)
        {
            IDataset dataset;
            if (iworkspace_0 == null)
            {
                dataset = null;
            }
            else if (string_0 == null)
            {
                dataset = null;
            }
            else if (string_0 == "")
            {
                dataset = null;
            }
            else if (ispatialReference_0 != null)
            {
                IFeatureWorkspace iworkspace0 = (IFeatureWorkspace) iworkspace_0;
                IDataset dataset1 = null;
                try
                {
                    dataset1 = iworkspace0.CreateFeatureDataset(string_0, ispatialReference_0);
                }
                catch (Exception exception)
                {
                }
                dataset = dataset1;
            }
            else
            {
                dataset = null;
            }
            return dataset;
        }

        public static IWorkspace CreateFileGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory fileGDBWorkspaceFactoryClass = new FileGDBWorkspaceFactory();
            try
            {
                IWorkspaceName workspaceName =
                    fileGDBWorkspaceFactoryClass.Create(System.IO.Path.GetDirectoryName(string_0),
                        System.IO.Path.GetFileNameWithoutExtension(string_0), null, 0);
                workspace = (workspaceName as IName).Open() as IWorkspace;
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                int errorCode = cOMException.ErrorCode;
                MessageBox.Show(string.Concat("错误代码:", errorCode.ToString(), "\r\n", cOMException.Message));
            }
            catch (Exception exception)
            {
            }
            ComReleaser.ReleaseCOMObject(fileGDBWorkspaceFactoryClass);
            fileGDBWorkspaceFactoryClass = null;
            return workspace;
        }

        public static IWorkspace CreateLocalGDB(string string_0)
        {
            IWorkspace workspace;
            workspace = (System.IO.Path.GetExtension(string_0).ToLower() != ".mdb"
                ? WorkspaceOperator.CreateFileGDB(string_0)
                : WorkspaceOperator.CreatePersonGDB(string_0));
            return workspace;
        }

        public static IWorkspace CreatePersonGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory accessWorkspaceFactoryClass = new AccessWorkspaceFactory();
            try
            {
                IWorkspaceName workspaceName =
                    accessWorkspaceFactoryClass.Create(System.IO.Path.GetDirectoryName(string_0),
                        System.IO.Path.GetFileNameWithoutExtension(string_0), null, 0);
                workspace = (workspaceName as IName).Open() as IWorkspace;
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                int errorCode = cOMException.ErrorCode;
                MessageBox.Show(string.Concat("错误代码:", errorCode.ToString(), "\r\n", cOMException.Message));
            }
            catch (Exception exception)
            {
            }
            ComReleaser.ReleaseCOMObject(accessWorkspaceFactoryClass);
            accessWorkspaceFactoryClass = null;
            return workspace;
        }

        public static string GetFinalName(IWorkspace iworkspace_0, esriDatasetType esriDatasetType_0, string string_0)
        {
            string text = string_0;
            int num = 1;
            if (esriDatasetType_0 == esriDatasetType.esriDTFeatureDataset)
            {
                try
                {
                    while (true)
                    {
                        IFeatureDataset featureDataset = (iworkspace_0 as IFeatureWorkspace).OpenFeatureDataset(text);
                        if (featureDataset == null)
                        {
                            break;
                        }
                        text = string_0 + "_" + num.ToString();
                        num++;
                    }
                    return text;
                }
                catch
                {
                    return text;
                }
            }
            string text2;
            ((IFieldChecker) new FieldChecker
            {
                ValidateWorkspace = iworkspace_0
            }).ValidateTableName(string_0, out text2);
            text = text2;
            if (iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace ||
                iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
            {
                while (((IWorkspace2) iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, text))
                {
                    text = text2 + "_" + num.ToString();
                    num++;
                }
            }
            else if (iworkspace_0.Type != esriWorkspaceType.esriFileSystemWorkspace)
            {
            }
            return text;
        }


        public static string GetFinalName2(IWorkspace iworkspace_0, esriDatasetType esriDatasetType_0, string string_0,
            string string_1, string string_2)
        {
            string text = string_0 + string_1 + string_2;
            int num = 1;
            if (esriDatasetType_0 == esriDatasetType.esriDTFeatureDataset)
            {
                try
                {
                    while (true)
                    {
                        IFeatureDataset featureDataset = (iworkspace_0 as IFeatureWorkspace).OpenFeatureDataset(text);
                        if (featureDataset == null)
                        {
                            break;
                        }
                        text = string.Concat(new string[]
                        {
                            string_0,
                            string_1,
                            string_2,
                            "_",
                            num.ToString()
                        });
                        num++;
                    }
                    return text;
                }
                catch
                {
                    return text;
                }
            }
            string text2;
            ((IFieldChecker) new FieldChecker
            {
                ValidateWorkspace = iworkspace_0
            }).ValidateTableName(string_1, out text2);
            text = string_0 + text2 + string_2;
            if (iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace ||
                iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
            {
                while (((IWorkspace2) iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, text))
                {
                    text = string.Concat(new string[]
                    {
                        string_0,
                        text2,
                        string_2,
                        "_",
                        num.ToString()
                    });
                    num++;
                }
            }
            else if (iworkspace_0.Type != esriWorkspaceType.esriFileSystemWorkspace)
            {
            }
            return text;
        }

        public static string GetWorkspaceConnectInfo(IWorkspace iworkspace_0)
        {
            string str;
            if (iworkspace_0 != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                if ((iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace
                    ? false
                    : iworkspace_0.Type != esriWorkspaceType.esriFileSystemWorkspace))
                {
                    string lower = Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("DBCLIENT")).ToLower();
                    stringBuilder.Append(lower);
                    stringBuilder.Append(",");
                    lower =
                        Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("DB_CONNECTION_PROPERTIES"))
                            .ToLower();
                    stringBuilder.Append(lower);
                    stringBuilder.Append(",");
                    lower = Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("Database")).ToLower();
                    stringBuilder.Append(lower);
                }
                else
                {
                    stringBuilder.Append(iworkspace_0.PathName);
                }
                str = stringBuilder.ToString();
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static string GetWorkspaceConnectInfo(IPropertySet ipropertySet_0)
        {
            string str;
            string lower;
            if (ipropertySet_0 != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (ipropertySet_0.Count >= 5)
                {
                    lower = Convert.ToString(ipropertySet_0.GetProperty("DBCLIENT")).ToLower();
                    stringBuilder.Append(lower);
                    stringBuilder.Append(",");
                    lower = Convert.ToString(ipropertySet_0.GetProperty("DB_CONNECTION_PROPERTIES")).ToLower();
                    stringBuilder.Append(lower);
                    stringBuilder.Append(",");
                    lower = Convert.ToString(ipropertySet_0.GetProperty("Database")).ToLower();
                    stringBuilder.Append(lower);
                }
                else
                {
                    lower = Convert.ToString(ipropertySet_0.GetProperty("Database")).ToLower();
                    stringBuilder.Append(lower);
                }
                str = stringBuilder.ToString();
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static bool IsConnectedToGeodatabase(IWorkspace iworkspace_0)
        {
            IWorkspaceProperty property =
                ((IWorkspaceProperties) iworkspace_0).Property[
                    esriWorkspacePropertyGroupType.esriWorkspacePropertyGroup, 7];
            return Convert.ToBoolean(property.PropertyValue);
        }

        public static bool IsConnectedToGeodatabase(IWorkspaceName iworkspaceName_0)
        {
            bool geodatabase;
            bool flag = false;
            try
            {
                object property = iworkspaceName_0.ConnectionProperties.GetProperty("IS_GEODATABASE");
                if (property != null)
                {
                    flag = Convert.ToBoolean(property);
                }
                else
                {
                    geodatabase =
                        WorkspaceOperator.IsConnectedToGeodatabase((iworkspaceName_0 as IName).Open() as IWorkspace);
                    return geodatabase;
                }
            }
            catch (Exception exception)
            {
            }
            geodatabase = flag;
            return geodatabase;
        }

        public static ITextSymbol MakeTextSymbol(string string_0, double double_0)
        {
            ITextSymbol textSymbol;
            try
            {
                ISimpleTextSymbol textSymbolClass = (ISimpleTextSymbol) (new TextSymbol());
                IFontDisp stdFontClass = (IFontDisp) (new StdFont());
                stdFontClass.Name = string_0;
                textSymbolClass.Font = stdFontClass;
                RgbColor rgbColorClass = new RgbColor();
                textSymbolClass.Angle = 0;
                textSymbolClass.RightToLeft = false;
                textSymbolClass.Size = double_0;
                textSymbolClass.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                textSymbolClass.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                textSymbol = textSymbolClass;
            }
            catch
            {
                textSymbol = null;
            }
            return textSymbol;
        }

        public static IWorkspace OpenFileGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory fileGDBWorkspaceFactoryClass = new FileGDBWorkspaceFactory();
            try
            {
                workspace = fileGDBWorkspaceFactoryClass.OpenFromFile(string_0, 0);
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                int errorCode = cOMException.ErrorCode;
                MessageBox.Show(string.Concat("错误代码:", errorCode.ToString(), "\r\n", cOMException.Message));
            }
            catch (Exception exception)
            {
            }
            ComReleaser.ReleaseCOMObject(fileGDBWorkspaceFactoryClass);
            fileGDBWorkspaceFactoryClass = null;
            return workspace;
        }

        public static IWorkspace OpenPersonGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory accessWorkspaceFactoryClass = new AccessWorkspaceFactory();
            try
            {
                workspace = accessWorkspaceFactoryClass.OpenFromFile(string_0, 0);
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                int errorCode = cOMException.ErrorCode;
                MessageBox.Show(string.Concat("错误代码:", errorCode.ToString(), "\r\n", cOMException.Message));
            }
            catch (Exception exception)
            {
            }
            ComReleaser.ReleaseCOMObject(accessWorkspaceFactoryClass);
            accessWorkspaceFactoryClass = null;
            return workspace;
        }

        public static IWorkspace OpenSDEGDB(string string_0)
        {
            IWorkspace workspace = null;
            IWorkspaceFactory sdeWorkspaceFactoryClass = new SdeWorkspaceFactory();
            try
            {
                workspace = sdeWorkspaceFactoryClass.OpenFromFile(string_0, 0);
            }
            catch (COMException cOMException1)
            {
                COMException cOMException = cOMException1;
                int errorCode = cOMException.ErrorCode;
                MessageBox.Show(string.Concat("错误代码:", errorCode.ToString(), "\r\n", cOMException.Message));
            }
            catch (Exception exception)
            {
            }
            ComReleaser.ReleaseCOMObject(sdeWorkspaceFactoryClass);
            sdeWorkspaceFactoryClass = null;
            return workspace;
        }

        public static IWorkspace OpenWorkspace(IPropertySet ipropertySet_0)
        {
            object obj;
            object obj1;
            IWorkspaceFactory sdeWorkspaceFactoryClass;
            IWorkspace workspace = null;
            if (ipropertySet_0.Count != 1)
            {
                sdeWorkspaceFactoryClass = new SdeWorkspaceFactory();
                try
                {
                    workspace = sdeWorkspaceFactoryClass.Open(ipropertySet_0, 0);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                string str = "";
                string lower = "";
                ipropertySet_0.GetAllProperties(out obj, out obj1);
                if (((string[]) obj)[0] == "DATABASE")
                {
                    str = ((object[]) obj1)[0].ToString();
                    lower = System.IO.Path.GetExtension(str).ToLower();
                }
                if (lower == ".mdb")
                {
                    sdeWorkspaceFactoryClass = new AccessWorkspaceFactory();
                    try
                    {
                        workspace = sdeWorkspaceFactoryClass.Open(ipropertySet_0, 0);
                    }
                    catch (Exception exception1)
                    {
                        MessageBox.Show(exception1.Message);
                    }
                }
                else if (lower == ".gdb")
                {
                    sdeWorkspaceFactoryClass = new FileGDBWorkspaceFactory();
                    try
                    {
                        workspace = sdeWorkspaceFactoryClass.Open(ipropertySet_0, 0);
                    }
                    catch (Exception exception2)
                    {
                        MessageBox.Show(exception2.Message);
                    }
                }
            }
            return workspace;
        }

        public static IWorkspace OpenWorkspace(string string_0)
        {
            IWorkspace workspace;
            string lower = System.IO.Path.GetExtension(string_0).ToLower();
            if (lower == ".mdb")
            {
                workspace = WorkspaceOperator.OpenPersonGDB(string_0);
            }
            else if (lower == ".sde")
            {
                workspace = WorkspaceOperator.OpenSDEGDB(string_0);
            }
            else if ((lower == null ? false : lower.Length != 0))
            {
                workspace = null;
            }
            else
            {
                workspace = WorkspaceOperator.OpenFileGDB(string_0);
            }
            return workspace;
        }

        public static bool WorkspaceIsSame(IWorkspace iworkspace_0, IWorkspace iworkspace_1)
        {
            bool flag;
            if (!(iworkspace_0 == null ? false : iworkspace_1 != null))
            {
                flag = true;
            }
            else if (iworkspace_0.Type == iworkspace_1.Type)
            {
                bool flag1 = false;
                if ((iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace
                    ? false
                    : iworkspace_0.Type != esriWorkspaceType.esriFileSystemWorkspace))
                {
                    string lower = Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("DBCLIENT")).ToLower();
                    if (lower == Convert.ToString(iworkspace_1.ConnectionProperties.GetProperty("DBCLIENT")).ToLower())
                    {
                        string str = lower;
                        lower =
                            Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("DB_CONNECTION_PROPERTIES"))
                                .ToLower();
                        if (lower ==
                            Convert.ToString(iworkspace_1.ConnectionProperties.GetProperty("DB_CONNECTION_PROPERTIES"))
                                .ToLower())
                        {
                            if (str != "sqlserver")
                            {
                                flag1 = true;
                            }
                            else
                            {
                                lower =
                                    Convert.ToString(iworkspace_0.ConnectionProperties.GetProperty("Database"))
                                        .ToLower();
                                string lower1 =
                                    Convert.ToString(iworkspace_1.ConnectionProperties.GetProperty("Database"))
                                        .ToLower();
                                flag1 = lower == lower1;
                            }
                        }
                    }
                }
                else
                {
                    flag1 = string.Compare(iworkspace_0.PathName, iworkspace_1.PathName, true) == 0;
                }
                flag = flag1;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
    }
}