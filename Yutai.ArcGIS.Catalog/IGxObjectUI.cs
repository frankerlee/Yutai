using System.Drawing;

namespace Yutai.ArcGIS.Catalog
{
    public interface IGxObjectUI
    {
        Bitmap LargeImage { get; }

        Bitmap LargeSelectedImage { get; }

        Bitmap SmallImage { get; }

        Bitmap SmallSelectedImage { get; }
    }
}