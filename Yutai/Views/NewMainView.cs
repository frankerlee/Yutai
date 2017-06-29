using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using Yutai.ArcGIS.Common;
using Yutai.Forms;
using Yutai.Menu;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Shared;
using Yutai.UI.Helpers;
using Yutai.UI.Menu;

namespace Yutai.Views
{
    public partial class NewMainView : Yutai.UI.Forms.MainWindowView2, IMainView
    {
        public const string SerializationKey = ""; // intentionally empty
        private const string WindowTitle = "Yutai 地理信息平台";
        private readonly IAppContext _context;
        private bool _locked;
        private bool _rendered;
        private YutaiTool _mapCurrentTool;
        private YutaiTool _layoutCurrentTool;

        private OnActiveHookChangedHandler onActiveHookChangedHandler;
        private OnMousePostionHandler onMousePostionHandler;

        public event OnActiveHookChangedHandler OnActiveHookChanged
        {
            add
            {
                OnActiveHookChangedHandler onActiveHookChangedHandler = this.onActiveHookChangedHandler;
                OnActiveHookChangedHandler onActiveHookChangedHandler2;
                do
                {
                    onActiveHookChangedHandler2 = onActiveHookChangedHandler;
                    OnActiveHookChangedHandler value2 =
                        (OnActiveHookChangedHandler) System.Delegate.Combine(onActiveHookChangedHandler2, value);
                    onActiveHookChangedHandler =
                        System.Threading.Interlocked.CompareExchange<OnActiveHookChangedHandler>(
                            ref this.onActiveHookChangedHandler, value2, onActiveHookChangedHandler2);
                } while (onActiveHookChangedHandler != onActiveHookChangedHandler2);
            }
            remove
            {
                OnActiveHookChangedHandler onActiveHookChangedHandler = this.onActiveHookChangedHandler;
                OnActiveHookChangedHandler onActiveHookChangedHandler2;
                do
                {
                    onActiveHookChangedHandler2 = onActiveHookChangedHandler;
                    OnActiveHookChangedHandler value2 =
                        (OnActiveHookChangedHandler) System.Delegate.Remove(onActiveHookChangedHandler2, value);
                    onActiveHookChangedHandler =
                        System.Threading.Interlocked.CompareExchange<OnActiveHookChangedHandler>(
                            ref this.onActiveHookChangedHandler, value2, onActiveHookChangedHandler2);
                } while (onActiveHookChangedHandler != onActiveHookChangedHandler2);
            }
        }

        public event OnMousePostionHandler OnMousePostion
        {
            add
            {
                OnMousePostionHandler onMousePostionHandler = this.onMousePostionHandler;
                OnMousePostionHandler onMousePostionHandler2;
                do
                {
                    onMousePostionHandler2 = onMousePostionHandler;
                    OnMousePostionHandler value2 =
                        (OnMousePostionHandler) System.Delegate.Combine(onMousePostionHandler2, value);
                    onMousePostionHandler =
                        System.Threading.Interlocked.CompareExchange<OnMousePostionHandler>(
                            ref this.onMousePostionHandler, value2, onMousePostionHandler2);
                } while (onMousePostionHandler != onMousePostionHandler2);
            }
            remove
            {
                OnMousePostionHandler onMousePostionHandler = this.onMousePostionHandler;
                OnMousePostionHandler onMousePostionHandler2;
                do
                {
                    onMousePostionHandler2 = onMousePostionHandler;
                    OnMousePostionHandler value2 =
                        (OnMousePostionHandler) System.Delegate.Remove(onMousePostionHandler2, value);
                    onMousePostionHandler =
                        System.Threading.Interlocked.CompareExchange<OnMousePostionHandler>(
                            ref this.onMousePostionHandler, value2, onMousePostionHandler2);
                } while (onMousePostionHandler != onMousePostionHandler2);
            }
        }


        public NewMainView(IAppContext context)
        {
            if (DesignMode == false)
            {
                Logger.Current.Trace("Start Main View");
            }
            _context = context;
            if (DesignMode == false)
            {
                Logger.Current.Trace("Start MainView InitializeComponent");
            }
            InitializeComponent();

            Logger.Current.Trace("End MainView InitializeComponent");

            //ActivateMap();
            //statusStripEx1.Items.Clear();
            //statusStripEx1.Refresh();

            //ToolTipHelper.Init(this.superToolTip1);

            this.FormClosing += MainView_FormClosing;

            this.Shown += MainView_Shown;
            Logger.Current.Trace("End MainView");
        }

        private void MainView_Shown(object sender, EventArgs e)
        {
            _rendered = true;

            UpdateView();

            //ForceTaskBarDisplay();
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;

            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                return;
            }

