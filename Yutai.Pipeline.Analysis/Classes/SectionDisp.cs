using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Yutai.Pipeline.Analysis.Classes
{
    public class SectionDisp
    {
        private IPipeConfig ipipeConfig_0;

        public ArrayList m_arrObjectRects = new ArrayList();

        public int m_nSelectIndex = -1;

        private bool bool_0;

        private bool KsymctcwAN;

        private bool bool_1;

        private string string_0 = "地下管线断面图";

        private string string_1;

        private string string_2;

        private bool bool_2 = true;

        private Form form_0;

        private Graphics graphics_0;

        private Matrix matrix_0 = new Matrix();

        private int int_0;

        private int int_1;

        private float float_0;

        private float float_1;

        private float float_2;

        private float float_3;

        private float float_4;

        private float float_5;

        private float float_6;

        private float float_7;

        private float float_8;

        private float float_9;

        private float float_10;

        private float float_11;

        private float float_12;

        private float float_13;

        private float float_14;

        private float gjPaJaeteX;

        private float float_15;

        private int int_2;

        private float float_16;

        private float float_17;

        private float float_18;

        private float float_19;

        private float float_20;

        private float float_21;

        private double double_0;

        private double double_1;

        private double double_2;

        private double double_3;

        private float float_22;

        private float float_23;

        private float float_24;

        private float float_25;

        private float float_26;

        private float float_27;

        private float float_28;

        private float float_29 = 1f;

        private string[] string_3 = new string[6];

        private double double_4;

        public bool bPrint;

        private float float_30;

        private float float_31;

        private float yQsaxPkehn = 1f;

        public IPipeConfig PipeConfig
        {
            get
            {
                return this.ipipeConfig_0;
            }
            set
            {
                this.ipipeConfig_0 = value;
            }
        }

        public float OnePixelInMM
        {
            get
            {
                return 25.4f / this.graphics_0.DpiX;
            }
        }

        public bool CustomScale
        {
            get
            {
                return this.KsymctcwAN;
            }
            set
            {
                this.KsymctcwAN = value;
            }
        }

        public string Title
        {
            get
            {
                return this.string_0;
            }
            set
            {
                this.string_0 = value;
            }
        }

        public string RoadName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string SectionNo
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public bool bRotate
        {
            get
            {
                return this.bool_2;
            }
            set
            {
                this.bool_2 = value;
            }
        }

        public bool bFalse
        {
            get
            {
                return this.bool_1;
            }
            set
            {
                this.bool_1 = value;
            }
        }

        public double TranAngle
        {
            get
            {
                return this.double_4;
            }
            set
            {
                this.double_4 = value;
            }
        }

        public float PrintScaleX
        {
            get
            {
                return this.float_26;
            }
            set
            {
                this.float_26 = value;
            }
        }

        public float PrintScaleY
        {
            get
            {
                return this.float_27;
            }
            set
            {
                this.float_27 = value;
            }
        }

        public string TranAngleText
        {
            get
            {
                string[] array = new string[]
                {
                    "E",
                    "EN",
                    "N",
                    "WN",
                    "W",
                    "WS",
                    "S",
                    "ES"
                };
                double num = this.double_4;
                num = ApplyMath.MakeAngleTo2P(num);
                int num2 = (int)num;
                int num3 = num2 / 45;
                int num4 = num2 - 45 * num3;
                if ((double)num4 >= 22.5)
                {
                    num3++;
                }
                num3 %= 8;
                return array[num3];
            }
        }

        public SectionDisp(object objFrom)
        {
            this.form_0 = (Form)objFrom;
            this.graphics_0 = this.form_0.CreateGraphics();
            this.string_3[0] = "埋深(m)";
            this.string_3[1] = "规格(mm)";
            this.string_3[2] = "间距(m)";
            this.string_3[3] = "红线间距";
            this.string_3[4] = "管线高程";
            this.string_3[5] = "地面高程";
            this.float_24 = 100f;
            this.float_25 = 100f;
            this.float_28 = 1f;
            this.float_29 = 1f;
            this.bool_0 = false;
            this.int_2 = 6;
            this.method_3();
        }

        private string method_0(string text)
        {
            return this.ipipeConfig_0.getLineConfig_DM(text);
        }

        private void method_1()
        {
            Font font = new Font("宋体", 0.4f * this.float_13 * this.float_28 * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            Pen pen = new Pen(Color.Black, this.OnePixelInMM);
            float x = this.float_16 + this.float_20 * 0.8f;
            float y = this.float_17 + this.float_2 + this.float_10 + this.float_11 + this.float_12 + this.float_13;
            PointF pt = new PointF(x, y);
            this.method_9(ref pt, (double)this.float_28);
            x = this.float_16 + this.float_20 * 0.9f;
            PointF pointF = new PointF(x, y);
            this.method_9(ref pointF, (double)this.float_28);
            this.graphics_0.DrawLine(pen, pt, pointF);
            GPoint gPoint = new GPoint();
            gPoint.X = (double)pointF.X;
            gPoint.Y = (double)pointF.Y;
            gPoint.OffSetByLen(210.0, (double)this.float_20 * 0.025 * (double)this.float_28);
            PointF pt2 = new PointF((float)gPoint.X, (float)gPoint.Y);
            this.graphics_0.DrawLine(pen, pointF, pt2);
            PointF point = default(PointF);
            point.X = (pt.X + pointF.X) / 2f;
            point.Y = pt.Y;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Far;
            this.graphics_0.DrawString(this.TranAngleText, font, Brushes.Black, point, stringFormat);
        }

        private float method_2(float x)
        {
            Graphics graphics = this.form_0.CreateGraphics();
            graphics.Transform.Reset();
            PointF[] array = new PointF[1];
            array[0].X = x;
            array[0].Y = 0f;
            graphics.PageUnit = GraphicsUnit.Millimeter;
            graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, array);
            graphics.Dispose();
            return array[0].X;
        }

        public void CalcLogicalInfo()
        {
            Graphics graphics = this.form_0.CreateGraphics();
            this.graphics_0 = this.form_0.CreateGraphics();
            graphics.Transform.Reset();
            Rectangle clientRectangle = this.form_0.ClientRectangle;
            clientRectangle.Y += this.int_0;
            clientRectangle.Y += this.int_1;
            clientRectangle.Height -= this.int_0;
            clientRectangle.Height -= this.int_1;
            PointF[] array = new PointF[]
            {
                new PointF(clientRectangle.Left, clientRectangle.Top),
                new PointF(clientRectangle.Right, clientRectangle.Bottom)
            };
            graphics.Transform.Reset();
            graphics.PageUnit = GraphicsUnit.Millimeter;
            graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, array);
            this.float_16 = (float)array[0].X;
            this.float_17 = (float)array[0].Y;
            this.float_18 = (float)array[1].X;
            this.float_19 = (float)array[1].Y;
            this.float_20 = Math.Abs(this.float_18 - this.float_16);
            this.float_21 = Math.Abs(this.float_19 - this.float_17);
            graphics.Dispose();
        }

        public void CalcLogicalInfoP(object objPrint)
        {
            PrintPageEventArgs printPageEventArgs = (PrintPageEventArgs)objPrint;
            float num = (float)printPageEventArgs.PageBounds.Width * 2.54f / 10f;
            float num2 = (float)printPageEventArgs.PageBounds.Height * 2.54f / 10f;
            Margins margins = printPageEventArgs.PageSettings.Margins;
            this.float_0 = (float)margins.Left * 2.54f / 10f;
            this.float_1 = (float)margins.Right * 2.54f / 10f;
            this.float_2 = (float)margins.Top * 2.54f / 10f;
            this.float_3 = (float)margins.Bottom * 2.54f / 10f;
            this.float_16 = 0f;
            this.float_17 = 0f;
            this.float_18 = (this.float_20 = num);
            this.float_19 = (this.float_21 = num2);
        }

        public void DrawFrame()
        {
            this.graphics_0.FillRegion(Brushes.White, this.graphics_0.Clip);
            this.method_14();
            this.method_15();
            this.method_4();
            this.method_5();
            this.method_6();
            this.method_7();
            this.method_16();
            this.method_17();
            this.method_8();
        }

        public void Paint()
        {
            this.float_28 = this.float_29;
            this.bool_0 = false;
            this.method_3();
            this.CalcLogicalInfo();
            this.graphics_0.PageUnit = GraphicsUnit.Millimeter;
            this.graphics_0.Transform = this.matrix_0;
            this.DrawFrame();
        }

        public void PrintPage(object objPrint)
        {
            this.float_29 = this.float_28;
            this.float_28 = 1f;
            this.yQsaxPkehn = 1f;
            this.bPrint = true;
            this.bool_0 = true;
            this.method_3();
            PrintPageEventArgs printPageEventArgs = (PrintPageEventArgs)objPrint;
            this.graphics_0 = printPageEventArgs.Graphics;
            this.CalcLogicalInfoP(objPrint);
            this.graphics_0.PageUnit = GraphicsUnit.Millimeter;
            this.DrawFrame();
            this.bPrint = false;
        }

        public void OnResize(int nMenuH, int nToolBarH, float a, float b)
        {
            this.int_0 = nMenuH;
            this.int_1 = nToolBarH;
            this.graphics_0 = this.form_0.CreateGraphics();
            this.ViewEntire();
            this.float_30 = a;
            this.float_31 = b;
        }

        private void method_3()
        {
            if (!this.bool_0)
            {
                this.float_4 = 22f;
                this.float_6 = 3f;
                this.float_8 = 20f;
                this.float_9 = 1f;
                this.float_5 = 7f;
                this.float_7 = 11f;
                this.float_10 = 7f;
                this.float_11 = 3f;
                this.float_12 = 3f;
                this.float_13 = 7f;
                this.float_0 = 4f;
                this.float_1 = 4f;
                this.float_2 = 4f;
                this.float_3 = 4f;
            }
            else
            {
                this.float_4 = 30f;
                this.float_5 = 10f;
                this.float_6 = 5f;
                this.float_7 = 15f;
                this.float_8 = 20f;
                this.float_9 = 2f;
                this.float_10 = 10f;
                this.float_11 = 5f;
                this.float_12 = 5f;
                this.float_13 = 10f;
                this.float_0 = 10f;
                this.float_1 = 10f;
                this.float_2 = 10f;
                this.float_3 = 10f;
            }
        }

        public void Pan(Point ptDown, Point ptUp)
        {
            float offsetX = this.method_2((float)(ptUp.X - ptDown.X));
            float offsetY = this.method_2((float)(ptUp.Y - ptDown.Y));
            this.matrix_0.Translate(offsetX, offsetY);
            this.form_0.Invalidate();
        }

        public void ZoomIn(Point ptVal)
        {
            Rectangle clientRectangle = this.form_0.ClientRectangle;
            PointF pointF = new PointF((float)((clientRectangle.Left + clientRectangle.Right) / 2), (float)((clientRectangle.Top + clientRectangle.Bottom) / 2));
            float num = (float)ptVal.X - pointF.X;
            float num2 = (float)ptVal.Y - pointF.Y;
            num = this.method_2(num);
            num2 = this.method_2(num2);
            this.matrix_0.Translate(-num, -num2);
            num = (float)clientRectangle.Width * this.float_28 * 0.05f;
            num2 = (float)clientRectangle.Height * this.float_28 * 0.05f;
            num = this.method_2(num);
            num2 = this.method_2(num2);
            this.matrix_0.Translate(-num, -num2);
            this.float_28 *= 1.1f;
            this.float_29 = this.float_28;
            this.form_0.Invalidate();
        }

        public void ZoomOut(Point ptVal)
        {
            Rectangle clientRectangle = this.form_0.ClientRectangle;
            PointF pointF = new PointF((float)((clientRectangle.Left + clientRectangle.Right) / 2), (float)((clientRectangle.Top + clientRectangle.Bottom) / 2));
            float num = (float)ptVal.X - pointF.X;
            float num2 = (float)ptVal.Y - pointF.Y;
            num = this.method_2(num);
            num2 = this.method_2(num2);
            this.matrix_0.Translate(-num, -num2);
            num = (float)clientRectangle.Width * this.float_28 * 0.05f;
            num2 = (float)clientRectangle.Height * this.float_28 * 0.05f;
            num = this.method_2(num);
            num2 = this.method_2(num2);
            this.matrix_0.Translate(num, num2);
            this.float_28 /= 1.1f;
            this.float_29 = this.float_28;
            this.form_0.Invalidate();
        }

        public void ViewEntire()
        {
            this.matrix_0.Reset();
            this.float_28 = 1f;
            this.float_29 = this.float_28;
            this.form_0.Invalidate();
        }

        private void vmEmJgFbCo()
        {
            RectangleF rectangleF = default(Rectangle);
            rectangleF.X = this.float_16 + this.float_0;
            rectangleF.Y = this.float_17 + this.float_2 + this.float_10 + this.float_11;
            rectangleF.Width = this.float_20 - this.float_0 - this.float_1;
            rectangleF.Height = this.float_21 - this.float_2 - this.float_3 - this.float_10 - this.float_11 + this.OnePixelInMM;
            this.method_10(ref rectangleF, (double)this.float_28);
            Pen pen = new Pen(Color.Black, 25.4f / this.graphics_0.DpiX);
            this.graphics_0.DrawRectangle(pen, this.method_12(rectangleF));
        }

        private void method_4()
        {
            float num = this.float_16 + this.float_0 + this.float_6;
            float num2 = this.float_18 - this.float_1 - this.float_6;
            float num3 = this.float_19 - (this.float_3 + this.float_5 * (float)this.int_2 * this.yQsaxPkehn);
            float num4 = this.float_19 - this.float_3;
            float num5 = num + this.float_4;
            num *= this.float_28;
            num2 *= this.float_28;
            num3 *= this.float_28;
            num4 *= this.float_28;
            num5 *= this.float_28;
            Pen pen = new Pen(Color.Black, 50.8f / this.graphics_0.DpiX);
            this.graphics_0.DrawLine(pen, (this.float_16 + this.float_0) * this.float_28, (this.float_17 + this.float_2 + this.float_10 + this.float_11) * this.float_28, (this.float_18 - this.float_1) * this.float_28, (this.float_17 + this.float_2 + this.float_10 + this.float_11) * this.float_28);
            this.graphics_0.DrawLine(pen, (this.float_16 + this.float_0) * this.float_28, (this.float_17 + this.float_2 + this.float_10 + this.float_11) * this.float_28, (this.float_16 + this.float_0) * this.float_28, num4);
            this.graphics_0.DrawLine(pen, (this.float_18 - this.float_1) * this.float_28, (this.float_17 + this.float_2 + this.float_10 + this.float_11) * this.float_28, (this.float_18 - this.float_1) * this.float_28, num4);
            this.graphics_0.DrawLine(pen, (this.float_16 + this.float_0) * this.float_28, num4, (this.float_18 - this.float_1) * this.float_28, num4);
            Pen pen2 = new Pen(Color.Black, this.OnePixelInMM);
            this.graphics_0.DrawLine(pen2, num, num4, num, num3);
            this.graphics_0.DrawLine(pen2, num5, num4, num5, num3);
            this.graphics_0.DrawLine(pen2, num2, num4, num2, num3);
            float num6 = this.float_28 * this.float_5 * this.yQsaxPkehn;
            for (long num7 = 0L; num7 <= (long)this.int_2; num7 += 1L)
            {
                this.graphics_0.DrawLine(pen2, num, num4 - (float)num7 * num6, num2, num4 - (float)num7 * num6);
            }
        }

        private void method_5()
        {
            Font font = new Font("宋体", 0.4f * this.float_5 * this.float_28 * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < this.int_2; i++)
            {
                float x = this.float_16 + this.float_0 + this.float_6;
                float y = this.float_19 - this.float_3 - this.float_5 * (float)(i + 1) * this.yQsaxPkehn;
                float width = this.float_4;
                float height = this.float_5;
                RectangleF layoutRectangle = new RectangleF(x, y, width, height);
                this.method_10(ref layoutRectangle, (double)this.float_28);
                string s = this.string_3[this.int_2 - this.int_2 + i];
                this.graphics_0.DrawString(s, font, Brushes.Black, layoutRectangle, stringFormat);
            }
        }

        private void method_6()
        {
            RectangleF layoutRectangle = default(RectangleF);
            layoutRectangle.X = this.float_16 + this.float_0;
            layoutRectangle.Y = this.float_17 + this.float_2;
            layoutRectangle.Height = this.float_10 * this.yQsaxPkehn;
            layoutRectangle.Width = this.float_20 - this.float_0 - this.float_1;
            this.method_10(ref layoutRectangle, (double)this.float_28);
            Font font = new Font("宋体", 0.8f * this.float_10 * this.float_28 * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            this.graphics_0.DrawString(this.string_0, font, Brushes.Black, layoutRectangle, stringFormat);
        }

        private void method_7()
        {
            RectangleF layoutRectangle = default(RectangleF);
            layoutRectangle.X = this.float_16 + this.float_0 + this.float_6;
            layoutRectangle.Y = this.float_17 + this.float_2 + this.float_10 / this.yQsaxPkehn;
            layoutRectangle.Height = this.float_11 * this.yQsaxPkehn;
            layoutRectangle.Width = this.float_20 - this.float_0 - this.float_1 - 2f * this.float_6;
            this.method_10(ref layoutRectangle, (double)this.float_28);
            Font font = new Font("宋体", 0.8f * this.float_11 * this.float_28 * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            string text = "所在道路：";
            text += this.string_1;
            this.graphics_0.DrawString(text, font, Brushes.Black, layoutRectangle, stringFormat);
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;
            string text2 = "断面号:";
            text2 += this.string_2;
            this.graphics_0.DrawString(text2, font, Brushes.Black, layoutRectangle, stringFormat);
        }

        private void method_8()
        {
            Font font = new Font("宋体", 0.4f * this.float_13 * this.float_28 * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            float x = (this.float_16 + this.float_20) / 2f;
            float y = this.float_17 + this.float_2 + this.float_10 * this.yQsaxPkehn + this.float_11 + this.float_12 + this.float_13;
            PointF point = new PointF(x, y);
            this.method_9(ref point, (double)this.float_28);
            int num = this.bool_0 ? ((int)this.float_26) : ((int)this.float_24);
            int num2 = this.bool_0 ? ((int)this.float_27) : ((int)this.float_25);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Center;
            this.graphics_0.DrawString("比例尺", font, Brushes.Black, point, stringFormat);
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;
            this.graphics_0.DrawString("垂直:1:" + num2.ToString("f0"), font, Brushes.Black, point, stringFormat);
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Far;
            this.graphics_0.DrawString("水平:1:" + num.ToString("f0"), font, Brushes.Black, point, stringFormat);
        }

        private void method_9(ref PointF ptr, double num)
        {
            ptr.X *= (float)num;
            ptr.Y *= (float)num;
        }

        private void method_10(ref RectangleF ptr, double num)
        {
            ptr.X *= (float)num;
            ptr.Y *= (float)num;
            ptr.Width *= (float)num;
            ptr.Height *= (float)num;
        }

        private void method_11(ref SizeF ptr, double num)
        {
            ptr.Width *= (float)num;
            ptr.Height *= (float)num;
        }

        private Rectangle method_12(RectangleF rectangleF)
        {
            return new Rectangle
            {
                X = (int)rectangleF.X,
                Y = (int)rectangleF.Y,
                Width = (int)rectangleF.Width,
                Height = (int)rectangleF.Height
            };
        }

        public void SetDataBound(double dMinX, double dMaxX, double dMinY, double dMaxY)
        {
            this.double_0 = dMinX * 1000.0;
            this.double_1 = dMaxX * 1000.0;
            this.double_2 = dMinY * 1000.0;
            this.double_3 = dMaxY * 1000.0;
            this.method_13(ref this.double_2, ref this.double_3);
            this.float_22 = (float)(this.double_1 - this.double_0);
            this.float_23 = (float)(this.double_3 - this.double_2);
            this.float_24 = this.float_22 / this.float_15;
            this.float_25 = this.float_23 / this.float_8 / 5f;
        }

        private void method_13(ref double ptr, ref double ptr2)
        {
            int num = (int)ptr / 1000;
            num = (num - 1) * 1000;
            int num2 = (int)ptr2 / 1000;
            num2 *= 1000;
            int num3 = num2 - num;
            int num4 = num3 / 5000;
            num4 = (num4 + 1) * 5000;
            if ((num4 - num3) % 2000 == 0)
            {
                num -= (num4 - num3) / 2;
                num2 += (num4 - num3) / 2;
            }
            else
            {
                num -= (num4 - num3) / 2000 * 1000;
                num2 += ((num4 - num3) / 2000 + 1) * 1000;
            }
            ptr = (double)num;
            ptr2 = (double)num2;
        }

        private void method_14()
        {
            this.float_15 = this.float_20 - this.float_0 - this.float_6 - this.float_4 - this.float_1 - this.float_6;
            this.float_14 = this.float_16 + this.float_0 + this.float_6 + this.float_4 + this.float_15 * 0.1f;
            this.gjPaJaeteX = this.float_20 - this.float_1 - this.float_6 - this.float_15 * 0.1f;
            this.float_15 *= 0.8f;
        }

        private void method_15()
        {
            this.float_24 = this.float_22 / this.float_15;
            Rectangle clientRectangle = this.form_0.ClientRectangle;
            if (this.bPrint)
            {
                this.float_25 = this.float_23 / this.float_7 / 5f + 1f;
            }
            else
            {
                this.yQsaxPkehn = (float)clientRectangle.Height / 514f;
                this.float_25 = this.float_23 / this.float_7 / 5f / this.yQsaxPkehn + 1f;
            }
            if (!this.KsymctcwAN)
            {
                this.float_26 = this.float_24;
                this.float_27 = this.float_25;
            }
        }

        private void method_16()
        {
            float num = this.bool_0 ? (this.float_23 / this.float_27 / 5f) : (this.float_7 * this.yQsaxPkehn);
            float num2 = this.float_9;
            float onePixelInMM = this.OnePixelInMM;
            float num3 = 0f;
            Pen pen = new Pen(Color.Black, onePixelInMM);
            float num4 = this.float_16 + this.float_0 + this.float_6 + this.float_4;
            float num5 = this.float_19 - (this.float_3 + this.float_5 * (float)this.int_2 * this.yQsaxPkehn);
            float num6 = this.float_16 + this.float_0 + this.float_6 + this.float_4 + num2;
            num4 *= this.float_28;
            num6 *= this.float_28;
            num5 *= this.float_28;
            for (long num7 = 0L; num7 <= 4L; num7 += 1L)
            {
                num3 = this.float_19 - (this.float_3 + this.float_5 * (float)this.int_2 * this.yQsaxPkehn + num * (float)(num7 + 1L));
                num3 *= this.float_28;
                this.graphics_0.DrawLine(pen, new PointF(num4, num3), new PointF(num6, num3));
            }
            this.graphics_0.DrawLine(pen, new PointF(num4, num5), new PointF(num4, num3));
        }

        private void method_17()
        {
            float num = this.bool_0 ? (this.float_23 / this.float_27 / 5f) : (this.float_7 * this.yQsaxPkehn);
            Font font = new Font("宋体", 0.4f * this.float_5 * this.float_28 * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            for (int i = 1; i <= 5; i++)
            {
                float num2 = (float)i * num;
                num2 = this.float_19 - (this.float_3 + this.float_5 * (float)this.int_2 * this.yQsaxPkehn + num2);
                float num3 = this.float_16 + this.float_0 + this.float_6 + this.float_4 - 4f;
                float num4 = num2;
                num3 *= this.float_28;
                num4 *= this.float_28;
                string s = Convert.ToString((this.double_2 + (double)((float)i * this.float_23 / 5f)) / 1000.0);
                this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(num3, num4), stringFormat);
            }
        }

        public void DrawVerSection(ArrayList pArrPipLines, ArrayList pArrPipePoints, int nSelectIndex)
        {
            int count = pArrPipLines.Count;
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine)pArrPipLines[i];
                int num = pipeLine.Size();
                for (int j = 0; j < num; j++)
                {
                    GPoint gPoint = pipeLine[j];
                    Pen pen = new Pen(Color.Black, this.OnePixelInMM);
                    pen.DashStyle = DashStyle.Dash;
                    double x = gPoint.X;
                    double z = gPoint.Z;
                    float num2 = this.method_23(x);
                    float num3 = this.method_24(z);
                    float num4 = num2;
                    float num5 = this.float_19 - this.float_3;
                    num2 *= this.float_28;
                    num3 *= this.float_28;
                    num4 *= this.float_28;
                    num5 *= this.float_28;
                    this.graphics_0.DrawLine(pen, new PointF(num2, num3), new PointF(num4, num5));
                    double x2 = gPoint.X;
                    float num6 = this.method_23(x2);
                    num6 *= this.float_28;
                    float num7 = this.float_19 - (this.float_3 + this.float_5 * (float)this.int_2 * this.yQsaxPkehn);
                    float num8 = num7 + this.float_5 * this.yQsaxPkehn;
                    float num9 = (float)gPoint.M;
                    string text = num9.ToString("f3");
                    this.method_31(num6, num7 * this.float_28, num8 * this.float_28, 5, this.float_5 * this.float_28 * this.yQsaxPkehn, text, this.bool_2);
                    num7 = num8;
                    num8 += this.float_5 * this.yQsaxPkehn;
                    float num10 = (float)gPoint.Z;
                    string text2 = num10.ToString("f3");
                    this.method_31(num6, num7 * this.float_28, num8 * this.float_28, 5, this.float_5 * this.float_28 * this.yQsaxPkehn, text2, this.bool_2);
                    num7 = this.float_19 - (this.float_3 + this.float_5);
                    num8 = this.float_19 - this.float_3;
                    string text3 = (num9 - num10).ToString("f3");
                    this.method_31(num6, num7 * this.float_28, num8 * this.float_28, 5, this.float_5 * this.float_28 * this.yQsaxPkehn, text3, this.bool_2);
                }
            }
            this.m_arrObjectRects.Clear();
            this.method_28(pArrPipLines);
            this.method_29(pArrPipePoints);
            this.method_25(pArrPipLines);
            this.method_33(pArrPipLines);
            this.method_36(pArrPipLines);
        }

        public void DrawTranSection(ArrayList pArrPipePoints)
        {
            int count = pArrPipePoints.Count;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)pArrPipePoints[i];
                if (pipePoint.PointType == PipePoint.SectionPointType.sptPipe)
                {
                    Pen pen = new Pen(Color.Black, this.OnePixelInMM);
                    pen.DashStyle = DashStyle.Dash;
                    double x = pipePoint.x;
                    double z = pipePoint.z;
                    float num = this.method_23(x);
                    float num2 = this.method_24(z);
                    float num3 = num;
                    float num4 = this.float_19 - this.float_3;
                    num *= this.float_28;
                    num2 *= this.float_28;
                    num3 *= this.float_28;
                    num4 *= this.float_28;
                    this.graphics_0.DrawLine(pen, new PointF(num, num2), new PointF(num3, num4));
                    double x2 = pipePoint.x;
                    float num5 = this.method_23(x2);
                    num5 *= this.float_28;
                    float num6 = this.float_19 - (this.float_3 + this.float_5 * (float)this.int_2 * this.yQsaxPkehn);
                    float num7 = num6 + this.float_5 * this.yQsaxPkehn;
                    float num8 = (float)pipePoint.m;
                    string text = num8.ToString("f3");
                    this.method_31(num5, num6 * this.float_28, num7 * this.float_28, 5, this.float_5 * this.float_28 * this.yQsaxPkehn, text, this.bool_2);
                    num6 = num7;
                    num7 += this.float_5 * this.yQsaxPkehn;
                    float num9 = (float)pipePoint.z;
                    string text2 = num9.ToString("f3");
                    this.method_31(num5, num6 * this.float_28, num7 * this.float_28, 5, this.float_5 * this.float_28 * this.yQsaxPkehn, text2, this.bool_2);
                    num6 = this.float_19 - (this.float_3 + this.float_5 * this.yQsaxPkehn);
                    num7 = this.float_19 - this.float_3;
                    string text3 = (num8 - num9).ToString("f3");
                    this.method_31(num5, num6 * this.float_28, num7 * this.float_28, 5, this.float_5 * this.float_28 * this.yQsaxPkehn, text3, this.bool_2);
                    num6 = this.float_19 - (this.float_3 + this.float_5 * 2f * this.yQsaxPkehn);
                    num7 = this.float_19 - this.float_3 - this.float_5 * this.yQsaxPkehn;
                    this.method_31(num5, num6 * this.float_28, num7 * this.float_28, 5, this.float_5 * this.float_28 * this.yQsaxPkehn, pipePoint.strPipeWidthHeight, this.bool_2);
                }
            }
            this.m_arrObjectRects.Clear();
            this.method_30(pArrPipePoints);
            this.method_26(pArrPipePoints);
            this.method_20(pArrPipePoints);
            this.method_19(pArrPipePoints);
            this.method_22(pArrPipePoints);
            this.method_21(pArrPipePoints);
            this.method_1();
            this.method_34(pArrPipePoints);
            this.method_18(pArrPipePoints);
            this.method_35(pArrPipePoints);
        }

        private void method_18(ArrayList arrayLists)
        {
            Font font = new Font("宋体", this.float_5 * this.float_28 / 6f, GraphicsUnit.Millimeter);
            int count = arrayLists.Count;
            Pen pen = new Pen(Color.Black, this.OnePixelInMM);
            StringFormat stringFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            PipePoint pipePoint = new PipePoint();
            bool flag = false;
            int num = 0;
            while (true)
            {
                if (num < count)
                {
                    PipePoint item = (PipePoint)arrayLists[num];
                    if (item.PointType == PipePoint.SectionPointType.sptMidRoadLine)
                    {
                        pipePoint = item;
                        flag = true;
                        break;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    break;
                }
            }
            if (flag)
            {
                for (int i = 0; i < count; i++)
                {
                    PipePoint item1 = (PipePoint)arrayLists[i];
                    if (item1.PointType == PipePoint.SectionPointType.sptPipe)
                    {
                        double float28 = item1.x;
                        float28 = (double)this.method_23(float28);
                        float single = (float)float28;
                        float float19 = this.float_19 - (this.float_3 + this.float_5 * (float)(this.int_2 - 2) * this.yQsaxPkehn - this.float_5 / 2f) * this.yQsaxPkehn;
                        single = single * this.float_28;
                        float19 = float19 * this.float_28;
                        float single1 = (float)Math.Abs(pipePoint.x - item1.x);
                        string str = single1.ToString("f3");
                        float28 = float28 * (double)this.float_28;
                        float float191 = this.float_19 - (this.float_3 + this.float_5 * 4f * this.yQsaxPkehn);
                        float float5 = float191 + this.float_5 * this.yQsaxPkehn;
                        this.method_32((float)float28, float191 * this.float_28, float5 * this.float_28, 5, this.float_5 * this.float_28 * this.yQsaxPkehn, str, Brushes.Red, this.bool_2);
                    }
                }
            }
        }

        private void method_19(ArrayList arrayList)
        {
            int count = arrayList.Count;
            int num = 0;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)arrayList[i];
                Color red = Color.Red;
                Pen pen = new Pen(red, this.OnePixelInMM);
                pen.DashStyle = DashStyle.Dash;
                pen.Width = 0.5f;
                if (pipePoint.PointType == PipePoint.SectionPointType.sptMidRoadLine)
                {
                    double num2 = pipePoint.x;
                    double num3 = pipePoint.m;
                    num2 = (double)this.method_23(num2);
                    num3 = (double)this.method_24(num3);
                    GPoint value = new GPoint(num2 * (double)this.float_28, num3 * (double)this.float_28);
                    this.m_arrObjectRects.Add(value);
                    float num4 = (float)num2;
                    float num5 = (float)(num3 - 5.0);
                    float num6 = (float)num2;
                    float num7 = this.float_19 - this.float_3;
                    num4 *= this.float_28;
                    num6 *= this.float_28;
                    num5 *= this.float_28;
                    num7 *= this.float_28;
                    this.graphics_0.DrawLine(pen, new PointF(num4, num5), new PointF(num6, num7));
                    num++;
                }
            }
        }

        private void method_20(ArrayList arrayList)
        {
            int count = arrayList.Count;
            int num = 0;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)arrayList[i];
                Color color = Color.FromArgb(pipePoint.Red, pipePoint.Green, pipePoint.Blue);
                Pen pen = new Pen(color, this.OnePixelInMM);
                pen.Width = 1f;
                if (pipePoint.PointType == PipePoint.SectionPointType.sptRoadBorder)
                {
                    double num2 = pipePoint.x;
                    double num3 = pipePoint.m;
                    num2 = (double)this.method_23(num2);
                    num3 = (double)this.method_24(num3);
                    GPoint value = new GPoint(num2 * (double)this.float_28, num3 * (double)this.float_28);
                    this.m_arrObjectRects.Add(value);
                    float num4 = (float)num2;
                    float num5 = (float)num3;
                    float num6 = (float)num2;
                    float num7 = (float)(num3 - 2.0);
                    num4 *= this.float_28;
                    num6 *= this.float_28;
                    num5 *= this.float_28;
                    num7 *= this.float_28;
                    this.graphics_0.DrawLine(pen, new PointF(num4, num5), new PointF(num6, num7));
                    pen = new Pen(Color.Black, this.OnePixelInMM);
                    pen.DashStyle = DashStyle.Dash;
                    num4 = (float)num2;
                    num5 = (float)num3;
                    num6 = (float)num2;
                    num7 = this.float_19 - this.float_3;
                    num4 *= this.float_28;
                    num6 *= this.float_28;
                    num5 *= this.float_28;
                    num7 *= this.float_28;
                    this.graphics_0.DrawLine(pen, new PointF(num4, num5), new PointF(num6, num7));
                    num++;
                }
            }
        }

        private void method_21(ArrayList arrayList)
        {
            int count = arrayList.Count;
            int num = 0;
            float num2 = 0f;
            float num3 = 0f;
            PipePoint.SectionPointType sectionPointType = PipePoint.SectionPointType.sptDrawPoint;
            bool flag = false;
            bool flag2 = false;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)arrayList[i];
                Color color = Color.FromArgb(128, 64, 0);
                Pen pen = new Pen(color, this.OnePixelInMM);
                pen.Width = 1f;
                if (pipePoint.PointType == PipePoint.SectionPointType.sptMidRoadLine)
                {
                    flag = true;
                }
                if (pipePoint.PointType == PipePoint.SectionPointType.sptDrawPoint || pipePoint.PointType == PipePoint.SectionPointType.sptRoadBorder)
                {
                    num++;
                    if (flag2 != flag)
                    {
                        num = 1;
                    }
                    double x = pipePoint.x;
                    double m = pipePoint.m;
                    if (num == 1)
                    {
                        num2 = this.method_23(x);
                        num3 = this.method_24(m);
                        sectionPointType = pipePoint.PointType;
                        flag2 = flag;
                    }
                    if (num == 2)
                    {
                        if (flag2 != flag)
                        {
                            flag2 = flag;
                            num = 0;
                        }
                        else if (sectionPointType == pipePoint.PointType)
                        {
                            flag2 = false;
                            num = 0;
                        }
                        else
                        {
                            num = 0;
                            float num4 = this.method_23(x);
                            this.method_24(m);
                            RectangleF rectangleF = default(RectangleF);
                            rectangleF.X = num2;
                            rectangleF.Y = num3 - 1f;
                            rectangleF.Width = num4 - num2;
                            rectangleF.Height = 2f;
                            this.method_10(ref rectangleF, (double)this.float_28);
                            GPoint value = new GPoint(x * (double)this.float_28, m * (double)this.float_28);
                            this.m_arrObjectRects.Add(value);
                            this.graphics_0.FillRectangle(Brushes.BurlyWood, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                            string str = Application.StartupPath + "\\断面要素\\";
                            string str2 = "tree.gif";
                            Image image = Image.FromFile(str + str2);
                            this.graphics_0.DrawImage(image, (num2 + num4) / 2f - 5f, num3 - 15f, 10f, 14f);
                        }
                    }
                }
            }
        }

        private void method_22(ArrayList arrayList)
        {
            int count = arrayList.Count;
            int num = 0;
            float num2 = 0f;
            float num3 = 0f;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)arrayList[i];
                Color color = Color.FromArgb(0, 255, 0);
                Pen pen = new Pen(color, this.OnePixelInMM);
                pen.Width = 1f;
                if (pipePoint.PointType == PipePoint.SectionPointType.sptMidGreen)
                {
                    num++;
                    double x = pipePoint.x;
                    double m = pipePoint.m;
                    if (num == 1)
                    {
                        num2 = this.method_23(x);
                        num3 = this.method_24(m);
                    }
                    if (num == 2)
                    {
                        num = 0;
                        float num4 = this.method_23(x);
                        this.method_24(m);
                        RectangleF rectangleF = default(RectangleF);
                        rectangleF.X = num2;
                        rectangleF.Y = num3 - 2f;
                        rectangleF.Width = num4 - num2;
                        rectangleF.Height = 2f;
                        this.method_10(ref rectangleF, (double)this.float_28);
                        GPoint value = new GPoint(x * (double)this.float_28, m * (double)this.float_28);
                        this.m_arrObjectRects.Add(value);
                        this.graphics_0.FillRectangle(Brushes.GreenYellow, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                        string str = Application.StartupPath + "\\断面要素\\";
                        string str2 = "smalltree.gif";
                        Image image = Image.FromFile(str + str2);
                        this.graphics_0.DrawImage(image, (num2 + num4) / 2f - 3f, num3 - 10f, 6f, 8f);
                    }
                }
            }
        }

        private float method_23(double num)
        {
            num *= 1000.0;
            float num2 = this.bool_0 ? this.float_26 : this.float_24;
            float num3 = (float)((num - this.double_0) / (double)num2);
            return num3 + this.float_14;
        }

        private float method_24(double num)
        {
            num *= 1000.0;
            float num2 = this.bool_0 ? this.float_27 : this.float_25;
            float num3 = (float)((num - this.double_2) / (double)num2);
            return this.float_19 - (this.float_3 + this.float_5 * (float)this.int_2 * this.yQsaxPkehn + num3);
        }

        private void method_25(ArrayList arrayList)
        {
            int count = arrayList.Count;
            Pen pen = new Pen(Color.Black, this.OnePixelInMM);
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine)arrayList[i];
                float num = 0f;
                float num2 = 0f;
                int num3 = pipeLine.Size();
                for (int j = 0; j < num3; j++)
                {
                    GPoint deepCopy = pipeLine[j].GetDeepCopy();
                    double x = deepCopy.X;
                    double m = deepCopy.M;
                    float num4 = this.method_23(x);
                    float num5 = this.method_24(m);
                    if (j > 0)
                    {
                        this.graphics_0.DrawLine(pen, new PointF(num * this.float_28, num2 * this.float_28), new PointF(num4 * this.float_28, num5 * this.float_28));
                        this.method_27(new GPoint((double)(num * this.float_28), (double)(num2 * this.float_28)), new GPoint((double)(num4 * this.float_28), (double)(num5 * this.float_28)));
                    }
                    num = num4;
                    num2 = num5;
                }
            }
        }

        private void method_26(ArrayList arrayList)
        {
            int count = arrayList.Count;
            Pen pen = new Pen(Color.Black, this.OnePixelInMM);
            float num = 0f;
            float num2 = 0f;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)arrayList[i];
                double x = pipePoint.x;
                double m = pipePoint.m;
                float num3 = this.method_23(x);
                float num4 = this.method_24(m);
                if (i > 0)
                {
                    this.graphics_0.DrawLine(pen, new PointF(num * this.float_28, num2 * this.float_28), new PointF(num3 * this.float_28, num4 * this.float_28));
                    this.method_27(new GPoint((double)(num * this.float_28), (double)(num2 * this.float_28)), new GPoint((double)(num3 * this.float_28), (double)(num4 * this.float_28)));
                }
                num = num3;
                num2 = num4;
            }
        }

        private void method_27(GPoint deepCopy, GPoint deepCopy2)
        {
            if (deepCopy.DistanceToPt(deepCopy2) >= 1.0)
            {
                if (deepCopy.X > deepCopy2.X)
                {
                    GPoint deepCopy3 = deepCopy.GetDeepCopy();
                    deepCopy = deepCopy2.GetDeepCopy();
                    deepCopy2 = deepCopy3.GetDeepCopy();
                }
                double dLen = (double)(this.float_15 / 100f * this.float_28);
                double num = 135.0;
                double angleToPt = deepCopy.GetAngleToPt(deepCopy2);
                double dLen2 = (double)(this.float_15 / 50f * this.float_28);
                GPoint deepCopy4 = deepCopy.GetDeepCopy();
                GPoint deepCopy5 = deepCopy.GetDeepCopy();
                while (true)
                {
                    deepCopy4.OffSetByLen(angleToPt, dLen2);
                    if (deepCopy4.DistanceToPt(deepCopy) >= deepCopy.DistanceToPt(deepCopy2))
                    {
                        break;
                    }
                    deepCopy5 = deepCopy4.GetDeepCopy();
                    deepCopy5.OffSetByLen(angleToPt + num, dLen);
                    Pen pen = new Pen(Color.Black, this.OnePixelInMM);
                    this.graphics_0.DrawLine(pen, new PointF((float)deepCopy5.X, (float)deepCopy5.Y), new PointF((float)deepCopy4.X, (float)deepCopy4.Y));
                }
            }
        }

        private void method_28(ArrayList arrayList)
        {
            int count = arrayList.Count;
            Pen pen = new Pen(Color.Red, this.OnePixelInMM);
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine)arrayList[i];
                Color color = Color.FromArgb(pipeLine.Red, pipeLine.Green, pipeLine.Blue);
                Pen pen2 = new Pen(color, this.OnePixelInMM);
                Pen pen3;
                if (this.m_nSelectIndex < count && this.m_nSelectIndex == i)
                {
                    pen3 = pen;
                }
                else
                {
                    pen3 = pen2;
                }
                float num = 0f;
                float num2 = 0f;
                GPolyLine gPolyLine = new GPolyLine();
                int num3 = pipeLine.Size();
                for (int j = 0; j < num3; j++)
                {
                    GPoint deepCopy = pipeLine[j].GetDeepCopy();
                    double x = deepCopy.X;
                    double z = deepCopy.Z;
                    float num4 = this.method_23(x);
                    float num5 = this.method_24(z);
                    if (j > 0)
                    {
                        this.graphics_0.DrawLine(pen3, new PointF(num * this.float_28, num2 * this.float_28), new PointF(num4 * this.float_28, num5 * this.float_28));
                    }
                    num = num4;
                    num2 = num5;
                    gPolyLine.PushBack((double)(num4 * this.float_28), (double)(num5 * this.float_28));
                }
                this.m_arrObjectRects.Add(gPolyLine);
            }
        }

        private void method_29(ArrayList arrayList)
        {
            int count = arrayList.Count;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)arrayList[i];
                Color color = Color.FromArgb(pipePoint.Red, pipePoint.Green, pipePoint.Blue);
                Pen pen = new Pen(color, this.OnePixelInMM);
                if (pipePoint.PointType == PipePoint.SectionPointType.sptPipe)
                {
                    double x = pipePoint.x;
                    double z = pipePoint.z;
                    float num = this.method_23(x);
                    float num2 = this.method_24(z);
                    RectangleF rectangleF = default(RectangleF);
                    rectangleF.X = num;
                    rectangleF.Y = num2;
                    rectangleF.Width = 0f;
                    rectangleF.Height = 0f;
                    rectangleF.Inflate(1.5f, 1.5f);
                    this.method_10(ref rectangleF, (double)this.float_28);
                    PointF pointF = new PointF(num - 1.5f, num2 - 1.5f);
                    this.method_9(ref pointF, (double)this.float_28);
                    GPoint value = new GPoint((double)num * (double)this.float_28, (double)num2 * (double)this.float_28);
                    this.m_arrObjectRects.Add(value);
                    if (this.m_nSelectIndex >= count - 1 && this.m_nSelectIndex == count - 1 + i)
                    {
                        if (pipePoint.strPipeWidthHeight.IndexOf('x') == -1 && pipePoint.strPipeWidthHeight.IndexOf('X') == -1)
                        {
                            this.graphics_0.FillEllipse(Brushes.Red, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                        }
                        else
                        {
                            this.graphics_0.FillRectangle(Brushes.Red, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                        }
                    }
                    else if (pipePoint.strPipeWidthHeight.IndexOf('x') == -1 && pipePoint.strPipeWidthHeight.IndexOf('X') == -1)
                    {
                        this.graphics_0.FillEllipse(Brushes.White, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    }
                    else
                    {
                        this.graphics_0.FillRectangle(Brushes.White, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    }
                    if (pipePoint.strPipeWidthHeight.IndexOf('x') == -1 && pipePoint.strPipeWidthHeight.IndexOf('X') == -1)
                    {
                        this.graphics_0.DrawEllipse(pen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    }
                    else
                    {
                        this.graphics_0.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    }
                }
            }
        }

        private void method_30(ArrayList arrayList)
        {
            int count = arrayList.Count;
            int num = 0;
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint = (PipePoint)arrayList[i];
                Color color = Color.FromArgb(pipePoint.Red, pipePoint.Green, pipePoint.Blue);
                Pen pen = new Pen(color, this.OnePixelInMM);
                if (pipePoint.PointType == PipePoint.SectionPointType.sptPipe)
                {
                    double x = pipePoint.x;
                    double z = pipePoint.z;
                    float num2 = this.method_23(x);
                    float num3 = this.method_24(z);
                    RectangleF rectangleF = default(RectangleF);
                    rectangleF.X = num2;
                    rectangleF.Y = num3;
                    rectangleF.Width = 0f;
                    rectangleF.Height = 0f;
                    rectangleF.Inflate(1.5f, 1.5f);
                    this.method_10(ref rectangleF, (double)this.float_28);
                    GPoint value = new GPoint((double)num2 * (double)this.float_28, (double)num3 * (double)this.float_28);
                    this.m_arrObjectRects.Add(value);
                    if (this.m_nSelectIndex == num)
                    {
                        if (pipePoint.strPipeWidthHeight.IndexOf('x') == -1 && pipePoint.strPipeWidthHeight.IndexOf('X') == -1)
                        {
                            this.graphics_0.FillEllipse(Brushes.Red, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                        }
                        else
                        {
                            this.graphics_0.FillRectangle(Brushes.Red, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                        }
                    }
                    else if (pipePoint.strPipeWidthHeight.IndexOf('x') == -1 && pipePoint.strPipeWidthHeight.IndexOf('X') == -1)
                    {
                        this.graphics_0.FillEllipse(Brushes.White, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    }
                    else
                    {
                        this.graphics_0.FillRectangle(Brushes.White, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    }
                    if (pipePoint.strPipeWidthHeight.IndexOf('x') == -1 && pipePoint.strPipeWidthHeight.IndexOf('X') == -1)
                    {
                        this.graphics_0.DrawEllipse(pen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    }
                    else
                    {
                        this.graphics_0.DrawRectangle(pen, rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
                    }
                    num++;
                }
            }
        }

        private void method_31(float dx, float num, float num2, int num3, float num4, string s, bool flag)
        {
            Font font;
            if (flag)
            {
                font = new Font("宋体", num4 / (float)num3, GraphicsUnit.Millimeter);
            }
            else
            {
                font = new Font("宋体", num4 / (float)num3 * 1.5f, GraphicsUnit.Millimeter);
            }
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Far;
            float dy = (num + num2) / 2f;
            GraphicsContainer container = this.graphics_0.BeginContainer();
            this.graphics_0.PageUnit = GraphicsUnit.Millimeter;
            this.graphics_0.TranslateTransform(dx, dy);
            if (flag)
            {
                this.graphics_0.RotateTransform(270f);
            }
            this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(0f, 0f), stringFormat);
            this.graphics_0.EndContainer(container);
        }

        private void method_32(float dx, float num, float num2, int num3, float num4, string s, Brush brush, bool flag)
        {
            Font font;
            if (flag)
            {
                font = new Font("宋体", num4 / (float)num3, GraphicsUnit.Millimeter);
            }
            else
            {
                font = new Font("宋体", num4 / (float)num3 * 1.5f, GraphicsUnit.Millimeter);
            }
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Far;
            float dy = (num + num2) / 2f;
            GraphicsContainer container = this.graphics_0.BeginContainer();
            this.graphics_0.PageUnit = GraphicsUnit.Millimeter;
            this.graphics_0.TranslateTransform(dx, dy);
            if (flag)
            {
                this.graphics_0.RotateTransform(270f);
            }
            this.graphics_0.DrawString(s, font, brush, new PointF(0f, 0f), stringFormat);
            this.graphics_0.EndContainer(container);
        }

        private void method_33(ArrayList arrayList)
        {
            Font font = new Font("宋体", this.float_5 * this.float_28 / 3f * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            int count = arrayList.Count;
            new Pen(Color.Black, this.OnePixelInMM);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine)arrayList[i];
                int num = pipeLine.Size();
                float num2 = 0f;
                GPoint gPoint = new GPoint();
                for (int j = 0; j < num; j++)
                {
                    GPoint deepCopy = pipeLine[j].GetDeepCopy();
                    double x = deepCopy.X;
                    float num3 = this.method_23(x);
                    if (j > 0)
                    {
                        float num4 = (num3 + num2) / 2f;
                        float num5 = this.float_19 - (this.float_3 + this.float_5 * (float)(this.int_2 - 3) * this.yQsaxPkehn - this.float_5 / 2f * this.yQsaxPkehn);
                        num4 *= this.float_28;
                        num5 *= this.float_28;
                        string s = ((float)Math.Abs(gPoint.X - deepCopy.X)).ToString("f3");
                        if (this.bool_1)
                        {
                            if (i % 2 == 0)
                            {
                                this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(num4, num5 - 2f), stringFormat);
                            }
                            else
                            {
                                this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(num4, num5 + 2f), stringFormat);
                            }
                        }
                        else
                        {
                            this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(num4, num5), stringFormat);
                        }
                    }
                    gPoint = deepCopy.GetDeepCopy();
                    num2 = num3;
                }
            }
        }

        private void method_34(ArrayList arrayList)
        {
            Font font = new Font("宋体", this.float_5 * this.float_28 / 3f * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            int count = arrayList.Count;
            new Pen(Color.Black, this.OnePixelInMM);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            int num = 0;
            float num2 = 0f;
            PipePoint pipePoint = new PipePoint();
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint2 = (PipePoint)arrayList[i];
                if (pipePoint2.PointType == PipePoint.SectionPointType.sptPipe || pipePoint2.PointType == PipePoint.SectionPointType.sptMidRoadLine || pipePoint2.PointType == PipePoint.SectionPointType.sptRoadBorder)
                {
                    double x = pipePoint2.x;
                    float num3 = this.method_23(x);
                    if (num == 0)
                    {
                        num2 = num3;
                        pipePoint = pipePoint2;
                        num++;
                    }
                    else
                    {
                        float num4 = (num3 + num2) / 2f;
                        float num5 = this.float_19 - (this.float_3 + this.float_5 * (float)(this.int_2 - 3) * this.yQsaxPkehn - this.float_5 / 2f * this.yQsaxPkehn);
                        num4 *= this.float_28;
                        num5 *= this.float_28;
                        string s = ((float)Math.Abs(pipePoint.x - pipePoint2.x)).ToString("f3");
                        if (this.bool_1)
                        {
                            if (i % 2 == 0)
                            {
                                this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(num4, num5 - 2f), stringFormat);
                            }
                            else
                            {
                                this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(num4, num5 + 2f), stringFormat);
                            }
                        }
                        else
                        {
                            this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(num4, num5), stringFormat);
                        }
                        num2 = num3;
                        pipePoint = pipePoint2;
                    }
                }
            }
        }

        private void method_35(ArrayList arrayList)
        {
            Font font = new Font("宋体", this.float_5 * this.float_28 / 4f, GraphicsUnit.Millimeter);
            int count = arrayList.Count;
            new Pen(Color.Black, this.OnePixelInMM);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Far;
            PipePoint pipePoint = new PipePoint();
            for (int i = 0; i < count; i++)
            {
                PipePoint pipePoint2 = (PipePoint)arrayList[i];
                if (pipePoint2.PointType == PipePoint.SectionPointType.sptPipe)
                {
                    double x = pipePoint2.x;
                    float num = this.method_23(x);
                    float num2 = this.float_19 - (this.float_3 + this.float_5 * (float)this.int_2 + this.float_5 / 2f);
                    num *= this.float_28;
                    num2 *= this.float_28;
                    pipePoint.DistanceToPipePoint(pipePoint2);
                    string s = this.method_0(pipePoint2.bstrDatasetName);
                    this.graphics_0.DrawString(s, font, Brushes.Black, new PointF(num, num2), stringFormat);
                }
            }
        }

        private void method_36(ArrayList arrayList)
        {
            Font font = new Font("宋体", this.float_5 * this.float_28 / 3f * this.yQsaxPkehn, GraphicsUnit.Millimeter);
            int count = arrayList.Count;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < count; i++)
            {
                PipeLine pipeLine = (PipeLine)arrayList[i];
                int num = pipeLine.Size();
                double x = pipeLine[0].X;
                double x2 = pipeLine[num - 1].X;
                double num2 = (x + x2) / 2.0;
                float num3 = this.method_23(num2);
                float num4 = this.float_19 - (this.float_3 + this.float_5 * ((float)this.int_2 - 4.5f) * this.yQsaxPkehn);
                num3 *= this.float_28;
                num4 *= this.float_28;
                string pipeWidthHeight = pipeLine.PipeWidthHeight;
                this.graphics_0.DrawString(pipeWidthHeight, font, Brushes.Black, new PointF(num3, num4), stringFormat);
            }
        }

        public int GetSelectIndex(Point ptMouseDown)
        {
            int nSelectIndex = -1;
            int count = this.m_arrObjectRects.Count;
            PointF[] array = new PointF[1];
            array[0].X = (float)ptMouseDown.X;
            array[0].Y = (float)ptMouseDown.Y;
            this.graphics_0.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, array);
            for (int i = 0; i < count; i++)
            {
                object obj = this.m_arrObjectRects[i];
                if (obj is GPoint)
                {
                    GPoint gPoint = (GPoint)obj;
                    double num = gPoint.DistanceToPt((double)array[0].X, (double)array[0].Y);
                    if (num < (double)(1.5f * this.float_28))
                    {
                        nSelectIndex = i;
                    }
                }
                else
                {
                    GPolyLine gPolyLine = (GPolyLine)this.m_arrObjectRects[i];
                    double num = gPolyLine.GetMinDistanceToPt(new GPoint((double)array[0].X, (double)array[0].Y));
                    if (num < (double)(1f * this.float_28))
                    {
                        nSelectIndex = i;
                    }
                }
            }
            this.m_nSelectIndex = nSelectIndex;
            if (this.m_nSelectIndex >= 0)
            {
                this.form_0.Invalidate();
            }
            return this.m_nSelectIndex;
        }

        public void method_37()
        {
            string str = Application.StartupPath + "\\断面截图\\";
            string str2 = "截图0.jpg";
            SectionDisp.FromGraphics(this.graphics_0, this.form_0.Width - 10, this.form_0.Height - 50).Save(str + str2);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "jpg文件|*.jpg";
            saveFileDialog.Title = "保存截面截图";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(str + str2, saveFileDialog.FileName);
            }
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int BitBlt(HandleRef hDesDC, int x, int y, int w, int h, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

        public static Bitmap FromGraphics(Graphics g, int w, int h)
        {
            Bitmap bitmap = new Bitmap(w, h);
            Graphics graphics = Graphics.FromImage(bitmap);
            SectionDisp.BitBlt(new HandleRef(null, graphics.GetHdc()), 0, 0, w, h, new HandleRef(null, g.GetHdc()), 0, 25, 13369376);
            graphics.ReleaseHdc();
            g.ReleaseHdc();
            return bitmap;
        }
    }
}
