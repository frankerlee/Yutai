using ESRI.ArcGIS.esriSystem;
using System;
using System.Drawing;
using System.IO;

namespace Yutai.Catalog
{
	public class GxOfficeFile : IGxObject, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxFileSetup, IGxOfficeFile
	{
		private string string_0 = "";

		private string string_1 = "";

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		public string BaseName
		{
			get
			{
				return this.string_0;
			}
		}

		public string Category
		{
			get
			{
				return this.string_1;
			}
		}

		public UID ClassID
		{
			get
			{
				return null;
			}
		}

		public UID ContextMenu
		{
			get
			{
				return null;
			}
		}

		public string FullName
		{
			get
			{
				return this.string_0;
			}
		}

		public IName InternalObjectName
		{
			get
			{
                IFileName fileName = new FileName() as IFileName;
                fileName.Path = this.string_0;
                return fileName as IName; 
			}
		}

		public bool IsValid
		{
			get
			{
				return this.string_0.Trim().Length > 0;
			}
		}

		string Yutai.Catalog.IGxFileSetup.Category
		{
			set
			{
			}
		}

		IName Yutai.Catalog.IGxObjectInternalName.InternalObjectName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public Bitmap LargeImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (this.string_1 != "WORD" ? ImageLib.GetSmallImage(49) : ImageLib.GetSmallImage(48));
				return bitmap;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (this.string_1 != "WORD" ? ImageLib.GetSmallImage(49) : ImageLib.GetSmallImage(48));
				return bitmap;
			}
		}

		public string Name
		{
			get
			{
				string str;
				str = (this.string_0.Length != 0 ? System.IO.Path.GetFileName(this.string_0) : "");
				return str;
			}
		}

		public UID NewMenu
		{
			get
			{
				return null;
			}
		}

		public IGxObject Parent
		{
			get
			{
				return this.igxObject_0;
			}
		}

		public string Path
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
				string lower = System.IO.Path.GetExtension(this.string_0).ToLower();
				if (lower == ".doc")
				{
					this.string_1 = "WORD";
				}
				else if (lower == ".xls")
				{
					this.string_1 = "EXCEL";
				}
			}
		}

		public int PropertyCount
		{
			get
			{
				return 0;
			}
		}

		public Bitmap SmallImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (this.string_1 != "WORD" ? ImageLib.GetSmallImage(49) : ImageLib.GetSmallImage(48));
				return bitmap;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (this.string_1 != "WORD" ? ImageLib.GetSmallImage(49) : ImageLib.GetSmallImage(48));
				return bitmap;
			}
		}

		public GxOfficeFile()
		{
		}

		public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
		{
			this.igxObject_0 = igxObject_1;
			this.igxCatalog_0 = igxCatalog_1;
			if (this.igxObject_0 is IGxObjectContainer)
			{
				(this.igxObject_0 as IGxObjectContainer).AddChild(this);
			}
		}

		public bool CanCopy()
		{
			return false;
		}

		public bool CanDelete()
		{
			return false;
		}

		public bool CanRename()
		{
			return false;
		}

		public void Close(bool bool_0)
		{
		}

		public void Delete()
		{
		}

		public void Detach()
		{
			if (this.igxCatalog_0 != null)
			{
				this.igxCatalog_0.ObjectDeleted(this);
			}
			if (this.igxObject_0 is IGxObjectContainer)
			{
				(this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
			}
			this.igxObject_0 = null;
			this.igxCatalog_0 = null;
		}

		public void Edit()
		{
		}

		public void EditProperties(int int_0)
		{
		}

		public void GetPropByIndex(int int_0, ref string string_2, ref object object_0)
		{
		}

		public object GetProperty(string string_2)
		{
			return null;
		}

		public void New()
		{
		}

		public void Open()
		{
		}

		public void Refresh()
		{
		}

		public void Rename(string string_2)
		{
		}

		public void Save()
		{
		}

		public void SetImages(int int_0, int int_1, int int_2, int int_3)
		{
		}

		public void SetProperty(string string_2, object object_0)
		{
		}
	}
}