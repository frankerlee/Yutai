using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Template.Views
{
    public interface ITemplateView : IMenuProvider
    {
        void Initialize(IAppContext context, TemplatePlugin plugin);
      
    }
}