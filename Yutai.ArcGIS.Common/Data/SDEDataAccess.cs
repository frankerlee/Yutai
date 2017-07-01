using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Data
{
    public class SDEDataAccess : AbstractDataAccess
    {
        public override DatabaseType DBType
        {
            get { return DatabaseType.SDE; }
        }

        public SDEDataAccess()
        {
            this.m_Workspace = null;
        }

        public override IWorkspace OpenWorkspace(IPropertySet ipropertySet_0)
        {
            try
            {
                this.m_Workspace = (new SdeWorkspaceFactory()).Open(ipropertySet_0, 0);
            }
            catch
            {
            }
            return this.m_Workspace;
        }
    }
}