using ESRI.ArcGIS.Controls;

namespace Yutai.ArcGIS.Common.Framework
{
    public interface IMapControlForm
    {
        IMapControl2 MapControl { get; }

        bool IsRegisterAsDocument { set; }
    }
}