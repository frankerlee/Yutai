using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.ArcGIS.Common.DXF;

namespace Yutai.ArcGIS.Common.Meta
{
    public class MetaForm
    {
        private static int b;

        private static int cn_obj;

        private static DXFData Data;

        private static int g;

        private static int hatch_style;

        private static int imgHeight;

        private static int imgWidth;

        private static byte line_style;

        protected Metafile mf;

        public Graphics mfGraph;

        private static Metafile mfImage;

        public string mfName;

        public MemoryStream mfStream;

        private static float obj_width;

        private static int r;

        private readonly static float rad;

        private static float text_size;

        private static byte text_style;

        private static string tmpName;

        private static bool transform;

        private static PointF transPoint;

        private static DXFExport vDXF;

        static MetaForm()
        {
            MetaForm.mfImage = null;
            MetaForm.obj_width = 0f;
            MetaForm.r = 0;
            MetaForm.g = 0;
            MetaForm.b = 0;
            MetaForm.tmpName = "";
            MetaForm.imgWidth = 0;
            MetaForm.imgHeight = 0;
            MetaForm.text_size = 0f;
            MetaForm.rad = 0.01745329f;
            MetaForm.line_style = 0;
            MetaForm.text_style = 0;
            MetaForm.hatch_style = -1;
            MetaForm.cn_obj = 0;
            MetaForm.transform = false;
            MetaForm.transPoint = new PointF();
        }

        public MetaForm(Form aForm, EmfType mfType, bool draw)
        {
            this.mf = null;
            this.mfStream = null;
            this.mfStream = new MemoryStream();
            Graphics graphic = aForm.CreateGraphics();
            IntPtr hdc = graphic.GetHdc();
            this.mf = new Metafile(this.mfStream, hdc, new Rectangle(0, 0, aForm.Width, aForm.Height),
                MetafileFrameUnit.Pixel, mfType);
            graphic.ReleaseHdc(hdc);
            graphic.Dispose();
            this.mfGraph = Graphics.FromImage(this.mf);
            if (draw)
            {
                aForm.Paint += new PaintEventHandler(this.DrawMetaFromStream);
                aForm.Invalidate();
            }
        }

        public MetaForm(string aName, Form aForm, int Width, int Height, EmfType mfType, bool draw)
        {
            this.mf = null;
            this.mfStream = null;
            Graphics graphic = aForm.CreateGraphics();
            IntPtr hdc = graphic.GetHdc();
            if (File.Exists(aName))
            {
                File.Delete(aName);
            }
            this.mf = new Metafile(aName, hdc, new Rectangle(0, 0, Width, Height), MetafileFrameUnit.Pixel, mfType);
            graphic.ReleaseHdc(hdc);
            graphic.Dispose();
            this.mfGraph = Graphics.FromImage(this.mf);
            this.mfName = aName;
            if (draw)
            {
                aForm.Paint += new PaintEventHandler(this.Draw_Meta);
                aForm.Invalidate();
            }
        }

        private static void AddObjectsPath(ArrayList arr)
        {
            int j;
            int count;
            for (int i = 0; i < arr.Count; i++)
            {
                switch ((arr[i] as MetaForm.ObjectEnt).type)
                {
                    case MetaForm.ObjTypeRecord.objCloseLine:
                    {
                        count = (arr[i] as MetaForm.ObjectEnt).points.Count - 1;
                        for (j = 0; j <= count; j++)
                        {
                            if (j < count)
                            {
                                MetaForm.DrawLine((PointF) (arr[i] as MetaForm.ObjectEnt).points[j],
                                    (PointF) (arr[i] as MetaForm.ObjectEnt).points[j + 1]);
                            }
                        }
                        j--;
                        if ((arr[i] as MetaForm.ObjectEnt).compobj != -1)
                        {
                            int item = (arr[i] as MetaForm.ObjectEnt).compobj;
                            int num = i;
                            while ((arr[num] as MetaForm.ObjectEnt).compobj != -1)
                            {
                                item = (arr[num] as MetaForm.ObjectEnt).compobj;
                                num--;
                            }
                            if (j > count)
                            {
                                break;
                            }
                            MetaForm.DrawLine((PointF) (arr[i] as MetaForm.ObjectEnt).points[j],
                                (PointF) (arr[item] as MetaForm.ObjectEnt).points[0]);
                            break;
                        }
                        else
                        {
                            if (j > count)
                            {
                                break;
                            }
                            MetaForm.DrawLine((PointF) (arr[i] as MetaForm.ObjectEnt).points[j],
                                (PointF) (arr[i] as MetaForm.ObjectEnt).points[0]);
                            break;
                        }
                    }
                    case MetaForm.ObjTypeRecord.objCloseBezier:
                    {
                        MetaForm.AddPolyBez((arr[i] as MetaForm.ObjectEnt).points);
                        j = (arr[i] as MetaForm.ObjectEnt).points.Count - 1;
                        if ((arr[i] as MetaForm.ObjectEnt).compobj != -1)
                        {
                            int item1 = (arr[i] as MetaForm.ObjectEnt).compobj;
                            int num1 = i;
                            while ((arr[num1] as MetaForm.ObjectEnt).compobj != -1)
                            {
                                item1 = (arr[num1] as MetaForm.ObjectEnt).compobj;
                                num1--;
                            }
                            MetaForm.DrawLine((PointF) (arr[i] as MetaForm.ObjectEnt).points[j],
                                (PointF) (arr[item1] as MetaForm.ObjectEnt).points[0]);
                            break;
                        }
                        else
                        {
                            MetaForm.DrawLine((PointF) (arr[i] as MetaForm.ObjectEnt).points[j],
                                (PointF) (arr[i] as MetaForm.ObjectEnt).points[0]);
                            break;
                        }
                    }
                    case MetaForm.ObjTypeRecord.objBezier:
                    {
                        MetaForm.AddPolyBez((arr[i] as MetaForm.ObjectEnt).points);
                        break;
                    }
                    case MetaForm.ObjTypeRecord.objLine:
                    {
                        count = (arr[i] as MetaForm.ObjectEnt).points.Count - 1;
                        for (j = 0; j <= count; j++)
                        {
                            if (j < count)
                            {
                                MetaForm.DrawLine((PointF) (arr[i] as MetaForm.ObjectEnt).points[j],
                                    (PointF) (arr[i] as MetaForm.ObjectEnt).points[j + 1]);
                            }
                        }
                        break;
                    }
                }
            }
        }

