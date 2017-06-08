using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateOLEElement : MapTemplateElement
    {
        public MapTemplateOLEElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.OLEElement;
            base.Name = "OLE";
        }

        public MapTemplateOLEElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.OLEElement;
            base.Name = "OLE";
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplateOLEElement element = new MapTemplateOLEElement(mapTemplate_1);
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
            IPoint position = this.GetPosition(ipageLayout_0);
            int hWnd = (ipageLayout_0 as IActiveView).ScreenDisplay.hWnd;
            JLK.ExtendClass.IOleFrame frame = new OleFrame();
            if (frame.CreateOleClientItem((ipageLayout_0 as IActiveView).ScreenDisplay, hWnd))
            {
                IElement element = frame as IElement;
                IEnvelope envelope = new EnvelopeClass();
                envelope.PutCoords(position.X, position.Y, position.X + 4.0, position.Y + 8.0);
                IEnvelope bounds = new EnvelopeClass();
                element.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                envelope.PutCoords(position.X, position.Y, position.X + bounds.Width, position.Y + bounds.Height);
                element.Geometry = envelope;
            }
            this.Element = frame as IElement;
            return this.Element;
        }

        public override void Update(IPageLayout ipageLayout_0)
        {
            if (this.Element != null)
            {
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.Element, null);
                IPoint position = this.GetPosition(ipageLayout_0);
                IEnvelope bounds = new EnvelopeClass();
                this.Element.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                if (!bounds.IsEmpty)
                {
                    IEnvelope envelope2 = new EnvelopeClass();
                    envelope2.PutCoords(position.X, position.Y, position.X + bounds.Width, position.Y + bounds.Height);
                    base.m_pElement.Geometry = envelope2;
                }
                (ipageLayout_0 as IActiveView).GraphicsContainer.UpdateElement(this.Element);
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, this.Element, null);
                this.Save();
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
                try
                {
                }
                catch
                {
                }
            }
        }
    }
}

