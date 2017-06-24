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
using AfterDraw = Yutai.Plugins.Interfaces.AfterDraw;

namespace Yutai
{
    public class AppContext : ISecureContext,IYTHookHelper,IAppContextEvents
    {
        private readonly IApplicationContainer _container;
        private readonly IStyleService _styleService;
        private IConfigService _configService;
        private IProjectService _project;
        private IToolbarCollection _toolbars;
        private IMainView _mainView;
        private IPageLayoutControl2 _pageLayoutControl2 = null;
        private AxMapControl _axMapControl;

        private MapLegendPresenter _mapLegendPresenter;
        private OverviewPresenter _overviewPresenter;
        private XmlProject _yutaiProject;
        private IGeometry _bufferGeometry;
        private string _currentToolName;
        private bool _isInEdit;
        private bool _canEdited;
        private string _oldToolName;
        private object _hook;
        private IOperationStack m_pOperationStack;
        private IStyleGallery m_pStyleGallery;
        private ToolTip _toolTip;


        private IGxSelection _gxSelection;
        private IGxCatalog _gxCatalog;
        private IGxObject _gxObject;
        private YutaiTool _currentTool;

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

        public object GxSelection
        {
            get
            {
                return _gxSelection as object;
            }
            set
            {
                if (_gxSelection is IGxSelectionEvents)
                {
                    (_gxSelection as IGxSelectionEvents).OnSelectionChanged -= new OnSelectionChangedEventHandler(this.GxSelection_Changed);
                }
                _gxSelection = value as IGxSelection;
                if (_gxSelection is IGxSelectionEvents)
                {
                    (_gxSelection as IGxSelectionEvents).OnSelectionChanged += new OnSelectionChangedEventHandler(this.GxSelection_Changed);
                }
            }
        }

        private void GxSelection_Changed(IGxSelection igxSelection_1, object object_0)
        {
            if (this._gxSelection == igxSelection_1)
            {
                this._gxObject = igxSelection_1.FirstObject;
            }
        }

        public object GxCatalog {
            get
            {
                return _gxCatalog;
            }
            set
            {
                this._gxCatalog = value as IGxCatalog;
                this.GxSelection = this._gxCatalog.Selection;
            }
        }

        public object GxObject
        {
            get
            {
                return _gxObject;
            }
            set
            {
                _gxObject = value as IGxObject;
            }
        }

        public void RefreshContextMenu()
        {
            RibbonMenu.RefreshContextMenu();
        }

        public void ClearCurrentTool()
        {
            _mainView.MapControl.CurrentTool = null;
        }

        public AppConfig Config
        {
            get { return _configService.Config; }
        }

        public IApplicationContainer Container
        {
            get { return _container; }
        }

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
            get { return _mainView.MapControl; }
        }

        public ITOCControl2 SceneLegend { get; private set; }

        public ISceneControl SceneControl { get; private set; }

        public IStatusBar StatusBar { get; private set; }

        public SynchronizationContext SynchronizationContext { get; private set; }

        public IToolbarCollection Toolbars { get; private set; }

        public IAppView View { get; private set; }

        public IMainView MainView
        {
            get { return _mainView; }
        }

        public void Close()
        {
            _mainView.Close();
        }

        public bool Initialized { get; private set; }

        public bool SetCurrentTool(YutaiTool tool)
        {
            if (tool.ItemType == RibbonItemType.Tool)
            {
                ITool oldTool = _mainView.MapControl.CurrentTool;
                _oldToolName = oldTool == null ? string.Empty : ((YutaiTool) oldTool).Name;
                _mainView.MapControl.CurrentTool = (ITool) tool;
                CurrentToolName = tool.Name;
                RibbonMenu.ChangeCurrentTool(_oldToolName, tool.Name);
                if (tool is IToolContextMenu)
                {
                    RibbonMenu.SetContextMenu(_mainView.MapControlContainer);
                }
                
                return true;
            }
            return false;
        }

        public bool SetCurrentTool(string toolName)
        {
            ITool oldTool = _mainView.MapControl.CurrentTool;
            _oldToolName = oldTool == null ? string.Empty : ((YutaiTool)oldTool).Name;
           
            BarItem item=this.RibbonMenu.SubItems.FindItem(toolName);
            if (item != null)
            {
                _mainView.MapControl.CurrentTool = ((ITool) item.Tag);
                RibbonMenu.ChangeCurrentTool(_oldToolName, toolName);
            }


            return true;
        }

        public bool UpdateContextMenu()
        {
            if (RibbonMenu.GetContextMenuVisible()) return false;
            RibbonMenu.SetContextMenu(_mainView.MapControlContainer);
            return true;
        }

        public string CurrentToolName { get; internal set; }


        public IGeometry BufferGeometry
        {
            get { return _bufferGeometry; }
            set { _bufferGeometry = value; }
        }

        IStyleGallery IAppContext.StyleGallery { get; set; }
        public PyramidPromptType PyramidPromptType { get; set; }

        public bool IsInEdit
        {
            get { return _isInEdit; }
            set { _isInEdit = value; }
        }

        public bool CanEdited
        {
            get { return _canEdited; }
            set { _canEdited = value; }
        }


        public IPluginManager PluginManager { get; private set; }

        public Control GetDockPanelObject(string dockName)
        {
           if(dockName == MapLegendDockPanel.DefaultDockName)
                    return _mapLegendPresenter.LegendControl as Control;
           else if(dockName== OverviewDockPanel.DefaultDockName)
               
                    return _overviewPresenter.OverviewControl as Control;
             else
                    throw new ArgumentOutOfRangeException("panel");
            
        }

