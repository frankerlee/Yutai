using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;


using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxShapefileDataset : IGxObject, IGxDataset, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxContextMenuWap
	{
		private IGxObject igxObject_0 = null;

		private IDatasetName idatasetName_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private string string_0 = "";

		private IPopuMenuWrap ipopuMenuWrap_0 = null;

		public string BaseName
		{
			get
			{
				return System.IO.Path.GetFileNameWithoutExtension(this.string_0);
			}
		}

		public string Category
		{
			get
			{
				return (this.idatasetName_0 != null ? "Shapefile" : "错误Shapefile");
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
				catch
				{
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
				return System.IO.Path.GetFileName(this.string_0);
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
					if (this.idatasetName_0 is IFeatureClassName)
					{
						IFeatureClassName idatasetName0 = this.idatasetName_0 as IFeatureClassName;
						esriGeometryType shapeType = ((idatasetName0 as IName).Open() as IFeatureClass).ShapeType;
						switch (shapeType)
						{
							case esriGeometryType.esriGeometryPoint:
							case esriGeometryType.esriGeometryMultipoint:
							{
								smallImage = ImageLib.GetSmallImage(23);
								return smallImage;
							}
							case esriGeometryType.esriGeometryPolyline:
							case esriGeometryType.esriGeometryPath:
							{
								smallImage = ImageLib.GetSmallImage(24);
								return smallImage;
							}
							case esriGeometryType.esriGeometryPolygon:
							{
								smallImage = ImageLib.GetSmallImage(25);
								return smallImage;
							}
							case esriGeometryType.esriGeometryEnvelope:
							{
								break;
							}
							default:
							{
								if (shapeType == esriGeometryType.esriGeometryRay)
								{
									smallImage = ImageLib.GetSmallImage(24);
									return smallImage;
								}
								break;
							}
						}
					}
				}
				catch
				{
				}
				smallImage = ImageLib.GetSmallImage(31);
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
					if (this.idatasetName_0 is IFeatureClassName)
					{
						IFeatureClassName idatasetName0 = this.idatasetName_0 as IFeatureClassName;
						esriGeometryType shapeType = ((idatasetName0 as IName).Open() as IFeatureClass).ShapeType;
						switch (shapeType)
						{
							case esriGeometryType.esriGeometryPoint:
							case esriGeometryType.esriGeometryMultipoint:
							{
								smallImage = ImageLib.GetSmallImage(23);
								return smallImage;
							}
							case esriGeometryType.esriGeometryPolyline:
							case esriGeometryType.esriGeometryPath:
							{
								smallImage = ImageLib.GetSmallImage(24);
								return smallImage;
							}
							case esriGeometryType.esriGeometryPolygon:
							{
								smallImage = ImageLib.GetSmallImage(25);
								return smallImage;
							}
							case esriGeometryType.esriGeometryEnvelope:
							{
								break;
							}
							default:
							{
								if (shapeType == esriGeometryType.esriGeometryRay)
								{
									smallImage = ImageLib.GetSmallImage(24);
									return smallImage;
								}
								break;
							}
						}
					}
				}
				catch
				{
				}
				smallImage = ImageLib.GetSmallImage(31);
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

		public GxShapefileDataset()
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
			return true;
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
				MessageBox.Show(exception.Message);
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
			this.ipopuMenuWrap_0.AddItem("CopyItem", false);
			this.ipopuMenuWrap_0.AddItem("DeleteObject", true);
			this.ipopuMenuWrap_0.AddItem("ReName", false);
			this.ipopuMenuWrap_0.AddItem("GxObjectProperty", true);
			this.ipopuMenuWrap_0.UpdateUI();
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
					if (System.IO.Path.GetExtension(string_1).ToLower() == ".shp")
					{
						string_1 = System.IO.Path.GetFileNameWithoutExtension(string_1);
					}
					this.Dataset.Rename(string_1);
					this.idatasetName_0.Name = string.Concat(string_1, ".shp");
					this.string_0 = string.Concat(this.idatasetName_0.WorkspaceName.PathName, "\\", this.idatasetName_0.Name);
					this.igxCatalog_0.ObjectChanged(this);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
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