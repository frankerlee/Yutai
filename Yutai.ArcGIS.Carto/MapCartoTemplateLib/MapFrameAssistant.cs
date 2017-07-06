using System;
using System.Diagnostics;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    internal class MapFrameAssistant
    {
        public static double DDDMMSS2DEG(double angle)
        {
            bool flag = false;
            if (angle < 0.0)
            {
                flag = true;
            }
            Math.Abs(angle);
            double num = Math.Truncate(angle);
            double d = (angle - num) * 100.0;
            double num3 = Math.Truncate(d);
            double num4 = (d - num3) * 100.0;
            if (flag)
            {
                return -((num + (num3 / 60.0)) + (num4 / 3600.0));
            }
            return ((num + (num3 / 60.0)) + (num4 / 3600.0));
        }

        public static double DEG2DDDMMSS(double angle)
        {
            bool flag = false;
            if (angle < 0.0)
            {
                flag = true;
            }
            Math.Abs(angle);
            double num = Math.Truncate(angle);
            double num2 = Math.Truncate((double) ((angle * 60.0) - (num * 60.0)));
            double num3 = ((angle * 3600.0) - (num * 3600.0)) - (num2 * 60.0);
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

        public static double DEG2DDDMMSS(double angle, ref int dd, ref int mm, ref int ss)
        {
            bool flag = false;
            if (angle < 0.0)
            {
                flag = true;
            }
            Math.Abs(angle);
            double d = Math.Truncate(angle);
            double num2 = Math.Truncate((double) ((angle * 60.0) - (d * 60.0)));
            double num3 = ((angle * 3600.0) - (d * 3600.0)) - (num2 * 60.0);
            if (num3 > 59.5)
            {
                num3 = 0.0;
                num2++;
            }
            if (flag)
            {
                dd = Convert.ToInt32((double) (-1.0 * Math.Truncate(d)));
                mm = Convert.ToInt32((double) (-1.0 * Math.Truncate(num2)));
                ss = Convert.ToInt32((double) (-1.0 * Math.Truncate(num3)));
            }
            else
            {
                dd = Convert.ToInt32(Math.Truncate(d));
                mm = Convert.ToInt32(Math.Truncate(num2));
                ss = Convert.ToInt32(Math.Truncate(num3));
            }
            if (flag)
            {
                return -((d + (num2 / 100.0)) + (num3 / 10000.0));
            }
            return ((d + (num2 / 100.0)) + (num3 / 10000.0));
        }

        internal static IMapFrame GetFocusMapFrame(IPageLayout pPageLayout)
        {
            IGraphicsContainer container = pPageLayout as IGraphicsContainer;
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

        public static void TextWidth(IActiveView pActiveView, ITextElement pTextElement, out double width, out double height)
        {
            width = 0.0;
            height = 0.0;
            try
            {
                IEnvelope bounds = new EnvelopeClass();
                (pTextElement as IElement).QueryBounds(pActiveView.ScreenDisplay, bounds);
                width = bounds.Width;
                height = bounds.Height;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
    }
}

