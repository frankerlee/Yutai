using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using stdole;

namespace Yutai.Pipeline.Analysis.Helpers
{
    public class BaseFun
    {
        private string string_0;

        private double double_0;

        public int _iLineColor;

        private string string_1;

        private double double_1;

        private int int_0;

        private bool bool_0;

        private bool bool_1;

        private bool bool_2;

        public BaseFun()
        {
            this.method_0();
        }

        private void method_0()
        {
            this.string_1 = "宋体";
            this.double_1 = 10.0;
            this.int_0 = 0;
            this.bool_0 = false;
            this.bool_1 = false;
            this.bool_2 = false;
            this.string_0 = "";
            this._iLineColor = 0;
            this.double_0 = 1.7;
            try
            {
                string url = Application.StartupPath + "\\config\\EMNote.xml";
                XmlTextReader xmlTextReader = new XmlTextReader(url);
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlTextReader.Name == "text")
                        {
                            try
                            {
                                this.string_1 = xmlTextReader.GetAttribute("font");
                            }
                            catch
                            {
                            }
                            try
                            {
                                this.double_1 = Convert.ToDouble(xmlTextReader.GetAttribute("size"));
                            }
                            catch
                            {
                            }
                            try
                            {
                                int argb = Convert.ToInt32(xmlTextReader.GetAttribute("color"));
                                IRgbColor rgbColor = new RgbColor();
                                Color color = Color.FromArgb(argb);
                                rgbColor.Red=((int)color.R);
                                rgbColor.Green=((int)color.G);
                                rgbColor.Blue=((int)color.B);
                                this.int_0 = rgbColor.RGB;
                            }
                            catch
                            {
                            }
                            try
                            {
                                this.bool_0 = Convert.ToBoolean(xmlTextReader.GetAttribute("bold"));
                            }
                            catch
                            {
                            }
                            try
                            {
                                this.bool_1 = Convert.ToBoolean(xmlTextReader.GetAttribute("italic"));
                            }
                            catch
                            {
                            }
                            try
                            {
                                this.bool_2 = Convert.ToBoolean(xmlTextReader.GetAttribute("underline"));
                                continue;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        if (xmlTextReader.Name == "line")
                        {
                            try
                            {
                                this.string_0 = xmlTextReader.GetAttribute("name").ToString();
                            }
                            catch
                            {
                            }
                            try
                            {
                                this._iLineColor = Convert.ToInt32(xmlTextReader.GetAttribute("color"));
                            }
                            catch
                            {
                            }
                            try
                            {
                                this.double_0 = Convert.ToDouble(xmlTextReader.GetAttribute("width"));
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                xmlTextReader.Close();
            }
            catch
            {
            }
        }

        private void method_1(List<string> fileArray)
        {
            string str = Application.StartupPath + "\\";
            string text = str + "Symbol.Xml";
            try
            {
                if (File.Exists(text))
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXml(text);
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        string text2 = str + "Style\\" + dataRow[0].ToString();
                        if (File.Exists(text2))
                        {
                            fileArray.Add(text2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ISymbol GetLineSymbol(out string sLineSymbolName)
        {
            List<string> list = new List<string>();
            this.method_1(list);
            IStyleGallery styleGallery = new ServerStyleGallery();
            ISymbol result;
            foreach (string current in list)
            {
                styleGallery.Clear();
                ((IStyleGalleryStorage)styleGallery).AddFile(current);
                IEnumStyleGalleryItem enumStyleGalleryItem = styleGallery.get_Items("Line Symbols", current, "");
                enumStyleGalleryItem.Reset();
                for (IStyleGalleryItem styleGalleryItem = enumStyleGalleryItem.Next(); styleGalleryItem != null; styleGalleryItem = enumStyleGalleryItem.Next())
                {
                    if (this.string_0 == "")
                    {
                        sLineSymbolName = styleGalleryItem.Name;
                        ISymbol symbol = (ISymbol)styleGalleryItem.Item;
                        result = symbol;
                        return result;
                    }
                    if (styleGalleryItem.Name == this.string_0)
                    {
                        IRgbColor rgbColor = new RgbColor();
                        rgbColor.RGB=(this._iLineColor);
                        ((ILineSymbol)styleGalleryItem.Item).Color=(rgbColor);
                        ((ILineSymbol)styleGalleryItem.Item).Width=(this.double_0);
                        sLineSymbolName = styleGalleryItem.Name;
                        ISymbol symbol = (ISymbol)styleGalleryItem.Item;
                        result = symbol;
                        return result;
                    }
                }
            }
            sLineSymbolName = "";
            result = null;
            return result;
        }

        public ISymbol GetLineSymbol()
        {
            string text = "";
            return this.GetLineSymbol(out text);
        }

        public IElement GetTextElement(double dblAngle, string sNoteText, IPoint PosPt)
        {
            return this.GetTextElement(dblAngle, sNoteText, PosPt, this.int_0);
        }

        public IElement GetTextElement(double dblAngle, string sNoteText, IPoint PosPt, int iColor)
        {
            IRgbColor rgbColor = new RgbColor();
            rgbColor.RGB=(iColor);
            IFont font = new SystemFont() as IFont;
            font.Name = this.string_1;
            font.Size = (decimal)this.double_1;
            font.Bold = this.bool_0;
            font.Italic = this.bool_1;
            font.Underline = this.bool_2;
            ITextSymbol textSymbol = new TextSymbol();
            textSymbol.Color=(rgbColor);
            textSymbol.Font=((IFontDisp)font);
            textSymbol.Angle=(dblAngle);
            textSymbol.RightToLeft=(false);
            textSymbol.VerticalAlignment=(0);
            textSymbol.HorizontalAlignment=(0);
            ITextElement textElement = new TextElement() as ITextElement;
            textElement.Symbol=(textSymbol);
            textElement.Text=(sNoteText);
            textElement.ScaleText=(true);
            ((IElement)textElement).Geometry=(PosPt);
            ((IElementProperties)textElement).Name=("EMMapNote");
            return (IElement)textElement;
        }

        public IElement GetLineElement(IPolyline pPolyLine)
        {
            IElement element = new LineElement();
            ((ILineElement)element).Symbol=((ILineSymbol)this.GetLineSymbol());
            ((IElementProperties)element).Name=("EMMapNote");
            element.Geometry=(pPolyLine);
            return element;
        }

        public void NoteOneRowMessage(IActiveView pView, IPolyline pLine, string sNoteText)
        {
            ITextElement textElement = (ITextElement)this.GetTextElement(0.0, sNoteText, pLine.ToPoint);
            IEnvelope envelope = new Envelope() as IEnvelope;
            ((IElement)textElement).QueryBounds(pView.ScreenDisplay, envelope);
            IPoint toPoint = pLine.ToPoint;
            IPoint expr_41 = toPoint;
            expr_41.Y=(expr_41.Y + envelope.Height);
            if (pLine.ToPoint.X < pLine.FromPoint.X)
            {
                IPoint expr_71 = toPoint;
                expr_71.X=(expr_71.X - envelope.Width);
            }
            ((IElement)textElement).Geometry=(toPoint);
            ((IGraphicsContainer)pView).AddElement((IElement)textElement, 0);
            ((IGraphicsContainer)pView).AddElement(this.GetLineElement(pLine), 0);
            object missing = Type.Missing;
            IPoint toPoint2 = pLine.ToPoint;
            if (pLine.ToPoint.X < pLine.FromPoint.X)
            {
                IPoint expr_EB = toPoint2;
                expr_EB.X=(expr_EB.X - envelope.Width);
            }
            else
            {
                IPoint expr_101 = toPoint2;
                expr_101.X=(expr_101.X + envelope.Width);
            }
            IPointCollection pointCollection = new Polyline();
            pointCollection.AddPoint(pLine.ToPoint, ref missing, ref missing);
            pointCollection.AddPoint(toPoint2, ref missing, ref missing);
            ((IGraphicsContainer)pView).AddElement(this.GetLineElement((IPolyline)pointCollection), 0);
            IPoint toPoint3 = pLine.ToPoint;
            IPoint expr_160 = toPoint3;
            expr_160.Y=(expr_160.Y + envelope.Height);
            IPointCollection pointCollection2 = new Polyline();
            pointCollection2.AddPoint(pLine.ToPoint, ref missing, ref missing);
            pointCollection2.AddPoint(toPoint3, ref missing, ref missing);
            ((IGraphicsContainer)pView).AddElement(this.GetLineElement((IPolyline)pointCollection2), 0);
            pView.Refresh();
        }

        public void NoteOrientationCenterText(IActiveView pView, IPolyline pPolyLine, string sNoteText)
        {
            IPoint point = ((IPointCollection)pPolyLine).get_Point(0);
            IPoint point2 = ((IPointCollection)pPolyLine).get_Point(1);
            double d = (point2.Y - point.Y) / (point2.X - point.X);
            double num = Math.Atan(d) * 180.0 / 3.1415926535897931;
            double value = Math.Atan(d);
            IElement textElement = this.GetTextElement(0.0, sNoteText, pPolyLine.ToPoint);
            IEnvelope envelope = new Envelope() as IEnvelope;
            textElement.QueryBounds(pView.ScreenDisplay, envelope);
            double num2;
            double num3;
            if ((((IPointCollection)pPolyLine).get_Point(1).X < ((IPointCollection)pPolyLine).get_Point(0).X && ((IPointCollection)pPolyLine).get_Point(1).Y > ((IPointCollection)pPolyLine).get_Point(0).Y) || (((IPointCollection)pPolyLine).get_Point(1).X > ((IPointCollection)pPolyLine).get_Point(0).X && ((IPointCollection)pPolyLine).get_Point(1).Y < ((IPointCollection)pPolyLine).get_Point(0).Y))
            {
                num2 = envelope.Height * Math.Sin(Math.Abs(value)) - 0.5 * envelope.Width * Math.Cos(Math.Abs(value));
                num3 = envelope.Height * Math.Cos(Math.Abs(value)) + 0.5 * envelope.Width * Math.Sin(Math.Abs(value));
            }
            else
            {
                num2 = -envelope.Height * Math.Sin(Math.Abs(value)) - 0.5 * envelope.Width * Math.Cos(Math.Abs(value));
                num3 = envelope.Height * Math.Cos(Math.Abs(value)) - 0.5 * envelope.Width * Math.Sin(Math.Abs(value));
            }
            double arg_260_1 = num;
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.X=((pPolyLine.FromPoint.X + pPolyLine.ToPoint.X) / 2.0 + num2);
            pointClass.Y=((pPolyLine.FromPoint.Y + pPolyLine.ToPoint.Y) / 2.0 + num3);
            textElement = this.GetTextElement(arg_260_1, sNoteText, pointClass);
            ((IElementProperties)textElement).Name=("EMMapNote");
            ((IGraphicsContainer)pView).AddElement(textElement, 0);
            pView.Refresh();
        }

        public static IPoint GetParallelLeftOffsetPoint(IPolyline pPolyline, IPoint Pt, double dblOffset)
        {
            IPoint point = ((IPointCollection)pPolyline).get_Point(0);
            IPoint point2 = ((IPointCollection)pPolyline).get_Point(1);
            double d = (point2.Y - point.Y) / (point2.X - point.X);
            double value = Math.Atan(d);
            double num;
            double num2;
            if ((point2.X < point.X && point2.Y > point.Y) || (point2.X > point.X && point2.Y < point.Y))
            {
                num = dblOffset * Math.Sin(Math.Abs(value));
                num2 = dblOffset * Math.Cos(Math.Abs(value));
            }
            else
            {
                num = -dblOffset * Math.Sin(Math.Abs(value));
                num2 = dblOffset * Math.Cos(Math.Abs(value));
            }
            IPoint pointClass = new ESRI.ArcGIS.Geometry.Point();
            pointClass.X=(Pt.X + num);
            pointClass.Y=(Pt.Y + num2);
            return pointClass;
        }

        public static void SelectObjsByShape(IMap pMap, IGeometry Geo)
        {
            BaseFun.SelectObjsByShape(pMap, Geo, true);
        }

        public static void SelectObjsByShape(IMap pMap, IGeometry Geo, bool justOnce)
        {
            if (Geo.GeometryType == (esriGeometryType) 1)
            {
                IPoint point = (IPoint)Geo;
                double num = ((IActiveView)pMap).Extent.Width / 200.0;
                double num2 = point.X - num;
                double num3 = point.Y - num;
                double num4 = point.X + num;
                double num5 = point.Y + num;
                IEnvelope envelope = new Envelope() as IEnvelope;
                envelope.PutCoords(num2, num3, num4, num5);
               pMap.ClearSelection();
               pMap.SelectByShape(envelope, new SelectionEnvironment(), justOnce);
            }
            else if (Geo.GeometryType == (esriGeometryType) 3)
            {
                IPolyline polyline = (IPolyline)Geo;
                ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
                pMap.SelectByShape(polyline, selectionEnvironment, justOnce);
            }
            if (pMap.SelectionCount > 0)
            {
                ((IActiveView)pMap).Refresh();
            }
        }

        public static double GetDistance(double X1, double Y1, double X2, double Y2)
        {
            return Math.Sqrt(Math.Pow(X2 - X1, 2.0) + Math.Pow(Y2 - Y1, 2.0));
        }

        public static double GetAngle(double startX, double startY, double endX, double endY)
        {
            double num = Math.Abs(endX - startX);
            double num2 = Math.Abs(endY - startY);
            double num3 = Math.Sqrt(num * num + num2 * num2);
            double result;
            if (num3 == 0.0)
            {
                result = 0.0;
            }
            else
            {
                double num4 = Math.Asin(num2 / num3) / 3.1415926535897931 * 180.0;
                if (endX <= startX && endY >= startY)
                {
                    num4 = 180.0 - num4;
                }
                else if (endX <= startX && endY <= startY)
                {
                    num4 = 180.0 + num4;
                }
                else if (endX >= startX && endY <= startY)
                {
                    num4 = 360.0 - num4;
                }
                if (num4 == 360.0)
                {
                    num4 = 0.0;
                }
                result = num4;
            }
            return result;
        }
    }
}