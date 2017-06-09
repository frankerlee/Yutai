using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;

namespace Yutai.Plugins.Interfaces
{
    public interface IAppContext
    {
        AppConfig Config { get; }

        //界面采用Ribbon后的管理接口
        IRibbonMenu RibbonMenu { get; }

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

        string CurrentToolName { get; }

        IGeometry BufferGeometry
        {
            get;
            set;
        }

        IStyleGallery StyleGallery { get; set; }
        PyramidPromptType PyramidPromptType { get; set; }


        void ShowCommandString(string msg, CommandTipsType tipType);

        void SetStatus(string empty);
        void UpdateUI();
        void SetToolTip(string str);
        IBroadcasterService Broadcaster { get; }
    }
}
