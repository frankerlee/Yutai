using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Interfaces
{
    public interface IToolbarCollection : IToolbarCollectionBase
    {
        IToolbar MapToolbar { get; }

        IToolbar FileToolbar { get; }
    }
}