            if (!FireViewClosing())
            {
                e.Cancel = true;
            }
            else
            {
                //_dockingManager1.SaveLayout(SerializationKey, false);
                //_mainFrameBarManager1.SaveLayout(SerializationKey, false);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x00040000; // Turn on WS_EX_APPWINDOW
                return cp;
            }
        }

        public IMapControl3 MapControl
        {
            get { return (IMapControl3) axMapControl1.Object; }
        }

        public AxMapControl MapControlContainer
        {
            get { return axMapControl1; }
        }

        public YutaiTool CurrentTool
        {
            get
            {
                YutaiTool result;
                if (tabContent.SelectedTabPageIndex == 0)
                {
                    if (this.axMapControl1 != null)
                    {
                        result = this.axMapControl1.CurrentTool as YutaiTool;
                        return result;
                    }
                }
                else if (this.axPageLayoutControl1.CurrentTool != null)
                {
                    result = this.axPageLayoutControl1.CurrentTool as YutaiTool;
                    return result;
                }
                result = null;
                return result;
            }
            set
            {
                if (tabContent.SelectedTabPageIndex != 0)
                {
                    if (this.axPageLayoutControl1 != null)
                    {
                        this.axPageLayoutControl1.CurrentTool = value;
                    }
                }
                else
                {
                    if (this.axMapControl1 != null)
                    {
                        this.axMapControl1.CurrentTool = value;
                    }
                }
            }
        }

        public IActiveView ActiveView
        {
            get
            {
                return tabContent.SelectedTabPageIndex == 0 ? axMapControl1.ActiveView : axPageLayoutControl1.ActiveView;
            }
        }

        public IMap FocusMap
        {
            get
            {
                return tabContent.SelectedTabPageIndex == 0
                    ? axMapControl1.ActiveView.FocusMap
                    : axPageLayoutControl1.ActiveView.FocusMap;
            }
        }

        public IPageLayoutControl3 PageLayoutControl
        {
            get { return this.axPageLayoutControl1.Object as IPageLayoutControl3; }
        }

        public string ActiveViewType
        {
            get
            {
                string result;
                result = tabContent.SelectedTabPageIndex == 0 ? "MapControl" : "PageLayoutControl";
                return result;
            }
        }

        public object ActiveGISControl
        {
            get
            {
                if (this.axMapControl1 == null || this.axPageLayoutControl1 == null)
                {
                    throw new System.Exception("无法获取地图控件:\r\n不管是地图还是排版控件均为初始化!");
                }
                object @object;
                @object = tabContent.SelectedTabPageIndex == 0
                    ? this.axMapControl1.Object
                    : this.axPageLayoutControl1.Object;
                return @object;
            }
        }

