using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Controls;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.Plugins.Services;

namespace Yutai.Controls
{
    public class OverviewPresenter : CommandDispatcher<OverviewDockPanel, OverviewCommand>
    {
        private readonly IAppContext _context;
        private readonly IBroadcasterService _broadcaster;
        private readonly OverviewDockPanel _overviewDockPanel;
        public MainPlugin _Plugin;
        
        public OverviewPresenter(IAppContext context, IBroadcasterService broadcaster,
                               OverviewDockPanel overviewDockPanel,MainPlugin plugin)
            : base(overviewDockPanel)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (broadcaster == null) throw new ArgumentNullException("broadcaster");
            if (overviewDockPanel == null) throw new ArgumentNullException("legendDockPanel");

            _context = context;
            _broadcaster = broadcaster;
            _overviewDockPanel = overviewDockPanel;
            _Plugin = plugin;
            _Plugin.ProjectOpened += _Plugin_ProjectOpened;


            //View.LegendKeyDown += OnLegendKeyDown;
        }
        public OverviewDockPanel OverviewControl
        {
            get { return _overviewDockPanel; }
        }

        public void SetBuddyControl(IMapControl2 mainMap)
        {
            _overviewDockPanel.InitMainMap();
        }

        private void _Plugin_ProjectOpened(object sender, EventArgs e)
        {
            _overviewDockPanel.InitOverview();
        }

        public override void RunCommand(OverviewCommand command)
        {
            switch (command)
            {
                    case OverviewCommand.FullExtent:
                    _overviewDockPanel.FullExtent();
                    break;
                case OverviewCommand.Current:
                    _overviewDockPanel.Current();
                    break;
                    case OverviewCommand.Properties:
                    break;
            }
        }
    }
}
