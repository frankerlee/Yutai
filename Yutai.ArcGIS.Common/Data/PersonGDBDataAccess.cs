using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Data
{
	public class PersonGDBDataAccess : AbstractDataAccess
	{
		public override DatabaseType DBType
		{
			get
			{
				return DatabaseType.PersonGDB;
			}
		}

		public PersonGDBDataAccess()
		{
		}

		public override IWorkspace OpenWorkspace(IPropertySet ipropertySet_0)
		{
			try
			{
				this.m_Workspace = (new AccessWorkspaceFactory()).Open(ipropertySet_0, 0);
			}
			catch
			{
			}
			return this.m_Workspace;
		}
	}
}