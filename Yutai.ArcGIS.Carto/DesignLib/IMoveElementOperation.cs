using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public interface IMoveElementOperation : IOperation
    {
        IActiveView ActiveView { set; }

        IEnumElement Elements { set; }

        IPoint Point { set; }
    }
}

