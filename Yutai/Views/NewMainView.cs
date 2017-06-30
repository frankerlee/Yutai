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
using Yutai.Plugins.Enums;
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
        private IMapControl3 _mapControl;
        private IPageLayoutControl2 _layoutControl;
        private ControlsSynchronizer _controlsSynchronizer;

#region Events
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
#endregion


        public NewMainView(IAppContext context)
        {
            _context = context;
            InitializeComponent();
            _mapControl = this.axMapControl1.Object as IMapControl3;
            _layoutControl=this.axPageLayoutControl1.Object as IPageLayoutControl2;
            

            this.FormClosing += MainView_FormClosing;
            this.Shown += MainView_Shown;
            Logger.Current.Trace("End MainView");
        }

        private void MainView_Shown(object sender, EventArgs e)
        {
            _rendered = true;
            _controlsSynchronizer = new ControlsSynchronizer(_mapControl, _layoutControl);
            this.tabContent.SelectedTabPageIndex = 0;
            IMap map = new MapClass();
            map.Name = "地图";
            _controlsSynchronizer.ReplaceMap(map);
            tabContent.TabPages[1].PageVisible = false;
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

        //public IMapControl3 MapControl
        //{
        //    //get { return (IMapControl3) axMapControl1.Object; }
        //    get { return _mapControl; }
        //}

        public IMapControl3 MapControl { get{return _mapControl;} }

        public AxMapControl MapControlContainer
        {
            get { return axMapControl1; }
        }

        public YutaiTool CurrentTool { get{return _controlsSynchronizer.CurrentTool;} set { _controlsSynchronizer.CurrentTool=value; } }
        public IActiveView ActiveView { get { return _controlsSynchronizer.ActiveView; } }
        public IMap FocusMap { get { return _controlsSynchronizer.FocusMap; } }
        public IPageLayoutControl2 PageLayoutControl { get {return _layoutControl;} }
        public string ActiveViewType { get { return _controlsSynchronizer.ActiveViewType; } }
        public object ActiveGISControl { get { return _controlsSynchronizer.ActiveControl; } }
        public void ActivateMap()
        {
            tabContent.TabPages[0].PageVisible = true;
            tabContent.SelectedTabPageIndex = 0;
        }

        public void ActivatePageLayout()
        {
          //  _controlsSynchronizer.ActivatePageLayout();
            tabContent.TabPages[1].PageVisible = true;
            tabContent.SelectedTabPageIndex = 1;
        }

        public GISControlType ControlType { get; }


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
           // this.toolTipController1.SetToolTip(this.axMapControl1, msg);
        }

        public string GetMapTooltip()
        {
            return this.toolTipController1.GetToolTip(axMapControl1);
        }

        public void SetTooltip(string msg)
        {
           // this.toolTipController1.SetToolTip(this.axMapControl1, msg);
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

       
        

        private void tabContent_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page == pageMap)
            {
                tabContent.TabPages[0].PageVisible = true;
                _controlsSynchronizer.ActivateMap();
                tabContent.TabPages[1].PageVisible = false;
            }
            else if (e.Page == pageLayout)
            {
                tabContent.TabPages[1].PageVisible = true;
                _controlsSynchronizer.ActivatePageLayout();
                IMap map = _mapControl.Map;
                _controlsSynchronizer.ReplaceMap(map);
                tabContent.TabPages[0].PageVisible = false;
            }
        }
    }
}