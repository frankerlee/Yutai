using ESRI.ArcGIS.Carto;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Editor.Views
{
    public interface IEditTemplateView : IMenuProvider
    {
        IFeatureLayer CurrentEditLayer { get; }
        IConstructTool CurrentConstructTool { get; }

        void OnTemplateSelectedChanged();

        void OnConstructToolToolChanged();
        void Initialize(IAppContext context);

        void ValidateWorkspace();
    }
}