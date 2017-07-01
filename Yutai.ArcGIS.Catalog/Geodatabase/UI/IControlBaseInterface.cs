using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal interface IControlBaseInterface
    {
        void Init();

        IField Filed { set; }

        bool IsEdit { set; }

        IWorkspace Workspace { set; }
    }
}