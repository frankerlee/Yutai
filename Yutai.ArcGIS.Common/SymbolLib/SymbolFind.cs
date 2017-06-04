using ESRI.ArcGIS.Display;

namespace Yutai.ArcGIS.Common.SymbolLib
{
	internal class SymbolFind
	{
		public static IStyleGalleryItem FindStyleGalleryItem(string string_0, IStyleGallery istyleGallery_0, string string_1, string string_2, string string_3)
		{
			IStyleGalleryItem result;
			if (istyleGallery_0 == null)
			{
				result = null;
			}
			else
			{
				try
				{
					IEnumStyleGalleryItem enumStyleGalleryItem = istyleGallery_0.get_Items(string_2, string_1, string_3);
					enumStyleGalleryItem.Reset();
					for (IStyleGalleryItem styleGalleryItem = enumStyleGalleryItem.Next(); styleGalleryItem != null; styleGalleryItem = enumStyleGalleryItem.Next())
					{
						if (styleGalleryItem.Name.ToUpper() == string_0.ToUpper())
						{
							result = styleGalleryItem;
							return result;
						}
					}
				}
				catch
				{
				}
				System.GC.Collect();
				result = null;
			}
			return result;
		}

		public static IStyleGalleryItem FindStyleGalleryItem(int int_0, IStyleGallery istyleGallery_0, string string_0, string string_1, string string_2)
		{
			IStyleGalleryItem result;
			if (istyleGallery_0 == null)
			{
				result = null;
			}
			else if (string_0 == "")
			{
				result = null;
			}
			else
			{
				try
				{
					IEnumStyleGalleryItem enumStyleGalleryItem = istyleGallery_0.get_Items(string_1, string_0, string_2);
					enumStyleGalleryItem.Reset();
					for (IStyleGalleryItem styleGalleryItem = enumStyleGalleryItem.Next(); styleGalleryItem != null; styleGalleryItem = enumStyleGalleryItem.Next())
					{
						if (styleGalleryItem.ID == int_0)
						{
							result = styleGalleryItem;
							return result;
						}
					}
				}
				catch
				{
				}
				System.GC.Collect();
				result = null;
			}
			return result;
		}
	}
}
