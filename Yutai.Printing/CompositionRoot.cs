using Yutai.Plugins.Mvp;
using Yutai.Plugins.Printing.Views;

namespace Yutai.Plugins.Printing
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterService<IMapTemplateView, MapTemplateView>()
                .RegisterSingleton<MapTemplatePresenter>();
            //    .RegisterService<IGeometryInfoView, GeometryInfoView>()
            //     .RegisterService<IAttributeEditView, AttributeEditView>()
            //    .RegisterSingleton<EditTemplatePresenter>()
            //     .RegisterSingleton<GeometryInfoPresenter>()
            //     .RegisterSingleton<AttributeEditPresenter>();
        }
    }
}