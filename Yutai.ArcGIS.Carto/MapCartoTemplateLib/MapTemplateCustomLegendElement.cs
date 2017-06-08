using System;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateCustomLegendElement : MapTemplateElement
    {
        [CompilerGenerated]
        private string string_1;

        public MapTemplateCustomLegendElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.CustomLegendElement;
            base.Name = "自定义图例";
        }

        public MapTemplateCustomLegendElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.CustomLegendElement;
            base.Name = "自定义图例";
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplateCustomLegendElement element = new MapTemplateCustomLegendElement(mapTemplate_1) {
                LegendInfo = this.LegendInfo
            };
            this.CopyTo(element);
            return element;
        }

        public override IElement CreateElement(IPageLayout ipageLayout_0)
        {
            IPoint position = this.GetPosition(ipageLayout_0);
            CustomLegend legend = new CustomLegend {
                LegendInfo = this.LegendInfo
            };
            legend.Init(ipageLayout_0 as IActiveView, position);
            this.Element = legend;
            return this.Element;
        }

        public override IElement GetElement(IPageLayout ipageLayout_0)
        {
            double num;
            if (base.m_pElement == null)
            {
                this.CreateElement(ipageLayout_0);
            }
            if (base.m_pElement == null)
            {
                return null;
            }
            IPoint position = this.GetPosition(ipageLayout_0);
            IEnvelope bounds = new EnvelopeClass();
            base.m_pElement.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            if (!bounds.IsEmpty)
            {
                IEnvelope envelope2 = new EnvelopeClass();
                envelope2.PutCoords(position.X, position.Y, position.X + bounds.Width, position.Y - bounds.Height);
                base.m_pElement.Geometry = envelope2;
            }
            IEnvelope envelope = (MapFrameAssistant.GetFocusMapFrame(ipageLayout_0) as IElement).Geometry.Envelope;
            IEnvelope envelope4 = base.m_pElement.Geometry.Envelope;
            if (envelope4.Envelope.Width > (envelope.Envelope.Width / 3.0))
            {
                num = (envelope.Envelope.Width / envelope4.Envelope.Width) / 3.0;
                this.method_5(base.m_pElement, num);
            }
            else if (envelope4.Envelope.Height > (envelope.Envelope.Height / 3.0))
            {
                num = (envelope.Envelope.Height / envelope4.Envelope.Height) / 3.0;
                this.method_5(base.m_pElement, num);
            }
            return base.m_pElement;
        }

        protected override IPoint GetPosition(IPageLayout ipageLayout_0)
        {
            IPoint point = new PointClass();
            point.PutCoords(0.0, 0.0);
            try
            {
                IPoint upperLeft;
                double yOffset;
                IGraphicsContainer container = ipageLayout_0 as IGraphicsContainer;
                IElement element = base.FindElementByType(ipageLayout_0, "外框");
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
                double num = 0.0;
                if (base.MapTemplate.BorderSymbol is ILineSymbol)
                {
                    num = base.MapTemplate.OutBorderWidth / 2.0;
                }
                IEnvelope envelope = this.Element.Geometry.Envelope;
                switch (base.ElementLocation.LocationType)
                {
                    case LocationType.UpperLeft:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, (upperLeft.Y + base.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.UpperrCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax) / 2.0, bounds.YMax);
                        point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, (upperLeft.Y + base.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.UpperRight:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, (upperLeft.Y + base.ElementLocation.YOffset) + num);
                        return point;

                    case LocationType.LeftUpper:
                        upperLeft = bounds.UpperLeft;
                        point.PutCoords((upperLeft.X + base.ElementLocation.XOffset) - num, upperLeft.Y + base.ElementLocation.YOffset);
                        return point;

                    case LocationType.RightUpper:
                        upperLeft = bounds.UpperRight;
                        point.PutCoords((upperLeft.X + base.ElementLocation.XOffset) + num, upperLeft.Y + base.ElementLocation.YOffset);
                        return point;

                    case LocationType.LeftCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMin, (bounds.YMin + bounds.YMax) / 2.0);
                        point.PutCoords((upperLeft.X + base.ElementLocation.XOffset) - num, upperLeft.Y + base.ElementLocation.YOffset);
                        return point;

                    case LocationType.RightCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords(bounds.XMax, (bounds.YMin + bounds.YMax) / 2.0);
                        point.PutCoords((upperLeft.X + base.ElementLocation.XOffset) + num, upperLeft.Y + base.ElementLocation.YOffset);
                        return point;

                    case LocationType.LeftLower:
                        upperLeft = bounds.LowerLeft;
                        if (!base.MapTemplate.IsChangeBottomLength)
                        {
                            break;
                        }
                        yOffset = base.ElementLocation.YOffset;
                        if (yOffset > 0.0)
                        {
                            yOffset = (base.ElementLocation.YOffset + base.MapTemplate.BottomInOutSpace) - base.MapTemplate.OldBottomInOutSpace;
                        }
                        point.PutCoords((upperLeft.X + base.ElementLocation.XOffset) - num, upperLeft.Y + yOffset);
                        return point;

                    case LocationType.RightLower:
                        upperLeft = bounds.LowerRight;
                        if (!base.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_046E;
                        }
                        yOffset = base.ElementLocation.YOffset;
                        if (yOffset > 0.0)
                        {
                            yOffset = (base.ElementLocation.YOffset + base.MapTemplate.BottomInOutSpace) - base.MapTemplate.OldBottomInOutSpace;
                        }
                        point.PutCoords((upperLeft.X + base.ElementLocation.XOffset) + num, upperLeft.Y + yOffset);
                        return point;

                    case LocationType.LowerLeft:
                        upperLeft = bounds.LowerLeft;
                        if (!base.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_0529;
                        }
                        yOffset = base.ElementLocation.YOffset;
                        if (yOffset > 0.0)
                        {
                            yOffset = (base.ElementLocation.YOffset + base.MapTemplate.BottomInOutSpace) - base.MapTemplate.OldBottomInOutSpace;
                        }
                        point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, (upperLeft.Y + yOffset) - num);
                        return point;

                    case LocationType.LowerCenter:
                        upperLeft = new PointClass();
                        upperLeft.PutCoords((bounds.XMin + bounds.XMax) / 2.0, bounds.YMin);
                        if (!base.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_05FF;
                        }
                        yOffset = base.ElementLocation.YOffset;
                        if (yOffset > 0.0)
                        {
                            yOffset = (base.ElementLocation.YOffset + base.MapTemplate.BottomInOutSpace) - base.MapTemplate.OldBottomInOutSpace;
                        }
                        point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, yOffset - num);
                        return point;

                    case LocationType.LowerRight:
                        upperLeft = bounds.LowerRight;
                        if (!base.MapTemplate.IsChangeBottomLength)
                        {
                            goto Label_06B7;
                        }
                        yOffset = base.ElementLocation.YOffset;
                        if (yOffset > 0.0)
                        {
                            yOffset = (base.ElementLocation.YOffset + base.MapTemplate.BottomInOutSpace) - base.MapTemplate.OldBottomInOutSpace;
                        }
                        point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, (upperLeft.Y + yOffset) - num);
                        return point;

                    default:
                        return point;
                }
                point.PutCoords((upperLeft.X + base.ElementLocation.XOffset) - num, upperLeft.Y + base.ElementLocation.YOffset);
                return point;
            Label_046E:
                point.PutCoords((upperLeft.X + base.ElementLocation.XOffset) + num, upperLeft.Y + base.ElementLocation.YOffset);
                return point;
            Label_0529:
                point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, (upperLeft.Y + base.ElementLocation.YOffset) - num);
                return point;
            Label_05FF:
                point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, (upperLeft.Y + base.ElementLocation.YOffset) - num);
                return point;
            Label_06B7:
                point.PutCoords(upperLeft.X + base.ElementLocation.XOffset, (upperLeft.Y + base.ElementLocation.YOffset) - num);
            }
            catch
            {
            }
            return point;
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

        private void method_5(IElement ielement_0, double double_0)
        {
            IEnvelope from = base.m_pElement.Geometry.Envelope;
            IEnvelope envelope = ielement_0.Geometry.Envelope;
            envelope.Expand(double_0, double_0, true);
            IEnvelope to = new EnvelopeClass();
            to.PutCoords(from.XMin, from.YMin, from.XMin + envelope.Width, from.YMin + envelope.Height);
            IAffineTransformation2D transformation = new AffineTransformation2DClass();
            transformation.DefineFromEnvelopes(from, to);
            (base.m_pElement as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
        }

        public override void Update(IPageLayout ipageLayout_0)
        {
            if (this.Element != null)
            {
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.Element, null);
                IPoint position = this.GetPosition(ipageLayout_0);
                try
                {
                    if (this.Element is CustomLegend)
                    {
                        (this.Element as CustomLegend).LegendInfo = this.LegendInfo;
                        (this.Element as CustomLegend).Init(ipageLayout_0 as IActiveView, position);
                    }
                    else
                    {
                        CustomLegend legend2 = new CustomLegend {
                            LegendInfo = this.LegendInfo
                        };
                        legend2.Init(ipageLayout_0 as IActiveView, position);
                        this.Element = legend2;
                    }
                    (ipageLayout_0 as IActiveView).GraphicsContainer.UpdateElement(base.m_pElement);
                    (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                    this.Save();
                }
                catch (Exception)
                {
                }
            }
        }

        public string LegendInfo
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            set
            {
                this.string_1 = value;
            }
        }

        protected override IPropertySet PropertySet
        {
            get
            {
                IPropertySet set = new PropertySetClass();
                set.SetProperty("LegendInfo", this.LegendInfo);
                return set;
            }
            set
            {
                try
                {
                    this.LegendInfo = value.GetProperty("LegendInfo").ToString();
                }
                catch
                {
                }
            }
        }
    }
}

