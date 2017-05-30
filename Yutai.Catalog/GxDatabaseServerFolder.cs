using ESRI.ArcGIS.esriSystem;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxDatabaseServerFolder : IGxObject, IGxObjectContainer, IGxObjectUI
	{
		private string string_0 = "";

		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		public bool AreChildrenViewable
		{
			get
			{
				return true;
			}
		}

		public string BaseName
		{
			get
			{
				return "空间数据库服务器";
			}
		}

		public string Category
		{
			get
			{
				return "空间数据库服务器文件夹";
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				return this.igxObjectArray_0 as IEnumGxObject;
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
				return "空间数据库服务器";
			}
		}

		public bool HasChildren
		{
			get
			{
				return true;
			}
		}

		public IName InternalObjectName
		{
			get
			{
				return null;
			}
		}

		public bool IsValid
		{
			get
			{
				return false;
			}
		}

		public Bitmap LargeImage
		{
			get
			{
				return ImageLib.GetSmallImage(74);
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				return ImageLib.GetSmallImage(74);
			}
		}

		public string Name
		{
			get
			{
				return "空间数据库服务器";
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
			}
		}

		public Bitmap SmallImage
		{
			get
			{
				return ImageLib.GetSmallImage(74);
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				return ImageLib.GetSmallImage(74);
			}
		}

		public GxDatabaseServerFolder()
		{
			this.string_0 = string.Concat(Environment.SystemDirectory.Substring(0, 2), "\\Documents and Settings\\Administrator\\Application Data\\ESRI\\ArcCatalog");
			if (!Directory.Exists(this.string_0))
			{
				try
				{
					Directory.CreateDirectory(this.string_0);
				}
				catch
				{
				}
			}
		}

		public IGxObject AddChild(IGxObject igxObject_1)
		{
			this.igxObjectArray_0.Insert(-1, igxObject_1);
			return igxObject_1;
		}

		public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
		{
			this.igxObject_0 = igxObject_1;
			this.igxCatalog_0 = igxCatalog_1;
			if (this.igxObject_0 is IGxObjectContainer)
			{
				(this.igxObject_0 as IGxObjectContainer).AddChild(this);
			}
			if (this.igxObjectArray_0.Count == 0)
			{
				this.method_0();
			}
		}

		public void DeleteChild(IGxObject igxObject_1)
		{
			int num = 0;
			while (true)
			{
				if (num >= this.igxObjectArray_0.Count)
				{
					break;
				}
				else if (this.igxObjectArray_0.Item(num) == igxObject_1)
				{
					this.igxObjectArray_0.Remove(num);
					break;
				}
				else
				{
					num++;
				}
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

		private void method_0()
		{
			this.igxObjectArray_0.Empty();
			IGxObject gxAddGDBSConnection = new GxAddGDBSConnection();
			gxAddGDBSConnection.Attach(this, this.igxCatalog_0);
			if (Directory.Exists(this.string_0))
			{
				string[] files = Directory.GetFiles(this.string_0, "*.gds");
				for (int i = 0; i < (int)files.Length; i++)
				{
					string str = files[i];
					try
					{
						gxAddGDBSConnection = new GxGDBSConnection();
						(gxAddGDBSConnection as IGxGDSConnection).LoadFromFile(str);
						gxAddGDBSConnection.Attach(this, this.igxCatalog_0);
						this.igxCatalog_0.ObjectRefreshed(this);
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.ToString());
					}
				}
			}
		}

		public void Refresh()
		{
			this.igxObjectArray_0.Empty();
			this.method_0();
		}

		public void SearchChildren(string string_1, IGxObjectArray igxObjectArray_1)
		{
		}

		public override string ToString()
		{
			return "GxDatabaseServerFolder";
		}
	}
}