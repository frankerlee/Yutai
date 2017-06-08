using Syncfusion.Windows.Forms.Grid.Grouping;
using Yutai.Controls;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;
using Yutai.UI.Forms;
using Yutai.UI.Helpers;
using Yutai.Views;
using Yutai.Views.Abstract;

namespace Yutai
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterSingleton<IMainView, NewMainView>()
                .RegisterSingleton<IAppContext, AppContext>()
                .RegisterSingleton<IAppView, AppView>()
                .RegisterView<IConfigView, ConfigView>()
                .RegisterInstance<IApplicationContainer>(container)
                 .RegisterService<MapLegendPresenter>()
                .RegisterService<MapLegendDockPanel>()
                 .RegisterService<OverviewPresenter>()
                .RegisterService<OverviewDockPanel>(); 

            Services.CompositionRoot.Compose(container);
            Plugins.CompositionRoot.Compose(container);
            UI.CompositionRoot.Compose(container);
        

            CommandBarHelper.InitMenuColors();

           // GridEngineFactory.Factory = new AllowResizingIndividualRows();
        }
    }
}