        public void ActivateMap()
        {
            try
            {
                //this.axPageLayoutControl1.Visible = false;
                this.tabContent.TabPages[0].PageVisible = true;
                this.tabContent.TabPages[1].PageVisible = false;
                
                this.pageLayout.Visible = false;
                this.pageMap.Visible = true;
                //this.axMapControl1.Visible = true;
                if (this.axPageLayoutControl1.CurrentTool != null)
                {
                    this._layoutCurrentTool = this.axPageLayoutControl1.CurrentTool as YutaiTool;
                }
                if (this._mapCurrentTool != null)
                {
                    this.axMapControl1.CurrentTool = this._mapCurrentTool as ITool;
                }
                this.tabContent.SelectedTabPageIndex = 0;
                if (this.onActiveHookChangedHandler != null)
                {
                    this.onActiveHookChangedHandler(this);
                }
                if (this.axPageLayoutControl1.ActiveView != null &&
                    this.axPageLayoutControl1.ActiveView.FocusMap != null)
                {
                    this.axMapControl1.Map = this.axPageLayoutControl1.ActiveView.FocusMap;
                }
                FireArcGISControlChanging();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format("ControlsSynchronizer::ActivateMap:\r\n{0}", ex.Message));
            }
        }


        public void ActivatePageLayout()
        {
            try
            {
                this.tabContent.TabPages[1].PageVisible = true;
                this.tabContent.TabPages[0].PageVisible=false;
                
                if (this.axMapControl1.CurrentTool != null)
                {
                    this._mapCurrentTool = this.axMapControl1.CurrentTool as YutaiTool;
                }
                IMapFrame focusMapFrame = this.GetFocusMapFrame(this.axPageLayoutControl1.PageLayout);
                (focusMapFrame.Map as IMapClipOptions).ClipType = esriMapClipType.esriMapClipNone;
                if (focusMapFrame.Map is IMapAutoExtentOptions)
                {
                    (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentDefault;
                }
                (focusMapFrame.Map as IActiveView).Extent = this.axMapControl1.ActiveView.Extent;
                if (this._layoutCurrentTool != null)
                {
                    this.axPageLayoutControl1.CurrentTool = this._layoutCurrentTool;
                }
                this.tabContent.SelectedTabPageIndex = 1;
                if (this.onActiveHookChangedHandler != null)
                {
                    this.onActiveHookChangedHandler(this);
                }
                CopyLayerToLayout();
                FireArcGISControlChanging();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(string.Format("ControlsSynchronizer::ActivatePageLayout:\r\n{0}", ex.Message));
            }
        }

        private void CopyLayerToLayout()
        {
            IMaps maps = new Maps();
            maps.Add(this.axMapControl1.Map);
            this.axPageLayoutControl1.PageLayout.ReplaceMaps(maps);
            this.axPageLayoutControl1.ActiveView.Refresh();
        }

        internal IMapFrame GetFocusMapFrame(IPageLayout ipageLayout)
        {
            IGraphicsContainer graphicsContainer = ipageLayout as IGraphicsContainer;
            graphicsContainer.Reset();
            IMapFrame result;
            for (IElement element = graphicsContainer.Next(); element != null; element = graphicsContainer.Next())
            {
                if (element is IMapFrame)
                {
                    result = (element as IMapFrame);
                    return result;
                }
            }
            result = null;
            return result;
        }

        public event EventHandler<CancelEventArgs> ViewClosing;
        public event EventHandler<RenderedEventArgs> ViewUpdating;
        public event EventHandler<EventArgs> ArcGISControlChanging;

        public event Action BeforeShow;

        public void Lock()
        {
            _locked = true;
        }

        public void Unlock()
        {
            _locked = false;
            UpdateView();
        }

        private bool FireViewClosing()
        {
            var handler = ViewClosing;
            if (handler != null)
            {
                var args = new CancelEventArgs();
                handler(this, args);
                if (args.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private void FireViewUpdating(bool rendered)
        {
            var handler = ViewUpdating;
            if (handler != null)
            {
                handler(this, new RenderedEventArgs {Rendered = rendered});
            }
        }

        private void FireArcGISControlChanging()
        {
            var handler = ArcGISControlChanging;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        #region IView implementation

        private void RestorePreviousState()
        {
            //_dockingManager1.TryRestoreLayout(SerializationKey);
            //_mainFrameBarManager1.TryRestoreLayout(SerializationKey);
        }

        public override void ShowView(IWin32Window parent = null)
        {
            //RestorePreviousState();

            /* DockPanelHelper.ClosePanel(_context, DockPanelKeys.TableEditor);
             DockPanelHelper.ClosePanel(_context, DockPanelKeys.Tasks);

             DockPanelHelper.ShowPanel(_context, DockPanelKeys.MapLegend);*/

            Program.Timer.Stop();
            Logger.Current.Info("Loading time: " + Program.Timer.Elapsed);

            SplashView.Instance.Close();

            //_context.DockPanels.Unlock();

            // don't set it initially or it will cause a lot of resizing
            // with reallocation of buffer when panels / toolbars are loaded
            //  _mapControl1.Dock = DockStyle.Fill;

            //statusStripEx1.ContextMenuStrip.Opening += OnStatusBarCustomizationMenuOpening;

            Invoke(BeforeShow);

            AppConfig.Instance.FirstRun = false;

            Show();

            Activate();

            Application.Run(this);
        }

        private void UpdateStatusBarCustomizationMenu()
        {
            //var hash = new HashSet<string>
            //               {
            //                   StatusBarKeys.TileProvider,
            //                   StatusBarKeys.MapScale,
            //                   StatusBarKeys.MapUnits
            //                 //  StatusBarKeys.ProjectionDropDown
            //               };
            //try
            //{
            //    foreach (ToolStripItem item in statusStripEx1.ContextMenuStrip.Items)
            //    {
            //        var status = ReflectionHelper.GetInstanceField(item, "m_item") as ToolStripItem;
            //        if (status == null)
            //        {
            //            item.Visible = false;
            //            continue;
            //        }

            //        var metadata = status.Tag as MenuItemMetadata;

            //        if (metadata != null && hash.Contains(metadata.Key))
            //        {
            //            item.Text = StatusBarKeys.GetStatusItemName(metadata.Key);
            //            item.Visible = true;
            //            continue;
            //        }

            //        item.Visible = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.Current.Warn("Failed to update status bar customization menu.", ex);
            //}
        }

        private void OnStatusBarCustomizationMenuOpening(object sender, CancelEventArgs e)
        {
            //UpdateStatusBarCustomizationMenu();
        }

        private string GetCaption()
        {
            string caption = WindowTitle;

            if (_context.Project != null && !_context.Project.IsEmpty)
            {
                caption += @" - " + _context.Project.Filename;
            }

            return caption;
        }

        public void DoUpdateView(bool focusMap = true)
        {
            if (_locked) return;

            Text = GetCaption();

            // broadcast to plugins
            if (_rendered)
            {
                FireViewUpdating(_rendered);
            }

            /* if (ActiveForm == _mapControl1.ParentForm && focusMap)
             {
                 _mapControl1.Focus();
             }*/
        }

        public void SetMapTooltip(string msg)
        {
            this.toolTipController1.SetToolTip(this.axMapControl1, msg);
        }

        public string GetMapTooltip()
        {
            return this.toolTipController1.GetToolTip(axMapControl1);
        }

        public void SetTooltip(string msg)
        {
            this.toolTipController1.SetToolTip(this.axMapControl1, msg);
        }


        public override void UpdateView()
        {
            DoUpdateView();
        }

        public ButtonBase OkButton
        {
            get { return null; }
        }

        #endregion

        #region IMainView implementation

        public object DockingManager
        {
            get { return this.dockManager; }
        }

        public object MenuManager
        {
            get { return null; }
        }

        public object RibbonManager
        {
            get { return ribbonManager; }
        }

        public object RibbonStatusBar
        {
            get { return ribbonStatusBar; }
        }

        public object MapContainer
        {
            get { return tabContent; }
        }

        public IView View
        {
            get { return this; }
        }

        #endregion

        #region 地图和制图模块切换工作
        private int GetLayerIndex(IBasicMap pMap, ILayer pLayer)
        {
           
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (pMap.get_Layer(i) == pLayer)
                {
                    return i;
                }
            }
            return -1;
        }

        private void MoveLayerByIndex(object lyrObject, int newIndex)
        {
            if (lyrObject is ILayer && this.axMapControl1 != null)
            {
                int fromIndex = this.GetLayerIndex(this.axMapControl1.Map as IBasicMap, lyrObject as ILayer);
                this.axMapControl1.MoveLayerTo(fromIndex, newIndex);
                this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, lyrObject, null);
            }
        }

        private void CopyLayerToMap()
        {
            if (this.tabContent.SelectedTabPageIndex != 1) return;
            try
            {
                //if (this.iactiveViewEvents_Event_0 != null)
                //{
                //    this.iactiveViewEvents_Event_0.ItemAdded -= new IActiveViewEvents_ItemAddedEventHandler(this, (System.UIntPtr)ldftn(method_8));
                //    this.iactiveViewEvents_Event_0.ItemReordered -= new IActiveViewEvents_ItemReorderedEventHandler(this, (System.UIntPtr)ldftn(method_0));
                //    this.iactiveViewEvents_Event_0.ItemDeleted -= new IActiveViewEvents_ItemDeletedEventHandler(this, (System.UIntPtr)ldftn(method_7));
                //}
            }
            catch
            {
            }
            IMap focusMap = this.axPageLayoutControl1.ActiveView.FocusMap;
            //this.iactiveViewEvents_Event_0 = (focusMap as IActiveViewEvents_Event);
            //try
            //{
            //    if (this.iactiveViewEvents_Event_0 != null)
            //    {
            //        this.iactiveViewEvents_Event_0.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(this, (System.UIntPtr)ldftn(method_8));
            //        this.iactiveViewEvents_Event_0.ItemReordered += new IActiveViewEvents_ItemReorderedEventHandler(this, (System.UIntPtr)ldftn(method_0));
            //        this.iactiveViewEvents_Event_0.ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(this, (System.UIntPtr)ldftn(method_7));
            //    }
            //}
            //catch
            //{
            //}
            this.axMapControl1.Map.ClearLayers();
            (this.axMapControl1.Map as IActiveView).ContentsChanged();
            this.axMapControl1.Map.MapUnits = focusMap.MapUnits;
            this.axMapControl1.Map.SpatialReferenceLocked = false;
            this.axMapControl1.Map.SpatialReference = focusMap.SpatialReference;
            this.axMapControl1.Map.Name = focusMap.Name;
            for (int i = 0; i < focusMap.LayerCount; i++)
            {
                ILayer layer = focusMap.get_Layer(i);
                this.axMapControl1.AddLayer(layer, i);
            }
            (this.axMapControl1.Map as IGraphicsContainer).DeleteAllElements();
            IGraphicsContainer graphicsContainer = focusMap as IGraphicsContainer;
            graphicsContainer.Reset();
            IElement element = graphicsContainer.Next();
            int num = 0;
            while (element != null)
            {
                (this.axMapControl1.Map as IGraphicsContainer).AddElement(element, num);
                num++;
                element = graphicsContainer.Next();
            }
            (this.axMapControl1.Map as ITableCollection).RemoveAllTables();
            ITableCollection tableCollection = focusMap as ITableCollection;
            for (int i = 0; i < tableCollection.TableCount; i++)
            {
                (this.axMapControl1.Map as ITableCollection).AddTable(tableCollection.get_Table(i));
            }
            this.axMapControl1.ActiveView.Extent = (focusMap as IActiveView).Extent;
            this.axMapControl1.ActiveView.Refresh();
        }
        #endregion
    }
}