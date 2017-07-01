using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace Yutai.ArcGIS.Common.Carto
{
    public interface IAlignElementsOperation : IOperation
    {
        IActiveView ActiveView { set; }

        IEnvelope AlignEnvelope { set; }

        enumAlignType AlignType { set; }

        IEnumElement Elements { set; }
    }
}