using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateGroupElement : MapTemplateElement
    {
        private IGroupElement igroupElement_0;


        public MapTemplateGroupElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            this.igroupElement_0 = new GroupElementClass();
            base.MapTemplateElementType = MapTemplateElementType.MyGruopElement;
            base.Name = "扩展组合元素";
            base.ElementLocation.LocationType = LocationType.LeftLower;
            this.SizeStyle = MapCartoTemplateLib.SizeStyle.SameAsInsideWidth;
            this.SizeScale = 1.0;
        }

        public MapTemplateGroupElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            this.igroupElement_0 = new GroupElementClass();
            base.MapTemplateElementType = MapTemplateElementType.MyGruopElement;
            base.Name = "扩展组合元素";
            base.ElementLocation.LocationType = LocationType.LeftLower;
            this.SizeStyle = MapCartoTemplateLib.SizeStyle.SameAsInsideWidth;
            this.SizeScale = 1.0;
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            return null;
        }

        public void Create(IEnumElement ienumElement_0)
        {
            ienumElement_0.Reset();
            IElement element = ienumElement_0.Next();
            if (this.igroupElement_0 == null)
            {
                this.igroupElement_0 = new GroupElementClass();
            }
            while (element != null)
            {
                this.igroupElement_0.AddElement((element as IClone).Clone() as IElement);
                element = ienumElement_0.Next();
            }
            if ((this.igroupElement_0 as IElement).Geometry.Envelope.IsEmpty)
            {
            }
        }

        public void Create(IList<IElement> list_0)
        {
            foreach (IElement element in list_0)
            {
                this.igroupElement_0.AddElement((element as IClone).Clone() as IElement);
            }
        }

        public override IElement CreateElement(IPageLayout ipageLayout_0)
        {
            this.GetPosition(ipageLayout_0);
            this.Element = this.igroupElement_0 as IElement;
            return this.Element;
        }

        public override IElement GetElement(IPageLayout ipageLayout_0)
        {
            IElement element = base.GetElement(ipageLayout_0);
            if (this.SizeStyle != MapCartoTemplateLib.SizeStyle.Fixed)
            {
                double num;
                IEnvelope envelope = (MapFrameAssistant.GetFocusMapFrame(ipageLayout_0) as IElement).Geometry.Envelope;
                IEnvelope from = element.Geometry.Envelope;
                if (from.IsEmpty)
                {
                    return element;
                }
                IEnvelope to = element.Geometry.Envelope;
                if (this.SizeStyle == MapCartoTemplateLib.SizeStyle.SameAsInsideWidth)
                {
                    num = (from.Height*envelope.Width)/from.Width;
                    to.PutCoords(from.XMin, from.YMin, from.XMin + envelope.Width, from.YMin + num);
                }
                else if (this.SizeStyle == MapCartoTemplateLib.SizeStyle.SameAsInsideHeight)
                {
                    num = (from.Width*envelope.Height)/from.Height;
                    to.PutCoords(from.XMin, from.YMin, from.XMin + num, from.YMin + envelope.Height);
                }
                else
                {
                    double num2;
                    if (this.SizeStyle == MapCartoTemplateLib.SizeStyle.InsideWidthScale)
                    {
                        num2 = envelope.Width*this.SizeScale;
                        num = (from.Height*num2)/from.Width;
                        to.PutCoords(from.XMin, from.YMin, from.XMin + num2, from.YMin + num);
                    }
                    else if (this.SizeStyle == MapCartoTemplateLib.SizeStyle.InsideWidthScale)
                    {
                        num = envelope.Height*this.SizeScale;
                        num2 = (from.Width*num)/from.Height;
                        to.PutCoords(from.XMin, from.YMin, from.XMin + num2, from.YMin + num);
                    }
                }
                IAffineTransformation2D transformation = new AffineTransformation2DClass();
                transformation.DefineFromEnvelopes(from, to);
                (element as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
            }
            return element;
        }

        public override void Init()
        {
            IElement element;
            string text;
            ExpressionCalculator calculator;
            int num2;
            string str2;
            MapTemplateParam param;
            for (int i = 0; i < this.igroupElement_0.ElementCount; i++)
            {
                element = this.igroupElement_0.get_Element(i);
                if (element is ITextElement)
                {
                    text = (element as ITextElement).Text;
                    if ((text.Length != 0) && (text[0] == '='))
                    {
                        calculator = new ExpressionCalculator();
                        calculator.Init(text.Substring(1));
                        num2 = 0;
                        while (num2 < calculator.ParamList.Count)
                        {
                            str2 = calculator.ParamList.Keys[num2];
                            param = base.MapTemplate.FindParamByName(str2);
                            if (param != null)
                            {
                                calculator.ParamList[str2] = param.Value;
                            }
                            num2++;
                        }
                        string str3 = calculator.Calculate().ToString();
                        if (str3 == "")
                        {
                            str3 = "  ";
                        }
                        (element as ITextElement).Text = str3;
                        (this.igroupElement_0 as IGroupElement3).ReplaceElement(element, element);
                    }
                }
            }
            IEnumElement elements = (base.m_pElement as IGroupElement).Elements;
            elements.Reset();
            for (element = elements.Next(); element != null; element = elements.Next())
            {
                if (element is ITextElement)
                {
                    text = (element as ITextElement).Text;
                    if ((text.Length > 0) && (text[0] == '='))
                    {
                        calculator = new ExpressionCalculator();
                        calculator.Init(text.Substring(1));
                        for (num2 = 0; num2 < calculator.ParamList.Count; num2++)
                        {
                            str2 = calculator.ParamList.Keys[num2];
                            param = base.MapTemplate.FindParamByName(str2);
                            if (param != null)
                            {
                                calculator.ParamList[str2] = param.Value;
                            }
                        }
                        (element as ITextElement).Text = calculator.Calculate().ToString();
                    }
                }
            }
            base.Init();
        }

        public override void Update(IPageLayout ipageLayout_0)
        {
            if (base.m_pElement != null)
            {
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                IPoint position = this.GetPosition(ipageLayout_0);
                IEnvelope bounds = new EnvelopeClass();
                base.m_pElement.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                if (!bounds.IsEmpty)
                {
                    IEnvelope envelope2 = new EnvelopeClass();
                    envelope2.PutCoords(position.X, position.Y, position.X + bounds.Width, position.Y + bounds.Height);
                    base.m_pElement.Geometry = envelope2;
                }
                (ipageLayout_0 as IActiveView).GraphicsContainer.UpdateElement(base.m_pElement);
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                this.Save();
            }
        }

        protected override IPropertySet PropertySet
        {
            get
            {
                IPropertySet set = new PropertySetClass();
                set.SetProperty("GroupElement", this.igroupElement_0);
                set.SetProperty("SizeStyle", (int) this.SizeStyle);
                set.SetProperty("SizeScale", this.SizeScale);
                return set;
            }
            set
            {
                try
                {
                    this.igroupElement_0 = value.GetProperty("SizeStyle") as IGroupElement;
                }
                catch
                {
                }
                try
                {
                    this.SizeStyle = (SizeStyle) Convert.ToInt32(value.GetProperty("GroupElement"));
                }
                catch
                {
                }
                try
                {
                    this.SizeScale = Convert.ToDouble(value.GetProperty("SizeScale"));
                    if (this.SizeScale == 0.0)
                    {
                        this.SizeScale = 1.0;
                    }
                }
                catch
                {
                }
            }
        }

        public double SizeScale { get; set; }

        public MapCartoTemplateLib.SizeStyle SizeStyle { get; set; }
    }
}