        private static void AddPolyBez(ArrayList obj)
        {
            MetaForm.Data.Clear();
            MetaForm.Data.count = obj.Count;
            Color color = Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b);
            MetaForm.Data.color = DXFExport.ColorToDXF(color);
            MetaForm.Data.points = new ArrayList();
            MetaForm.Data.points.Add(new ArrayList());
            for (int i = 0; i < obj.Count; i++)
            {
                DXFPoint dXFPoint = new DXFPoint();
                PointF item = (PointF) obj[i];
                dXFPoint.X = item.X;
                item = (PointF) obj[i];
                dXFPoint.Y = -item.Y + (float) MetaForm.imgHeight;
                dXFPoint.Z = 0f;
                if (MetaForm.transform)
                {
                    DXFPoint x = dXFPoint;
                    x.X = x.X + MetaForm.transPoint.X;
                    DXFPoint y = dXFPoint;
                    y.Y = y.Y + MetaForm.transPoint.Y;
                }
                ((ArrayList) MetaForm.Data.points[0]).Add(dXFPoint);
            }
            MetaForm.vDXF.AddPolyBezier(MetaForm.Data, 0);
        }

        public bool Clear_Paint(Form aForm)
        {
            if (this.mfStream == null)
            {
                aForm.Paint -= new PaintEventHandler(this.Draw_Meta);
                aForm.Invalidate();
            }
            else
            {
                aForm.Paint -= new PaintEventHandler(this.DrawMetaFromStream);
                aForm.Invalidate();
            }
            return true;
        }

        public static float Conversion_Angle(float Val)
        {
            while (Val > 0f)
            {
                Val = Val - 360f;
            }
            return Val;
        }

