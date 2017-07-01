using System.Runtime.InteropServices;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.ExtendClass
{
    [ComVisible(true)]
    [Guid("668C6FDD-41E9-4162-80A8-818968654054")]
    public interface ITriangleElement
    {
        double Angle { get; set; }

        ISimpleFillSymbol FillSymbol { get; set; }

        double Size { get; set; }
    }
}