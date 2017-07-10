using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Editor.Views
{
    public class TopologyErrorPresenter : CommandDispatcher<ITopologyErrorView, TopologyErrorViewCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;

        #region Constructors

        public TopologyErrorPresenter(IAppContext context, ITopologyErrorView view)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;

            View.Initialize(context);

            EditorEvent.OnStopEditing += EditorEvent_OnStopEditing;
            //View.Panels.BeforePanelClosed += (s, e) => View.BeforeClose();
        }

        public IWorkspace EditWorkspace
        {
            set { ((IGeometryInfoView)View).EditWorkspace = value; }
        }

        public IMap FocusMap
        {
            set { View.FocusMap = value; }
        }

        private void EditorEvent_OnStopEditing()
        {
            _context.DockPanels.ShowDockPanel(((IDockPanelView)View).DockName, false, false);
        }

        #endregion

        #region Properties

        #endregion

        public Control GetInternalObject()
        {
            return View as Control;
        }

        public override void RunCommand(TopologyErrorViewCommand command)
        {
            return;
        }
    }
}