using Yutai.Plugins.Identifer.Enums;
using Yutai.Plugins.Identifer.Views;
using Yutai.Plugins.Mvp;
using Yutai.Shared;

namespace Yutai.Plugins.Identifer
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            EnumHelper.RegisterConverter(new IdentifierModeConverter());
            container.RegisterService<IIdentifierView, IdentifierDockPanel>()
                .RegisterSingleton<IdentifierPresenter>();
        }
    }
}