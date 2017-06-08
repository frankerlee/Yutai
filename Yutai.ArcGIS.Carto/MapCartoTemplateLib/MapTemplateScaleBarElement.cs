using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplateScaleBarElement : MapTemplateElement
    {
        public MapTemplateScaleBarElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.ScaleBarElement;
            base.Name = "比例尺";
            base.Style = new HollowScaleBarClass();
        }

        public MapTemplateScaleBarElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.ScaleBarElement;
            base.Name = "比例尺";
            base.Style = new HollowScaleBarClass();
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplateScaleBarElement element = new MapTemplateScaleBarElement(mapTemplate_1);
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
            IElement element = null;
            IScaleBar style;
            if (base.Style != null)
            {
                style = base.Style as IScaleBar;
            }
            else
            {
                style = new HollowScaleBarClass();
            }
            UIDClass class2 = new UIDClass {
                Value = "esriCarto.ScaleBar"
            };
            UID clsid = class2;
            IMapFrame frame = (ipageLayout_0 as IGraphicsContainer).FindFrame((ipageLayout_0 as IActiveView).FocusMap) as IMapFrame;
            style.Units = frame.Map.DistanceUnits;
            INumberFormat numberFormat = style.NumberFormat;
            if (numberFormat is INumericFormat)
            {
                (numberFormat as INumericFormat).RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
                (numberFormat as INumericFormat).UseSeparator = false;
                (numberFormat as INumericFormat).RoundingValue = 0;
                style.NumberFormat = numberFormat;
            }
            element = frame.CreateSurroundFrame(clsid, style) as IElement;
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
            if ((base.m_pElement as IMapSurroundFrame).MapSurround is IScaleBar)
            {
                base.m_pElement = this.CreateElement(ipageLayout_0);
                IScaleBar mapSurround = (base.m_pElement as IMapSurroundFrame).MapSurround as IScaleBar;
                switch (base.ElementLocation.LocationType)
                {
                    case LocationType.LeftUpper:
                    case LocationType.LeftCenter:
                    case LocationType.LeftLower:
                        position.X -= bounds.Height;
                        break;

                    case LocationType.LowerLeft:
                    case LocationType.LowerCenter:
                    case LocationType.LowerRight:
                        position.Y -= bounds.Height;
                        break;
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

        protected override IPropertySet PropertySet
        {
            get
            {
                IPropertySet set = new PropertySetClass();
                set.SetProperty("Style", base.Style);
                return set;
            }
            set
            {
                base.Style = value.GetProperty("Style");
            }
        }
    }
}

