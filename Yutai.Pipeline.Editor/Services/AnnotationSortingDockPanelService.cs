// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  AnnotationSortingDockPanelService.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/07/26  10:48
// 更新时间 :  2017/07/26  10:48

using System;
using DevExpress.XtraBars.Docking;
using Yutai.Pipeline.Editor.Views;
using Yutai.Plugins.Interfaces;

namespace Yutai.Pipeline.Editor.Services
{
    public class AnnotationSortingDockPanelService
    {
        private IAppContext _context;
        private AnnotationSortingPresenter _presenter;
        private PipelineEditorPlugin _plugin;

        public AnnotationSortingDockPanelService(IAppContext context, AnnotationSortingPresenter presenter,
            PipelineEditorPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");
            if (plugin == null) throw new ArgumentNullException("plugin");

            _context = context;
            _presenter = presenter;
            _plugin = plugin;
        }

        public DockPanel AddPanel()
        {
            return _context.DockPanels.Add(_presenter.GetInternalObject() as IDockPanelView, _plugin.Identity);
        }

        public void Show()
        {
            DockPanel panel =
                _context.DockPanels.GetDockPanel(((IDockPanelView) _presenter.GetInternalObject()).DockName);
            if (panel == null)
                panel = AddPanel();
            _context.DockPanels.ShowDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName, true, true);
        }

        public bool Visible
            => _context.DockPanels.GetDockVisible(((IDockPanelView) _presenter.GetInternalObject()).DockName);

        public void Hide()
        {
            DockPanel panel =
                _context.DockPanels.GetDockPanel(((IDockPanelView) _presenter.GetInternalObject()).DockName);
            if (panel == null)
                return;
            _context.DockPanels.ShowDockPanel(((IDockPanelView)_presenter.GetInternalObject()).DockName, false, false);
        }
    }
}