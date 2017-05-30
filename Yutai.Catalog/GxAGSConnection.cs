using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;
using Yutai.Catalog.UI;

using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.Shared;

namespace Yutai.Catalog
{
    public interface StorageHepler : IStorageHepler
    {

    }

    public class StorageHeplerClass : IStorageHepler, StorageHepler
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern StorageHeplerClass();

        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern object OpenAGS([In] string FileName, out object pOutUseAGSObjectsNames);

        [DispId(6)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern object OpenStorageStream([In] string FileName, [In] string streamName);

        [DispId(4)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void SaveAGS([In] string FileName, [In] string AGSName, [In] object pAGSObjectNames, [In] object pAGSName);

        [DispId(5)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void SaveLayer([In] string FileName, [In] object pLayer);

        [DispId(7)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void SaveStorageStream([In] string pwcsName, [In] string streamName, [In] object pUnknown);

        [DispId(1)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern object StorageOpenAGS([In] string pwcsName);

        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void StorageSaveAGS([In] string pwcsName, [In] string Name, [In] object pUnknown);
    }
    public interface IStorageHepler
    {
        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        object OpenAGS([In] string FileName, out object pOutUseAGSObjectsNames);

        [DispId(6)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        object OpenStorageStream([In] string FileName, [In] string streamName);

        [DispId(4)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SaveAGS([In] string FileName, [In] string AGSName, [In] object pAGSObjectNames, [In] object pAGSName);

        [DispId(5)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SaveLayer([In] string FileName, [In] object pLayer);

        [DispId(7)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void SaveStorageStream([In] string pwcsName, [In] string streamName, [In] object pUnknown);

        [DispId(1)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        object StorageOpenAGS([In] string pwcsName);

        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void StorageSaveAGS([In] string pwcsName, [In] string Name, [In] object pUnknown);
    }
    public class GxAGSConnection : IGxObject, IGxAGSConnection, IGxObjectContainer, IGxObjectEdit, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxRemoteConnection
	{
		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private object object_0 = null;

		private string string_0 = "";

		private IAGSServerConnectionName iagsserverConnectionName_0 = null;

		private IAGSServerConnection2 iagsserverConnection2_0 = null;

		private IPropertySet ipropertySet_0 = null;

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		public IAGSServerConnectionName AGSServerConnectionName
		{
			get
			{
				if (this.iagsserverConnectionName_0 == null && this.iagsserverConnection2_0 != null)
				{
					this.iagsserverConnectionName_0 = this.iagsserverConnection2_0.FullName as IAGSServerConnectionName;
				}
				return this.iagsserverConnectionName_0;
			}
			set
			{
				this.iagsserverConnectionName_0 = value;
			}
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
				return this.string_0;
			}
		}

		public string Category
		{
			get
			{
				string str;
				if (this.iagsserverConnectionName_0 != null)
				{
					str = (this.iagsserverConnectionName_0.ConnectionType != esriAGSConnectionType.esriAGSConnectionTypeLAN ? string.Concat("ArcGIS Server ", this.iagsserverConnectionName_0.ConnectionProperties.GetProperty("URL").ToString()) : string.Concat("ArcGIS Server ", this.iagsserverConnectionName_0.ConnectionProperties.GetProperty("MACHINE").ToString()));
				}
				else
				{
					str = "ArcGIS Server ";
				}
				return str;
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

		public int ConnectionMode
		{
			get;
			set;
		}

		public UID ContextMenu
		{
			get
			{
				return null;
			}
		}

		public string FileName
		{
			get
			{
				return this.string_0;
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
				return this.iagsserverConnectionName_0 as IName;
			}
		}

		public bool IsConnected
		{
			get
			{
				return this.iagsserverConnection2_0 != null;
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
				bitmap = (!this.IsConnected ? ImageLib.GetSmallImage(13) : ImageLib.GetSmallImage(14));
				return bitmap;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!this.IsConnected ? ImageLib.GetSmallImage(13) : ImageLib.GetSmallImage(14));
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

		public object SelectedServerObjects
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

		public Bitmap SmallImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!this.IsConnected ? ImageLib.GetSmallImage(13) : ImageLib.GetSmallImage(14));
				return bitmap;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (!this.IsConnected ? ImageLib.GetSmallImage(13) : ImageLib.GetSmallImage(14));
				return bitmap;
			}
		}

		public GxAGSConnection()
		{
			this.ConnectionMode = -1;
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

		public bool CanCopy()
		{
			return false;
		}

		public bool CanDelete()
		{
			return true;
		}

		public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
		{
			return false;
		}

		public bool CanRename()
		{
			return true;
		}

		public void Connect()
		{
			try
			{
				IAGSServerConnectionFactory2 aGSServerConnectionFactoryClass = new AGSServerConnectionFactory() as IAGSServerConnectionFactory2;
				if (this.ipropertySet_0 == null)
				{
					this.ipropertySet_0 = aGSServerConnectionFactoryClass.ReadConnectionPropertiesFromFile(this.string_0);
				}
				this.iagsserverConnection2_0 = aGSServerConnectionFactoryClass.Open(this.ipropertySet_0, 0) as IAGSServerConnection2;
				this.Init(null);
				this.method_1();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				MessageBox.Show(exception.Message);
                Logger.Current.Write(exception.Message,LogLevel.Error,null);
				//CErrorLog.writeErrorLog(this, exception, "");
			}
		}

		public void Delete()
		{
			try
			{
				if (this.IsConnected)
				{
					this.Disconnect();
				}
				File.Delete(this.string_0);
				this.Detach();
			}
			catch
			{
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

		public void Disconnect()
		{
			this.igxObjectArray_0.Empty();
			this.Init(null);
			this.iagsserverConnection2_0 = null;
		}

		public void EditProperties(int int_1)
		{
			if (this.ConnectionMode == 0)
			{
				frmArcGISServerUseProperty _frmArcGISServerUseProperty = new frmArcGISServerUseProperty();
				if (this.iagsserverConnectionName_0 == null)
				{
					_frmArcGISServerUseProperty.ConnectionProperties = this.ipropertySet_0;
				}
				else
				{
					_frmArcGISServerUseProperty.AGSServerConnectionName = this.iagsserverConnectionName_0;
				}
				_frmArcGISServerUseProperty.ConnectionFile = this.FileName;
				if (_frmArcGISServerUseProperty.ShowDialog() == DialogResult.OK)
				{
				}
			}
			else if ((this.ConnectionMode == 1 ? true : this.ConnectionMode == 2))
			{
				frmArcGISServerManageProperty _frmArcGISServerManageProperty = new frmArcGISServerManageProperty();
				if (this.iagsserverConnectionName_0 == null)
				{
					_frmArcGISServerManageProperty.ConnectionProperties = this.ipropertySet_0;
				}
				else
				{
					_frmArcGISServerManageProperty.AGSServerConnectionName = this.iagsserverConnectionName_0;
				}
				_frmArcGISServerManageProperty.ConnectionFile = this.FileName;
				_frmArcGISServerManageProperty.ShowDialog();
			}
		}

		public void EditServerProperties(int int_1, short short_0)
		{
			frmAGSProperty _frmAGSProperty = new frmAGSProperty()
			{
				AGSServerConnectionAdmin = this.iagsserverConnection2_0 as IAGSServerConnectionAdmin
			};
			_frmAGSProperty.ShowDialog();
		}

		public void GetPropByIndex(int int_1, ref string string_1, ref object object_1)
		{
		}

		public object GetProperty(string string_1)
		{
			return null;
		}

		public void Init(object object_1)
		{
			if (object_1 != null)
			{
				this.ipopuMenuWrap_0 = object_1 as IPopuMenuWrap;
			}
			this.ipopuMenuWrap_0.Clear();
			this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
			this.ipopuMenuWrap_0.AddItem("ReName", false);
			this.ipopuMenuWrap_0.AddItem("RefreshItem", true);
			this.ipopuMenuWrap_0.AddItem("Connection", true);
			this.ipopuMenuWrap_0.AddItem("DisConnection", false);
			if (this.IsConnected)
			{
				this.ipopuMenuWrap_0.AddItem("ServerProperty", true);
			}
			this.ipopuMenuWrap_0.AddItem("GxObjectProperty", true);
		}

		public void LoadFromFile(string string_1)
		{
			this.string_0 = string_1;
			if (this.IsConnected)
			{
				this.Disconnect();
			}
			if (File.Exists(string_1))
			{
				try
				{
                    IAGSServerConnectionFactory2 factory=new AGSServerConnectionFactory() as IAGSServerConnectionFactory2;

                    this.ipropertySet_0 = factory.ReadConnectionPropertiesFromFile(string_1);
					try
					{
						this.ConnectionMode = Convert.ToInt32(this.ipropertySet_0.GetProperty("CONNECTIONMODE"));
					}
					catch
					{
					}
				}
				catch (Exception exception)
				{
					//CErrorLog.writeErrorLog(this, exception, "");
				}
			}
		}

		private bool method_0(string string_1)
		{
			bool flag;
			if (this.object_0 != null)
			{
				System.Array object0 = (System.Array)this.object_0;
				int num = 0;
				while (num < object0.Length)
				{
					if ((string)object0.GetValue(num) == string_1)
					{
						flag = true;
						return flag;
					}
					else
					{
						num++;
					}
				}
				flag = false;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		private void method_1()
		{
			object obj;
			object obj1;
			IGxObject gxAddAGSObject;
			bool flag;
			try
			{
				this.ipropertySet_0.GetAllProperties(out obj, out obj1);
				string[] strArrays = (string[])obj;
				if (((int)strArrays.Length != 6 ? false : strArrays[1] == "MANAGERURL"))
				{
					gxAddAGSObject = new GxAddAGSObject();
					gxAddAGSObject.Attach(this, this.igxCatalog_0);
				}
				if (this.iagsserverConnection2_0 != null)
				{
					IEnumBSTR folders = this.iagsserverConnection2_0.GetFolders("");
					folders.Reset();
					for (string i = folders.Next(); i != null; i = folders.Next())
					{
						IGxServersFolder gxServersFolder = new GxServersFolder()
						{
							AGSServerConnection = this.iagsserverConnection2_0,
							FolderName = i
						};
						(gxServersFolder as IGxObject).Attach(this, this.igxCatalog_0);
					}
					IAGSEnumServerObjectName serverObjectNamesEx = this.iagsserverConnection2_0.ServerObjectNamesEx[""];
					serverObjectNamesEx.Reset();
					for (IAGSServerObjectName j = serverObjectNamesEx.Next(); j != null; j = serverObjectNamesEx.Next())
					{
						if (j.Type.ToLower() == "mapserver")
						{
							if (this.method_0(j.Name))
							{
								gxAddAGSObject = new GxAGSMap();
								(gxAddAGSObject as IGxAGSObject).AGSServerObjectName = j;
								flag = true;
								if (this.ConnectionMode == 0 && (gxAddAGSObject as IGxAGSObject).Status != "Started")
								{
									flag = false;
								}
								if (flag)
								{
									gxAddAGSObject.Attach(this, this.igxCatalog_0);
								}
							}
						}
						else if (j.Type.ToLower() == "gpserver")
						{
							if (this.method_0(j.Name))
							{
								gxAddAGSObject = new GxGPServer();
								(gxAddAGSObject as IGxAGSObject).AGSServerObjectName = j;
								flag = true;
								if (this.ConnectionMode == 0 && (gxAddAGSObject as IGxAGSObject).Status != "Started")
								{
									flag = false;
								}
								if (flag)
								{
									gxAddAGSObject.Attach(this, this.igxCatalog_0);
								}
							}
						}
						else if (j.Type.ToLower() == "featureserver")
						{
							if (this.ConnectionMode == 0)
							{
								gxAddAGSObject = new GxFeatureService();
								(gxAddAGSObject as IGxAGSObject).AGSServerObjectName = j;
								if ((gxAddAGSObject as IGxAGSObject).Status == "Started")
								{
									gxAddAGSObject.Attach(this, this.igxCatalog_0);
								}
							}
						}
						else if (j.Type.ToLower() == "geometryserver" && this.ConnectionMode > 0)
						{
							gxAddAGSObject = new GxGeometryServer();
							(gxAddAGSObject as IGxAGSObject).AGSServerObjectName = j;
							gxAddAGSObject.Attach(this, this.igxCatalog_0);
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

		public bool Paste(IEnumName ienumName_0, ref bool bool_0)
		{
			return false;
		}

		public void Refresh()
		{
			if (this.igxCatalog_0 != null)
			{
				this.igxObjectArray_0.Empty();
				this.method_1();
				this.igxCatalog_0.ObjectRefreshed(this);
			}
		}

		public void Rename(string string_1)
		{
			string directoryName = Path.GetDirectoryName(this.string_0);
			File.Delete(this.string_0);
			this.string_0 = string.Concat(directoryName, "\\", string_1);
			this.SaveToFile(this.string_0);
		}

		public void SaveToFile(string string_1)
		{
			if (this.iagsserverConnectionName_0 != null)
			{
				if (string_1[1] != ':')
				{
					string str = string.Concat(Environment.SystemDirectory.Substring(0, 2), "\\Users\\Administrator\\AppData\\Roaming\\ESRI\\Desktop10.2\\ArcCatalog\\");
					if (!Directory.Exists(str))
					{
						try
						{
							Directory.CreateDirectory(str);
						}
						catch
						{
						}
					}
					this.string_0 = string.Concat(str, string_1);
				}
				else
				{
					this.string_0 = string_1;
				}
				IStorageHepler storageHeplerClass = new StorageHeplerClass();
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.string_0);
				this.string_0 = string.Concat(Path.GetDirectoryName(this.string_0), "\\", fileNameWithoutExtension, ".ags");
				if (this.object_0 != null)
				{
					storageHeplerClass.SaveAGS(this.string_0, fileNameWithoutExtension, this.object_0, this.iagsserverConnectionName_0);
				}
				else
				{
					storageHeplerClass.StorageSaveAGS(this.string_0, fileNameWithoutExtension, this.iagsserverConnectionName_0);
				}
			}
		}

		public void SearchChildren(string string_1, IGxObjectArray igxObjectArray_1)
		{
		}

		public void SetProperty(string string_1, object object_1)
		{
		}
	}
}