using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Yutai.Shared;

namespace Yutai.Catalog
{
	public class GxDatabase : IGxObject, IGxDatabase, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap, IGxRemoteConnection
	{
		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private string string_0;

		private IWorkspaceName iworkspaceName_0;

		private IWorkspace iworkspace_0 = null;

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		//private SysGrants sysGrants_0;

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		private bool bool_0 = false;

		private bool bool_1 = false;

		public bool AreChildrenViewable
		{
			get
			{
				return this.igxObjectArray_0.Count > 0;
			}
		}

		public string BaseName
		{
			get
			{
				return Path.GetFileNameWithoutExtension(this.string_0);
			}
		}

		public string Category
		{
			get
			{
				string str;
				if (this.iworkspaceName_0 == null)
				{
					str = "错误的数据库连接";
				}
				else if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					str = (!(this.iworkspaceName_0.WorkspaceFactory is IOleDBConnectionInfo) ? "空间数据库连接" : "OLE DB连接");
				}
				else if (this.iworkspaceName_0.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
				{
					str = "";
				}
				else
				{
					str = (this.iworkspaceName_0.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1" ? "个人空间数据库" : "文件型空间数据库");
				}
				return str;
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				IEnumGxObject igxObjectArray0;
				if (!this.IsRemoteDatabase)
				{
					if (!this.IsConnected)
					{
						this.Connect();
					}
					igxObjectArray0 = this.igxObjectArray_0 as IEnumGxObject;
				}
				else
				{
					igxObjectArray0 = this.igxObjectArray_0 as IEnumGxObject;
				}
				return igxObjectArray0;
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
				string str;
				str = (this.iworkspaceName_0.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace ? this.string_0 : string.Concat("Database Connections\\", Path.GetFileName(this.string_0)));
				return str;
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
				return this.iworkspaceName_0 as IName;
			}
		}

		public bool IsConnected
		{
			get
			{
				bool flag;
				if (this.iworkspace_0 == null)
				{
					flag = false;
				}
				else if (this.iworkspace_0.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
				{
					try
					{
						if (this.iworkspace_0.WorkspaceFactory is ISetDefaultConnectionInfo)
						{
							IWorkspaceFactoryStatus sdeWorkspaceFactoryClass = new SdeWorkspaceFactory() as IWorkspaceFactoryStatus;
							IWorkspaceStatus workspaceStatu = sdeWorkspaceFactoryClass.PingWorkspaceStatus(this.iworkspace_0);
							if (workspaceStatu.ConnectionStatus == esriWorkspaceConnectionStatus.esriWCSAvailable)
							{
								try
								{
									this.iworkspace_0 = null;
									this.iworkspace_0 = sdeWorkspaceFactoryClass.OpenAvailableWorkspace(workspaceStatu);
									flag = true;
									return flag;
								}
								catch (Exception exception)
								{
									//CErrorLog.writeErrorLog(this, exception, "");
								}
							}
							else if (workspaceStatu.ConnectionStatus == esriWorkspaceConnectionStatus.esriWCSDown)
							{
								try
								{
									this.iworkspace_0 = null;
									this.iworkspace_0 = sdeWorkspaceFactoryClass.OpenAvailableWorkspace(workspaceStatu);
									flag = true;
									return flag;
								}
								catch (Exception exception1)
								{
									//CErrorLog.writeErrorLog(this, exception1, "");
								}
							}
							else if (workspaceStatu.ConnectionStatus == esriWorkspaceConnectionStatus.esriWCSUp)
							{
								flag = true;
								return flag;
							}
						}
						else if (this.iworkspace_0.WorkspaceFactory is IOleDBConnectionInfo)
						{
							flag = true;
							return flag;
						}
					}
					catch (Exception exception2)
					{
						//CErrorLog.writeErrorLog(this, exception2, "");
					}
					flag = false;
				}
				else
				{
					flag = true;
				}
				return flag;
			}
		}

		public bool IsEnterpriseGeodatabase
		{
			get
			{
				bool flag;
				flag = (this.iworkspaceName_0 != null ? this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace : false);
				return flag;
			}
		}

		public bool IsGeoDatabase
		{
			get
			{
				bool bool1 = this.bool_1;
				if (!this.IsRemoteDatabase)
				{
					bool1 = true;
				}
				else if (!this.bool_0)
				{
					this.bool_0 = true;
					bool1 = (this.iworkspace_0 != null ? WorkspaceOperator.IsConnectedToGeodatabase(this.iworkspace_0) : WorkspaceOperator.IsConnectedToGeodatabase(this.iworkspaceName_0));
					this.bool_1 = bool1;
				}
				return bool1;
			}
		}

		public bool IsRemoteDatabase
		{
			get
			{
				bool flag;
				flag = (this.iworkspaceName_0 != null ? this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace : false);
				return flag;
			}
		}

		public bool IsValid
		{
			get
			{
				return this.iworkspaceName_0 != null;
			}
		}

		IName Yutai.Catalog.IGxObjectInternalName.InternalObjectName
		{
			get
			{
				return this.iworkspaceName_0 as IName;
			}
			set
			{
			}
		}

		public Bitmap LargeImage
		{
			get
			{
				Bitmap smallImage;
				if (this.iworkspaceName_0 == null)
				{
					smallImage = null;
				}
				else if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					smallImage = (!this.IsConnected ? ImageLib.GetSmallImage(9) : ImageLib.GetSmallImage(10));
				}
				else if (this.iworkspaceName_0.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
				{
					smallImage = null;
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(15);
				}
				return smallImage;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				Bitmap smallImage;
				if (this.iworkspaceName_0 == null)
				{
					smallImage = null;
				}
				else if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					smallImage = (!this.IsConnected ? ImageLib.GetSmallImage(9) : ImageLib.GetSmallImage(10));
				}
				else if (this.iworkspaceName_0.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
				{
					smallImage = null;
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(15);
				}
				return smallImage;
			}
		}

		public string Name
		{
			get
			{
				Path.GetExtension(this.string_0).ToLower();
				return Path.GetFileName(this.string_0);
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

		public Bitmap SmallImage
		{
			get
			{
				Bitmap smallImage;
				if (this.iworkspaceName_0 == null)
				{
					smallImage = null;
				}
				else if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					smallImage = (!this.IsConnected ? ImageLib.GetSmallImage(9) : ImageLib.GetSmallImage(10));
				}
				else if (this.iworkspaceName_0.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
				{
					smallImage = null;
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(15);
				}
				return smallImage;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap smallImage;
				if (this.iworkspaceName_0 == null)
				{
					smallImage = null;
				}
				else if (this.iworkspaceName_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					smallImage = (!this.IsConnected ? ImageLib.GetSmallImage(9) : ImageLib.GetSmallImage(10));
				}
				else if (this.iworkspaceName_0.Type != esriWorkspaceType.esriLocalDatabaseWorkspace)
				{
					smallImage = null;
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(15);
				}
				return smallImage;
			}
		}

		public IWorkspace Workspace
		{
			get
			{
				return this.iworkspace_0;
			}
		}

		public IWorkspaceName WorkspaceName
		{
			get
			{
				return this.iworkspaceName_0;
			}
			set
			{
				this.iworkspaceName_0 = value;
				this.string_0 = this.iworkspaceName_0.PathName;
			}
		}

		public GxDatabase()
		{
			//if ((AppConfigInfo.UserID.Length <= 0 ? false : AppConfigInfo.UserID != "admin"))
			//{
			//	this.sysGrants_0 = new SysGrants(AppConfigInfo.UserID);
			//}
		}

		public IGxObject AddChild(IGxObject igxObject_1)
		{
			IGxObject igxObject1;
			if (!(igxObject_1 is IGxDataset))
			{
				igxObject1 = null;
			}
			else if (this.igxObjectArray_0.Count != 0)
			{
				IDatasetName datasetName = (igxObject_1 as IGxDataset).DatasetName;
				bool flag = false;
				if (datasetName.Type == esriDatasetType.esriDTFeatureDataset)
				{
					flag = true;
				}
				string upper = igxObject_1.Name.ToUpper();
				int num = 0;
				int num1 = 0;
				int count = this.igxObjectArray_0.Count;
				int num2 = 0;
				do
				{
				Label1:
					num2 = (num1 + count) / 2;
					IGxObject gxObject = this.igxObjectArray_0.Item(num2);
					if (!flag)
					{
						if ((gxObject as IGxDataset).DatasetName.Type != esriDatasetType.esriDTFeatureDataset)
						{
							goto Label2;
						}
						num1 = num2 + 1;
						if (num1 == count)
						{
							this.igxObjectArray_0.Insert(num1, igxObject_1);
							igxObject1 = igxObject_1;
							return igxObject1;
						}
						else
						{
							goto Label1;
						}
					}
					else if ((gxObject as IGxDataset).DatasetName.Type != esriDatasetType.esriDTFeatureDataset)
					{
						count = num2;
						continue;
					}
				Label2:
					num = gxObject.Name.ToUpper().CompareTo(upper);
					if (num == 0)
					{
						igxObject1 = gxObject;
						return igxObject1;
					}
					else
					{
						if (num <= 0)
						{
							num1 = num2 + 1;
						}
						else
						{
							count = num2;
						}
						if (num1 == count)
						{
							this.igxObjectArray_0.Insert(num1, igxObject_1);
							igxObject1 = igxObject_1;
							return igxObject1;
						}
						else
						{
							goto Label1;
						}
					}
				}
				while (num1 != count);
				this.igxObjectArray_0.Insert(num1, igxObject_1);
				igxObject1 = igxObject_1;
			}
			else
			{
				this.igxObjectArray_0.Insert(-1, igxObject_1);
				igxObject1 = igxObject_1;
			}
			return igxObject1;
		}

		public IGxObject AddChild1(IGxObject igxObject_1)
		{
			IGxObject igxObject1;
			if (!(igxObject_1 is IGxDataset))
			{
				igxObject1 = null;
			}
			else
			{
				IDatasetName datasetName = (igxObject_1 as IGxDataset).DatasetName;
				bool flag = false;
				if (datasetName.Type == esriDatasetType.esriDTFeatureDataset)
				{
					flag = true;
				}
				string upper = igxObject_1.Name.ToUpper();
				int num = 0;
				for (int i = 0; i < this.igxObjectArray_0.Count; i++)
				{
					IGxObject gxObject = this.igxObjectArray_0.Item(i);
					num = gxObject.Name.ToUpper().CompareTo(upper);
					if (gxObject.Category != igxObject_1.Category)
					{
						if (flag && (gxObject as IGxDataset).DatasetName.Type != esriDatasetType.esriDTFeatureDataset)
						{
							this.igxObjectArray_0.Insert(i, igxObject_1);
							igxObject1 = igxObject_1;
							return igxObject1;
						}
					}
					else if (num == 0)
					{
						igxObject1 = gxObject;
						return igxObject1;
					}
					else if (num > 0)
					{
						this.igxObjectArray_0.Insert(i, igxObject_1);
						igxObject1 = igxObject_1;
						return igxObject1;
					}
				}
				this.igxObjectArray_0.Insert(-1, igxObject_1);
				igxObject1 = igxObject_1;
			}
			return igxObject1;
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

		public bool CanPaste(IEnumName ienumName_0, ref bool bool_2)
		{
			bool flag;
			if (this.Category != "OLE DB连接")
			{
				ienumName_0.Reset();
				IName name = ienumName_0.Next();
				bool type = false;
				while (name != null)
				{
					if (name is IFeatureClassName)
					{
						type = true;
						if (((name as IDatasetName).WorkspaceName.PathName != this.iworkspaceName_0.PathName ? true : (name as IFeatureClassName).FeatureDatasetName == null))
						{
							bool_2 = false;
						}
						else
						{
							bool_2 = true;
						}
					}
					else if (name is ITableName)
					{
						type = (name as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriFileSystemWorkspace;
						bool_2 = false;
					}
					else if (!(name is IFeatureDatasetName))
					{
						bool_2 = false;
						flag = false;
						return flag;
					}
					else if ((name as IDatasetName).WorkspaceName.PathName != this.iworkspaceName_0.PathName)
					{
						bool_2 = false;
						type = true;
					}
					else
					{
						type = false;
					}
					if (!type)
					{
						bool_2 = false;
						flag = false;
						return flag;
					}
					else
					{
						name = ienumName_0.Next();
					}
				}
				flag = true;
			}
			else
			{
				bool_2 = false;
				flag = false;
			}
			return flag;
		}

		public bool CanRename()
		{
			bool flag;
			flag = (this.iworkspace_0 == null ? true : (this.iworkspace_0 as IDataset).CanRename());
			return flag;
		}

		public void Connect()
		{
			object obj;
			object obj1;
			try
			{
				this.iworkspaceName_0.ConnectionProperties.GetAllProperties(out obj, out obj1);
				this.iworkspace_0 = (this.iworkspaceName_0 as IName).Open() as IWorkspace;
				this.iworkspace_0.ConnectionProperties.GetAllProperties(out obj, out obj1);
				//if (this.sysGrants_0 == null && this.iworkspace_0.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				//{
				//	if ((AppConfigInfo.UserID.Length <= 0 ? false : AppConfigInfo.UserID != "admin"))
				//	{
				//		this.sysGrants_0 = new SysGrants(AppConfigInfo.UserID);
				//	}
				//}
				this.method_0(this.iworkspace_0);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (exception is COMException)
				{
					uint errorCode = (uint)(exception as COMException).ErrorCode;
					if (!(errorCode == -2147216023 ? false : errorCode != -2147216127))
					{
						MessageBox.Show("连接数据库失败", "连接");
						return;
					}
					else if (errorCode == -2147216022)
					{
						MessageBox.Show("连接数据库失败\r\n该服务器上的SDE没有启动", "连接");
						return;
					}
					else if (errorCode == -2147155559)
					{
						MessageBox.Show("连接的数据库服务器未启动", "连接");
						return;
					}
					else if (errorCode == -2147216072)
					{
						MessageBox.Show("连接的数据库服务器已暂停", "连接");
						return;
					}
					else if ((errorCode == -2147467259 || errorCode == -2147155644 ? true : errorCode == -2147216118))
					{
						MessageBox.Show("连接数据库失败", "连接");
						return;
					}
				}
			//	CErrorLog.writeErrorLog(null, exception, "");
			}
		}

		public void Delete()
		{
			try
			{
				if (!this.IsRemoteDatabase)
				{
					this.igxObjectArray_0.Empty();
					if (this.iworkspace_0 != null)
					{
						Marshal.ReleaseComObject(this.iworkspace_0);
						this.iworkspace_0 = null;
						GC.Collect();
					}
					string pathName = this.iworkspaceName_0.PathName;
					if (this.iworkspaceName_0.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
					{
						File.Delete(pathName);
					}
					else
					{
						Directory.Delete(pathName, true);
					}
				}
				else
				{
					(this.iworkspaceName_0.WorkspaceFactory as IRemoteDatabaseWorkspaceFactory).DeleteConnectionFile(this.string_0);
				}
				this.Detach();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
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
			if (this.iworkspace_0 != null)
			{
				Marshal.ReleaseComObject(this.iworkspace_0);
				this.iworkspace_0 = null;
				GC.Collect();
			}
		}

		public void EditProperties(int int_0)
		{
			if (this.iworkspace_0 == null)
			{
				System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
				this.Connect();
				if (this.iworkspace_0 == null)
				{
					return;
				}
				this.igxCatalog_0.ObjectChanged(this);
				this.igxCatalog_0.ObjectRefreshed(this);
				System.Windows.Forms.Cursor.Current = Cursors.Default;
			}
			if (!(this.iworkspace_0 is IOleDBConnectionInfo))
			{
				//frmGDBInfo _frmGDBInfo = new frmGDBInfo()
				//{
				//	Workspace = this.iworkspace_0
				//};
				//_frmGDBInfo.ShowDialog();
			}
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
			bool bool1 = this.bool_1;
			if (!this.IsRemoteDatabase)
			{
				bool1 = true;
			}
			else if (!this.bool_0)
			{
				this.bool_0 = true;
				bool1 = (this.iworkspace_0 != null ? WorkspaceOperator.IsConnectedToGeodatabase(this.iworkspace_0) : WorkspaceOperator.IsConnectedToGeodatabase(this.iworkspaceName_0));
				this.bool_1 = bool1;
			}
			this.ipopuMenuWrap_0.AddItem("CopyItem", false);
			this.ipopuMenuWrap_0.AddItem("PasteItem", false);
			this.ipopuMenuWrap_0.AddItem("RefreshItem", false);
			this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
			this.ipopuMenuWrap_0.AddItem("ReName", false);
			this.ipopuMenuWrap_0.ClearSubItem("NewBarSubItem");
			this.ipopuMenuWrap_0.AddSubmenuItem("NewBarSubItem", "新建", "", true);
			if (bool1)
			{
				this.ipopuMenuWrap_0.AddItem("NewFeatureDataset", "NewBarSubItem", false);
			}
			this.ipopuMenuWrap_0.AddItem("NewFeatureClass", "NewBarSubItem", false);
			this.ipopuMenuWrap_0.AddItem("NewTable", "NewBarSubItem", false);
			if (bool1)
			{
				this.ipopuMenuWrap_0.AddItem("NewRelationClass", "NewBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("NewRasterFolder", "NewBarSubItem", true);
				this.ipopuMenuWrap_0.AddItem("NewRasterDataset", "NewBarSubItem", false);
			}
			this.ipopuMenuWrap_0.ClearSubItem("ImportBarSubItem");
			this.ipopuMenuWrap_0.AddSubmenuItem("ImportBarSubItem", "导入", "", true);
			this.ipopuMenuWrap_0.AddItem("ImportSingleFeatureClass", "ImportBarSubItem", false);
			this.ipopuMenuWrap_0.AddItem("ConvertToMultiFeatureClass", "ImportBarSubItem", false);
			this.ipopuMenuWrap_0.AddItem("ImportSingleTable", "ImportBarSubItem", false);
			this.ipopuMenuWrap_0.AddItem("MultiTableDataConvert", "ImportBarSubItem", false);
			if (bool1)
			{
				this.ipopuMenuWrap_0.AddItem("RasterToGDB", "ImportBarSubItem", false);
			}
			this.ipopuMenuWrap_0.AddItem("ImportXY", "ImportBarSubItem", false);
			if (this.IsRemoteDatabase)
			{
				if (!bool1)
				{
					this.ipopuMenuWrap_0.AddItem("EnableEnterpriseGeodatabaseCommand", true);
				}
				this.ipopuMenuWrap_0.AddItem("VersionInfo", true);
				this.ipopuMenuWrap_0.AddItem("Connection", false);
				this.ipopuMenuWrap_0.AddItem("DisConnection", false);
				this.ipopuMenuWrap_0.AddItem("ConnectionProperty", false);
			}
			this.ipopuMenuWrap_0.AddItem("GxObjectProperty", true);
		}

		private void method_0(IWorkspace iworkspace_1)
		{
			try
			{
				IEnumDatasetName datasetNames = iworkspace_1.DatasetNames[esriDatasetType.esriDTAny];
				datasetNames.Reset();
				IDatasetName datasetName = datasetNames.Next();
				IGxObject gxRasterDataset = null;
				while (datasetName != null)
				{
					gxRasterDataset = null;
					if (!this.IsEnterpriseGeodatabase)
					{
						if ((datasetName.Type == esriDatasetType.esriDTRasterDataset ? false : datasetName.Type != esriDatasetType.esriDTRasterCatalog))
						{
							gxRasterDataset = ((datasetName.Type == esriDatasetType.esriDTFeatureClass ? false : datasetName.Type != esriDatasetType.esriDTTable) ? new GxDataset() : new GxDataset());
						}
						else
						{
							gxRasterDataset = new GxRasterDataset();
						}
						if (gxRasterDataset != null)
						{
							(gxRasterDataset as IGxDataset).DatasetName = datasetName;
							gxRasterDataset.Attach(this, this.igxCatalog_0);
						}
					}
					else
					{
						if (!(datasetName.Type == esriDatasetType.esriDTRasterDataset ? false : datasetName.Type != esriDatasetType.esriDTRasterCatalog))
						{
							gxRasterDataset = new GxRasterDataset();
						}
						else if ((datasetName.Type == esriDatasetType.esriDTFeatureClass ? false : datasetName.Type != esriDatasetType.esriDTTable))
						{
							gxRasterDataset = new GxDataset();
						}
						//else if (AppConfigInfo.UserID.Length <= 0)
						//{
						//	gxRasterDataset = new GxDataset();
						//}
						//else if (AppConfigInfo.UserID.ToLower() == "admin")
						//{
						//	gxRasterDataset = new GxDataset();
						//}
						//else if (this.sysGrants_0 == null)
						//{
						//	gxRasterDataset = new GxDataset();
						//}
						//else if (this.sysGrants_0.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 1, datasetName.Name))
						//{
						//	gxRasterDataset = new GxDataset();
						//}
						if (gxRasterDataset != null)
						{
							(gxRasterDataset as IGxDataset).DatasetName = datasetName;
							gxRasterDataset.Attach(this, this.igxCatalog_0);
						}
					}
					datasetName = datasetNames.Next();
				}
			}
			catch (Exception exception)
			{
				//CErrorLog.writeErrorLog(this, exception, "");
			}
		}

		public bool Paste(IEnumName ienumName_0, ref bool bool_2)
		{
			bool flag;
			IEnumNameMapping enumNameMapping;
			if (this.Category != "OLE DB连接")
			{
				bool flag1 = true;
				try
				{
					if (!bool_2)
					{
						//flag1 = false;
						//IGeoDBDataTransfer myGeoDBDataTransfer = new MyGeoDBDataTransfer();
						//myGeoDBDataTransfer.GenerateNameMapping(ienumName_0, this.iworkspaceName_0 as IName, out enumNameMapping);
						//frmGeoDBDataTransfer _frmGeoDBDataTransfer = new frmGeoDBDataTransfer()
						//{
						//	EnumNameMapping = enumNameMapping,
						//	ToName = this.iworkspaceName_0 as IName,
						//	GeoDBTransfer = myGeoDBDataTransfer
						//};
						//if (_frmGeoDBDataTransfer.ShowDialog() != DialogResult.OK)
						//{
						//	flag = false;
						//	return flag;
						//}
						//else
						//{
						//	this.Refresh();
						//	flag = true;
						//	return flag;
						//}
					}
					else
					{
						ienumName_0.Reset();
						IName iworkspaceName0 = ienumName_0.Next();
						if ((iworkspaceName0 as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriFileSystemWorkspace && (iworkspaceName0 as IDatasetName).WorkspaceName.PathName == this.iworkspaceName_0.PathName)
						{
							IDatasetContainer datasetContainer = (this.iworkspaceName_0 as IName).Open() as IDatasetContainer;
							while (iworkspaceName0 != null)
							{
								datasetContainer.AddDataset(iworkspaceName0.Open() as IDataset);
								(iworkspaceName0 as IFeatureClassName).FeatureDatasetName = null;
								IGxObject gxDataset = new GxDataset();
								(iworkspaceName0 as IDatasetName).WorkspaceName = this.iworkspaceName_0;
								(gxDataset as IGxDataset).DatasetName = iworkspaceName0 as IDatasetName;
								gxDataset.Attach(this, this.igxCatalog_0);
								this.igxCatalog_0.ObjectAdded(gxDataset);
								iworkspaceName0 = ienumName_0.Next();
							}
							flag = true;
							return flag;
						}
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
					flag = false;
					return flag;
				}
				flag = flag1;
			}
			else
			{
				bool_2 = false;
				flag = false;
			}
			return flag;
		}

		public void Refresh()
		{
			this.bool_0 = false;
			if (this.IsConnected)
			{
				this.igxObjectArray_0.Empty();
				this.iworkspace_0 = null;
				this.iworkspace_0 = (this.iworkspaceName_0 as IName).Open() as IWorkspace;
				this.method_0(this.iworkspace_0);
				this.igxCatalog_0.ObjectRefreshed(this);
			}
		}

		public void Rename(string string_1)
		{
			string str;
			if (string_1 != null)
			{
				string extension = Path.GetExtension(string_1);
				string str1 = Path.GetFileNameWithoutExtension(string_1).Trim();
				if (str1.Length == 0)
				{
					MessageBox.Show("必须键入文件名!");
					this.igxCatalog_0.ObjectChanged(this);
				}
				else if (!this.IsRemoteDatabase)
				{
					if (this.Category == "个人空间数据库" && extension.ToLower() != ".mdb")
					{
						string_1 = string.Concat(string_1, ".mdb");
					}
					IDataset iworkspace0 = this.iworkspace_0 as IDataset;
					try
					{
						str = Path.Combine(Path.GetDirectoryName(this.string_0), string_1);
						if (this.Category == "个人空间数据库")
						{
							if (File.Exists(str))
							{
								MessageBox.Show("已存在同名个人数据库，请重新指定其他名字");
								return;
							}
						}
						else if (Directory.Exists(str))
						{
							MessageBox.Show("已存在同名文件型数据库，请重新指定其他名字");
							return;
						}
						iworkspace0.Rename(string_1);
						if (str.ToLower() != this.string_0.ToLower())
						{
							this.string_0 = str;
							this.iworkspaceName_0.PathName = this.string_0;
							this.igxCatalog_0.ObjectChanged(this);
						}
					}
					catch (Exception exception)
					{
					}
				}
				else
				{
					string str2 = ".sde";
					if (this.iworkspaceName_0.WorkspaceFactory is IOleDBConnectionInfo)
					{
						str2 = ".odc";
					}
					if (extension.ToLower() != str2)
					{
						string_1 = string.Concat(str1, str2);
					}
					try
					{
						str = Path.Combine(Path.GetDirectoryName(this.string_0), string_1);
						if (!File.Exists(str))
						{
							IRemoteDatabaseWorkspaceFactory workspaceFactory = this.iworkspaceName_0.WorkspaceFactory as IRemoteDatabaseWorkspaceFactory;
							workspaceFactory.RenameConnectionFile(this.string_0, string_1);
							this.string_0 = str;
							this.iworkspaceName_0.PathName = this.string_0;
							this.igxCatalog_0.ObjectChanged(this);
						}
						else
						{
							MessageBox.Show("已存在同名空间数据库连接，请重新指定其他名字");
						}
					}
					catch (Exception exception1)
					{
						MessageBox.Show(exception1.Message);
					}
				}
			}
		}

		public void SearchChildren(string string_1, IGxObjectArray igxObjectArray_1)
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