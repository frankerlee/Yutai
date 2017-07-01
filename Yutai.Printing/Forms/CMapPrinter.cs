using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Controls.Controls;

namespace Yutai.Plugins.Printing.Forms
{
    public class CMapPrinter
    {
        private IMap imap_0;

        private IStyleGallery istyleGallery_0 = null;

        private IGeometry igeometry_0 = null;

        public IGeometry ClipGeometry
        {
            set { this.igeometry_0 = value; }
        }

        public CMapPrinter(IMap imap_1)
        {
            this.imap_0 = imap_1;
        }

        public CMapPrinter(IMap imap_1, IStyleGallery istyleGallery_1)
        {
            this.imap_0 = imap_1;
            this.istyleGallery_0 = istyleGallery_1;
        }

        public void showPrintUI(string string_0)
        {
            new FormMapPrinter
            {
                SourcesMap = this.imap_0,
                ClipGeometry = this.igeometry_0,
                StyleGallery = this.istyleGallery_0,
                Text = string_0
            }.ShowDialog();
        }

        private void method_0(IMap imap_1, IMap imap_2)
        {
            IObjectCopy objectCopy = new ObjectCopy();
            object pInObject = objectCopy.Copy(imap_1);
            object obj = imap_2;
            objectCopy.Overwrite(pInObject, ref obj);
        }
    }
}