using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
    public interface INewElementOperation : IOperation
    {
        IActiveView ActiveView { set; }

        IActiveView ContainHook { set; }

        IElement Element { set; }
    }
}