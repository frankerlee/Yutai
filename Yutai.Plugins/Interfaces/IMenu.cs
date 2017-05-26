using Yutai.Plugins.Concrete;

namespace Yutai.Plugins.Interfaces
{
    public interface IRibbonItemCollection
    {
        IRibbonItem AddRibbonItem(IRibbonItem item, PluginIdentity identity);
        IRibbonItem this[string key] { get; }
        void Insert(IRibbonItem item, string key);
        void Remove(IRibbonItem item);
        void Remove(string key);
        void Clear();
        int Count { get; }
    }

    public interface IRibbonMenu
    {
        IRibbonItem FindItem(string key, PluginIdentity identity);
        void RemoveItemsForPlugin(PluginIdentity identity);
        IRibbonMenuIndex SubItems { get; }
        IRibbonItem AddCommand(YutaiCommand command);
        IRibbonItem AddCommand(YutaiMenuCommand command);
        void Insert(IRibbonItem item, int index);
        void Remove(int index);
        void Remove(IRibbonItem item);
        void Clear();
        int IndexOf(IRibbonItem item);
        int Count { get; }
        IRibbonItem InsertBefore { get; set; }
    }

    public interface IMenu : IMenuBase
    {
        IDropDownMenuItem FileMenu { get; }

        IDropDownMenuItem LayerMenu { get; }

        IDropDownMenuItem ViewMenu { get; }

        IDropDownMenuItem PluginsMenu { get; }

        IDropDownMenuItem TilesMenu { get; }

        IDropDownMenuItem ToolsMenu { get; }

        IDropDownMenuItem HelpMenu { get; }

        IDropDownMenuItem MapMenu { get; }
    }
}