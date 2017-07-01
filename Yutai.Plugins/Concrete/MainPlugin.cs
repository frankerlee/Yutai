using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Concrete
{
    public class MainPlugin : BasePlugin
    {
        public MainPlugin()
        {
            Identity = PluginIdentity.Default;
        }

        public override void Initialize(IAppContext context)
        {
        }

        public override void Terminate()
        {
        }

        protected override void RegisterServices(IApplicationContainer container)
        {
        }
    }
}