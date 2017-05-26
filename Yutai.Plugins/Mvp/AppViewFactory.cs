using Yutai.Plugins.Interfaces;

namespace Yutai.Plugins.Mvp
{
    public static class AppViewFactory
    {
        public static IAppView Instance { get; internal set; }
    }
}