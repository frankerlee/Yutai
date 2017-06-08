using System;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    internal class MapFrameAssistant
    {
        public static double DDDMMSS2DEG(double double_0)
        {
            bool flag = false;
            if (double_0 < 0.0)
            {
                flag = true;
            }
            Math.Abs(double_0);
            double num = Math.Truncate(double_0);
            double d = (double_0 - num) * 100.0;
            double num3 = Math.Truncate(d);
            double num4 = (d - num3) * 100.0;
            if (flag)
            {
                return -((num + (num3 / 60.0)) + (num4 / 3600.0));
            }
            return ((num + (num3 / 60.0)) + (num4 / 3600.0));
        }

        public static double DEG2DDDMMSS(double double_0)
        {
            bool flag = false;
            if (double_0 < 0.0)
            {
                flag = true;
            }
            Math.Abs(double_0);
            double num = Math.Truncate(double_0);
            double num2 = Math.Truncate((double) ((double_0 * 60.0) - (num * 60.0)));
            double num3 = ((double_0 * 3600.0) - (num * 3600.0)) - (num2 * 60.0);
            if (num3 > 59.99999)
            {
                num3 = 0.0;
                num2++;
            }
            if (flag)
            {
                return -((num + (num2 / 100.0)) + (num3 / 10000.0));
            }
            return ((num + (num2 / 100.0)) + (num3 / 10000.0));
        }

        public static double DEG2DDDMMSS(double double_0, ref int int_0, ref int int_1, ref int int_2)
        {
            bool flag = false;
            if (double_0 < 0.0)
            {
                flag = true;
            }
            Math.Abs(double_0);
            double d = Math.Truncate(double_0);
            double num2 = Math.Truncate((double) ((double_0 * 60.0) - (d * 60.0)));
            double num3 = ((double_0 * 3600.0) - (d * 3600.0)) - (num2 * 60.0);
            if (num3 > 59.5)
            {
                num3 = 0.0;
                num2++;
            }
            if (flag)
            {
                int_0 = Convert.ToInt32((double) (-1.0 * Math.Truncate(d)));
                int_1 = Convert.ToInt32((double) (-1.0 * Math.Truncate(num2)));
                int_2 = Convert.ToInt32((double) (-1.0 * Math.Truncate(num3)));
            }
            else
            {
                int_0 = Convert.ToInt32(Math.Truncate(d));
                int_1 = Convert.ToInt32(Math.Truncate(num2));
                int_2 = Convert.ToInt32(Math.Truncate(num3));
            }
            if (flag)
            {
                return -((d + (num2 / 100.0)) + (num3 / 10000.0));
            }
            return ((d + (num2 / 100.0)) + (num3 / 10000.0));
        }

        internal static IMapFrame GetFocusMapFrame(IPageLayout ipageLayout_0)
        {
            IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
            container.Reset();
            for (IElement element = container.Next(); element != null; element = container.Next())
            {
                if (element is IMapFrame)
                {
                    return (element as IMapFrame);
                }
            }
            return null;
        }

        public static void TextWidth(IActiveView iactiveView_0, ITextElement itextElement_0, out double double_0, out double double_1)
        {
            double_0 = 0.0;
            double_1 = 0.0;
            try
            {
                IEnvelope bounds = new EnvelopeClass();
                (itextElement_0 as IElement).QueryBounds(iactiveView_0.ScreenDisplay, bounds);
                double_0 = bounds.Width;
                double_1 = bounds.Height;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
    }
}

