using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class CheckInOutWorkspaceInfo
    {
        private IEnumName ienumName_0 = new NamesEnumeratorClass();
        private IWorkspace iworkspace_0 = null;

        public CheckInOutWorkspaceInfo(IWorkspace iworkspace_1)
        {
            this.iworkspace_0 = iworkspace_1;
        }

        public IEnumName EnumName
        {
            get
            {
                return this.ienumName_0;
            }
        }

        public IWorkspace Workspace
        {
            get
            {
                return this.iworkspace_0;
            }
        }
    }
}

