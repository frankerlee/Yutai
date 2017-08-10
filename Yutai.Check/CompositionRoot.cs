using Yutai.Check.Views;
using Yutai.Plugins.Mvp;

namespace Yutai.Check
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            //container.RegisterService<IQueryResultView, QueryResultView>()
            //     .RegisterSingleton<QueryResultPresenter>();
            //    .RegisterService<IGeometryInfoView, GeometryInfoView>()
            //     .RegisterService<IAttributeEditView, AttributeEditView>()
            //    .RegisterSingleton<EditTemplatePresenter>()
            //     .RegisterSingleton<GeometryInfoPresenter>()
            //     .RegisterSingleton<AttributeEditPresenter>();
            container.RegisterService<ICheckResultView, CheckResultDockPanel>()
                .RegisterSingleton<CheckResultPresenter>();
        }
    }
}
