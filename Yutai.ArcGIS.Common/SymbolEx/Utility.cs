using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolEx
{
    internal static class Utility
    {
        public const uint E_NOTIMPL = 2147500033;

        public const uint E_FAIL = 2147500037;

        public const double m_dPi = 3.14159265358979;

        public const int LOGPIXELSX = 88;

        public const int LOGPIXELSY = 90;

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int Chord(int hDC, int X1, int Y1, int X2, int Y2, int X3, int Y3, int X4, int Y4);

        public static ICircularArc CreateCircArc(IPoint pCenter, IPoint pFrom, ref IPoint pTo)
        {
            ICircularArc circularArcClass = null;
            circularArcClass = new CircularArc();
            circularArcClass.PutCoords(pCenter, pFrom, pTo, esriArcOrientation.esriArcClockwise);
            return circularArcClass;
        }

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int CreatePen(int nPenStyle, int nWidth, int crColor);

        public static IPoint CreatePoint(double dX, double dY)
        {
            IPoint pointClass = null;
            pointClass = new Point();
            pointClass.PutCoords(dX, dY);
            return pointClass;
        }

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int CreateSolidBrush(int crColor);

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteObject(int hObject);

        public static void FromMapPoint(ITransformation displayTransform, ref IPoint pMapPoint, ref int dXIn,
            ref int dYIn)
        {
            double num = 0;
            double num1 = 0;
            if (displayTransform != null)
            {
                ((IDisplayTransformation) displayTransform).FromMapPoint(pMapPoint, out dXIn, out dYIn);
            }
            else
            {
                pMapPoint.QueryCoords(out num, out num1);
                dXIn = Convert.ToInt32(num);
                dYIn = Convert.ToInt32(num1);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetDeviceCaps(int hDC, int nIndex);

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetROP2(int hDC);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetWindowRect(int hWnd, ref Utility.RECT lpRect);

        public static bool IsANumber(string strString)
        {
            bool flag = false;
            flag = true;
            if (!Utility.IsNumber(strString))
            {
                flag = false;
            }
            return flag;
        }

        public static bool IsNumber(string str)
        {
            bool flag;
            int num = 0;
            while (true)
            {
                if (num >= str.Length)
                {
                    flag = true;
                    break;
                }
                else if ((char.IsDigit(str[num]) ? true : str[num] == '.'))
                {
                    num++;
                }
                else
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int Polygon(int hDC, ref Utility.POINTAPI lpPoint, int nCount);

        public static double Radians(double dDegrees)
        {
            return dDegrees*0.0174532925199433;
        }

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hdc);

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int SelectObject(int hDC, int hObject);

        [DllImport("gdi32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int SetROP2(int hDC, int nDrawMode);

        public static int TwipsPerPixelX()
        {
            return 16;
        }

        public static int TwipsPerPixelY()
        {
            return 16;
        }

        public struct POINTAPI
        {
            public int x;

            public int y;
        }

        public struct RECT
        {
            public int left;

            public int top;

            public int right;

            public int bottom;
        }
    }
}