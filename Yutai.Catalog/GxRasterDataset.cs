using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;


using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxRasterDataset : IGxObject, IGxDataset, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap
	{
		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private IGxObject igxObject_0 = null;

		private IDatasetName idatasetName_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private bool bool_0 = false;

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		public bool AreChildrenViewable
		{
			get
			{
				return (!(this.idatasetName_0 is IRasterBandName) ? true : false);
			}
		}

		public string BaseName
		{
			get
			{
				string str;
				str = (this.idatasetName_0 == null ? "" : Path.GetFileNameWithoutExtension(this.idatasetName_0.Name));
				return str;
			}
		}

		public string Category
		{
			get
			{
				string str;
				if (this.idatasetName_0.Type != esriDatasetType.esriDTRasterBand)
				{
					str = (this.idatasetName_0.Type != esriDatasetType.esriDTRasterDataset ? "删格目录" : "删格数据集合");
				}
				else
				{
					str = "删格波段";
				}
				return str;
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				if (!this.bool_0)
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

		public IDataset Dataset
		{
			get
			{
				IDataset dataset;
				try
				{
					if (this.idatasetName_0 != null)
					{
						dataset = (this.idatasetName_0 as IName).Open() as IDataset;
						return dataset;
					}
				}
				catch (Exception exception)
				{
					//CErrorLog.writeErrorLog(this, exception, "");
				}
				dataset = null;
				return dataset;
			}
		}

		public IDatasetName DatasetName
		{
			get
			{
				return this.idatasetName_0;
			}
			set
			{
				this.idatasetName_0 = value;
			}
		}

		public string FullName
		{
			get
			{
				string str;
				if (this.idatasetName_0 == null)
				{
					str = "";
				}
				else if (this.idatasetName_0.Type != esriDatasetType.esriDTRasterCatalog)
				{
					str = (this.idatasetName_0.WorkspaceName.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace ? string.Concat(this.idatasetName_0.WorkspaceName.PathName, "\\", this.idatasetName_0.Name) : string.Concat("数据库连接\\", Path.GetFileName(this.idatasetName_0.WorkspaceName.PathName), "\\", this.idatasetName_0.Name));
				}
				else
				{
					str = (this.idatasetName_0.WorkspaceName.Type != esriWorkspaceType.esriRemoteDatabaseWorkspace ? string.Concat(this.idatasetName_0.WorkspaceName.PathName, "\\", this.idatasetName_0.Name) : string.Concat("数据库连接\\", Path.GetFileName(this.idatasetName_0.WorkspaceName.PathName), "\\", this.idatasetName_0.Name));
				}
				return str;
			}
		}

		public bool HasChildren
		{
			get
			{
				return (!(this.idatasetName_0 is IRasterBandName) ? true : false);
			}
		}

		public IName InternalObjectName
		{
			get
			{
				return this.idatasetName_0 as IName;
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
				return this.idatasetName_0 as IName;
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
				bitmap = (this.idatasetName_0.Type != esriDatasetType.esriDTRasterCatalog ? ImageLib.GetSmallImage(17) : ImageLib.GetSmallImage(46));
				return bitmap;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (this.idatasetName_0.Type != esriDatasetType.esriDTRasterCatalog ? ImageLib.GetSmallImage(17) : ImageLib.GetSmallImage(46));
				return bitmap;
			}
		}

		public string Name
		{
			get
			{
				string fileName;
				try
				{
					if (this.idatasetName_0 != null)
					{
						fileName = Path.GetFileName(this.idatasetName_0.Name);
						return fileName;
					}
				}
				catch
				{
				}
				fileName = "";
				return fileName;
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
				Bitmap bitmap;
				bitmap = (this.idatasetName_0.Type != esriDatasetType.esriDTRasterCatalog ? ImageLib.GetSmallImage(17) : ImageLib.GetSmallImage(46));
				return bitmap;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap bitmap;
				bitmap = (this.idatasetName_0.Type != esriDatasetType.esriDTRasterCatalog ? ImageLib.GetSmallImage(17) : ImageLib.GetSmallImage(46));
				return bitmap;
			}
		}

		public esriDatasetType Type
		{
			get
			{
				esriDatasetType _esriDatasetType;
				_esriDatasetType = (this.idatasetName_0 == null ? esriDatasetType.esriDTAny : this.idatasetName_0.Type);
				return _esriDatasetType;
			}
		}

		public GxRasterDataset()
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

		public bool CanCopy()
		{
			return true;
		}

		public bool CanDelete()
		{
			IDataset dataset = this.Dataset;
			bool flag = false;
			try
			{
				flag = dataset.CanDelete();
			}
			catch
			{
			}
			dataset = null;
			return flag;
		}

		public bool CanPaste(IEnumName ienumName_0, ref bool bool_1)
		{
			return false;
		}

		public bool CanRename()
		{
			bool flag;
			try
			{
				IDataset dataset = this.Dataset;
				if (dataset != null)
				{
					bool flag1 = false;
					flag1 = dataset.CanRename();
					dataset = null;
					flag = flag1;
					return flag;
				}
			}
			catch
			{
			}
			flag = false;
			return flag;
		}

		public void Delete()
		{
			try
			{
				IDataset dataset = this.Dataset;
				dataset.Delete();
				this.Detach();
				Marshal.ReleaseComObject(dataset);
				dataset = null;
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

		public void EditProperties(int int_0)
		{
			//IDataset dataset = this.Dataset;
			//if (dataset != null && dataset is IObjectClass)
			//{
			//	ObjectClassInfoEdit objectClassInfoEdit = new ObjectClassInfoEdit()
			//	{
			//		ObjectClass = dataset as IObjectClass
			//	};
			//	objectClassInfoEdit.ShowDialog();
			//}
		}

		public void GetPropByIndex(int int_0, ref string string_0, ref object object_0)
		{
		}

		public object GetProperty(string string_0)
		{
			return null;
		}

		public void Init(object object_0)
		{
			this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
			this.ipopuMenuWrap_0.Clear();
			if (this.idatasetName_0.Type == esriDatasetType.esriDTRasterDataset)
			{
				this.ipopuMenuWrap_0.AddItem("CopyItem", false);
				this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
				this.ipopuMenuWrap_0.AddItem("ReName", false);
				this.ipopuMenuWrap_0.AddItem("RefreshItem", true);
				this.ipopuMenuWrap_0.UpdateUI();
			}
			else if (this.idatasetName_0.Type != esriDatasetType.esriDTRasterBand && this.idatasetName_0.Type == esriDatasetType.esriDTRasterCatalog)
			{
				this.ipopuMenuWrap_0.AddItem("DeleteObject", false);
			}
		}

		private void method_0()
		{
			this.bool_0 = true;
			this.igxObjectArray_0.Empty();
			if (this.idatasetName_0.Type != esriDatasetType.esriDTRasterDataset)
			{
			    return;
			}
			else
			{
				try
				{
					IEnumDatasetName subsetNames = this.idatasetName_0.SubsetNames;
					subsetNames.Reset();
					for (IDatasetName i = subsetNames.Next(); i != null; i = subsetNames.Next())
					{
						IGxObject gxRasterDataset = new GxRasterDataset();
						(gxRasterDataset as IGxDataset).DatasetName = i;
						gxRasterDataset.Attach(this, this.igxCatalog_0);
					}
				}
				catch
				{
				}
			}
		}

		public bool Paste(IEnumName ienumName_0, ref bool bool_1)
		{
			return false;
		}

		public void Refresh()
		{
		}

		public void Rename(string string_0)
		{
			try
			{
				if (string_0 != null)
				{
					IDataset dataset = this.Dataset;
					if (dataset != null)
					{
						Path.GetFileNameWithoutExtension(string_0);
						this.Dataset.Rename(string_0);
						this.idatasetName_0.Name = string_0;
						Marshal.ReleaseComObject(dataset);
						dataset = null;
						this.igxCatalog_0.ObjectChanged(this);
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		public void SearchChildren(string string_0, IGxObjectArray igxObjectArray_1)
		{
		}

		public void SetProperty(string string_0, object object_0)
		{
		}

		public override string ToString()
		{
			return this.FullName;
		}
	}
}