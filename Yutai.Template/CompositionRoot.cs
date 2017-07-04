using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Template
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            //container.RegisterService<IMapTemplateView, MapTemplateView>()
            //    .RegisterSingleton<MapTemplatePresenter>();
            //    .RegisterService<IGeometryInfoView, GeometryInfoView>()
            //     .RegisterService<IAttributeEditView, AttributeEditView>()
            //    .RegisterSingleton<EditTemplatePresenter>()
            //     .RegisterSingleton<GeometryInfoPresenter>()
            //     .RegisterSingleton<AttributeEditPresenter>();
        }
    }
}