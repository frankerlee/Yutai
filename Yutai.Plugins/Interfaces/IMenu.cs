using Syncfusion.Windows.Forms.Tools;

namespace Yutai.Plugins.Interfaces
{
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