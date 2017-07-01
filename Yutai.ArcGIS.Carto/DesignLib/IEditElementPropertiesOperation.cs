using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public interface IEditElementPropertiesOperation : IOperation
    {
        IActiveView ActiveView { set; }

        IElement Element { set; }
    }
}