        private static bool DelFunc(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr recordData,
            PlayRecordCallback callbackData)
        {
            byte[] numArray = new byte[dataSize];
            if (recordData != IntPtr.Zero)
            {
                Marshal.Copy(recordData, numArray, 0, dataSize);
            }
            EmfPlusRecordType emfPlusRecordType = recordType;
            if (emfPlusRecordType <= EmfPlusRecordType.DrawArc)
            {
                if (emfPlusRecordType == EmfPlusRecordType.EmfHeader)
                {
                    MetaForm.imgWidth = MetaForm.EmfWidthToWidth((int) numArray[24], (int) numArray[25]);
                    MetaForm.imgHeight = MetaForm.EmfWidthToWidth((int) numArray[28], (int) numArray[29]);
                    MetaForm.cn_obj = (int) MetaForm.Format_Size2(numArray[41], numArray[42]);
                }
                else
                {
                    switch (emfPlusRecordType)
                    {
                        case EmfPlusRecordType.Object:
                        {
                            if ((int) numArray.Length >= 32 || numArray[4] > 1)
                            {
                                if (numArray[4] != 0)
                                {
                                    MetaForm.SplitRecObject(numArray);
                                    return true;
                                }
                                if ((int) numArray.Length == 32)
                                {
                                    MetaForm.r = numArray[30];
                                    MetaForm.g = numArray[29];
                                    MetaForm.b = numArray[28];
                                }
                                if ((int) numArray.Length > 32)
                                {
                                    MetaForm.r = numArray[34];
                                    MetaForm.g = numArray[33];
                                    MetaForm.b = numArray[32];
                                }
                                if (numArray[8] == 0)
                                {
                                    MetaForm.line_style = 0;
                                }
                                else
                                {
                                    MetaForm.line_style = numArray[20];
                                }
                                MetaForm.text_size = MetaForm.FormatSize((int) numArray[6], (int) numArray[7]);
                                MetaForm.obj_width = MetaForm.FormatSize((int) numArray[18], (int) numArray[19]);
                                MetaForm.text_style = numArray[12];
                                break;
                            }
                            else
                            {
                                MetaForm.r = numArray[14];
                                MetaForm.g = numArray[13];
                                MetaForm.b = numArray[12];
                                MetaForm.hatch_style = numArray[8];
                                break;
                            }
                        }
                        case EmfPlusRecordType.Clear:
                        case EmfPlusRecordType.FillPie:
                        case EmfPlusRecordType.DrawPie:
                        {
                            return true;
                        }
                        case EmfPlusRecordType.FillRects:
                        {
                            if (numArray[3] == 255)
                            {
                                MetaForm.r = numArray[2];
                                MetaForm.g = numArray[1];
                                MetaForm.b = numArray[0];
                            }
                            MetaForm.DrawRectangle(numArray, 8, true);
                            MetaForm.hatch_style = -1;
                            break;
                        }
                        case EmfPlusRecordType.DrawRects:
                        {
                            MetaForm.DrawRectangle(numArray, 4, false);
                            break;
                        }
                        case EmfPlusRecordType.FillPolygon:
                        {
                            MetaForm.DrawPolyLine(numArray, 4, true, false);
                            MetaForm.hatch_style = -1;
                            break;
                        }
                        case EmfPlusRecordType.DrawLines:
                        {
                            if (numArray[0] != 2)
                            {
                                MetaForm.DrawPolyLine(numArray, 0, false, false);
                                break;
                            }
                            else
                            {
                                MetaForm.DrawLine(numArray, 4);
                                break;
                            }
                        }
                        case EmfPlusRecordType.FillEllipse:
                        {
                            if (numArray[3] != 0)
                            {
                                MetaForm.r = numArray[2];
                                MetaForm.g = numArray[1];
                                MetaForm.b = numArray[0];
                            }
                            if (numArray[8] != numArray[10])
                            {
                                MetaForm.DrawEllipse(numArray, 4, true);
                            }
                            else
                            {
                                MetaForm.DrawCircle(numArray, 4, true);
                            }
                            MetaForm.hatch_style = -1;
                            break;
                        }
                        case EmfPlusRecordType.DrawEllipse:
                        {
                            if (numArray[4] != numArray[6])
                            {
                                MetaForm.DrawEllipse(numArray, 0, false);
                                break;
                            }
                            else
                            {
                                MetaForm.DrawCircle(numArray, 0, false);
                                break;
                            }
                        }
                        case EmfPlusRecordType.DrawArc:
                        {
                            if (numArray[12] != numArray[14])
                            {
                                MetaForm.DrawEllArc(numArray);
                                break;
                            }
                            else
                            {
                                MetaForm.DrawArc(numArray);
                                break;
                            }
                        }
                        default:
                        {
                            return true;
                        }
                    }
                }
            }
            else if (emfPlusRecordType == EmfPlusRecordType.DrawBeziers)
            {
                MetaForm.DrawPolyLine(numArray, 0, false, true);
            }
            else if (emfPlusRecordType == EmfPlusRecordType.DrawString)
            {
                MetaForm.r = numArray[2];
                MetaForm.g = numArray[1];
                MetaForm.b = numArray[0];
                MetaForm.DrawString(numArray);
            }
            else
            {
                switch (emfPlusRecordType)
                {
                    case EmfPlusRecordType.ResetWorldTransform:
                    {
                        MetaForm.transform = false;
                        break;
                    }
                    case EmfPlusRecordType.MultiplyWorldTransform:
                    {
                        return true;
                    }
                    case EmfPlusRecordType.TranslateWorldTransform:
                    {
                        MetaForm.transform = true;
                        MetaForm.transPoint.X = MetaForm.FormatSize((int) numArray[2], (int) numArray[3]);
                        MetaForm.transPoint.Y = MetaForm.FormatSize((int) numArray[6], (int) numArray[7]);
                        break;
                    }
                    default:
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        private void Draw_Meta(object sender, PaintEventArgs e)
        {
            if (this.mfName != null && File.Exists(this.mfName))
            {
                Image metafile = new Metafile(this.mfName);
                e.Graphics.DrawImage(metafile, 0, 0);
                metafile.Dispose();
            }
        }

        private static void DrawArc(byte[] arr)
        {
            float single = MetaForm.FormatSize((int) arr[2], (int) arr[3]);
            float single1 = MetaForm.FormatSize((int) arr[6], (int) arr[7]);
            float x = MetaForm.Format_Size2(arr[8], arr[9]);
            float y = MetaForm.Format_Size2(arr[10], arr[11]);
            float single2 = MetaForm.Format_Size2(arr[12], arr[13]);
            float single3 = MetaForm.Format_Size2(arr[14], arr[15]);
            if (MetaForm.transform)
            {
                x = x + MetaForm.transPoint.X;
                y = y + MetaForm.transPoint.Y;
            }
            MetaForm.Data.Clear();
            MetaForm.SetLineStyle((int) MetaForm.line_style);
            MetaForm.Data.thickness = MetaForm.obj_width;
            MetaForm.Data.color = DXFExport.ColorToDXF(Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b));
            MetaForm.Data.point.X = x;
            MetaForm.Data.point.Y = -y + (float) MetaForm.imgHeight;
            MetaForm.Data.point1.X = single2 + x;
            MetaForm.Data.point1.Y = -single3 - y + (float) MetaForm.imgHeight;
            MetaForm.Data.radius = (MetaForm.Data.point1.X - MetaForm.Data.point.X)/2f;
            DXFPoint data = MetaForm.Data.point;
            data.Y = data.Y - MetaForm.Data.radius;
            DXFPoint dXFPoint = MetaForm.Data.point1;
            dXFPoint.Y = dXFPoint.Y - MetaForm.Data.radius;
            DXFPoint data1 = MetaForm.Data.point;
            data1.X = data1.X + MetaForm.Data.radius;
            DXFPoint x1 = MetaForm.Data.point1;
            x1.X = x1.X + MetaForm.Data.radius;
            MetaForm.Data.startAngle = -single1 - single;
            MetaForm.Data.endAngle = -single;
            MetaForm.Data.selfType = 0;
            MetaForm.vDXF.AddArc(MetaForm.Data);
        }

        private static void DrawCircle(byte[] arr, byte cn, bool hatch)
        {
            float x = MetaForm.Format_Size2(arr[cn], arr[cn + 1]);
            float y = MetaForm.Format_Size2(arr[cn + 2], arr[cn + 3]);
            float single = MetaForm.Format_Size2(arr[cn + 4], arr[cn + 5]);
            float single1 = MetaForm.Format_Size2(arr[cn + 6], arr[cn + 7]);
            if (MetaForm.transform)
            {
                x = x + MetaForm.transPoint.X;
                y = y + MetaForm.transPoint.Y;
            }
            MetaForm.Data.Clear();
            MetaForm.Data.color = DXFExport.ColorToDXF(Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b));
            MetaForm.Data.thickness = MetaForm.obj_width;
            MetaForm.Data.point.X = x;
            MetaForm.Data.point.Y = -y + (float) MetaForm.imgHeight;
            MetaForm.Data.point1.X = single + x;
            MetaForm.Data.point1.Y = -single1 - y + (float) MetaForm.imgHeight;
            MetaForm.Data.radius = (MetaForm.Data.point1.X - MetaForm.Data.point.X)/2f;
            DXFPoint data = MetaForm.Data.point;
            data.Y = data.Y - MetaForm.Data.radius;
            DXFPoint dXFPoint = MetaForm.Data.point1;
            dXFPoint.Y = dXFPoint.Y - MetaForm.Data.radius;
            DXFPoint data1 = MetaForm.Data.point;
            data1.X = data1.X + MetaForm.Data.radius;
            DXFPoint x1 = MetaForm.Data.point1;
            x1.X = x1.X + MetaForm.Data.radius;
            MetaForm.SetLineStyle((int) MetaForm.line_style);
            if (!hatch)
            {
                MetaForm.vDXF.AddCircle(MetaForm.Data);
                return;
            }
            MetaForm.Data.startAngle = 0f;
            MetaForm.Data.endAngle = 360f;
            MetaForm.SetHatchStyle(MetaForm.hatch_style);
            MetaForm.Data.selfType = 1;
            MetaForm.vDXF.AddHatch(MetaForm.Data);
        }

        private static void DrawEllArc(byte[] arr)
        {
            float single = MetaForm.FormatSize((int) arr[2], (int) arr[3]);
            float single1 = MetaForm.FormatSize((int) arr[6], (int) arr[7]);
            float x = MetaForm.Format_Size2(arr[8], arr[9]);
            float y = MetaForm.Format_Size2(arr[10], arr[11]);
            float single2 = MetaForm.Format_Size2(arr[12], arr[13]);
            float single3 = MetaForm.Format_Size2(arr[14], arr[15]);
            if (MetaForm.transform)
            {
                x = x + MetaForm.transPoint.X;
                y = y + MetaForm.transPoint.Y;
            }
            MetaForm.Data.Clear();
            MetaForm.SetLineStyle((int) MetaForm.line_style);
            MetaForm.Data.color = DXFExport.ColorToDXF(Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b));
            MetaForm.Data.thickness = MetaForm.obj_width;
            MetaForm.Data.point.X = x;
            MetaForm.Data.point.Y = -y + (float) MetaForm.imgHeight;
            MetaForm.Data.point1.X = single2 + x;
            MetaForm.Data.point1.Y = -single3 - y + (float) MetaForm.imgHeight;
            MetaForm.Data.radius = (MetaForm.Data.point1.X - MetaForm.Data.point.X)/2f;
            DXFPoint data = MetaForm.Data.point;
            data.Y = data.Y - MetaForm.Data.radius;
            DXFPoint dXFPoint = MetaForm.Data.point1;
            dXFPoint.Y = dXFPoint.Y - MetaForm.Data.radius;
            DXFPoint data1 = MetaForm.Data.point;
            data1.X = data1.X + MetaForm.Data.radius;
            DXFPoint x1 = MetaForm.Data.point1;
            x1.X = x1.X + MetaForm.Data.radius;
            if (single2 <= single3)
            {
                DXFPoint y1 = MetaForm.Data.point;
                y1.Y = y1.Y - (single3 - single2)/2f;
                DXFPoint dXFPoint1 = MetaForm.Data.point1;
                dXFPoint1.Y = dXFPoint1.Y - (single3 - single2)/2f;
            }
            else
            {
                DXFPoint data2 = MetaForm.Data.point;
                data2.Y = data2.Y + (single2 - single3)/2f;
                DXFPoint y2 = MetaForm.Data.point1;
                y2.Y = y2.Y + (single2 - single3)/2f;
            }
            MetaForm.Data.point1.X = (MetaForm.Data.point1.X - MetaForm.Data.point.X)/2f;
            MetaForm.Data.point1.Y = (MetaForm.Data.point1.Y - MetaForm.Data.point.Y)/2f;
            if (MetaForm.Data.radius < MetaForm.Data.point1.Y)
            {
                MetaForm.Data.radius = MetaForm.Data.point1.Y;
            }
            DXFPoint x2 = MetaForm.Data.point1;
            x2.X = x2.X/MetaForm.Data.radius;
            DXFPoint dXFPoint2 = MetaForm.Data.point1;
            dXFPoint2.Y = dXFPoint2.Y/MetaForm.Data.radius;
            if (MetaForm.Data.point1.X <= MetaForm.Data.point1.Y)
            {
                MetaForm.Data.point1.Y = MetaForm.Data.radius;
                MetaForm.Data.radius = MetaForm.Data.point1.X;
                MetaForm.Data.point1.X = 0f;
            }
            else
            {
                MetaForm.Data.point1.X = MetaForm.Data.radius;
                MetaForm.Data.radius = MetaForm.Data.point1.Y;
                MetaForm.Data.point1.Y = 0f;
            }
            MetaForm.Data.selfType = 1;
            MetaForm.Data.startAngle = single*MetaForm.rad;
            MetaForm.Data.endAngle = (single1 + single)*MetaForm.rad;
            MetaForm.vDXF.AddArc(MetaForm.Data);
        }

