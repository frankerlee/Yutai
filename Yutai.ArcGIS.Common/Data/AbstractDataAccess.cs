using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Geodatabase;
using Fields = ESRI.ArcGIS.Geodatabase.Fields;

namespace Yutai.ArcGIS.Common.Data
{
	public abstract class AbstractDataAccess : IDataAccess
	{
		protected IWorkspace m_Workspace;

		public abstract DatabaseType DBType
		{
			get;
		}

		protected AbstractDataAccess()
		{
		}

		public void CloseWorkspace()
		{
			Marshal.ReleaseComObject(this.m_Workspace);
			this.m_Workspace = null;
		}

		public IFeatureClass CreateFeatureClass(string string_0, esriGeometryType esriGeometryType_0, ISpatialReference ispatialReference_0, bool bool_0, bool bool_1, int int_0)
		{
			IFeatureWorkspace mWorkspace = (IFeatureWorkspace)this.m_Workspace;
			IFieldsEdit fieldsClass = (IFieldsEdit)(new Fields() as IFieldsEdit);
			FieldEdit.AddDefaultField(fieldsClass, esriGeometryType_0, ispatialReference_0, bool_0, bool_1, int_0);
			IFeatureClass featureClass = mWorkspace.CreateFeatureClass(string_0, fieldsClass, null, null, esriFeatureType.esriFTSimple, "Shape", "");
			return featureClass;
		}

		public static IFeatureClass CreateFeatureClass(IWorkspace iworkspace_0, string string_0, esriGeometryType esriGeometryType_0, ISpatialReference ispatialReference_0, bool bool_0, bool bool_1, int int_0)
		{
			IFeatureWorkspace iworkspace0 = (IFeatureWorkspace)iworkspace_0;
			IFieldsEdit fieldsClass = (IFieldsEdit)(new Fields());
			FieldEdit.AddDefaultField(fieldsClass, esriGeometryType_0, ispatialReference_0, bool_0, bool_1, int_0);
			IFeatureClass featureClass = iworkspace0.CreateFeatureClass(string_0, fieldsClass, null, null, esriFeatureType.esriFTSimple, "Shape", "");
			return featureClass;
		}

		public IFeatureClass OpenFeatureClass(string string_0)
		{
			IFeatureClass featureClass;
			try
			{
				featureClass = ((IFeatureWorkspace)this.m_Workspace).OpenFeatureClass(string_0);
				return featureClass;
			}
			catch
			{
			}
			featureClass = null;
			return featureClass;
		}

		public static IFeatureClass OpenFeatureClass(IWorkspace iworkspace_0, string string_0)
		{
			IFeatureClass featureClass;
			try
			{
				featureClass = ((IFeatureWorkspace)iworkspace_0).OpenFeatureClass(string_0);
				return featureClass;
			}
			catch
			{
			}
			featureClass = null;
			return featureClass;
		}

		public IFeatureClass OpenFeatureClass(IPropertySet ipropertySet_0, string string_0)
		{
			return AbstractDataAccess.OpenFeatureClass(this.OpenWorkspace(ipropertySet_0), string_0);
		}

		public IFeatureClass[] OpenFeatureClass(string[] string_0)
		{
			IFeatureClass[] featureClassArray;
			try
			{
				IFeatureClass[] featureClassArray1 = new IFeatureClass[(int)string_0.Length];
				IFeatureWorkspace mWorkspace = (IFeatureWorkspace)this.m_Workspace;
				for (int i = 0; i < (int)string_0.Length; i++)
				{
					featureClassArray1[i] = mWorkspace.OpenFeatureClass(string_0[i]);
				}
				featureClassArray = featureClassArray1;
				return featureClassArray;
			}
			catch
			{
			}
			featureClassArray = null;
			return featureClassArray;
		}

		public static IFeatureClass[] OpenFeatureClass(IWorkspace iworkspace_0, string[] string_0)
		{
			IFeatureClass[] featureClassArray;
			try
			{
				IFeatureClass[] featureClassArray1 = new IFeatureClass[(int)string_0.Length];
				IFeatureWorkspace iworkspace0 = (IFeatureWorkspace)iworkspace_0;
				for (int i = 0; i < (int)string_0.Length; i++)
				{
					featureClassArray1[i] = iworkspace0.OpenFeatureClass(string_0[i]);
				}
				featureClassArray = featureClassArray1;
				return featureClassArray;
			}
			catch
			{
			}
			featureClassArray = null;
			return featureClassArray;
		}

		public IFeatureClass[] OpenFeatureClass(IPropertySet ipropertySet_0, string[] string_0)
		{
			return AbstractDataAccess.OpenFeatureClass(this.OpenWorkspace(ipropertySet_0), string_0);
		}

		public ITable OpenTable(string string_0)
		{
			ITable table;
			try
			{
				table = ((IFeatureWorkspace)this.m_Workspace).OpenTable(string_0);
				return table;
			}
			catch
			{
			}
			table = null;
			return table;
		}

		public static ITable OpenTable(IWorkspace iworkspace_0, string string_0)
		{
			ITable table;
			try
			{
				table = ((IFeatureWorkspace)iworkspace_0).OpenTable(string_0);
				return table;
			}
			catch
			{
			}
			table = null;
			return table;
		}

		public ITable OpenTable(IPropertySet ipropertySet_0, string string_0)
		{
			return AbstractDataAccess.OpenTable(this.OpenWorkspace(ipropertySet_0), string_0);
		}

		public static ITable[] OpenTable(IWorkspace iworkspace_0, string[] string_0)
		{
			ITable[] tableArray;
			try
			{
				ITable[] tableArray1 = new ITable[(int)string_0.Length];
				IFeatureWorkspace iworkspace0 = (IFeatureWorkspace)iworkspace_0;
				for (int i = 0; i < (int)string_0.Length; i++)
				{
					tableArray1[i] = iworkspace0.OpenTable(string_0[i]);
				}
				tableArray = tableArray1;
				return tableArray;
			}
			catch
			{
			}
			tableArray = null;
			return tableArray;
		}

		public ITable[] OpenTable(string[] string_0)
		{
			ITable[] tableArray;
			try
			{
				ITable[] tableArray1 = new ITable[(int)string_0.Length];
				IFeatureWorkspace mWorkspace = (IFeatureWorkspace)this.m_Workspace;
				for (int i = 0; i < (int)string_0.Length; i++)
				{
					tableArray1[i] = mWorkspace.OpenTable(string_0[i]);
				}
				tableArray = tableArray1;
				return tableArray;
			}
			catch
			{
			}
			tableArray = null;
			return tableArray;
		}

		public ITable[] OpenTable(IPropertySet ipropertySet_0, string[] string_0)
		{
			return AbstractDataAccess.OpenTable(this.OpenWorkspace(ipropertySet_0), string_0);
		}

		public abstract IWorkspace OpenWorkspace(IPropertySet ipropertySet_0);
	}
}