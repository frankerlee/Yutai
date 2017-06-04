using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Helpers
{
    public static class FlashUtility
    {
        public static void FlashGeometry(IGeometry pGeo, IMapControl2 pMapConrol)
        {
            if (pGeo == null) return;
            ////该语句保证在Flash前重画窗口
            pMapConrol.ActiveView.ScreenDisplay.UpdateWindow();
            switch (pGeo.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    FlashPoint(pGeo as IPoint, pMapConrol);
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    FlashPolyline(pGeo as IPolyline, pMapConrol);
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    FlashPolygon(pGeo as IPolygon, pMapConrol);
                    break;
                default:
                    return;
            }
        }

        public static void FlashPoint(IPoint pPt, IMapControl2 pMapConrol)
        {
            if (pPt == null) return;
            ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbol();
            pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            pMarkerSymbol.Color = SetFlashColor();
            ISymbol pSymbol = pMarkerSymbol as ISymbol;
            pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            pMapConrol.FlashShape(pPt, 3, 200, pSymbol);
        }

        public static void FlashPolyline(IPolyline pPolyline, IMapControl2 pMapControl)
        {
            if (pPolyline == null) return;
            ISimpleLineSymbol pLinesymbol = new SimpleLineSymbol();
            pLinesymbol.Width = 4;
            pLinesymbol.Color = SetFlashColor();
            ISymbol pSymbol = pLinesymbol as ISymbol;
            pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            pMapControl.FlashShape(pPolyline, 3, 200, pSymbol);
        }

        public static void FlashPolygon(IPolygon pPolygon, IMapControl2 pMapControl)
        {
            if (pPolygon == null) return;
            ISimpleFillSymbol pFillSymbol = new SimpleFillSymbol();
            pFillSymbol.Outline.Width = 12;
            pFillSymbol.Color = SetFlashColor();
            ISymbol pSymbol = pFillSymbol as ISymbol;
            pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            pMapControl.FlashShape(pPolygon, 3, 200, pSymbol);
        }

        public static void FlashFeature(IFeature pFeature, IMapControl2 pMapControl)
        {
            if (pFeature == null) return;
            IGeometry pGeo = pFeature.ShapeCopy;
            FlashGeometry(pGeo, pMapControl);
        }

        private static IRgbColor SetFlashColor()
        {
            IRgbColor pRgbClr = new RgbColor();
            pRgbClr.RGB = System.Drawing.Color.FromArgb(255, 0, 0).ToArgb();
            return pRgbClr;
        }
    }
}
