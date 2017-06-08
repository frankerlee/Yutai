using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
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

        public IMapControl2 MapControl { get { return (IMapControl2)axMapControl1.Object; } }
        
        public event EventHandler<CancelEventArgs> ViewClosing;

        public event EventHandler<RenderedEventArgs> ViewUpdating;

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
            var hash = new HashSet<string>
                           {
                               StatusBarKeys.TileProvider,
                               StatusBarKeys.MapScale,
                               StatusBarKeys.MapUnits
                             //  StatusBarKeys.ProjectionDropDown
                           };
            try
            {
                foreach (ToolStripItem item in statusStripEx1.ContextMenuStrip.Items)
                {
                    var status = ReflectionHelper.GetInstanceField(item, "m_item") as ToolStripItem;
                    if (status == null)
                    {
                        item.Visible = false;
                        continue;
                    }

                    var metadata = status.Tag as MenuItemMetadata;

                    if (metadata != null && hash.Contains(metadata.Key))
                    {
                        item.Text = StatusBarKeys.GetStatusItemName(metadata.Key);
                        item.Visible = true;
                        continue;
                    }

                    item.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Current.Warn("Failed to update status bar customization menu.", ex);
            }
        }

        private void OnStatusBarCustomizationMenuOpening(object sender, CancelEventArgs e)
        {
            UpdateStatusBarCustomizationMenu();
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
