using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxServersFolder : IGxObject, IGxObjectContainer, IGxObjectUI, IGxContextMenuWap, IGxRemoteContainer, IGxServersFolder
	{
		private string string_0 = "";

		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		public IAGSServerConnection2 AGSServerConnection
		{
			get;
			set;
		}

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
				return this.FolderName;
			}
		}

		public string Category
		{
			get
			{
				return "ArcGIS Server文件夹";
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				if (this.igxObjectArray_0.Count == 0)
				{
					this.method_0();
				}
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

		public string FolderName
		{
			get;
			set;
		}

		public string FullName
		{
			get
			{
				return this.FolderName;
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
				return ImageLib.GetSmallImage(6);
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				return ImageLib.GetSmallImage(7);
			}
		}

		public string Name
		{
			get
			{
				return this.FolderName;
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
				return ImageLib.GetSmallImage(6);
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				return ImageLib.GetSmallImage(7);
			}
		}

		public GxServersFolder()
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

		public void Init(object object_0)
		{
			this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
			this.ipopuMenuWrap_0.Clear();
			this.ipopuMenuWrap_0.AddItem("RefreshItem", false);
		}

		private void method_0()
		{
			IGxObject gxAGSMap;
			bool flag;
			try
			{
				if (this.AGSServerConnection != null)
				{
					IAGSEnumServerObjectName serverObjectNamesEx = this.AGSServerConnection.ServerObjectNamesEx[this.FolderName];
					serverObjectNamesEx.Reset();
					for (IAGSServerObjectName i = serverObjectNamesEx.Next(); i != null; i = serverObjectNamesEx.Next())
					{
						if (i.Type.ToLower() == "mapserver")
						{
							gxAGSMap = new GxAGSMap();
							(gxAGSMap as IGxAGSObject).AGSServerObjectName = i;
							flag = true;
							if ((this.Parent as IGxAGSConnection).ConnectionMode == 0 && (gxAGSMap as IGxAGSObject).Status != "Started")
							{
								flag = false;
							}
							if (flag)
							{
								gxAGSMap.Attach(this, this.igxCatalog_0);
							}
						}
						else if (i.Type.ToLower() == "featureserver")
						{
							if ((this.Parent as IGxAGSConnection).ConnectionMode == 0)
							{
								gxAGSMap = new GxFeatureService();
								(gxAGSMap as IGxAGSObject).AGSServerObjectName = i;
								if ((gxAGSMap as IGxAGSObject).Status == "Started")
								{
									gxAGSMap.Attach(this, this.igxCatalog_0);
								}
							}
						}
						else if (i.Type.ToLower() == "gpserver")
						{
							gxAGSMap = new GxGPServer();
							(gxAGSMap as IGxAGSObject).AGSServerObjectName = i;
							flag = true;
							if ((this.Parent as IGxAGSConnection).ConnectionMode == 0 && (gxAGSMap as IGxAGSObject).Status != "Started")
							{
								flag = false;
							}
							if (flag)
							{
								gxAGSMap.Attach(this, this.igxCatalog_0);
							}
						}
						else if (i.Type.ToLower() == "geometryserver")
						{
							if ((this.Parent as IGxAGSConnection).ConnectionMode > 0)
							{
								gxAGSMap = new GxGeometryServer();
								(gxAGSMap as IGxAGSObject).AGSServerObjectName = i;
								gxAGSMap.Attach(this, this.igxCatalog_0);
							}
						}
						else if (i.Type.ToLower() == "searchserver")
						{
							gxAGSMap = new GxSearchServer();
							(gxAGSMap as IGxAGSObject).AGSServerObjectName = i;
							flag = true;
							if ((this.Parent as IGxAGSConnection).ConnectionMode == 0 && (gxAGSMap as IGxAGSObject).Status != "Started")
							{
								flag = false;
							}
							if (flag)
							{
								gxAGSMap.Attach(this, this.igxCatalog_0);
							}
						}
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(exception.Message);
				//CErrorLog.writeErrorLog(this, exception, "");
			}
		}

		public void Refresh()
		{
			this.igxObjectArray_0.Empty();
			this.method_0();
			this.igxCatalog_0.ObjectRefreshed(this);
		}

		public void SearchChildren(string string_2, IGxObjectArray igxObjectArray_1)
		{
		}

		public override string ToString()
		{
			return "GxServersFolder";
		}
	}
}