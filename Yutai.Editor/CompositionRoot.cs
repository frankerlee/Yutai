using Yutai.Plugins.Editor.Views;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Editor
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterService<IEditTemplateView, EditTemplateView>()
                .RegisterService<IGeometryInfoView, GeometryInfoView>()
                .RegisterService<IAttributeEditView, AttributeEditView>()
                .RegisterService<ITopologyErrorView, TopologyErrorView>()
                .RegisterSingleton<EditTemplatePresenter>()
                .RegisterSingleton<GeometryInfoPresenter>()
                .RegisterSingleton<AttributeEditPresenter>()
                .RegisterSingleton<TopologyErrorPresenter>();
        }
    }
}