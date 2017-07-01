using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Events;
using Yutai.Plugins.Identifer.Enums;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.UI.Docking;

namespace Yutai.Plugins.Identifer.Views
{
    public class IdentifierPresenter : CommandDispatcher<IIdentifierView, IdentifierCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        private IMap _map;
        private IMapControlEvents2_Event _mapEvents;
        private IdentifierPlugin _plugin;
        private IPoint _queryPoint;

        public IdentifierPresenter(IAppContext context, IIdentifierView view, IdentifierPlugin plugin)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _map = _context.MapControl.Map;
            _plugin = plugin;

            view.ModeChanged += OnIdentifierModeChanged;
            view.ItemSelected += OnItemSelected;
            _plugin.MapIdentifying += PluginOnMapIdentifying;
            _plugin.UnMapIdentify += PluginOnUnMapIdentify;
            _plugin.StartMapIdentify += PluginOnStartMapIdentify;
        }

        private void PluginOnStartMapIdentify(object sender, EventArgs eventArgs)
        {
            View.InitializeActiveViewEvents();
            ActivatePanel();
        }

        private void PluginOnUnMapIdentify(object sender, EventArgs eventArgs)
        {
            View.ClearActiveViewEvents();
        }

        private void PluginOnMapIdentifying(object sender, MapIdentifyArgs mapIdentifyArgs)
        {
            View.Identify(mapIdentifyArgs.Envelope);
        }


        public Control GetInternalObject()
        {
            return View as Control;
        }


        public override void RunCommand(IdentifierCommand command)
        {
            switch (command)
            {
                case IdentifierCommand.Clear:
                    View.Clear();
                    break;
            }
        }


        private void ActivatePanel()
        {
            var panel = _context.DockPanels.GetDockPanel(((IDockPanelView) View).DockName);
            if (panel != null)
            {
                panel.Visible = true;
                _context.DockPanels.SetActivePanel(((IDockPanelView) View).DockName);
            }
        }

        private void OnIdentifierModeChanged()
        {
            //MessageBox.Show("Mode Changed");
            View.Identify(null);
        }

        private void OnItemSelected()
        {
        }
    }
}