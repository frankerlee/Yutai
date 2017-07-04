using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using Syncfusion.Windows.Forms.Tools;
using Yutai.ArcGIS.Catalog;
using Yutai.ArcGIS.Common;
using Yutai.Controls;
using Yutai.Forms;
using Yutai.Helper;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;
using Yutai.Services.Serialization;
using Yutai.Shared;
using Yutai.UI.Docking;
using Yutai.UI.Forms;
using Yutai.UI.Menu;
using Yutai.UI.Menu.Ribbon;
using Yutai.UI.Style;
using Yutai.Views;
using AfterDraw = Yutai.Plugins.Interfaces.AfterDraw;

namespace Yutai
{
    public class AppContext : ISecureContext, IYTHookHelper, IAppContextEvents
    {
        private readonly IStyleService _styleService;
        private IConfigService _configService;
        private IProjectService _project;
        private IToolbarCollection _toolbars;
        private IPageLayoutControl2 _pageLayoutControl2 = null;

        private MapLegendPresenter _mapLegendPresenter;
        private OverviewPresenter _overviewPresenter;
        private string _currentToolName;
        private string _oldToolName;
        private object _hook;
        private IStyleGallery m_pStyleGallery;
        private ToolTip _toolTip;


        private IGxSelection _gxSelection;
        private IGxCatalog _gxCatalog;
        private IGxObject _gxObject;
        private YutaiTool _currentTool;

        public IBroadcasterService Broadcaster { get; private set; }

        public object GxSelection
        {
            get { return _gxSelection as object; }
            set
            {
                if (_gxSelection is IGxSelectionEvents)
                    (_gxSelection as IGxSelectionEvents).OnSelectionChanged -=
                        new OnSelectionChangedEventHandler(GxSelection_Changed);
                _gxSelection = value as IGxSelection;
                if (_gxSelection is IGxSelectionEvents)
                    (_gxSelection as IGxSelectionEvents).OnSelectionChanged +=
                        new OnSelectionChangedEventHandler(GxSelection_Changed);
            }
        }

        public object GxCatalog
        {
            get { return _gxCatalog; }
            set
            {
                _gxCatalog = value as IGxCatalog;
                GxSelection = _gxCatalog.Selection;
            }
        }

        public object GxObject
        {
            get { return _gxObject; }
            set { _gxObject = value as IGxObject; }
        }


        public AppConfig Config
        {
            get { return _configService.Config; }
        }

        public IApplicationContainer Container { get; }

        public IRibbonMenu RibbonMenu { get; private set; }
        public IDockPanelCollection DockPanels { get; private set; }

        public IMenu Menu { get; private set; }

        public IProject Project
        {
            get { return _project as IProject; }
        }

        public ITOCControl2 MapLegend { get; private set; }

        public IMapControl2 MapControl
        {
            get { return MainView.MapControl; }
        }

        public ITOCControl2 SceneLegend { get; private set; }

        public ISceneControl SceneControl { get; private set; }

        public IStatusBar StatusBar { get; private set; }

        public SynchronizationContext SynchronizationContext { get; private set; }

        public IToolbarCollection Toolbars { get; private set; }

        public IAppView View { get; private set; }

        public IMainView MainView { get; private set; }


        public bool Initialized { get; private set; }


        public string CurrentToolName { get; internal set; }


        public IGeometry BufferGeometry { get; set; }

        IStyleGallery IAppContext.StyleGallery { get; set; }
        public PyramidPromptType PyramidPromptType { get; set; }

        public bool IsInEdit { get; set; }

        public bool CanEdited { get; set; }


        public IPluginManager PluginManager { get; private set; }


        public XmlProject YutaiProject { get; set; }

        public AxMapControl MapControlContainer { get; set; }


        public ILayer CurrentLayer { get; set; }

        public double Tolerance
        {
            get { return Config.Tolerance; }
            set { Config.Tolerance = value; }
        }

        public IEngineSnapEnvironment SnapEnvironment
        {
            get { return Config.EngineSnapEnvironment; }
        }

        public IStyleGallery StyleGallery { get; }
        public string MapDocName { get; set; }

        public double SnapTolerance
        {
            get { return Config.SnapTolerance; }
            set { Config.SnapTolerance = value; }
        }

        public IMapDocument MapDocument { get; set; }

        public YutaiTool CurrentTool
        {
            get { return _currentTool; }
            set
            {
                SetCurrentTool((YutaiTool) value);
                _currentTool = (YutaiTool) value;
            }
        }


        public object Hook
        {
            get
            {
                if (_hook == null) return FocusMap;
                return _hook;
            }
            set { _hook = value; }
        }

        public IActiveView ActiveView
        {
            get { return MainView.ActiveView; }
        }

        public IPageLayout PageLayout
        {
            get
            {
                if (_pageLayoutControl2 != null) return _pageLayoutControl2.PageLayout;
                else return null;
            }
        }


        public IMap FocusMap
        {
            get { return MainView.FocusMap; }
        }

        public IOperationStack OperationStack { get; private set; }

