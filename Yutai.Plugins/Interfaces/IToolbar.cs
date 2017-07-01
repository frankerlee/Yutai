using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Concrete;
using Yutai.Plugins.Enums;

namespace Yutai.Plugins.Interfaces
{
    //在Ribbon中，IToolbar可以理解成ToolStripTabItem
    public interface IToolbar
    {
        string Name { get; set; }
        IMenuItemCollection Items { get; }
        bool Visible { get; set; }
        object Tag { get; set; }
        string Key { get; }
        ToolbarDockState DockState { get; set; }
        PluginIdentity PluginIdentity { get; }
        bool Enabled { get; set; }
        void Update();
        void Refresh();
    }
}