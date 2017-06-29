using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;

namespace Yutai.Controls
{
    public class MapLegendPresenter : CommandDispatcher<MapLegendDockPanel, MapLegendCommand>
    {
        private readonly IAppContext _context;
        private readonly IBroadcasterService _broadcaster;
        private readonly MapLegendDockPanel _legendDockPanel;

        public MapLegendPresenter(IAppContext context, IBroadcasterService broadcaster,
                               MapLegendDockPanel legendDockPanel)
            : base(legendDockPanel)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (broadcaster == null) throw new ArgumentNullException("broadcaster");
            if (legendDockPanel == null) throw new ArgumentNullException("legendDockPanel");

            _context = context;
            _broadcaster = broadcaster;
            _legendDockPanel = legendDockPanel;
            //_context.MainView.ArcGISControlChanging += MainView_ArcGISControlChanging;
            //View.LegendKeyDown += OnLegendKeyDown;
        }

        private void MainView_ArcGISControlChanging(object sender, EventArgs e)
        {
           
            if (_context.MainView.ActiveViewType.StartsWith("M"))
            {
                _legendDockPanel.LegendControl.SetBuddyControl(_context.MainView.MapControl);
            }
            else
            {
                _legendDockPanel.LegendControl.SetBuddyControl(_context.MainView.PageLayoutControl);
            }
        }

        private void OnLegendKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete )//&& _legendDockPanel.Legend.SelectedLayer != null)
            {
                RunCommand(MapLegendCommand.RemoveLayer);
            }
        }

        public MapLegendDockPanel LegendControl
        {
            get { return _legendDockPanel; }
        }

        public override void RunCommand(MapLegendCommand command)
        {
        
        }
    }
}
