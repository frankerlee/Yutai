using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yutai.Plugins.Events;

namespace Yutai.Services.Serialization
{
    public interface IProjectLoaderBase
    {
        event System.EventHandler<ProgressEventArgs> ProgressChanged;
    }

    public interface IProjectLoader : IProjectLoaderBase
    {
        bool Restore(XmlProject project);
    }
}