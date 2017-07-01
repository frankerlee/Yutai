using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateGraphicsElement : MapTemplateElement
    {
        public MapTemplateGraphicsElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.GraphicElement;
            base.Name = "图形";
        }

        public MapTemplateGraphicsElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.GraphicElement;
            base.Name = "图形";
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplateGraphicsElement element = new MapTemplateGraphicsElement(mapTemplate_1);
            this.CopyTo(element);
            return element;
        }

        public override IElement CreateElement(IPageLayout ipageLayout_0)
        {
            return base.m_pElement;
        }

        public override IElement GetElement(IPageLayout ipageLayout_0)
        {
            IPoint position = this.GetPosition(ipageLayout_0);
            IPoint lowerLeft = base.m_pElement.Geometry.Envelope.LowerLeft;
            double dx = position.X - lowerLeft.X;
            double dy = position.Y - lowerLeft.Y;
            if (base.m_pElement is ITransform2D)
            {
                (base.m_pElement as ITransform2D).Move(dx, dy);
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
                    new EnvelopeClass();
                    double dx = position.X - bounds.XMin;
                    double dy = position.Y - bounds.YMin;
                    (base.m_pElement as ITransform2D).Move(dx, dy);
                }
                (ipageLayout_0 as IActiveView).GraphicsContainer.UpdateElement(base.m_pElement);
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                this.Save();
            }
        }

        public override IElement Element
        {
            get { return base.Element; }
            set
            {
                base.Element = value;
                if (base.m_pElement is ILineElement)
                {
                    base.Name = "线元素";
                }
                else if (base.m_pElement is IPolygonElement)
                {
                    base.Name = "面元素";
                }
                else if (base.m_pElement is IRectangleElement)
                {
                    base.Name = "矩形元素";
                }
                else if (base.m_pElement is IEllipseElement)
                {
                    base.Name = "椭圆元素";
                }
                else if (base.m_pElement is ICircleElement)
                {
                    base.Name = "圆元素";
                }
                else if (base.m_pElement is IMarkerElement)
                {
                    base.Name = "点元素";
                }
            }
        }

        protected override IPropertySet PropertySet
        {
            get { return new PropertySetClass(); }
            set { }
        }
    }
}