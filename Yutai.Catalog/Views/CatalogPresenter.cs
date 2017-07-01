using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Catalog.Views
{
    public enum CatalogViewCommand
    {
        Close
    }

    public class CatalogPresenter : CommandDispatcher<ICatalogView, CatalogViewCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;

        #region Constructors

        public CatalogPresenter(IAppContext context, ICatalogView view)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;

            View.Initialize(context);

            // EditorEvent.OnStopEditing += EditorEvent_OnStopEditing;
            //View.Panels.BeforePanelClosed += (s, e) => View.BeforeClose();
        }

        private void EditorEvent_OnStopEditing()
        {
            _context.DockPanels.ShowDockPanel(((IDockPanelView) View).DockName, false, false);
        }

        #endregion

        #region Properties

        #endregion

        public Control GetInternalObject()
        {
            return View as Control;
        }

        public override void RunCommand(CatalogViewCommand command)
        {
            return;
        }
    }
}