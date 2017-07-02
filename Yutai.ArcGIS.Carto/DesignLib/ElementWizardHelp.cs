using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.ExtendClass;

namespace Yutai.ArcGIS.Carto.DesignLib
{
    public class ElementWizardHelp
    {
        private static ElementType m_ElementType;

        private static string m_Position;

        private static string m_pLegendInfo;

        private static bool m_HasElementSelect;

        private static string m_PictureFileName;

        private static bool m_IsVerticalText;

        private static object m_pStyle;

        private static string m_Text;

        private static int m_posoitiontype;

        public static ElementType ElementType
        {
            get
            {
                return ElementWizardHelp.m_ElementType;
            }
            set
            {
                ElementWizardHelp.m_ElementType = value;
                if (ElementWizardHelp.m_ElementType == ElementType.ConstantText)
                {
                    ElementWizardHelp.m_pStyle = new TextSymbolClass();
                }
                else if (ElementWizardHelp.m_ElementType == ElementType.SingleText)
                {
                    ElementWizardHelp.m_pStyle = new TextSymbolClass();
                }
                else if (ElementWizardHelp.m_ElementType == ElementType.MultiText)
                {
                    ElementWizardHelp.m_pStyle = new TextSymbolClass();
                }
                else if (ElementWizardHelp.m_ElementType == ElementType.JoinTable)
                {
                    ElementWizardHelp.m_pStyle = new TextSymbolClass();
                }
                else if (ElementWizardHelp.m_ElementType == ElementType.ScaleText)
                {
                    ElementWizardHelp.m_pStyle = new ScaleTextClass();
                }
                else if (ElementWizardHelp.m_ElementType == ElementType.ScaleBar)
                {
                    ElementWizardHelp.m_pStyle = new HollowScaleBarClass();
                }
                else if (ElementWizardHelp.m_ElementType == ElementType.Legend)
                {
                    ElementWizardHelp.m_pStyle = new LegendClass_2();
                }
                else if (ElementWizardHelp.m_ElementType != ElementType.North)
                {
                    ElementWizardHelp.m_ElementType = ElementType.DataGraphicElement;
                }
                else
                {
                    ElementWizardHelp.m_pStyle = new MarkerNorthArrowClass();
                }
            }
        }

        public static bool HasElementSelect
        {
            get
            {
                return ElementWizardHelp.m_HasElementSelect;
            }
            set
            {
                ElementWizardHelp.m_HasElementSelect = value;
            }
        }

        public static bool IsVerticalText
        {
            get
            {
                return ElementWizardHelp.m_IsVerticalText;
            }
            set
            {
                ElementWizardHelp.m_IsVerticalText = value;
            }
        }

        public static string LegendInfo
        {
            get
            {
                return ElementWizardHelp.m_pLegendInfo;
            }
            set
            {
                ElementWizardHelp.m_pLegendInfo = value;
            }
        }

        public static string PictureFileName
        {
            set
            {
                ElementWizardHelp.m_PictureFileName = value;
            }
        }

        public static string Position
        {
            get
            {
                return ElementWizardHelp.m_Position;
            }
            set
            {
                ElementWizardHelp.m_Position = value;
            }
        }

        public static object Style
        {
            get
            {
                return ElementWizardHelp.m_pStyle;
            }
            set
            {
                ElementWizardHelp.m_pStyle = value;
            }
        }

        public static string Text
        {
            get
            {
                return ElementWizardHelp.m_Text;
            }
            set
            {
                ElementWizardHelp.m_Text = value;
            }
        }

        static ElementWizardHelp()
        {
            ElementWizardHelp.old_acctor_mc();
        }

        public ElementWizardHelp()
        {
        }

