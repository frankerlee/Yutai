// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  ProjectListener.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  17:02
// 更新时间 :  2017/06/06  17:02

using System;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;
using Yutai.UI.Docking;

namespace Yutai.Plugins.TableEditor
{
    public class ProjectListener
    {
        private readonly IAppContext _context;

        public ProjectListener(IAppContext context, TableEditorPlugin plugin)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (plugin == null) throw new ArgumentNullException("plugin");
            _context = context;
            plugin.ProjectClosed += OnProjectClosed;
        }


        private void OnProjectClosed(object sender, EventArgs e)
        {
            var panel = _context.DockPanels.GetDockPanel(TableEditorDockPanel.DefaultDockName);
            if (panel.Visible)
            {
                panel.Visible = false;
            }
        }
    }
}