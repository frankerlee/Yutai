using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateNorthElement : MapTemplateElement
    {
        public MapTemplateNorthElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.NorthElement;
            base.Name = "指北针";
            base.Style = new MarkerNorthArrowClass();
        }

        public MapTemplateNorthElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.NorthElement;
            base.Name = "指北针";
            base.Style = new MarkerNorthArrowClass();
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplateNorthElement element = new MapTemplateNorthElement(mapTemplate_1);
            try
            {
            }
            catch (Exception)
            {
            }
            this.CopyTo(element);
            return element;
        }

        public override IElement CreateElement(IPageLayout ipageLayout_0)
        {
            INorthArrow optionalStyle = new MarkerNorthArrowClass();
            (optionalStyle as IMarkerNorthArrow).MarkerSymbol = (base.Style as IMarkerNorthArrow).MarkerSymbol;
            IEnvelope oldBounds = new EnvelopeClass();
            IPoint position = this.GetPosition(ipageLayout_0);
            oldBounds.PutCoords(position.X, position.Y, position.X + 3.0, position.Y + 3.0);
            UID clsid = new UIDClass
            {
                Value = "esriCarto.MarkerNorthArrow"
            };
            IMapFrame frame =
                (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
            IElement element = frame.CreateSurroundFrame(clsid, optionalStyle) as IElement;
            IEnvelope newBounds = new EnvelopeClass();
            optionalStyle.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, oldBounds, newBounds);
            oldBounds.PutCoords(position.X, position.Y, position.X + newBounds.Width, position.Y + newBounds.Height);
            element.Geometry = oldBounds;
            this.Element = element;
            return element;
        }

        public override IElement GetElement(IPageLayout ipageLayout_0)
        {
            this.CreateElement(ipageLayout_0);
            if (base.m_pElement == null)
            {
                return null;
            }
            IPoint position = this.GetPosition(ipageLayout_0);
            IEnvelope bounds = new EnvelopeClass();
            base.m_pElement.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
            if ((base.m_pElement as IMapSurroundFrame).MapSurround is INorthArrow)
            {
                INorthArrow mapSurround = (base.m_pElement as IMapSurroundFrame).MapSurround as INorthArrow;
                if (mapSurround.Map != (ipageLayout_0 as IActiveView).FocusMap)
                {
                    mapSurround.Map = (ipageLayout_0 as IActiveView).FocusMap;
                    mapSurround.Refresh();
                }
            }
            if (!bounds.IsEmpty)
            {
                IEnvelope envelope2 = new EnvelopeClass();
                envelope2.PutCoords(position.X, position.Y, position.X + bounds.Width, position.Y + bounds.Height);
                base.m_pElement.Geometry = envelope2;
            }
            return base.m_pElement;
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

        public override IElement Element
        {
            get { return base.Element; }
            set { base.Element = value; }
        }

        protected override IPropertySet PropertySet
        {
            get { return new PropertySetClass(); }
            set { }
        }
    }
}