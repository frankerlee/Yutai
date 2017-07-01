using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Data
{
    public class ShapeFileDataAccess : AbstractDataAccess
    {
        public override DatabaseType DBType
        {
            get { return DatabaseType.ShapeFile; }
        }

        public ShapeFileDataAccess()
        {
        }

        public override IWorkspace OpenWorkspace(IPropertySet ipropertySet_0)
        {
            try
            {
                this.m_Workspace = (new ShapefileWorkspaceFactory()).Open(ipropertySet_0, 0);
            }
            catch
            {
            }
            return this.m_Workspace;
        }
    }
}