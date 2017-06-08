using System;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateLegendElement : MapTemplateElement
    {
        [CompilerGenerated]
        private IBackground ibackground_0;
        [CompilerGenerated]
        private IBorder iborder_0;
        [CompilerGenerated]
        private IShadow ishadow_0;

        public MapTemplateLegendElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.LegendElement;
            base.Name = "图例";
            this.method_4();
        }

        public MapTemplateLegendElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.LegendElement;
            base.Name = "图例";
            this.method_4();
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplateLegendElement element = new MapTemplateLegendElement(mapTemplate_1);
            try
            {
                if (this.Background != null)
                {
                    element.Background = (this.Background as IClone).Clone() as IBackground;
                }
                if (this.Border != null)
                {
                    element.Border = (this.Border as IClone).Clone() as IBorder;
                }
                if (this.Shadow != null)
                {
                    element.Shadow = (this.Shadow as IClone).Clone() as IShadow;
                }
            }
            catch (Exception)
            {
            }
            this.CopyTo(element);
            return element;
        }

        public override IElement CreateElement(IPageLayout ipageLayout_0)
        {
            ILegend style;
            if (base.Style != null)
            {
                style = base.Style as ILegend;
            }
            else
            {
                style = new LegendClass_2();
            }
            style.AutoAdd = true;
            style.AutoReorder = true;
            style.AutoVisibility = true;
            UID clsid = new UIDClass {
                Value = "esriCarto.Legend"
            };
            IMapFrame frame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
            IElement element = frame.CreateSurroundFrame(clsid, style) as IElement;
            (element as IFrameElement).Border = this.Border;
            (element as IFrameElement).Background = this.Background;
            (element as IFrameProperties).Shadow = this.Shadow;
            IEnvelope oldBounds = new EnvelopeClass();
            IPoint position = this.GetPosition(ipageLayout_0);
            oldBounds.PutCoords(position.X, position.Y, position.X + 4.0, position.Y + 8.0);
            IEnvelope newBounds = new EnvelopeClass();
            style.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, oldBounds, newBounds);
            oldBounds.PutCoords(position.X, position.Y, position.X + newBounds.Width, position.Y + newBounds.Height);
            element.Geometry = oldBounds;
            this.Element = element;
            return element;
        }

        public override IElement GetElement(IPageLayout ipageLayout_0)
        {
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
            if ((base.m_pElement as IMapSurroundFrame).MapSurround is ILegend)
            {
                ILegend mapSurround = (base.m_pElement as IMapSurroundFrame).MapSurround as ILegend;
                mapSurround.Map = (ipageLayout_0 as IActiveView).FocusMap;
                mapSurround.Refresh();
            }
            if (!bounds.IsEmpty)
            {
                IEnvelope envelope2 = new EnvelopeClass();
                envelope2.PutCoords(position.X, position.Y, position.X + bounds.Width, position.Y + bounds.Height);
                base.m_pElement.Geometry = envelope2;
            }
            return base.m_pElement;
        }

        private void method_4()
        {
            base.Style = new LegendClass_2();
            this.Border = new SymbolBorderClass();
            this.Background = new SymbolBackgroundClass();
            this.Shadow = new SymbolShadowClass();
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

        public IBackground Background
        {
            [CompilerGenerated]
            get
            {
                return this.ibackground_0;
            }
            [CompilerGenerated]
            set
            {
                this.ibackground_0 = value;
            }
        }

        public IBorder Border
        {
            [CompilerGenerated]
            get
            {
                return this.iborder_0;
            }
            [CompilerGenerated]
            set
            {
                this.iborder_0 = value;
            }
        }

        protected override IPropertySet PropertySet
        {
            get
            {
                return new PropertySetClass();
            }
            set
            {
            }
        }

        public IShadow Shadow
        {
            [CompilerGenerated]
            get
            {
                return this.ishadow_0;
            }
            [CompilerGenerated]
            set
            {
                this.ishadow_0 = value;
            }
        }
    }
}

