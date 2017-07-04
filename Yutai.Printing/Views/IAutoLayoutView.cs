using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Printing.Views
{
    public interface IAutoLayoutView : IMenuProvider
    {
        void Initialize(IAppContext context, PrintingPlugin plugin);
        void SetBuddyControl();
    }

    public enum AutoLayoutCommand
    {
        Close = 0,
    }

    public class AutoLayoutPresenter : CommandDispatcher<IAutoLayoutView, AutoLayoutCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        private IMap _map;
        private IMapControlEvents2_Event _mapEvents;
        private PrintingPlugin _plugin;
        private IPoint _queryPoint;
        

        public AutoLayoutPresenter(IAppContext context, IAutoLayoutView view, PrintingPlugin plugin)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _plugin = plugin;
            view.Initialize(_context, _plugin);
            ((IAppContextEvents)_context).OnActiveHookChanged += OnOnActiveHookChanged;
            _context.MainView.ArcGISControlChanging += MainViewOnArcGisControlChanging;

            //view.ModeChanged += OnIdentifierModeChanged;
            //view.ItemSelected += OnItemSelected;
            //_plugin.MapIdentifying += PluginOnMapIdentifying;
            //_plugin.UnMapIdentify += PluginOnUnMapIdentify;
            //_plugin.StartMapIdentify += PluginOnStartMapIdentify;
        }

        private void MainViewOnArcGisControlChanging(object sender, EventArgs eventArgs)
        {
            if (_context.MainView.ControlType != GISControlType.PageLayout)
            {
                //_context.DockPanels.ShowDockPanel(((IDockPanelView)View).DockName, false, false);
            }
        }

        private void OnOnActiveHookChanged(object object0)
        {
            View.SetBuddyControl();
            if (_context.MainView.ControlType != GISControlType.MapControl)
            {
               

                //var panel = _context.DockPanels.GetDockPanel(((IDockPanelView)View).DockName);
                //if (panel != null)
                //{
                //    panel.Visible = false;
                //    _context.DockPanels.ShowDockPanel(((IDockPanelView)View).DockName, false, false);
                //}
            }
            else
            {
                
            }
        }

        //private void PluginOnStartMapIdentify(object sender, EventArgs eventArgs)
        //{
        //    View.InitializeActiveViewEvents();
        //    ActivatePanel();
        //}

        //private void PluginOnUnMapIdentify(object sender, EventArgs eventArgs)
        //{
        //    View.ClearActiveViewEvents();
        //}

        //private void PluginOnMapIdentifying(object sender, MapIdentifyArgs mapIdentifyArgs)
        //{
        //    View.Identify(mapIdentifyArgs.Envelope);
        //}


        public Control GetInternalObject()
        {
            return View as Control;
        }


        public override void RunCommand(AutoLayoutCommand command)
        {
            switch (command)
            {
                case AutoLayoutCommand.Close:
                    // View.Clear();
                    break;
            }
        }


        private void ActivatePanel()
        {
            var panel = _context.DockPanels.GetDockPanel(((IDockPanelView)View).DockName);
            if (panel != null)
            {
                panel.Visible = true;
                _context.DockPanels.SetActivePanel(((IDockPanelView)View).DockName);
            }
        }

        private void OnIdentifierModeChanged()
        {
            //MessageBox.Show("Mode Changed");
            // View.Identify(null);
        }

        private void OnItemSelected()
        {
        }
    }
}
