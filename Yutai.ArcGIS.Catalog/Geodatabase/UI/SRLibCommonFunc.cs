using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class SRLibCommonFunc
    {
        internal static IProjectForm m_pfrm;

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

        public static void Project(IFeatureClass ifeatureClass_0, ISpatialReference ispatialReference_0)
        {
            IFields fields = ifeatureClass_0.Fields;
            int index = fields.FindField(ifeatureClass_0.ShapeFieldName);
            IField field = fields.get_Field(index);
            IGeometryDef geometryDef = field.GeometryDef;
            ISpatialReference spatialReference = geometryDef.SpatialReference;
            ((IGeometryDefEdit) geometryDef).SpatialReference_2 = ispatialReference_0;
            ((IFieldEdit) field).GeometryDef_2 = geometryDef;
        }

        public static void Project(IFeatureDataset ifeatureDataset_0, ISpatialReference ispatialReference_0,
            IWorkspace iworkspace_0, string string_0)
        {
            IGeometryDef outputGeometryDef = new GeometryDefClass();
            ISpatialReference spatialReference = ((IGeoDataset) ifeatureDataset_0).SpatialReference;
            ((IGeometryDefEdit) outputGeometryDef).SpatialReference_2 = ispatialReference_0;
            IWorkspace workspace = ifeatureDataset_0.Workspace;
            IFeatureDataConverter converter = new FeatureDataConverterClass();
            IWorkspaceName name = new WorkspaceNameClass
            {
                ConnectionProperties = workspace.ConnectionProperties,
                WorkspaceFactoryProgID = workspace.WorkspaceFactory.GetClassID().Value.ToString()
            };
            IWorkspaceName name2 = new WorkspaceNameClass();
            new PropertySetClass();
            name2.ConnectionProperties = iworkspace_0.ConnectionProperties;
            name2.WorkspaceFactoryProgID = iworkspace_0.WorkspaceFactory.GetClassID().Value.ToString();
            IDatasetName name3 = new FeatureDatasetNameClass();
            string str = ifeatureDataset_0.Name;
            int num = str.LastIndexOf(".");
            if (num != -1)
            {
                str = str.Substring(num + 1);
            }
            name3.Name = str;
            name3.WorkspaceName = name;
            IDatasetName name4 = new FeatureDatasetNameClass
            {
                WorkspaceName = name2,
                Name = string_0
            };
            converter.ConvertFeatureDataset((IFeatureDatasetName) name3, (IFeatureDatasetName) name4, outputGeometryDef,
                "", 1000, 0);
        }

        public static void Project(IFeatureClass ifeatureClass_0, ISpatialReference ispatialReference_0,
            IFeatureDatasetName ifeatureDatasetName_0, string string_0, double double_0)
        {
            string str2;
            double num3;
            double num4;
            double num5;
            double num6;
            IEnumFieldError error;
            IFields fields2;
            IWorkspace workspace = ((IDataset) ifeatureClass_0).Workspace;
            IFeatureDataConverter converter = new FeatureDataConverterClass();
            IWorkspaceName name = new WorkspaceNameClass
            {
                ConnectionProperties = workspace.ConnectionProperties,
                WorkspaceFactoryProgID = workspace.WorkspaceFactory.GetClassID().Value.ToString()
            };
            IWorkspaceName workspaceName = (ifeatureDatasetName_0 as IDatasetName).WorkspaceName;
            IDatasetName name3 = new FeatureClassNameClass();
            string aliasName = ifeatureClass_0.AliasName;
            int index = aliasName.LastIndexOf(".");
            if (index != -1)
            {
                aliasName = aliasName.Substring(index + 1);
            }
            name3.Name = aliasName;
            name3.WorkspaceName = name;
            IWorkspace2 workspace2 = (workspaceName as IName).Open() as IWorkspace2;
            IDatasetName name4 = new FeatureClassNameClass
            {
                WorkspaceName = workspaceName
            };
            (name4 as IFeatureClassName).FeatureDatasetName = ifeatureDatasetName_0 as IDatasetName;
            IFieldChecker checker = new FieldCheckerClass
            {
                ValidateWorkspace = workspace2 as IWorkspace
            };
            string[] strArray = string_0.Split(new char[] {'.'});
            string_0 = strArray[strArray.Length - 1] + "_Project";
            checker.ValidateTableName(string_0, out str2);
            string str3 = str2;
            int num2 = 1;
            if ((workspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) ||
                (workspaceName.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
            {
                while (workspace2.get_NameExists(esriDatasetType.esriDTFeatureClass, str3))
                {
                    str3 = str2 + "_" + num2.ToString();
                    num2++;
                }
            }
            else if (workspaceName.Type == esriWorkspaceType.esriFileSystemWorkspace)
            {
                while (File.Exists(str3 + ".shp"))
                {
                    str3 = str2 + "_" + num2.ToString();
                    num2++;
                }
            }
            name4.Name = str3;
            IFields inputField = new FieldsClass();
            index = ifeatureClass_0.Fields.FindField(ifeatureClass_0.ShapeFieldName);
            IField field = ifeatureClass_0.Fields.get_Field(index);
            IGeometryDef geometryDef = field.GeometryDef;
            ISpatialReference spatialReference = geometryDef.SpatialReference;
            ispatialReference_0.GetDomain(out num3, out num4, out num5, out num6);
            ((IGeometryDefEdit) geometryDef).GridCount_2 = 1;
            ((IGeometryDefEdit) geometryDef).set_GridSize(0, double_0);
            ((IGeometryDefEdit) geometryDef).SpatialReference_2 = ispatialReference_0;
            ((IFieldEdit) field).GeometryDef_2 = geometryDef;
            for (int i = 0; i < ifeatureClass_0.Fields.FieldCount; i++)
            {
                if (i == index)
                {
                    ((IFieldsEdit) inputField).AddField(field);
                }
                else
                {
                    IField field2 = ifeatureClass_0.Fields.get_Field(i);
                    ((IFieldsEdit) inputField).AddField(field2);
                }
            }
            checker.Validate(inputField, out error, out fields2);
            if (m_pfrm != null)
            {
                m_pfrm.FeatureProgress = converter;
            }
            try
            {
                converter.ConvertFeatureClass((IFeatureClassName) name3, null, ifeatureDatasetName_0,
                    (IFeatureClassName) name4, geometryDef, fields2, "", 1000, 0);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        public static void Project(IFeatureClass ifeatureClass_0, ISpatialReference ispatialReference_0,
            IWorkspace iworkspace_0, string string_0, double double_0)
        {
            string str2;
            double num3;
            double num4;
            double num5;
            double num6;
            IEnumFieldError error;
            IFields fields2;
            IWorkspace workspace = ((IDataset) ifeatureClass_0).Workspace;
            IFeatureDataConverter converter = new FeatureDataConverterClass();
            IWorkspaceName name = new WorkspaceNameClass
            {
                ConnectionProperties = workspace.ConnectionProperties,
                WorkspaceFactoryProgID = workspace.WorkspaceFactory.GetClassID().Value.ToString()
            };
            IWorkspaceName name2 = new WorkspaceNameClass();
            new PropertySetClass();
            name2.ConnectionProperties = iworkspace_0.ConnectionProperties;
            name2.WorkspaceFactoryProgID = iworkspace_0.WorkspaceFactory.GetClassID().Value.ToString();
            IDatasetName name3 = new FeatureClassNameClass();
            string aliasName = ifeatureClass_0.AliasName;
            int index = aliasName.LastIndexOf(".");
            if (index != -1)
            {
                aliasName = aliasName.Substring(index + 1);
            }
            name3.Name = aliasName;
            name3.WorkspaceName = name;
            IDatasetName name4 = new FeatureClassNameClass
            {
                WorkspaceName = name2
            };
            IFieldChecker checker = new FieldCheckerClass
            {
                ValidateWorkspace = iworkspace_0
            };
            string[] strArray = string_0.Split(new char[] {'.'});
            string_0 = strArray[strArray.Length - 1] + "_Project";
            checker.ValidateTableName(string_0, out str2);
            string str3 = str2;
            int num2 = 1;
            if ((iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace) ||
                (iworkspace_0.Type == esriWorkspaceType.esriLocalDatabaseWorkspace))
            {
                while (((IWorkspace2) iworkspace_0).get_NameExists(esriDatasetType.esriDTFeatureClass, str3))
                {
                    str3 = str2 + "_" + num2.ToString();
                    num2++;
                }
            }
            else if (iworkspace_0.Type == esriWorkspaceType.esriFileSystemWorkspace)
            {
                while (File.Exists(str3 + ".shp"))
                {
                    str3 = str2 + "_" + num2.ToString();
                    num2++;
                }
            }
            name4.Name = str3;
            IFields inputField = new FieldsClass();
            index = ifeatureClass_0.Fields.FindField(ifeatureClass_0.ShapeFieldName);
            IField field = ifeatureClass_0.Fields.get_Field(index);
            IGeometryDef geometryDef = field.GeometryDef;
            ISpatialReference spatialReference = geometryDef.SpatialReference;
            ispatialReference_0.GetDomain(out num3, out num4, out num5, out num6);
            ((IGeometryDefEdit) geometryDef).GridCount_2 = 1;
            ((IGeometryDefEdit) geometryDef).set_GridSize(0, double_0);
            ((IGeometryDefEdit) geometryDef).SpatialReference_2 = ispatialReference_0;
            ((IFieldEdit) field).GeometryDef_2 = geometryDef;
            for (int i = 0; i < ifeatureClass_0.Fields.FieldCount; i++)
            {
                if (i == index)
                {
                    ((IFieldsEdit) inputField).AddField(field);
                }
                else
                {
                    IField field2 = ifeatureClass_0.Fields.get_Field(i);
                    ((IFieldsEdit) inputField).AddField(field2);
                }
            }
            checker.Validate(inputField, out error, out fields2);
            if (m_pfrm != null)
            {
                m_pfrm.FeatureProgress = converter;
            }
            try
            {
                converter.ConvertFeatureClass((IFeatureClassName) name3, null, null, (IFeatureClassName) name4,
                    geometryDef, fields2, "", 1000, 0);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}