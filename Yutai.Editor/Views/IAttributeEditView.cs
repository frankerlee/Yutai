using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Editor.Views
{
    public interface IAttributeEditView : IMenuProvider
    {
        void Initialize(IAppContext context);
    }
}