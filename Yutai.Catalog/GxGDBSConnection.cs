using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;

using System;
using System.Drawing;
using System.IO;

namespace Yutai.Catalog
{
	public class GxGDBSConnection : IGxObject, IGxObjectContainer, IGxObjectProperties, IGxObjectUI, IGxContextMenuWap, IGxGDSConnection
	{
		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private string string_0 = "";

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		private IDataServerManager idataServerManager_0 = new DataServerManager();

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
				return this.string_0;
			}
		}

		public string Category
		{
			get
			{
				return "Database Server ";
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

		public object DataServerManager
		{
			get
			{
				return this.idataServerManager_0;
			}
		}

		public string FullName
		{
			get
			{
				return this.string_0;
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

		public bool IsAdministrator
		{
			get
			{
				return false;
			}
		}

		public bool IsConnected
		{
			get
			{
				return this.idataServerManager_0.IsConnected;
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
				Bitmap bitmap;
				bitmap = (!this.IsConnected ? ImageLib.GetSmallImage(77) : ImageLib.GetSmallImage(76));
				return bitmap;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!this.IsConnected ? ImageLib.GetSmallImage(77) : ImageLib.GetSmallImage(76));
				return bitmap;
			}
		}

		public string Name
		{
			get
			{
				return Path.GetFileNameWithoutExtension(this.string_0);
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

		public int PropertyCount
		{
			get
			{
				return 0;
			}
		}

		public string ServerName
		{
			get
			{
				return this.idataServerManager_0.ServerName;
			}
		}

		public Bitmap SmallImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!this.IsConnected ? ImageLib.GetSmallImage(77) : ImageLib.GetSmallImage(76));
				return bitmap;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!this.IsConnected ? ImageLib.GetSmallImage(77) : ImageLib.GetSmallImage(76));
				return bitmap;
			}
		}

		public GxGDBSConnection()
		{
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
		}

		public void AttachGeoDatabase(string string_1, string string_2)
		{
			(this.idataServerManager_0 as IDataServerManagerAdmin).AttachGeodatabase(string_1, string_2, "");
		}

		public void Connect()
		{
			this.idataServerManager_0.Connect();
			IEnumBSTR geodatabaseNames = (this.idataServerManager_0 as IDataServerManagerAdmin).GetGeodatabaseNames();
			geodatabaseNames.Reset();
			for (string i = geodatabaseNames.Next(); i != null; i = geodatabaseNames.Next())
			{
				(new GxGDSGeodatabase(i, this.idataServerManager_0)).Attach(this, this.igxCatalog_0);
			}
		}

		public void CreateGeoDatabase(string string_1, string string_2, int int_0)
		{
			(this.idataServerManager_0 as IDataServerManagerAdmin).CreateGeodatabase(string_1, string_2, int_0, "", 0);
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

		public void Disconnect()
		{
			this.idataServerManager_0.Disconnect();
		}

		public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
		{
		}

		public object GetProperty(string string_1)
		{
			return null;
		}

		public void Init(object object_0)
		{
			this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
			this.ipopuMenuWrap_0.Clear();
			this.ipopuMenuWrap_0.AddItem("Connection", true);
			this.ipopuMenuWrap_0.AddItem("DisConnection", false);
		}

		public void LoadFromFile(string string_1)
		{
			this.string_0 = string_1;
			this.idataServerManager_0.InitFromFile(string_1);
		}

		public void Pause()
		{
			(this.idataServerManager_0 as IServiceControl).PauseServer();
		}

		public void Refresh()
		{
			if (this.igxCatalog_0 != null)
			{
				this.igxObjectArray_0.Empty();
				this.igxCatalog_0.ObjectRefreshed(this);
			}
		}

		public void RestoreGeodatabase(string string_1, string string_2, string string_3)
		{
			(this.idataServerManager_0 as IDataServerManagerAdmin).RestoreGeodatabase(string_1, string_2, string_3);
		}

		public void Resume()
		{
			(this.idataServerManager_0 as IServiceControl).ContinueServer();
		}

		public void SearchChildren(string string_1, IGxObjectArray igxObjectArray_1)
		{
		}

		public void SetProperty(string string_1, object object_0)
		{
		}

		public void Start()
		{
			(this.idataServerManager_0 as IServiceControl).StartServer();
		}

		public void Stop()
		{
			(this.idataServerManager_0 as IServiceControl).StopServer();
		}
	}
}