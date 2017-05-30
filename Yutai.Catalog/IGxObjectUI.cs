using System.Drawing;

namespace Yutai.Catalog
{
	public interface IGxObjectUI
	{
		Bitmap LargeImage
		{
			get;
		}

		Bitmap LargeSelectedImage
		{
			get;
		}

		Bitmap SmallImage
		{
			get;
		}

		Bitmap SmallSelectedImage
		{
			get;
		}
	}
}