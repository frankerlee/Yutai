using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.SymbolEx
{
    [ComVisible(true)]
    [Guid("5D9B0995-8F1D-429f-8305-A7C332682403")]
    [ProgId("JLK.SymbolEx.LabelSymbol")]
    public class LabelSymbol : ISymbol, ILineSymbol, IDisplayName, IClone, IPersistVariant, ISymbolRotation, IMapLevel,
        IPropertySupport, ILabelSymbol
    {
        private const string m_sDisplayName = "LabelSymbol";

        private const int m_lCurrPersistVers = 1;

        public const string GUID = "JLK.SymbolEx.LabelSymbol";

        private IDisplayTransformation m_trans;

        private esriRasterOpCode m_lROP2Old;

        private int m_lhDC;

        private int m_lOldPen;

        private int m_lOldBrush;

        private int m_lPen;

        private int m_lBrushTop;

        private int m_lBrushLeft;

        private int m_lBrushRight;

        private Utility.POINTAPI[] m_coords = new Utility.POINTAPI[7];

        private double m_dDeviceRadius;

        private esriRasterOpCode m_lROP2;

        private double m_dSize;

        private double m_dXOffset;

        private double m_dYOffset;

        private double m_dAngle;

        private double m_dDeviceRatio;

        private double m_dDeviceXOffset;

        private double m_dDeviceYOffset;

        private bool m_bRotWithTrans;

        private double m_dMapRotation;

        private int m_lMapLevel;

        private double m_baselength = 300;

        private IColor m_pColor = new RgbColor();

        private double m_widht = 0;

        string ESRI.ArcGIS.Display.IDisplayName.NameString
        {
            get { return "LabelSymbol"; }
        }

        IColor ESRI.ArcGIS.Display.ILineSymbol.Color
        {
            get { return this.m_pColor; }
            set { this.m_pColor = value; }
        }

        double ESRI.ArcGIS.Display.ILineSymbol.Width
        {
            get { return this.m_widht; }
            set { this.m_widht = value; }
        }

        int ESRI.ArcGIS.Display.IMapLevel.MapLevel
        {
            get { return this.m_lMapLevel; }
            set { this.m_lMapLevel = value; }
        }

        esriRasterOpCode ESRI.ArcGIS.Display.ISymbol.ROP2
        {
            get { return this.m_lROP2; }
            set
            {
                if (Convert.ToInt32(value) >= 1)
                {
                    this.m_lROP2 = value;
                }
                else
                {
                    this.m_lROP2 = esriRasterOpCode.esriROPCopyPen;
                }
            }
        }

        bool ESRI.ArcGIS.Display.ISymbolRotation.RotateWithTransform
        {
            get { return this.m_bRotWithTrans; }
            set { this.m_bRotWithTrans = value; }
        }

        UID ESRI.ArcGIS.esriSystem.IPersistVariant.ID
        {
            get
            {
                return new UID()
                {
                    Value = "JLK.SymbolEx.LabelSymbol"
                };
            }
        }

        public LabelSymbol()
        {
            this.Initialize();
        }

        public bool Applies(object pUnk)
        {
            IColor color = pUnk as IColor;
            LabelSymbol labelSymbol = pUnk as LabelSymbol;
            return ((color != null ? false : null == labelSymbol) ? false : true);
        }

        public object Apply(object newObject)
        {
            object current = null;
            IColor color = newObject as IColor;
            if (null != color)
            {
                current = ((IPropertySupport) this).Current[newObject];
                ((IMarkerSymbol) this).Color = color;
            }
            current = ((IPropertySupport) this).Current[newObject];
            ((IClone) this).Assign((IClone) newObject);
            return current;
        }

        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string str = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            LineSymbol.Register(str);
        }

        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string str = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            LineSymbol.Unregister(str);
        }

        public static double azimuth(double x1, double y1, double x2, double y2)
        {
            double num = x2 - x1;
            double num1 = y2 - y1;
            double num2 = Math.Atan2(num1, num)*57.2957795130823;
            if (num2 < 0)
            {
                num2 = 360 + num2;
            }
            return num2;
        }

        public static double azimuth(IPoint pt1, IPoint pt2)
        {
            double num = LabelSymbol.azimuth(pt1.X, pt1.Y, pt2.X, pt2.Y);
            return num;
        }

        private void CalcCoords(double x, ref double y)
        {
            this.m_coords[0].x = Convert.ToInt32(x);
            this.m_coords[0].y = Convert.ToInt32(y);
            double num = 0;
            num = Math.Sqrt(this.m_dDeviceRadius*this.m_dDeviceRadius/2);
            this.m_coords[2].x = Convert.ToInt32(x + this.m_dDeviceXOffset);
            this.m_coords[2].y = Convert.ToInt32(y - this.m_dDeviceYOffset);
            this.m_coords[1].x = this.m_coords[2].x - Convert.ToInt32(num);
            this.m_coords[1].y = this.m_coords[2].y - Convert.ToInt32(num);
            this.m_coords[4].x = this.m_coords[2].x + Convert.ToInt32(num);
            this.m_coords[4].y = this.m_coords[2].y + Convert.ToInt32(num);
            this.m_coords[3].x = this.m_coords[1].x;
            this.m_coords[3].y = this.m_coords[4].y;
            this.m_coords[5].x = this.m_coords[2].x - Convert.ToInt32(this.m_dDeviceRadius);
            this.m_coords[5].y = this.m_coords[2].y - Convert.ToInt32(this.m_dDeviceRadius);
            this.m_coords[6].x = this.m_coords[2].x + Convert.ToInt32(this.m_dDeviceRadius);
            this.m_coords[6].y = this.m_coords[2].y + Convert.ToInt32(this.m_dDeviceRadius);
            this.RotateCoords();
        }

        public bool CanApply(object pUnk)
        {
            return ((IPropertySupport) this).Applies(pUnk);
        }

        private void Draw(Graphics g, Utility.POINTAPI[] ctrlpts)
        {
            int i;
            double num = 0;
            List<double> nums = new List<double>();
            for (i = 0; i < (int) ctrlpts.Length - 1; i++)
            {
                double num1 = (double) (ctrlpts[i].x - ctrlpts[i + 1].x);
                double num2 = (double) (ctrlpts[i].y - ctrlpts[i + 1].y);
                double num3 = Math.Sqrt(num1*num1 + num2*num2);
                num = num + num3;
                nums.Add(num3);
            }
            double num4 = 50;
            PointF[] pointFArray = new PointF[(int) ctrlpts.Length];
            PointF[] pointFArray1 = new PointF[(int) ctrlpts.Length];
            double item = num;
            for (i = 0; i < (int) ctrlpts.Length - 1; i++)
            {
                double num5 = item/num;
                double num6 = LabelSymbol.azimuth((double) ctrlpts[i].x, (double) ctrlpts[i].y,
                    (double) ctrlpts[i + 1].x, (double) ctrlpts[i + 1].y);
                double num7 = num6 + 270;
                double num8 = num4*Math.Cos(num7/180*3.14159265358979)*num5;
                double num9 = num4*Math.Sin(num7/180*3.14159265358979)*num5;
                PointF pointF = new PointF();
                pointFArray[i].X = (float) ((double) ctrlpts[i].x + num8);
                pointFArray[i].Y = (float) ((double) ctrlpts[i].y + num9);
                num7 = num6 + 90;
                num8 = num4*Math.Cos(num7/180*3.14159265358979)*num5;
                num9 = num4*Math.Sin(num7/180*3.14159265358979)*num5;
                pointFArray1[i].X = (float) ((double) ctrlpts[i].x + num8);
                pointFArray1[i].Y = (float) ((double) ctrlpts[i].y + num9);
                item = item - nums[i];
            }
            int length = (int) ctrlpts.Length;
            double num10 = LabelSymbol.azimuth((double) ctrlpts[length - 2].x, (double) ctrlpts[length - 2].y,
                (double) ctrlpts[length - 1].x, (double) ctrlpts[length - 1].y);
            double num11 = 30;
            double num12 = num10 + 30 - 180;
            double num13 = num11*Math.Cos(num12/180*3.14159265358979);
            double num14 = num11*Math.Sin(num12/180*3.14159265358979);
            pointFArray[length - 1].X = (float) ((double) ctrlpts[length - 1].x + num13);
            pointFArray[length - 1].Y = (float) ((double) ctrlpts[length - 1].y + num14);
            double num15 = (double) ctrlpts[length - 1].x + num13 + num13;
            double num16 = (double) ctrlpts[length - 1].y + num14 + num14;
            num12 = num10 - 30 + 180;
            num13 = num11*Math.Cos(num12/180*3.14159265358979);
            num14 = num11*Math.Sin(num12/180*3.14159265358979);
            pointFArray1[length - 1].X = (float) ((double) ctrlpts[length - 1].x + num13);
            pointFArray1[length - 1].Y = (float) ((double) ctrlpts[length - 1].y + num14);
            double num17 = (double) ctrlpts[length - 1].x + num13 + num13;
            double num18 = (double) ctrlpts[length - 1].y + num14 + num14;
            g.DrawLine(Pens.Black, (float) ctrlpts[length - 1].x, (float) ctrlpts[length - 1].y, (float) num15,
                (float) num16);
            g.DrawLine(Pens.Black, (float) ctrlpts[length - 1].x, (float) ctrlpts[length - 1].y, (float) num17,
                (float) num18);
            if (length <= 2)
            {
                g.DrawLines(Pens.Red, pointFArray);
                g.DrawLines(Pens.Green, pointFArray1);
            }
            else
            {
                g.DrawCurve(Pens.Red, pointFArray);
                g.DrawCurve(Pens.Green, pointFArray1);
            }
        }

        private void Draw2(Graphics g, Utility.POINTAPI[] ctrlpts)
        {
            int i;
            double num;
            double num1;
            double num2;
            double num3;
            double num4;
            double num5;
            double num6;
            double num7 = 0;
            List<double> nums = new List<double>();
            for (i = 0; i < (int) ctrlpts.Length - 1; i++)
            {
                num = (double) (ctrlpts[i].x - ctrlpts[i + 1].x);
                num1 = (double) (ctrlpts[i].y - ctrlpts[i + 1].y);
                double num8 = Math.Sqrt(num*num + num1*num1);
                num7 = num7 + num8;
                nums.Add(num8);
            }
            double referenceScale = 1;
            if ((this.m_trans.ReferenceScale == 0 ? false : this.m_trans.Units != esriUnits.esriUnknownUnits))
            {
                try
                {
                    referenceScale = this.m_trans.ReferenceScale/this.m_trans.ScaleRatio;
                }
                catch
                {
                }
            }
            double num9 = 50*referenceScale;
            PointF[] x = new PointF[(int) ctrlpts.Length];
            PointF[] y = new PointF[(int) ctrlpts.Length];
            double item = num7;
            for (i = 0; i < (int) ctrlpts.Length - 1; i++)
            {
                num2 = item/num7;
                num3 = LabelSymbol.azimuth((double) ctrlpts[i].x, (double) ctrlpts[i].y, (double) ctrlpts[i + 1].x,
                    (double) ctrlpts[i + 1].y);
                num4 = num3 + 270;
                num5 = num9*Math.Cos(num4/180*3.14159265358979)*num2;
                num6 = num9*Math.Sin(num4/180*3.14159265358979)*num2;
                x[i].X = (float) ((double) ctrlpts[i].x + num5);
                x[i].Y = (float) ((double) ctrlpts[i].y + num6);
                num4 = num3 + 90;
                num5 = num9*Math.Cos(num4/180*3.14159265358979)*num2;
                num6 = num9*Math.Sin(num4/180*3.14159265358979)*num2;
                y[i].X = (float) ((double) ctrlpts[i].x + num5);
                y[i].Y = (float) ((double) ctrlpts[i].y + num6);
                item = item - nums[i];
            }
            int length = (int) ctrlpts.Length;
            double num10 = LabelSymbol.azimuth((double) ctrlpts[length - 2].x, (double) ctrlpts[length - 2].y,
                (double) ctrlpts[length - 1].x, (double) ctrlpts[length - 1].y);
            double num11 = 60*referenceScale;
            double num12 = num11*Math.Cos(0.523598775598299);
            if (num12 > nums[length - 2])
            {
                x[length - 1].X = x[length - 2].X;
                x[length - 1].Y = x[length - 2].Y;
                y[length - 1].X = y[length - 2].X;
                y[length - 1].Y = y[length - 2].Y;
            }
            else
            {
                double item1 = nums[length - 2] - num12;
                num = (double) ctrlpts[length - 2].x + item1*Math.Cos(num10/180*3.14159265358979);
                num1 = (double) ctrlpts[length - 2].y + item1*Math.Sin(num10/180*3.14159265358979);
                num2 = num12/num7;
                num3 = num10;
                num4 = num3 + 270;
                num5 = num9*Math.Cos(num4/180*3.14159265358979)*num2;
                num6 = num9*Math.Sin(num4/180*3.14159265358979)*num2;
                x[length - 1].X = (float) (num + num5);
                x[length - 1].Y = (float) (num1 + num6);
                num4 = num3 + 90;
                num5 = num9*Math.Cos(num4/180*3.14159265358979)*num2;
                num6 = num9*Math.Sin(num4/180*3.14159265358979)*num2;
                y[length - 1].X = (float) (num + num5);
                y[length - 1].Y = (float) (num1 + num6);
            }
            double num13 = num10 + 30 - 180;
            double num14 = num11*Math.Cos(num13/180*3.14159265358979);
            double num15 = num11*Math.Sin(num13/180*3.14159265358979);
            double num16 = (double) ctrlpts[length - 1].x + num14;
            double num17 = (double) ctrlpts[length - 1].y + num15;
            num13 = num10 - 30 + 180;
            num14 = num11*Math.Cos(num13/180*3.14159265358979);
            num15 = num11*Math.Sin(num13/180*3.14159265358979);
            double num18 = (double) ctrlpts[length - 1].x + num14;
            double num19 = (double) ctrlpts[length - 1].y + num15;
            System.Drawing.Color color = ColorTranslator.FromOle(this.m_pColor.RGB);
            Pen pen = new Pen(color, (float) this.m_widht);
            g.DrawLine(pen, (float) ctrlpts[length - 1].x, (float) ctrlpts[length - 1].y, (float) num16, (float) num17);
            g.DrawLine(pen, (float) ctrlpts[length - 1].x, (float) ctrlpts[length - 1].y, (float) num18, (float) num19);
            g.DrawLine(pen, (float) x[length - 1].X, (float) x[length - 1].Y, (float) num16, (float) num17);
            g.DrawLine(pen, (float) y[length - 1].X, (float) y[length - 1].Y, (float) num18, (float) num19);
            if (length <= 2)
            {
                g.DrawLines(pen, x);
                g.DrawLines(pen, y);
            }
            else
            {
                g.DrawCurve(pen, x);
                g.DrawCurve(pen, y);
            }
            pen.Dispose();
        }

        void ESRI.ArcGIS.Display.ISymbol.Draw(IGeometry Geometry)
        {
            if (Geometry != null)
            {
                if (Geometry is IPointCollection)
                {
                    IPointCollection geometry = (IPointCollection) Geometry;
                    this.m_coords = new Utility.POINTAPI[geometry.PointCount];
                    int num = 0;
                    int num1 = 0;
                    for (int i = 0; i < geometry.PointCount; i++)
                    {
                        IPoint point = geometry.Point[i];
                        Utility.FromMapPoint(this.m_trans, ref point, ref num, ref num1);
                        this.m_coords[i].x = num;
                        this.m_coords[i].y = num1;
                    }
                    Graphics graphic = Graphics.FromHdc(new IntPtr(this.m_lhDC));
                    this.Draw2(graphic, this.m_coords);
                    graphic.Dispose();
                }
            }
        }

        void ESRI.ArcGIS.Display.ISymbol.QueryBoundary(int hDC, ITransformation displayTransform, IGeometry Geometry,
            IPolygon Boundary)
        {
            if (!(Geometry == null | Boundary == null))
            {
                if (Geometry is IPointCollection)
                {
                    Boundary.SetEmpty();
                    IPointCollection geometry = (IPointCollection) Geometry;
                    IDisplayTransformation displayTransformation = (IDisplayTransformation) displayTransform;
                    this.QueryBoundsFromGeom(hDC, ref displayTransformation, ref Boundary, ref geometry);
                }
            }
        }

        void ESRI.ArcGIS.Display.ISymbol.ResetDC()
        {
            this.m_trans = null;
            this.m_lhDC = 0;
        }

        void ESRI.ArcGIS.Display.ISymbol.SetupDC(int hDC, ITransformation Transformation)
        {
            this.m_trans = Transformation as IDisplayTransformation;
            this.m_lhDC = hDC;
            this.SetupDeviceRatio(this.m_lhDC, this.m_trans);
            this.m_dDeviceRadius = this.m_dSize/2*this.m_dDeviceRatio;
            this.m_dDeviceXOffset = this.m_dXOffset*this.m_dDeviceRatio;
            this.m_dDeviceYOffset = this.m_dYOffset*this.m_dDeviceRatio;
            if (!this.m_bRotWithTrans)
            {
                this.m_dMapRotation = 0;
            }
            else
            {
                this.m_dMapRotation = this.m_trans.Rotation;
            }
        }

        void ESRI.ArcGIS.esriSystem.IClone.Assign(IClone src)
        {
            LabelSymbol labelSymbol = null;
            ILineSymbol lineSymbol = null;
            ILineSymbol width = null;
            if (src != null)
            {
                if (src is LabelSymbol)
                {
                    labelSymbol = src as LabelSymbol;
                    lineSymbol = src as ILineSymbol;
                    width = this;
                    width.Width = lineSymbol.Width;
                    width.Color = lineSymbol.Color;
                    //this.ROP2 = (src as ISymbol).ROP2;
                    //this.RotateWithTransform = (src as ISymbolRotation).RotateWithTransform;
                    //this.MapLevel = (src as IMapLevel).MapLevel;
                }
            }
        }

        IClone ESRI.ArcGIS.esriSystem.IClone.Clone()
        {
            IClone labelSymbol = null;
            labelSymbol = new LabelSymbol();
            labelSymbol.Assign(this);
            return labelSymbol;
        }

        bool ESRI.ArcGIS.esriSystem.IClone.IsEqual(IClone other)
        {
            bool width = false;
            LabelSymbol labelSymbol = null;
            LabelSymbol labelSymbol1 = null;
            ILineSymbol lineSymbol = null;
            ILineSymbol lineSymbol1 = null;
            ISymbol symbol = null;
            ISymbol symbol1 = null;
            IDisplayName displayName = null;
            IDisplayName displayName1 = null;
            ISymbolRotation symbolRotation = null;
            ISymbolRotation symbolRotation1 = null;
            IMapLevel mapLevel = null;
            IMapLevel mapLevel1 = null;
            if (other != null)
            {
                if (other is LabelSymbol)
                {
                    labelSymbol = other as LabelSymbol;
                    labelSymbol1 = this;
                    lineSymbol = other as ILineSymbol;
                    lineSymbol1 = this;
                    System.Drawing.Color color = ColorTranslator.FromOle(Convert.ToInt32(lineSymbol1.Color.RGB));
                    width = width & color.Equals(ColorTranslator.FromOle(Convert.ToInt32(lineSymbol.Color.RGB)));
                    width = width & lineSymbol1.Width == lineSymbol.Width;
                    symbol = other as ISymbol;
                    symbol1 = this;
                    width = width & symbol1.ROP2 == symbol.ROP2;
                    displayName = other as IDisplayName;
                    displayName1 = this;
                    width = width & (displayName1.NameString == displayName.NameString);
                    symbolRotation = other as ISymbolRotation;
                    symbolRotation1 = this;
                    width = width & symbolRotation1.RotateWithTransform == symbolRotation.RotateWithTransform;
                    mapLevel = other as IMapLevel;
                    mapLevel1 = this;
                    width = width & mapLevel1.MapLevel == mapLevel.MapLevel;
                }
            }
            return width;
        }

        bool ESRI.ArcGIS.esriSystem.IClone.IsIdentical(IClone other)
        {
            bool flag = false;
            if (other != null)
            {
                if (other is LabelSymbol)
                {
                    flag = (LabelSymbol) other == this;
                }
            }
            return flag;
        }

        void ESRI.ArcGIS.esriSystem.IPersistVariant.Load(IVariantStream Stream)
        {
            int num = 0;
            num = Convert.ToInt32(Stream.Read());
            if (num > 1 | num <= 0)
            {
                throw new Exception("Failed to read from stream");
            }
            this.InitializeMembers();
            if (num == 1)
            {
                this.m_lROP2 = (esriRasterOpCode) Stream.Read();
                this.m_widht = Convert.ToDouble(Stream.Read());
                this.m_pColor = Stream.Read() as IColor;
                this.m_bRotWithTrans = Convert.ToBoolean(Stream.Read());
                this.m_lMapLevel = Convert.ToInt32(Stream.Read());
            }
        }

        void ESRI.ArcGIS.esriSystem.IPersistVariant.Save(IVariantStream Stream)
        {
            Stream.Write(1);
            Stream.Write(this.m_lROP2);
            Stream.Write(this.m_widht);
            Stream.Write(this.m_pColor);
            Stream.Write(this.m_bRotWithTrans);
            Stream.Write(this.m_lMapLevel);
        }

        ~LabelSymbol()
        {
            this.Terminate();
        }

        public object get_Current(object pUnk)
        {
            object color;
            if (null == pUnk as IColor)
            {
                color = ((IClone) this).Clone();
            }
            else
            {
                color = ((ILineSymbol) this).Color;
            }
            return color;
        }

        private void Initialize()
        {
            this.InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.m_lhDC = 0;
            this.m_lOldPen = 0;
            this.m_lPen = 0;
            this.m_lOldBrush = 0;
            this.m_lBrushTop = 0;
            this.m_lBrushLeft = 0;
            this.m_lBrushRight = 0;
            this.m_dDeviceRadius = 0;
            this.m_trans = null;
            this.m_lROP2 = esriRasterOpCode.esriROPCopyPen;
            this.m_widht = 1;
            this.m_bRotWithTrans = true;
        }

        private double PointsToMap(ITransformation displayTransform, double dPointSize)
        {
            double num = 0;
            num = (displayTransform != null
                ? ((IDisplayTransformation) displayTransform).FromPoints(dPointSize)
                : dPointSize*this.m_dDeviceRatio);
            return num;
        }

        private void QueryBound(IPointCollection pts, ITransformation displayTransform, out double minx, out double miny,
            out double maxx, out double maxy)
        {
            IPoint point;
            IPoint point1;
            int i;
            double x;
            double y;
            double num;
            double num1;
            double num2;
            double num3;
            double num4;
            double num5 = 0;
            List<double> nums = new List<double>();
            for (i = 0; i < pts.PointCount - 1; i++)
            {
                point = pts.Point[i];
                point1 = pts.Point[i + 1];
                x = point.X - point1.X;
                y = point.Y - point1.Y;
                double num6 = Math.Sqrt(x*x + y*y);
                num5 = num5 + num6;
                nums.Add(num6);
            }
            double num7 = 1;
            IEnvelope envelope = (pts as IGeometry).Envelope;
            double map = this.PointsToMap(displayTransform, 50)*num7;
            minx = envelope.XMin;
            miny = envelope.YMin;
            maxx = envelope.XMax;
            maxy = envelope.YMax;
            double item = num5;
            for (i = 0; i < pts.PointCount - 1; i++)
            {
                point = pts.Point[i];
                point1 = pts.Point[i + 1];
                num = item/num5;
                num1 = LabelSymbol.azimuth(point, point1);
                num2 = num1 + 270;
                num3 = map*Math.Cos(num2/180*3.14159265358979)*num;
                num4 = map*Math.Sin(num2/180*3.14159265358979)*num;
                maxx = (maxx > point.X + num3 ? maxx : point.X + num3);
                maxy = (maxy > point.Y + num4 ? maxy : point.Y + num4);
                minx = (minx < point.X + num3 ? minx : point.X + num3);
                miny = (miny < point.Y + num4 ? miny : point.Y + num4);
                num2 = num1 + 90;
                num3 = map*Math.Cos(num2/180*3.14159265358979)*num;
                num4 = map*Math.Sin(num2/180*3.14159265358979)*num;
                maxx = (maxx > point.X + num3 ? maxx : point.X + num3);
                maxy = (maxy > point.Y + num4 ? maxy : point.Y + num4);
                minx = (minx < point.X + num3 ? minx : point.X + num3);
                miny = (miny < point.Y + num4 ? miny : point.Y + num4);
                item = item - nums[i];
            }
            int pointCount = pts.PointCount;
            point = pts.Point[pointCount - 2];
            point1 = pts.Point[pointCount - 1];
            double num8 = LabelSymbol.azimuth(point, point1);
            double map1 = this.PointsToMap(displayTransform, 60)*num7;
            double num9 = map1*Math.Cos(0.523598775598299);
            if (num9 <= nums[pointCount - 2])
            {
                double item1 = nums[pointCount - 2] - num9;
                x = point.X + item1*Math.Cos(num8/180*3.14159265358979);
                y = point.Y + item1*Math.Sin(num8/180*3.14159265358979);
                num = num9/num5;
                num1 = num8;
                num2 = num1 + 270;
                num3 = map*Math.Cos(num2/180*3.14159265358979)*num;
                num4 = map*Math.Sin(num2/180*3.14159265358979)*num;
                maxx = (maxx > x + num3 ? maxx : x + num3);
                maxy = (maxy > y + num4 ? maxy : y + num4);
                minx = (minx < x + num3 ? minx : x + num3);
                miny = (miny < y + num4 ? miny : y + num4);
                num2 = num1 + 90;
                num3 = map*Math.Cos(num2/180*3.14159265358979)*num;
                num4 = map*Math.Sin(num2/180*3.14159265358979)*num;
                maxx = (maxx > x + num3 ? maxx : x + num3);
                maxy = (maxy > y + num4 ? maxy : y + num4);
                minx = (minx < x + num3 ? minx : x + num3);
                miny = (miny < y + num4 ? miny : y + num4);
            }
            double num10 = num8 + 30 - 180;
            double num11 = map1*Math.Cos(num10/180*3.14159265358979);
            double num12 = map1*Math.Sin(num10/180*3.14159265358979);
            maxx = (maxx > point1.X + num11 ? maxx : point1.X + num11);
            maxy = (maxy > point1.Y + num12 ? maxy : point1.Y + num12);
            minx = (minx < point1.X + num11 ? minx : point1.X + num11);
            miny = (miny < point1.Y + num12 ? miny : point1.Y + num12);
            num10 = num8 - 30 + 180;
            num11 = map1*Math.Cos(num10/180*3.14159265358979);
            num12 = map1*Math.Sin(num10/180*3.14159265358979);
            maxx = (maxx > point1.X + num11 ? maxx : point1.X + num11);
            maxy = (maxy > point1.Y + num12 ? maxy : point1.Y + num12);
            minx = (minx < point1.X + num11 ? minx : point1.X + num11);
            miny = (miny < point1.Y + num12 ? miny : point1.Y + num12);
        }

        private void QueryBoundsFromGeom(int hDC, ref IDisplayTransformation transform, ref IPolygon boundary,
            ref IPointCollection points)
        {
            double map = 0;
            double num = 0;
            double map1 = 0;
            num = this.PointsToMap(transform, this.m_dSize);
            if (this.m_dXOffset != 0)
            {
                map = this.PointsToMap(transform, this.m_dXOffset);
            }
            if (this.m_dYOffset != 0)
            {
                map1 = this.PointsToMap(transform, this.m_dYOffset);
            }
            this.SetupDeviceRatio(hDC, transform);
            IPointCollection pointCollection = null;
            ISegmentCollection segmentCollection = null;
            double num1 = 0;
            double num2 = 0;
            pointCollection = (IPointCollection) boundary;
            segmentCollection = (ISegmentCollection) boundary;
            num2 = num/2;
            num1 = Math.Sqrt(num2*num2/2);
            object value = Missing.Value;
            IEnvelope envelope = (points as IGeometry).Envelope;
            double num3 = 100;
            double num4 = 200;
            double num5 = -100;
            double num6 = 400;
            this.QueryBound(points, transform, out num3, out num4, out num5, out num6);
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.PutCoords(num3, num4);
            pointCollection.AddPoint(pointClass, ref value, ref value);
            pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.PutCoords(num5, num4);
            pointCollection.AddPoint(pointClass, ref value, ref value);
            pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.PutCoords(num5, num6);
            pointCollection.AddPoint(pointClass, ref value, ref value);
            pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.PutCoords(num3, num6);
            pointCollection.AddPoint(pointClass, ref value, ref value);
            pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.PutCoords(num3, num4);
            pointCollection.AddPoint(pointClass, ref value, ref value);
        }

        [ComRegisterFunction]
        [ComVisible(false)]
        private static void RegisterFunction(Type registerType)
        {
            LabelSymbol.ArcGISCategoryRegistration(registerType);
        }

        private void RotateCoords()
        {
            double mDAngle = 0;
            mDAngle = 360 - (this.m_dAngle + this.m_dMapRotation);
            short i = 0;
            Utility.POINTAPI mCoords = new Utility.POINTAPI();
            for (i = 0; i <= 4; i = (short) (i + 1))
            {
                if (i != 2)
                {
                    mCoords = this.m_coords[i];
                    this.m_coords[i].x = this.m_coords[2].x +
                                         Convert.ToInt32((double) (mCoords.x - this.m_coords[2].x)*
                                                         Math.Cos(Utility.Radians(mDAngle))) -
                                         Convert.ToInt32((double) (mCoords.y - this.m_coords[2].y)*
                                                         Math.Sin(Utility.Radians(mDAngle)));
                    this.m_coords[i].y = this.m_coords[2].y +
                                         Convert.ToInt32((double) (mCoords.x - this.m_coords[2].x)*
                                                         Math.Sin(Utility.Radians(mDAngle))) +
                                         Convert.ToInt32((double) (mCoords.y - this.m_coords[2].y)*
                                                         Math.Cos(Utility.Radians(mDAngle)));
                }
            }
        }

        private void SetupDeviceRatio(int hDC, IDisplayTransformation displayTransform)
        {
            if (displayTransform != null)
            {
                if (displayTransform.Resolution != 0)
                {
                    this.m_dDeviceRatio = displayTransform.Resolution/72;
                    if (displayTransform.ReferenceScale != 0)
                    {
                        this.m_dDeviceRatio = this.m_dDeviceRatio*displayTransform.ReferenceScale/
                                              displayTransform.ScaleRatio;
                    }
                }
            }
            else if (hDC == 0)
            {
                this.m_dDeviceRatio = (double) (1/(Utility.TwipsPerPixelX()/20));
            }
            else
            {
                this.m_dDeviceRatio = Convert.ToDouble(Utility.GetDeviceCaps(hDC, 88))/72;
            }
        }

        private void Terminate()
        {
            this.m_trans = null;
            this.m_pColor = null;
        }

        [ComUnregisterFunction]
        [ComVisible(false)]
        private static void UnregisterFunction(Type registerType)
        {
            LabelSymbol.ArcGISCategoryUnregistration(registerType);
        }
    }
}