        public static IElement CreateElement(IPageLayout ipageLayout_0)
        {
            int num;
            double num1;
            double num2;
            ITextSymbol symbol;
            esriTextHorizontalAlignment horizontalAlignment;
            esriTextVerticalAlignment verticalAlignment;
            IScaleText scaleTextClass;
            UID uIDClass;
            IMapFrame mapFrame;
            IEnvelope envelopeClass;
            IEnvelope envelope;
            ILegend legendClass2;
            IScaleBar hollowScaleBarClass;
            double num3;
            double num4;
            INorthArrow markerNorthArrowClass;
            IPoint position = ElementWizardHelp.GetPosition(ipageLayout_0, out num, out num1, out num2);
            IElement textElementClass = null;
            switch (ElementWizardHelp.m_ElementType)
            {
                case ElementType.ConstantText:
                    {
                        textElementClass = new TextElementClass();
                        (textElementClass as IElementProperties).Name = ElementWizardHelp.m_Text;
                        (textElementClass as ITextElement).Text = ElementWizardHelp.m_Text;
                        if (ElementWizardHelp.m_pStyle != null)
                        {
                            ElementWizardHelp.SetTextSymblAlign(ElementWizardHelp.m_pStyle as ITextSymbol, num);
                            (textElementClass as ITextElement).Symbol = ElementWizardHelp.m_pStyle as ITextSymbol;
                        }
                        textElementClass.Geometry = position;
                        if (!ElementWizardHelp.m_IsVerticalText)
                        {
                            (textElementClass as IElementProperties).Type = "";
                            symbol = (textElementClass as ITextElement).Symbol;
                            if (symbol.Angle != 270)
                            {
                                return textElementClass;
                            }
                            (symbol as ICharacterOrientation).CJKCharactersRotation = false;
                            symbol.Angle = 0;
                            horizontalAlignment = symbol.HorizontalAlignment;
                            verticalAlignment = symbol.VerticalAlignment;
                            if (horizontalAlignment == esriTextHorizontalAlignment.esriTHALeft)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            }
                            else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHAFull)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            }
                            else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHARight)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
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
                            (textElementClass as ITextElement).Symbol = symbol;
                            return textElementClass;
                        }
                        else
                        {
                            (textElementClass as IElementProperties).Type = "竖向";
                            symbol = (textElementClass as ITextElement).Symbol;
                            (symbol as ICharacterOrientation).CJKCharactersRotation = true;
                            symbol.Angle = 270;
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
                            (textElementClass as ITextElement).Symbol = symbol;
                            return textElementClass;
                        }
                    }
                case ElementType.SingleText:
                    {
                        textElementClass = new TextElementClass();
                        (textElementClass as IElementProperties).Name = ElementWizardHelp.m_Text;
                        (textElementClass as ITextElement).Text = string.Concat("=", ElementWizardHelp.m_Text);
                        if (ElementWizardHelp.m_pStyle != null)
                        {
                            ElementWizardHelp.SetTextSymblAlign(ElementWizardHelp.m_pStyle as ITextSymbol, num);
                            (textElementClass as ITextElement).Symbol = ElementWizardHelp.m_pStyle as ITextSymbol;
                        }
                        textElementClass.Geometry = position;
                        if (!ElementWizardHelp.m_IsVerticalText)
                        {
                            (textElementClass as IElementProperties).Type = "";
                            symbol = (textElementClass as ITextElement).Symbol;
                            if (symbol.Angle != 270)
                            {
                                return textElementClass;
                            }
                            (symbol as ICharacterOrientation).CJKCharactersRotation = false;
                            symbol.Angle = 0;
                            horizontalAlignment = symbol.HorizontalAlignment;
                            verticalAlignment = symbol.VerticalAlignment;
                            if (horizontalAlignment == esriTextHorizontalAlignment.esriTHALeft)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            }
                            else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHAFull)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            }
                            else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHARight)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
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
                            (textElementClass as ITextElement).Symbol = symbol;
                            return textElementClass;
                        }
                        else
                        {
                            (textElementClass as IElementProperties).Type = "竖向";
                            symbol = (textElementClass as ITextElement).Symbol;
                            (symbol as ICharacterOrientation).CJKCharactersRotation = true;
                            symbol.Angle = 270;
                            horizontalAlignment = symbol.HorizontalAlignment;
                            verticalAlignment = symbol.VerticalAlignment;
                            if (horizontalAlignment == esriTextHorizontalAlignment.esriTHALeft)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            }
                            else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHAFull)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            }
                            else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHARight)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
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
                            (textElementClass as ITextElement).Symbol = symbol;
                            return textElementClass;
                        }
                    }
                case ElementType.MultiText:
                    {
                        textElementClass = new TextElementClass();
                        (textElementClass as IElementProperties).Name = ElementWizardHelp.m_Text;
                        (textElementClass as ITextElement).Text = string.Concat("M=", ElementWizardHelp.m_Text);
                        if (ElementWizardHelp.m_pStyle != null)
                        {
                            ElementWizardHelp.SetTextSymblAlign(ElementWizardHelp.m_pStyle as ITextSymbol, num);
                            (textElementClass as ITextElement).Symbol = ElementWizardHelp.m_pStyle as ITextSymbol;
                        }
                        textElementClass.Geometry = position;
                        if (!ElementWizardHelp.m_IsVerticalText)
                        {
                            (textElementClass as IElementProperties).Type = "";
                            symbol = (textElementClass as ITextElement).Symbol;
                            if (symbol.Angle != 270)
                            {
                                return textElementClass;
                            }
                            (symbol as ICharacterOrientation).CJKCharactersRotation = false;
                            symbol.Angle = 0;
                            horizontalAlignment = symbol.HorizontalAlignment;
                            verticalAlignment = symbol.VerticalAlignment;
                            if (horizontalAlignment == esriTextHorizontalAlignment.esriTHALeft)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            }
                            else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHAFull)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                            }
                            else if (horizontalAlignment == esriTextHorizontalAlignment.esriTHARight)
                            {
                                symbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
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
                            (textElementClass as ITextElement).Symbol = symbol;
                            return textElementClass;
                        }
                        else
                        {
                            (textElementClass as IElementProperties).Type = "竖向";
                            symbol = (textElementClass as ITextElement).Symbol;
                            (symbol as ICharacterOrientation).CJKCharactersRotation = true;
                            symbol.Angle = 270;
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
                            (textElementClass as ITextElement).Symbol = symbol;
                            return textElementClass;
                        }
                    }
                case ElementType.JoinTable:
                    {
                        textElementClass = (new JionTab()).CreateJionTab(ipageLayout_0 as IActiveView, position);
                        return textElementClass;
                    }
                case ElementType.ScaleText:
                    {
                        if (ElementWizardHelp.m_pStyle == null)
                        {
                            scaleTextClass = new ScaleTextClass();
                        }
                        else
                        {
                            scaleTextClass = ElementWizardHelp.m_pStyle as IScaleText;
                        }
                        uIDClass = new UIDClass()
                        {
                            Value = "esriCarto.ScaleText"
                        };
                        mapFrame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                        scaleTextClass.MapUnits = mapFrame.Map.DistanceUnits;
                        scaleTextClass.PageUnits = ipageLayout_0.Page.Units;
                        scaleTextClass.Style = esriScaleTextStyleEnum.esriScaleTextAbsolute;
                        INumberFormat numberFormat = scaleTextClass.NumberFormat;
                        if (numberFormat is INumericFormat)
                        {
                            (numberFormat as INumericFormat).RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
                            (numberFormat as INumericFormat).UseSeparator = false;
                            (numberFormat as INumericFormat).RoundingValue = 0;
                            scaleTextClass.NumberFormat = numberFormat;
                        }
                        textElementClass = mapFrame.CreateSurroundFrame(uIDClass, scaleTextClass) as IElement;
                        envelopeClass = new EnvelopeClass();
                        envelopeClass.PutCoords(position.X, position.Y, position.X + 4, position.Y + 8);
                        envelope = new EnvelopeClass();
                        scaleTextClass.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelopeClass, envelope);
                        envelopeClass.PutCoords(position.X, position.Y, position.X + envelope.Width, position.Y + envelope.Height);
                        textElementClass.Geometry = envelopeClass;
                        return textElementClass;
                    }
                case ElementType.CustomLegend:
                    {
                        CustomLegend customLegend = new CustomLegend()
                        {
                            LegendInfo = ElementWizardHelp.m_pLegendInfo
                        };
                        customLegend.Init(ipageLayout_0 as IActiveView, position);
                        textElementClass = customLegend;
                        return textElementClass;
                    }
                case ElementType.Legend:
                    {
                        if (ElementWizardHelp.m_pStyle == null)
                        {
                            legendClass2 = new LegendClass_2();
                        }
                        else
                        {
                            legendClass2 = ElementWizardHelp.m_pStyle as ILegend;
                        }
                        legendClass2.AutoAdd = true;
                        legendClass2.AutoReorder = true;
                        legendClass2.AutoVisibility = true;
                        uIDClass = new UIDClass()
                        {
                            Value = "esriCarto.Legend"
                        };
                        mapFrame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                        textElementClass = mapFrame.CreateSurroundFrame(uIDClass, legendClass2) as IElement;
                        envelopeClass = new EnvelopeClass();
                        envelopeClass.PutCoords(position.X, position.Y, position.X + 4, position.Y + 8);
                        envelope = new EnvelopeClass();
                        legendClass2.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelopeClass, envelope);
                        envelopeClass.PutCoords(position.X, position.Y, position.X + envelope.Width, position.Y + envelope.Height);
                        textElementClass.Geometry = envelopeClass;
                        return textElementClass;
                    }
                case ElementType.Picture:
                    {
                        string lower = System.IO.Path.GetExtension(ElementWizardHelp.m_PictureFileName).ToLower();
                        textElementClass = null;
                        string str = lower;
                        if (string.IsNullOrEmpty(str))
                        {
                            textElementClass = new PngPictureElementClass();
                            (textElementClass as IPictureElement).ImportPictureFromFile(ElementWizardHelp.m_PictureFileName);
                            (textElementClass as IPictureElement).MaintainAspectRatio = true;
                            num3 = 0;
                            num4 = 0;
                            (textElementClass as IPictureElement2).QueryIntrinsicSize(ref num3, ref num4);
                            num3 = num3 * 0.0353;
                            num4 = num4 * 0.0353;
                            (textElementClass as IElementProperties2).AutoTransform = true;
                            envelopeClass = new EnvelopeClass();
                            envelopeClass.PutCoords(position.X, position.Y, position.X + num3, position.Y + num4);
                            textElementClass.Geometry = envelopeClass;
                            return textElementClass;
                        }
                        else if (str == ".bmp")
                        {
                            textElementClass = new BmpPictureElementClass();
                        }
                        else if (str == ".jpg")
                        {
                            textElementClass = new JpgPictureElementClass();
                        }
                        else if (str == ".gif")
                        {
                            textElementClass = new GifPictureElementClass();
                        }
                        else if (str == ".tif")
                        {
                            textElementClass = new TifPictureElementClass();
                        }
                        else
                        {
                            if (str != ".emf")
                            {
                                textElementClass = new PngPictureElementClass();
                                (textElementClass as IPictureElement).ImportPictureFromFile(ElementWizardHelp.m_PictureFileName);
                                (textElementClass as IPictureElement).MaintainAspectRatio = true;
                                num3 = 0;
                                num4 = 0;
                                (textElementClass as IPictureElement2).QueryIntrinsicSize(ref num3, ref num4);
                                num3 = num3 * 0.0353;
                                num4 = num4 * 0.0353;
                                (textElementClass as IElementProperties2).AutoTransform = true;
                                envelopeClass = new EnvelopeClass();
                                envelopeClass.PutCoords(position.X, position.Y, position.X + num3, position.Y + num4);
                                textElementClass.Geometry = envelopeClass;
                                return textElementClass;
                            }
                            textElementClass = new EmfPictureElementClass();
                        }
                    (textElementClass as IPictureElement).ImportPictureFromFile(ElementWizardHelp.m_PictureFileName);
                        (textElementClass as IPictureElement).MaintainAspectRatio = true;
                        num3 = 0;
                        num4 = 0;
                        (textElementClass as IPictureElement2).QueryIntrinsicSize(ref num3, ref num4);
                        num3 = num3 * 0.0353;
                        num4 = num4 * 0.0353;
                        (textElementClass as IElementProperties2).AutoTransform = true;
                        envelopeClass = new EnvelopeClass();
                        envelopeClass.PutCoords(position.X, position.Y, position.X + num3, position.Y + num4);
                        textElementClass.Geometry = envelopeClass;
                        return textElementClass;
                    }
                case ElementType.OLE:
                    {
                        int screenDisplay = (ipageLayout_0 as IActiveView).ScreenDisplay.hWnd;
                        Common.ExtendClass.IOleFrame oleFrame = new OleFrame();
                        if (!oleFrame.CreateOleClientItem((ipageLayout_0 as IActiveView).ScreenDisplay, screenDisplay))
                        {
                            return textElementClass;
                        }
                        textElementClass = oleFrame as IElement;
                        envelopeClass = new EnvelopeClass();
                        envelopeClass.PutCoords(position.X, position.Y, position.X + 4, position.Y + 8);
                        envelope = new EnvelopeClass();
                        textElementClass.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelope);
                        envelopeClass.PutCoords(position.X, position.Y, position.X + envelope.Width, position.Y + envelope.Height);
                        textElementClass.Geometry = envelopeClass;
                        return textElementClass;
                    }
                case ElementType.ScaleBar:
                    {
                        if (ElementWizardHelp.m_pStyle == null)
                        {
                            hollowScaleBarClass = new HollowScaleBarClass();
                        }
                        else
                        {
                            hollowScaleBarClass = ElementWizardHelp.m_pStyle as IScaleBar;
                        }
                        uIDClass = new UIDClass()
                        {
                            Value = "esriCarto.ScaleBar"
                        };
                        mapFrame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                        hollowScaleBarClass.Units = mapFrame.Map.DistanceUnits;
                        textElementClass = mapFrame.CreateSurroundFrame(uIDClass, hollowScaleBarClass) as IElement;
                        envelopeClass = new EnvelopeClass();
                        envelopeClass.PutCoords(position.X, position.Y, position.X + 4, position.Y + 8);
                        envelope = new EnvelopeClass();
                        hollowScaleBarClass.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelopeClass, envelope);
                        envelopeClass.PutCoords(position.X, position.Y, position.X + envelope.Width, position.Y + envelope.Height);
                        textElementClass.Geometry = envelopeClass;
                        return textElementClass;
                    }
                case ElementType.North:
                    {
                        if (ElementWizardHelp.m_pStyle == null)
                        {
                            markerNorthArrowClass = new MarkerNorthArrowClass();
                        }
                        else
                        {
                            markerNorthArrowClass = ElementWizardHelp.m_pStyle as INorthArrow;
                        }
                        envelopeClass = new EnvelopeClass();
                        envelopeClass.PutCoords(position.X, position.Y, position.X + 3, position.Y + 3);
                        uIDClass = new UIDClass()
                        {
                            Value = "esriCarto.MarkerNorthArrow"
                        };
                        mapFrame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                        textElementClass = mapFrame.CreateSurroundFrame(uIDClass, markerNorthArrowClass) as IElement;
                        envelope = new EnvelopeClass();
                        markerNorthArrowClass.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelopeClass, envelope);
                        envelopeClass.PutCoords(position.X, position.Y, position.X + envelope.Width, position.Y + envelope.Height);
                        textElementClass.Geometry = envelopeClass;
                        return textElementClass;
                    }
                case ElementType.GraphicElement:
                    {
                        return textElementClass;
                    }
                case ElementType.DataGraphicElement:
                    {
                        envelopeClass = new EnvelopeClass();
                        envelopeClass.PutCoords(position.X, position.Y, position.X + 8, position.Y + 8);
                        textElementClass = new DataGraphicsElement()
                        {
                            Geometry = envelopeClass
                        };
                        return textElementClass;
                    }
                default:
                    {
                        return textElementClass;
                    }
            }
            
        }

        private static IPoint GetPosition(IPageLayout ipageLayout_0, out int int_0, out double double_0, out double double_1)
        {
            IPoint upperLeft;
            int_0 = 0;
            double_0 = 0;
            double_1 = 0;
            IPoint pointClass = new PointClass();
            pointClass.PutCoords(0, 0);
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(ElementWizardHelp.m_Position);
                XmlNode itemOf = xmlDocument.ChildNodes[0];
                for (int i = 0; i < itemOf.ChildNodes.Count; i++)
                {
                    XmlNode xmlNodes = itemOf.ChildNodes[i];
                    string value = xmlNodes.Attributes["name"].Value;
                    string str = xmlNodes.Attributes["value"].Value;
                    string str1 = value;
                    if (str1 != null)
                    {
                        if (str1 == "position")
                        {
                            int_0 = int.Parse(str);
                        }
                        else if (str1 == "xoffset")
                        {
                            double_0 = double.Parse(str);
                        }
                        else if (str1 == "yoffset")
                        {
                            double_1 = double.Parse(str);
                        }
                    }
                }
                IGraphicsContainer ipageLayout0 = ipageLayout_0 as IGraphicsContainer;
                IElement element = CommandFunction.FindElementByType(ipageLayout_0, "外框");
                IEnvelope envelopeClass = null;
                if (element != null)
                {
                    envelopeClass = new EnvelopeClass();
                    element.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelopeClass);
                }
                if (envelopeClass == null)
                {
                    ipageLayout0.Reset();
                    IMapFrame mapFrame = ipageLayout0.FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
                    envelopeClass = (mapFrame as IElement).Geometry.Envelope;
                }
                ElementWizardHelp.m_posoitiontype = int_0;
                switch (int_0)
                {
                    case 0:
                        {
                            upperLeft = envelopeClass.UpperLeft;
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 1:
                        {
                            upperLeft = new PointClass();
                            upperLeft.PutCoords((envelopeClass.XMin + envelopeClass.XMax) / 2, envelopeClass.YMax);
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 2:
                        {
                            upperLeft = envelopeClass.UpperRight;
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 3:
                        {
                            upperLeft = envelopeClass.UpperLeft;
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 4:
                        {
                            upperLeft = envelopeClass.UpperRight;
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 5:
                        {
                            upperLeft = new PointClass();
                            upperLeft.PutCoords(envelopeClass.XMin, (envelopeClass.YMin + envelopeClass.YMax) / 2);
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 6:
                        {
                            upperLeft = new PointClass();
                            upperLeft.PutCoords(envelopeClass.XMax, (envelopeClass.YMin + envelopeClass.YMax) / 2);
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 7:
                        {
                            upperLeft = envelopeClass.LowerLeft;
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 8:
                        {
                            upperLeft = envelopeClass.LowerRight;
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 9:
                        {
                            upperLeft = envelopeClass.LowerLeft;
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 10:
                        {
                            upperLeft = new PointClass();
                            upperLeft.PutCoords((envelopeClass.XMin + envelopeClass.XMax) / 2, envelopeClass.YMin);
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                    case 11:
                        {
                            upperLeft = envelopeClass.LowerRight;
                            pointClass.PutCoords(upperLeft.X + double_0, upperLeft.Y + double_1);
                            break;
                        }
                }
            }
            catch
            {
            }
            return pointClass;
        }

        public static string GetTextElementValue()
        {
            return "";
        }

        public static void Init()
        {
            ElementWizardHelp.m_ElementType = ElementType.ConstantText;
            ElementWizardHelp.m_Position = "";
            ElementWizardHelp.m_IsVerticalText = false;
            ElementWizardHelp.m_Text = "";
            ElementWizardHelp.m_pLegendInfo = "";
            ElementWizardHelp.m_PictureFileName = "";
            ElementWizardHelp.m_HasElementSelect = true;
        }

        private static void old_acctor_mc()
        {
            ElementWizardHelp.m_ElementType = ElementType.ConstantText;
            ElementWizardHelp.m_Position = "";
            ElementWizardHelp.m_pLegendInfo = "";
            ElementWizardHelp.m_HasElementSelect = true;
            ElementWizardHelp.m_PictureFileName = "";
            ElementWizardHelp.m_IsVerticalText = false;
            ElementWizardHelp.m_pStyle = new TextSymbolClass();
            ElementWizardHelp.m_Text = "";
            ElementWizardHelp.m_posoitiontype = -1;
        }

        private static void SetTextSymblAlign(ITextSymbol itextSymbol_0, int int_0)
        {
            switch (int_0)
            {
                case 0:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        break;
                    }
                case 1:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        break;
                    }
                case 2:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        break;
                    }
                case 3:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                        break;
                    }
                case 4:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                        break;
                    }
                case 5:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                        break;
                    }
                case 6:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                        break;
                    }
                case 7:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        break;
                    }
                case 8:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                        break;
                    }
                case 9:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                        break;
                    }
                case 10:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                        break;
                    }
                case 11:
                    {
                        itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                        itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                        break;
                    }
            }
        }
    }
}