using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Yutai.Plugins.Enums;
using Yutai.Plugins.Events;
using Yutai.Plugins.Interfaces;
using Yutai.Plugins.Mvp;

namespace Yutai.Plugins.Scene.Views
{
    public interface ISceneView:IMenuProvider
    {
        ISceneControl SceneControl { get; }
        IScene Scene { get; }
        ITOCControl TOCBuddy { get; set; }
        void Initialize(IAppContext context, ScenePlugin plugin);

        bool IsLinkMap { get; set; }
        void OpenSXD(string docName);
    }
}
