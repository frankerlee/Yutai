namespace JLK.Catalog
{
    using System.Drawing;

    public interface IGxObjectUI
    {
        Bitmap LargeImage { get; }

        Bitmap LargeSelectedImage { get; }

        Bitmap SmallImage { get; }

        Bitmap SmallSelectedImage { get; }
    }
}

