using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using stdole;
using Yutai.ArcGIS.Carto.DesignLib;
using Yutai.ArcGIS.Common.Symbol;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplate
    {
        [CompilerGenerated]
        private bool bool_0;
        [CompilerGenerated]
        private bool bool_1;
        [CompilerGenerated]
        private bool bool_2;
        [CompilerGenerated]
        private bool bool_3;
        [CompilerGenerated]
        private bool bool_4;
        [CompilerGenerated]
        private bool bool_5;
        [CompilerGenerated]
        private bool bool_6;
        [CompilerGenerated]
        private bool bool_7;
        [CompilerGenerated]
        private bool bool_8;
        [CompilerGenerated]
        private bool bool_9;
        private double double_0 = 10.0;
        private double double_1 = 13.0;
        [CompilerGenerated]
        private double double_10;
        [CompilerGenerated]
        private double double_11;
        [CompilerGenerated]
        private double double_12;
        [CompilerGenerated]
        private double double_13;
        [CompilerGenerated]
        private double double_14;
        [CompilerGenerated]
        private double double_15;
        [CompilerGenerated]
        private double double_16;
        [CompilerGenerated]
        private double double_17;
        [CompilerGenerated]
        private double double_18;
        [CompilerGenerated]
        private double double_19;
        private double double_2 = 0.0;
        [CompilerGenerated]
        private double double_20;
        [CompilerGenerated]
        private double double_21;
        private double double_3 = 0.0;
        private double double_4 = 0.0;
        private double double_5 = 0.0;
        private double double_6 = 1.0;
        [CompilerGenerated]
        private double double_7;
        [CompilerGenerated]
        private double double_8;
        [CompilerGenerated]
        private double double_9;
        [CompilerGenerated]
        private esriUnits esriUnits_0;
        [CompilerGenerated]
        private IMapGrid imapGrid_0;
        [CompilerGenerated]
        private int int_0;
        private ISymbol isymbol_0 = null;
        [CompilerGenerated]
        private ISymbol isymbol_1;
        [CompilerGenerated]
        private ISymbol isymbol_2;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapFrameType mapFrameType_0;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapFrameType mapFrameType_1;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapFramingType mapFramingType_0;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateClass mapTemplateClass_0;
        public List<MapCartoTemplateLib.MapTemplateElement> mapTemplateElelemt;
        [CompilerGenerated]
        private MapCartoTemplateLib.MapTemplateGallery mapTemplateGallery_0;
        public List<MapCartoTemplateLib.MapTemplateParam> mapTemplateParam;
        private string string_0 = "宋体";
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        [CompilerGenerated]
        private string string_3;
        [CompilerGenerated]
        private string string_4;
        [CompilerGenerated]
        private string string_5;
        [CompilerGenerated]
        private MapCartoTemplateLib.TemplateSizeStyle templateSizeStyle_0;

        public MapTemplate(int int_1, MapCartoTemplateLib.MapTemplateClass mapTemplateClass_1)
        {
            this.MapTemplateGallery = mapTemplateClass_1.MapTemplateGallery;
            this.ClassGuid = mapTemplateClass_1.Guid;
            this.IsTest = true;
            this.MapTemplateClass = mapTemplateClass_1;
            this.OID = int_1;
            if (this.OID != -1)
            {
                this.Load();
            }
            else
            {
                this.method_13();
            }
        }

        public void AddMapTemplateElement(MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0)
        {
            if (mapTemplateElement_0 != null)
            {
                if (this.mapTemplateElelemt == null)
                {
                    this.mapTemplateElelemt = new List<MapCartoTemplateLib.MapTemplateElement>();
                }
                if (!this.mapTemplateElelemt.Contains(mapTemplateElement_0))
                {
                    this.mapTemplateElelemt.Add(mapTemplateElement_0);
                }
            }
        }

        public void AddMapTemplateParam(MapCartoTemplateLib.MapTemplateParam mapTemplateParam_0)
        {
            if (mapTemplateParam_0 != null)
            {
                if (this.mapTemplateParam == null)
                {
                    this.mapTemplateParam = new List<MapCartoTemplateLib.MapTemplateParam>();
                }
                if (!this.mapTemplateParam.Contains(mapTemplateParam_0))
                {
                    this.mapTemplateParam.Add(mapTemplateParam_0);
                }
            }
        }

        public bool CanCreateTK(IActiveView iactiveView_0)
        {
            if (iactiveView_0.FocusMap.SpatialReference == null)
            {
                return false;
            }
            return (iactiveView_0.FocusMap.SpatialReference is IProjectedCoordinateSystem);
        }

        public MapCartoTemplateLib.MapTemplate Clone()
        {
            MapCartoTemplateLib.MapTemplate template = new MapCartoTemplateLib.MapTemplate(-1, this.MapTemplateClass) {
                AnnoUnit = this.AnnoUnit,
                AnnoUnitZoomScale = this.AnnoUnitZoomScale,
                BigFontSize = this.BigFontSize
            };
            if (this.BorderSymbol != null)
            {
                template.BorderSymbol = (this.BorderSymbol as IClone).Clone() as ISymbol;
            }
            template.Guid = System.Guid.NewGuid().ToString();
            template.BottomInOutSpace = this.BottomInOutSpace;
            template.Description = this.Description;
            template.DrawCornerShortLine = this.DrawCornerShortLine;
            template.DrawRoundText = this.DrawRoundText;
            template.DrawCornerText = this.DrawCornerText;
            template.DrawJWD = this.DrawJWD;
            template.DrawRoundLineShortLine = this.DrawRoundLineShortLine;
            template.TemplateSizeStyle = this.TemplateSizeStyle;
            template.FontName = this.FontName;
            if (this.GridSymbol != null)
            {
                template.GridSymbol = (this.GridSymbol as IClone).Clone() as ISymbol;
            }
            template.Height = this.Height;
            template.IsTest = true;
            template.LeftInOutSpace = this.LeftInOutSpace;
            template.LegendInfo = this.LegendInfo;
            template.MapFrameType = this.MapFrameType;
            if (this.MapGrid != null)
            {
                try
                {
                    template.MapGrid = (this.MapGrid as IClone).Clone() as IMapGrid;
                }
                catch
                {
                }
            }
            template.MapXInterval = this.MapXInterval;
            template.MapYInterval = this.MapYInterval;
            template.Name = "拷贝 " + this.Name;
            template.OutBorderWidth = this.OutBorderWidth;
            if (this.OutSideStyle != null)
            {
                template.OutSideStyle = (this.OutSideStyle as IClone).Clone() as ISymbol;
            }
            template.RightInOutSpace = this.RightInOutSpace;
            template.Scale = this.Scale;
            template.SmallFontSize = this.SmallFontSize;
            template.TopInOutSpace = this.TopInOutSpace;
            template.Width = this.Width;
            template.XInterval = this.XInterval;
            template.YInterval = this.YInterval;
            template.Save();
            foreach (MapCartoTemplateLib.MapTemplateParam param in this.MapTemplateParam)
            {
                MapCartoTemplateLib.MapTemplateParam param2 = param.Clone(template);
                param2.Save();
                template.AddMapTemplateParam(param2);
            }
            foreach (MapCartoTemplateLib.MapTemplateElement element in this.MapTemplateElement)
            {
                MapCartoTemplateLib.MapTemplateElement element2 = element.Clone(template);
                element2.Save();
                template.AddMapTemplateElement(element2);
            }
            return template;
        }

        public MapCartoTemplateLib.MapTemplate Clone(MapCartoTemplateLib.MapTemplateClass mapTemplateClass_1)
        {
            MapCartoTemplateLib.MapTemplate template = new MapCartoTemplateLib.MapTemplate(-1, mapTemplateClass_1) {
                Guid = System.Guid.NewGuid().ToString(),
                AnnoUnit = this.AnnoUnit,
                AnnoUnitZoomScale = this.AnnoUnitZoomScale,
                BigFontSize = this.BigFontSize
            };
            if (this.BorderSymbol != null)
            {
                template.BorderSymbol = (this.BorderSymbol as IClone).Clone() as ISymbol;
            }
            template.BottomInOutSpace = this.BottomInOutSpace;
            template.Description = this.Description;
            template.DrawCornerShortLine = this.DrawCornerShortLine;
            template.DrawCornerText = this.DrawCornerText;
            template.DrawJWD = this.DrawJWD;
            template.DrawRoundText = this.DrawRoundText;
            template.DrawRoundLineShortLine = this.DrawRoundLineShortLine;
            template.TemplateSizeStyle = this.TemplateSizeStyle;
            this.IsAdapationScale = this.IsAdapationScale;
            template.FontName = this.FontName;
            if (this.GridSymbol != null)
            {
                template.GridSymbol = (this.GridSymbol as IClone).Clone() as ISymbol;
            }
            template.Height = this.Height;
            template.IsTest = true;
            template.LeftInOutSpace = this.LeftInOutSpace;
            template.LegendInfo = this.LegendInfo;
            template.MapFrameType = this.MapFrameType;
            if (this.MapGrid != null)
            {
                try
                {
                    template.MapGrid = (this.MapGrid as IClone).Clone() as IMapGrid;
                }
                catch
                {
                }
            }
            template.MapXInterval = this.MapXInterval;
            template.MapYInterval = this.MapYInterval;
            template.Name = "拷贝 " + this.Name;
            template.OutBorderWidth = this.OutBorderWidth;
            if (this.OutSideStyle != null)
            {
                template.OutSideStyle = (this.OutSideStyle as IClone).Clone() as ISymbol;
            }
            template.RightInOutSpace = this.RightInOutSpace;
            template.Scale = this.Scale;
            template.SmallFontSize = this.SmallFontSize;
            template.TopInOutSpace = this.TopInOutSpace;
            template.Width = this.Width;
            template.XInterval = this.XInterval;
            template.YInterval = this.YInterval;
            template.Save();
            foreach (MapCartoTemplateLib.MapTemplateParam param in this.MapTemplateParam)
            {
                MapCartoTemplateLib.MapTemplateParam param2 = param.Clone(template);
                param2.Save();
                template.AddMapTemplateParam(param2);
            }
            foreach (MapCartoTemplateLib.MapTemplateElement element in this.MapTemplateElement)
            {
                MapCartoTemplateLib.MapTemplateElement element2 = element.Clone(template);
                element2.Save();
                template.AddMapTemplateElement(element2);
            }
            return template;
        }

        public IElement CreateCornerShortLine(MapCartoTemplateLib.YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            if (this.BorderSymbol == null)
            {
                return null;
            }
            IPoint point = jlktransformation_0.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = jlktransformation_0.ToPageLayoutPoint(ipoint_1);
            ILineSymbol symbol = this.method_25();
            IGroupElement element2 = new GroupElementClass();
            element2.AddElement(this.method_29(point.X, point.Y, -this.LeftInOutSpace, -this.BottomInOutSpace, symbol));
            element2.AddElement(this.method_29(point.X, point2.Y, -this.LeftInOutSpace, this.TopInOutSpace, symbol));
            element2.AddElement(this.method_29(point2.X, point2.Y, this.RightInOutSpace, this.TopInOutSpace, symbol));
            element2.AddElement(this.method_29(point2.X, point.Y, this.RightInOutSpace, -this.BottomInOutSpace, symbol));
            int num = 10;
            int num2 = 13;
            if (this.DrawCornerText)
            {
                string str;
                string str2;
                ITextSymbol symbol2 = this.FontStyle((double) num, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
                ITextSymbol symbol3 = this.FontStyle((double) num2, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
                double x = ipoint_0.X;
                double num4 = (this.Width * this.Scale) / 100.0;
                double num5 = (this.Height * this.Scale) / 100.0;
                x = ((int) (x / num4)) * num4;
                if (this.BottomInOutSpace > this.TopInOutSpace)
                {
                    symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                }
                else
                {
                    symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                }
                symbol2.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                this.method_23(x, this.MapXInterval, out str, out str2);
                if (str.Length > 0)
                {
                    element2.AddElement(this.method_26(point.X, point.Y - ((this.BottomInOutSpace > this.TopInOutSpace) ? 0.0 : this.BottomInOutSpace), str, symbol2) as IElement);
                }
                element2.AddElement(this.method_26(point.X, point.Y - ((this.BottomInOutSpace > this.TopInOutSpace) ? 0.0 : this.BottomInOutSpace), str2, symbol3) as IElement);
                symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                if (str.Length > 0)
                {
                    element2.AddElement(this.method_26(point.X, point2.Y + this.TopInOutSpace, str, symbol2) as IElement);
                }
                element2.AddElement(this.method_26(point.X, point2.Y + this.TopInOutSpace, str2, symbol3) as IElement);
                x = ipoint_0.Y;
                if (Math.Abs(x) > num5)
                {
                    x = ((int) (x / num5)) * num5;
                }
                PointClass class2 = new PointClass {
                    X = point.X,
                    Y = point.Y
                };
                IPoint point3 = class2;
                double num6 = 0.0;
                double num7 = 0.0;
                this.method_23(x, this.MapXInterval, out str, out str2);
                TextElementClass class3 = new TextElementClass {
                    Geometry = point3,
                    Symbol = symbol2,
                    Text = str
                };
                ITextElement element3 = class3;
                jlktransformation_0.TextWidth(element3, out num6, out num7);
                symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                symbol2.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                if (str.Length > 0)
                {
                    element2.AddElement(this.method_26(point.X - this.LeftInOutSpace, point.Y, str, symbol2) as IElement);
                }
                element2.AddElement(this.method_26((point.X - this.LeftInOutSpace) + num6, point.Y, str2, symbol3) as IElement);
                symbol2.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                if (str.Length > 0)
                {
                    TextElementClass class4 = new TextElementClass {
                        Geometry = point3,
                        Symbol = symbol3,
                        Text = str2
                    };
                    element3 = class4;
                    jlktransformation_0.TextWidth(element3, out num6, out num7);
                    element2.AddElement(this.method_26((point2.X + this.RightInOutSpace) - num6, point.Y, str, symbol2) as IElement);
                }
                element2.AddElement(this.method_26(point2.X + this.RightInOutSpace, point.Y, str2, symbol3) as IElement);
                x = ((int) (ipoint_1.X / num4)) * num4;
                if (this.BottomInOutSpace > this.TopInOutSpace)
                {
                    symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                    symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                }
                else
                {
                    symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                    symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                }
                symbol2.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                this.method_23(x, this.MapXInterval, out str, out str2);
                if (str.Length > 0)
                {
                    element2.AddElement(this.method_26(point2.X, point.Y - ((this.BottomInOutSpace > this.TopInOutSpace) ? 0.0 : this.BottomInOutSpace), str, symbol2) as IElement);
                }
                element2.AddElement(this.method_26(point2.X, point.Y - ((this.BottomInOutSpace > this.TopInOutSpace) ? 0.0 : this.BottomInOutSpace), str2, symbol3) as IElement);
                symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
                if (str.Length > 0)
                {
                    element2.AddElement(this.method_26(point2.X, point2.Y + this.TopInOutSpace, str, symbol2) as IElement);
                }
                element2.AddElement(this.method_26(point2.X, point2.Y + this.TopInOutSpace, str2, symbol3) as IElement);
                x = ipoint_1.Y;
                if (Math.Abs(x) > num5)
                {
                    x = ((int) (x / num5)) * num5;
                }
                this.method_23(x, this.MapYInterval, out str, out str2);
                TextElementClass class5 = new TextElementClass {
                    Geometry = point3,
                    Symbol = symbol2,
                    Text = str
                };
                element3 = class5;
                jlktransformation_0.TextWidth(element3, out num6, out num7);
                symbol2.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                symbol3.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
                symbol2.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                if (str.Length > 0)
                {
                    element2.AddElement(this.method_26(point.X - this.LeftInOutSpace, point2.Y, str, symbol2) as IElement);
                }
                element2.AddElement(this.method_26((point.X - this.LeftInOutSpace) + num6, point2.Y, str2, symbol3) as IElement);
                symbol2.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                symbol3.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
                if (str.Length > 0)
                {
                    TextElementClass class6 = new TextElementClass {
                        Geometry = point3,
                        Symbol = symbol3,
                        Text = str2
                    };
                    element3 = class6;
                    jlktransformation_0.TextWidth(element3, out num6, out num7);
                    element2.AddElement(this.method_26((point2.X + this.RightInOutSpace) - num6, point2.Y, str, symbol2) as IElement);
                }
                element2.AddElement(this.method_26(point2.X + this.RightInOutSpace, point2.Y, str2, symbol3) as IElement);
            }
            return (element2 as IElement);
        }

        protected IElement CreateCornerShortLine(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            if (this.BorderSymbol == null)
            {
                return null;
            }
            ILineSymbol symbol = this.method_25();
            IGroupElement element2 = new GroupElementClass();
            element2.AddElement(this.method_29(ipoint_0.X, ipoint_0.Y, -this.LeftInOutSpace, -this.BottomInOutSpace, symbol));
            element2.AddElement(this.method_29(ipoint_1.X, ipoint_1.Y, -this.LeftInOutSpace, this.TopInOutSpace, symbol));
            element2.AddElement(this.method_29(ipoint_2.X, ipoint_2.Y, this.RightInOutSpace, this.TopInOutSpace, symbol));
            element2.AddElement(this.method_29(ipoint_3.X, ipoint_3.Y, this.RightInOutSpace, -this.BottomInOutSpace, symbol));
            return (element2 as IElement);
        }

        public void CreateDesignTK(IActiveView iactiveView_0)
        {
            IMapFrame focusMapFrame;
            IEnvelope mapBounds;
            double xMin;
            double num;
            (iactiveView_0.FocusMap as IMapClipOptions).ClipType = esriMapClipType.esriMapClipNone;
            if (iactiveView_0.FocusMap is IMapAutoExtentOptions)
            {
                (iactiveView_0.FocusMap as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentDefault;
            }
            if (this.MapFramingType == MapFramingType.AnyFraming)
            {
                focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
                IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
                IEnvelope envelopeClass = new EnvelopeClass();
                envelopeClass.PutCoords(3, 3, 53, 53);
                ITransform2D transform2D = focusMapFrame as ITransform2D;
                IAffineTransformation2D affineTransformation2DClass = new AffineTransformation2DClass();
                affineTransformation2DClass.DefineFromEnvelopes(envelope, envelopeClass);
                transform2D.Transform(esriTransformDirection.esriTransformForward, affineTransformation2DClass);
                envelope = (focusMapFrame as IElement).Geometry.Envelope;
                mapBounds = focusMapFrame.MapBounds;
                IEnvelope extent = (iactiveView_0.FocusMap as IActiveView).Extent;
                xMin = 0;
                num = 0;
                if (mapBounds.YMin <= 0)
                {
                    num = 1000 + Math.Abs(mapBounds.YMin);
                }
                if (mapBounds.XMin <= 0)
                {
                    xMin = 500000 + mapBounds.XMin;
                }
                this.Scale = focusMapFrame.Map.MapScale;
                mapBounds.Offset(xMin, num);
                this.CreateTKByRect(iactiveView_0, mapBounds);
            }
            else if (this.MapFramingType == MapFramingType.StandardFraming)
            {
                focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
                mapBounds = iactiveView_0.Extent;
                if (this.MapFrameType == MapFrameType.MFTRect)
                {
                    xMin = 0;
                    num = 0;
                    if (mapBounds.YMin <= 0)
                    {
                        num = 1000 + Math.Abs(mapBounds.YMin);
                    }
                    if (mapBounds.XMin <= 0)
                    {
                        xMin = 500000 + mapBounds.XMin;
                    }
                    mapBounds.Offset(xMin, num);
                    (focusMapFrame.Map as IActiveView).Extent = mapBounds;
                    this.CreateTKN(iactiveView_0, mapBounds.LowerLeft);
                }
                else if (this.MapFrameType == MapFrameType.MFTTrapezoid)
                {
                    string str = "G";
                    int scale = (int)this.Scale;
                    if (scale <= 10000)
                    {
                        if (scale <= 1000)
                        {
                            if (scale == 500)
                            {
                                str = "K";
                            }
                            else if (scale == 1000)
                            {
                                str = "J";
                            }
                        }
                        else if (scale == 2000)
                        {
                            str = "I";
                        }
                        else if (scale == 5000)
                        {
                            str = "H";
                        }
                        else if (scale == 10000)
                        {
                            str = "G";
                        }
                    }
                    else if (scale <= 50000)
                    {
                        if (scale == 25000)
                        {
                            str = "F";
                        }
                        else if (scale == 50000)
                        {
                            str = "E";
                        }
                    }
                    else if (scale == 100000)
                    {
                        str = "D";
                    }
                    else if (scale == 250000)
                    {
                        str = "C";
                    }
                    else if (scale == 500000)
                    {
                        str = "B";
                    }
                    string str1 = "";
                    str1 = (this.Scale <= 1000 ? string.Concat("J50", str, "00010001") : string.Concat("J50", str, "001001"));
                    MapNoAssistant mapNoAssistant = MapNoAssistantFactory.CreateMapNoAssistant(str1);
                    this.Scale = (double)mapNoAssistant.GetScale();
                    this.double_6 = this.Scale;
                    this.CreateTrapezoidTK(iactiveView_0 as IPageLayout, mapNoAssistant);
                }
            }
        }


        public void CreateTK(IActiveView iactiveView_0)
        {
            double width = this.Width;
            double height = this.Height;
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
            IEnvelope from = (focusMapFrame as IElement).Geometry.Envelope;
            double num3 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 8.0;
            double num4 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 8.0;
            IEnvelope to = new EnvelopeClass();
            double xMin = (this.LeftInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
            double yMin = (this.BottomInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
            to.PutCoords(xMin, yMin, from.Width + xMin, from.Height + yMin);
            IAffineTransformation2D transformation = new AffineTransformation2DClass();
            transformation.DefineFromEnvelopes(from, to);
            (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
            (iactiveView_0 as IPageLayout).Page.PutCustomSize(from.Width + num3, from.Height + num4);
            if (this.MapFrameType == MapCartoTemplateLib.MapFrameType.MFTTrapezoid)
            {
                this.method_6(iactiveView_0 as IPageLayout);
            }
            else
            {
                IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
                double mapScale = focusMapFrame.MapScale;
                if (mapScale != 0.0)
                {
                    this.double_6 = mapScale;
                    if (!this.IsAdapationScale)
                    {
                    }
                }
                else
                {
                    this.double_6 = 500.0;
                }
                if (this.MapGrid != null)
                {
                    (this.MapGrid as IMeasuredGrid).XIntervalSize = (this.XInterval * this.double_6) / 100.0;
                    (this.MapGrid as IMeasuredGrid).YIntervalSize = (this.YInterval * this.double_6) / 100.0;
                    (focusMapFrame as IMapGrids).AddMapGrid(this.MapGrid);
                    if (this.BorderSymbol != null)
                    {
                        IElement element = this.method_14(focusMapFrame, this.BorderSymbol);
                        (iactiveView_0 as IGraphicsContainer).AddElement(element, -1);
                    }
                }
                else
                {
                    this.Width = from.Width;
                    this.Height = from.Height;
                    this.method_3(iactiveView_0 as IPageLayout, extent.LowerLeft, extent.UpperRight);
                }
            }
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            this.double_2 = envelope.XMin;
            this.double_3 = envelope.YMin;
            this.double_4 = envelope.XMax;
            this.double_5 = envelope.YMax;
            this.method_0(iactiveView_0);
            double num8 = ((((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_4) - this.double_2;
            double num9 = ((((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_3;
            (iactiveView_0 as IPageLayout).Page.PutCustomSize(num8, num9);
            this.Width = width;
            this.Height = height;
        }

        public void CreateTK(IActiveView iactiveView_0, MapNoAssistant mapNoAssistant_0)
        {
            if (this.CanCreateTK(iactiveView_0))
            {
                double num3;
                double num4;
                IEnvelope envelope2;
                IAffineTransformation2D transformationd;
                double width = this.Width;
                double height = this.Height;
                IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
                IEnvelope from = (focusMapFrame as IElement).Geometry.Envelope;
                if (this.MapFrameType == MapCartoTemplateLib.MapFrameType.MFTTrapezoid)
                {
                    if (this.TemplateSizeStyle == MapCartoTemplateLib.TemplateSizeStyle.SameAsMapFrame)
                    {
                        num3 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 8.0;
                        num4 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 8.0;
                        envelope2 = new EnvelopeClass();
                        double xMin = (this.LeftInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
                        double yMin = (this.BottomInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
                        envelope2.PutCoords(xMin, yMin, from.Width + xMin, from.Height + yMin);
                        transformationd = new AffineTransformation2DClass();
                        transformationd.DefineFromEnvelopes(from, envelope2);
                        ITransform2D transformd = focusMapFrame as ITransform2D;
                        transformd.Transform(esriTransformDirection.esriTransformForward, transformationd);
                        (iactiveView_0 as IPageLayout).Page.PutCustomSize(from.Width + num3, from.Height + num4);
                        this.method_6(iactiveView_0 as IPageLayout);
                    }
                    else
                    {
                        if (mapNoAssistant_0 == null)
                        {
                            mapNoAssistant_0 = new LandUseMapNoAssistant("GF490994");
                        }
                        this.Scale = mapNoAssistant_0.GetScale();
                        this.double_6 = this.Scale;
                        this.CreateTrapezoidTK(iactiveView_0 as IPageLayout, mapNoAssistant_0);
                    }
                    this.Width = width;
                    this.Height = height;
                }
                else
                {
                    this.double_6 = this.Scale;
                    if (focusMapFrame.MapBounds == null)
                    {
                        focusMapFrame.MapBounds = (focusMapFrame.Map as IActiveView).FullExtent;
                    }
                    if (this.TemplateSizeStyle == MapCartoTemplateLib.TemplateSizeStyle.SameAsMapFrame)
                    {
                        this.CreateTK(iactiveView_0);
                        this.Width = width;
                        this.Height = height;
                    }
                    else
                    {
                        if (this.IsAdapationScale)
                        {
                            this.Scale = 1.0;
                        }
                        double num1 = ((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 8.0;
                        double num13 = ((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 8.0;
                        envelope2 = new EnvelopeClass();
                        double num7 = ((this.BorderSymbol == null) ? 0.0 : (this.LeftInOutSpace + (this.OutBorderWidth / 2.0))) + 2.0;
                        double num8 = ((this.BorderSymbol == null) ? 0.0 : (this.BottomInOutSpace + (this.OutBorderWidth / 2.0))) + 2.0;
                        envelope2.PutCoords(num7, num8, width + num7, height + num8);
                        transformationd = new AffineTransformation2DClass();
                        transformationd.DefineFromEnvelopes(from, envelope2);
                        (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformationd);
                        IEnvelope envelope1 = (focusMapFrame as IElement).Geometry.Envelope;
                        envelope2 = new EnvelopeClass();
                        num3 = (this.Width * this.Scale) / 100.0;
                        num4 = (this.Height * this.Scale) / 100.0;
                        IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
                        double x = extent.LowerLeft.X;
                        double y = extent.LowerLeft.Y;
                        x = ((int) (x / num3)) * num3;
                        y = ((int) (y / num4)) * num4;
                        if (x < 0.0)
                        {
                            x = 1000.0;
                        }
                        if (y < 0.0)
                        {
                            y = 1000.0;
                        }
                        envelope2.PutCoords(x, y, x + ((this.Width * this.Scale) / 100.0), y + ((this.Height * this.Scale) / 100.0));
                        IActiveView map = focusMapFrame.Map as IActiveView;
                        (focusMapFrame.Map as IActiveView).Extent = envelope2;
                        IEnvelope envelope6 = (focusMapFrame as IElement).Geometry.Envelope;
                        focusMapFrame.Map.MapScale = this.Scale;
                        IEnvelope envelope7 = (focusMapFrame.Map as IActiveView).Extent;
                        if (width < 0.0)
                        {
                            width = -width;
                        }
                        IGraphicsContainer graphicsContainer = iactiveView_0.GraphicsContainer;
                        envelope2.PutCoords(num7, num8, width + num7, height + num8);
                        IEnvelope envelope8 = (focusMapFrame as IElement).Geometry.Envelope;
                        this.method_2(iactiveView_0 as IPageLayout, x, y);
                    }
                }
                IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
                this.double_2 = envelope.XMin;
                this.double_3 = envelope.YMin;
                this.double_4 = envelope.XMax;
                this.double_5 = envelope.YMax;
                this.method_0(iactiveView_0);
                double num11 = ((((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_4) - this.double_2;
                if (this.double_3 < 0.0)
                {
                    this.double_3 = 0.0;
                }
                double num12 = ((((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_3;
                (iactiveView_0 as IPageLayout).Page.PutCustomSize(num11, num12);
            }
        }

        public void CreateTK(IActiveView iactiveView_0, double double_22, double double_23)
        {
            this.double_6 = this.Scale;
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
            if (focusMapFrame.Map is IMapAutoExtentOptions)
            {
                (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentDefault;
            }
            double width = this.Width;
            double height = this.Height;
            IEnvelope from = (focusMapFrame as IElement).Geometry.Envelope;
            IEnvelope to = new EnvelopeClass();
            double xMin = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 2.0;
            double yMin = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 2.0;
            to.PutCoords(xMin, yMin, width + xMin, height + yMin);
            IAffineTransformation2D transformation = new AffineTransformation2DClass();
            transformation.DefineFromEnvelopes(from, to);
            (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            to = new EnvelopeClass();
            double num5 = (this.Width * this.Scale) / 100.0;
            double num6 = (this.Height * this.Scale) / 100.0;
            IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
            double num7 = double_22;
            double num8 = double_23;
            num7 = ((int) (num7 / num5)) * num5;
            num8 = ((int) (num8 / num6)) * num6;
            to.PutCoords(num7, num8, num7 + ((this.Width * this.Scale) / 100.0), num8 + ((this.Height * this.Scale) / 100.0));
            IActiveView map = focusMapFrame.Map as IActiveView;
            IEnvelope envelope6 = (focusMapFrame.Map as IActiveView).Extent;
            (focusMapFrame.Map as IActiveView).Extent = to;
            IEnvelope envelope7 = (focusMapFrame.Map as IActiveView).Extent;
            focusMapFrame.Map.MapScale = this.Scale;
            envelope = (focusMapFrame as IElement).Geometry.Envelope;
            envelope = (focusMapFrame as IElement).Geometry.Envelope;
            focusMapFrame.MapBounds = to;
            (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentBounds = to;
            if (width < 0.0)
            {
                width = -width;
            }
            double num9 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 5.0;
            double num10 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 5.0;
            (iactiveView_0 as IPageLayout).Page.PutCustomSize(width + num9, height + num10);
            IGraphicsContainer graphicsContainer = iactiveView_0.GraphicsContainer;
            this.method_2(iactiveView_0 as IPageLayout, num7, num8);
            IEnvelope envelope4 = (focusMapFrame as IElement).Geometry.Envelope;
            this.double_2 = envelope4.XMin;
            this.double_3 = envelope4.YMin;
            this.double_4 = envelope4.XMax;
            this.double_5 = envelope4.YMax;
            this.method_0(iactiveView_0);
            double num11 = ((((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_4) - this.double_2;
            double num12 = ((((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_3;
            (iactiveView_0 as IPageLayout).Page.PutCustomSize(num11, num12);
            if (focusMapFrame.Map is IMapAutoExtentOptions)
            {
                (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentBounds;
                (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentBounds = envelope;
                IEnvelope autoExtentBounds = (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentBounds;
            }
        }

        public void CreateTKByRect(IActiveView iactiveView_0, IEnvelope ienvelope_0)
        {
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
            IEnvelope from = (focusMapFrame as IElement).Geometry.Envelope;
            double width = from.Width;
            double height = from.Height;
            double num3 = (ienvelope_0.Width / width) * 100.0;
            double num4 = (ienvelope_0.Height / height) * 100.0;
            num3 = (num3 > num4) ? num3 : num4;
            int num5 = (int) (num3 / 100.0);
            this.Scale = num5 * 100;
            this.double_6 = this.Scale;
            width = (ienvelope_0.Width / this.Scale) * 100.0;
            height = (ienvelope_0.Height / this.Scale) * 100.0;
            if (!(this.IsTest || !this.FixedWidthAndBottomSpace))
            {
                this.OldBottomInOutSpace = this.BottomInOutSpace;
                this.BottomInOutSpace = (this.BottomInOutSpace * width) / this.Width;
                this.IsChangeBottomLength = true;
                this.BottomLengthScale = width / this.Width;
            }
            double num1 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 8.0;
            double num11 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 8.0;
            IEnvelope to = new EnvelopeClass();
            double xMin = (this.LeftInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
            double yMin = (this.BottomInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
            to.PutCoords(xMin, yMin, width + xMin, height + yMin);
            IAffineTransformation2D transformation = new AffineTransformation2DClass();
            transformation.DefineFromEnvelopes(from, to);
            (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
            (focusMapFrame.Map as IActiveView).Extent = ienvelope_0;
            focusMapFrame.Map.MapScale = this.Scale;
            if (this.MapFrameType == MapCartoTemplateLib.MapFrameType.MFTTrapezoid)
            {
                PointClass class2 = new PointClass {
                    X = ienvelope_0.XMin,
                    Y = ienvelope_0.YMin
                };
                IPoint point = class2;
                PointClass class3 = new PointClass {
                    X = ienvelope_0.XMin,
                    Y = ienvelope_0.YMax
                };
                IPoint point2 = class3;
                PointClass class4 = new PointClass {
                    X = ienvelope_0.XMax,
                    Y = ienvelope_0.YMax
                };
                IPoint point3 = class4;
                PointClass class5 = new PointClass {
                    X = ienvelope_0.XMax,
                    Y = ienvelope_0.YMin
                };
                IPoint point4 = class5;
                this.method_10(iactiveView_0 as IPageLayout, point, point2, point3, point4);
            }
            else
            {
                IEnvelope envelope4 = (focusMapFrame.Map as IActiveView).Extent;
                focusMapFrame.MapScale = this.Scale;
                double mapScale = focusMapFrame.MapScale;
                IEnvelope envelope3 = (focusMapFrame.Map as IActiveView).Extent;
                if (mapScale != 0.0)
                {
                    this.double_6 = mapScale;
                }
                else
                {
                    this.double_6 = 500.0;
                }
                if (this.MapGrid != null)
                {
                    (this.MapGrid as IMeasuredGrid).XIntervalSize = (this.XInterval * this.Scale) / 100.0;
                    (this.MapGrid as IMeasuredGrid).YIntervalSize = (this.YInterval * this.Scale) / 100.0;
                    (focusMapFrame as IMapGrids).AddMapGrid(this.MapGrid);
                    if (this.BorderSymbol != null)
                    {
                        IElement element = this.method_14(focusMapFrame, this.BorderSymbol);
                        (iactiveView_0 as IGraphicsContainer).AddElement(element, -1);
                    }
                }
                else
                {
                    this.Width = from.Width;
                    this.Height = from.Height;
                    this.method_3(iactiveView_0 as IPageLayout, envelope4.LowerLeft, envelope4.UpperRight);
                }
            }
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            this.double_2 = envelope.XMin;
            this.double_3 = envelope.YMin;
            this.double_4 = envelope.XMax;
            this.double_5 = envelope.YMax;
            this.method_0(iactiveView_0);
            double num9 = ((((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_4) - this.double_2;
            double num10 = ((((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_3;
            (iactiveView_0 as IPageLayout).Page.PutCustomSize(num9, num10);
            this.Width = width;
            this.Height = height;
            IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
            if (focusMapFrame.Map is IMapAutoExtentOptions)
            {
                (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentBounds;
                (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentBounds = extent;
                IEnvelope autoExtentBounds = (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentBounds;
            }
        }

        public void CreateTKEx(IActiveView iactiveView_0, IEnvelope ienvelope_0)
        {
            IMapFrame focusMapFrame;
            if (ienvelope_0 == null)
            {
                focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
                if (this.IsTest && !this.IsAdapationScale)
                {
                    focusMapFrame.Map.MapScale = this.Scale;
                }
                ienvelope_0 = focusMapFrame.MapBounds;
                double x = 0.0;
                double y = 0.0;
                if (ienvelope_0.YMin < 0.0)
                {
                    y = 1000.0 + Math.Abs(ienvelope_0.YMin);
                }
                if (ienvelope_0.XMin < 0.0)
                {
                    x = 1000.0 + Math.Abs(ienvelope_0.XMin);
                }
                ienvelope_0.Offset(x, y);
            }
            if (this.IsAdapationScale)
            {
                focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
                double width = this.Width;
                double height = this.Height;
                double num5 = (ienvelope_0.Width / width) * 100.0;
                double num6 = (ienvelope_0.Height / height) * 100.0;
                num5 = (num5 > num6) ? num5 : num6;
                int num7 = ((int) (num5 / 100.0)) + 1;
                this.Scale = num7 * 100;
                this.double_6 = this.Scale;
                IEnvelope from = (focusMapFrame as IElement).Geometry.Envelope;
                IEnvelope to = new EnvelopeClass();
                this.method_1();
                to.PutCoords(2.0, 2.0, width + 2.0, height + 2.0);
                IAffineTransformation2D transformation = new AffineTransformation2DClass();
                transformation.DefineFromEnvelopes(from, to);
                (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
                IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
                to = new EnvelopeClass();
                double num8 = (this.Width * this.double_6) / 100.0;
                double num9 = (this.Height * this.double_6) / 100.0;
                IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
                double num10 = num8 - ienvelope_0.Width;
                double num11 = num9 - ienvelope_0.Height;
                double num12 = ienvelope_0.XMin - (num10 / 2.0);
                double num13 = ienvelope_0.YMin - (num11 / 2.0);
                double xMin = num12;
                double yMin = num13;
                to.PutCoords(xMin, yMin, xMin + ((this.Width * this.double_6) / 100.0), yMin + ((this.Height * this.double_6) / 100.0));
                IActiveView map = focusMapFrame.Map as IActiveView;
                (focusMapFrame.Map as IActiveView).Extent = to;
                focusMapFrame.Map.MapScale = this.Scale;
                envelope = (focusMapFrame as IElement).Geometry.Envelope;
                envelope = (focusMapFrame as IElement).Geometry.Envelope;
                if (width < 0.0)
                {
                    width = -width;
                }
                IGraphicsContainer graphicsContainer = iactiveView_0.GraphicsContainer;
                to.PutCoords(2.0, 2.0, width + 2.0, height + 2.0);
                envelope = (focusMapFrame as IElement).Geometry.Envelope;
                envelope = (focusMapFrame.Map as IActiveView).Extent;
                this.method_2(iactiveView_0 as IPageLayout, xMin, yMin);
                IEnvelope envelope4 = (focusMapFrame as IElement).Geometry.Envelope;
                this.double_2 = envelope4.XMin;
                this.double_3 = envelope4.YMin;
                this.double_4 = envelope4.XMax;
                this.double_5 = envelope4.YMax;
                this.method_0(iactiveView_0);
                double num16 = ((((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_4) - this.double_2;
                double num17 = ((((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_3;
                (iactiveView_0 as IPageLayout).Page.PutCustomSize(num16, num17);
                if (focusMapFrame.Map is IMapAutoExtentOptions)
                {
                    (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentType = esriExtentTypeEnum.esriExtentBounds;
                    (focusMapFrame.Map as IMapAutoExtentOptions).AutoExtentBounds = envelope;
                }
            }
            else
            {
                this.CreateTKByRect(iactiveView_0, ienvelope_0);
            }
        }

        public void CreateTKEx(IActiveView iactiveView_0, double double_22, double double_23)
        {
            this.double_6 = this.Scale;
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
            double width = this.Width;
            double height = this.Height;
            IEnvelope from = (focusMapFrame as IElement).Geometry.Envelope;
            IEnvelope to = new EnvelopeClass();
            to.PutCoords(2.0, 2.0, width + 2.0, height + 2.0);
            IAffineTransformation2D transformation = new AffineTransformation2DClass();
            transformation.DefineFromEnvelopes(from, to);
            ITransform2D transformd = focusMapFrame as ITransform2D;
            transformd.Transform(esriTransformDirection.esriTransformForward, transformation);
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            to = new EnvelopeClass();
            double num1 = (this.Width * this.Scale) / 100.0;
            double num7 = (this.Height * this.Scale) / 100.0;
            IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
            double xMin = double_22;
            double yMin = double_23;
            to.PutCoords(xMin, yMin, xMin + ((this.Width * this.Scale) / 100.0), yMin + ((this.Height * this.Scale) / 100.0));
            IActiveView map = focusMapFrame.Map as IActiveView;
            (focusMapFrame.Map as IActiveView).Extent = to;
            focusMapFrame.Map.MapScale = this.Scale;
            IEnvelope envelope6 = (focusMapFrame as IElement).Geometry.Envelope;
            IEnvelope envelope7 = (focusMapFrame as IElement).Geometry.Envelope;
            if (width < 0.0)
            {
                width = -width;
            }
            double num8 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 5.0;
            double num9 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 5.0;
            transformd.Transform(esriTransformDirection.esriTransformForward, transformation);
            IGraphicsContainer graphicsContainer = iactiveView_0.GraphicsContainer;
            to.PutCoords(2.0, 2.0, width + 2.0, height + 2.0);
            (focusMapFrame as IElement).Geometry = to;
            IEnvelope envelope8 = (focusMapFrame as IElement).Geometry.Envelope;
            IEnvelope envelope9 = (focusMapFrame.Map as IActiveView).Extent;
            this.method_2(iactiveView_0 as IPageLayout, xMin, yMin);
            IEnvelope envelope3 = (focusMapFrame as IElement).Geometry.Envelope;
            this.double_2 = envelope3.XMin;
            this.double_3 = envelope3.YMin;
            this.double_4 = envelope3.XMax;
            this.double_5 = envelope3.YMax;
            this.method_0(iactiveView_0);
            double num5 = ((((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_2;
            double num6 = ((((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_3;
            (iactiveView_0 as IPageLayout).Page.PutCustomSize(num5, num6);
        }

        public void CreateTKN(IActiveView iactiveView_0)
        {
            double width = this.Width;
            double height = this.Height;
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
            IEnvelope from = (focusMapFrame as IElement).Geometry.Envelope;
            double num3 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 8.0;
            double num4 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 8.0;
            IEnvelope to = new EnvelopeClass();
            double xMin = (this.LeftInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
            double yMin = (this.BottomInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
            to.PutCoords(xMin, yMin, from.Width + xMin, from.Height + yMin);
            IAffineTransformation2D transformation = new AffineTransformation2DClass();
            transformation.DefineFromEnvelopes(from, to);
            (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformation);
            (iactiveView_0 as IPageLayout).Page.PutCustomSize(from.Width + num3, from.Height + num4);
            IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
            double mapScale = focusMapFrame.MapScale;
            if (mapScale != 0.0)
            {
                this.double_6 = ((int) (mapScale / 100.0)) * 100.0;
                if (!this.IsAdapationScale)
                {
                }
            }
            else
            {
                this.double_6 = 500.0;
            }
            if (this.MapGrid != null)
            {
                (this.MapGrid as IMeasuredGrid).XIntervalSize = (this.XInterval * this.double_6) / 100.0;
                (this.MapGrid as IMeasuredGrid).YIntervalSize = (this.YInterval * this.double_6) / 100.0;
                (focusMapFrame as IMapGrids).AddMapGrid(this.MapGrid);
                if (this.BorderSymbol != null)
                {
                    IElement element = this.method_14(focusMapFrame, this.BorderSymbol);
                    (iactiveView_0 as IGraphicsContainer).AddElement(element, -1);
                }
            }
            else
            {
                this.Width = from.Width;
                this.Height = from.Height;
                this.method_3(iactiveView_0 as IPageLayout, extent.LowerLeft, extent.UpperRight);
            }
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            this.double_2 = envelope.XMin;
            this.double_3 = envelope.YMin;
            this.double_4 = envelope.XMax;
            this.double_5 = envelope.YMax;
            this.method_0(iactiveView_0);
            double num8 = ((((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_4) - this.double_2;
            double num9 = ((((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_3;
            (iactiveView_0 as IPageLayout).Page.PutCustomSize(num8, num9);
            this.Width = width;
            this.Height = height;
        }

        public void CreateTKN(IActiveView iactiveView_0, IPoint ipoint_0)
        {
            if (this.MapFrameType == MapCartoTemplateLib.MapFrameType.MFTTrapezoid)
            {
                ISpatialReference spatialReference = iactiveView_0.FocusMap.SpatialReference;
                if (spatialReference is IProjectedCoordinateSystem)
                {
                    IGeographicCoordinateSystem geographicCoordinateSystem = (spatialReference as IProjectedCoordinateSystem).GeographicCoordinateSystem;
                    ipoint_0.SpatialReference = spatialReference;
                    ipoint_0.Project(geographicCoordinateSystem);
                    THTools tools = new THTools();
                    string str = "";
                    if (tools.BL2FileName_standard((int) this.Scale, ipoint_0.X, ipoint_0.Y, out str))
                    {
                        MapCartoTemplateLib.MapTemplateParam param = this.FindParamByName("图号");
                        if (param != null)
                        {
                            param.Value = str;
                        }
                        MapNoAssistant assistant = MapNoAssistantFactory.CreateMapNoAssistant(str);
                        this.Scale = assistant.GetScale();
                        this.double_6 = this.Scale;
                        this.CreateTrapezoidTK(iactiveView_0 as IPageLayout, assistant);
                    }
                }
            }
            else
            {
                double num = (this.Width * this.Scale) / 100.0;
                double num2 = (this.Height * this.Scale) / 100.0;
                double num3 = ((int) (ipoint_0.X / num)) * num;
                double num4 = ((int) (ipoint_0.Y / num2)) * num2;
                if (this.DrawJWD)
                {
                    double num5 = num3 + num;
                    double num6 = num4 + num2;
                    PointClass class2 = new PointClass {
                        X = num3,
                        Y = num4
                    };
                    IPoint point = class2;
                    PointClass class3 = new PointClass {
                        X = num3,
                        Y = num6
                    };
                    IPoint point2 = class3;
                    PointClass class4 = new PointClass {
                        X = num5,
                        Y = num6
                    };
                    IPoint point3 = class4;
                    PointClass class5 = new PointClass {
                        X = num5,
                        Y = num4
                    };
                    IPoint point4 = class5;
                    this.method_10(iactiveView_0 as IPageLayout, point, point2, point3, point4);
                }
                else
                {
                    this.CreateTK(iactiveView_0, num3, num4);
                }
            }
        }

        public void CreateTKN(IActiveView iactiveView_0, MapNoAssistant mapNoAssistant_0)
        {
            if (this.CanCreateTK(iactiveView_0))
            {
                double num3;
                double num4;
                IEnvelope envelope2;
                IAffineTransformation2D transformationd;
                double width = this.Width;
                double height = this.Height;
                IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(iactiveView_0 as IPageLayout);
                IEnvelope from = (focusMapFrame as IElement).Geometry.Envelope;
                if (this.MapFramingType != MapCartoTemplateLib.MapFramingType.StandardFraming)
                {
                }
                if (this.MapFrameType == MapCartoTemplateLib.MapFrameType.MFTTrapezoid)
                {
                    if (this.TemplateSizeStyle == MapCartoTemplateLib.TemplateSizeStyle.SameAsMapFrame)
                    {
                        num3 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 8.0;
                        num4 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 8.0;
                        envelope2 = new EnvelopeClass();
                        double xMin = (this.LeftInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
                        double yMin = (this.BottomInOutSpace + (this.OutBorderWidth / 2.0)) + 2.0;
                        envelope2.PutCoords(xMin, yMin, from.Width + xMin, from.Height + yMin);
                        transformationd = new AffineTransformation2DClass();
                        transformationd.DefineFromEnvelopes(from, envelope2);
                        ITransform2D transformd = focusMapFrame as ITransform2D;
                        transformd.Transform(esriTransformDirection.esriTransformForward, transformationd);
                        (iactiveView_0 as IPageLayout).Page.PutCustomSize(from.Width + num3, from.Height + num4);
                        this.method_6(iactiveView_0 as IPageLayout);
                    }
                    else
                    {
                        if (mapNoAssistant_0 == null)
                        {
                            mapNoAssistant_0 = new LandUseMapNoAssistant("GF490994");
                        }
                        this.Scale = mapNoAssistant_0.GetScale();
                        this.double_6 = this.Scale;
                        this.CreateTrapezoidTK(iactiveView_0 as IPageLayout, mapNoAssistant_0);
                    }
                    this.Width = width;
                    this.Height = height;
                }
                else
                {
                    this.double_6 = this.Scale;
                    if (focusMapFrame.MapBounds == null)
                    {
                        focusMapFrame.MapBounds = (focusMapFrame.Map as IActiveView).FullExtent;
                    }
                    if (this.TemplateSizeStyle == MapCartoTemplateLib.TemplateSizeStyle.SameAsMapFrame)
                    {
                        this.CreateTK(iactiveView_0);
                        this.Width = width;
                        this.Height = height;
                    }
                    else
                    {
                        if (this.IsAdapationScale)
                        {
                            this.Scale = 1.0;
                        }
                        double num1 = ((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 8.0;
                        double num13 = ((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 8.0;
                        envelope2 = new EnvelopeClass();
                        double num7 = ((this.BorderSymbol == null) ? 0.0 : (this.LeftInOutSpace + (this.OutBorderWidth / 2.0))) + 2.0;
                        double num8 = ((this.BorderSymbol == null) ? 0.0 : (this.BottomInOutSpace + (this.OutBorderWidth / 2.0))) + 2.0;
                        envelope2.PutCoords(num7, num8, width + num7, height + num8);
                        transformationd = new AffineTransformation2DClass();
                        transformationd.DefineFromEnvelopes(from, envelope2);
                        (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformationd);
                        IEnvelope envelope1 = (focusMapFrame as IElement).Geometry.Envelope;
                        envelope2 = new EnvelopeClass();
                        num3 = (this.Width * this.Scale) / 100.0;
                        num4 = (this.Height * this.Scale) / 100.0;
                        IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
                        double x = extent.LowerLeft.X;
                        double y = extent.LowerLeft.Y;
                        x = ((int) (x / num3)) * num3;
                        y = ((int) (y / num4)) * num4;
                        if (x < 0.0)
                        {
                            x = 1000.0;
                        }
                        if (y < 0.0)
                        {
                            y = 1000.0;
                        }
                        envelope2.PutCoords(x, y, x + ((this.Width * this.Scale) / 100.0), y + ((this.Height * this.Scale) / 100.0));
                        IActiveView map = focusMapFrame.Map as IActiveView;
                        (focusMapFrame.Map as IActiveView).Extent = envelope2;
                        IEnvelope envelope6 = (focusMapFrame as IElement).Geometry.Envelope;
                        focusMapFrame.Map.MapScale = this.Scale;
                        IEnvelope envelope7 = (focusMapFrame.Map as IActiveView).Extent;
                        if (width < 0.0)
                        {
                            width = -width;
                        }
                        IGraphicsContainer graphicsContainer = iactiveView_0.GraphicsContainer;
                        envelope2.PutCoords(num7, num8, width + num7, height + num8);
                        IEnvelope envelope8 = (focusMapFrame as IElement).Geometry.Envelope;
                        this.method_2(iactiveView_0 as IPageLayout, x, y);
                    }
                }
                IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
                this.double_2 = envelope.XMin;
                this.double_3 = envelope.YMin;
                this.double_4 = envelope.XMax;
                this.double_5 = envelope.YMax;
                this.method_0(iactiveView_0);
                double num11 = ((((this.BorderSymbol == null) ? 0.0 : ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_4) - this.double_2;
                if (this.double_3 < 0.0)
                {
                    this.double_3 = 0.0;
                }
                double num12 = ((((this.BorderSymbol == null) ? 0.0 : ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth)) + 6.0) + this.double_5) - this.double_3;
                (iactiveView_0 as IPageLayout).Page.PutCustomSize(num11, num12);
            }
        }

        public void CreateTrapezoidTK(IPageLayout ipageLayout_0, MapNoAssistant mapNoAssistant_0)
        {
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(ipageLayout_0);
            IEnvelope bounds = new EnvelopeClass();
            IProjectedCoordinateSystem spatialReference = focusMapFrame.Map.SpatialReference as IProjectedCoordinateSystem;
            List<IPoint> projectCoord = mapNoAssistant_0.GetProjectCoord(spatialReference);
            this.MapXInterval = (this.XInterval * this.Scale) / 100.0;
            this.MapYInterval = (this.YInterval * this.Scale) / 100.0;
            double xMin = (projectCoord[1].X < projectCoord[0].X) ? projectCoord[1].X : projectCoord[0].X;
            double xMax = (projectCoord[3].X > projectCoord[2].X) ? projectCoord[3].X : projectCoord[2].X;
            double yMin = (projectCoord[3].Y < projectCoord[0].Y) ? projectCoord[3].Y : projectCoord[0].Y;
            double yMax = (projectCoord[1].Y > projectCoord[2].Y) ? projectCoord[1].Y : projectCoord[2].Y;
            double num5 = ((xMax - xMin) / this.Scale) * 100.0;
            double num6 = ((yMax - yMin) / this.Scale) * 100.0;
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            IEnvelope to = new EnvelopeClass();
            double num7 = (this.LeftInOutSpace + (this.OutBorderWidth / 2.0)) + 4.0;
            double num8 = (this.BottomInOutSpace + (this.OutBorderWidth / 2.0)) + 4.0;
            to.PutCoords(num7, num8, num5 + num7, num6 + num8);
            IAffineTransformation2D transformationd = new AffineTransformation2DClass();
            transformationd.DefineFromEnvelopes(envelope, to);
            (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformationd);
            to = new EnvelopeClass();
            to.PutCoords(xMin, yMin, xMax, yMax);
            (focusMapFrame.Map as IActiveView).Extent = to;
            focusMapFrame.MapBounds = to;
            focusMapFrame.Map.MapScale = this.Scale;
            if (num5 < 0.0)
            {
                num5 = -num5;
            }
            double num9 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 8.0;
            double num10 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 8.0;
            ipageLayout_0.Page.PutCustomSize(num5 + num9, num6 + num10);
            if (this.BorderSymbol != null)
            {
                IElement element = this.method_14(focusMapFrame, this.BorderSymbol);
                (ipageLayout_0 as IGraphicsContainer).AddElement(element, -1);
            }
            MapCartoTemplateLib.YTTransformation transformation = new MapCartoTemplateLib.YTTransformation(ipageLayout_0 as IActiveView);
            IGroupElement element2 = this.method_28(ipageLayout_0, projectCoord[0], projectCoord[1], projectCoord[2], projectCoord[3]);
            IPointCollection points = new PolygonClass();
            object before = Missing.Value;
            object after = Missing.Value;
            points.AddPoint(projectCoord[3], ref before, ref after);
            object obj4 = Missing.Value;
            object obj5 = Missing.Value;
            points.AddPoint(projectCoord[2], ref obj4, ref obj5);
            object obj6 = Missing.Value;
            object obj7 = Missing.Value;
            points.AddPoint(projectCoord[1], ref obj6, ref obj7);
            object obj8 = Missing.Value;
            object obj9 = Missing.Value;
            points.AddPoint(projectCoord[0], ref obj8, ref obj9);
            (points as IPolygon).Close();
            (points as ITopologicalOperator).Simplify();
            (points as IGeometry).SpatialReference = focusMapFrame.Map.SpatialReference;
            (focusMapFrame.Map as IMapClipOptions).ClipType = esriMapClipType.esriMapClipShape;
            (focusMapFrame.Map as IMapClipOptions).ClipGeometry = points as IGeometry;
            if (this.DrawJWD)
            {
                IElement element3 = null;
                double num11 = 0.0;
                double num12 = 0.0;
                double num13 = 0.0;
                double num14 = 0.0;
                mapNoAssistant_0.GetBLInfo(out num12, out num11, out num14, out num13);
                try
                {
                    element3 = this.method_42(ipageLayout_0 as IActiveView, num11, num12, num13, num14, transformation.ToPageLayoutPoint(projectCoord[0]), transformation.ToPageLayoutPoint(projectCoord[1]), transformation.ToPageLayoutPoint(projectCoord[2]), transformation.ToPageLayoutPoint(projectCoord[3]));
                }
                catch (Exception)
                {
                }
                if (element3 != null)
                {
                    if (element3 is IGroupElement)
                    {
                        element3.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                        (element3 as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
                    }
                    element2.AddElement(element3);
                }
            }
            (element2 as IElement).QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            (ipageLayout_0 as IGraphicsContainer).AddElement(element2 as IElement, -1);
            (element2 as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
            (ipageLayout_0 as IGraphicsContainer).UpdateElement(element2 as IElement);
            this.method_0(ipageLayout_0 as IActiveView);
            focusMapFrame.Border = null;
            (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        public void Delete()
        {
            if (this.OID != -1)
            {
                if (this.mapTemplateElelemt != null)
                {
                    foreach (MapCartoTemplateLib.MapTemplateElement element in this.mapTemplateElelemt)
                    {
                        element.Delete();
                    }
                }
                if (this.mapTemplateParam != null)
                {
                    foreach (MapCartoTemplateLib.MapTemplateParam param in this.mapTemplateParam)
                    {
                        param.Delete();
                    }
                }
                this.MapTemplateGallery.MapTemplateTable.GetRow(this.OID).Delete();
                this.RemoveAllMapTemplateElement();
                this.RemoveAllMapTemplateParam();
            }
        }

        public MapCartoTemplateLib.MapTemplateParam FindParamByName(string string_6)
        {
            foreach (MapCartoTemplateLib.MapTemplateParam param in this.MapTemplateParam)
            {
                if (param.Name == string_6)
                {
                    return param;
                }
            }
            return null;
        }

        protected ITextSymbol FontStyle(double double_22, esriTextHorizontalAlignment esriTextHorizontalAlignment_0, esriTextVerticalAlignment esriTextVerticalAlignment_0)
        {
            ITextSymbol symbol = new TextSymbolClass();
            IFontDisp font = symbol.Font;
            font.Name = this.string_0;
            symbol.Font = font;
            IRgbColor color = new RgbColorClass {
                Blue = 0,
                Red = 0,
                Green = 0
            };
            symbol.Size = double_22;
            symbol.Color = color;
            symbol.HorizontalAlignment = esriTextHorizontalAlignment_0;
            symbol.VerticalAlignment = esriTextVerticalAlignment_0;
            return symbol;
        }

        public void Init()
        {
            if (this.OID != -1)
            {
                this.MapTemplateGallery.MapTemplateTable.GetRow(this.OID);
            }
        }

        public void Load()
        {
            if (this.OID != -1)
            {
                IRow row = null;
                row = this.MapTemplateGallery.MapTemplateTable.GetRow(this.OID);
                this.Name = RowAssisant.GetFieldValue(row, "Name").ToString();
                try
                {
                    this.Guid = RowAssisant.GetFieldValue(row, "Guid").ToString();
                }
                catch
                {
                }
                if (string.IsNullOrEmpty(this.Guid))
                {
                    this.Guid = System.Guid.NewGuid().ToString();
                    RowAssisant.SetFieldValue(row, "Guid", this.Guid);
                    row.Store();
                }
                this.Width = Convert.ToDouble(RowAssisant.GetFieldValue(row, "Width"));
                this.Height = Convert.ToDouble(RowAssisant.GetFieldValue(row, "Height"));
                this.Scale = Convert.ToDouble(RowAssisant.GetFieldValue(row, "Scale"));
                int num = Convert.ToInt16(RowAssisant.GetFieldValue(row, "FrameType"));
                if (num == 0)
                {
                    this.MapFramingType = MapCartoTemplateLib.MapFramingType.StandardFraming;
                    this.MapFrameType = MapCartoTemplateLib.MapFrameType.MFTRect;
                }
                else if (num == 1)
                {
                    this.MapFramingType = MapCartoTemplateLib.MapFramingType.StandardFraming;
                    this.MapFrameType = MapCartoTemplateLib.MapFrameType.MFTTrapezoid;
                }
                else if (num == 2)
                {
                    this.MapFramingType = MapCartoTemplateLib.MapFramingType.AnyFraming;
                    this.MapFrameType = MapCartoTemplateLib.MapFrameType.MFTRect;
                }
                else
                {
                    this.MapFramingType = MapCartoTemplateLib.MapFramingType.AnyFraming;
                    this.MapFrameType = MapCartoTemplateLib.MapFrameType.MFTTrapezoid;
                }
                this.NewMapFrameTypeVal = this.MapFrameType;
                this.OutBorderWidth = Convert.ToDouble(RowAssisant.GetFieldValue(row, "OutBorderWidth"));
                this.BorderSymbol = this.method_40(RowAssisant.GetFieldValue(row, "BorderSymbol"), "Symbol") as ISymbol;
                this.GridSymbol = this.method_40(RowAssisant.GetFieldValue(row, "GridSymbol"), "Symbol") as ISymbol;
                this.LeftInOutSpace = Convert.ToDouble(RowAssisant.GetFieldValue(row, "LeftInOutSpace"));
                this.RightInOutSpace = Convert.ToDouble(RowAssisant.GetFieldValue(row, "RightInOutSpace"));
                this.BottomInOutSpace = Convert.ToDouble(RowAssisant.GetFieldValue(row, "BottomInOutSpace"));
                this.TopInOutSpace = Convert.ToDouble(RowAssisant.GetFieldValue(row, "TopInOutSpace"));
                this.AnnoUnit = (esriUnits) Convert.ToInt16(RowAssisant.GetFieldValue(row, "AnnoUnit"));
                this.AnnoUnitZoomScale = Convert.ToDouble(RowAssisant.GetFieldValue(row, "AnnoUnitZoomScale"));
                this.XInterval = Convert.ToDouble(RowAssisant.GetFieldValue(row, "XInterval"));
                this.YInterval = Convert.ToDouble(RowAssisant.GetFieldValue(row, "YInterval"));
                try
                {
                    this.IsAdapationScale = Convert.ToInt16(RowAssisant.GetFieldValue(row, "IsAdapationScale")) == 1;
                }
                catch
                {
                }
                this.TemplateSizeStyle = (TemplateSizeStyle) Convert.ToInt32(RowAssisant.GetFieldValue(row, "FixDataRange"));
                this.Description = RowAssisant.GetFieldValue(row, "Description").ToString();
                this.FontName = RowAssisant.GetFieldValue(row, "FontName").ToString();
                try
                {
                    this.FixedWidthAndBottomSpace = Convert.ToInt16(RowAssisant.GetFieldValue(row, "FixedWidthAndBottomSpace")) == 1;
                }
                catch
                {
                }
                this.MapGrid = this.method_40(RowAssisant.GetFieldValue(row, "MapGrid"), "MapGrid") as IMapGrid;
                this.DrawCornerShortLine = Convert.ToInt16(RowAssisant.GetFieldValue(row, "DrawCornerShortLine")) == 1;
                this.DrawJWD = Convert.ToInt16(RowAssisant.GetFieldValue(row, "DrawJWD")) == 1;
                this.DrawRoundLineShortLine = Convert.ToInt16(RowAssisant.GetFieldValue(row, "DrawRoundLineShortLine")) == 1;
                try
                {
                    this.DrawRoundText = Convert.ToInt16(RowAssisant.GetFieldValue(row, "DrawRoundText")) == 1;
                }
                catch
                {
                    this.DrawCornerText = false;
                }
                try
                {
                    this.DrawCornerText = Convert.ToInt16(RowAssisant.GetFieldValue(row, "DrawCornerText")) == 1;
                }
                catch
                {
                    this.DrawCornerText = false;
                }
                this.TemplateSizeStyle = (TemplateSizeStyle) Convert.ToInt32(RowAssisant.GetFieldValue(row, "FixDataRange"));
                this.SmallFontSize = Convert.ToSingle(RowAssisant.GetFieldValue(row, "SmallFontSize"));
                this.BigFontSize = Convert.ToSingle(RowAssisant.GetFieldValue(row, "BigFontSize"));
                this.method_11();
                this.method_12();
            }
        }

        public void LoadFromFile(string string_6)
        {
            int num3;
            System.IO.FileStream input = new System.IO.FileStream(string_6, FileMode.Open);
            BinaryReader reader = new BinaryReader(input);
            int count = reader.ReadInt32();
            byte[] buffer = reader.ReadBytes(count);
            IMemoryBlobStream stream2 = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = stream2
            };
            ((IMemoryBlobStreamVariant) stream2).ImportFromVariant(buffer);
            IPropertySet set = new PropertySetClass();
            (set as IPersistStream).Load(pstm);
            string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(string_6);
            this.Name = fileNameWithoutExtension;
            this.Width = Convert.ToDouble(set.GetProperty("Width"));
            this.Height = Convert.ToDouble(set.GetProperty("Height"));
            this.Scale = Convert.ToDouble(set.GetProperty("Scale"));
            this.MapFrameType = (MapFrameType) Convert.ToInt32(set.GetProperty("FrameType"));
            this.BorderSymbol = set.GetProperty("BorderSymbol") as ISymbol;
            this.GridSymbol = set.GetProperty("GridSymbol") as ISymbol;
            this.OutBorderWidth = Convert.ToDouble(set.GetProperty("OutBorderWidth"));
            this.LeftInOutSpace = Convert.ToDouble(set.GetProperty("LeftInOutSpace"));
            this.RightInOutSpace = Convert.ToDouble(set.GetProperty("RightInOutSpace"));
            this.BottomInOutSpace = Convert.ToDouble(set.GetProperty("BottomInOutSpace"));
            this.TopInOutSpace = Convert.ToDouble(set.GetProperty("TopInOutSpace"));
            this.MapGrid = set.GetProperty("MapGrid") as IMapGrid;
            this.AnnoUnit = (esriUnits) Convert.ToInt32(set.GetProperty("AnnoUnit"));
            this.AnnoUnitZoomScale = Convert.ToDouble(set.GetProperty("AnnoUnitZoomScale"));
            this.XInterval = Convert.ToDouble(set.GetProperty("XInterval"));
            this.YInterval = Convert.ToDouble(set.GetProperty("YInterval"));
            this.TemplateSizeStyle = (TemplateSizeStyle) Convert.ToInt32(set.GetProperty("FixDataRange"));
            this.Description = Convert.ToString(set.GetProperty("Description"));
            int num2 = Convert.ToInt32(set.GetProperty("DrawCornerShortLine"));
            this.DrawCornerShortLine = num2 == 1;
            num2 = Convert.ToInt32(set.GetProperty("DrawJWD"));
            this.DrawJWD = num2 == 1;
            num2 = Convert.ToInt32(set.GetProperty("DrawRoundLineShortLine"));
            this.DrawRoundLineShortLine = num2 == 1;
            num2 = Convert.ToInt32(set.GetProperty("DrawRoundText"));
            this.DrawRoundText = num2 == 1;
            num2 = Convert.ToInt32(set.GetProperty("DrawCornerText"));
            this.DrawCornerText = num2 == 1;
            num2 = Convert.ToInt32(set.GetProperty("FixedWidthAndBottomSpace"));
            this.FixedWidthAndBottomSpace = num2 == 1;
            num2 = Convert.ToInt32(set.GetProperty("IsAdapationScale"));
            this.IsAdapationScale = num2 == 1;
            this.FontName = Convert.ToString(set.GetProperty("FontName"));
            this.SmallFontSize = Convert.ToDouble(set.GetProperty("SmallFontSize"));
            this.BigFontSize = Convert.ToDouble(set.GetProperty("BigFontSize"));
            IPropertySetArray property = set.GetProperty("MapParams") as IPropertySetArray;
            if (property != null)
            {
                for (num3 = 0; num3 < property.Count; num3++)
                {
                    IPropertySet set2 = property.get_Element(num3);
                    MapCartoTemplateLib.MapTemplateParam param = new MapCartoTemplateLib.MapTemplateParam(-1, this);
                    param.Load(set2);
                    this.AddMapTemplateParam(param);
                }
            }
            property = set.GetProperty("MapElements") as IPropertySetArray;
            if (property != null)
            {
                for (num3 = 0; num3 < property.Count; num3++)
                {
                    MapCartoTemplateLib.MapTemplateElement element = MapCartoTemplateLib.MapTemplateElement.CreateMapTemplateElement(property.get_Element(num3), this);
                    if (element != null)
                    {
                        this.AddMapTemplateElement(element);
                    }
                }
            }
        }

        private void method_0(IActiveView iactiveView_0)
        {
            IGraphicsContainer graphicsContainer = iactiveView_0.GraphicsContainer;
            for (int i = 0; i < this.MapTemplateElement.Count; i++)
            {
                if (!this.IsTest)
                {
                    this.MapTemplateElement[i].Init();
                }
                IElement element = this.MapTemplateElement[i].GetElement(iactiveView_0 as IPageLayout);
                if (element is IGroupElement)
                {
                    for (int j = 0; j < (element as IGroupElement).ElementCount; j++)
                    {
                        IElement element2 = (element as IGroupElement).get_Element(j);
                        if (element2 is ITextElement)
                        {
                            string text = (element2 as ITextElement).Text;
                            if ((text.Length == 0) || (text[0] == '='))
                            {
                            }
                        }
                    }
                }
                if (element != null)
                {
                    try
                    {
                        graphicsContainer.AddElement(element, -1);
                        IEnvelope envelope = element.Geometry.Envelope;
                        this.double_2 = (this.double_2 < envelope.XMin) ? this.double_2 : envelope.XMin;
                        this.double_3 = (this.double_3 < envelope.YMin) ? this.double_3 : envelope.YMin;
                        this.double_4 = (this.double_4 > envelope.XMax) ? this.double_4 : envelope.XMax;
                        this.double_5 = (this.double_5 > envelope.YMax) ? this.double_5 : envelope.YMax;
                    }
                    catch
                    {
                    }
                }
            }
        }

        private IPoint method_1()
        {
            PointClass class2 = new PointClass {
                X = 0.0,
                Y = 0.0
            };
            IPoint point = class2;
            double num1 = (this.LeftInOutSpace + (this.OutBorderWidth / 2.0)) + 4.0;
            double num2 = (this.BottomInOutSpace + (this.OutBorderWidth / 2.0)) + 4.0;
            return point;
        }

        private void method_10(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(ipageLayout_0);
            IEnvelope bounds = new EnvelopeClass();
            IProjectedCoordinateSystem spatialReference = focusMapFrame.Map.SpatialReference as IProjectedCoordinateSystem;
            IPoint point = this.method_8(ipoint_0, spatialReference);
            IPoint point2 = this.method_8(ipoint_1, spatialReference);
            IPoint point3 = this.method_8(ipoint_3, spatialReference);
            IPoint point4 = this.method_8(ipoint_2, spatialReference);
            double num = (point.X < point2.X) ? point.X : point2.X;
            double num2 = (point.Y < point3.Y) ? point.Y : point3.Y;
            double num3 = (point3.X > point4.X) ? point3.X : point4.X;
            double num4 = (point4.Y > point2.Y) ? point4.Y : point2.Y;
            double num5 = ((double) ((int) (MapFrameAssistant.DEG2DDDMMSS(num) * 10000.0))) / 10000.0;
            double num6 = ((double) ((int) (MapFrameAssistant.DEG2DDDMMSS(num2) * 10000.0))) / 10000.0;
            double num7 = ((double) ((int) ((MapFrameAssistant.DEG2DDDMMSS(num3) * 10000.0) + 0.5))) / 10000.0;
            double num8 = ((double) ((int) ((MapFrameAssistant.DEG2DDDMMSS(num4) * 10000.0) + 0.5))) / 10000.0;
            ipoint_0.X = MapFrameAssistant.DDDMMSS2DEG(num5);
            ipoint_0.Y = MapFrameAssistant.DDDMMSS2DEG(num6);
            ipoint_1.X = MapFrameAssistant.DDDMMSS2DEG(num5);
            ipoint_1.Y = MapFrameAssistant.DDDMMSS2DEG(num8);
            ipoint_2.X = MapFrameAssistant.DDDMMSS2DEG(num7);
            ipoint_2.Y = MapFrameAssistant.DDDMMSS2DEG(num8);
            ipoint_3.X = MapFrameAssistant.DDDMMSS2DEG(num7);
            ipoint_3.Y = MapFrameAssistant.DDDMMSS2DEG(num6);
            List<IPoint> list = new List<IPoint> {
                this.method_9(ipoint_0, spatialReference),
                this.method_9(ipoint_1, spatialReference),
                this.method_9(ipoint_2, spatialReference),
                this.method_9(ipoint_3, spatialReference)
            };
            this.MapXInterval = (this.XInterval * this.Scale) / 100.0;
            this.MapYInterval = (this.YInterval * this.Scale) / 100.0;
            double xMin = (list[1].X < list[0].X) ? list[1].X : list[0].X;
            double xMax = (list[3].X > list[2].X) ? list[3].X : list[2].X;
            double yMin = (list[3].Y < list[0].Y) ? list[3].Y : list[0].Y;
            double yMax = (list[1].Y > list[2].Y) ? list[1].Y : list[2].Y;
            double num13 = ((xMax - xMin) / this.Scale) * 100.0;
            double num14 = ((yMax - yMin) / this.Scale) * 100.0;
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            IEnvelope to = new EnvelopeClass();
            double num15 = (this.LeftInOutSpace + (this.OutBorderWidth / 2.0)) + 4.0;
            double num16 = (this.BottomInOutSpace + (this.OutBorderWidth / 2.0)) + 4.0;
            to.PutCoords(num15, num16, num13 + num15, num14 + num16);
            IAffineTransformation2D transformationd = new AffineTransformation2DClass();
            transformationd.DefineFromEnvelopes(envelope, to);
            (focusMapFrame as ITransform2D).Transform(esriTransformDirection.esriTransformForward, transformationd);
            to = new EnvelopeClass();
            to.PutCoords(xMin, yMin, xMax, yMax);
            (focusMapFrame.Map as IActiveView).Extent = to;
            focusMapFrame.MapBounds = to;
            focusMapFrame.Map.MapScale = this.Scale;
            if (num13 < 0.0)
            {
                num13 = -num13;
            }
            double num17 = ((this.LeftInOutSpace + this.RightInOutSpace) + this.OutBorderWidth) + 8.0;
            double num18 = ((this.TopInOutSpace + this.BottomInOutSpace) + this.OutBorderWidth) + 8.0;
            ipageLayout_0.Page.PutCustomSize(num13 + num17, num14 + num18);
            if (this.BorderSymbol != null)
            {
                IElement element = this.method_14(focusMapFrame, this.BorderSymbol);
                (ipageLayout_0 as IGraphicsContainer).AddElement(element, -1);
            }
            MapCartoTemplateLib.YTTransformation transformation = new MapCartoTemplateLib.YTTransformation(ipageLayout_0 as IActiveView);
            IGroupElement element2 = this.method_28(ipageLayout_0, list[0], list[1], list[2], list[3]);
            IPointCollection points = new PolygonClass();
            object before = Missing.Value;
            object after = Missing.Value;
            points.AddPoint(list[3], ref before, ref after);
            object obj4 = Missing.Value;
            object obj5 = Missing.Value;
            points.AddPoint(list[2], ref obj4, ref obj5);
            object obj6 = Missing.Value;
            object obj7 = Missing.Value;
            points.AddPoint(list[1], ref obj6, ref obj7);
            object obj8 = Missing.Value;
            object obj9 = Missing.Value;
            points.AddPoint(list[0], ref obj8, ref obj9);
            (points as IPolygon).Close();
            (points as ITopologicalOperator).Simplify();
            (points as IGeometry).SpatialReference = focusMapFrame.Map.SpatialReference;
            (focusMapFrame.Map as IMapClipOptions).ClipType = esriMapClipType.esriMapClipShape;
            (focusMapFrame.Map as IMapClipOptions).ClipGeometry = points as IGeometry;
            if (this.DrawJWD)
            {
                IElement element3 = null;
                try
                {
                    element3 = this.method_41(ipageLayout_0 as IActiveView, transformation, list[0], list[1], list[2], list[3]);
                }
                catch (Exception)
                {
                }
                if (element3 != null)
                {
                    if (element3 is IGroupElement)
                    {
                        element3.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                        (element3 as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
                    }
                    element2.AddElement(element3);
                }
            }
            (element2 as IElement).QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            (ipageLayout_0 as IGraphicsContainer).AddElement(element2 as IElement, -1);
            (element2 as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
            (ipageLayout_0 as IGraphicsContainer).UpdateElement(element2 as IElement);
            this.method_0(ipageLayout_0 as IActiveView);
            focusMapFrame.Border = null;
            (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void method_11()
        {
            if (this.mapTemplateParam != null)
            {
                this.mapTemplateParam.Clear();
            }
            IQueryFilter queryFilter = new QueryFilterClass {
                WhereClause = "MapTemplateOID=" + this.OID
            };
            ICursor cursor = this.MapTemplateGallery.MapTemplateParamTable.Search(queryFilter, false);
            IRow o = cursor.NextRow();
            int index = this.MapTemplateGallery.MapTemplateParamTable.FindField("Name");
            while (o != null)
            {
                o.get_Value(index).ToString();
                MapCartoTemplateLib.MapTemplateParam param = new MapCartoTemplateLib.MapTemplateParam(o.OID, this);
                param.Load();
                this.AddMapTemplateParam(param);
                o = cursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(o);
        }

        private void method_12()
        {
            if (this.mapTemplateElelemt != null)
            {
                this.mapTemplateElelemt.Clear();
            }
            IQueryFilter queryFilter = new QueryFilterClass {
                WhereClause = "TemplateID=" + this.OID
            };
            ICursor cursor = this.MapTemplateGallery.MapTemplateElementTable.Search(queryFilter, false);
            IRow o = cursor.NextRow();
            int index = this.MapTemplateGallery.MapTemplateElementTable.FindField("ElementType");
            while (o != null)
            {
                o.get_Value(index).ToString();
                MapCartoTemplateLib.MapTemplateElement element = MapCartoTemplateLib.MapTemplateElement.CreateMapTemplateElement(o.OID, this);
                if (element != null)
                {
                    this.AddMapTemplateElement(element);
                }
                o = cursor.NextRow();
            }
            ComReleaser.ReleaseCOMObject(o);
        }

        private void method_13()
        {
            this.Guid = System.Guid.NewGuid().ToString();
            this.Width = 50.0;
            this.Height = 50.0;
            this.XInterval = 10.0;
            this.YInterval = 10.0;
            this.Scale = 500.0;
            this.LeftInOutSpace = 1.0;
            this.RightInOutSpace = 1.0;
            this.TopInOutSpace = 1.0;
            this.BottomInOutSpace = 1.0;
            this.AnnoUnit = esriUnits.esriKilometers;
            this.AnnoUnitZoomScale = 1.0;
            this.Description = "";
            this.DrawCornerShortLine = true;
            this.DrawJWD = false;
            this.DrawRoundLineShortLine = true;
            SimpleMarkerSymbolClass class2 = new SimpleMarkerSymbolClass {
                Style = esriSimpleMarkerStyle.esriSMSCross,
                Size = 28.0
            };
            this.GridSymbol = class2;
            this.LegendInfo = "";
            this.MapFrameType = MapCartoTemplateLib.MapFrameType.MFTRect;
            this.NewMapFrameTypeVal = MapCartoTemplateLib.MapFrameType.MFTRect;
            this.MapFramingType = MapCartoTemplateLib.MapFramingType.StandardFraming;
            this.MapGrid = null;
            this.Name = "模板";
            this.OutBorderWidth = 0.3;
            this.BorderSymbol = new CartographicLineSymbolClass();
            (this.BorderSymbol as ILineSymbol).Width = this.OutBorderWidth / 0.0352777778;
            this.MapXInterval = (this.XInterval * this.Scale) / 100.0;
            this.MapYInterval = (this.YInterval * this.Scale) / 100.0;
        }

        private IElement method_14(IMapFrame imapFrame_0, ISymbol isymbol_3)
        {
            IEnvelope envelope = (imapFrame_0 as IElement).Geometry.Envelope;
            IPoint lowerLeft = envelope.LowerLeft;
            IPoint upperRight = envelope.UpperRight;
            if (isymbol_3 is ILineSymbol)
            {
                return this.method_17(lowerLeft, upperRight, isymbol_3);
            }
            if (isymbol_3 is IFillSymbol)
            {
                return this.method_16(lowerLeft, upperRight, isymbol_3);
            }
            return null;
        }

        private IElement method_15(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, ISymbol isymbol_3)
        {
            if (isymbol_3 is ILineSymbol)
            {
                return this.method_18(ipoint_0, ipoint_1, ipoint_2, ipoint_3, isymbol_3 as ILineSymbol);
            }
            if (isymbol_3 is IFillSymbol)
            {
                return this.method_19(ipoint_0, ipoint_1, ipoint_2, ipoint_3, isymbol_3 as IFillSymbol);
            }
            return null;
        }

        private IElement method_16(IPoint ipoint_0, IPoint ipoint_1, ISymbol isymbol_3)
        {
            IElement element = new PolygonElementClass();
            IFillShapeElement element2 = element as IFillShapeElement;
            IGeometryCollection geometrys = new PolygonClass();
            IElementProperties2 properties = null;
            object missing = Type.Missing;
            IGeometry inGeometry = new RingClass();
            IGeometry geometry2 = new RingClass();
            IPointCollection points = inGeometry as IPointCollection;
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.LeftInOutSpace, ipoint_0.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.LeftInOutSpace, ipoint_1.Y + this.TopInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X + this.RightInOutSpace, ipoint_1.Y + this.TopInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X + this.RightInOutSpace, ipoint_0.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.LeftInOutSpace, ipoint_0.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(inGeometry, ref missing, ref missing);
                points = geometry2 as IPointCollection;
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - this.OutBorderWidth, (ipoint_0.Y - this.BottomInOutSpace) - this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - this.OutBorderWidth, (ipoint_1.Y + this.TopInOutSpace) + this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X + this.RightInOutSpace) + this.OutBorderWidth, (ipoint_1.Y + this.TopInOutSpace) + this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X + this.RightInOutSpace) + this.OutBorderWidth, (ipoint_0.Y - this.BottomInOutSpace) - this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - this.OutBorderWidth, (ipoint_0.Y - this.BottomInOutSpace) - this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(geometry2, ref missing, ref missing);
                element.Geometry = geometrys as IGeometry;
                element2.Symbol = isymbol_3 as IFillSymbol;
                properties = element as IElementProperties2;
                properties.Type = "外框";
            }
            catch
            {
            }
            return element;
        }

        private IElement method_17(IPoint ipoint_0, IPoint ipoint_1, ISymbol isymbol_3)
        {
            IElement element = new LineElementClass();
            ILineElement element2 = null;
            IElementProperties2 properties = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            double num = this.OutBorderWidth / 2.0;
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - num, (ipoint_0.Y - this.BottomInOutSpace) - num);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - num, (ipoint_1.Y + this.TopInOutSpace) + num);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X + this.RightInOutSpace) + num, (ipoint_1.Y + this.TopInOutSpace) + num);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X + this.RightInOutSpace) + num, (ipoint_0.Y - this.BottomInOutSpace) - num);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - num, (ipoint_0.Y - this.BottomInOutSpace) - num);
                points.AddPoint(inPoint, ref missing, ref missing);
                element.Geometry = polyline;
                properties = element as IElementProperties2;
                properties.Type = "外框";
                (isymbol_3 as ILineSymbol).Width = this.OutBorderWidth / 0.0352777778;
                element2 = element as ILineElement;
                element2.Symbol = isymbol_3 as ILineSymbol;
            }
            catch
            {
            }
            return element;
        }

        private IElement method_18(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, ILineSymbol ilineSymbol_0)
        {
            IElement element = new LineElementClass();
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IElementProperties2 properties = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            ilineSymbol_0.Width = this.OutBorderWidth / 0.0352777778;
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - (this.OutBorderWidth / 2.0), (ipoint_0.Y - this.BottomInOutSpace) - (this.OutBorderWidth / 2.0));
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X - this.LeftInOutSpace) - (this.OutBorderWidth / 2.0), (ipoint_1.Y + this.TopInOutSpace) + (this.OutBorderWidth / 2.0));
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_2.X + this.RightInOutSpace) + (this.OutBorderWidth / 2.0), (ipoint_2.Y + this.TopInOutSpace) + (this.OutBorderWidth / 2.0));
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_3.X + this.RightInOutSpace) + (this.OutBorderWidth / 2.0), (ipoint_3.Y - this.BottomInOutSpace) - (this.OutBorderWidth / 2.0));
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - (this.OutBorderWidth / 2.0), (ipoint_0.Y - this.BottomInOutSpace) - (this.OutBorderWidth / 2.0));
                points.AddPoint(inPoint, ref missing, ref missing);
                element.Geometry = polyline;
                properties = element as IElementProperties2;
                properties.Type = "外框";
                symbol = ilineSymbol_0;
                element2 = element as ILineElement;
                element2.Symbol = symbol;
            }
            catch (Exception)
            {
            }
            return element;
        }

        private IElement method_19(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, IFillSymbol ifillSymbol_0)
        {
            IElement element = new PolygonElementClass();
            IFillShapeElement element2 = element as IFillShapeElement;
            IGeometryCollection geometrys = new PolygonClass();
            IElementProperties2 properties = null;
            object missing = Type.Missing;
            IGeometry inGeometry = new RingClass();
            IGeometry geometry2 = new RingClass();
            IPointCollection points = inGeometry as IPointCollection;
            try
            {
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.LeftInOutSpace, ipoint_0.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X - this.LeftInOutSpace, ipoint_1.Y + this.TopInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_2.X + this.RightInOutSpace, ipoint_2.Y + this.TopInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_3.X + this.RightInOutSpace, ipoint_3.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.LeftInOutSpace, ipoint_0.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(inGeometry, ref missing, ref missing);
                points = geometry2 as IPointCollection;
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - this.OutBorderWidth, (ipoint_0.Y - this.BottomInOutSpace) - this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_1.X - this.LeftInOutSpace) - this.OutBorderWidth, (ipoint_1.Y + this.TopInOutSpace) + this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_2.X + this.RightInOutSpace) + this.OutBorderWidth, (ipoint_2.Y + this.TopInOutSpace) + this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_3.X + this.RightInOutSpace) + this.OutBorderWidth, (ipoint_3.Y - this.BottomInOutSpace) - this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords((ipoint_0.X - this.LeftInOutSpace) - this.OutBorderWidth, (ipoint_0.Y - this.BottomInOutSpace) - this.OutBorderWidth);
                points.AddPoint(inPoint, ref missing, ref missing);
                geometrys.AddGeometry(geometry2, ref missing, ref missing);
                element.Geometry = geometrys as IGeometry;
                element2.Symbol = ifillSymbol_0;
                properties = element as IElementProperties2;
                properties.Type = "外框";
            }
            catch (Exception)
            {
            }
            return element;
        }

        private void method_2(IPageLayout ipageLayout_0, double double_22, double double_23)
        {
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(ipageLayout_0);
            (focusMapFrame as IMapGrids).ClearMapGrids();
            if (this.MapGrid != null)
            {
                (this.MapGrid as IMeasuredGrid).XIntervalSize = (this.XInterval * this.double_6) / 100.0;
                (this.MapGrid as IMeasuredGrid).YIntervalSize = (this.YInterval * this.double_6) / 100.0;
                (focusMapFrame as IMapGrids).AddMapGrid(this.MapGrid);
                if (this.BorderSymbol != null)
                {
                    IElement element = this.method_14(focusMapFrame, this.BorderSymbol);
                    (ipageLayout_0 as IGraphicsContainer).AddElement(element, -1);
                }
            }
            else
            {
                double num;
                double num2;
                IPoint point = new PointClass();
                point.PutCoords(double_22, double_23);
                IPoint point2 = new PointClass();
                if (this.double_6 == 0.0)
                {
                    num = this.Width / 100.0;
                    num2 = this.Height / 100.0;
                }
                else
                {
                    num = (this.Width * this.double_6) / 100.0;
                    num2 = (this.Height * this.double_6) / 100.0;
                }
                point2.PutCoords(double_22 + num, double_23 + num2);
                this.method_3(ipageLayout_0, point, point2);
            }
        }

        private IPointCollection method_20(IPolyline ipolyline_0, IPolygon ipolygon_0)
        {
            IMultipoint multipoint = null;
            ITopologicalOperator @operator = ipolygon_0 as ITopologicalOperator;
            @operator.Simplify();
            multipoint = @operator.Intersect(ipolyline_0, esriGeometryDimension.esriGeometry0Dimension) as IMultipoint;
            return (multipoint as IPointCollection);
        }

        private ISymbol method_21()
        {
            return this.BorderSymbol;
        }

        private void method_22(double double_22, double double_23, out string string_6, out string string_7)
        {
            string_6 = "";
            string_7 = "";
            int num = (int) Math.Truncate((double) (double_22 / double_23));
            if (num != 0)
            {
                string_6 = num.ToString();
            }
            int num2 = (int) (double_23 / 1000.0);
            string_7 = ((int) Math.Truncate((double) (((double_22 - (num * double_23)) / double_23) * num2))).ToString();
            if ((string_7.Length < 2) && (num != 0))
            {
                string_7 = "0" + string_7;
            }
        }

        private void method_23(double double_22, double double_23, out string string_6, out string string_7)
        {
            string_6 = "";
            string_7 = "";
            int num = (int) Math.Truncate((double) (double_22 / 100000.0));
            if (num != 0)
            {
                string_6 = num.ToString();
            }
            int num2 = (int) Math.Truncate((double) (double_22 - (num * 100000.0)));
            int num3 = (int) (Math.Truncate((double) (((double) num2) / double_23)) * double_23);
            double num4 = ((double) num3) / 1000.0;
            if (double_23 < 100.0)
            {
                string_7 = num4.ToString("0.00");
                if ((string_7.Length < 5) && (num != 0))
                {
                    string_7 = "0" + string_7;
                }
            }
            else if (double_23 < 1000.0)
            {
                string_7 = num4.ToString("0.0");
                if ((string_7.Length < 4) && (num != 0))
                {
                    string_7 = "0" + string_7;
                }
            }
            else
            {
                string_7 = num4.ToString("0");
                if ((string_7.Length < 2) && (num != 0))
                {
                    string_7 = "0" + string_7;
                }
            }
        }

        private void method_24(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, out double double_22, out double double_23, out double double_24, out double double_25, out double double_26, out double double_27, out double double_28, out double double_29)
        {
            double_22 = (ipoint_0.X > ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double_23 = (ipoint_0.Y > ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double_24 = (ipoint_2.X < ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double_25 = (ipoint_1.Y < ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            double_26 = (ipoint_0.X < ipoint_1.X) ? (ipoint_0.X - 3.0) : (ipoint_1.X - 3.0);
            double_28 = (ipoint_0.Y < ipoint_3.Y) ? (ipoint_0.Y - 3.0) : (ipoint_3.Y - 3.0);
            double_27 = (ipoint_2.X > ipoint_3.X) ? (ipoint_2.X + 3.0) : (ipoint_3.X + 3.0);
            double_29 = (ipoint_1.Y > ipoint_2.Y) ? (ipoint_1.Y + 3.0) : (ipoint_2.Y + 3.0);
        }

        private ILineSymbol method_25()
        {
            CartographicLineSymbolClass class2 = new CartographicLineSymbolClass {
                Cap = esriLineCapStyle.esriLCSSquare
            };
            RgbColorClass class3 = new RgbColorClass {
                Red = 0,
                Blue = 0,
                Green = 0
            };
            class2.Color = class3;
            class2.Join = esriLineJoinStyle.esriLJSMitre;
            class2.Width = 1.0;
            return class2;
        }

        private ITextElement method_26(double double_22, double double_23, string string_6, ITextSymbol itextSymbol_0)
        {
            PointClass class2 = new PointClass {
                X = double_22,
                Y = double_23
            };
            IPoint point = class2;
            return new TextElementClass { Geometry = point, Text = string_6, Symbol = itextSymbol_0 };
        }

        private IElement method_27(MapCartoTemplateLib.YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            double num;
            double num2;
            double num3;
            double num4;
            double num5;
            double num6;
            double num7;
            double num8;
            IPolyline polyline;
            IPointCollection points;
            IPoint point3;
            IPoint point4;
            IPointCollection points2;
            IPolygon polygon = this.method_36(jlktransformation_0.ToPageLayoutPoint(ipoint_0), jlktransformation_0.ToPageLayoutPoint(ipoint_1), jlktransformation_0.ToPageLayoutPoint(ipoint_2), jlktransformation_0.ToPageLayoutPoint(ipoint_3), true);
            IGroupElement element = new GroupElementClass();
            this.method_24(ipoint_0, ipoint_1, ipoint_2, ipoint_3, out num, out num2, out num3, out num4, out num5, out num6, out num7, out num8);
            IPoint point = this.method_32(num, num2);
            IPoint point2 = this.method_32(num3, num4);
            double x = point.X;
            double y = point.Y;
            double num11 = point2.X;
            double num12 = point2.Y;
            double num13 = Math.Abs((double) (x - ipoint_0.X)) / this.Scale;
            if (num13 < 0.02)
            {
                x += this.MapXInterval;
            }
            if (num11 > ipoint_3.X)
            {
                num11 -= this.MapXInterval;
            }
            num13 = Math.Abs((double) (num11 - ipoint_3.X)) / this.Scale;
            if (num13 < 0.02)
            {
                num11 -= this.MapXInterval;
                num6 -= this.MapXInterval;
            }
            num11 += this.MapXInterval / 2.0;
            num13 = Math.Abs((double) (y - ipoint_0.Y)) / this.Scale;
            if (num13 < 0.02)
            {
                y += this.MapYInterval;
            }
            if (num12 > ipoint_2.Y)
            {
                num12 -= this.MapXInterval;
            }
            num13 = Math.Abs((double) (num12 - ipoint_2.Y)) / this.Scale;
            if (num13 < 0.02)
            {
                num12 -= this.MapYInterval;
                num8 -= this.MapYInterval;
            }
            num12 += this.MapYInterval / 2.0;
            point.X = x;
            point.Y = y;
            num3 = num11;
            num4 = num12;
            string str = "";
            string str2 = "";
            double num14 = 0.4;
            double num15 = 0.2;
            int smallFontSize = (int) this.SmallFontSize;
            int bigFontSize = (int) this.BigFontSize;
            ITextSymbol symbol = this.FontStyle((double) smallFontSize, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol2 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol3 = this.FontStyle((double) smallFontSize, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol4 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol5 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol6 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol7 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol8 = this.FontStyle((double) smallFontSize, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            object missing = Type.Missing;
            ILineSymbol symbol9 = this.method_25();
            bool flag = true;
            try
            {
                while (x <= num3)
                {
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    PointClass class2 = new PointClass {
                        X = x,
                        Y = num7
                    };
                    point3 = class2;
                    point3 = jlktransformation_0.ToPageLayoutPoint(point3);
                    PointClass class3 = new PointClass {
                        X = point3.X,
                        Y = ((point3.Y - this.BottomInOutSpace) - this.OutBorderWidth) - 10.0
                    };
                    point4 = class3;
                    points.AddPoint(point4, ref missing, ref missing);
                    PointClass class4 = new PointClass {
                        X = x,
                        Y = num8
                    };
                    point3 = class4;
                    point3 = jlktransformation_0.ToPageLayoutPoint(point3);
                    PointClass class5 = new PointClass {
                        X = point3.X,
                        Y = ((point3.Y + this.TopInOutSpace) + this.OutBorderWidth) + 10.0
                    };
                    point4 = class5;
                    points.AddPoint(point4, ref missing, ref missing);
                    points2 = this.method_20(polyline, polygon);
                    points.RemovePoints(0, points.PointCount);
                    if (points2.PointCount < 4)
                    {
                        x += this.MapXInterval;
                    }
                    else
                    {
                        points.AddPoint(points2.get_Point(2), ref missing, ref missing);
                        points.AddPoint(points2.get_Point(3), ref missing, ref missing);
                        point3 = points2.get_Point(2);
                        point4 = points2.get_Point(3);
                        point3.Y = (point3.Y > point4.Y) ? point3.Y : point4.Y;
                        if (this.DrawRoundLineShortLine)
                        {
                            LineElementClass class6 = new LineElementClass {
                                Symbol = symbol9,
                                Geometry = polyline
                            };
                            element.AddElement(class6);
                        }
                        this.method_23(x, this.MapXInterval, out str, out str2);
                        if (this.DrawRoundText)
                        {
                            element.AddElement(this.method_26(point3.X, point3.Y, str2, symbol3) as IElement);
                            if (flag || ((x + this.MapXInterval) > num6))
                            {
                                element.AddElement(this.method_26(point3.X, point3.Y, str, symbol6) as IElement);
                            }
                        }
                        polyline = new PolylineClass();
                        points = polyline as IPointCollection;
                        points.AddPoint(points2.get_Point(0), ref missing, ref missing);
                        points.AddPoint(points2.get_Point(1), ref missing, ref missing);
                        point3 = points2.get_Point(0);
                        point4 = points2.get_Point(1);
                        point3.Y = (point3.Y < point4.Y) ? point3.Y : point4.Y;
                        if (this.DrawRoundLineShortLine)
                        {
                            LineElementClass class7 = new LineElementClass {
                                Symbol = symbol9,
                                Geometry = polyline
                            };
                            element.AddElement(class7);
                        }
                        if (this.DrawRoundText)
                        {
                            element.AddElement(this.method_26(point3.X, point3.Y, str2, symbol8) as IElement);
                        }
                        if ((flag || ((x + this.MapXInterval) > num6)) && this.DrawRoundText)
                        {
                            element.AddElement(this.method_26(point3.X, point3.Y, str, symbol7) as IElement);
                        }
                        x += this.MapXInterval;
                        flag = false;
                    }
                }
            }
            catch
            {
            }
            flag = true;
            while (y <= num4)
            {
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                PointClass class8 = new PointClass {
                    X = num5,
                    Y = y
                };
                point3 = class8;
                point3 = jlktransformation_0.ToPageLayoutPoint(point3);
                PointClass class9 = new PointClass {
                    X = (point3.X - this.LeftInOutSpace) - 10.0,
                    Y = point3.Y
                };
                point4 = class9;
                points.AddPoint(point4, ref missing, ref missing);
                PointClass class10 = new PointClass {
                    X = num6,
                    Y = y
                };
                point3 = class10;
                point3 = jlktransformation_0.ToPageLayoutPoint(point3);
                PointClass class11 = new PointClass {
                    X = (point3.X + this.RightInOutSpace) + 10.0,
                    Y = point3.Y
                };
                point4 = class11;
                points.AddPoint(point4, ref missing, ref missing);
                points2 = this.method_20(polyline, polygon);
                if (points2.PointCount < 4)
                {
                    y += this.MapYInterval;
                }
                else
                {
                    double num18;
                    double num19;
                    double num20;
                    double num21;
                    points.RemovePoints(0, points.PointCount);
                    points.AddPoint(points2.get_Point(2), ref missing, ref missing);
                    points.AddPoint(points2.get_Point(3), ref missing, ref missing);
                    point3 = points2.get_Point(2);
                    point4 = points2.get_Point(3);
                    point3.X = (point3.X > point4.X) ? point3.X : point4.X;
                    if (this.DrawRoundLineShortLine)
                    {
                        LineElementClass class12 = new LineElementClass {
                            Symbol = symbol9,
                            Geometry = polyline
                        };
                        element.AddElement(class12);
                    }
                    this.method_23(y, this.MapYInterval, out str, out str2);
                    PointClass class13 = new PointClass {
                        X = point3.X,
                        Y = point3.Y + num15
                    };
                    IPoint point5 = class13;
                    TextElementClass class14 = new TextElementClass {
                        Geometry = point5,
                        Symbol = symbol2,
                        Text = str
                    };
                    ITextElement element2 = class14;
                    jlktransformation_0.TextWidth(element2, out num18, out num19);
                    TextElementClass class15 = new TextElementClass {
                        Geometry = point5,
                        Symbol = symbol,
                        Text = str2
                    };
                    element2 = class15;
                    jlktransformation_0.TextWidth(element2, out num20, out num21);
                    if (this.DrawRoundText)
                    {
                        element.AddElement(element2 as IElement);
                        if (flag || ((y + this.YInterval) > num8))
                        {
                            element.AddElement(this.method_26(point3.X - num20, point3.Y + num14, str, symbol4) as IElement);
                        }
                    }
                    polyline = new PolylineClass();
                    points = polyline as IPointCollection;
                    points.AddPoint(points2.get_Point(0), ref missing, ref missing);
                    points.AddPoint(points2.get_Point(1), ref missing, ref missing);
                    point3 = points2.get_Point(0);
                    point4 = points2.get_Point(1);
                    point3.X = (point3.X < point4.X) ? point3.X : point4.X;
                    if (this.DrawRoundLineShortLine)
                    {
                        LineElementClass class16 = new LineElementClass {
                            Symbol = symbol9,
                            Geometry = polyline
                        };
                        element.AddElement(class16);
                    }
                    if (this.DrawRoundText)
                    {
                        element.AddElement(this.method_26(point3.X + num18, point3.Y + num15, str2, symbol8) as IElement);
                    }
                    if ((flag || ((y + this.YInterval) > num8)) && this.DrawRoundText)
                    {
                        element.AddElement(this.method_26(point3.X, point3.Y + num14, str, symbol5) as IElement);
                    }
                    flag = false;
                    y += this.MapYInterval;
                }
            }
            return (element as IElement);
        }

        private IGroupElement method_28(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            int num;
            MapCartoTemplateLib.YTTransformation transformation = new MapCartoTemplateLib.YTTransformation(ipageLayout_0 as IActiveView);
            IPoint point = transformation.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = transformation.ToPageLayoutPoint(ipoint_1);
            IPoint point3 = transformation.ToPageLayoutPoint(ipoint_2);
            IPoint point4 = transformation.ToPageLayoutPoint(ipoint_3);
            IGroupElement element = new GroupElementClass();
            IElement element2 = null;
            if (this.GridSymbol is ILineSymbol)
            {
                element2 = this.method_38(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            }
            else if (this.GridSymbol is IMarkerSymbol)
            {
                element2 = this.method_34(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            }
            new EnvelopeClass();
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.method_27(transformation, ipoint_0, ipoint_1, ipoint_2, ipoint_3);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            if (this.DrawCornerShortLine)
            {
                element2 = this.CreateCornerShortLine(point, point2, point3, point4);
            }
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                }
                else
                {
                    element.AddElement(element2);
                }
            }
            element2 = this.method_30(point, point2, point3, point4);
            if (element2 != null)
            {
                if (element2 is IGroupElement)
                {
                    for (num = 0; num < (element2 as IGroupElement).ElementCount; num++)
                    {
                        element.AddElement((element2 as IGroupElement).get_Element(num));
                    }
                    return element;
                }
                element.AddElement(element2);
            }
            return element;
        }

        private IElement method_29(double double_22, double double_23, double double_24, double double_25, ILineSymbol ilineSymbol_0)
        {
            object missing = Type.Missing;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            PointClass class2 = new PointClass {
                X = double_22 + double_24,
                Y = double_23
            };
            IPoint inPoint = class2;
            points.AddPoint(inPoint, ref missing, ref missing);
            PointClass class3 = new PointClass {
                X = double_22,
                Y = double_23
            };
            inPoint = class3;
            points.AddPoint(inPoint, ref missing, ref missing);
            PointClass class4 = new PointClass {
                X = double_22,
                Y = double_23 + double_25
            };
            inPoint = class4;
            points.AddPoint(inPoint, ref missing, ref missing);
            return new LineElementClass { Symbol = ilineSymbol_0, Geometry = polyline };
        }

        private void method_3(IPageLayout ipageLayout_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            this.MapXInterval = (this.XInterval * this.double_6) / 100.0;
            this.MapYInterval = (this.YInterval * this.double_6) / 100.0;
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(ipageLayout_0);
            new EnvelopeClass();
            IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
            MapCartoTemplateLib.YTTransformation transformation = new MapCartoTemplateLib.YTTransformation(ipageLayout_0 as IActiveView);
            IPoint point = transformation.ToPageLayoutPoint(ipoint_0);
            IPoint point2 = transformation.ToPageLayoutPoint(ipoint_1);
            IGroupElement element = new GroupElementClass();
            IElement element2 = null;
            if (this.GridSymbol is ILineSymbol)
            {
                element2 = this.method_35(transformation, ipoint_0, ipoint_1);
            }
            else if (this.GridSymbol is IMarkerSymbol)
            {
                element2 = this.method_33(transformation, ipoint_0, ipoint_1);
            }
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            element2 = this.method_14(focusMapFrame, this.method_21());
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            try
            {
                if (this.DrawRoundLineShortLine)
                {
                    element2 = this.method_44(transformation, ipoint_0, ipoint_1);
                    if (element2 != null)
                    {
                        element.AddElement(element2);
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (this.DrawCornerShortLine)
                {
                    element2 = this.CreateCornerShortLine(transformation, ipoint_0, ipoint_1);
                    if (element2 != null)
                    {
                        element.AddElement(element2);
                    }
                }
            }
            catch (Exception)
            {
            }
            if (this.DrawJWD)
            {
                PointClass class2 = new PointClass {
                    X = ipoint_0.X,
                    Y = ipoint_1.Y,
                    SpatialReference = ipoint_0.SpatialReference
                };
                IPoint point3 = class2;
                PointClass class3 = new PointClass {
                    X = ipoint_1.X,
                    Y = ipoint_0.Y,
                    SpatialReference = ipoint_0.SpatialReference
                };
                IPoint point4 = class3;
                element2 = this.method_41(ipageLayout_0 as IActiveView, transformation, ipoint_0, point3, ipoint_1, point4);
                if (element2 != null)
                {
                    if (element2 is IGroupElement)
                    {
                        IEnvelope envelope = new EnvelopeClass();
                        element2.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, envelope);
                        (element2 as ITransform2D).Scale(envelope.UpperLeft, 1.0, 1.0);
                    }
                    element.AddElement(element2);
                }
            }
            element2 = this.method_31(point, point2);
            if (element2 != null)
            {
                element.AddElement(element2);
            }
            IEnvelope bounds = new EnvelopeClass();
            (element as IElement).QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            (ipageLayout_0 as IGraphicsContainer).AddElement(element as IElement, -1);
            (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IElement method_30(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            IElement element = new LineElementClass();
            IElementProperties2 properties = null;
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            points.AddPoint(ipoint_0, ref missing, ref missing);
            points.AddPoint(ipoint_1, ref missing, ref missing);
            points.AddPoint(ipoint_2, ref missing, ref missing);
            points.AddPoint(ipoint_3, ref missing, ref missing);
            points.AddPoint(ipoint_0, ref missing, ref missing);
            element.Geometry = polyline;
            properties = element as IElementProperties2;
            properties.Type = "内框";
            symbol = this.method_25();
            element2 = element as ILineElement;
            element2.Symbol = symbol;
            return element;
        }

        private IElement method_31(IPoint ipoint_0, IPoint ipoint_1)
        {
            IElement element = new LineElementClass();
            IElementProperties2 properties = null;
            ILineElement element2 = null;
            ILineSymbol symbol = null;
            IPolyline polyline = new PolylineClass();
            IPointCollection points = polyline as IPointCollection;
            object missing = Type.Missing;
            points.AddPoint(ipoint_0, ref missing, ref missing);
            IPoint inPoint = new PointClass();
            inPoint.PutCoords(ipoint_0.X, ipoint_1.Y);
            points.AddPoint(inPoint, ref missing, ref missing);
            points.AddPoint(ipoint_1, ref missing, ref missing);
            inPoint = new PointClass();
            inPoint.PutCoords(ipoint_1.X, ipoint_0.Y);
            points.AddPoint(inPoint, ref missing, ref missing);
            points.AddPoint(ipoint_0, ref missing, ref missing);
            element.Geometry = polyline;
            properties = element as IElementProperties2;
            properties.Type = "内框";
            symbol = this.method_25();
            element2 = element as ILineElement;
            element2.Symbol = symbol;
            return element;
        }

        private IPoint method_32(double double_22, double double_23)
        {
            double x = double_22;
            double y = double_23;
            double num3 = (this.XInterval * this.double_6) / 100.0;
            x = Math.Truncate((double) (double_22 / num3)) * num3;
            if (x <= double_22)
            {
                x += num3;
            }
            if (Math.Abs((double) (x - double_22)) < 1E-07)
            {
                x += num3;
            }
            double num4 = (this.YInterval * this.double_6) / 100.0;
            y = Math.Truncate((double) (double_23 / num4)) * num4;
            if (y <= double_23)
            {
                y += num4;
            }
            if (Math.Abs((double) (y - double_23)) < 1E-07)
            {
                y += num4;
            }
            IPoint point = new PointClass();
            point.PutCoords(x, y);
            return point;
        }

        private IElement method_33(MapCartoTemplateLib.YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            PointClass class2 = new PointClass {
                X = ipoint_0.X,
                Y = ipoint_1.Y
            };
            IPoint point = class2;
            PointClass class3 = new PointClass {
                X = ipoint_1.X,
                Y = ipoint_0.Y
            };
            IPoint point2 = class3;
            return this.method_34(jlktransformation_0, ipoint_0, point, ipoint_1, point2);
        }

        private IElement method_34(MapCartoTemplateLib.YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            double num9;
            IMarkerSymbol gridSymbol;
            IPoint point3;
            IPolyline polyline;
            IPointCollection points;
            IPoint point4;
            IPoint point5;
            double num = (ipoint_0.X > ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double num2 = (ipoint_0.Y > ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double num3 = (ipoint_2.X < ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double num4 = (ipoint_1.Y < ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_32(num, num2);
            IPoint point2 = this.method_32(num3, num4);
            double x = point2.X;
            double y = point2.Y;
            double num7 = point.X;
            double num8 = point.Y;
            if (this.DrawJWD)
            {
                num9 = Math.Abs((double) (num7 - ipoint_0.X)) / this.Scale;
                if (num9 < 0.02)
                {
                    num7 += this.MapXInterval;
                }
                if (x > ipoint_3.X)
                {
                    x -= this.MapXInterval;
                }
                num9 = Math.Abs((double) (x - ipoint_3.X)) / this.Scale;
                if (num9 < 0.02)
                {
                    x -= this.MapXInterval;
                }
                x += this.MapXInterval / 2.0;
                num9 = Math.Abs((double) (num8 - ipoint_0.Y)) / this.Scale;
                if (num9 < 0.02)
                {
                    num8 += this.MapYInterval;
                }
                if (y > ipoint_2.Y)
                {
                    y -= this.MapYInterval;
                }
                num9 = Math.Abs((double) (y - ipoint_2.Y)) / this.Scale;
                if (num9 < 0.02)
                {
                    y -= this.MapYInterval;
                }
                y += this.MapYInterval / 2.0;
            }
            else
            {
                num9 = Math.Abs((double) (num7 - ipoint_0.X)) / this.Scale;
                if (num9 < 0.01)
                {
                    num7 += this.MapXInterval;
                }
                if (x > ipoint_3.X)
                {
                    x -= this.MapXInterval;
                }
                num9 = Math.Abs((double) (x - ipoint_3.X)) / this.Scale;
                if (num9 < 0.01)
                {
                    x -= this.MapXInterval;
                }
                x += this.MapXInterval / 2.0;
                num9 = Math.Abs((double) (num8 - ipoint_0.Y)) / this.Scale;
                if (num9 < 0.01)
                {
                    num8 += this.MapYInterval;
                }
                if (y > ipoint_2.Y)
                {
                    y -= this.MapYInterval;
                }
                num9 = Math.Abs((double) (y - ipoint_2.Y)) / this.Scale;
                if (num9 < 0.01)
                {
                    y -= this.MapYInterval;
                }
                y += this.MapYInterval / 2.0;
            }
            point.X = num7;
            point.Y = num8;
            if (this.GridSymbol is IMarkerSymbol)
            {
                gridSymbol = this.GridSymbol as IMarkerSymbol;
            }
            else
            {
                gridSymbol = new SimpleMarkerSymbolClass {
                    Size = 10.0
                };
                (gridSymbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCross;
                gridSymbol.Color = ColorManage.CreatColor(0, 0, 0);
            }
            ILineSymbol symbol2 = this.method_25();
            double num10 = ipoint_3.Y - ipoint_0.Y;
            double num11 = ipoint_2.Y - ipoint_1.Y;
            double num12 = ipoint_3.X - ipoint_0.X;
            double num13 = ipoint_2.X - ipoint_1.X;
            while (num7 < x)
            {
                point3 = new PointClass();
                double num14 = ipoint_0.Y + (((num7 - ipoint_0.X) / num12) * num10);
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                PointClass class2 = new PointClass {
                    X = num7,
                    Y = num14
                };
                point4 = class2;
                point4 = jlktransformation_0.ToPageLayoutPoint(point4);
                object before = Missing.Value;
                object after = Missing.Value;
                points.AddPoint(point4, ref before, ref after);
                PointClass class3 = new PointClass {
                    X = point4.X,
                    Y = point4.Y + 0.5
                };
                point5 = class3;
                object obj4 = Missing.Value;
                object obj5 = Missing.Value;
                points.AddPoint(point5, ref obj4, ref obj5);
                LineElementClass class4 = new LineElementClass {
                    Symbol = symbol2,
                    Geometry = polyline
                };
                element.AddElement(class4);
                double num15 = ipoint_1.Y + (((num7 - ipoint_1.X) / num13) * num11);
                polyline = new PolylineClass();
                point3 = new PointClass();
                points = polyline as IPointCollection;
                PointClass class5 = new PointClass {
                    X = num7,
                    Y = num15
                };
                point4 = class5;
                point4 = jlktransformation_0.ToPageLayoutPoint(point4);
                object obj6 = Missing.Value;
                object obj7 = Missing.Value;
                points.AddPoint(point4, ref obj6, ref obj7);
                PointClass class6 = new PointClass {
                    X = point4.X,
                    Y = point4.Y - 0.5
                };
                point5 = class6;
                object obj8 = Missing.Value;
                object obj9 = Missing.Value;
                points.AddPoint(point5, ref obj8, ref obj9);
                LineElementClass class7 = new LineElementClass {
                    Symbol = symbol2,
                    Geometry = polyline
                };
                element.AddElement(class7);
                num7 += this.MapXInterval;
            }
            num10 = ipoint_1.X - ipoint_0.X;
            num11 = ipoint_2.X - ipoint_3.X;
            num12 = ipoint_1.Y - ipoint_0.Y;
            num13 = ipoint_2.Y - ipoint_3.Y;
            while (num8 < y)
            {
                point3 = new PointClass();
                double num16 = ipoint_0.X + (((num8 - ipoint_0.Y) / num12) * num10);
                polyline = new PolylineClass();
                points = polyline as IPointCollection;
                PointClass class8 = new PointClass {
                    X = num16,
                    Y = num8
                };
                point4 = class8;
                point4 = jlktransformation_0.ToPageLayoutPoint(point4);
                object obj10 = Missing.Value;
                object obj11 = Missing.Value;
                points.AddPoint(point4, ref obj10, ref obj11);
                PointClass class9 = new PointClass {
                    X = point4.X + 0.5,
                    Y = point4.Y
                };
                point5 = class9;
                object obj12 = Missing.Value;
                object obj13 = Missing.Value;
                points.AddPoint(point5, ref obj12, ref obj13);
                LineElementClass class10 = new LineElementClass {
                    Symbol = symbol2,
                    Geometry = polyline
                };
                element.AddElement(class10);
                double num17 = ipoint_3.X + (((num8 - ipoint_2.Y) / num13) * num11);
                polyline = new PolylineClass();
                point3 = new PointClass();
                points = polyline as IPointCollection;
                PointClass class11 = new PointClass {
                    X = num17,
                    Y = num8
                };
                point4 = class11;
                point4 = jlktransformation_0.ToPageLayoutPoint(point4);
                object obj14 = Missing.Value;
                object obj15 = Missing.Value;
                points.AddPoint(point4, ref obj14, ref obj15);
                PointClass class12 = new PointClass {
                    X = point4.X - 0.5,
                    Y = point4.Y
                };
                point5 = class12;
                object obj16 = Missing.Value;
                object obj17 = Missing.Value;
                points.AddPoint(point5, ref obj16, ref obj17);
                LineElementClass class13 = new LineElementClass {
                    Symbol = symbol2,
                    Geometry = polyline
                };
                element.AddElement(class13);
                num8 += this.MapYInterval;
            }
            num7 = point.X;
            for (num8 = point.Y; num7 < x; num8 = point.Y)
            {
                while (num8 < y)
                {
                    point3 = new PointClass();
                    point3.PutCoords(num7, num8);
                    IElement element2 = new MarkerElementClass {
                        Geometry = jlktransformation_0.ToPageLayoutPoint(point3)
                    };
                    (element2 as IMarkerElement).Symbol = gridSymbol;
                    element.AddElement(element2);
                    num8 += this.MapYInterval;
                }
                num7 += this.MapXInterval;
            }
            (element as IElementProperties2).Type = "公里网";
            return (element as IElement);
        }

        private IElement method_35(MapCartoTemplateLib.YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            PointClass class2 = new PointClass {
                X = ipoint_0.X,
                Y = ipoint_1.Y
            };
            IPoint point = class2;
            PointClass class3 = new PointClass {
                X = ipoint_1.X,
                Y = ipoint_0.Y
            };
            IPoint point2 = class3;
            return this.method_38(jlktransformation_0, ipoint_0, point, ipoint_1, point2);
        }

        private IPolygon method_36(IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3, bool bool_10)
        {
            IPolygon polygon = new PolygonClass();
            IRing inGeometry = new RingClass();
            IPointCollection points = inGeometry as IPointCollection;
            object missing = Type.Missing;
            points.AddPoint(ipoint_0, ref missing, ref missing);
            points.AddPoint(ipoint_1, ref missing, ref missing);
            points.AddPoint(ipoint_2, ref missing, ref missing);
            points.AddPoint(ipoint_3, ref missing, ref missing);
            points.AddPoint(ipoint_0, ref missing, ref missing);
            inGeometry.Close();
            (polygon as IGeometryCollection).AddGeometry(inGeometry, ref missing, ref missing);
            if (bool_10)
            {
                IRing ring2 = new RingClass();
                points = ring2 as IPointCollection;
                IPoint inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.LeftInOutSpace, ipoint_0.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_1.X - this.LeftInOutSpace, ipoint_1.Y + this.TopInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_2.X + this.RightInOutSpace, ipoint_2.Y + this.TopInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_3.X + this.RightInOutSpace, ipoint_3.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                inPoint.PutCoords(ipoint_0.X - this.LeftInOutSpace, ipoint_0.Y - this.BottomInOutSpace);
                points.AddPoint(inPoint, ref missing, ref missing);
                ring2.Close();
                (polygon as IGeometryCollection).AddGeometry(ring2, ref missing, ref missing);
            }
            return polygon;
        }

        private void method_37(MapCartoTemplateLib.YTTransformation jlktransformation_0, IPolygon ipolygon_0, bool bool_10, double double_22, double double_23, double double_24, double double_25, double double_26, ILineSymbol ilineSymbol_0, IGroupElement igroupElement_0)
        {
            IPointCollection points = null;
            object missing = Type.Missing;
            while (double_22 < double_23)
            {
                IPolyline polyline = new PolylineClass();
                points = polyline as IPointCollection;
                IPoint inPoint = new PointClass();
                if (bool_10)
                {
                    inPoint.PutCoords(double_25, double_22);
                }
                else
                {
                    inPoint.PutCoords(double_22, double_25);
                }
                points.AddPoint(inPoint, ref missing, ref missing);
                inPoint = new PointClass();
                if (bool_10)
                {
                    inPoint.PutCoords(double_26, double_22);
                }
                else
                {
                    inPoint.PutCoords(double_22, double_26);
                }
                points.AddPoint(inPoint, ref missing, ref missing);
                IPointCollection points2 = this.method_20(polyline, ipolygon_0);
                points.RemovePoints(0, points.PointCount);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(0)), ref missing, ref missing);
                points.AddPoint(jlktransformation_0.ToPageLayoutPoint(points2.get_Point(1)), ref missing, ref missing);
                IElement element = new LineElementClass {
                    Geometry = polyline
                };
                (element as ILineElement).Symbol = ilineSymbol_0;
                igroupElement_0.AddElement(element);
                double_22 += double_24;
            }
        }

        private IElement method_38(MapCartoTemplateLib.YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            ILineSymbol gridSymbol;
            IPolygon polygon = this.method_36(ipoint_0, ipoint_1, ipoint_2, ipoint_3, false);
            IGroupElement element = new GroupElementClass();
            double num = (ipoint_0.X > ipoint_1.X) ? ipoint_0.X : ipoint_1.X;
            double num2 = (ipoint_0.Y > ipoint_3.Y) ? ipoint_0.Y : ipoint_3.Y;
            double num3 = (ipoint_2.X < ipoint_3.X) ? ipoint_2.X : ipoint_3.X;
            double num4 = (ipoint_1.Y < ipoint_2.Y) ? ipoint_1.Y : ipoint_2.Y;
            double num5 = (ipoint_0.X < ipoint_1.X) ? (ipoint_0.X - 1.0) : (ipoint_1.X - 1.0);
            double num6 = (ipoint_0.Y < ipoint_3.Y) ? (ipoint_0.Y - 1.0) : (ipoint_3.Y - 1.0);
            double num7 = (ipoint_2.X > ipoint_3.X) ? (ipoint_2.X + 1.0) : (ipoint_3.X + 1.0);
            double num8 = (ipoint_1.Y > ipoint_2.Y) ? (ipoint_1.Y + 1.0) : (ipoint_2.Y + 1.0);
            IPoint point = this.method_32(num, num2);
            IPoint point2 = this.method_32(num3, num4);
            double x = point.X;
            double y = point.Y;
            double num11 = point2.X;
            double num12 = point2.Y;
            double num13 = Math.Abs((double) (x - ipoint_0.X)) / this.Scale;
            if (num13 < 0.02)
            {
                x += this.MapXInterval;
            }
            if (num11 > ipoint_3.X)
            {
                num11 -= this.MapXInterval;
            }
            num13 = Math.Abs((double) (num11 - ipoint_3.X)) / this.Scale;
            if (num13 < 0.02)
            {
                num11 -= this.MapXInterval;
            }
            num11 += this.MapXInterval / 2.0;
            num13 = Math.Abs((double) (y - ipoint_0.Y)) / this.Scale;
            if (num13 < 0.02)
            {
                y += this.MapYInterval;
            }
            if (num12 > ipoint_2.Y)
            {
                num12 -= this.MapYInterval;
            }
            num13 = Math.Abs((double) (num12 - ipoint_2.Y)) / this.Scale;
            if (num13 < 0.02)
            {
                num12 -= this.MapYInterval;
            }
            num12 += this.MapYInterval / 2.0;
            if (this.GridSymbol is ILineSymbol)
            {
                gridSymbol = this.GridSymbol as ILineSymbol;
            }
            else
            {
                gridSymbol = new SimpleLineSymbolClass {
                    Color = ColorManage.CreatColor(0, 0, 0)
                };
            }
            this.method_37(jlktransformation_0, polygon, false, x, num11, this.MapXInterval, num6, num8, gridSymbol, element);
            this.method_37(jlktransformation_0, polygon, true, y, num12, this.MapYInterval, num5, num7, gridSymbol, element);
            (element as IElementProperties2).Type = "公里网";
            return (element as IElement);
        }

        private object method_39(object object_0, string string_6)
        {
            IMemoryBlobStream stream = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = stream
            };
            IPropertySet set = new PropertySetClass();
            IPersistStream stream3 = set as IPersistStream;
            set.SetProperty(string_6, object_0);
            stream3.Save(pstm, 0);
            return stream;
        }

        private void method_4(IPageLayout ipageLayout_0, string string_6)
        {
        }

        private object method_40(object object_0, string string_6)
        {
            if (object_0 is DBNull)
            {
                return null;
            }
            IMemoryBlobStream o = object_0 as IMemoryBlobStream;
            IPropertySet set = new PropertySetClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = o
            };
            IPersistStream stream3 = set as IPersistStream;
            object property = null;
            try
            {
                object obj4;
                object obj5;
                stream3.Load(pstm);
                set.GetAllProperties(out obj4, out obj5);
                property = set.GetProperty(string_6);
            }
            catch (Exception)
            {
            }
            ComReleaser.ReleaseCOMObject(stream3);
            ComReleaser.ReleaseCOMObject(set);
            ComReleaser.ReleaseCOMObject(o);
            set = null;
            return property;
        }

        private IElement method_41(IActiveView iactiveView_0, MapCartoTemplateLib.YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            IPoint jWD = jlktransformation_0.GetJWD(ipoint_0);
            IPoint point2 = jlktransformation_0.GetJWD(ipoint_1);
            IPoint point3 = jlktransformation_0.GetJWD(ipoint_2);
            IPoint point4 = jlktransformation_0.GetJWD(ipoint_3);
            IPoint point5 = jlktransformation_0.ToPageLayoutPoint(ipoint_0);
            IPoint point6 = jlktransformation_0.ToPageLayoutPoint(ipoint_1);
            IPoint point7 = jlktransformation_0.ToPageLayoutPoint(ipoint_2);
            IPoint point8 = jlktransformation_0.ToPageLayoutPoint(ipoint_3);
            IGroupElement element = new GroupElementClass();
            string str = "\x00b0";
            string str2 = "'";
            string str3 = "″";
            string str4 = "";
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            double num4 = 0.0;
            double num5 = 0.0;
            IPoint point9 = new PointClass();
            ITextSymbol symbol = null;
            ITextSymbol symbol2 = null;
            ITextSymbol symbol3 = null;
            ITextSymbol symbol4 = null;
            symbol = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            symbol2 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            symbol3 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            symbol4 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            MapFrameAssistant.DEG2DDDMMSS(jWD.X, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            point9.PutCoords(10.0, 10.0);
            TextElementClass class2 = new TextElementClass {
                Text = num.ToString() + str,
                Geometry = point9,
                Symbol = symbol
            };
            MapFrameAssistant.TextWidth(iactiveView_0, class2, out num4, out num5);
            element.AddElement(this.method_26(point5.X - num4, point5.Y - this.BottomInOutSpace, str4, symbol) as IElement);
            MapFrameAssistant.DEG2DDDMMSS(point2.X, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element.AddElement(this.method_26(point6.X - num4, point6.Y + this.TopInOutSpace, str4, symbol2) as IElement);
            MapFrameAssistant.DEG2DDDMMSS(point4.X, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            point9.PutCoords(10.0, 10.0);
            TextElementClass class3 = new TextElementClass {
                Text = num.ToString() + str,
                Symbol = symbol,
                Geometry = point9
            };
            MapFrameAssistant.TextWidth(iactiveView_0, class3, out num4, out num5);
            element.AddElement(this.method_26(point8.X - num4, point8.Y - this.BottomInOutSpace, str4, symbol) as IElement);
            MapFrameAssistant.DEG2DDDMMSS(point3.X, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString() + str2 + num3.ToString() + str3;
            element.AddElement(this.method_26(point7.X - num4, point7.Y + this.TopInOutSpace, str4, symbol2) as IElement);
            MapFrameAssistant.DEG2DDDMMSS(jWD.Y, ref num, ref num2, ref num3);
            element.AddElement(this.method_26(point5.X - (this.LeftInOutSpace / 2.0), point5.Y, num.ToString() + str, symbol3) as IElement);
            element.AddElement(this.method_26(point5.X - ((this.LeftInOutSpace * 9.0) / 10.0), point5.Y, num2.ToString() + str2 + num3.ToString() + str3, symbol4) as IElement);
            MapFrameAssistant.DEG2DDDMMSS(point4.Y, ref num, ref num2, ref num3);
            element.AddElement(this.method_26(point8.X + (this.RightInOutSpace / 2.0), point8.Y, num.ToString() + str, symbol3) as IElement);
            element.AddElement(this.method_26(point8.X + ((this.RightInOutSpace * 1.0) / 10.0), point8.Y, num2.ToString() + str2 + num3.ToString() + str3, symbol4) as IElement);
            MapFrameAssistant.DEG2DDDMMSS(point2.Y, ref num, ref num2, ref num3);
            element.AddElement(this.method_26(point6.X - (this.LeftInOutSpace / 2.0), point6.Y, num.ToString() + str, symbol3) as IElement);
            element.AddElement(this.method_26(point6.X - ((this.LeftInOutSpace * 9.0) / 10.0), point6.Y, num2.ToString() + str2 + num3.ToString() + str3, symbol4) as IElement);
            MapFrameAssistant.DEG2DDDMMSS(point3.Y, ref num, ref num2, ref num3);
            element.AddElement(this.method_26(point7.X + (this.RightInOutSpace / 2.0), point7.Y, num.ToString() + str, symbol3) as IElement);
            element.AddElement(this.method_26(point7.X + ((this.RightInOutSpace * 1.0) / 10.0), point7.Y, num2.ToString() + str2 + num3.ToString() + str3, symbol4) as IElement);
            return (element as IElement);
        }

        private IElement method_42(IActiveView iactiveView_0, double double_22, double double_23, double double_24, double double_25, IPoint ipoint_0, IPoint ipoint_1, IPoint ipoint_2, IPoint ipoint_3)
        {
            IGroupElement element = new GroupElementClass();
            string str = "\x00b0";
            string str2 = "'";
            string str3 = "″";
            string str4 = "";
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            double num4 = 0.0;
            double num5 = 0.0;
            IPoint point = new PointClass();
            ITextSymbol symbol = null;
            ITextSymbol symbol2 = null;
            ITextSymbol symbol3 = null;
            ITextSymbol symbol4 = null;
            symbol = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            symbol2 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            symbol3 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            symbol4 = this.FontStyle(8.0, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            MapFrameAssistant.DEG2DDDMMSS(double_22, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString("00") + str2 + num3.ToString("00") + str3;
            point.PutCoords(10.0, 10.0);
            TextElementClass class2 = new TextElementClass {
                Text = num.ToString() + str,
                Geometry = point,
                Symbol = symbol
            };
            MapFrameAssistant.TextWidth(iactiveView_0, class2, out num4, out num5);
            element.AddElement(this.method_26(ipoint_0.X - num4, ipoint_0.Y - this.BottomInOutSpace, str4, symbol) as IElement);
            element.AddElement(this.method_26(ipoint_1.X - num4, ipoint_1.Y + this.TopInOutSpace, str4, symbol2) as IElement);
            double_22 += double_24;
            MapFrameAssistant.DEG2DDDMMSS(double_22, ref num, ref num2, ref num3);
            str4 = num.ToString() + str + num2.ToString("00") + str2 + num3.ToString("00") + str3;
            point.PutCoords(10.0, 10.0);
            TextElementClass class3 = new TextElementClass {
                Text = num.ToString() + str,
                Symbol = symbol,
                Geometry = point
            };
            MapFrameAssistant.TextWidth(iactiveView_0, class3, out num4, out num5);
            element.AddElement(this.method_26(ipoint_3.X - num4, ipoint_3.Y - this.BottomInOutSpace, str4, symbol) as IElement);
            element.AddElement(this.method_26(ipoint_2.X - num4, ipoint_2.Y + this.TopInOutSpace, str4, symbol2) as IElement);
            MapFrameAssistant.DEG2DDDMMSS(double_23, ref num, ref num2, ref num3);
            element.AddElement(this.method_26(ipoint_0.X - (this.LeftInOutSpace / 2.0), ipoint_0.Y, num.ToString() + str, symbol3) as IElement);
            element.AddElement(this.method_26(ipoint_0.X - ((this.LeftInOutSpace * 9.0) / 10.0), ipoint_0.Y, num2.ToString("00") + str2 + num3.ToString("00") + str3, symbol4) as IElement);
            element.AddElement(this.method_26(ipoint_3.X + (this.RightInOutSpace / 2.0), ipoint_3.Y, num.ToString() + str, symbol3) as IElement);
            element.AddElement(this.method_26(ipoint_3.X + ((this.RightInOutSpace * 1.0) / 10.0), ipoint_3.Y, num2.ToString("00") + str2 + num3.ToString("00") + str3, symbol4) as IElement);
            double_23 += double_25;
            MapFrameAssistant.DEG2DDDMMSS(double_23, ref num, ref num2, ref num3);
            element.AddElement(this.method_26(ipoint_1.X - (this.LeftInOutSpace / 2.0), ipoint_1.Y, num.ToString() + str, symbol3) as IElement);
            element.AddElement(this.method_26(ipoint_1.X - ((this.LeftInOutSpace * 9.0) / 10.0), ipoint_1.Y, num2.ToString("00") + str2 + num3.ToString("00") + str3, symbol4) as IElement);
            element.AddElement(this.method_26(ipoint_2.X + (this.RightInOutSpace / 2.0), ipoint_2.Y, num.ToString() + str, symbol3) as IElement);
            element.AddElement(this.method_26(ipoint_2.X + ((this.RightInOutSpace * 1.0) / 10.0), ipoint_2.Y, num2.ToString("00") + str2 + num3.ToString("00") + str3, symbol4) as IElement);
            return (element as IElement);
        }

        private string method_43(double double_22)
        {
            if (this.AnnoUnit == esriUnits.esriKilometers)
            {
                double_22 /= 1000.0;
            }
            double_22 /= this.AnnoUnitZoomScale;
            return string.Format("{0:0}", double_22);
        }

        private IElement method_44(MapCartoTemplateLib.YTTransformation jlktransformation_0, IPoint ipoint_0, IPoint ipoint_1)
        {
            IPoint point2;
            IPolyline polyline;
            IPointCollection points;
            IPoint point3;
            string str;
            string str2;
            IGroupElement element = new GroupElementClass();
            IPoint point = this.method_32(ipoint_0.X, ipoint_0.Y);
            double x = point.X;
            double y = point.Y;
            int smallFontSize = (int) this.SmallFontSize;
            int bigFontSize = (int) this.BigFontSize;
            double num5 = 0.2;
            ITextSymbol symbol = this.FontStyle((double) smallFontSize, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol2 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol3 = this.FontStyle((double) smallFontSize, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol4 = this.FontStyle((double) smallFontSize, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol5 = this.FontStyle((double) smallFontSize, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol6 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol7 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHACenter, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol8 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVATop);
            ITextSymbol symbol9 = this.FontStyle((double) bigFontSize, esriTextHorizontalAlignment.esriTHARight, esriTextVerticalAlignment.esriTVABottom);
            ITextSymbol symbol10 = this.FontStyle((double) smallFontSize, esriTextHorizontalAlignment.esriTHALeft, esriTextVerticalAlignment.esriTVABottom);
            object missing = Type.Missing;
            ILineSymbol symbol11 = this.method_25();
            while (true)
            {
                if (x >= ipoint_1.X)
                {
                    break;
                }
                try
                {
                    PointClass class2 = new PointClass {
                        X = x,
                        Y = ipoint_0.Y
                    };
                    point2 = class2;
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    if (this.DrawRoundLineShortLine)
                    {
                        polyline = new PolylineClass();
                        points = polyline as IPointCollection;
                        points.AddPoint(point2, ref missing, ref missing);
                        PointClass class3 = new PointClass {
                            X = point2.X,
                            Y = point2.Y - this.BottomInOutSpace
                        };
                        point3 = class3;
                        points.AddPoint(point3, ref missing, ref missing);
                        LineElementClass class4 = new LineElementClass {
                            Symbol = symbol11,
                            Geometry = polyline
                        };
                        element.AddElement(class4);
                    }
                    if (this.DrawRoundText)
                    {
                        this.method_23(x, this.MapXInterval, out str, out str2);
                        element.AddElement(this.method_26(point2.X, point2.Y - this.BottomInOutSpace, str2, symbol10) as IElement);
                        if (!string.IsNullOrEmpty(str))
                        {
                            element.AddElement(this.method_26(point2.X, point2.Y - this.BottomInOutSpace, str, symbol9) as IElement);
                        }
                    }
                    PointClass class5 = new PointClass {
                        X = x,
                        Y = ipoint_1.Y
                    };
                    point2 = class5;
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    if (this.DrawRoundLineShortLine)
                    {
                        polyline = new PolylineClass();
                        points = polyline as IPointCollection;
                        points.AddPoint(point2, ref missing, ref missing);
                        PointClass class6 = new PointClass {
                            X = point2.X,
                            Y = point2.Y + this.TopInOutSpace
                        };
                        point3 = class6;
                        points.AddPoint(point3, ref missing, ref missing);
                        LineElementClass class7 = new LineElementClass {
                            Symbol = symbol11,
                            Geometry = polyline
                        };
                        element.AddElement(class7);
                    }
                    if (this.DrawRoundText)
                    {
                        this.method_23(x, this.MapXInterval, out str, out str2);
                        element.AddElement(this.method_26(point2.X, point2.Y + this.TopInOutSpace, str2, symbol3) as IElement);
                        if (!string.IsNullOrEmpty(str))
                        {
                            element.AddElement(this.method_26(point2.X, point2.Y + this.TopInOutSpace, str, symbol8) as IElement);
                        }
                    }
                }
                catch
                {
                }
                x += this.MapXInterval;
            }
            while (y < ipoint_1.Y)
            {
                try
                {
                    PointClass class8 = new PointClass {
                        X = ipoint_0.X,
                        Y = y
                    };
                    point2 = class8;
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    if (this.DrawRoundLineShortLine)
                    {
                        polyline = new PolylineClass();
                        points = polyline as IPointCollection;
                        points.AddPoint(point2, ref missing, ref missing);
                        PointClass class9 = new PointClass {
                            X = point2.X - this.LeftInOutSpace,
                            Y = point2.Y
                        };
                        point3 = class9;
                        points.AddPoint(point3, ref missing, ref missing);
                        LineElementClass class10 = new LineElementClass {
                            Symbol = symbol11,
                            Geometry = polyline
                        };
                        element.AddElement(class10);
                    }
                    this.method_23(y, this.MapYInterval, out str, out str2);
                    double num6 = 0.0;
                    double num7 = 0.0;
                    double num8 = 0.0;
                    double num9 = 0.0;
                    PointClass class11 = new PointClass {
                        X = point2.X,
                        Y = point2.Y + num5
                    };
                    IPoint point4 = class11;
                    ITextElement element2 = null;
                    if (!string.IsNullOrEmpty(str))
                    {
                        TextElementClass class12 = new TextElementClass {
                            Geometry = point4,
                            Symbol = symbol2,
                            Text = str
                        };
                        element2 = class12;
                        jlktransformation_0.TextWidth(element2, out num6, out num7);
                    }
                    TextElementClass class13 = new TextElementClass {
                        Geometry = point4,
                        Symbol = symbol,
                        Text = str2
                    };
                    element2 = class13;
                    jlktransformation_0.TextWidth(element2, out num8, out num9);
                    if (this.DrawRoundText)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            element.AddElement(this.method_26(point2.X - (this.LeftInOutSpace / 2.0), point2.Y, str, symbol6) as IElement);
                        }
                        element.AddElement(this.method_26(point2.X - (this.LeftInOutSpace / 2.0), point2.Y, str2, symbol4) as IElement);
                    }
                    PointClass class14 = new PointClass {
                        X = ipoint_1.X,
                        Y = y
                    };
                    point2 = class14;
                    point2 = jlktransformation_0.ToPageLayoutPoint(point2);
                    if (this.DrawRoundLineShortLine)
                    {
                        polyline = new PolylineClass();
                        points = polyline as IPointCollection;
                        points.AddPoint(point2, ref missing, ref missing);
                        PointClass class15 = new PointClass {
                            X = point2.X + this.RightInOutSpace,
                            Y = point2.Y
                        };
                        point3 = class15;
                        points.AddPoint(point3, ref missing, ref missing);
                        LineElementClass class16 = new LineElementClass {
                            Symbol = symbol11,
                            Geometry = polyline
                        };
                        element.AddElement(class16);
                    }
                    if (this.DrawRoundText)
                    {
                        element.AddElement(this.method_26(point2.X + (this.RightInOutSpace / 2.0), point2.Y, str2, symbol5) as IElement);
                        if (!string.IsNullOrEmpty(str))
                        {
                            element.AddElement(this.method_26(point2.X + (this.RightInOutSpace / 2.0), point2.Y, str, symbol7) as IElement);
                        }
                    }
                }
                catch
                {
                }
                y += this.MapYInterval;
            }
            return (element as IElement);
        }

        private double method_5(double double_22, double double_23)
        {
            int num = (int) (double_22 / double_23);
            if ((double_22 < 0.0) && ((double_23 * num) > double_22))
            {
                num--;
            }
            return (num * double_23);
        }

        private void method_6(IPageLayout ipageLayout_0)
        {
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(ipageLayout_0);
            IEnvelope bounds = new EnvelopeClass();
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
            double num = (int) ((extent.Width / envelope.Width) * 100.0);
            int num2 = (int) (num / 1000.0);
            double num3 = num2 * 0x3e8;
            if (Math.Abs((double) (num3 - num)) > 10.0)
            {
                num2++;
            }
            num = num2 * 0x3e8;
            double x = extent.LowerLeft.X;
            double y = extent.LowerLeft.Y;
            this.double_6 = num;
            this.MapXInterval = this.XInterval * this.double_6;
            this.MapYInterval = this.YInterval * this.double_6;
            this.Width = envelope.Width;
            this.Height = envelope.Height;
            new MapCartoTemplateLib.YTTransformation(ipageLayout_0 as IActiveView);
            IGroupElement element = this.method_28(ipageLayout_0, extent.LowerLeft, extent.UpperLeft, extent.UpperRight, extent.LowerRight);
            (element as IElement).QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            (ipageLayout_0 as IGraphicsContainer).AddElement(element as IElement, -1);
            (element as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
            (ipageLayout_0 as IGraphicsContainer).UpdateElement(element as IElement);
            focusMapFrame.Border = null;
            (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void method_7(IPageLayout ipageLayout_0)
        {
            IMapFrame focusMapFrame = MapFrameAssistant.GetFocusMapFrame(ipageLayout_0);
            IEnvelope bounds = new EnvelopeClass();
            IEnvelope envelope = (focusMapFrame as IElement).Geometry.Envelope;
            IEnvelope extent = (focusMapFrame.Map as IActiveView).Extent;
            double num = (int) ((extent.Width / envelope.Width) * 100.0);
            double[] numArray = new double[] { 500.0, 1000.0, 2000.0, 5000.0, 10000.0, 25000.0, 50000.0, 100000.0, 250000.0, 500000.0, 1000000.0 };
            bool flag = false;
            for (int i = 0; i < numArray.Length; i++)
            {
                if (num < numArray[i])
                {
                    num = numArray[i];
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                num = numArray[numArray.Length - 1];
            }
            double x = extent.LowerLeft.X;
            double y = extent.LowerLeft.Y;
            this.double_6 = num;
            this.MapXInterval = this.XInterval * this.double_6;
            this.MapYInterval = this.YInterval * this.double_6;
            this.Width = envelope.Width;
            this.Height = envelope.Height;
            new MapCartoTemplateLib.YTTransformation(ipageLayout_0 as IActiveView);
            IGroupElement element = this.method_28(ipageLayout_0, extent.LowerLeft, extent.UpperLeft, extent.UpperRight, extent.LowerRight);
            (element as IElement).QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            (ipageLayout_0 as IGraphicsContainer).AddElement(element as IElement, -1);
            (element as ITransform2D).Scale(bounds.UpperLeft, 1.0, 1.0);
            (ipageLayout_0 as IGraphicsContainer).UpdateElement(element as IElement);
            focusMapFrame.Border = null;
            (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IPoint method_8(IPoint ipoint_0, IProjectedCoordinateSystem iprojectedCoordinateSystem_0)
        {
            ipoint_0.SpatialReference = iprojectedCoordinateSystem_0;
            ipoint_0.Project(iprojectedCoordinateSystem_0.GeographicCoordinateSystem);
            return ipoint_0;
        }

        private IPoint method_9(IPoint ipoint_0, IProjectedCoordinateSystem iprojectedCoordinateSystem_0)
        {
            ipoint_0.SpatialReference = iprojectedCoordinateSystem_0.GeographicCoordinateSystem;
            ipoint_0.Project(iprojectedCoordinateSystem_0);
            return ipoint_0;
        }

        public void RemoveAllMapTemplateElement()
        {
            if (this.mapTemplateElelemt != null)
            {
                this.mapTemplateElelemt.Clear();
            }
        }

        public void RemoveAllMapTemplateParam()
        {
            if (this.mapTemplateParam != null)
            {
                this.mapTemplateParam.Clear();
            }
        }

        public void RemoveMapTemplateElement(MapCartoTemplateLib.MapTemplateElement mapTemplateElement_0)
        {
            if (((mapTemplateElement_0 != null) && (this.mapTemplateElelemt != null)) && this.mapTemplateElelemt.Contains(mapTemplateElement_0))
            {
                this.mapTemplateElelemt.Remove(mapTemplateElement_0);
            }
        }

        public void RemoveMapTemplateParam(MapCartoTemplateLib.MapTemplateParam mapTemplateParam_0)
        {
            if (((mapTemplateParam_0 != null) && (this.mapTemplateParam != null)) && this.mapTemplateParam.Contains(mapTemplateParam_0))
            {
                this.mapTemplateParam.Remove(mapTemplateParam_0);
            }
        }

        public void Save()
        {
            IRow row = null;
            if (this.OID == -1)
            {
                row = this.MapTemplateGallery.MapTemplateTable.CreateRow();
                this.OID = row.OID;
            }
            else
            {
                row = this.MapTemplateGallery.MapTemplateTable.GetRow(this.OID);
            }
            RowAssisant.SetFieldValue(row, "Name", this.Name);
            RowAssisant.SetFieldValue(row, "ClassID", this.MapTemplateClass.OID);
            RowAssisant.SetFieldValue(row, "ClassGuid", this.ClassGuid);
            RowAssisant.SetFieldValue(row, "Guid", this.Guid);
            RowAssisant.SetFieldValue(row, "Width", this.Width);
            RowAssisant.SetFieldValue(row, "Height", this.Height);
            RowAssisant.SetFieldValue(row, "Scale", this.Scale);
            if (this.MapFramingType == MapCartoTemplateLib.MapFramingType.StandardFraming)
            {
                if (this.MapFrameType == MapCartoTemplateLib.MapFrameType.MFTRect)
                {
                    RowAssisant.SetFieldValue(row, "FrameType", 0);
                }
                else
                {
                    RowAssisant.SetFieldValue(row, "FrameType", 1);
                }
            }
            else if (this.MapFrameType == MapCartoTemplateLib.MapFrameType.MFTRect)
            {
                RowAssisant.SetFieldValue(row, "FrameType", 2);
            }
            else
            {
                RowAssisant.SetFieldValue(row, "FrameType", 3);
            }
            RowAssisant.SetFieldValue(row, "BorderSymbol", this.method_39(this.BorderSymbol, "Symbol"));
            if (this.GridSymbol != null)
            {
                RowAssisant.SetFieldValue(row, "GridSymbol", this.method_39(this.GridSymbol, "Symbol"));
            }
            else
            {
                RowAssisant.SetFieldValue(row, "GridSymbol", DBNull.Value);
            }
            RowAssisant.SetFieldValue(row, "OutBorderWidth", this.OutBorderWidth);
            RowAssisant.SetFieldValue(row, "LeftInOutSpace", this.LeftInOutSpace);
            RowAssisant.SetFieldValue(row, "RightInOutSpace", this.RightInOutSpace);
            RowAssisant.SetFieldValue(row, "BottomInOutSpace", this.BottomInOutSpace);
            RowAssisant.SetFieldValue(row, "TopInOutSpace", this.RightInOutSpace);
            if (this.MapGrid != null)
            {
                RowAssisant.SetFieldValue(row, "MapGrid", this.method_39(this.MapGrid, "MapGrid"));
            }
            RowAssisant.SetFieldValue(row, "AnnoUnit", (int) this.AnnoUnit);
            RowAssisant.SetFieldValue(row, "AnnoUnitZoomScale", this.AnnoUnitZoomScale);
            RowAssisant.SetFieldValue(row, "XInterval", this.XInterval);
            RowAssisant.SetFieldValue(row, "YInterval", this.YInterval);
            RowAssisant.SetFieldValue(row, "FixDataRange", (int) this.TemplateSizeStyle);
            RowAssisant.SetFieldValue(row, "Description", this.Description);
            RowAssisant.SetFieldValue(row, "DrawCornerShortLine", this.DrawCornerShortLine ? 1 : 0);
            RowAssisant.SetFieldValue(row, "DrawJWD", this.DrawJWD ? 1 : 0);
            RowAssisant.SetFieldValue(row, "DrawRoundLineShortLine", this.DrawRoundLineShortLine ? 1 : 0);
            RowAssisant.SetFieldValue(row, "DrawRoundText", this.DrawRoundText ? 1 : 0);
            RowAssisant.SetFieldValue(row, "DrawCornerText", this.DrawCornerText ? 1 : 0);
            RowAssisant.SetFieldValue(row, "FixedWidthAndBottomSpace", this.FixedWidthAndBottomSpace ? 1 : 0);
            RowAssisant.SetFieldValue(row, "IsAdapationScale", this.IsAdapationScale ? 1 : 0);
            RowAssisant.SetFieldValue(row, "FontName", this.FontName);
            RowAssisant.SetFieldValue(row, "SmallFontSize", this.SmallFontSize);
            RowAssisant.SetFieldValue(row, "BigFontSize", this.BigFontSize);
            row.Store();
            if (this.mapTemplateParam != null)
            {
                foreach (MapCartoTemplateLib.MapTemplateParam param in this.mapTemplateParam)
                {
                    param.Save();
                }
            }
            if (this.mapTemplateElelemt != null)
            {
                foreach (MapCartoTemplateLib.MapTemplateElement element in this.mapTemplateElelemt)
                {
                    element.Save();
                }
            }
        }

        public void SaveToFile(string string_6)
        {
            object obj2;
            System.IO.FileStream output = new System.IO.FileStream(string_6, FileMode.CreateNew);
            BinaryWriter writer = new BinaryWriter(output);
            IPropertySet set = new PropertySetClass();
            set.SetProperty("Name", this.Name);
            set.SetProperty("Width", this.Width);
            set.SetProperty("Height", this.Height);
            set.SetProperty("Scale", this.Scale);
            set.SetProperty("FrameType", (int) this.MapFrameType);
            set.SetProperty("BorderSymbol", this.BorderSymbol);
            set.SetProperty("GridSymbol", this.GridSymbol);
            set.SetProperty("OutBorderWidth", this.OutBorderWidth);
            set.SetProperty("LeftInOutSpace", this.LeftInOutSpace);
            set.SetProperty("RightInOutSpace", this.RightInOutSpace);
            set.SetProperty("BottomInOutSpace", this.BottomInOutSpace);
            set.SetProperty("TopInOutSpace", this.RightInOutSpace);
            set.SetProperty("MapGrid", this.MapGrid);
            set.SetProperty("AnnoUnit", (int) this.AnnoUnit);
            set.SetProperty("AnnoUnitZoomScale", this.AnnoUnitZoomScale);
            set.SetProperty("XInterval", this.XInterval);
            set.SetProperty("YInterval", this.YInterval);
            set.SetProperty("FixDataRange", (int) this.TemplateSizeStyle);
            set.SetProperty("Description", this.Description);
            set.SetProperty("DrawCornerShortLine", this.DrawCornerShortLine ? 1 : 0);
            set.SetProperty("DrawJWD", this.DrawJWD ? 1 : 0);
            set.SetProperty("DrawRoundLineShortLine", this.DrawRoundLineShortLine ? 1 : 0);
            set.SetProperty("DrawRoundText", this.DrawRoundText ? 1 : 0);
            set.SetProperty("DrawCornerText", this.DrawCornerText ? 1 : 0);
            set.SetProperty("FixedWidthAndBottomSpace", this.FixedWidthAndBottomSpace ? 1 : 0);
            set.SetProperty("IsAdapationScale", this.IsAdapationScale ? 1 : 0);
            set.SetProperty("FontName", this.FontName);
            set.SetProperty("SmallFontSize", this.SmallFontSize);
            set.SetProperty("BigFontSize", this.BigFontSize);
            IPropertySetArray array = new PropertySetArrayClass();
            if (this.mapTemplateParam != null)
            {
                foreach (MapCartoTemplateLib.MapTemplateParam param in this.mapTemplateParam)
                {
                    param.Save(array);
                }
            }
            set.SetProperty("MapParams", array);
            array = new PropertySetArrayClass();
            if (this.mapTemplateElelemt != null)
            {
                foreach (MapCartoTemplateLib.MapTemplateElement element in this.mapTemplateElelemt)
                {
                    element.Save(array);
                }
            }
            set.SetProperty("MapElements", array);
            IMemoryBlobStream stream2 = new MemoryBlobStreamClass();
            IObjectStream pstm = new ObjectStreamClass {
                Stream = stream2
            };
            (set as IPersistStream).Save(pstm, 0);
            ((IMemoryBlobStreamVariant) stream2).ExportToVariant(out obj2);
            writer.Write(((byte[]) obj2).Length);
            writer.Write((byte[]) obj2);
            writer.Close();
            output.Close();
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void Update(IPageLayout ipageLayout_0)
        {
            this.Save();
        }

        public esriUnits AnnoUnit
        {
            [CompilerGenerated]
            get
            {
                return this.esriUnits_0;
            }
            [CompilerGenerated]
            set
            {
                this.esriUnits_0 = value;
            }
        }

        public double AnnoUnitZoomScale
        {
            [CompilerGenerated]
            get
            {
                return this.double_12;
            }
            [CompilerGenerated]
            set
            {
                this.double_12 = value;
            }
        }

        public double BigFontSize
        {
            get
            {
                return this.double_0;
            }
            set
            {
                this.double_0 = value;
            }
        }

        public ISymbol BorderSymbol
        {
            [CompilerGenerated]
            get
            {
                return this.isymbol_1;
            }
            [CompilerGenerated]
            set
            {
                this.isymbol_1 = value;
            }
        }

        public double BottomInOutSpace
        {
            [CompilerGenerated]
            get
            {
                return this.double_9;
            }
            [CompilerGenerated]
            set
            {
                this.double_9 = value;
            }
        }

        public double BottomLengthScale
        {
            [CompilerGenerated]
            get
            {
                return this.double_18;
            }
            [CompilerGenerated]
            set
            {
                this.double_18 = value;
            }
        }

        public string ClassGuid
        {
            [CompilerGenerated]
            get
            {
                return this.string_4;
            }
            [CompilerGenerated]
            set
            {
                this.string_4 = value;
            }
        }

        public static MapCartoTemplateLib.MapTemplate CurrentMapTemplate { get; set; }

        public string Description
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            set
            {
                this.string_2 = value;
            }
        }

        public bool DrawCornerShortLine
        {
            [CompilerGenerated]
            get
            {
                return this.bool_2;
            }
            [CompilerGenerated]
            set
            {
                this.bool_2 = value;
            }
        }

        public bool DrawCornerText
        {
            [CompilerGenerated]
            get
            {
                return this.bool_7;
            }
            [CompilerGenerated]
            set
            {
                this.bool_7 = value;
            }
        }

        public bool DrawJWD
        {
            [CompilerGenerated]
            get
            {
                return this.bool_3;
            }
            [CompilerGenerated]
            set
            {
                this.bool_3 = value;
            }
        }

        public bool DrawRoundLineShortLine
        {
            [CompilerGenerated]
            get
            {
                return this.bool_4;
            }
            [CompilerGenerated]
            set
            {
                this.bool_4 = value;
            }
        }

        public bool DrawRoundText
        {
            [CompilerGenerated]
            get
            {
                return this.bool_5;
            }
            [CompilerGenerated]
            set
            {
                this.bool_5 = value;
            }
        }

        public bool FixedWidthAndBottomSpace
        {
            [CompilerGenerated]
            get
            {
                return this.bool_1;
            }
            [CompilerGenerated]
            set
            {
                this.bool_1 = value;
            }
        }

        public string FontName
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

        public ISymbol GridSymbol
        {
            [CompilerGenerated]
            get
            {
                return this.isymbol_2;
            }
            [CompilerGenerated]
            set
            {
                this.isymbol_2 = value;
            }
        }

        public string Guid
        {
            [CompilerGenerated]
            get
            {
                return this.string_3;
            }
            [CompilerGenerated]
            set
            {
                this.string_3 = value;
            }
        }

        public double Height
        {
            [CompilerGenerated]
            get
            {
                return this.double_17;
            }
            [CompilerGenerated]
            set
            {
                this.double_17 = value;
            }
        }

        public bool IsAdapationScale
        {
            [CompilerGenerated]
            get
            {
                return this.bool_6;
            }
            [CompilerGenerated]
            set
            {
                this.bool_6 = value;
            }
        }

        public bool IsApply
        {
            [CompilerGenerated]
            get
            {
                return this.bool_0;
            }
            [CompilerGenerated]
            set
            {
                this.bool_0 = value;
            }
        }

        public bool IsChangeBottomLength
        {
            [CompilerGenerated]
            get
            {
                return this.bool_8;
            }
            [CompilerGenerated]
            set
            {
                this.bool_8 = value;
            }
        }

        public bool IsTest
        {
            [CompilerGenerated]
            get
            {
                return this.bool_9;
            }
            [CompilerGenerated]
            set
            {
                this.bool_9 = value;
            }
        }

        public double LeftInOutSpace
        {
            [CompilerGenerated]
            get
            {
                return this.double_7;
            }
            [CompilerGenerated]
            set
            {
                this.double_7 = value;
            }
        }

        public string LegendInfo
        {
            [CompilerGenerated]
            get
            {
                return this.string_5;
            }
            [CompilerGenerated]
            set
            {
                this.string_5 = value;
            }
        }

        public MapCartoTemplateLib.MapFrameType MapFrameType
        {
            [CompilerGenerated]
            get
            {
                return this.mapFrameType_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapFrameType_0 = value;
            }
        }

        public MapCartoTemplateLib.MapFramingType MapFramingType
        {
            [CompilerGenerated]
            get
            {
                return this.mapFramingType_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapFramingType_0 = value;
            }
        }

        public IMapGrid MapGrid
        {
            [CompilerGenerated]
            get
            {
                return this.imapGrid_0;
            }
            [CompilerGenerated]
            set
            {
                this.imapGrid_0 = value;
            }
        }

        public MapCartoTemplateLib.MapTemplateClass MapTemplateClass
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplateClass_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplateClass_0 = value;
            }
        }

        public List<MapCartoTemplateLib.MapTemplateElement> MapTemplateElement
        {
            get
            {
                if (this.mapTemplateElelemt == null)
                {
                    this.mapTemplateElelemt = new List<MapCartoTemplateLib.MapTemplateElement>();
                }
                return this.mapTemplateElelemt;
            }
            set
            {
                this.RemoveAllMapTemplateElement();
                if (value != null)
                {
                    foreach (MapCartoTemplateLib.MapTemplateElement element in value)
                    {
                        this.AddMapTemplateElement(element);
                    }
                }
            }
        }

        public MapCartoTemplateLib.MapTemplateGallery MapTemplateGallery
        {
            [CompilerGenerated]
            get
            {
                return this.mapTemplateGallery_0;
            }
            [CompilerGenerated]
            set
            {
                this.mapTemplateGallery_0 = value;
            }
        }

        public List<MapCartoTemplateLib.MapTemplateParam> MapTemplateParam
        {
            get
            {
                if (this.mapTemplateParam == null)
                {
                    this.mapTemplateParam = new List<MapCartoTemplateLib.MapTemplateParam>();
                }
                return this.mapTemplateParam;
            }
            set
            {
                this.RemoveAllMapTemplateParam();
                if (value != null)
                {
                    foreach (MapCartoTemplateLib.MapTemplateParam param in value)
                    {
                        this.AddMapTemplateParam(param);
                    }
                }
            }
        }

        protected double MapXInterval
        {
            [CompilerGenerated]
            get
            {
                return this.double_20;
            }
            [CompilerGenerated]
            set
            {
                this.double_20 = value;
            }
        }

        protected double MapYInterval
        {
            [CompilerGenerated]
            get
            {
                return this.double_21;
            }
            [CompilerGenerated]
            set
            {
                this.double_21 = value;
            }
        }

        public string Name
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

        internal MapCartoTemplateLib.MapFrameType NewMapFrameTypeVal
        {
            [CompilerGenerated]
            get
            {
                return this.mapFrameType_1;
            }
            [CompilerGenerated]
            set
            {
                this.mapFrameType_1 = value;
            }
        }

        public int OID
        {
            [CompilerGenerated]
            get
            {
                return this.int_0;
            }
            [CompilerGenerated]
            set
            {
                this.int_0 = value;
            }
        }

        public double OldBottomInOutSpace
        {
            [CompilerGenerated]
            get
            {
                return this.double_19;
            }
            [CompilerGenerated]
            set
            {
                this.double_19 = value;
            }
        }

        public double OutBorderWidth
        {
            [CompilerGenerated]
            get
            {
                return this.double_11;
            }
            [CompilerGenerated]
            set
            {
                this.double_11 = value;
            }
        }

        public ISymbol OutSideStyle
        {
            get
            {
                return this.isymbol_0;
            }
            set
            {
                this.isymbol_0 = value;
            }
        }

        public double RightInOutSpace
        {
            [CompilerGenerated]
            get
            {
                return this.double_8;
            }
            [CompilerGenerated]
            set
            {
                this.double_8 = value;
            }
        }

        public double Scale
        {
            [CompilerGenerated]
            get
            {
                return this.double_15;
            }
            [CompilerGenerated]
            set
            {
                this.double_15 = value;
            }
        }

        public double SmallFontSize
        {
            get
            {
                return this.double_1;
            }
            set
            {
                this.double_1 = value;
            }
        }

        [Obsolete("不在使用")]
        public MapCartoTemplateLib.TemplateSizeStyle TemplateSizeStyle
        {
            [CompilerGenerated]
            get
            {
                return this.templateSizeStyle_0;
            }
            [CompilerGenerated]
            set
            {
                this.templateSizeStyle_0 = value;
            }
        }

        public double TopInOutSpace
        {
            [CompilerGenerated]
            get
            {
                return this.double_10;
            }
            [CompilerGenerated]
            set
            {
                this.double_10 = value;
            }
        }

        public double Width
        {
            [CompilerGenerated]
            get
            {
                return this.double_16;
            }
            [CompilerGenerated]
            set
            {
                this.double_16 = value;
            }
        }

        public double XInterval
        {
            [CompilerGenerated]
            get
            {
                return this.double_13;
            }
            [CompilerGenerated]
            set
            {
                this.double_13 = value;
            }
        }

        public double YInterval
        {
            [CompilerGenerated]
            get
            {
                return this.double_14;
            }
            [CompilerGenerated]
            set
            {
                this.double_14 = value;
            }
        }
    }
}

