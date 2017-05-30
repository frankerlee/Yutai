using ESRI.ArcGIS.esriSystem;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxFile : IGxObject, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxFileSetup
	{
		private string string_0 = "";

		private IGxObject igxObject_0;

		public string BaseName
		{
			get
			{
				return null;
			}
		}

		public string Category
		{
			set
			{
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
				return null;
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
				return false;
			}
		}

		string Yutai.Catalog.IGxObject.Category
		{
			get
			{
				return null;
			}
		}

		IName Yutai.Catalog.IGxObjectInternalName.InternalObjectName
		{
			get
			{
                IFileName fileName = new FileName() as IFileName;
                fileName.Path = this.string_0;
                return (IName)fileName;
            }
			set
			{
			}
		}

		public Bitmap LargeImage
		{
			get
			{
				return null;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				return null;
			}
		}

		public string Name
		{
			get
			{
				return null;
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
				return null;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				return null;
			}
		}

		public GxFile()
		{
		}

		public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_0)
		{
			this.igxObject_0 = igxObject_1;
		}

		public bool CanCopy()
		{
			return true;
		}

		public bool CanDelete()
		{
			return true;
		}

		public bool CanRename()
		{
			return true;
		}

		public void Close(bool bool_0)
		{
		}

		public void Delete()
		{
			try
			{
				File.Delete(this.string_0);
				this.Detach();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		public void Detach()
		{
		}

		public void Edit()
		{
		}

		public void EditProperties(int int_0)
		{
		}

		public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
		{
		}

		public object GetProperty(string string_1)
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

		public void Rename(string string_1)
		{
		}

		public void Save()
		{
		}

		public void SetImages(int int_0, int int_1, int int_2, int int_3)
		{
		}

		public void SetProperty(string string_1, object object_0)
		{
		}

		public override string ToString()
		{
			return this.FullName;
		}
	}
}