using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Pipeline.Editor.Emuns;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Pipeline.Editor.Views
{
    public class IdentifyRoadNamePresenter : CommandDispatcher<IIdentifyRoadNameView, IdentifyRoadNameCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        public IdentifyRoadNamePresenter(IAppContext context, IIdentifyRoadNameView view) : base(view)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;

            View.Initialize(context);

            EditorEvent.OnStopEditing += EditorEventOnOnStopEditing;
        }

        private void EditorEventOnOnStopEditing()
        {
            _context.DockPanels.ShowDockPanel(((IDockPanelView)View).DockName, false, false);
        }

        public override void RunCommand(IdentifyRoadNameCommand command)
        {
            return;
        }

        public Control GetInternalObject()
        {
            return View as Control;
        }
    }
}
