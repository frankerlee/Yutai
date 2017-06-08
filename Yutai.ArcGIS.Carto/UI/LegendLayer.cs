using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Carto.UI
{
    internal class LegendLayer
    {
        private ILayer ilayer_0 = null;
        private ILegendItem ilegendItem_0 = null;
        private IStyleGalleryItem istyleGalleryItem_0 = new ServerStyleGalleryItemClass();
        private IStyleGalleryItem istyleGalleryItem_1 = new ServerStyleGalleryItemClass();

        public LegendLayer(ILayer ilayer_1)
        {
            this.ilayer_0 = ilayer_1;
            this.ilegendItem_0 = this.method_0();
        }

        private ILegendItem method_0()
        {
            return new HorizontalLegendItemClass { Layer = this.ilayer_0 };
        }

        public override string ToString()
        {
            if (this.ilayer_0 != null)
            {
                return this.ilayer_0.Name;
            }
            return "";
        }

        public IStyleGalleryItem AreaPatchStyleGalleryItem
        {
            get
            {
                return this.istyleGalleryItem_1;
            }
            set
            {
                this.istyleGalleryItem_1 = value;
            }
        }

        public ILayer Layer
        {
            get
            {
                return this.ilayer_0;
            }
            set
            {
                this.ilayer_0 = value;
                this.ilegendItem_0 = this.method_0();
            }
        }

        public ILegendItem LegendItem
        {
            get
            {
                return this.ilegendItem_0;
            }
        }

        public IStyleGalleryItem LinePatchStyleGalleryItem
        {
            get
            {
                return this.istyleGalleryItem_0;
            }
            set
            {
                this.istyleGalleryItem_0 = value;
            }
        }
    }
}

