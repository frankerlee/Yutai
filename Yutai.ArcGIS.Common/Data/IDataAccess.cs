using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Data
{
	public interface IDataAccess
	{
		DatabaseType DBType
		{
			get;
		}

		void CloseWorkspace();

		IFeatureClass CreateFeatureClass(string string_0, esriGeometryType esriGeometryType_0, ISpatialReference ispatialReference_0, bool bool_0, bool bool_1, int int_0);

		IFeatureClass OpenFeatureClass(string string_0);

		IFeatureClass OpenFeatureClass(IPropertySet ipropertySet_0, string string_0);

		IFeatureClass[] OpenFeatureClass(string[] string_0);

		IFeatureClass[] OpenFeatureClass(IPropertySet ipropertySet_0, string[] string_0);

		ITable OpenTable(string string_0);

		ITable[] OpenTable(string[] string_0);

		ITable OpenTable(IPropertySet ipropertySet_0, string string_0);

		ITable[] OpenTable(IPropertySet ipropertySet_0, string[] string_0);

		IWorkspace OpenWorkspace(IPropertySet ipropertySet_0);
	}
}