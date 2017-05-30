using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.Server;
using Yutai.Catalog.UI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxAGSMap : IGxObject, IGxAGSMap, IGxObjectEdit, IGxObjectProperties, IGxObjectUI, IGxContextMenuWap, IGxAGSObject, IGxLayerSource
	{
		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private IAGSServerObjectName iagsserverObjectName_0 = null;

		private IAGSServerConnection iagsserverConnection_0 = null;

		private IServerObjectConfiguration iserverObjectConfiguration_0 = null;

		private int int_0 = -1;

		private int int_1 = 0;

		private int int_2 = 0;

		private string string_0 = "";

		public IAGSServerObjectName AGSServerObjectName
		{
			get
			{
				return this.iagsserverObjectName_0;
			}
			set
			{
				this.iagsserverObjectName_0 = value;
				this.iagsserverConnection_0 = (this.iagsserverObjectName_0.AGSServerConnectionName as IName).Open() as IAGSServerConnection;
				this.int_0 = Convert.ToInt32(this.iagsserverObjectName_0.AGSServerConnectionName.ConnectionProperties.GetProperty("CONNECTIONMODE"));
			}
		}

		public string BaseName
		{
			get
			{
				return this.Name;
			}
		}

		public string Category
		{
			get
			{
				string str;
				str = (this.iagsserverObjectName_0 == null ? "MapServer" : this.iagsserverObjectName_0.Type);
				return str;
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

		public string DefaultMapName
		{
			get
			{
				if (this.string_0.Length == 0 && this.iagsserverObjectName_0 != null)
				{
					IMapServer mapServer = (this.iagsserverObjectName_0 as IName).Open() as IAGSServerObject as IMapServer;
					if (mapServer != null)
					{
						this.string_0 = mapServer.DefaultMapName;
					}
				}
				return this.string_0;
			}
		}

		public string FullName
		{
			get
			{
				string str;
				str = (this.iagsserverObjectName_0 == null ? "" : this.iagsserverObjectName_0.Name);
				return str;
			}
		}

		public IName InternalObjectName
		{
			get
			{
				return this.iagsserverObjectName_0 as IName;
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
				Bitmap smallImage;
				if (this.Status == "Started")
				{
					smallImage = ImageLib.GetSmallImage(53);
				}
				else if (this.Status != "Stopped")
				{
					smallImage = (this.Status != "Paused" ? ImageLib.GetSmallImage(53) : ImageLib.GetSmallImage(55));
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(54);
				}
				return smallImage;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				Bitmap smallImage;
				if (this.Status == "Started")
				{
					smallImage = ImageLib.GetSmallImage(53);
				}
				else if (this.Status != "Stopped")
				{
					smallImage = (this.Status != "Paused" ? ImageLib.GetSmallImage(53) : ImageLib.GetSmallImage(55));
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(54);
				}
				return smallImage;
			}
		}

		public string Name
		{
			get
			{
				string str;
				if (this.iagsserverObjectName_0 == null)
				{
					str = "";
				}
				else
				{
					string name = this.iagsserverObjectName_0.Name;
					string[] strArrays = name.Split(new char[] { '/' });
					str = strArrays[(int)strArrays.Length - 1];
				}
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

		public int NumInstancesInUse
		{
			get
			{
				IServerObjectConfigurationStatus configurationStatus = (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.GetConfigurationStatus(this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type);
				return configurationStatus.InstanceInUseCount;
			}
		}

		public int NumInstancesRunning
		{
			get
			{
				IServerObjectConfigurationStatus configurationStatus = (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.GetConfigurationStatus(this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type);
				return configurationStatus.InstanceCount;
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

		public Bitmap SmallImage
		{
			get
			{
				Bitmap smallImage;
				if (this.Status == "Started")
				{
					smallImage = ImageLib.GetSmallImage(53);
				}
				else if (this.Status != "Stopped")
				{
					smallImage = (this.Status != "Paused" ? ImageLib.GetSmallImage(53) : ImageLib.GetSmallImage(55));
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(54);
				}
				return smallImage;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap smallImage;
				if (this.Status == "Started")
				{
					smallImage = ImageLib.GetSmallImage(53);
				}
				else if (this.Status != "Stopped")
				{
					smallImage = (this.Status != "Paused" ? ImageLib.GetSmallImage(53) : ImageLib.GetSmallImage(55));
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(54);
				}
				return smallImage;
			}
		}

		public string Status
		{
			get
			{
				string str;
				try
				{
					IServerObjectConfigurationStatus configurationStatus = (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.GetConfigurationStatus(this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type);
					if (configurationStatus.Status == esriConfigurationStatus.esriCSStarted)
					{
						str = "Started";
					}
					else if (configurationStatus.Status == esriConfigurationStatus.esriCSStarting)
					{
						str = "Starting";
					}
					else if (configurationStatus.Status == esriConfigurationStatus.esriCSStopped)
					{
						str = "Stopped";
					}
					else if (configurationStatus.Status == esriConfigurationStatus.esriCSStopping)
					{
						str = "Stopping";
					}
					else if (configurationStatus.Status == esriConfigurationStatus.esriCSPaused)
					{
						str = "Paused";
					}
					else if (configurationStatus.Status != esriConfigurationStatus.esriCSDeleted)
					{
						str = "Started";
						return str;
					}
					else
					{
						str = "Deleted";
					}
				}
				catch (Exception exception)
				{
					str = "Started";
					return str;
				}
				return str;
			}
		}

		public GxAGSMap()
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
			return true;
		}

		public bool CanRename()
		{
			return false;
		}

		public void Delete()
		{
			try
			{
				string name = this.iagsserverObjectName_0.Name;
				string type = this.iagsserverObjectName_0.Type;
				(this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectAdmin.DeleteConfiguration(name, type);
				this.Detach();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(exception.Message);
				//CErrorLog.writeErrorLog(this, exception, "");
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

		public void EditProperties(int int_3)
		{
		}

		public void EditServerObjectProperties(int int_3)
		{
			frmServerObjectPropertySheet _frmServerObjectPropertySheet = new frmServerObjectPropertySheet();
			this.iserverObjectConfiguration_0 = (this.iagsserverConnection_0 as IAGSServerConnectionAdmin).ServerObjectConfiguration[this.iagsserverObjectName_0.Name, this.iagsserverObjectName_0.Type];
			_frmServerObjectPropertySheet.AGSConnectionAdmin = this.iagsserverConnection_0 as IAGSServerConnectionAdmin;
			_frmServerObjectPropertySheet.ServerObjectConfig = this.iserverObjectConfiguration_0;
			_frmServerObjectPropertySheet.Status = this.Status;
			_frmServerObjectPropertySheet.ShowDialog();
			this.iserverObjectConfiguration_0 = null;
		}

		public void GetPropByIndex(int int_3, ref string string_1, ref object object_0)
		{
		}

		public object GetProperty(string string_1)
		{
			return null;
		}

		public void Init(object object_0)
		{
			IPopuMenuWrap object0 = null;
			object0 = object_0 as IPopuMenuWrap;
			object0.Clear();
			if (this.int_0 > 0)
			{
				object0.AddItem("ServerStartCommand", true);
				object0.AddItem("ServerStopCommand", true);
				object0.AddItem("ServicesProperty", true);
			}
			object0.AddItem("GxObjectProperty", true);
		}

		public void Refresh()
		{
			if (this.igxCatalog_0 != null)
			{
				this.igxCatalog_0.ObjectRefreshed(this);
			}
		}

		public void Rename(string string_1)
		{
		}

		public void SetProperty(string string_1, object object_0)
		{
		}
	}
}