using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yutai.Plugins.Mef
{
    public interface IPluginMetadata
    {
        string Name { get; }
        string Author { get; }
        string Guid { get; }
        bool Empty { get; }
    }
}