using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace Yutai.ArcGIS.Common.Symbol
{
	public class MyStyleGalleryItem : IStyleGalleryItem, IClone
	{
		private object object_0 = null;

		private int int_0 = -1;

		private string string_0 = "";

		private string string_1 = "";

		public object Item
		{
			get
			{
				return this.object_0;
			}
			set
			{
				this.object_0 = value;
			}
		}

		public int ID
		{
			get
			{
				return this.int_0;
			}
		}

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

		public string Category
		{
			get
			{
				return this.string_1;
			}
			set
			{
				this.string_1 = value;
			}
		}

		public int ItemID
		{
			set
			{
				this.int_0 = value;
			}
		}

		public MyStyleGalleryItem()
		{
		}

		public MyStyleGalleryItem(string string_2, string string_3, object object_1)
		{
			this.object_0 = object_1;
			this.string_0 = string_2;
			this.string_1 = string_3;
		}

		public bool IsEqual(IClone iclone_0)
		{
			return false;
		}

		public void Assign(IClone iclone_0)
		{
		}

		public IClone Clone()
		{
			return new MyStyleGalleryItem(this.string_0, this.string_1, this.object_0);
		}

		public bool IsIdentical(IClone iclone_0)
		{
			return false;
		}
	}
}
