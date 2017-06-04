using System;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Editor.Menu
{
    public class DockPanelService
    {
        //public DockPanelService(IAppContext context, IdentifierPresenter presenter, IdentifierPlugin plugin)
        //{
        //    if (context == null) throw new ArgumentNullException("context");
        //    if (presenter == null) throw new ArgumentNullException("presenter");
        //    if (plugin == null) throw new ArgumentNullException("plugin");

        //    var panels = context.DockPanels;

        //    panels.Lock();
        //    var panel = panels.Add(presenter.GetInternalObject(), Yutai.UI.Docking.DockPanelKeys.Identifier, plugin.Identity);
        //    panel.Caption = "信息查看器";
        //    panel.TabPosition = 4;
        //    panel.SetIcon(Resources.ico_identify);

        //    //var mapLegend = panels.Preview;
        //    //if (preview != null && preview.Visible)
        //    //{
        //    //    panel.DockTo(preview, DockPanelState.Tabbed, 150);
        //    //}
        //    panel.DockTo(null, DockPanelState.Right, 150);

        //    panels.Unlock();
        //}
    }
}