        public Control GetDockPanelObject(string dockName)
        {
            if (dockName == MapLegendDockPanel.DefaultDockName)
                return _mapLegendPresenter.LegendControl as Control;
            else if (dockName == OverviewDockPanel.DefaultDockName)

                return _overviewPresenter.OverviewControl as Control;
            else
                throw new ArgumentOutOfRangeException("panel");
        }

        private void GxSelection_Changed(IGxSelection igxSelection, object gobject)
        {
            if (_gxSelection == igxSelection)
                _gxObject = igxSelection.FirstObject;
        }


        public AppContext(
            IApplicationContainer container,
            IStyleService styleService
        )
        {
            Logger.Current.Trace("In AppContext");
            if (container == null) throw new ArgumentNullException("container");
            if (styleService == null) throw new ArgumentNullException("styleService");
            Container = container;
            _styleService = styleService;
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
            MapLegendPresenter mapLegendPresenter,
            OverviewPresenter overviewPresenter
        )
        {
            Logger.Current.Trace("Start AppContext.Init()");
            if (mainView == null) throw new ArgumentNullException("mainView");
            if (project == null) throw new ArgumentNullException("project");
            if (mapLegendPresenter == null) throw new ArgumentNullException("legendPresenter");
            //初始化图例控件
            _mapLegendPresenter = mapLegendPresenter;
            var legend = _mapLegendPresenter.LegendControl;
            legend.LegendControl.SetBuddyControl(mainView.MapControl);


            // it's expected here that we are on the UI thread
            SynchronizationContext = SynchronizationContext.Current;

            PluginManager = Container.GetSingleton<IPluginManager>();
            Broadcaster = Container.GetSingleton<IBroadcasterService>();
            Container.RegisterInstance<IMapControl2>(mainView.MapControl);

            MainView = mainView;
            View = new AppView(mainView, _styleService);
            _project = project;
            _configService = configService;
            MainView.AddFrameworkControl(legend.LegendControl.Object);

            _overviewPresenter = overviewPresenter;
            _overviewPresenter.SetBuddyControl(mainView.MapControl);
            MainView.AddFrameworkControl(_overviewPresenter.OverviewControl);

            //  _map = mainView.Map;
            //  
            //   Repository = repository;

            //  Legend.Lock();

            DockPanels = new DockPanelCollection(mainView.DockingManager, mainView as Form, Broadcaster, _styleService);

            //Menu到最后丢弃不用，Menu部分全部采用Ribbon
            RibbonMenu = RibbonFactory.InitMenus((RibbonControl) mainView.RibbonManager,
                mainView.RibbonStatusBar as RibbonStatusBar);

            // Menu = MenuFactory.CreateMainMenu(mainView.RibbonManager,true);
            // Toolbars = MenuFactory.CreateMainToolbars(mainView.MenuManager);
            // StatusBar = MenuFactory.CreateStatusBar(mainView.RibbonStatusBar, PluginIdentity.Default);

            // _projectionDatabase.ReadFromExecutablePath(Application.ExecutablePath);

            // Repository.Initialize(this);

            // comment this line to prevent locator loading            
            // may be useful for ocx debugging to not create additional 
            // instance of map
            // _locator = new LocatorPresenter(_map);

            this.InitDocking();

            //YTHookHelper设置

            OperationStack = new OperationStackClass();
            m_pStyleGallery = null;

            //Catalog配置
            GxCatalog = new GxCatalog();
            //GxSelection=new GxSelection();
            //if (this._gxSelection is IGxSelectionEvents)
            //{
            //    (this._gxSelection as IGxSelectionEvents).OnSelectionChanged += new OnSelectionChangedEventHandler(this.GxSelection_Changed);
            //}

            Initialized = true;
            Logger.Current.Trace("End AppContext.Init()");

            //为了减少修改，给ApplicationRef赋值
            ApplicationRef.AppContext = this;
        }

        internal void InitPlugins(IConfigService configService)
        {
            var pluginManager = PluginManager;
            pluginManager.PluginUnloaded += ManagerPluginUnloaded;
            pluginManager.AssemblePlugins((ISplashView) SplashView.Instance);

            var guids = configService.Config.ApplicationPlugins;
            if (guids != null)
                PluginManager.RestoreApplicationPlugins(guids, this);
        }

        private void ManagerPluginUnloaded(object sender, PluginEventArgs e)
        {
            //Toolbars.RemoveItemsForPlugin(e.Identity);
            //Menu.RemoveItemsForPlugin(e.Identity);
            DockPanels.RemoveItemsForPlugin(e.Identity);
            //Toolbox.RemoveItemsForPlugin(e.Identity);
            //StatusBar.RemoveItemsForPlugin(e.Identity);
        }

        public void RefreshContextMenu()
        {
            RibbonMenu.RefreshContextMenu();
        }

        public void RunConfigPage(string pageKey)
        {
            Config.LoadAllConfigPages = false;
            Config.CustomConfigPages = pageKey;
            var model = Container.GetInstance<ConfigViewModel>();
            Container.Run<ConfigPresenter, ConfigViewModel>(model);
        }

        public void ClearCurrentTool()
        {
            MainView.MapControl.CurrentTool = null;
        }

