using System.Drawing;

namespace Yutai.Plugins.Interfaces
{
    public interface IMenuIcon
    {
        Image Image { get; }
        bool UseNativeSize { get; }
    }
}