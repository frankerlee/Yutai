using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.Controls;
using Yutai.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Yutai.Shared;
using Yutai.UI.Docking;
using Yutai.UI.Forms;
using Yutai.UI.Menu;
using Yutai.UI.Style;

namespace Yutai
{
    public class AppContext : ISecureContext
    {
        private readonly IApplicationContainer _container;
        private readonly IStyleService _styleService;
        private IConfigService _configService;
        private IProjectService _project;
        private IToolbarCollection _toolbars;
        private IMainView _mainView;

        private MapLegendPresenter _mapLegendPresenter;

        public AppContext(
            IApplicationContainer container,
            IStyleService styleService
          )
        {
            Logger.Current.Trace("In AppContext");
            if (container == null) throw new ArgumentNullException("container");
            if (styleService == null) throw new ArgumentNullException("styleService");
            _container = container;
            _styleService = styleService;
        }

        public IBroadcasterService Broadcaster { get; private set; }
        public AppConfig Config
        {
            get { return _configService.Config; }
        }

        public IApplicationContainer Container
        {
            get { return _container; }
        }

        public IDockPanelCollection DockPanels { get; private set; }

        public IMenu Menu
        {
            get;
            private set;
        }

        public IProject Project
        {
            get { return _project as IProject; }
        }

        public ITOCControl2 MapLegend
        {
            get;
            private set;
        }

        public IMapControl2 MapControl
        {
            get { return _mainView.MapControl; }
        }

        public ITOCControl2 SceneLegend
        {
            get;
            private set;
        }

        public ISceneControl SceneControl
        {
            get;
            private set;
        }

        public IStatusBar StatusBar
        {
            get;
            private set;
        }

        public SynchronizationContext SynchronizationContext
        {
            get;
            private set;
        }

        public IToolbarCollection Toolbars
        {
            get;
            private set;
        }

        public IAppView View
        {
            get;
            private set;
        }

        public IMainView MainView
        {
            get { return _mainView; }
        }

        public void Close()
        {
            _mainView.Close();
        }
        public bool Initialized
        {
            get;
            private set;
        }

        public bool SetCurrentTool(YutaiTool tool)
        {
            if (tool.ItemType == RibbonItemType.Tool)
            {
                _mainView.MapControl.CurrentTool = (ITool) tool;
                return true;
            }
            return false;
        }

        public IPluginManager PluginManager
        {
            get;
            private set;
        }

        public Control GetDockPanelObject(DefaultDockPanel panel)
        {
            switch (panel)
            {
                case DefaultDockPanel.MapLegend:
                    return _mapLegendPresenter.LegendControl as Control;
                /*  case DefaultDockPanel.Toolbox:
                     return _toolboxPresenter.View;
                 case DefaultDockPanel.Locator:
                     return _locator != null ? _locator.GetInternalObject() : null;*/
                default:
                    throw new ArgumentOutOfRangeException("panel");
            }
        }

        


        /// <summary>
        /// Sets all the necessary references from the main view. 
        /// </summary>
        /// <remarks>We don't use contructor injection here since most of other services use this one as a parameter.
        /// Perhaps property injection can be used.</remarks>
        internal void Init(
            IMainView mainView,
            IProjectService project,
            IConfigService configService,
            MapLegendPresenter mapLegendPresenter
           )
        {
            Logger.Current.Trace("Start AppContext.Init()");
            if (mainView == null) throw new ArgumentNullException("mainView");
            /*  if (project == null) throw new ArgumentNullException("project");*/

            if (mapLegendPresenter == null) throw new ArgumentNullException("legendPresenter");
            //初始化图例控件
            _mapLegendPresenter = mapLegendPresenter;
            var legend = _mapLegendPresenter.LegendControl;
            legend.LegendControl.SetBuddyControl(mainView.MapControl);
            /*  if (toolboxPresenter == null) throw new ArgumentNullException("toolboxPresenter");

              _toolboxPresenter = toolboxPresenter;
              _legendPresenter = legendPresenter;
              var legend = _legendPresenter.Legend;
              mainView.Map.Legend = legend;
              legend.Map = mainView.Map;*/

            // it's expected here that we are on the UI thread
            SynchronizationContext = SynchronizationContext.Current;

            PluginManager = _container.GetSingleton<IPluginManager>();
            Broadcaster = _container.GetSingleton<IBroadcasterService>();
            _container.RegisterInstance<IMapControl2>(mainView.MapControl);

            _mainView = mainView;
            View = new AppView(mainView, _styleService);
            /*    _project = project;
              //  _map = mainView.Map;
                _configService = configService;*/
            //   Repository = repository;

            //  Legend.Lock();

            DockPanels = new DockPanelCollection(mainView.DockingManager, mainView as Form, Broadcaster, _styleService);

            //Menu 和Toolbars需要修改，区分样式是Ribbon还是正常方式，参数从configService里面获取
            
            
           // Menu = MenuFactory.CreateMainMenu(mainView.RibbonManager,true);
           // Toolbars = MenuFactory.CreateMainToolbars(mainView.MenuManager);
            StatusBar = MenuFactory.CreateStatusBar(mainView.StatusBar, PluginIdentity.Default);

            // _projectionDatabase.ReadFromExecutablePath(Application.ExecutablePath);

            // Repository.Initialize(this);

            // comment this line to prevent locator loading            
            // may be useful for ocx debugging to not create additional 
            // instance of map
            // _locator = new LocatorPresenter(_map);

            this.InitDocking();

            Initialized = true;
            Logger.Current.Trace("End AppContext.Init()");
        }
    }
}
