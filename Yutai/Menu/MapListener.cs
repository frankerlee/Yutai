using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Services;

namespace Yutai.Menu
{
    public class MapListener
    {
        private readonly IBroadcasterService _broadcaster;
        //private readonly ContextMenuPresenter _contextMenuPresenter;
        private readonly IProjectService _projectService;
        private readonly IMapControlEvents2_Event _mapEvents;
        private readonly IActiveViewEvents_Event _activeViewEvents;
        private readonly IAppContext _context;

        public MapListener(IAppContext context, IBroadcasterService broadcaster, IProjectService projectService)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (broadcaster == null) throw new ArgumentNullException("broadcaster");
            if (projectService == null) throw new ArgumentNullException("projectService");

            _context = context;
            _broadcaster = broadcaster;
            _projectService = projectService;


            _mapEvents = _context.MapControl as IMapControlEvents2_Event;
            if (_mapEvents == null)
            {
                throw new ApplicationException("Invalid map state.");
            }

            RegisterEvents();
        }

        //这儿的事件监控主要用来发布一些对全局可能产生影响的事件，如选择集的变化需要重新刷新按钮等。
        private void RegisterEvents()
        {
            _mapEvents.OnExtentUpdated += MapEventsOnOnExtentUpdated;
            _mapEvents.OnSelectionChanged += MapEventsOnOnSelectionChanged;
        }


        private void MapEventsOnOnSelectionChanged()
        {
            _broadcaster.BroadcastEvent(p => p.ViewUpdating_, _context.View, null);
            _context.View.Update();
        }

        private void MapEventsOnOnExtentUpdated(object displayTransformation, bool sizeChanged, object newEnvelope)
        {
            _broadcaster.BroadcastEvent(p => p.ViewUpdating_, _context.View, null);
            _context.View.Update();
        }
    }
}