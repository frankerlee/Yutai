using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Syncfusion.Windows.Forms.Tools;
using Yutai.Forms;
using Yutai.Helper;
using Yutai.Menu;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Shared;
using Yutai.UI.Docking;
using Yutai.UI.Helpers;
using Yutai.UI.Menu;
using Action = System.Action;

namespace Yutai.Views
{
    public partial class MainView : Yutai.UI.Forms.MainWindowView, IMainView
    {
        public const string SerializationKey = ""; // intentionally empty
        private const string WindowTitle = "Yutai 地理信息平台";
        private readonly IAppContext _context;
        private bool _locked;
        private bool _rendered;
       

        public MainView(IAppContext context)
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
            {
                Logger.Current.Trace("End MainView InitializeComponent");


                statusStripEx1.Items.Clear();
                statusStripEx1.Refresh();

                ToolTipHelper.Init(this.superToolTip1);

                this.FormClosing += MainView_FormClosing;

                this.Shown += MainView_Shown;
                Logger.Current.Trace("End MainView");
            }
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
                _dockingManager1.SaveLayout(SerializationKey, false);
                _mainFrameBarManager1.SaveLayout(SerializationKey, false);
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

        public IMapControl3 MapControl { get { return (IMapControl3)axMapControl1.Object; } }
        public AxMapControl MapControlContainer { get { return axMapControl1; } }
        public YutaiTool CurrentTool { get; set; }
        public IActiveView ActiveView { get; set; }
        public IMap FocusMap { get; set; }
        public IPageLayoutControl3 PageLayoutControl { get; set; }
        public string ActiveViewType { get; }
        public object ActiveGISControl { get; }
        public object ActiveControl { get; }
        public void ActivateMap()
        {
            throw new NotImplementedException();
        }

        public void ActivatePageLayout()
        {
            throw new NotImplementedException();
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
                handler(this, new RenderedEventArgs { Rendered = rendered });
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

            _context.DockPanels.Unlock();

            // don't set it initially or it will cause a lot of resizing
            // with reallocation of buffer when panels / toolbars are loaded
            //  _mapControl1.Dock = DockStyle.Fill;

            statusStripEx1.ContextMenuStrip.Opening += OnStatusBarCustomizationMenuOpening;

            Invoke(BeforeShow);

            AppConfig.Instance.FirstRun = false;

            Show();

            Activate();

            Application.Run(this);
        }

        private void UpdateStatusBarCustomizationMenu()
        {
           
        }

        private void OnStatusBarCustomizationMenuOpening(object sender, CancelEventArgs e)
        {
           
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
            throw new NotImplementedException();
        }

        public string GetMapTooltip()
        {
            throw new NotImplementedException();
        }


        public void SetTooltip(string msg)
        {
           // ToolTipInfo info=new ToolTipInfo();
           //this.superToolTip1.SetToolTip(this.axMapControl1,new ToolTipInfo() { });
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
            get { return _dockingManager1; }
        }

        public object MenuManager
        {
            get { return _mainFrameBarManager1; }
        }

        public object RibbonManager
        {
            get { return ribbonControlAdv1; }
        }

        public object RibbonStatusBar { get { return null; } }

        public object StatusBar
        {
            get { return statusStripEx1; }
        }

        public object MapContainer
        {
            get { return tabControl1; }
        }

        public IView View
        {
            get { return this; }
        }


        #endregion
    }
}
