using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxMap : IGxObject, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxMap, IGxMapPageLayout
	{
		private string string_0 = "";

		private string string_1 = "";

		private IPageLayout ipageLayout_0 = null;

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		public string BaseName
		{
			get
			{
				return System.IO.Path.GetFileNameWithoutExtension(this.string_0);
			}
		}

		public string Category
		{
			get
			{
				return (this.string_1 != ".mxd" ? "发布的地图" : "地图文档");
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
				return false;
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
				return ImageLib.GetSmallImage(27);
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				return ImageLib.GetSmallImage(27);
			}
		}

		public string Name
		{
			get
			{
				return System.IO.Path.GetFileName(this.string_0);
			}
		}

		public UID NewMenu
		{
			get
			{
				return null;
			}
		}

		public IPageLayout PageLayout
		{
			get
			{
				if (this.ipageLayout_0 == null)
				{
					IMapDocument mapDocumentClass = new MapDocument();
					mapDocumentClass.Open(this.string_0, "");
					this.ipageLayout_0 = mapDocumentClass.PageLayout;
					mapDocumentClass.Close();
				}
				return this.ipageLayout_0;
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
				this.string_1 = System.IO.Path.GetExtension(this.string_0).ToLower();
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
				bitmap = (this.string_1 != ".mxd" ? ImageLib.GetSmallImage(28) : ImageLib.GetSmallImage(27));
				return bitmap;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (this.string_1 != ".mxd" ? ImageLib.GetSmallImage(28) : ImageLib.GetSmallImage(27));
				return bitmap;
			}
		}

		public GxMap()
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
			if (string_2 != null)
			{
				System.IO.Path.GetExtension(string_2);
				if (System.IO.Path.GetFileNameWithoutExtension(string_2).Trim().Length != 0)
				{
					string str = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.string_0), string_2);
					if (!File.Exists(str))
					{
						try
						{
							File.Copy(this.string_0, str);
							this.string_0 = str;
							File.Delete(this.string_0);
						}
						catch
						{
						}
					}
					else
					{
						MessageBox.Show("已存在同名文件，请重新指定其他名字");
					}
				}
				else
				{
					MessageBox.Show("必须键入文件名!");
					this.igxCatalog_0.ObjectChanged(this);
				}
			}
		}

		public void Save()
		{
		}

		public void SetProperty(string string_2, object object_0)
		{
		}

		public override string ToString()
		{
			return this.FullName;
		}
	}
}