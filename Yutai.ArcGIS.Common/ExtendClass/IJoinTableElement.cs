using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    internal interface IJoinTableElement
    {
        ILineSymbol LineSymbol { get; set; }

        string Row1Col1Text { get; set; }

        string Row1Col2Text { get; set; }

        string Row1Col3Text { get; set; }

        string Row2Col1Text { get; set; }

        string Row2Col3Text { get; set; }

        string Row3Col1Text { get; set; }

        string Row3Col2Text { get; set; }

        string Row3Col3Text { get; set; }

        ITextSymbol TextSymbol { get; set; }

        IElement CreateJionTab(IActiveView pAV, IPoint Leftdown);
    }
}