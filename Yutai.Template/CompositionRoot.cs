using Yutai.Plugins.Mvp;
using Yutai.Plugins.Template.Views;

namespace Yutai.Plugins.Template
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterService<ITemplateView, TemplateView>()
                .RegisterSingleton<TemplateViewPresenter>();
            //    .RegisterService<IGeometryInfoView, GeometryInfoView>()
            //     .RegisterService<IAttributeEditView, AttributeEditView>()
            //    .RegisterSingleton<EditTemplatePresenter>()
            //     .RegisterSingleton<GeometryInfoPresenter>()
            //     .RegisterSingleton<AttributeEditPresenter>();
        }
    }
}