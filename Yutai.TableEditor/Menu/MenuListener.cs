// 项目名称 :  Yutai
// 项目描述 :  
// 类 名 称 :  MenuListener.cs
// 版 本 号 :  
// 说    明 :  
// 作    者 :  
// 创建时间 :  2017/06/06  17:11
// 更新时间 :  2017/06/06  17:11

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using Yutai.Controls;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.TableEditor.Views;
using Yutai.UI.Docking;
using Yutai.UI.Menu;

namespace Yutai.Plugins.TableEditor.Menu
{
    public class MenuListener : MenuServiceBase
    {
        private readonly TableEditorPresenter _presenter;
        public MenuListener(IAppContext context, TableEditorPlugin plugin, TableEditorPresenter presenter) : base(context, plugin.Identity)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (presenter == null) throw new ArgumentNullException("presenter");

            _presenter = presenter;
            plugin.ItemClicked += PluginItemClicked;
            plugin.ViewUpdating += ViewUpdating;
            plugin.MessageBroadcasted += OnPluginMessageBroadcasted;
        }

        private void OnPluginMessageBroadcasted(object sender, PluginMessageEventArgs e)
        {
            if (e.Message == PluginMessages.ShowAttributeTable)
            {
                ShowTableEditor(sender as IMapLegendView);
            }
        }

        private void PluginItemClicked(object sender, MenuItemEventArgs e)
        {
        }

        private void ViewUpdating(object sender, EventArgs e)
        {
        }

        private void ShowTableEditor(IMapLegendView view)
        {
            var layer = view.SelectedLayer as IFeatureLayer;
            _presenter.OpenTable(layer);
            _context.DockPanels.ShowDockPanel(TableEditorDockPanel.DefaultDockName, true, true);
        }

    }
}