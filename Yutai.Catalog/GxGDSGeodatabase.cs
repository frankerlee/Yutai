using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

using System;
using System.Drawing;

namespace Yutai.Catalog
{
	public class GxGDSGeodatabase : IGxObject, IGxDatabase, IGxObjectContainer, IGxObjectUI, IGxPasteTarget, IGxGeodatabase, IGxRemoteConnection
	{
		private string string_0 = "dbo.Default";

		private string string_1 = "";

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		internal IDataServerManager dataServerManager = null;

		private IWorkspace iworkspace_0 = null;

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
				return string.Format("{0} ({1})", this.string_1, this.string_0);
			}
		}

		public string Category
		{
			get
			{
				return "空间数据库";
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				if (this.igxObjectArray_0.Count == 0)
				{
					IDataServerManagerAdmin dataServerManagerAdmin = (IDataServerManagerAdmin)this.dataServerManager;
					IWorkspaceName workspaceName = dataServerManagerAdmin.CreateWorkspaceName(this.string_1, "VERSION", "dbo.Default");
					this.iworkspace_0 = (workspaceName as IName).Open() as IWorkspace;
					this.method_0(this.iworkspace_0);
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

		public object DataServerManager
		{
			get
			{
				return this.dataServerManager;
			}
		}

		public string FullName
		{
			get
			{
				return string.Format("{0} ({1})", this.string_1, this.string_0);
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

		public bool IsConnected
		{
			get
			{
				return false;
			}
		}

		public bool IsEnterpriseGeodatabase
		{
			get
			{
				return false;
			}
		}

		public bool IsRemoteDatabase
		{
			get
			{
				return false;
			}
		}

		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		public Bitmap LargeImage
		{
			get
			{
				return ImageLib.GetSmallImage(15);
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				return ImageLib.GetSmallImage(15);
			}
		}

		public string Name
		{
			get
			{
				return string.Format("{0} ({1})", this.string_1, this.string_0);
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

		public Bitmap SmallImage
		{
			get
			{
				return ImageLib.GetSmallImage(15);
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				return ImageLib.GetSmallImage(15);
			}
		}

		public IWorkspace Workspace
		{
			get
			{
				return null;
			}
		}

		public IWorkspaceName WorkspaceName
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public GxGDSGeodatabase(string string_2, IDataServerManager idataServerManager_0)
		{
			this.string_1 = string_2;
			this.dataServerManager = idataServerManager_0;
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

		public void Attach(IGxObject igxObject_1, IGxCatalog igxCatalog_1)
		{
			this.igxObject_0 = igxObject_1;
			this.igxCatalog_0 = igxCatalog_1;
			if (this.igxObject_0 is IGxObjectContainer)
			{
				(this.igxObject_0 as IGxObjectContainer).AddChild(this);
			}
		}

		public void Backup(string string_2, string string_3, string string_4, out bool bool_0)
		{
			string serverName = this.dataServerManager.ServerName;
			bool_0 = false;
			(this.dataServerManager as IDataServerManagerAdmin).IsSimpleRecoveryModel(this.string_1, ref bool_0);
			(this.dataServerManager as IDataServerManagerAdmin).BackupGeodatabase(this.string_1, string_3, string_2, string_4);
		}

		public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
		{
			return false;
		}

		public void Connect()
		{
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

		public void DetachGeodatabase()
		{
			(this.dataServerManager as IDataServerManagerAdmin).DeleteGeodatabase(this.string_1);
		}

		public void Disconnect()
		{
		}

		public void GeodatabaseName(ref string string_2)
		{
			string_2 = this.string_1;
		}

		public void GetProperties(out string string_2, out object object_0, out int int_0, out string string_3, out object object_1)
		{
			(this.dataServerManager as IDataServerManagerAdmin).GetDBProperties(this.string_1, out string_3, out int_0, out string_2, out object_0, out object_1);
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
					datasetName = datasetNames.Next();
				}
			}
			catch (Exception exception)
			{
				//CErrorLog.writeErrorLog(this, exception, "");
			}
		}

		public bool Paste(IEnumName ienumName_0, ref bool bool_0)
		{
			return false;
		}

		public void Refresh()
		{
			for (int i = 0; i < this.igxObjectArray_0.Count; i++)
			{
				this.igxObjectArray_0.Item(i).Refresh();
			}
		}

		public void SearchChildren(string string_2, IGxObjectArray igxObjectArray_1)
		{
		}

		public void Upgrade()
		{
			(this.dataServerManager as IDataServerManagerAdmin).UpgradeGeoDatabase(this.string_1);
		}
	}
}