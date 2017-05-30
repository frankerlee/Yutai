using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxCadDataset : IGxObject, IGxDataset, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget
	{
		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private IDatasetName idatasetName_0 = null;

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private string string_0 = "";

		public bool AreChildrenViewable
		{
			get
			{
				return false;
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
				if (this.idatasetName_0 == null)
				{
					str = "错误";
				}
				else if (this.idatasetName_0.Type == esriDatasetType.esriDTCadDrawing)
				{
					str = "CAD要素集";
				}
				else if ((this.idatasetName_0 as IFeatureClassName).FeatureType != esriFeatureType.esriFTCoverageAnnotation)
				{
					string name = this.Name;
					if (name != null)
					{
						if (name == "Annotation")
						{
							str = "CAD注记要素类";
							return str;
						}
						else if (name == "Point")
						{
							str = "CAD点要素类";
							return str;
						}
						else if (name == "Polyline")
						{
							str = "CAD多义线要素类";
							return str;
						}
						else if (name == "Polygon")
						{
							str = "CAD多边形要素类";
							return str;
						}
						else
						{
							if (name != "MultiPatch")
							{
								goto Label1;
							}
							str = "CAD多面要素类";
							return str;
						}
					}
				Label1:
					str = "CAD";
				}
				else
				{
					str = "CAD注记要素类";
				}
				return str;
			}
		}

		public IEnumGxObject Children
		{
			get
			{
				if (this.HasChildren && this.igxObjectArray_0.Count == 0)
				{
					this.method_1();
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
					//CErrorLog.writeErrorLog(null, exception, "");
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
				this.string_0 = string.Concat(this.idatasetName_0.WorkspaceName.PathName, "\\", this.idatasetName_0.Name);
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
				return ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer ? false : this.idatasetName_0.Type != esriDatasetType.esriDTCadDrawing) ? false : true);
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
				try
				{
					if (this.idatasetName_0.Type != esriDatasetType.esriDTCadDrawing)
					{
						if (this.idatasetName_0.Type == esriDatasetType.esriDTFeatureClass)
						{
							if ((this.idatasetName_0 as IFeatureClassName).FeatureType != esriFeatureType.esriFTCoverageAnnotation)
							{
								string name = this.Name;
								if (name != null)
								{
									if (name == "Annotation")
									{
										smallImage = ImageLib.GetSmallImage(44);
										return smallImage;
									}
									else if (name == "Point")
									{
										smallImage = ImageLib.GetSmallImage(41);
										return smallImage;
									}
									else if (name == "Polyline")
									{
										smallImage = ImageLib.GetSmallImage(42);
										return smallImage;
									}
									else if (name == "Polygon")
									{
										smallImage = ImageLib.GetSmallImage(43);
										return smallImage;
									}
									else if (name == "MultiPatch")
									{
										smallImage = ImageLib.GetSmallImage(45);
										return smallImage;
									}
								}
							}
							else
							{
								smallImage = ImageLib.GetSmallImage(44);
								return smallImage;
							}
						}
						smallImage = ImageLib.GetSmallImage(40);
						return smallImage;
					}
					else
					{
						smallImage = ImageLib.GetSmallImage(40);
					}
				}
				catch (Exception exception)
				{
					//CErrorLog.writeErrorLog(null, exception, "");
					smallImage = ImageLib.GetSmallImage(40);
					return smallImage;
				}
				return smallImage;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap smallImage;
				try
				{
					if (this.idatasetName_0.Type != esriDatasetType.esriDTCadDrawing)
					{
						if (this.idatasetName_0 is IFeatureClassName)
						{
							if ((this.idatasetName_0 as IFeatureClassName).FeatureType != esriFeatureType.esriFTCoverageAnnotation)
							{
								string name = this.Name;
								if (name != null)
								{
									if (name == "Annotation")
									{
										smallImage = ImageLib.GetSmallImage(44);
										return smallImage;
									}
									else if (name == "Point")
									{
										smallImage = ImageLib.GetSmallImage(41);
										return smallImage;
									}
									else if (name == "Polyline")
									{
										smallImage = ImageLib.GetSmallImage(42);
										return smallImage;
									}
									else if (name == "Polygon")
									{
										smallImage = ImageLib.GetSmallImage(43);
										return smallImage;
									}
									else if (name == "MultiPatch")
									{
										smallImage = ImageLib.GetSmallImage(45);
										return smallImage;
									}
								}
							}
							else
							{
								smallImage = ImageLib.GetSmallImage(44);
								return smallImage;
							}
						}
						smallImage = ImageLib.GetSmallImage(40);
						return smallImage;
					}
					else
					{
						smallImage = ImageLib.GetSmallImage(40);
					}
				}
				catch (Exception exception)
				{
					//CErrorLog.writeErrorLog(null, exception, "");
					smallImage = ImageLib.GetSmallImage(40);
					return smallImage;
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

		public GxCadDataset()
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
			bool flag;
			try
			{
				IDataset dataset = this.Dataset;
				if (dataset != null)
				{
					bool flag1 = false;
					flag1 = dataset.CanCopy();
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

		public bool CanDelete()
		{
			bool flag;
			try
			{
				IDataset dataset = this.Dataset;
				if (dataset != null)
				{
					bool flag1 = false;
					flag1 = dataset.CanDelete();
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

		public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
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
				if (dataset != null)
				{
					dataset.Delete();
					dataset = null;
					this.igxCatalog_0.ObjectDeleted(this);
				}
			}
			catch (Exception exception)
			{
				//CErrorLog.writeErrorLog(null, exception, "");
			}
		}

		public void DeleteChild(IGxObject igxObject_1)
		{
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
			IDataset dataset = this.Dataset;
			if (dataset != null)
			{
				
			}
		}

		public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
		{
		}

		public object GetProperty(string string_1)
		{
			return null;
		}

		private void method_0(string string_1)
		{
			try
			{
				IGxObject gxCadDataset = new GxCadDataset();
				IDatasetName featureClassNameClass = new FeatureClassName() as IDatasetName;
			    IWorkspaceName workspaceNameClass = new WorkspaceName() as IWorkspaceName;

                workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory";
			    workspaceNameClass.PathName = this.idatasetName_0.WorkspaceName.PathName;
			
				featureClassNameClass.Name = string.Concat(this.idatasetName_0.Name, ":", string_1);
				featureClassNameClass.WorkspaceName = workspaceNameClass;
				(gxCadDataset as IGxDataset).DatasetName = featureClassNameClass;
				gxCadDataset.Attach(this, this.igxCatalog_0);
			}
			catch (Exception exception)
			{
				//CErrorLog.writeErrorLog(null, exception, "");
			}
		}

		private void method_1()
		{
			IGxObject gxCadDrawing = new GxCadDrawing();
		    IDatasetName cadDrawingNameClass = new CadDrawingName() as IDatasetName;
            IWorkspaceName workspaceNameClass = new WorkspaceName() as IWorkspaceName;
		    workspaceNameClass.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory";
		    workspaceNameClass.PathName = Path.GetDirectoryName(this.string_0);
			cadDrawingNameClass.Name = Path.GetFileName(this.string_0);
			cadDrawingNameClass.WorkspaceName = workspaceNameClass;
			(gxCadDrawing as IGxDataset).DatasetName = cadDrawingNameClass;
			gxCadDrawing.Attach(this, this.igxCatalog_0);
			this.method_0("Annotation");
			this.method_0("Point");
			this.method_0("Polyline");
			this.method_0("Polygon");
			this.method_0("MultiPatch");
		}

		public bool Paste(IEnumName ienumName_0, ref bool bool_0)
		{
			return false;
		}

		public void Refresh()
		{
		}

		public void Rename(string string_1)
		{
			try
			{
				if (string_1 != null && this.Dataset != null)
				{
					if (Path.GetFileNameWithoutExtension(string_1).Trim().Length != 0)
					{
						this.Dataset.Rename(string_1);
						this.idatasetName_0.Name = string_1;
						this.string_0 = string.Concat(this.idatasetName_0.WorkspaceName.PathName, "\\", this.idatasetName_0.Name);
						this.igxCatalog_0.ObjectChanged(this);
					}
					else
					{
						MessageBox.Show("必须键入文件名!");
						this.igxCatalog_0.ObjectChanged(this);
						return;
					}
				}
			}
			catch (Exception exception)
			{
				//CErrorLog.writeErrorLog(null, exception, "");
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