        public XmlProject YutaiProject
        {
            get { return _yutaiProject; }
            set { _yutaiProject = value; }
        }

        public AxMapControl MapControlContainer { get { return _axMapControl; } set
        {
            _axMapControl = value;
        } }


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
            _overviewPresenter = overviewPresenter;


            // it's expected here that we are on the UI thread
            SynchronizationContext = SynchronizationContext.Current;

            PluginManager = _container.GetSingleton<IPluginManager>();
            Broadcaster = _container.GetSingleton<IBroadcasterService>();
            _container.RegisterInstance<IMapControl2>(mainView.MapControl);

            _mainView = mainView;
            View = new AppView(mainView, _styleService);
            _project = project;
            _configService = configService;

            _overviewPresenter.SetBuddyControl(mainView.MapControl);
            //  _map = mainView.Map;
            //  
            //   Repository = repository;

            //  Legend.Lock();

            DockPanels = new DockPanelCollection(mainView.DockingManager, mainView as Form, Broadcaster, _styleService);

            //Menu到最后丢弃不用，Menu部分全部采用Ribbon
            RibbonMenu = RibbonFactory.InitMenus((RibbonControl) mainView.RibbonManager, mainView.RibbonStatusBar as RibbonStatusBar );

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

            m_pOperationStack = new OperationStackClass();
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
            pluginManager.AssemblePlugins();

            var guids = configService.Config.ApplicationPlugins;
            if (guids != null)
            {
                PluginManager.RestoreApplicationPlugins(guids, this);
            }
        }

        private void ManagerPluginUnloaded(object sender, PluginEventArgs e)
        {
            //Toolbars.RemoveItemsForPlugin(e.Identity);
            //Menu.RemoveItemsForPlugin(e.Identity);
            DockPanels.RemoveItemsForPlugin(e.Identity);
            //Toolbox.RemoveItemsForPlugin(e.Identity);
            //StatusBar.RemoveItemsForPlugin(e.Identity);
        }

       


        public ILayer CurrentLayer { get; set; }

        public double Tolerance
        {
            get { return Config.Tolerance; } set { Config.Tolerance = value; }
        }
        public IEngineSnapEnvironment SnapEnvironment { get { return Config.EngineSnapEnvironment; } }
        public IStyleGallery StyleGallery { get; }
        public string MapDocName { get; set; }
        public double SnapTolerance { get { return Config.SnapTolerance; } set { Config.SnapTolerance = value; } }
        public IMapDocument MapDocument { get; set; }
        public YutaiTool CurrentTool { get { return _currentTool; } set { SetCurrentTool((YutaiTool)value);
            _currentTool = (YutaiTool)value;
                
        } }

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
            {
                RibbonMenu.UpdateMenu();
            }
        }

        public void SetToolTip(string str)
        {
            // throw new NotImplementedException();

            if (this.Config.EngineSnapEnvironment != null)
            {
                if ((this.Config.EngineSnapEnvironment as IEngineEditProperties2).SnapTips)
                {
                    if (this._mainView != null)
                    {
                        
                        this._mainView.SetMapTooltip(str);
                    }
                }
                else if (!string.IsNullOrEmpty(this._mainView.GetMapTooltip()))
                {
                    this._mainView.SetMapTooltip(str);
                }
            }
        }

       

        public object Hook
        {
            get { if(_hook == null) return this.MapControl.Map;
                return _hook;
            }
            set { _hook = value; }
        }

        public IActiveView ActiveView
        {
            get { return MapControl.Map as IActiveView; }
        }

        public IPageLayout PageLayout {
            get
            {
                if (_pageLayoutControl2 != null) return _pageLayoutControl2.PageLayout;
                else return null;
            }
        }

      

        public IMap FocusMap { get { return MapControl.Map as IMap; } }
        public IOperationStack OperationStack { get { return m_pOperationStack; } }



        #region 事件处理
        private IActiveViewEvents_Event iactiveViewEvents_Event_0 = null;
        private IMap _focusMap;

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
            if (this.OnActiveHookChanged != null)
            {
                this.OnActiveHookChanged(hook);
            }
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
            if (this.OnLayerDeleted != null)
            {
                this.OnLayerDeleted(ilayer_2);
            }
        }

        public void MapClipChanged(object object_3)
        {
            if (this.OnMapClipChangedEvent != null)
            {
                this.OnMapClipChangedEvent(object_3);
            }
        }

    

        public void MapDocumentSave(string string_2)
        {
            if (this.OnMapDocumentSaveEvent != null)
            {
                this.OnMapDocumentSaveEvent(string_2);
            }
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
            if (this.OnMapDocumentChangedEvent != null)
            {
                this.OnMapDocumentChangedEvent();
            }
        }

        public void ShowCommandString(string msg, CommandTipsType tipType)
        {
            //throw new NotImplementedException();
        }

        public void SetStatus(string empty)
        {
            if (this.OnMessageEvent != null)
            {
                this.OnMessageEvent(empty);
            }
        }

        public void SetStatus(int int_0, string string_0)
        {
            if (this.OnMessageEventEx != null)
            {
                this.OnMessageEventEx(int_0, string_0);
            }
        }

        public void DockWindows(object object_0, Bitmap bitmap_0)
        {
            //
        }

        #endregion
    }
}