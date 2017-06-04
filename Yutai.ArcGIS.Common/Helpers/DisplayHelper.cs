using System;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Helpers
{
    public class DataRangeHelper
    {
        public DataRangeHelper()
        {
        }

        public static double GetMax(double pMinValue, double pUnits)
        {
            return pMinValue + (double)((long)2147483647) / pUnits;
        }

        public static double GetUnits(double pMinValue, double pMaxValue)
        {
            long num = (long)2147483647;
            double num1 = Math.Abs(pMaxValue - pMinValue);
            return (double)num / num1;
        }
    }
    public class DisplayHelper
    {
        public DisplayHelper()
        {
        }

        public static ITransformation CreateTransformationFromHDC(IntPtr HDC, int width, int height)
        {
            ITransformation transformation;
            IEnvelope envelopeClass = new Envelope() as IEnvelope;
            envelopeClass.PutCoords(0, 0, (double)width, (double)height);
            tagRECT _tagRECT = new tagRECT()
            {
                left = 0,
                top = 0,
                right = width,
                bottom = height
            };
            double dpiY = (double)Graphics.FromHdc(HDC).DpiY;
            if ((long)dpiY != (long)0)
            {
                IDisplayTransformation displayTransformationClass = new DisplayTransformation() as IDisplayTransformation;
                displayTransformationClass.Bounds = envelopeClass;
                displayTransformationClass.VisibleBounds = envelopeClass;
                displayTransformationClass.set_DeviceFrame(ref _tagRECT);
                displayTransformationClass.Resolution = dpiY;
                
                transformation = displayTransformationClass;
            }
            else
            {
                MessageBoxHelper.ShowMessageBox("获取设备比例尺失败");
                transformation = null;
            }
            return transformation;
        }

        public static void DrawGeometry(IScreenDisplay paramScreenDisplay, IGeometry paramGeom, ISymbol paramSymbol)
        {
            if ((paramScreenDisplay == null || paramGeom == null || paramGeom.IsEmpty ? false : paramSymbol != null))
            {
                paramScreenDisplay.StartDrawing(paramScreenDisplay.hDC, -2);
                paramScreenDisplay.SetSymbol(paramSymbol);
                if (paramGeom is IPoint)
                {
                    paramScreenDisplay.DrawPoint(paramGeom);
                }
                else if (paramGeom is IPolyline)
                {
                    paramScreenDisplay.DrawPolyline(paramGeom);
                }
                else if (paramGeom is IPolygon)
                {
                    paramScreenDisplay.DrawPolygon(paramGeom);
                }
                paramScreenDisplay.FinishDrawing();
            }
        }

        public static void DrawText(IScreenDisplay paramScreenDisplay, IGeometry paramGeom, string paramText, ISymbol paramSymbol)
        {
            if ((paramScreenDisplay == null || paramGeom == null || paramGeom.IsEmpty || paramSymbol == null ? false : paramSymbol is ITextSymbol))
            {
                paramScreenDisplay.StartDrawing(paramScreenDisplay.hDC, -2);
                paramScreenDisplay.UpdateWindow();
                paramScreenDisplay.SetSymbol(paramSymbol);
                if (paramText == null)
                {
                    paramText = "";
                }
                paramScreenDisplay.DrawText(paramGeom, paramText);
                paramScreenDisplay.FinishDrawing();
            }
        }

        public static double FromPixesToRealWorld(IActiveView pView, int pixes)
        {
            double num;
            if (pView != null)
            {
                int exportFrame = pView.ExportFrame.right - pView.ExportFrame.left;
                double width = pView.Extent.Width;
                num = (double)pixes * width / (double)exportFrame;
            }
            else
            {
                num = (double)pixes;
            }
            return num;
        }
    }
}
