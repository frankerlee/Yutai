using System.Collections.Generic;
using ESRI.ArcGIS.Display;

namespace Yutai.Plugins.Scene.Classes
{
	internal class clsTextureGroup
	{
		public static clsTextureGroup g_pDefTextureGrp;

		private List<string> list_0 = new List<string>();

		public string name;

		public bool Selected;

		private List<ISymbol> list_1 = new List<ISymbol>();

		private List<bool> list_2 = new List<bool>();

		private List<double> list_3 = new List<double>();

		private int int_0 = System.Drawing.Color.Brown.ToArgb();

		private ISymbol isymbol_0 = null;

		public List<string> TexturePaths
		{
			get
			{
				return this.list_0;
			}
		}

		public List<ISymbol> Symbols
		{
			get
			{
				return this.list_1;
			}
		}

		public List<bool> SymbolIsDirty
		{
			get
			{
				return this.list_2;
			}
		}

		public List<double> AspectRatios
		{
			get
			{
				return this.list_3;
			}
		}

		public int RoofColorRGB
		{
			get
			{
				return this.int_0;
			}
			set
			{
				this.int_0 = value;
			}
		}

		public ISymbol RoofSymbol
		{
			get
			{
				return this.isymbol_0;
			}
			set
			{
				this.isymbol_0 = value;
			}
		}

		public override string ToString()
		{
			return this.name;
		}

		public void Init()
		{
			for (int i = 0; i < this.TexturePaths.Count; i++)
			{
				bool flag = this.Symbols.Count <= i || this.AspectRatios.Count <= i || this.SymbolIsDirty.Count <= i || this.SymbolIsDirty[i] || this.SymbolIsDirty[i];
				if (flag)
				{
					IPictureFillSymbol pictureFillSymbol = new PictureFillSymbol();
					if (this.TexturePaths[i].IndexOf('[') == 0)
					{
						string text = this.TexturePaths[i].Substring(1, this.TexturePaths[i].Length - 2);
						text = System.Windows.Forms.Application.StartupPath + "\\" + text + ".bmp";
						pictureFillSymbol.CreateFillSymbolFromFile(esriIPictureType.esriIPictureBitmap, text);
					}
					else
					{
						pictureFillSymbol.CreateFillSymbolFromFile(esriIPictureType.esriIPictureBitmap, this.TexturePaths[i]);
					}
					this.Symbols.Add(pictureFillSymbol as ISymbol);
					double num = (double)pictureFillSymbol.Picture.Height;
					double num2 = (double)pictureFillSymbol.Picture.Width;
					double item = num / num2;
					this.AspectRatios.Add(item);
				}
			}
			this.RoofSymbol = (new SimpleFillSymbol
			{
				Color = new RgbColor
				{
					RGB = this.RoofColorRGB
				}
			} as ISymbol);
			for (int j = 0; j < this.Symbols.Count; j++)
			{
				this.SymbolIsDirty.Add(false);
			}
		}
	}
}