        private static void DrawEllipse(byte[] arr, byte cn, bool hatch)
        {
            float x = MetaForm.Format_Size2(arr[cn], arr[cn + 1]);
            float y = MetaForm.Format_Size2(arr[cn + 2], arr[cn + 3]);
            float single = MetaForm.Format_Size2(arr[cn + 4], arr[cn + 5]);
            float single1 = MetaForm.Format_Size2(arr[cn + 6], arr[cn + 7]);
            if (MetaForm.transform)
            {
                x = x + MetaForm.transPoint.X;
                y = y + MetaForm.transPoint.Y;
            }
            MetaForm.Data.Clear();
            MetaForm.SetLineStyle((int) MetaForm.line_style);
            MetaForm.Data.color = DXFExport.ColorToDXF(Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b));
            MetaForm.Data.thickness = MetaForm.obj_width;
            MetaForm.Data.point.X = x;
            MetaForm.Data.point.Y = -y + (float) MetaForm.imgHeight;
            MetaForm.Data.point1.X = single + x;
            MetaForm.Data.point1.Y = -single1 - y + (float) MetaForm.imgHeight;
            MetaForm.Data.radius = (MetaForm.Data.point1.X - MetaForm.Data.point.X)/2f;
            DXFPoint data = MetaForm.Data.point;
            data.Y = data.Y - MetaForm.Data.radius;
            DXFPoint dXFPoint = MetaForm.Data.point1;
            dXFPoint.Y = dXFPoint.Y - MetaForm.Data.radius;
            DXFPoint data1 = MetaForm.Data.point;
            data1.X = data1.X + MetaForm.Data.radius;
            DXFPoint x1 = MetaForm.Data.point1;
            x1.X = x1.X + MetaForm.Data.radius;
            if (single <= single1)
            {
                DXFPoint y1 = MetaForm.Data.point;
                y1.Y = y1.Y - (single1 - single)/2f;
                DXFPoint dXFPoint1 = MetaForm.Data.point1;
                dXFPoint1.Y = dXFPoint1.Y - (single1 - single)/2f;
            }
            else
            {
                DXFPoint data2 = MetaForm.Data.point;
                data2.Y = data2.Y + (single - single1)/2f;
                DXFPoint y2 = MetaForm.Data.point1;
                y2.Y = y2.Y + (single - single1)/2f;
            }
            MetaForm.Data.point1.X = (MetaForm.Data.point1.X - MetaForm.Data.point.X)/2f;
            MetaForm.Data.point1.Y = (MetaForm.Data.point1.Y - MetaForm.Data.point.Y)/2f;
            if (MetaForm.Data.radius < MetaForm.Data.point1.Y)
            {
                MetaForm.Data.radius = MetaForm.Data.point1.Y;
            }
            DXFPoint x2 = MetaForm.Data.point1;
            x2.X = x2.X/MetaForm.Data.radius;
            DXFPoint dXFPoint2 = MetaForm.Data.point1;
            dXFPoint2.Y = dXFPoint2.Y/MetaForm.Data.radius;
            if (MetaForm.Data.point1.X <= MetaForm.Data.point1.Y)
            {
                MetaForm.Data.point1.Y = MetaForm.Data.radius;
                MetaForm.Data.radius = MetaForm.Data.point1.X;
                MetaForm.Data.point1.X = 0f;
            }
            else
            {
                MetaForm.Data.point1.X = MetaForm.Data.radius;
                MetaForm.Data.radius = MetaForm.Data.point1.Y;
                MetaForm.Data.point1.Y = 0f;
            }
            if (!hatch)
            {
                MetaForm.vDXF.AddEllipse(MetaForm.Data);
                return;
            }
            MetaForm.Data.startAngle = 0f;
            MetaForm.Data.endAngle = 6.283185f;
            MetaForm.SetHatchStyle(MetaForm.hatch_style);
            MetaForm.Data.selfType = 2;
            MetaForm.vDXF.AddHatch(MetaForm.Data);
        }

        private static void DrawLine(PointF p1, PointF p2)
        {
            MetaForm.Data.Clear();
            Color color = Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b);
            MetaForm.Data.color = DXFExport.ColorToDXF(color);
            MetaForm.Data.point1.X = p1.X;
            MetaForm.Data.point1.Y = -p1.Y + (float) MetaForm.imgHeight;
            MetaForm.Data.point.X = p2.X;
            MetaForm.Data.point.Y = -p2.Y + (float) MetaForm.imgHeight;
            if (MetaForm.transform)
            {
                DXFPoint data = MetaForm.Data.point1;
                data.X = data.X + MetaForm.transPoint.X;
                DXFPoint y = MetaForm.Data.point1;
                y.Y = y.Y + MetaForm.transPoint.Y;
                DXFPoint x = MetaForm.Data.point;
                x.X = x.X + MetaForm.transPoint.X;
                DXFPoint dXFPoint = MetaForm.Data.point;
                dXFPoint.Y = dXFPoint.Y + MetaForm.transPoint.Y;
            }
            MetaForm.vDXF.AddLine(MetaForm.Data);
        }

        private static void DrawLine(byte[] arr, byte cn)
        {
            float x = MetaForm.Format_Size2(arr[cn], arr[cn + 1]);
            float y = MetaForm.Format_Size2(arr[cn + 2], arr[cn + 3]);
            float single = MetaForm.Format_Size2(arr[cn + 4], arr[cn + 5]);
            float y1 = MetaForm.Format_Size2(arr[cn + 6], arr[cn + 7]);
            if (MetaForm.transform)
            {
                x = x + MetaForm.transPoint.X;
                y = y + MetaForm.transPoint.Y;
                single = single + MetaForm.transPoint.X;
                y1 = y1 + MetaForm.transPoint.Y;
            }
            MetaForm.Data.Clear();
            Color color = Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b);
            MetaForm.Data.color = DXFExport.ColorToDXF(color);
            MetaForm.SetLineStyle((int) MetaForm.line_style);
            MetaForm.Data.thickness = MetaForm.obj_width;
            MetaForm.Data.point1.X = x;
            MetaForm.Data.point1.Y = -y + (float) MetaForm.imgHeight;
            MetaForm.Data.point.X = single;
            MetaForm.Data.point.Y = -y1 + (float) MetaForm.imgHeight;
            MetaForm.vDXF.AddLine(MetaForm.Data);
        }

        private void DrawMetaFromStream(object sender, PaintEventArgs e)
        {
            if (this.mfStream != null)
            {
                e.Graphics.DrawImage(this.mf, 0, 0);
            }
        }

        private static void DrawPolyLine(byte[] arr, byte cn, bool hatch, bool bezier)
        {
            MetaForm.Data.Clear();
            Color color = Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b);
            MetaForm.Data.color = DXFExport.ColorToDXF(color);
            MetaForm.SetLineStyle((int) MetaForm.line_style);
            MetaForm.Data.thickness = MetaForm.obj_width;
            MetaForm.Data.points.Add(new ArrayList());
            MetaForm.Data.count = arr[cn];
            for (int i = 4; i < arr[cn]*4 + 4; i = i + 4)
            {
                float x = MetaForm.Format_Size2(arr[cn + i], arr[cn + i + 1]);
                float y = MetaForm.Format_Size2(arr[cn + i + 2], arr[cn + i + 3]);
                if (MetaForm.transform)
                {
                    x = x + MetaForm.transPoint.X;
                    y = y + MetaForm.transPoint.Y;
                }
                y = -y + (float) MetaForm.imgHeight;
                ((ArrayList) MetaForm.Data.points[0]).Add(new DXFPoint(x, y, 0f));
            }
            if (hatch)
            {
                MetaForm.SetHatchStyle(MetaForm.hatch_style);
                MetaForm.Data.selfType = 0;
                MetaForm.vDXF.AddHatch(MetaForm.Data);
                return;
            }
            if (bezier)
            {
                MetaForm.vDXF.AddPolyBezier(MetaForm.Data, 0);
                return;
            }
            MetaForm.vDXF.AddPolyLine(MetaForm.Data, 0);
        }

        private static void DrawRectangle(byte[] arr, byte cn, bool hatch)
        {
            float x = MetaForm.Format_Size2(arr[cn], arr[cn + 1]);
            float y = MetaForm.Format_Size2(arr[cn + 2], arr[cn + 3]);
            float single = MetaForm.Format_Size2(arr[cn + 4], arr[cn + 5]);
            float single1 = MetaForm.Format_Size2(arr[cn + 6], arr[cn + 7]);
            if (MetaForm.transform)
            {
                x = x + MetaForm.transPoint.X;
                y = y + MetaForm.transPoint.Y;
            }
            MetaForm.Data.Clear();
            MetaForm.Data.color = DXFExport.ColorToDXF(Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b));
            MetaForm.SetLineStyle((int) MetaForm.line_style);
            MetaForm.Data.thickness = MetaForm.obj_width;
            MetaForm.Data.point.X = x;
            MetaForm.Data.point.Y = -y + (float) MetaForm.imgHeight;
            MetaForm.Data.point1.X = single + x;
            MetaForm.Data.point1.Y = -single1 - y + (float) MetaForm.imgHeight;
            if (!hatch)
            {
                MetaForm.vDXF.AddRectangle(MetaForm.Data);
                return;
            }
            MetaForm.Data.count = 1;
            MetaForm.Data.points.Add(new ArrayList());
            DXFPoint dXFPoint = new DXFPoint()
            {
                X = MetaForm.Data.point.X,
                Y = MetaForm.Data.point.Y,
                Z = 0f
            };
            ((ArrayList) MetaForm.Data.points[0]).Add(dXFPoint);
            dXFPoint = new DXFPoint()
            {
                X = MetaForm.Data.point1.X,
                Y = MetaForm.Data.point.Y,
                Z = 0f
            };
            ((ArrayList) MetaForm.Data.points[0]).Add(dXFPoint);
            dXFPoint = new DXFPoint()
            {
                X = MetaForm.Data.point1.X,
                Y = MetaForm.Data.point1.Y,
                Z = 0f
            };
            ((ArrayList) MetaForm.Data.points[0]).Add(dXFPoint);
            dXFPoint = new DXFPoint()
            {
                X = MetaForm.Data.point.X,
                Y = MetaForm.Data.point1.Y,
                Z = 0f
            };
            ((ArrayList) MetaForm.Data.points[0]).Add(dXFPoint);
            MetaForm.SetHatchStyle(MetaForm.hatch_style);
            MetaForm.Data.selfType = 0;
            MetaForm.vDXF.AddHatch(MetaForm.Data);
        }

        private static void DrawString(byte[] arr)
        {
            string str = "";
            float x = (float) MetaForm.TextPos((int) arr[14], (int) arr[15]);
            float y = (float) MetaForm.TextPos((int) arr[18], (int) arr[19]);
            if (MetaForm.transform)
            {
                x = x + MetaForm.transPoint.X;
                y = y + MetaForm.transPoint.Y;
            }
            for (int i = 0; i < arr[8]*2; i = i + 2)
            {
                str = string.Concat(str, Convert.ToChar(arr[28 + i]));
            }
            MetaForm.Data.Clear();
            MetaForm.Data.point.X = x;
            MetaForm.Data.point.Y = y;
            MetaForm.Data.color = DXFExport.ColorToDXF(Color.FromArgb(MetaForm.r, MetaForm.g, MetaForm.b));
            MetaForm.Data.height = MetaForm.text_size;
            MetaForm.Data.rotation = 0f;
            MetaForm.Data.text = str;
            MetaForm.SetTextStyle(MetaForm.text_style);
            MetaForm.Data.point.Y = -MetaForm.Data.point.Y + (float) MetaForm.imgHeight;
            MetaForm.vDXF.AddMText(MetaForm.Data);
        }

        private static int EmfWidthToWidth(int dt1, int dt2)
        {
            return (dt2*255 + dt1)/31;
        }

        private static float Format_Size2(byte dt1, byte dt2)
        {
            return (float) (dt1 + dt2*256);
        }

        private static float FormatSize(int dt1, int dt2)
        {
            double num;
            if (dt2 == 0)
            {
                return 0f;
            }
            if (dt2 <= 63)
            {
                return 1f;
            }
            if (dt1 < 128)
            {
                num = 64/Math.Pow(4, (double) (dt2 - 64));
            }
            else
            {
                dt1 = dt1 - 128;
                num = 64/(Math.Pow(4, (double) (dt2 - 64))*2);
            }
            if (num < 9.99999993922529E-09)
            {
                num = 128;
            }
            return (float) (128/num + (double) dt1/num);
        }

        public void Free()
        {
            if (this.mf != null)
            {
                this.mf.Dispose();
            }
            if (this.mfStream != null)
            {
                this.mfStream.Close();
                this.mfStream = null;
            }
            this.mf = null;
        }

        public bool SaveFromStream(string fName)
        {
            if (this.mfStream == null || this.mf == null)
            {
                return false;
            }
            FileStream fileStream = new FileStream(fName, FileMode.Create);
            this.mfStream.WriteTo(fileStream);
            fileStream.Close();
            return true;
        }

        private static void SetHatchStyle(int hst)
        {
            switch (hst)
            {
                case 0:
                {
                    MetaForm.Data.style = 2;
                    return;
                }
                case 1:
                {
                    MetaForm.Data.style = 3;
                    return;
                }
                case 2:
                {
                    MetaForm.Data.style = 4;
                    return;
                }
                case 3:
                {
                    MetaForm.Data.style = 5;
                    return;
                }
                case 4:
                {
                    MetaForm.Data.style = 6;
                    return;
                }
                case 5:
                {
                    MetaForm.Data.style = 7;
                    return;
                }
            }
            MetaForm.Data.style = 0;
        }

        private static void SetLineStyle(int val)
        {
            switch (val)
            {
                case 0:
                {
                    MetaForm.Data.text = "_SOLID";
                    return;
                }
                case 1:
                {
                    MetaForm.Data.text = "_DASH";
                    return;
                }
                case 2:
                {
                    MetaForm.Data.text = "_DOT";
                    return;
                }
                case 3:
                {
                    MetaForm.Data.text = "_DASHDOT";
                    return;
                }
                case 4:
                {
                    MetaForm.Data.text = "_DASHDOTDOT";
                    return;
                }
                default:
                {
                    return;
                }
            }
        }

        private static void SetTextStyle(byte tx_st)
        {
            string data = MetaForm.Data.text;
            if (tx_st == 4)
            {
                data = string.Concat("\\L", data);
            }
            data = string.Concat(";", data);
            data = (tx_st != 1 ? string.Concat("|b0", data) : string.Concat("|b1", data));
            data = (tx_st != 2 ? string.Concat("|i0", data) : string.Concat("|i1", data));
            data = string.Concat("{\\f", data, "}");
            MetaForm.Data.text = data;
        }

        public bool SplitingToStream(IntPtr Handle, StringWriter ASt)
        {
            MetaForm.vDXF = new DXFExport();
            MetaForm.Data = new DXFData();
            MetaForm.vDXF.fOffset = 50f;
            float[] singleArray = new float[] {5f, default(float)};
            MetaForm.vDXF.AddLType("_SOLID", singleArray);
            MetaForm.vDXF.AddLType("_DASH", new float[] {5f, -2f});
            MetaForm.vDXF.AddLType("_DOT", new float[] {2f, -2f});
            MetaForm.vDXF.AddLType("_DASHDOT", new float[] {5f, -2f, 2f, -2f});
            MetaForm.vDXF.AddLType("_DASHDOTDOT", new float[] {5f, -2f, 2f, -2f, 2f, -2f});
            MetaForm.Data = new DXFData()
            {
                height = 50f,
                rotation = 0f
            };
            MetaForm.Data.point.X = 57f;
            MetaForm.Data.point.Y = 0f;
            MetaForm.Data.color = 1;
            MetaForm.Data.text = "Trial version";
            MetaForm.vDXF.AddText(MetaForm.Data);
            Graphics graphic = Graphics.FromHwnd(Handle);
            Graphics graphic1 = graphic;
            using (graphic)
            {
                graphic1.EnumerateMetafile(this.mf, new Point(0, 0), new Rectangle(0, 0, this.mf.Width, this.mf.Height),
                    GraphicsUnit.Pixel, new Graphics.EnumerateMetafileProc(MetaForm.DelFunc));
            }
            graphic1.Dispose();
            MetaForm.vDXF.SaveToStream(ASt);
            return true;
        }

        private static void SplitRecObject(byte[] curArr)
        {
            int num = 12;
            int num1 = 4;
            if (curArr[9] != 64)
            {
                num1 = 8;
            }
            int num2 = num + (int) MetaForm.Format_Size2(curArr[4], curArr[5])*num1;
            int length = (int) curArr.Length;
            ArrayList arrayLists = new ArrayList();
            int num3 = -1;
            while (num2 < length)
            {
                if (curArr[num2] == 0)
                {
                    if (num2 + 1 >= length || curArr[num2 + 1] == 0)
                    {
                        break;
                    }
                    arrayLists.Add(new MetaForm.ObjectEnt());
                    num3++;
                    if (curArr[num2 + 1] == 1)
                    {
                        (arrayLists[num3] as MetaForm.ObjectEnt).type = MetaForm.ObjTypeRecord.objLine;
                    }
                    if (curArr[num2 + 1] == 3)
                    {
                        (arrayLists[num3] as MetaForm.ObjectEnt).type = MetaForm.ObjTypeRecord.objBezier;
                    }
                }
                if ((arrayLists[num3] as MetaForm.ObjectEnt).type == MetaForm.ObjTypeRecord.objBezier &&
                    curArr[num2] == 1 ||
                    (arrayLists[num3] as MetaForm.ObjectEnt).type == MetaForm.ObjTypeRecord.objLine && curArr[num2] == 3)
                {
                    arrayLists.Add(new MetaForm.ObjectEnt());
                    num3++;
                    if (curArr[num2] == 1)
                    {
                        (arrayLists[num3] as MetaForm.ObjectEnt).type = MetaForm.ObjTypeRecord.objLine;
                    }
                    if (curArr[num2] == 3)
                    {
                        (arrayLists[num3] as MetaForm.ObjectEnt).type = MetaForm.ObjTypeRecord.objBezier;
                    }
                    int count = (arrayLists[num3 - 1] as MetaForm.ObjectEnt).points.Count - 1;
                    (arrayLists[num3] as MetaForm.ObjectEnt).points.Add(
                        (arrayLists[num3 - 1] as MetaForm.ObjectEnt).points[count]);
                    (arrayLists[num3] as MetaForm.ObjectEnt).compobj = num3 - 1;
                }
                if (num1 != 4)
                {
                    (arrayLists[num3] as MetaForm.ObjectEnt).points.Add(
                        new PointF(MetaForm.FormatSize((int) curArr[num + 2], (int) curArr[num + 3]),
                            MetaForm.FormatSize((int) curArr[num + 6], (int) curArr[num + 7])));
                }
                else
                {
                    (arrayLists[num3] as MetaForm.ObjectEnt).points.Add(
                        new PointF(MetaForm.Format_Size2(curArr[num], curArr[num + 1]),
                            MetaForm.Format_Size2(curArr[num + 2], curArr[num + 3])));
                }
                if (curArr[num2] == 129 || curArr[num2] == 161)
                {
                    (arrayLists[num3] as MetaForm.ObjectEnt).type = MetaForm.ObjTypeRecord.objCloseLine;
                }
                if (curArr[num2] == 131 || curArr[num2] == 163)
                {
                    (arrayLists[num3] as MetaForm.ObjectEnt).type = MetaForm.ObjTypeRecord.objCloseBezier;
                }
                num = num + num1;
                num2++;
            }
            MetaForm.AddObjectsPath(arrayLists);
        }

        public bool Splitting(IntPtr Handle, string FileName)
        {
            MetaForm.vDXF = new DXFExport();
            MetaForm.Data = new DXFData();
            MetaForm.vDXF.fOffset = 50f;
            float[] singleArray = new float[] {5f, default(float)};
            MetaForm.vDXF.AddLType("_SOLID", singleArray);
            MetaForm.vDXF.AddLType("_DASH", new float[] {5f, -2f});
            MetaForm.vDXF.AddLType("_DOT", new float[] {2f, -2f});
            MetaForm.vDXF.AddLType("_DASHDOT", new float[] {5f, -2f, 2f, -2f});
            MetaForm.vDXF.AddLType("_DASHDOTDOT", new float[] {5f, -2f, 2f, -2f, 2f, -2f});
            MetaForm.Data = new DXFData()
            {
                height = 50f,
                rotation = 0f
            };
            MetaForm.Data.point.X = 57f;
            MetaForm.Data.point.Y = 0f;
            MetaForm.Data.color = 1;
            MetaForm.Data.text = "Trial version";
            MetaForm.vDXF.AddText(MetaForm.Data);
            Graphics graphic = Graphics.FromHwnd(Handle);
            Graphics graphic1 = graphic;
            using (graphic)
            {
                graphic1.EnumerateMetafile(this.mf, new Point(0, 0), new Rectangle(0, 0, this.mf.Width, this.mf.Height),
                    GraphicsUnit.Pixel, new Graphics.EnumerateMetafileProc(MetaForm.DelFunc));
            }
            graphic1.Dispose();
            MetaForm.vDXF.SaveToFile(FileName);
            return true;
        }

        public static bool Splitting(string pfName, IntPtr Handle, bool DelMeta)
        {
            bool flag;
            MetaForm.tmpName = pfName.Replace(".emf", ".tmp");
            if (File.Exists(MetaForm.tmpName))
            {
                File.Delete(MetaForm.tmpName);
            }
            if (File.Exists(MetaForm.tmpName.Replace(".tmp", "~.tmp")))
            {
                File.Delete(MetaForm.tmpName.Replace(".tmp", "~.tmp"));
            }
            try
            {
                MetaForm.mfImage = new Metafile(pfName);
                goto Label0;
            }
            catch
            {
                MessageBox.Show("Error in .emf file");
                flag = false;
            }
            return flag;
            Label0:
            MetaForm.vDXF = new DXFExport();
            MetaForm.Data = new DXFData();
            MetaForm.vDXF.fOffset = 50f;
            float[] singleArray = new float[] {5f, default(float)};
            MetaForm.vDXF.AddLType("_SOLID", singleArray);
            MetaForm.vDXF.AddLType("_DASH", new float[] {5f, -2f});
            MetaForm.vDXF.AddLType("_DOT", new float[] {2f, -2f});
            MetaForm.vDXF.AddLType("_DASHDOT", new float[] {5f, -2f, 2f, -2f});
            MetaForm.vDXF.AddLType("_DASHDOTDOT", new float[] {5f, -2f, 2f, -2f, 2f, -2f});
            MetaForm.Data = new DXFData()
            {
                height = 50f,
                rotation = 0f
            };
            MetaForm.Data.point.X = 57f;
            MetaForm.Data.point.Y = 0f;
            MetaForm.Data.color = 1;
            MetaForm.Data.text = "Trial version";
            MetaForm.vDXF.AddText(MetaForm.Data);
            Graphics graphic = Graphics.FromHwnd(Handle);
            Graphics graphic1 = graphic;
            using (graphic)
            {
                graphic1.EnumerateMetafile(MetaForm.mfImage, new Point(0, 0),
                    new Rectangle(0, 0, MetaForm.mfImage.Width, MetaForm.mfImage.Height), GraphicsUnit.Pixel,
                    new Graphics.EnumerateMetafileProc(MetaForm.DelFunc));
            }
            graphic1.Dispose();
            MetaForm.mfImage.Dispose();
            MetaForm.tmpName = pfName.Replace(".emf", ".dxf");
            MetaForm.vDXF.SaveToFile(MetaForm.tmpName);
            if (DelMeta)
            {
                File.Delete(pfName);
            }
            return true;
        }

        private static int TextPos(int dt1, int dt2)
        {
            double num;
            double num1;
            if (dt2 == 0)
            {
                return 0;
            }
            if (dt2 <= 63)
            {
                return 1;
            }
            if (dt1 < 128)
            {
                num = 64/Math.Pow(4, (double) (dt2 - 64));
                num1 = Math.Pow(4, (double) (dt2 - 63))/2;
            }
            else
            {
                dt1 = dt1 - 128;
                num = 64/(Math.Pow(4, (double) (dt2 - 64))*2);
                num1 = Math.Pow(4, (double) (dt2 - 63));
            }
            return (int) (num1 + (double) dt1/num);
        }

        private class ObjectEnt
        {
            public int compobj;

            public ArrayList points;

            public MetaForm.ObjTypeRecord type;

            public ObjectEnt()
            {
                this.points = new ArrayList();
                this.compobj = -1;
            }
        }

        private enum ObjTypeRecord
        {
            objCloseLine,
            objCloseBezier,
            objBezier,
            objLine
        }
    }
}