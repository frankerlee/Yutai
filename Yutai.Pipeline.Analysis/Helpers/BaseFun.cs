using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using stdole;
using Yutai.Pipeline.Config.Interfaces;

namespace Yutai.Pipeline.Analysis.Helpers
{
    public class BaseFun
    {
        private string lineName;

        private double lineWidth;

        public int lineColor;

        private string fontName;

        private double fontSize;

        private int fontColor;

        private bool fontBold;

        private bool fontItalic;

        private bool fontUnderline;
        private IPipelineConfig _config;

        public BaseFun(IPipelineConfig config)
        {
            this.InitConfig(config);
            _config = config;
        }

        private void InitConfig(IPipelineConfig config)
        {
            this.fontName = "华文中宋";
            this.fontSize = 12.0;
            this.fontColor = -16776961;
            this.fontBold = false;
            this.fontItalic = false;
            this.fontUnderline = false;
            this.lineName = "1033B";
            this.lineColor = 33554432;
            this.lineWidth = 1.7;
            try
            {
                ICommonConfig commonConfig = config.CommonConfigs.FirstOrDefault(c => c.Name.ToUpper() == "MAPNOTETEXT");
                if (commonConfig != null)
                {
                    string[] values = commonConfig.Value.Split(';');
                    for (int i = 0; i < values.Length; i++)
                    {
                        string[] pairs = values[i].Split(':');
                        string pairName = pairs[0].ToUpper();
                        string pairValue = pairs[1].ToUpper();
                        if (pairName == "FONT")
                        {
                            this.fontName = pairValue;
                            continue;
                        }
                        if (pairName == "SIZE")
                        {
                            this.fontSize =Convert.ToDouble(pairValue);
                            continue;
                        }
                        if (pairName == "COLOR")
                        {
                            this.fontColor = Convert.ToInt32(pairValue);
                            continue;
                        }
                        if (pairName.Contains("BOLD"))
                        {
                            this.fontBold = pairValue.StartsWith("T") ? true : false;
                            continue;
                        }
                        if (pairName.Contains("ITAL"))
                        {
                            this.fontItalic = pairValue.StartsWith("T") ? true : false;
                            continue;
                        }
                        if (pairName.Contains("UNDER"))
                        {
                            this.fontUnderline = pairValue.StartsWith("T") ? true : false;
                            continue;
                        }
                    }
                }
                commonConfig = config.CommonConfigs.FirstOrDefault(c => c.Name.ToUpper() == "MAPNOTELINE");
                if (commonConfig != null)
                {
                    string[] values = commonConfig.Value.Split(';');
                    for (int i = 0; i < values.Length; i++)
                    {
                        string[] pairs = values[i].Split(':');
                        string pairName = pairs[0].ToUpper();
                        string pairValue = pairs[1].ToUpper();
                        if (pairName == "NAME")
                        {
                            this.lineName = pairValue;
                            continue;
                        }
                        if (pairName == "WIDTH")
                        {
                            this.lineWidth = Convert.ToDouble(pairValue);
                            continue;
                        }
                        if (pairName == "COLOR")
                        {
                            this.lineColor = Convert.ToInt32(pairValue);
                            continue;
                        }
                    }
                }
               
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
            //this.method_1(list);
            IStyleGallery styleGallery = new ServerStyleGallery();
            ISymbol result;
            
            foreach (string current in _config.StyleFiles)
            {
                styleGallery.Clear();
                ((IStyleGalleryStorage)styleGallery).AddFile(current);
                IEnumStyleGalleryItem enumStyleGalleryItem = styleGallery.get_Items("Line Symbols", current, "");
                enumStyleGalleryItem.Reset();
                for (IStyleGalleryItem styleGalleryItem = enumStyleGalleryItem.Next(); styleGalleryItem != null; styleGalleryItem = enumStyleGalleryItem.Next())
                {
                    if (this.lineName == "")
                    {
                        sLineSymbolName = styleGalleryItem.Name;
                        ISymbol symbol = (ISymbol)styleGalleryItem.Item;
                        result = symbol;
                        return result;
                    }
                    if (styleGalleryItem.Name == this.lineName)
                    {
                        IRgbColor rgbColor = new RgbColor();
                        rgbColor.RGB=(this.lineColor);
                        ((ILineSymbol)styleGalleryItem.Item).Color=(rgbColor);
                        ((ILineSymbol)styleGalleryItem.Item).Width=(this.lineWidth);
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
            return this.GetTextElement(dblAngle, sNoteText, PosPt, this.fontColor);
        }

        public IElement GetTextElement(double dblAngle, string sNoteText, IPoint PosPt, int iColor)
        {
            IRgbColor rgbColor = new RgbColor();
            rgbColor.RGB=(iColor);
            IFont font = new SystemFont() as IFont;
            font.Name = this.fontName;
            font.Size = (decimal)this.fontSize;
            font.Bold = this.fontBold;
            font.Italic = this.fontItalic;
            font.Underline = this.fontUnderline;
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
            else if (Geo.GeometryType == esriGeometryType.esriGeometryPolyline)
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