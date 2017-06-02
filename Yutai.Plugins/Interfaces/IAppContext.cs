using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IAppContext
    {
        AppConfig Config { get; }

        IApplicationContainer Container { get; }
        IDockPanelCollection DockPanels { get; }

        IMenu Menu { get; }
        IProject Project { get; }
        //二维地图控件和图例部分
        ITOCControl2 MapLegend { get; }
        IMapControl2 MapControl { get; }
        //三维地图控件和图例部分
        ITOCControl2 SceneLegend { get; }
        ISceneControl SceneControl { get; }

        IStatusBar StatusBar { get; }

        SynchronizationContext SynchronizationContext { get; }

        IToolbarCollection Toolbars { get; }

        IAppView View { get; }

        IMainView MainView { get; }

        bool Initialized { get; }

        bool SetCurrentTool(YutaiTool tool);

        IGeometry BufferGeometry
        {
            get;
            set;
        }


    }
}
