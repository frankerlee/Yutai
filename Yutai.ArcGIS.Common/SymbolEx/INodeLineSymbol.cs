using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.SymbolEx
{
    public interface INodeLineSymbol
    {
        int DrawStyle { get; set; }

        IMarkerSymbol NodeSymbol { get; set; }
    }
}