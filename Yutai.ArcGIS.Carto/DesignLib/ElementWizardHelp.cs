using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ElementWizardHelp
    {
        private static ElementType m_ElementType;
        private static bool m_HasElementSelect;
        private static bool m_IsVerticalText;
        private static string m_PictureFileName;
        private static string m_pLegendInfo;
        private static string m_Position;
        private static int m_posoitiontype;
        private static object m_pStyle;
        private static string m_Text;

        static ElementWizardHelp()
        {
            old_acctor_mc();
        }

        public static IElement CreateElement(IPageLayout ipageLayout_0)
        {
            int num;
            double num2;
            double num3;
            ITextSymbol symbol;
            esriTextHorizontalAlignment horizontalAlignment;
            esriTextVerticalAlignment verticalAlignment;
            UID uid;
            IMapFrame frame;
            IEnvelope envelope;
            IEnvelope envelope2;
            IPoint point = GetPosition(ipageLayout_0, out num, out num2, out num3);
            IElement element = null;
            switch (m_ElementType)
            {
                case ElementType.ConstantText:
                    element = new TextElementClass();
                    (element as IElementProperties).Name = m_Text;
                    (element as ITextElement).Text = m_Text;
                    if (m_pStyle != null)
                    {
                        SetTextSymblAlign(m_pStyle as ITextSymbol, num);
                        (element as ITextElement).Symbol = m_pStyle as ITextSymbol;
                    }
                    element.Geometry = point;
                    if (!m_IsVerticalText)
                    {
                        (element as IElementProperties).Type = "";
                        symbol = (element as ITextElement).Symbol;
                        if (symbol.Angle == 270.0)
                        {
                            (symbol as ICharacterOrientation).CJKCharactersRotation = false;
                            symbol.Angle = 0.0;
                            horizontalAlignment = symbol.HorizontalAlignment;
                            verticalAlignment = symbol.VerticalAlignment;
                            switch (horizontalAlignment)
                            {
                                case esriTextHorizontalAlignment.esriTHALeft:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                                    break;

                                case esriTextHorizontalAlignment.esriTHAFull:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                                    break;

                                case esriTextHorizontalAlignment.esriTHARight:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                                    break;

                                case esriTextHorizontalAlignment.esriTHACenter:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                                    break;
                            }
                            if (verticalAlignment == esriTextVerticalAlignment.esriTVATop)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVABottom)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVACenter)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVABaseline)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                            }
                            (element as ITextElement).Symbol = symbol;
                        }
                        return element;
                    }
                    (element as IElementProperties).Type = "竖向";
                    symbol = (element as ITextElement).Symbol;
                    (symbol as ICharacterOrientation).CJKCharactersRotation = true;
                    symbol.Angle = 270.0;
                    horizontalAlignment = symbol.HorizontalAlignment;
                    verticalAlignment = symbol.VerticalAlignment;
                    switch (horizontalAlignment)
                    {
                        case esriTextHorizontalAlignment.esriTHALeft:
                            symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                            break;

                        case esriTextHorizontalAlignment.esriTHAFull:
                            symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                            break;

                        case esriTextHorizontalAlignment.esriTHARight:
                            symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            break;

                        case esriTextHorizontalAlignment.esriTHACenter:
                            symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                            break;
                    }
                    if (verticalAlignment == esriTextVerticalAlignment.esriTVATop)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVABottom)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVACenter)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVABaseline)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    }
                    (element as ITextElement).Symbol = symbol;
                    return element;

                case ElementType.SingleText:
                    element = new TextElementClass();
                    (element as IElementProperties).Name = m_Text;
                    (element as ITextElement).Text = "=" + m_Text;
                    if (m_pStyle != null)
                    {
                        SetTextSymblAlign(m_pStyle as ITextSymbol, num);
                        (element as ITextElement).Symbol = m_pStyle as ITextSymbol;
                    }
                    element.Geometry = point;
                    if (!m_IsVerticalText)
                    {
                        (element as IElementProperties).Type = "";
                        symbol = (element as ITextElement).Symbol;
                        if (symbol.Angle == 270.0)
                        {
                            (symbol as ICharacterOrientation).CJKCharactersRotation = false;
                            symbol.Angle = 0.0;
                            horizontalAlignment = symbol.HorizontalAlignment;
                            verticalAlignment = symbol.VerticalAlignment;
                            switch (horizontalAlignment)
                            {
                                case esriTextHorizontalAlignment.esriTHALeft:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                                    break;

                                case esriTextHorizontalAlignment.esriTHAFull:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                                    break;

                                case esriTextHorizontalAlignment.esriTHARight:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                                    break;

                                case esriTextHorizontalAlignment.esriTHACenter:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                                    break;
                            }
                            if (verticalAlignment == esriTextVerticalAlignment.esriTVATop)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVABottom)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVACenter)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVABaseline)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                            }
                            (element as ITextElement).Symbol = symbol;
                        }
                        return element;
                    }
                    (element as IElementProperties).Type = "竖向";
                    symbol = (element as ITextElement).Symbol;
                    (symbol as ICharacterOrientation).CJKCharactersRotation = true;
                    symbol.Angle = 270.0;
                    horizontalAlignment = symbol.HorizontalAlignment;
                    verticalAlignment = symbol.VerticalAlignment;
                    switch (horizontalAlignment)
                    {
                        case esriTextHorizontalAlignment.esriTHALeft:
                            symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            break;

                        case esriTextHorizontalAlignment.esriTHAFull:
                            symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            break;

                        case esriTextHorizontalAlignment.esriTHARight:
                            symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                            break;

                        case esriTextHorizontalAlignment.esriTHACenter:
                            symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                            break;
                    }
                    if (verticalAlignment == esriTextVerticalAlignment.esriTVATop)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVABottom)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVACenter)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVABaseline)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    }
                    (element as ITextElement).Symbol = symbol;
                    return element;

                case ElementType.MultiText:
                    element = new TextElementClass();
                    (element as IElementProperties).Name = m_Text;
                    (element as ITextElement).Text = "M=" + m_Text;
                    if (m_pStyle != null)
                    {
                        SetTextSymblAlign(m_pStyle as ITextSymbol, num);
                        (element as ITextElement).Symbol = m_pStyle as ITextSymbol;
                    }
                    element.Geometry = point;
                    if (!m_IsVerticalText)
                    {
                        (element as IElementProperties).Type = "";
                        symbol = (element as ITextElement).Symbol;
                        if (symbol.Angle == 270.0)
                        {
                            (symbol as ICharacterOrientation).CJKCharactersRotation = false;
                            symbol.Angle = 0.0;
                            horizontalAlignment = symbol.HorizontalAlignment;
                            verticalAlignment = symbol.VerticalAlignment;
                            switch (horizontalAlignment)
                            {
                                case esriTextHorizontalAlignment.esriTHALeft:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                                    break;

                                case esriTextHorizontalAlignment.esriTHAFull:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                                    break;

                                case esriTextHorizontalAlignment.esriTHARight:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                                    break;

                                case esriTextHorizontalAlignment.esriTHACenter:
                                    symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                                    break;
                            }
                            if (verticalAlignment == esriTextVerticalAlignment.esriTVATop)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVABottom)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVACenter)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                            }
                            else if (verticalAlignment == esriTextVerticalAlignment.esriTVABaseline)
                            {
                                symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                            }
                            (element as ITextElement).Symbol = symbol;
                        }
                        return element;
                    }
                    (element as IElementProperties).Type = "竖向";
                    symbol = (element as ITextElement).Symbol;
                    (symbol as ICharacterOrientation).CJKCharactersRotation = true;
                    symbol.Angle = 270.0;
                    horizontalAlignment = symbol.HorizontalAlignment;
                    verticalAlignment = symbol.VerticalAlignment;
                    if (horizontalAlignment == esriTextHorizontalAlignment.esriTHALeft)
                    {
                        symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    }
                    else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHAFull)
                    {
                        symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    }
                    else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHARight)
                    {
                        symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    }
                    else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHACenter)
                    {
                        symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                    }
                    if (verticalAlignment == esriTextVerticalAlignment.esriTVATop)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVABottom)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVACenter)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                    }
                    else if (verticalAlignment == esriTextVerticalAlignment.esriTVABaseline)
                    {
                        symbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    }
                    (element as ITextElement).Symbol = symbol;
                    return element;

                case ElementType.JoinTable:
                {
                    JionTab tab = new JionTab();
                    return tab.CreateJionTab(ipageLayout_0 as IActiveView, point);
                }
                case ElementType.ScaleText:
                {
                    IScaleText pStyle;
                    if (m_pStyle == null)
                    {
                        pStyle = new ScaleTextClass();
                    }
                    else
                    {
                        pStyle = m_pStyle as IScaleText;
                    }
                    uid = new UIDClass {
                        Value = "esriCarto.ScaleText"
                    };
                    frame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                    pStyle.MapUnits = frame.Map.DistanceUnits;
                    pStyle.PageUnits = ipageLayout_0.Page.Units;
                    pStyle.Style = esriScaleTextStyleEnum.esriScaleTextAbsolute;
                    INumberFormat numberFormat = pStyle.NumberFormat;
                    if (numberFormat is INumericFormat)
                    {
                        (numberFormat as INumericFormat).RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
                        (numberFormat as INumericFormat).UseSeparator = false;
                        (numberFormat as INumericFormat).RoundingValue = 0;
                        pStyle.NumberFormat = numberFormat;
                    }
                    element = frame.CreateSurroundFrame(uid, pStyle) as IElement;
                    envelope = new EnvelopeClass();
                    envelope.PutCoords(point.X, point.Y, point.X + 4.0, point.Y + 8.0);
                    envelope2 = new EnvelopeClass();
                    pStyle.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelope, envelope2);
                    envelope.PutCoords(point.X, point.Y, point.X + envelope2.Width, point.Y + envelope2.Height);
                    element.Geometry = envelope;
                    return element;
                }
                case ElementType.CustomLegend:
                {
                    CustomLegend legend = new CustomLegend {
                        LegendInfo = m_pLegendInfo
                    };
                    legend.Init(ipageLayout_0 as IActiveView, point);
                    return legend;
                }
                case ElementType.Legend:
                    ILegend legend2;
                    if (m_pStyle == null)
                    {
                        legend2 = new LegendClass_2();
                    }
                    else
                    {
                        legend2 = m_pStyle as ILegend;
                    }
                    legend2.AutoAdd = true;
                    legend2.AutoReorder = true;
                    legend2.AutoVisibility = true;
                    uid = new UIDClass {
                        Value = "esriCarto.Legend"
                    };
                    frame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                    element = frame.CreateSurroundFrame(uid, legend2) as IElement;
                    envelope = new EnvelopeClass();
                    envelope.PutCoords(point.X, point.Y, point.X + 4.0, point.Y + 8.0);
                    envelope2 = new EnvelopeClass();
                    legend2.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelope, envelope2);
                    envelope.PutCoords(point.X, point.Y, point.X + envelope2.Width, point.Y + envelope2.Height);
                    element.Geometry = envelope;
                    return element;

                case ElementType.Picture:
                {
                    string str = System.IO.Path.GetExtension(m_PictureFileName).ToLower();
                    element = null;
                    string str2 = str;
                    switch (str2)
                    {
                        case null:
                            goto Label_0B45;

                        case ".bmp":
                            element = new BmpPictureElementClass();
                            goto Label_0B4C;

                        case ".jpg":
                            element = new JpgPictureElementClass();
                            goto Label_0B4C;

                        case ".gif":
                            element = new GifPictureElementClass();
                            goto Label_0B4C;

                        case ".tif":
                            element = new TifPictureElementClass();
                            goto Label_0B4C;
                    }
                    if (str2 != ".emf")
                    {
                        goto Label_0B45;
                    }
                    element = new EmfPictureElementClass();
                    goto Label_0B4C;
                }
                case ElementType.OLE:
                {
                    int hWnd = (ipageLayout_0 as IActiveView).ScreenDisplay.hWnd;
                    JLK.ExtendClass.IOleFrame frame3 = new OleFrame();
                    if (frame3.CreateOleClientItem((ipageLayout_0 as IActiveView).ScreenDisplay, hWnd))
                    {
                        element = frame3 as IElement;
                        envelope = new EnvelopeClass();
                        envelope.PutCoords(point.X, point.Y, point.X + 4.0, point.Y + 8.0);
                        envelope2 = new EnvelopeClass();
                        element.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelope2);
                        envelope.PutCoords(point.X, point.Y, point.X + envelope2.Width, point.Y + envelope2.Height);
                        element.Geometry = envelope;
                    }
                    return element;
                }
                case ElementType.ScaleBar:
                    IScaleBar bar;
                    if (m_pStyle == null)
                    {
                        bar = new HollowScaleBarClass();
                    }
                    else
                    {
                        bar = m_pStyle as IScaleBar;
                    }
                    uid = new UIDClass {
                        Value = "esriCarto.ScaleBar"
                    };
                    frame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                    bar.Units = frame.Map.DistanceUnits;
                    element = frame.CreateSurroundFrame(uid, bar) as IElement;
                    envelope = new EnvelopeClass();
                    envelope.PutCoords(point.X, point.Y, point.X + 4.0, point.Y + 8.0);
                    envelope2 = new EnvelopeClass();
                    bar.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelope, envelope2);
                    envelope.PutCoords(point.X, point.Y, point.X + envelope2.Width, point.Y + envelope2.Height);
                    element.Geometry = envelope;
                    return element;

                case ElementType.North:
                    INorthArrow arrow;
                    if (m_pStyle == null)
                    {
                        arrow = new MarkerNorthArrowClass();
                    }
                    else
                    {
                        arrow = m_pStyle as INorthArrow;
                    }
                    envelope = new EnvelopeClass();
                    envelope.PutCoords(point.X, point.Y, point.X + 3.0, point.Y + 3.0);
                    uid = new UIDClass {
                        Value = "esriCarto.MarkerNorthArrow"
                    };
                    frame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                    element = frame.CreateSurroundFrame(uid, arrow) as IElement;
                    envelope2 = new EnvelopeClass();
                    arrow.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelope, envelope2);
                    envelope.PutCoords(point.X, point.Y, point.X + envelope2.Width, point.Y + envelope2.Height);
                    element.Geometry = envelope;
                    return element;

                case ElementType.GraphicElement:
                    return element;

                case ElementType.DataGraphicElement:
                    envelope = new EnvelopeClass();
                    envelope.PutCoords(point.X, point.Y, point.X + 8.0, point.Y + 8.0);
                    return new DataGraphicsElement { Geometry = envelope };
            }
            return element;
        Label_0B45:
            element = new PngPictureElementClass();
        Label_0B4C:
            (element as IPictureElement).ImportPictureFromFile(m_PictureFileName);
            (element as IPictureElement).MaintainAspectRatio = true;
            double widthPoints = 0.0;
            double heightPoints = 0.0;
            (element as IPictureElement2).QueryIntrinsicSize(ref widthPoints, ref heightPoints);
            widthPoints *= 0.0353;
            heightPoints *= 0.0353;
            (element as IElementProperties2).AutoTransform = true;
            envelope = new EnvelopeClass();
            envelope.PutCoords(point.X, point.Y, point.X + widthPoints, point.Y + heightPoints);
            element.Geometry = envelope;
            return element;
        }

        private static IPoint GetPosition(IPageLayout ipageLayout_0, out int int_0, out double double_0, out double double_1)
        {
            int_0 = 0;
            double_0 = 0.0;
            double_1 = 0.0;
            IPoint point = new PointClass();
            point.PutCoords(0.0, 0.0);
            try
            {
                IPoint upperLeft;
                XmlDocument document = new XmlDocument();
                document.LoadXml(m_Position);
                XmlNode node = document.ChildNodes[0];
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode node2 = node.ChildNodes[i];
                    string str = node2.Attributes["name"].Value;
                    string s = node2.Attributes["value"].Value;
                    switch (str)
                    {
                        case "position":
                            int_0 = int.Parse(s);
                            break;

                        case "xoffset":
                            double_0 = double.Parse(s);
                            break;

                        case "yoffset":
                            double_1 = double.Parse(s);
                            break;
                    }
                }
                IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
                IElement element = CommandFunction.FindElementByType(ipageLayout_0, "外框");
                IEnvelope bounds = null;
                if (element != null)
                {
                    bounds = new EnvelopeClass();
                    element.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                }
                if (bounds == null)
                {
                    container.Reset();
                    IMapFrame frame = container.FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                    bounds = (frame as IElement).Geometry.Envelope;
                }
                m_posoitiontype = int_0;
                switch (int_0)
                {
                    case 0:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 1:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax) / 2.0, bounds.YMax);
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 2:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 3:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 4:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 5:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMin, (bounds.YMin + bounds.YMax) / 2.0);
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 6:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMax, (bounds.YMin + bounds.YMax) / 2.0);
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 7:
                        upperLeft = bounds.LowerLeft;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 8:
                        upperLeft = bounds.LowerRight;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 9:
                        upperLeft = bounds.LowerLeft;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 10:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax) / 2.0, bounds.YMin);
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;

                    case 11:
                        upperLeft = bounds.LowerRight;
                        point.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                        return point;
                }
                return point;
            }
            catch
            {
            }
            return point;
        }

        public static string GetTextElementValue()
        {
            return "";
        }

        public static void Init()
        {
            m_ElementType = ElementType.ConstantText;
            m_Position = "";
            m_IsVerticalText = false;
            m_Text = "";
            m_pLegendInfo = "";
            m_PictureFileName = "";
            m_HasElementSelect = true;
        }

        private static void old_acctor_mc()
        {
            m_ElementType = ElementType.ConstantText;
            m_Position = "";
            m_pLegendInfo = "";
            m_HasElementSelect = true;
            m_PictureFileName = "";
            m_IsVerticalText = false;
            m_pStyle = new TextSymbolClass();
            m_Text = "";
            m_posoitiontype = -1;
        }

        private static void SetTextSymblAlign(ITextSymbol itextSymbol_0, int int_0)
        {
            switch (int_0)
            {
                case 0:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case 1:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case 2:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case 3:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;

                case 4:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;

                case 5:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                    break;

                case 6:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                    break;

                case 7:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case 8:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case 9:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;

                case 10:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;

                case 11:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;
            }
        }

        public static ElementType ElementType
        {
            get
            {
                return m_ElementType;
            }
            set
            {
                m_ElementType = value;
                if (m_ElementType == ElementType.ConstantText)
                {
                    m_pStyle = new TextSymbolClass();
                }
                else if (m_ElementType == ElementType.SingleText)
                {
                    m_pStyle = new TextSymbolClass();
                }
                else if (m_ElementType == ElementType.MultiText)
                {
                    m_pStyle = new TextSymbolClass();
                }
                else if (m_ElementType == ElementType.JoinTable)
                {
                    m_pStyle = new TextSymbolClass();
                }
                else if (m_ElementType == ElementType.ScaleText)
                {
                    m_pStyle = new ScaleTextClass();
                }
                else if (m_ElementType == ElementType.ScaleBar)
                {
                    m_pStyle = new HollowScaleBarClass();
                }
                else if (m_ElementType == ElementType.Legend)
                {
                    m_pStyle = new LegendClass_2();
                }
                else if (m_ElementType == ElementType.North)
                {
                    m_pStyle = new MarkerNorthArrowClass();
                }
                else if (m_ElementType != ElementType.DataGraphicElement)
                {
                }
            }
        }

        public static bool HasElementSelect
        {
            get
            {
                return m_HasElementSelect;
            }
            set
            {
                m_HasElementSelect = value;
            }
        }

        public static bool IsVerticalText
        {
            get
            {
                return m_IsVerticalText;
            }
            set
            {
                m_IsVerticalText = value;
            }
        }

        public static string LegendInfo
        {
            get
            {
                return m_pLegendInfo;
            }
            set
            {
                m_pLegendInfo = value;
            }
        }

        public static string PictureFileName
        {
            set
            {
                m_PictureFileName = value;
            }
        }

        public static string Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        public static object Style
        {
            get
            {
                return m_pStyle;
            }
            set
            {
                m_pStyle = value;
            }
        }

        public static string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
            }
        }
    }
}

