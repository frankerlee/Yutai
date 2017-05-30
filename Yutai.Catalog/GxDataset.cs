using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxDataset : IGxObject, IGxDataset, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget, IGxContextMenuWap
	{
		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private bool bool_0 = false;

		private string string_0 = null;

		private string string_1 = null;

		private IGxObject igxObject_0 = null;

		private IDatasetName idatasetName_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private string string_2 = "";

		//private SysGrants sysGrants_0 = new SysGrants(AppConfigInfo.UserID);

		private bool bool_1 = true;

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		public bool AreChildrenViewable
		{
			get
			{
				return ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer ? false : this.idatasetName_0.Type != esriDatasetType.esriDTFeatureDataset) ? false : true);
			}
		}

		public string BaseName
		{
			get
			{
				string str;
				str = (this.idatasetName_0 != null ? this.idatasetName_0.Name : "");
				return str;
			}
		}

		public string Category
		{
			get
			{
				return this.string_2;
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				if (this.HasChildren && this.igxObjectArray_0.Count == 0)
				{
					this.method_2();
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
				if (this.idatasetName_0 != null)
				{
					try
					{
						dataset = (this.idatasetName_0 as IName).Open() as IDataset;
						return dataset;
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						this.bool_1 = false;
						if (!(exception is COMException))
						{
							MessageBox.Show(exception.Message, "错误");
						}
						else
						{
							uint errorCode = (uint)(exception as COMException).ErrorCode;
							if ((errorCode == -2147216558 ? true : errorCode == -2147216072))
							{
								throw new Exception("打开对象类错误!");
							}
							//CErrorLog.writeErrorLog(this, exception, "");
						}
					}
					dataset = null;
				}
				else
				{
					dataset = null;
				}
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
				if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					this.bool_0 = true;
				}
				this.method_1();
				this.string_2 = this.idatasetName_0.Category;
			}
		}

		public string FullName
		{
			get
			{
				string str;
				if (this.idatasetName_0 != null)
				{
					string pathName = this.idatasetName_0.WorkspaceName.PathName;
					if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
					{
						pathName = string.Concat("Database Connections\\", System.IO.Path.GetFileName(pathName));
					}
					if (this.idatasetName_0 is IFeatureClassName)
					{
						IDatasetName featureDatasetName = (this.idatasetName_0 as IFeatureClassName).FeatureDatasetName;
						if (featureDatasetName != null)
						{
							pathName = string.Concat(pathName, "\\", featureDatasetName.Name);
						}
					}
					pathName = string.Concat(pathName, "\\", this.idatasetName_0.Name);
					str = pathName;
				}
				else
				{
					str = "";
				}
				return str;
			}
		}

		public bool HasChildren
		{
			get
			{
				return ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer ? false : this.idatasetName_0.Type != esriDatasetType.esriDTFeatureDataset) ? false : true);
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
				return this.idatasetName_0 != null;
			}
		}

		IName Yutai.Catalog.IGxObjectInternalName.InternalObjectName
		{
			get
			{
				return null;
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
				string str;
				str = (this.idatasetName_0 != null ? this.idatasetName_0.Name : "");
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
				if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset)
				{
					smallImage = ImageLib.GetSmallImage(18);
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
				{
					smallImage = ImageLib.GetSmallImage(19);
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTRelationshipClass)
				{
					smallImage = ImageLib.GetSmallImage(30);
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTTopology)
				{
					smallImage = ImageLib.GetSmallImage(29);
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTGeometricNetwork)
				{
					smallImage = ImageLib.GetSmallImage(38);
				}
				else if (this.idatasetName_0.Type != esriDatasetType.esriDTCadastralFabric)
				{
					if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureClass)
					{
						IFeatureClassName idatasetName0 = this.idatasetName_0 as IFeatureClassName;
						if (idatasetName0.FeatureType != esriFeatureType.esriFTAnnotation)
						{
							esriGeometryType shapeType = idatasetName0.ShapeType;
							switch (shapeType)
							{
								case esriGeometryType.esriGeometryPoint:
								case esriGeometryType.esriGeometryMultipoint:
								{
									smallImage = ImageLib.GetSmallImage(20);
									return smallImage;
								}
								case esriGeometryType.esriGeometryPolyline:
								case esriGeometryType.esriGeometryPath:
								{
									smallImage = ImageLib.GetSmallImage(21);
									return smallImage;
								}
								case esriGeometryType.esriGeometryPolygon:
								{
									smallImage = ImageLib.GetSmallImage(22);
									return smallImage;
								}
								case esriGeometryType.esriGeometryEnvelope:
								{
									break;
								}
								case esriGeometryType.esriGeometryAny:
								{
									smallImage = ImageLib.GetSmallImage(79);
									return smallImage;
								}
								default:
								{
									if (shapeType == esriGeometryType.esriGeometryRay)
									{
										smallImage = ImageLib.GetSmallImage(21);
										return smallImage;
									}
									break;
								}
							}
						}
						else
						{
							smallImage = ImageLib.GetSmallImage(39);
							return smallImage;
						}
					}
					else if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
					{
						smallImage = ImageLib.GetSmallImage(20);
						return smallImage;
					}
					else if (this.idatasetName_0.Type != esriDatasetType.esriDTTin)
					{
						if (this.idatasetName_0.Type != esriDatasetType.esriDTNetworkDataset)
						{
							goto Label1;
						}
						smallImage = ImageLib.GetSmallImage(73);
						return smallImage;
					}
					else
					{
						smallImage = ImageLib.GetSmallImage(47);
						return smallImage;
					}
				Label1:
					smallImage = ImageLib.GetSmallImage(20);
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(78);
				}
				return smallImage;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap smallImage;
				if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset)
				{
					smallImage = ImageLib.GetSmallImage(18);
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTCadastralFabric)
				{
					smallImage = ImageLib.GetSmallImage(78);
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
				{
					smallImage = ImageLib.GetSmallImage(19);
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTRelationshipClass)
				{
					smallImage = ImageLib.GetSmallImage(30);
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTTopology)
				{
					smallImage = ImageLib.GetSmallImage(29);
				}
				else if (this.idatasetName_0.Type != esriDatasetType.esriDTGeometricNetwork)
				{
					if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureClass)
					{
						IFeatureClassName idatasetName0 = this.idatasetName_0 as IFeatureClassName;
						if (idatasetName0.FeatureType != esriFeatureType.esriFTAnnotation)
						{
							esriGeometryType shapeType = idatasetName0.ShapeType;
							switch (shapeType)
							{
								case esriGeometryType.esriGeometryPoint:
								case esriGeometryType.esriGeometryMultipoint:
								{
									smallImage = ImageLib.GetSmallImage(20);
									return smallImage;
								}
								case esriGeometryType.esriGeometryPolyline:
								case esriGeometryType.esriGeometryPath:
								{
									smallImage = ImageLib.GetSmallImage(21);
									return smallImage;
								}
								case esriGeometryType.esriGeometryPolygon:
								{
									smallImage = ImageLib.GetSmallImage(22);
									return smallImage;
								}
								case esriGeometryType.esriGeometryEnvelope:
								{
									break;
								}
								case esriGeometryType.esriGeometryAny:
								{
									smallImage = ImageLib.GetSmallImage(79);
									return smallImage;
								}
								default:
								{
									if (shapeType == esriGeometryType.esriGeometryRay)
									{
										smallImage = ImageLib.GetSmallImage(21);
										return smallImage;
									}
									break;
								}
							}
						}
						else
						{
							smallImage = ImageLib.GetSmallImage(39);
							return smallImage;
						}
					}
					else if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
					{
						smallImage = ImageLib.GetSmallImage(20);
						return smallImage;
					}
					else if (this.idatasetName_0.Type != esriDatasetType.esriDTTin)
					{
						if (this.idatasetName_0.Type != esriDatasetType.esriDTNetworkDataset)
						{
							goto Label1;
						}
						smallImage = ImageLib.GetSmallImage(73);
						return smallImage;
					}
					else
					{
						smallImage = ImageLib.GetSmallImage(47);
						return smallImage;
					}
				Label1:
					smallImage = ImageLib.GetSmallImage(20);
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(38);
				}
				return smallImage;
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

		public GxDataset()
		{
		}

		public IGxObject AddChild(IGxObject igxObject_1)
		{
			IGxObject igxObject1;
			string upper = igxObject_1.Name.ToUpper();
			int num = 0;
			int num1 = 0;
			while (true)
			{
				if (num1 < this.igxObjectArray_0.Count)
				{
					IGxObject gxObject = this.igxObjectArray_0.Item(num1);
					num = gxObject.Name.ToUpper().CompareTo(upper);
					if (num > 0)
					{
						this.igxObjectArray_0.Insert(num1, igxObject_1);
						igxObject1 = igxObject_1;
						break;
					}
					else if (num == 0)
					{
						igxObject1 = gxObject;
						break;
					}
					else
					{
						num1++;
					}
				}
				else
				{
					this.igxObjectArray_0.Insert(-1, igxObject_1);
					igxObject1 = igxObject_1;
					break;
				}
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
			bool flag = false;
			try
			{
				flag = this.Dataset.CanDelete();
			}
			catch
			{
			}
			return flag;
		}

		public bool CanPaste(IEnumName ienumName_0, ref bool bool_2)
		{
			bool flag;
			if (this.idatasetName_0.Type != esriDatasetType.esriDTFeatureDataset)
			{
				bool_2 = false;
				flag = false;
			}
			else
			{
				ienumName_0.Reset();
				IName name = ienumName_0.Next();
				bool flag1 = false;
				while (name != null)
				{
					if (name is IFileName)
					{
						flag = false;
						return flag;
					}
					else
					{
						if (name is IFeatureClassName)
						{
							bool type = (name as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriFileSystemWorkspace;
							flag1 = type;
							if (type)
							{
								if ((name as IDatasetName).WorkspaceName.PathName != this.idatasetName_0.WorkspaceName.PathName)
								{
									bool_2 = false;
								}
								else
								{
									bool_2 = true;
								}
							}
						}
						else if (name is IFeatureDatasetName)
						{
							flag1 = true;
							if ((name as IDatasetName).WorkspaceName.PathName != this.idatasetName_0.WorkspaceName.PathName)
							{
								bool_2 = false;
							}
							else if ((name as IDatasetName).Name != this.idatasetName_0.Name)
							{
								bool_2 = true;
							}
							else
							{
								flag1 = false;
							}
						}
						else if (name is IWorkspaceName)
						{
							flag = false;
							return flag;
						}
						else if (name is ITableName)
						{
							flag = false;
							return flag;
						}
						if (!flag1)
						{
							flag = false;
							return flag;
						}
						else
						{
							name = ienumName_0.Next();
						}
					}
				}
				flag = true;
			}
			return flag;
		}

		public bool CanRename()
		{
			bool flag;
			try
			{
				bool flag1 = this.Dataset.CanRename();
				flag = flag1;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public void Delete()
		{
			try
			{
				this.Detach();
				IDataset dataset = this.Dataset;
				this.CanDelete();
				dataset.Delete();
			}
			catch (COMException cOMException)
			{
				if (cOMException.ErrorCode == -2147220969)
				{
					MessageBox.Show("不是该对象的所有者，无法删除对象");
				}
			}
			catch (Exception exception)
			{
				exception.ToString();
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
				if (this.igxCatalog_0 != null)
				{
					this.igxCatalog_0.ObjectDeleted(this);
				}
				if (this.igxObject_0 is IGxObjectContainer)
				{
					(this.igxObject_0 as IGxObjectContainer).DeleteChild(this);
				}
				this.igxCatalog_0 = null;
				this.igxObject_0 = null;
			}
		}

		public void EditProperties(int int_0)
		{
			//IDataset dataset = this.Dataset;
			//if (dataset != null)
			//{
			//	if (dataset is IFeatureClass)
			//	{
			//		try
			//		{
			//			frmEditObjectClass _frmEditObjectClass = new frmEditObjectClass()
			//			{
			//				ObjectClass = dataset as IObjectClass
			//			};
			//			_frmEditObjectClass.ShowDialog();
			//		}
			//		catch (Exception exception)
			//		{
			//		}
			//	}
			//	else if (dataset is IObjectClass)
			//	{
			//		ObjectClassInfoEdit objectClassInfoEdit = new ObjectClassInfoEdit()
			//		{
			//			ObjectClass = dataset as IObjectClass
			//		};
			//		objectClassInfoEdit.ShowDialog();
			//	}
			//	else if (dataset is IFeatureDataset)
			//	{
			//		frmEditFeatureDataset _frmEditFeatureDataset = new frmEditFeatureDataset()
			//		{
			//			FeatureDataset = dataset as IFeatureDataset
			//		};
			//		_frmEditFeatureDataset.ShowDialog();
			//	}
			//	else if (dataset.Type == esriDatasetType.esriDTTopology)
			//	{
			//		frmPropertySheet _frmPropertySheet = new frmPropertySheet();
			//		_frmPropertySheet.AddPage(new TopologyGeneralPropertyPage());
			//		_frmPropertySheet.AddPage(new TopologyClassesPropertyPage());
			//		_frmPropertySheet.AddPage(new TopologyRulesPropertyPage());
			//		_frmPropertySheet.EditProperties(dataset);
			//	}
			//	else if (dataset.Type == esriDatasetType.esriDTGeometricNetwork)
			//	{
			//		frmGNPropertySheet _frmGNPropertySheet = new frmGNPropertySheet()
			//		{
			//			GeometricNetwork = dataset as IGeometricNetwork
			//		};
			//		_frmGNPropertySheet.ShowDialog();
			//	}
			//	else if (dataset.Type == esriDatasetType.esriDTNetworkDataset)
			//	{
			//		frmNetworkPropertySheet _frmNetworkPropertySheet = new frmNetworkPropertySheet()
			//		{
			//			NetworkDataset = dataset as INetworkDataset
			//		};
			//		_frmNetworkPropertySheet.ShowDialog();
			//	}
			//	dataset = null;
			//}
		}

		~GxDataset()
		{
			if (this.idatasetName_0 != null)
			{
				Marshal.ReleaseComObject(this.idatasetName_0);
			}
			this.idatasetName_0 = null;
			//this.sysGrants_0 = null;
		}

		public void GetPropByIndex(int int_0, ref string string_3, ref object object_0)
		{
		}

		public object GetProperty(string string_3)
		{
			return null;
		}

		public void Init(object object_0)
		{
			this.ipopuMenuWrap_0 = object_0 as IPopuMenuWrap;
			this.ipopuMenuWrap_0.Clear();
			if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset)
			{
				this.ipopuMenuWrap_0.AddItem("CopyItem", false);
				this.ipopuMenuWrap_0.AddItem("PasteItem", false);
				this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
				this.ipopuMenuWrap_0.AddItem("ReName", false);
				this.ipopuMenuWrap_0.AddItem("RefreshItem", true);
				if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					this.ipopuMenuWrap_0.ClearSubItem("ArchivingSubItem");
					this.ipopuMenuWrap_0.AddSubmenuItem("ArchivingSubItem", "管理", "", true);
					this.ipopuMenuWrap_0.AddItem("JLK.Pluge.RegisterAsVersionCommand", "ArchivingSubItem", true);
					this.ipopuMenuWrap_0.AddItem("UnRegisterVersion", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("JLK.Pluge.EnableArchivingCommand", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("JLK.Pluge.DisableArchivingCommand", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("CreateVersionedViewCommand", "ArchivingSubItem", false);
				}
				this.ipopuMenuWrap_0.ClearSubItem("NewBarSubItem");
				this.ipopuMenuWrap_0.AddSubmenuItem("NewBarSubItem", "新建", "", true);
				this.ipopuMenuWrap_0.AddItem("NewFeatureClass", "NewBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("NewRelationClass", "NewBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("NewNetworkDatasetCommand", "NewBarSubItem", true);
				this.ipopuMenuWrap_0.AddItem("NewTopology", "NewBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("NewGeometryNetwork", "NewBarSubItem", false);
				this.ipopuMenuWrap_0.ClearSubItem("ImportBarSubItem");
				this.ipopuMenuWrap_0.AddSubmenuItem("ImportBarSubItem", "导入", "", true);
				this.ipopuMenuWrap_0.AddItem("ImportSingleFeatureClass", "ImportBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("ConvertToMultiFeatureClass", "ImportBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("ImportXY", "ImportBarSubItem", false);
				this.ipopuMenuWrap_0.ClearSubItem("ExportBarSubItem");
				this.ipopuMenuWrap_0.AddSubmenuItem("ExportBarSubItem", "导出", "", true);
				this.ipopuMenuWrap_0.AddItem("MultiFeatureClassExport", "ExportBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("GxObjectProperty", true);
				this.ipopuMenuWrap_0.UpdateUI();
			}
			else if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureClass)
			{
				this.ipopuMenuWrap_0.AddItem("CopyItem", false);
				this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
				this.ipopuMenuWrap_0.AddItem("ReName", false);
				this.ipopuMenuWrap_0.ClearSubItem("ArchivingSubItem");
				this.ipopuMenuWrap_0.AddSubmenuItem("ArchivingSubItem", "管理", "", true);
				if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace && (this.idatasetName_0 as IFeatureClassName).FeatureDatasetName == null)
				{
					this.ipopuMenuWrap_0.AddItem("JLK.Pluge.RegisterAsVersionCommand", "ArchivingSubItem", true);
					this.ipopuMenuWrap_0.AddItem("UnRegisterVersion", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("JLK.Pluge.EnableArchivingCommand", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("JLK.Pluge.DisableArchivingCommand", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("CreateVersionedViewCommand", "ArchivingSubItem", false);
				}
				this.ipopuMenuWrap_0.AddItem("CreateAttachmentsCommand", "ArchivingSubItem", true);
				this.ipopuMenuWrap_0.AddItem("DeleteAttachmentsCommand", "ArchivingSubItem", false);
				if ((this.Dataset as IObjectClass).ObjectClassID == -1)
				{
					this.ipopuMenuWrap_0.AddItem("RegisterAsObjectClass", "ArchivingSubItem", true);
				}
				this.ipopuMenuWrap_0.AddItem("DataLoader", true);
				this.ipopuMenuWrap_0.ClearSubItem("ExportBarSubItem");
				this.ipopuMenuWrap_0.AddSubmenuItem("ExportBarSubItem", "导出", "", true);
				this.ipopuMenuWrap_0.AddItem("ExportSingleToGeodatabase", "ExportBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("ExportSingleToShapefile", "ExportBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("GxObjectProperty", true);
				this.ipopuMenuWrap_0.UpdateUI();
			}
			else if (this.idatasetName_0.Type == esriDatasetType.esriDTTable)
			{
				this.ipopuMenuWrap_0.AddItem("CopyItem", false);
				this.ipopuMenuWrap_0.AddItem("PasteItem", false);
				this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
				this.ipopuMenuWrap_0.AddItem("ReName", false);
				this.ipopuMenuWrap_0.ClearSubItem("ArchivingSubItem");
				this.ipopuMenuWrap_0.AddSubmenuItem("ArchivingSubItem", "管理", "", true);
				this.ipopuMenuWrap_0.AddItem("JLK.Pluge.RegisterAsVersionCommand", "ArchivingSubItem", true);
				if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					this.ipopuMenuWrap_0.AddItem("UnRegisterVersion", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("JLK.Pluge.EnableArchivingCommand", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("JLK.Pluge.DisableArchivingCommand", "ArchivingSubItem", false);
					this.ipopuMenuWrap_0.AddItem("CreateVersionedViewCommand", "ArchivingSubItem", false);
				}
				this.ipopuMenuWrap_0.AddItem("CreateAttachmentsCommand", "ArchivingSubItem", true);
				this.ipopuMenuWrap_0.AddItem("DeleteAttachmentsCommand", "ArchivingSubItem", false);
				if ((this.Dataset as IObjectClass).ObjectClassID == -1)
				{
					this.ipopuMenuWrap_0.AddItem("RegisterAsObjectClass", "ArchivingSubItem", true);
				}
				this.ipopuMenuWrap_0.AddItem("DataLoader", true);
				this.ipopuMenuWrap_0.ClearSubItem("ExportBarSubItem");
				this.ipopuMenuWrap_0.AddSubmenuItem("ExportBarSubItem", "导出", "", true);
				this.ipopuMenuWrap_0.AddItem("ExportSingleToGeodatabase", "ExportBarSubItem", false);
				this.ipopuMenuWrap_0.AddItem("GxObjectProperty", true);
				this.ipopuMenuWrap_0.UpdateUI();
			}
			else if (this.idatasetName_0.Type == esriDatasetType.esriDTTopology)
			{
				this.ipopuMenuWrap_0.AddItem("DeleteObject", false);
				this.ipopuMenuWrap_0.AddItem("ValidateTopologyCommand", true);
				this.ipopuMenuWrap_0.AddItem("GxObjectProperty", true);
				this.ipopuMenuWrap_0.UpdateUI();
			}
			else if (this.idatasetName_0.Type != esriDatasetType.esriDTNetworkDataset)
			{
				this.ipopuMenuWrap_0.AddItem("DeleteObject", false);
				this.ipopuMenuWrap_0.UpdateUI();
			}
			else
			{
				this.ipopuMenuWrap_0.AddItem("DeleteObject", false);
				this.ipopuMenuWrap_0.AddItem("BuildNetworkCommand", true);
				this.ipopuMenuWrap_0.AddItem("GxObjectProperty", true);
				this.ipopuMenuWrap_0.UpdateUI();
			}
		}

		private string method_0(IDatasetName idatasetName_1)
		{
			string category;
			if (idatasetName_1 != null)
			{
				bool flag = false;
				if (idatasetName_1.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					flag = true;
				}
				switch (idatasetName_1.Type)
				{
					case esriDatasetType.esriDTFeatureDataset:
					{
						if (flag)
						{
							category = "SDE要素集";
							break;
						}
						else if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
						{
							category = "个人空间数据库要素集";
							break;
						}
						else
						{
							category = "文件型空间数据库要素集";
							break;
						}
					}
					case esriDatasetType.esriDTFeatureClass:
					{
						if (flag)
						{
							category = "SDE要素类";
							break;
						}
						else if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
						{
							category = "个人空间数据库要素类";
							break;
						}
						else
						{
							category = "文件型空间数据库要素类";
							break;
						}
					}
					case esriDatasetType.esriDTPlanarGraph:
					case esriDatasetType.esriDTText:
					case esriDatasetType.esriDTRasterBand:
					case esriDatasetType.esriDTCadDrawing:
					case esriDatasetType.esriDTRasterCatalog:
					case esriDatasetType.esriDTToolbox:
					case esriDatasetType.esriDTTool:
					case esriDatasetType.esriDTTerrain:
					case esriDatasetType.esriDTRepresentationClass:
					{
						category = "";
						break;
					}
					case esriDatasetType.esriDTGeometricNetwork:
					{
						if (flag)
						{
							category = "SDE几何网络";
							break;
						}
						else if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
						{
							category = "个人空间数据库几何网络";
							break;
						}
						else
						{
							category = "文件型空间数据库几何网络";
							break;
						}
					}
					case esriDatasetType.esriDTTopology:
					{
						if (this.bool_0)
						{
							category = "SDE拓扑";
							break;
						}
						else if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
						{
							category = "个人空间数据库拓扑";
							break;
						}
						else
						{
							category = "文件型空间数据库拓扑";
							break;
						}
					}
					case esriDatasetType.esriDTTable:
					{
						string str = this.DatasetName.Category;
						category = this.Dataset.Category;
						break;
					}
					case esriDatasetType.esriDTRelationshipClass:
					{
						if (flag)
						{
							category = "SDE关系类";
							break;
						}
						else if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
						{
							category = "个人空间数据库关系类";
							break;
						}
						else
						{
							category = "文件型空间数据库关系类";
							break;
						}
					}
					case esriDatasetType.esriDTRasterDataset:
					{
						if (!flag)
						{
							category = "删格数据集";
							break;
						}
						else
						{
							category = "SDE删格数据集";
							break;
						}
					}
					case esriDatasetType.esriDTTin:
					{
						category = "TIN数据集";
						break;
					}
					case esriDatasetType.esriDTNetworkDataset:
					{
						if (flag)
						{
							category = "SDE网络要素集";
							break;
						}
						else if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
						{
							category = "个人空间数据库网络要素集";
							break;
						}
						else
						{
							category = "文件型空间数据库网络要素集";
							break;
						}
					}
					case esriDatasetType.esriDTCadastralFabric:
					{
						if (flag)
						{
							category = "SDE宗地结构";
							break;
						}
						else if (idatasetName_1.WorkspaceName.WorkspaceFactoryProgID != "esriDataSourcesGDB.FileGDBWorkspaceFactory.1")
						{
							category = "个人空间数据库宗地结构";
							break;
						}
						else
						{
							category = "文件型空间数据库宗地结构";
							break;
						}
					}
					default:
					{
						goto case esriDatasetType.esriDTRepresentationClass;
					}
				}
			}
			else
			{
				category = "";
			}
			return category;
		}

		private void method_1()
		{
			this.string_0 = "";
			this.string_1 = "";
			if (this.idatasetName_0 != null)
			{
				string pathName = this.idatasetName_0.WorkspaceName.PathName;
				if (this.idatasetName_0.WorkspaceName.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
				{
					pathName = string.Concat("数据库连接\\", System.IO.Path.GetFileName(pathName));
				}
				this.string_0 = this.idatasetName_0.Name;
				if (this.idatasetName_0 is IFeatureClassName)
				{
					IDatasetName featureDatasetName = (this.idatasetName_0 as IFeatureClassName).FeatureDatasetName;
					if (featureDatasetName != null)
					{
						pathName = string.Concat(pathName, "\\", featureDatasetName.Name);
					}
				}
				this.string_1 = string.Concat(pathName, "\\", this.string_0);
			}
		}

		private void method_2()
		{
			try
			{
				IEnumDatasetName subsetNames = this.idatasetName_0.SubsetNames;
				subsetNames.Reset();
				IDatasetName datasetName = subsetNames.Next();
				IGxObject gxDataset = null;
				while (datasetName != null)
				{
					gxDataset = null;
					if (!this.bool_0)
					{
						gxDataset = new GxDataset();
					}
					//else if (AppConfigInfo.UserID == "admin")
					//{
					//	gxDataset = new GxDataset();
					//}
					//else if (AppConfigInfo.UserID.Length <= 0)
					//{
					//	gxDataset = new GxDataset();
					//}
					//else if (this.sysGrants_0.GetStaffAndRolesLayerPri(AppConfigInfo.UserID, 1, datasetName.Name))
					//{
					//	gxDataset = new GxDataset();
					//}
					if (gxDataset != null)
					{
						(gxDataset as IGxDataset).DatasetName = datasetName;
						gxDataset.Attach(this, this.igxCatalog_0);
					}
					datasetName = subsetNames.Next();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "错误");
			}
		}

		public bool Paste(IEnumName ienumName_0, ref bool bool_2)
		{
			IGxObject gxDataset;
			bool flag;
			IEnumNameMapping enumNameMapping;
			try
			{
				if (!bool_2)
				{
					ienumName_0.Reset();
					IGeoDBDataTransfer geoDBDataTransferClass = new GeoDBDataTransfer();
					geoDBDataTransferClass.GenerateNameMapping(ienumName_0, this.idatasetName_0.WorkspaceName as IName, out enumNameMapping);
					//frmGeoDBDataTransfer _frmGeoDBDataTransfer = new frmGeoDBDataTransfer()
					//{
					//	EnumNameMapping = enumNameMapping,
					//	ToName = this.idatasetName_0 as IName,
					//	GeoDBTransfer = geoDBDataTransferClass
					//};
					//if (_frmGeoDBDataTransfer.ShowDialog() == DialogResult.OK)
					//{
					//	this.Refresh();
					//	flag = true;
					//	return flag;
					//}
				}
				else
				{
					ienumName_0.Reset();
					IName idatasetName0 = ienumName_0.Next();
					if ((idatasetName0 as IDatasetName).WorkspaceName.Type != esriWorkspaceType.esriFileSystemWorkspace && (idatasetName0 as IDatasetName).WorkspaceName.PathName == this.idatasetName_0.WorkspaceName.PathName)
					{
						IDatasetContainer datasetContainer = (this.idatasetName_0 as IName).Open() as IDatasetContainer;
						if (datasetContainer != null)
						{
							while (idatasetName0 != null)
							{
								if (!(idatasetName0 is IFeatureDatasetName))
								{
									datasetContainer.AddDataset(idatasetName0.Open() as IDataset);
									gxDataset = new GxDataset();
									if (idatasetName0 is IFeatureClassName)
									{
										(idatasetName0 as IFeatureClassName).FeatureDatasetName = this.idatasetName_0;
									}
									else if (idatasetName0 is IRelationshipClassName)
									{
										(idatasetName0 as IRelationshipClassName).FeatureDatasetName = this.idatasetName_0;
									}
									else if (idatasetName0 is IGeometricNetworkName)
									{
										(idatasetName0 as IGeometricNetworkName).FeatureDatasetName = this.idatasetName_0;
									}
									(gxDataset as IGxDataset).DatasetName = idatasetName0 as IDatasetName;
									gxDataset.Attach(this, this.igxCatalog_0);
									this.igxCatalog_0.ObjectAdded(gxDataset);
								}
								else
								{
									IEnumDatasetName subsetNames = (idatasetName0 as IDatasetName).SubsetNames;
									subsetNames.Reset();
									for (IDatasetName i = subsetNames.Next(); i != null; i = subsetNames.Next())
									{
										datasetContainer.AddDataset((i as IName).Open() as IDataset);
										gxDataset = new GxDataset();
										if (i is IFeatureClassName)
										{
											(i as IFeatureClassName).FeatureDatasetName = this.idatasetName_0;
										}
										else if (i is IRelationshipClassName)
										{
											(i as IRelationshipClassName).FeatureDatasetName = this.idatasetName_0;
										}
										else if (i is IGeometricNetworkName)
										{
											(i as IGeometricNetworkName).FeatureDatasetName = this.idatasetName_0;
										}
										(gxDataset as IGxDataset).DatasetName = i;
										gxDataset.Attach(this, this.igxCatalog_0);
										this.igxCatalog_0.ObjectAdded(gxDataset);
									}
								}
								idatasetName0 = ienumName_0.Next();
							}
							flag = true;
							return flag;
						}
					}
				}
				flag = false;
				return flag;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				flag = false;
			}
			return flag;
		}

		public void Refresh()
		{
			if (this.igxCatalog_0 != null)
			{
				if ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer ? true : this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset))
				{
					this.igxObjectArray_0.Empty();
					GC.Collect();
					this.method_2();
				}
				this.igxCatalog_0.ObjectRefreshed(this);
			}
		}

		public void Rename(string string_3)
		{
			try
			{
				if (string_3 != null)
				{
					if (string_3.Trim().Length != 0)
					{
						IDataset dataset = this.Dataset;
						dataset.Rename(string_3);
						Marshal.ReleaseComObject(dataset);
						dataset = null;
						this.idatasetName_0.Name = string_3;
						this.Refresh();
						this.method_1();
					}
					else
					{
						MessageBox.Show("必须键入数据名!");
						this.igxCatalog_0.ObjectChanged(this);
						return;
					}
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "错误");
			}
		}

		public void SearchChildren(string string_3, IGxObjectArray igxObjectArray_1)
		{
		}

		public void SetProperty(string string_3, object object_0)
		{
		}

		public override string ToString()
		{
			return this.FullName;
		}
	}
}