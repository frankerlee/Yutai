using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxLayer : IGxObject, IGxLayer, IGxFile, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI
	{
		private string string_0 = "";

		private ILayer ilayer_0 = null;

		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

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
				return "图层";
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
				return this.string_0;
			}
		}

		public IName InternalObjectName
		{
			get
			{
			    IFileName fileName = new FileName() as IFileName;
			    fileName.Path = this.string_0;
			    return fileName as IName;
				
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
                IFileName fileName = new FileName() as IFileName;
			    fileName.Path = this.string_0;
			    return (IName) fileName;
				
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

		public ILayer Layer
		{
			get
			{
				ILayer layer;
				layer = (this.ilayer_0 == null ? this.method_1(this.string_0) : this.ilayer_0);
				return layer;
			}
			set
			{
				this.ilayer_0 = value;
			}
		}

		public Yutai.Catalog.LayerType LayerType
		{
			get
			{
				Yutai.Catalog.LayerType layerType;
				if (this.Layer == null)
				{
					layerType = Yutai.Catalog.LayerType.UnknownLayer;
				}
				else if (this.Layer is IGroupLayer)
				{
					layerType = Yutai.Catalog.LayerType.GroupLayer;
				}
				else if (this.Layer is IRasterLayer)
				{
					layerType = Yutai.Catalog.LayerType.RasterLayer;
				}
				else if (!(this.Layer is IFeatureLayer))
				{
					layerType = Yutai.Catalog.LayerType.UnknownLayer;
				}
				else
				{
					IFeatureClass featureClass = (this.Layer as IFeatureLayer).FeatureClass;
					layerType = (featureClass != null ? this.method_0(featureClass) : Yutai.Catalog.LayerType.UnknownLayer);
				}
				return layerType;
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

		public string Path
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
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
				IFeatureClass featureClass;
				esriGeometryType shapeType;
				try
				{
					if (this.ilayer_0 == null)
					{
						ILayer layer = this.method_1(this.string_0);
						if (layer != null)
						{
							if (layer is IGroupLayer)
							{
								smallImage = ImageLib.GetSmallImage(35);
								return smallImage;
							}
							else if (layer is IRasterLayer)
							{
								smallImage = ImageLib.GetSmallImage(36);
								return smallImage;
							}
							else if (layer is IFeatureLayer)
							{
								featureClass = (layer as IFeatureLayer).FeatureClass;
								if (featureClass != null)
								{
									shapeType = featureClass.ShapeType;
									switch (shapeType)
									{
										case esriGeometryType.esriGeometryPoint:
										case esriGeometryType.esriGeometryMultipoint:
										{
											smallImage = ImageLib.GetSmallImage(32);
											return smallImage;
										}
										case esriGeometryType.esriGeometryPolyline:
										case esriGeometryType.esriGeometryPath:
										{
											smallImage = ImageLib.GetSmallImage(33);
											return smallImage;
										}
										case esriGeometryType.esriGeometryPolygon:
										{
											smallImage = ImageLib.GetSmallImage(34);
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
												smallImage = ImageLib.GetSmallImage(33);
												return smallImage;
											}
											break;
										}
									}
								}
								featureClass = null;
							}
						}
					}
					else if (this.ilayer_0 is IGroupLayer)
					{
						smallImage = ImageLib.GetSmallImage(35);
						return smallImage;
					}
					else if (this.ilayer_0 is IRasterLayer)
					{
						smallImage = ImageLib.GetSmallImage(36);
						return smallImage;
					}
					else if (this.ilayer_0 is IFeatureLayer)
					{
						featureClass = (this.ilayer_0 as IFeatureLayer).FeatureClass;
						if (featureClass != null)
						{
							shapeType = featureClass.ShapeType;
							switch (shapeType)
							{
								case esriGeometryType.esriGeometryPoint:
								case esriGeometryType.esriGeometryMultipoint:
								{
									smallImage = ImageLib.GetSmallImage(32);
									return smallImage;
								}
								case esriGeometryType.esriGeometryPolyline:
								case esriGeometryType.esriGeometryPath:
								{
									smallImage = ImageLib.GetSmallImage(33);
									return smallImage;
								}
								case esriGeometryType.esriGeometryPolygon:
								{
									smallImage = ImageLib.GetSmallImage(34);
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
										smallImage = ImageLib.GetSmallImage(33);
										return smallImage;
									}
									break;
								}
							}
						}
					}
					smallImage = ImageLib.GetSmallImage(37);
					return smallImage;
				}
				catch
				{
					smallImage = ImageLib.GetSmallImage(37);
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
				IFeatureClass featureClass;
				esriGeometryType shapeType;
				try
				{
					if (this.ilayer_0 == null)
					{
						ILayer layer = this.method_1(this.string_0);
						if (layer != null)
						{
							if (layer is IGroupLayer)
							{
								smallImage = ImageLib.GetSmallImage(35);
								return smallImage;
							}
							else if (layer is IRasterLayer)
							{
								smallImage = ImageLib.GetSmallImage(36);
								return smallImage;
							}
							else if (layer is IFeatureLayer)
							{
								featureClass = (layer as IFeatureLayer).FeatureClass;
								if (featureClass != null)
								{
									shapeType = featureClass.ShapeType;
									switch (shapeType)
									{
										case esriGeometryType.esriGeometryPoint:
										case esriGeometryType.esriGeometryMultipoint:
										{
											smallImage = ImageLib.GetSmallImage(32);
											return smallImage;
										}
										case esriGeometryType.esriGeometryPolyline:
										case esriGeometryType.esriGeometryPath:
										{
											smallImage = ImageLib.GetSmallImage(33);
											return smallImage;
										}
										case esriGeometryType.esriGeometryPolygon:
										{
											smallImage = ImageLib.GetSmallImage(34);
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
												smallImage = ImageLib.GetSmallImage(33);
												return smallImage;
											}
											break;
										}
									}
								}
								featureClass = null;
							}
						}
					}
					else if (this.ilayer_0 is IGroupLayer)
					{
						smallImage = ImageLib.GetSmallImage(35);
						return smallImage;
					}
					else if (this.ilayer_0 is IRasterLayer)
					{
						smallImage = ImageLib.GetSmallImage(36);
						return smallImage;
					}
					else if (this.ilayer_0 is IFeatureLayer)
					{
						featureClass = (this.ilayer_0 as IFeatureLayer).FeatureClass;
						if (featureClass != null)
						{
							shapeType = featureClass.ShapeType;
							switch (shapeType)
							{
								case esriGeometryType.esriGeometryPoint:
								case esriGeometryType.esriGeometryMultipoint:
								{
									smallImage = ImageLib.GetSmallImage(32);
									return smallImage;
								}
								case esriGeometryType.esriGeometryPolyline:
								case esriGeometryType.esriGeometryPath:
								{
									smallImage = ImageLib.GetSmallImage(33);
									return smallImage;
								}
								case esriGeometryType.esriGeometryPolygon:
								{
									smallImage = ImageLib.GetSmallImage(34);
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
										smallImage = ImageLib.GetSmallImage(33);
										return smallImage;
									}
									break;
								}
							}
						}
					}
					smallImage = ImageLib.GetSmallImage(37);
					return smallImage;
				}
				catch
				{
					smallImage = ImageLib.GetSmallImage(37);
					return smallImage;
				}
				return smallImage;
			}
		}

		public GxLayer()
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
			return true;
		}

		public bool CanRename()
		{
			return true;
		}

		public void Close(bool bool_0)
		{
		}

		public void Delete()
		{
			try
			{
				File.Delete(this.string_0);
				this.Detach();
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

		public void Edit()
		{
		}

		public void EditProperties(int int_0)
		{
		}

		public void GetPropByIndex(int int_0, ref string string_1, ref object object_0)
		{
		}

		public object GetProperty(string string_1)
		{
			return null;
		}

		private Yutai.Catalog.LayerType method_0(IFeatureClass ifeatureClass_0)
		{
			Yutai.Catalog.LayerType layerType;
			if (ifeatureClass_0.FeatureType != esriFeatureType.esriFTAnnotation)
			{
				switch (ifeatureClass_0.ShapeType)
				{
					case esriGeometryType.esriGeometryPoint:
					{
						layerType = Yutai.Catalog.LayerType.PointLayer;
						break;
					}
					case esriGeometryType.esriGeometryMultipoint:
					{
						layerType = Yutai.Catalog.LayerType.PointLayer;
						break;
					}
					case esriGeometryType.esriGeometryPolyline:
					{
						layerType = Yutai.Catalog.LayerType.LineLayer;
						break;
					}
					case esriGeometryType.esriGeometryPolygon:
					{
						layerType = Yutai.Catalog.LayerType.PolygonLayer;
						break;
					}
					default:
					{
						layerType = Yutai.Catalog.LayerType.UnknownLayer;
						break;
					}
				}
			}
			else
			{
				layerType = Yutai.Catalog.LayerType.AnnoLayer;
			}
			return layerType;
		}

		private ILayer method_1(string string_1)
		{
			ILayer layer;
            IFileName fileName = new FileName() as IFileName;
            fileName.Path = string_1;
            
            ILayerFactoryHelper layerFactoryHelperClass = new LayerFactoryHelper();
			try
			{
				IEnumLayer enumLayer = layerFactoryHelperClass.CreateLayersFromName(fileName as IName);
				enumLayer.Reset();
				layer = enumLayer.Next();
				return layer;
			}
			catch (Exception exception)
			{
				//CErrorLog.writeErrorLog(null, exception, "");
			}
			layer = null;
			return layer;
		}

		public void New()
		{
		}

		public void Open()
		{
		}

		public void Refresh()
		{
		}

		public void Rename(string string_1)
		{
			try
			{
				if (string_1 != null)
				{
					string extension = System.IO.Path.GetExtension(this.string_0);
					if (extension != null)
					{
						string_1 = string.Concat(System.IO.Path.GetFileNameWithoutExtension(string_1), extension);
					}
					string_1 = string.Concat(System.IO.Path.GetDirectoryName(this.string_0), string_1);
					File.Move(this.string_0, string_1);
					this.string_0 = string_1;
					this.igxCatalog_0.ObjectChanged(this);
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		public void Save()
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