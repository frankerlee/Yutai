using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Locator.Enums;
using Yutai.Plugins.Mvp;
using Yutai.UI.Docking;

namespace Yutai.Plugins.Locator.Views
{
    public class LocatorPresenter : CommandDispatcher<ILocatorView, LocatorCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        private IMap _map;
        private IMapControlEvents2_Event _mapEvents;
        private LocatorPlugin _plugin;
        private IPoint _queryPoint;

        public LocatorPresenter(IAppContext context, ILocatorView view, LocatorPlugin plugin)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
            _map = _context.MapControl.Map;
            _plugin = plugin;
        }
        

        public Control GetInternalObject()
        {
            return View as Control;
        }



        public override void RunCommand(LocatorCommand command)
        {
            switch (command)
            {
                case LocatorCommand.Clear:
                    View.Clear();
                    break;
                case LocatorCommand.Search:
                    View.TrySearch(true);
                    break;
                default:
                    break;
            }
        }


        private void ActivatePanel()
        {
            _context.DockPanels.ShowDockPanel(LocatorDockPanel.DefaultDockName,true,true);
           
        }

       
        private void OnItemSelected()
        {

        }


    }
}
