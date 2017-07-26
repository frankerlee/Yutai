// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  AnnotationSortingPresenter.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/26  10:36
// 更新时间 :  2017/07/26  10:36

using System;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.Editor;
using Yutai.Pipeline.Editor.Emuns;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Pipeline.Editor.Views
{
    public class AnnotationSortingPresenter:CommandDispatcher<IAnnotationSortingView, AnnotationSortingCommand>, IDockPanelPresenter
    {
        private readonly IAppContext _context;
        public AnnotationSortingPresenter(IAppContext context, IAnnotationSortingView view) : base(view)
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

        public override void RunCommand(AnnotationSortingCommand command)
        {
            return;
        }

        public Control GetInternalObject()
        {
            return View as Control;
        }
    }
}