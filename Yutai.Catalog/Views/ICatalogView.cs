using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Catalog.Views
{
    public interface ICatalogView : IMenuProvider
    {
        void Initialize(IAppContext context);
    }
}