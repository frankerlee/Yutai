using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Editor.Views
{
    public class EditTemplatePresenter : CommandDispatcher<IEditTemplateView, EditTemplateCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
      
        #region Constructors

        public EditTemplatePresenter(IAppContext context,IEditTemplateView view)
            : base(view)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;

            View.Initialize(context);

            EditorEvent.OnStopEditing += EditorEvent_OnStopEditing;
            //View.Panels.BeforePanelClosed += (s, e) => View.BeforeClose();
           
        }

        private void EditorEvent_OnStopEditing()
        {
            _context.DockPanels.ShowDockPanel(((IDockPanelView)View).DockName,false,false);
        }


        #endregion
        #region Properties
        #endregion

        public Control GetInternalObject()
        {
            return View as Control;
        }

        public override void RunCommand(EditTemplateCommand command)
        {
            return;
        }

       

    }
}