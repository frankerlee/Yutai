using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Geodatabase.UI
{
	internal class SRLibCommonFunc
	{
		internal static IProjectForm m_pfrm;

		public SRLibCommonFunc()
		{
		}

		public static IFeatureClass CreateFeatureClass(object object_0, string string_0, ISpatialReference ispatialReference_0, esriFeatureType esriFeatureType_0, esriGeometryType esriGeometryType_0, IFields ifields_0, UID uid_0, UID uid_1, string string_1)
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
				ifields_0 = new ESRI.ArcGIS.Geodatabase.Fields();
				IFieldsEdit ifields0 = (IFieldsEdit)ifields_0;
				IGeometryDef geometryDefClass = new GeometryDef();
				IGeometryDefEdit esriGeometryType0 = (IGeometryDefEdit)geometryDefClass;
				esriGeometryType0.GeometryType_2 = esriGeometryType_0;
				esriGeometryType0.GridCount_2 = 1;
				esriGeometryType0.GridSize_2[0]= 0.5;
				esriGeometryType0.AvgNumPoints_2 = 2;
				esriGeometryType0.HasM_2 = false;
				esriGeometryType0.HasZ_2 = true;
				if (object_0 is IWorkspace)
				{
					esriGeometryType0.SpatialReference_2 = ispatialReference_0;
				}
				IField fieldClass = new ESRI.ArcGIS.Geodatabase.Field();
				IFieldEdit fieldEdit = (IFieldEdit)fieldClass;
				fieldEdit.Name_2 = "OBJECTID";
				fieldEdit.AliasName_2 = "OBJECTID";
				fieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
				ifields0.AddField(fieldClass);
				IField field = new ESRI.ArcGIS.Geodatabase.Field();
				IFieldEdit fieldEdit1 = (IFieldEdit)field;
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
				IFeatureWorkspace object0 = (IFeatureWorkspace)((IWorkspace)object_0);
				featureClass = object0.CreateFeatureClass(string_0, ifields_0, uid_0, uid_1, esriFeatureType_0, name, string_1);
			}
			else if (object_0 is IFeatureDataset)
			{
				IFeatureDataset featureDataset = (IFeatureDataset)object_0;
				featureClass = featureDataset.CreateFeatureClass(string_0, ifields_0, uid_0, uid_1, esriFeatureType_0, name, string_1);
			}
			return featureClass;
		}

		public static void Project(IFeatureClass ifeatureClass_0, ISpatialReference ispatialReference_0, IWorkspace iworkspace_0, string string_0, double double_0)
		{
			string str;
			double num;
			double num1;
			double num2;
			double num3;
			IEnumFieldError enumFieldError;
			IFields field;
			IWorkspace workspace = ((IDataset)ifeatureClass_0).Workspace;
			IFeatureDataConverter featureDataConverterClass = new FeatureDataConverter();
		    IWorkspaceName workspaceNameClass = new WorkspaceName() as IWorkspaceName;
		   
		        workspaceNameClass.ConnectionProperties = workspace.ConnectionProperties;
			    workspaceNameClass.WorkspaceFactoryProgID = workspace.WorkspaceFactory.GetClassID().Value.ToString();
			
			IWorkspaceName connectionProperties = new WorkspaceName() as IWorkspaceName;
			PropertySet propertySetClass = new PropertySet();
			connectionProperties.ConnectionProperties = iworkspace_0.ConnectionProperties;
			connectionProperties.WorkspaceFactoryProgID = iworkspace_0.WorkspaceFactory.GetClassID().Value.ToString();
			IDatasetName featureClassNameClass = new FeatureClassName() as IDatasetName;
			string aliasName = ifeatureClass_0.AliasName;
			int num4 = aliasName.LastIndexOf(".");
			if (num4 != -1)
			{
				aliasName = aliasName.Substring(num4 + 1);
			}
			featureClassNameClass.Name = aliasName;
			featureClassNameClass.WorkspaceName = workspaceNameClass;
		    IDatasetName datasetName = new FeatureClassName() as IDatasetName;

		    {
		        datasetName.WorkspaceName = connectionProperties;
		    };
			IFieldChecker fieldCheckerClass = new FieldChecker()
			{
				ValidateWorkspace = iworkspace_0
			};
			string[] strArrays = string_0.Split(new char[] { '.' });
			string_0 = string.Concat(strArrays[(int)strArrays.Length - 1], "_Project");
			fieldCheckerClass.ValidateTableName(string_0, out str);
			string str1 = str;
			int num5 = 1;
			if (!(iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace ? false : iworkspace_0.Type != esriWorkspaceType.esriLocalDatabaseWorkspace))
			{
				while (((IWorkspace2)iworkspace_0).NameExists[esriDatasetType.esriDTFeatureClass, str1])
				{
					str1 = string.Concat(str, "_", num5.ToString());
					num5++;
				}
			}
			else if (iworkspace_0.Type == esriWorkspaceType.esriFileSystemWorkspace)
			{
				while (File.Exists(string.Concat(str1, ".shp")))
				{
					str1 = string.Concat(str, "_", num5.ToString());
					num5++;
				}
			}
			datasetName.Name = str1;
			IFields fieldsClass = new ESRI.ArcGIS.Geodatabase.Fields();
			num4 = ifeatureClass_0.Fields.FindField(ifeatureClass_0.ShapeFieldName);
			IField field1 = ifeatureClass_0.Fields.Field[num4];
			IGeometryDef geometryDef = field1.GeometryDef;
			ISpatialReference spatialReference = geometryDef.SpatialReference;
			ispatialReference_0.GetDomain(out num, out num1, out num2, out num3);
			((IGeometryDefEdit)geometryDef).GridCount_2 = 1;
			((IGeometryDefEdit)geometryDef).GridSize_2[0] = double_0;
			((IGeometryDefEdit)geometryDef).SpatialReference_2 = ispatialReference_0;
			((IFieldEdit)field1).GeometryDef_2 = geometryDef;
			for (int i = 0; i < ifeatureClass_0.Fields.FieldCount; i++)
			{
				if (i != num4)
				{
					IField field2 = ifeatureClass_0.Fields.Field[i];
					((IFieldsEdit)fieldsClass).AddField(field2);
				}
				else
				{
					((IFieldsEdit)fieldsClass).AddField(field1);
				}
			}
			fieldCheckerClass.Validate(fieldsClass, out enumFieldError, out field);
			if (SRLibCommonFunc.m_pfrm != null)
			{
				SRLibCommonFunc.m_pfrm.FeatureProgress = featureDataConverterClass;
			}
			try
			{
				featureDataConverterClass.ConvertFeatureClass((IFeatureClassName)featureClassNameClass, null, null, (IFeatureClassName)datasetName, geometryDef, field, "", 1000, 0);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

        public static void Project(IFeatureClass ifeatureClass_0, ISpatialReference ispatialReference_0, IFeatureDatasetName ifeatureDatasetName_0, string string_0, double double_0)
        {
            IWorkspace workspace = ((IDataset)ifeatureClass_0).Workspace;
            IFeatureDataConverter featureDataConverter = new FeatureDataConverter();
            IWorkspaceName workspaceName = new WorkspaceName() as IWorkspaceName;
            workspaceName.ConnectionProperties = workspace.ConnectionProperties;
            workspaceName.WorkspaceFactoryProgID = workspace.WorkspaceFactory.GetClassID().Value.ToString();
            IWorkspaceName workspaceName2 = (ifeatureDatasetName_0 as IDatasetName).WorkspaceName;
            IDatasetName datasetName = new FeatureClassName() as IDatasetName;
            string text = ifeatureClass_0.AliasName;
            int num = text.LastIndexOf(".");
            if (num != -1)
            {
                text = text.Substring(num + 1);
            }
            datasetName.Name = text;
            datasetName.WorkspaceName = workspaceName;
            IWorkspace2 workspace2 = (workspaceName2 as IName).Open() as IWorkspace2;
            IDatasetName datasetName2 = new FeatureClassName() as IDatasetName;
            datasetName2.WorkspaceName = workspaceName2;
            (datasetName2 as IFeatureClassName).FeatureDatasetName = (ifeatureDatasetName_0 as IDatasetName);
            IFieldChecker fieldChecker = new FieldChecker();
            fieldChecker.ValidateWorkspace = (workspace2 as IWorkspace);
            string[] array = string_0.Split(new char[]
            {
        '.'
            });
            string_0 = array[array.Length - 1] + "_Project";
            string text2;
            fieldChecker.ValidateTableName(string_0, out text2);
            string text3 = text2;
            int num2 = 1;
            if (workspaceName2.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace || workspaceName2.Type == esriWorkspaceType.esriLocalDatabaseWorkspace)
            {
                while (workspace2.get_NameExists(esriDatasetType.esriDTFeatureClass, text3))
                {
                    text3 = text2 + "_" + num2.ToString();
                    num2++;
                }
            }
            else if (workspaceName2.Type == esriWorkspaceType.esriFileSystemWorkspace)
            {
                while (System.IO.File.Exists(text3 + ".shp"))
                {
                    text3 = text2 + "_" + num2.ToString();
                    num2++;
                }
            }
            datasetName2.Name = text3;
            IFields fields = new ESRI.ArcGIS.Geodatabase.Fields()  as IFields;
            num = ifeatureClass_0.Fields.FindField(ifeatureClass_0.ShapeFieldName);
            IField field = ifeatureClass_0.Fields.get_Field(num);
            IGeometryDef geometryDef = field.GeometryDef;
            ISpatialReference arg_1D9_0 = geometryDef.SpatialReference;
            double num3;
            double num4;
            double num5;
            double num6;
            ispatialReference_0.GetDomain(out num3, out num4, out num5, out num6);
            ((IGeometryDefEdit)geometryDef).GridCount_2 = 1;
            ((IGeometryDefEdit)geometryDef).set_GridSize(0, double_0);
            ((IGeometryDefEdit)geometryDef).SpatialReference_2 = ispatialReference_0;
            ((IFieldEdit)field).GeometryDef_2 = geometryDef;
            for (int i = 0; i < ifeatureClass_0.Fields.FieldCount; i++)
            {
                if (i == num)
                {
                    ((IFieldsEdit)fields).AddField(field);
                }
                else
                {
                    IField field2 = ifeatureClass_0.Fields.get_Field(i);
                    ((IFieldsEdit)fields).AddField(field2);
                }
            }
            IEnumFieldError enumFieldError;
            IFields outputFields;
            fieldChecker.Validate(fields, out enumFieldError, out outputFields);
            if (SRLibCommonFunc.m_pfrm != null)
            {
                SRLibCommonFunc.m_pfrm.FeatureProgress = featureDataConverter;
            }
            try
            {
                featureDataConverter.ConvertFeatureClass((IFeatureClassName)datasetName, null, ifeatureDatasetName_0, (IFeatureClassName)datasetName2, geometryDef, outputFields, "", 1000, 0);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        public static void Project(IFeatureDataset ifeatureDataset_0, ISpatialReference ispatialReference_0, IWorkspace iworkspace_0, string string_0)
		{
			IGeometryDef geometryDefClass = new GeometryDef();
			ISpatialReference spatialReference = ((IGeoDataset)ifeatureDataset_0).SpatialReference;
			((IGeometryDefEdit)geometryDefClass).SpatialReference_2 = ispatialReference_0;
			IWorkspace workspace = ifeatureDataset_0.Workspace;
			IFeatureDataConverter featureDataConverterClass = new FeatureDataConverter();
		    IWorkspaceName workspaceNameClass = new WorkspaceName() as IWorkspaceName as IWorkspaceName;

		    
		        workspaceNameClass.ConnectionProperties = workspace.ConnectionProperties;
                workspaceNameClass.WorkspaceFactoryProgID = workspace.WorkspaceFactory.GetClassID().Value.ToString();
            
			IWorkspaceName connectionProperties = new WorkspaceName() as IWorkspaceName as IWorkspaceName;
			PropertySet propertySetClass = new PropertySet();
			connectionProperties.ConnectionProperties = iworkspace_0.ConnectionProperties;
			connectionProperties.WorkspaceFactoryProgID = iworkspace_0.WorkspaceFactory.GetClassID().Value.ToString();
			IDatasetName featureDatasetNameClass = new FeatureDatasetName() as IDatasetName;
			string name = ifeatureDataset_0.Name;
			int num = name.LastIndexOf(".");
			if (num != -1)
			{
				name = name.Substring(num + 1);
			}
			featureDatasetNameClass.Name = name;
			featureDatasetNameClass.WorkspaceName = workspaceNameClass;
		    IDatasetName datasetName = new FeatureDatasetName() as IDatasetName;
		    {
		        datasetName.WorkspaceName = connectionProperties;
		        datasetName.Name = string_0;
		    };
			featureDataConverterClass.ConvertFeatureDataset((IFeatureDatasetName)featureDatasetNameClass, (IFeatureDatasetName)datasetName, geometryDefClass, "", 1000, 0);
		}

		public static void Project(IFeatureClass ifeatureClass_0, ISpatialReference ispatialReference_0)
		{
			IFields fields = ifeatureClass_0.Fields;
			IField field = fields.Field[fields.FindField(ifeatureClass_0.ShapeFieldName)];
			IGeometryDef geometryDef = field.GeometryDef;
			ISpatialReference spatialReference = geometryDef.SpatialReference;
			((IGeometryDefEdit)geometryDef).SpatialReference_2 = ispatialReference_0;
			((IFieldEdit)field).GeometryDef_2 = geometryDef;
		}
	}
}