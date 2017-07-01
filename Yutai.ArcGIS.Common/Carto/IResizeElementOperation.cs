using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
    public interface IResizeElementOperation : IOperation
    {
        IActiveView ActiveView { set; }

        IElement Element { set; }

        IGeometry Geometry { set; }
    }
}