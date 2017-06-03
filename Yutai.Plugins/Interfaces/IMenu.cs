using Syncfusion.Windows.Forms.Tools;
using Yutai.Plugins.Concrete;

namespace Yutai.Plugins.Interfaces
{


    public interface IRibbonMenu
    {
        IRibbonMenuItem FindItem(string key);
        //IRibbonItem FindItem(string key, PluginIdentity identity);
        void RemoveItemsForPlugin(PluginIdentity identity);
        IRibbonMenuIndex SubItems { get; }
        void AddCommand(YutaiCommand command);
      
        void Remove(IRibbonItem item);
        void Clear();
      
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