        public bool SetCurrentTool(YutaiTool tool)
        {
            if (tool.ItemType == RibbonItemType.Tool)
            {
                ITool oldTool = MainView.CurrentTool;
                _oldToolName = oldTool == null ? string.Empty : ((YutaiTool) oldTool).Name;
                MainView.CurrentTool = (YutaiTool) tool;
                CurrentToolName = tool.Name;
                RibbonMenu.ChangeCurrentTool(_oldToolName, tool.Name);
                if (tool is IToolContextMenu)
                    RibbonMenu.SetContextMenu(MainView.MapControlContainer);

                return true;
            }
            return false;
        }

        public bool SetCurrentTool(string toolName)
        {
            ITool oldTool = MainView.CurrentTool;
            _oldToolName = oldTool == null ? string.Empty : ((YutaiTool) oldTool).Name;

            BarItem item = RibbonMenu.SubItems.FindItem(toolName);
            if (item != null)
            {
                MainView.CurrentTool = (YutaiTool) item.Tag;
                RibbonMenu.ChangeCurrentTool(_oldToolName, toolName);
            }


            return true;
        }

        public bool UpdateContextMenu()
        {
            if (RibbonMenu.GetContextMenuVisible()) return false;
            RibbonMenu.SetContextMenu(MainView.MapControlContainer);
            return true;
        }

        public void ResetCurrentTool()
        {
        }

        public void ShowSplashMessage(string msg)
        {
            SplashView.Instance.ShowStatus(msg);
        }

        public void UpdateUI()
        {
            // throw new NotImplementedException();
            if (RibbonMenu != null)
                RibbonMenu.UpdateMenu();
        }

        public void SetToolTip(string str)
        {
            // throw new NotImplementedException();

            if (Config.EngineSnapEnvironment != null)
                if ((Config.EngineSnapEnvironment as IEngineEditProperties2).SnapTips)
                {
                    if (MainView != null)
                        MainView.SetMapTooltip(str);
                }
                else if (!string.IsNullOrEmpty(MainView.GetMapTooltip()))
                {
                    MainView.SetMapTooltip(str);
                }
        }

        public void Close()
        {
            MainView.Close();
        }

        #region 事件处理

        private IActiveViewEvents_Event iactiveViewEvents_Event_0 = null;


        public event MapReplacedHandler MapReplaced;

        public event OnActiveHookChangedHandler OnActiveHookChanged;

        public event OnApplicationClosedHandler OnApplicationClosed;

        public event OnCurrentLayerChangeHandler OnCurrentLayerChange;

        public event OnCurrentToolChangedHandler OnCurrentToolChanged;

        public event OnDockWindowsEventHandler OnDockWindowsEvent;

        public event OnHideDockWindowEventHandler OnHideDockWindowEvent;

        public event OnLayerDeletedHandler OnLayerDeleted;

        public event OnMapClipChangedEventHandler OnMapClipChangedEvent;

        public event OnMapCloseEventHandler OnMapCloseEvent;

        public event OnMapDocumentChangedEventHandler OnMapDocumentChangedEvent;

        public event OnMapDocumentSaveEventHandler OnMapDocumentSaveEvent;

        public event OnMessageEventHandler OnMessageEvent;

        public event OnMessageEventHandlerEx OnMessageEventEx;

        public event OnShowCommandStringHandler OnShowCommandString;

        public event OnUpdateUIEventHandler OnUpdateUIEvent;


        public void AcvtiveHookChanged(object hook)
        {
            if (OnActiveHookChanged != null)
                OnActiveHookChanged(hook);
        }

        public void AddAfterDrawCallBack(AfterDraw afterDraw_0)
        {
        }

        public void AddCommands(YutaiCommand icommand)
        {
            //最后交给RibbonMenu处理
        }

        public void LayerDeleted(ILayer ilayer_2)
        {
            if (OnLayerDeleted != null)
                OnLayerDeleted(ilayer_2);
        }

        public void MapClipChanged(object object_3)
        {
            if (OnMapClipChangedEvent != null)
                OnMapClipChangedEvent(object_3);
        }


        public void MapDocumentSave(string string_2)
        {
            if (OnMapDocumentSaveEvent != null)
                OnMapDocumentSaveEvent(string_2);
        }

        public void HideDockWindow(object object_0)
        {
        }

        bool IYTHookHelper.ShowCommandString(string string_0, CommandTipsType commandTipsType_0)
        {
            return true;
        }

        public void MapDocumentChanged()
        {
            if (OnMapDocumentChangedEvent != null)
                OnMapDocumentChangedEvent();
        }

        public void ShowCommandString(string msg, CommandTipsType tipType)
        {
            //throw new NotImplementedException();
        }

        public void SetStatus(string empty)
        {
            if (OnMessageEvent != null)
                OnMessageEvent(empty);
        }

        public void SetStatus(int int_0, string string_0)
        {
            if (OnMessageEventEx != null)
                OnMessageEventEx(int_0, string_0);
        }

        public void DockWindows(object object_0, Bitmap bitmap_0)
        {
            //
        }

        #endregion
    }
}