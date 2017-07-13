using Yutai.Plugins.Mvp;
using Yutai.Plugins.Scene.Views;

namespace Yutai.Plugins.Scene
{
    internal static class CompositionRoot
    {
        public static void Compose(IApplicationContainer container)
        {
            container.RegisterService<ISceneView, SceneView>()
                .RegisterSingleton<SceneViewPresenter>();
            //    .RegisterService<IAutoLayoutView, AutoLayoutView>()
            //    .RegisterSingleton<MapTemplatePresenter>()
            //    .RegisterSingleton<AutoLayoutPresenter>();
            //    .RegisterService<IGeometryInfoView, GeometryInfoView>()
            //     .RegisterService<IAttributeEditView, AttributeEditView>()
            //    .RegisterSingleton<EditTemplatePresenter>()
            //     .RegisterSingleton<GeometryInfoPresenter>()
            //     .RegisterSingleton<AttributeEditPresenter>();
        }
    }
}