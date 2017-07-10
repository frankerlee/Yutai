using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Editor.Views
{
    public interface ITopologyErrorView : IMenuProvider
    {
        void Initialize(IAppContext context);

        //IWorkspace EditWorkspace { set; }
        IMap FocusMap { set; }
    }
}