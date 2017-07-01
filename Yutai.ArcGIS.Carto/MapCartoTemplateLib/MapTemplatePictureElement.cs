using System;
using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Carto.MapCartoTemplateLib
{
    public class MapTemplatePictureElement : MapTemplateElement
    {
        public MapTemplatePictureElement(MapTemplate mapTemplate_1) : base(mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.PictureElement;
            base.Name = "图片";
        }

        public MapTemplatePictureElement(int int_1, MapTemplate mapTemplate_1) : base(int_1, mapTemplate_1)
        {
            base.MapTemplateElementType = MapTemplateElementType.PictureElement;
            base.Name = "图片";
        }

        public override MapTemplateElement Clone(MapTemplate mapTemplate_1)
        {
            MapTemplatePictureElement element = new MapTemplatePictureElement(mapTemplate_1);
            try
            {
                element.PitcuteFile = this.PitcuteFile;
            }
            catch (Exception)
            {
            }
            this.CopyTo(element);
            return element;
        }

        public override IElement CreateElement(IPageLayout ipageLayout_0)
        {
            if (this.PitcuteFile == null)
            {
                return null;
            }
            string str = System.IO.Path.GetExtension(this.PitcuteFile).ToLower();
            IElement element2 = null;
            string str2 = str;
            switch (str2)
            {
                case null:
                    break;

                case ".bmp":
                    element2 = new BmpPictureElementClass();
                    goto Label_009C;

                case ".jpg":
                    element2 = new JpgPictureElementClass();
                    goto Label_009C;

                case ".gif":
                    element2 = new GifPictureElementClass();
                    goto Label_009C;

                default:
                    if (str2 != ".tif")
                    {
                        if (str2 != ".emf")
                        {
                            break;
                        }
                        element2 = new EmfPictureElementClass();
                    }
                    else
                    {
                        element2 = new TifPictureElementClass();
                    }
                    goto Label_009C;
            }
            element2 = new PngPictureElementClass();
            Label_009C:
            (element2 as IPictureElement).ImportPictureFromFile(this.PitcuteFile);
            (element2 as IPictureElement).MaintainAspectRatio = true;
            double widthPoints = 0.0;
            double heightPoints = 0.0;
            (element2 as IPictureElement2).QueryIntrinsicSize(ref widthPoints, ref heightPoints);
            widthPoints *= 0.0353;
            heightPoints *= 0.0353;
            (element2 as IElementProperties2).AutoTransform = true;
            IPoint position = this.GetPosition(ipageLayout_0);
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(position.X, position.Y, position.X + widthPoints, position.Y + heightPoints);
            element2.Geometry = envelope;
            this.Element = element2;
            return element2;
        }

        public override void Update(IPageLayout ipageLayout_0)
        {
            if (base.m_pElement != null)
            {
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                IPoint position = this.GetPosition(ipageLayout_0);
                try
                {
                    IEnvelope bounds = new EnvelopeClass();
                    base.m_pElement.QueryBounds((ipageLayout_0 as IActiveView).ScreenDisplay, bounds);
                    if (!bounds.IsEmpty)
                    {
                        IEnvelope envelope2 = new EnvelopeClass();
                        envelope2.PutCoords(position.X, position.Y, position.X + bounds.Width,
                            position.Y + bounds.Height);
                        base.m_pElement.Geometry = envelope2;
                    }
                }
                catch
                {
                }
                (ipageLayout_0 as IActiveView).GraphicsContainer.UpdateElement(base.m_pElement);
                (ipageLayout_0 as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, base.m_pElement, null);
                this.Save();
            }
        }

        public string PitcuteFile { get; set; }

        protected override IPropertySet PropertySet
        {
            get { return new PropertySetClass(); }
            set { }
        }
    }
}