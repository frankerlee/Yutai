using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Pipeline.Analysis.Views
{
    public interface IQueryResultView : IMenuProvider
    {
        void Initialize(IAppContext context);
        void SetResult(IFeatureCursor cursor, IFeatureSelection selection);
    }
}