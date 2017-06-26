using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Docking;
using Yutai.Pipeline.Analysis.Views;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Analysis.Services
{
    class DockPanelService
    {
        public DockPanelService(IAppContext context, QueryResultPresenter presenter, PipelineAnalysisPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");

            var panels = context.DockPanels;
            panels.Lock();
            DockPanel panel = panels.Add(presenter.GetInternalObject() as IDockPanelView, plugin.Identity);
            panel.Visible = false;
            panels.Unlock();
        }
    }
}
