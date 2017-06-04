using System.Drawing;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	public interface IStyleDraw
	{
		void Draw(int int_0, Rectangle rectangle_0, double double_0, double double_1);

		Bitmap StyleGalleryItemToBmp(Size size_0, double double_0, double double_1);

		void StyleGalleryItemToBmp(Bitmap bitmap_0, double double_0, double double_1);
	}
}
