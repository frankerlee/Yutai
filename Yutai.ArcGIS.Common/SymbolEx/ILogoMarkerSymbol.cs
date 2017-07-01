using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.SymbolEx
{
    public interface ILogoMarkerSymbol
    {
        IColor ColorBorder { get; set; }

        IColor ColorLeft { get; set; }

        IColor ColorRight { get; set; }

        IColor ColorTop { get; set; }
    }
}