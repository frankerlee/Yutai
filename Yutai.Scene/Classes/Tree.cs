using System.IO;
using ESRI.ArcGIS.Display;

namespace Yutai.Plugins.Scene.Classes
{
	internal class Tree
	{
		private string string_0 = "";

		private string string_1 = "";

		private IPictureFillSymbol ipictureFillSymbol_0 = null;

		private System.Drawing.Bitmap bitmap_0 = null;

		public string Name
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
			}
		}

		public System.Drawing.Bitmap TreeBitmap
		{
			get
			{
				if (this.bitmap_0 == null)
				{
					this.bitmap_0 = (System.Drawing.Image.FromFile(this.string_1) as System.Drawing.Bitmap);
				}
				return this.bitmap_0;
			}
		}

		public IPictureFillSymbol Symbol
		{
			get
			{
				if (this.ipictureFillSymbol_0 == null)
				{
					IPictureFillSymbol pictureFillSymbol = new PictureFillSymbol();
					pictureFillSymbol.CreateFillSymbolFromFile(esriIPictureType.esriIPictureBitmap, this.string_1);
					pictureFillSymbol.BitmapTransparencyColor = new RgbColor
					{
						RGB = 0
					};
					this.ipictureFillSymbol_0 = pictureFillSymbol;
				}
				return this.ipictureFillSymbol_0;
			}
		}

		public Tree(string string_2)
		{
			this.string_0 = Path.GetFileNameWithoutExtension(string_2);
			this.string_1 = string_2;
		}

		public override string ToString()
		{
			return this.string_0;
		}
	}
}
