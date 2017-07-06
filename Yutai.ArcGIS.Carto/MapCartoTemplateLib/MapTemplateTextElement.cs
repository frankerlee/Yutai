using System;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateTextElement : MapTemplateElement
    {
        public MapTemplateTextElement(MapTemplate pMapTemplate) : base(pMapTemplate)
        {
            base.MapTemplateElementType = MapTemplateElementType.TextElement;
            base.Name = "文本";
            base.Style = new TextSymbolClass();
        }

        public MapTemplateTextElement(int id, MapTemplate pMapTemplate) : base(id, pMapTemplate)
        {
            base.MapTemplateElementType = MapTemplateElementType.TextElement;
            base.Name = "文本";
            base.Style = new TextSymbolClass();
        }

        public override MapTemplateElement Clone(MapTemplate pMapTemplate)
        {
            MapTemplateTextElement element = new MapTemplateTextElement(pMapTemplate)
            {
                IsVerticalText = this.IsVerticalText,
                Multiline = this.Multiline,
                Text = this.Text
            };
            this.CopyTo(element);
            return element;
        }

        public override IElement CreateElement(IPageLayout pPageLayout)
        {
            IElement element = new TextElementClass();
            (element as IElementProperties).Name = base.Name;
            (element as ITextElement).Text = this.Text;
            if (base.Style != null)
            {
                this.method_4(base.Style as ITextSymbol, base.ElementLocation.LocationType);
                (element as ITextElement).Symbol = base.Style as ITextSymbol;
            }
            this.Element = element;
            return element;
        }

        public override IElement GetElement(IPageLayout pPageLayout)
        {
            ITextSymbol symbol;
            esriTextHorizontalAlignment horizontalAlignment;
            esriTextVerticalAlignment verticalAlignment;
            if (base.m_pElement == null)
            {
                this.CreateElement(pPageLayout);
            }
            if (base.m_pElement == null)
            {
                return null;
            }
            IPoint position = this.GetPosition(pPageLayout);
            if (base.Style == null)
            {
                base.Style = (base.m_pElement as ITextElement).Symbol;
            }
            this.method_4(base.Style as ITextSymbol, base.ElementLocation.LocationType);
            (base.m_pElement as ITextElement).Symbol = base.Style as ITextSymbol;
            if (this.IsVerticalText)
            {
                symbol = (base.m_pElement as ITextElement).Symbol;
                if (symbol.Angle != 270.0)
                {
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
                    (base.m_pElement as ITextElement).Symbol = symbol;
                }
            }
            else
            {
                (base.m_pElement as IElementProperties).Type = "";
                symbol = (base.m_pElement as ITextElement).Symbol;
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
                    (base.m_pElement as ITextElement).Symbol = symbol;
                }
            }
            (base.m_pElement as ITextElement).Text = this.Text;
            base.m_pElement.Geometry = position;
            (base.m_pElement as IElementProperties2).AutoTransform = true;
            return base.m_pElement;
        }

        public override void Init()
        {
            if (this.Text[0] == '=')
            {
                ExpressionCalculator calculator = new ExpressionCalculator();
                calculator.Init(this.Text.Substring(1));
                for (int i = 0; i < calculator.ParamList.Count; i++)
                {
                    string str = calculator.ParamList.Keys[i];
                    MapTemplateParam param = base.MapTemplate.FindParamByName(str);
                    if (param != null)
                    {
                        calculator.ParamList[str] = param.Value;
                    }
                }
                object obj2 = calculator.Calculate();
                if (obj2 == null)
                {
                    (base.m_pElement as ITextElement).Text = "";
                }
                else
                {
                    string str2 = obj2.ToString();
                    if (str2.Length == 0)
                    {
                        str2 = " ";
                    }
                    this.Text = str2;
                    (base.m_pElement as ITextElement).Text = obj2.ToString();
                }
            }
        }

        private void method_4(ITextSymbol itextSymbol_0, LocationType locationType_0)
        {
            switch (locationType_0)
            {
                case LocationType.UpperLeft:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case LocationType.UpperrCenter:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case LocationType.UpperRight:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case LocationType.LeftUpper:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;

                case LocationType.RightUpper:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;

                case LocationType.LeftCenter:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                    break;

                case LocationType.RightCenter:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
                    break;

                case LocationType.LeftLower:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case LocationType.RightLower:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    break;

                case LocationType.LowerLeft:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;

                case LocationType.LowerCenter:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;

                case LocationType.LowerRight:
                    itextSymbol_0.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                    itextSymbol_0.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    break;
            }
        }

        public override void Update(IPageLayout pPageLayout)
        {
            if (this.Element != null)
            {
                ITextSymbol symbol;
                esriTextHorizontalAlignment horizontalAlignment;
                esriTextVerticalAlignment verticalAlignment;
                (pPageLayout as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.Element, null);
                IPoint position = this.GetPosition(pPageLayout);
                if (base.Style == null)
                {
                    base.Style = (base.m_pElement as ITextElement).Symbol;
                }
                this.method_4(base.Style as ITextSymbol, base.ElementLocation.LocationType);
                (base.m_pElement as ITextElement).Symbol = base.Style as ITextSymbol;
                if (this.IsVerticalText)
                {
                    symbol = (base.m_pElement as ITextElement).Symbol;
                    if (symbol.Angle != 270.0)
                    {
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
                        (base.m_pElement as ITextElement).Symbol = symbol;
                    }
                }
                else
                {
                    (base.m_pElement as IElementProperties).Type = "";
                    symbol = (base.m_pElement as ITextElement).Symbol;
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
                        (base.m_pElement as ITextElement).Symbol = symbol;
                    }
                }
                (base.m_pElement as ITextElement).Text = this.Text;
                base.m_pElement.Geometry = position;
                (pPageLayout as IActiveView).GraphicsContainer.UpdateElement(base.m_pElement);
                (pPageLayout as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                this.Save();
            }
        }

        public bool IsVerticalText { get; set; }
        public bool Multiline { get; set; }

        protected override IPropertySet PropertySet
        {
            get
            {
                IPropertySet set = new PropertySetClass();
                set.SetProperty("Style", base.Style);
                set.SetProperty("Text", this.Text);
                set.SetProperty("IsVerticalText", this.IsVerticalText ? 1 : 0);
                return set;
            }
            set
            {
                try
                {
                    base.Style = value.GetProperty("Style");
                    this.Text = value.GetProperty("Text").ToString();
                    value.GetProperty("IsVerticalText");
                    this.IsVerticalText = Convert.ToInt32(value.GetProperty("IsVerticalText")) == 1;
                }
                catch
                {
                }
            }
        }

        public string Text { get; set; }
    }
}