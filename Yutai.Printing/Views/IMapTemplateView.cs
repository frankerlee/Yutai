using System;
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
    public interface IMapTemplateView:IMenuProvider
    {
        void Initialize(IAppContext context, PrintingPlugin plugin);
        void SetBuddyControl();
        void ClearEvents();

        void InitEvents();
    }
    public enum MapTemplateCommand
    {
        Clear = 0,
    }

    public class MapTemplatePresenter : CommandDispatcher<IMapTemplateView, MapTemplateCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        private IMap _map;
        private IMapControlEvents2_Event _mapEvents;
        private PrintingPlugin _plugin;
        private IPoint _queryPoint;

        public MapTemplatePresenter(IAppContext context, IMapTemplateView view, PrintingPlugin plugin)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _plugin = plugin;
            view.Initialize(_context,_plugin);
            ((IAppContextEvents)_context).OnActiveHookChanged+= OnOnActiveHookChanged;
            _context.MainView.ArcGISControlChanging+= MainViewOnArcGisControlChanging;

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
                _context.DockPanels.ShowDockPanel(((IDockPanelView) View).DockName, false, false);
            }
        }

        private void OnOnActiveHookChanged(object object0)
        {
            if (_context.MainView.ControlType != GISControlType.PageLayout)
            {
                var panel = _context.DockPanels.GetDockPanel(((IDockPanelView)View).DockName);
                if (panel != null)
                {
                    panel.Visible = false;
                    _context.DockPanels.ShowDockPanel(((IDockPanelView)View).DockName,false,false);
                }
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


        public override void RunCommand(MapTemplateCommand command)
        {
            switch (command)
            {
                case MapTemplateCommand.Clear:
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

        public void ClearEvents()
        {
            View.ClearEvents();
        }

        public void InitEvents()
        {
            View.InitEvents();
        }
    }
}