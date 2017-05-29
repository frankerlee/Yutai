using Yutai.Plugins.Locator.Views;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Locator
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterService<ILocatorView,LocatorDockPanel>()
                .RegisterSingleton<LocatorPresenter>();
        }
    }
}
