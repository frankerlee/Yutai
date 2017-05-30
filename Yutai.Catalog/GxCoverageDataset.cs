using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Yutai.Catalog.UI;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxCoverageDataset : IGxObject, IGxDataset, IGxObjectContainer, IGxObjectEdit, IGxObjectInternalName, IGxObjectProperties, IGxObjectUI, IGxPasteTarget
	{
		private IGxObjectArray igxObjectArray_0 = new GxObjectArray();

		private IGxObject igxObject_0 = null;

		private IDatasetName idatasetName_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		public bool AreChildrenViewable
		{
			get
			{
				return this.idatasetName_0 is ICoverageName;
			}
		}

		public string BaseName
		{
			get
			{
				return this.idatasetName_0.Name;
			}
		}

		public string Category
		{
			get
			{
				string str;
				if (this.idatasetName_0 is ICoverageName)
				{
					switch (this.method_0())
					{
						case esriCoverageType.esriEmptyCoverage:
						{
							str = "空Coverage";
							break;
						}
						case esriCoverageType.esriAnnotationCoverage:
						{
							str = "注记Coverage";
							break;
						}
						case esriCoverageType.esriPointCoverage:
						{
							str = "点Coverage";
							break;
						}
						case esriCoverageType.esriLineCoverage:
						{
							str = "线Coverage";
							break;
						}
						case esriCoverageType.esriPolygonCoverage:
						{
							str = "面Coverage";
							break;
						}
						case esriCoverageType.esriPreliminaryPolygonCoverage:
						{
							str = "区域Coverage";
							break;
						}
						default:
						{
							str = "Coverage";
							return str;
						}
					}
				}
				else if (this.idatasetName_0 is ICoverageFeatureClassName)
				{
					esriCoverageFeatureClassType featureClassType = (this.idatasetName_0 as ICoverageFeatureClassName).FeatureClassType;
					switch (featureClassType)
					{
						case esriCoverageFeatureClassType.esriCFCTPoint:
						{
							str = "Coverage点要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTArc:
						{
							str = "Arc要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTPolygon:
						{
							str = "Coverage面要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTNode:
						{
							str = "Coverage节点要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTTic:
						{
							str = "Tic要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTAnnotation:
						{
							str = "Coverage注记要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTSection:
						{
							str = "Coverage片要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTRoute:
						{
							str = "路径要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTLink:
						{
							str = "Coverage连接要素类";
							break;
						}
						case esriCoverageFeatureClassType.esriCFCTArc | esriCoverageFeatureClassType.esriCFCTRoute:
						{
							str = "Coverage";
							return str;
						}
						case esriCoverageFeatureClassType.esriCFCTRegion:
						{
							str = "区域要素类";
							break;
						}
						default:
						{
							if (featureClassType == esriCoverageFeatureClassType.esriCFCTLabel)
							{
								str = "Coverage标注要素类";
								break;
							}
							else if (featureClassType == esriCoverageFeatureClassType.esriCFCTFile)
							{
								str = "文件要素类";
								break;
							}
							else
							{
								str = "Coverage";
								return str;
							}
						}
					}
				}
				else
				{
					str = "Coverage";
					return str;
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
				if (this.idatasetName_0 != null)
				{
					try
					{
						dataset = (this.idatasetName_0 as IName).Open() as IDataset;
						return dataset;
					}
					catch (Exception exception)
					{
						//CErrorLog.writeErrorLog(this, exception, "打开");
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
			}
		}

		public string FullName
		{
			get
			{
				string pathName = this.idatasetName_0.WorkspaceName.PathName;
				if (this.idatasetName_0 is ICoverageFeatureClassName)
				{
					IDatasetName featureDatasetName = (this.idatasetName_0 as IFeatureClassName).FeatureDatasetName;
					if (featureDatasetName != null)
					{
						pathName = string.Concat(pathName, "\\", featureDatasetName.Name);
					}
				}
				pathName = string.Concat(pathName, "\\", this.idatasetName_0.Name);
				return pathName;
			}
		}

		public bool HasChildren
		{
			get
			{
				return this.idatasetName_0 is ICoverageName;
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
				return this.idatasetName_0.Name;
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
				if (!(this.idatasetName_0 is ICoverageName))
				{
					if (this.idatasetName_0 is ICoverageFeatureClassName)
					{
						esriCoverageFeatureClassType featureClassType = (this.idatasetName_0 as ICoverageFeatureClassName).FeatureClassType;
						switch (featureClassType)
						{
							case esriCoverageFeatureClassType.esriCFCTPoint:
							{
								smallImage = ImageLib.GetSmallImage(63);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTArc:
							{
								smallImage = ImageLib.GetSmallImage(64);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTPolygon:
							{
								smallImage = ImageLib.GetSmallImage(65);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTNode:
							{
								smallImage = ImageLib.GetSmallImage(66);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTTic:
							{
								smallImage = ImageLib.GetSmallImage(67);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTAnnotation:
							{
								smallImage = ImageLib.GetSmallImage(68);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTSection:
							{
								smallImage = ImageLib.GetSmallImage(72);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTRoute:
							{
								smallImage = ImageLib.GetSmallImage(69);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTLink:
							{
								smallImage = ImageLib.GetSmallImage(72);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTArc | esriCoverageFeatureClassType.esriCFCTRoute:
							{
								break;
							}
							case esriCoverageFeatureClassType.esriCFCTRegion:
							{
								smallImage = ImageLib.GetSmallImage(70);
								return smallImage;
							}
							default:
							{
								if (featureClassType == esriCoverageFeatureClassType.esriCFCTLabel)
								{
									smallImage = ImageLib.GetSmallImage(71);
									return smallImage;
								}
								else if (featureClassType == esriCoverageFeatureClassType.esriCFCTFile)
								{
									smallImage = ImageLib.GetSmallImage(72);
									return smallImage;
								}
								else
								{
									break;
								}
							}
						}
					}
					smallImage = ImageLib.GetSmallImage(57);
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(57 + (int)this.method_0());
				}
				return smallImage;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap smallImage;
				if (!(this.idatasetName_0 is ICoverageName))
				{
					if (this.idatasetName_0 is ICoverageFeatureClassName)
					{
						esriCoverageFeatureClassType featureClassType = (this.idatasetName_0 as ICoverageFeatureClassName).FeatureClassType;
						switch (featureClassType)
						{
							case esriCoverageFeatureClassType.esriCFCTPoint:
							{
								smallImage = ImageLib.GetSmallImage(63);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTArc:
							{
								smallImage = ImageLib.GetSmallImage(64);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTPolygon:
							{
								smallImage = ImageLib.GetSmallImage(65);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTNode:
							{
								smallImage = ImageLib.GetSmallImage(66);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTTic:
							{
								smallImage = ImageLib.GetSmallImage(67);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTAnnotation:
							{
								smallImage = ImageLib.GetSmallImage(68);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTSection:
							{
								smallImage = ImageLib.GetSmallImage(72);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTRoute:
							{
								smallImage = ImageLib.GetSmallImage(69);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTLink:
							{
								smallImage = ImageLib.GetSmallImage(72);
								return smallImage;
							}
							case esriCoverageFeatureClassType.esriCFCTArc | esriCoverageFeatureClassType.esriCFCTRoute:
							{
								break;
							}
							case esriCoverageFeatureClassType.esriCFCTRegion:
							{
								smallImage = ImageLib.GetSmallImage(70);
								return smallImage;
							}
							default:
							{
								if (featureClassType == esriCoverageFeatureClassType.esriCFCTLabel)
								{
									smallImage = ImageLib.GetSmallImage(71);
									return smallImage;
								}
								else if (featureClassType == esriCoverageFeatureClassType.esriCFCTFile)
								{
									smallImage = ImageLib.GetSmallImage(72);
									return smallImage;
								}
								else
								{
									break;
								}
							}
						}
					}
					smallImage = ImageLib.GetSmallImage(57);
				}
				else
				{
					smallImage = ImageLib.GetSmallImage(57 + (int)this.method_0());
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

		public GxCoverageDataset()
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

		public bool CanPaste(IEnumName ienumName_0, ref bool bool_0)
		{
			return false;
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
			if (this.CanDelete())
			{
				try
				{
					this.Dataset.Delete();
					this.Detach();
				}
				catch
				{
				}
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
			frmCoveragePropertySheet _frmCoveragePropertySheet = new frmCoveragePropertySheet()
			{
				CoverageName = this.idatasetName_0 as ICoverageName
			};
			_frmCoveragePropertySheet.ShowDialog();
		}

		public void GetPropByIndex(int int_0, ref string string_0, ref object object_0)
		{
		}

		public object GetProperty(string string_0)
		{
			return null;
		}

		private esriCoverageType method_0()
		{
			esriCoverageType _esriCoverageType;
			IGxDataset gxDataset;
			esriCoverageFeatureClassType featureClassType;
			IEnumGxObject children = this.Children;
			if ((children as IGxObjectArray).Count != 1)
			{
				if ((children as IGxObjectArray).Count == 2)
				{
					children.Reset();
					gxDataset = children.Next() as IGxDataset;
				Label3:
					while (gxDataset != null)
					{
						featureClassType = (gxDataset.DatasetName as ICoverageFeatureClassName).FeatureClassType;
						switch (featureClassType)
						{
							case esriCoverageFeatureClassType.esriCFCTPoint:
							{
								_esriCoverageType = esriCoverageType.esriPointCoverage;
								return _esriCoverageType;
							}
							case esriCoverageFeatureClassType.esriCFCTArc:
							{
								_esriCoverageType = esriCoverageType.esriLineCoverage;
								return _esriCoverageType;
							}
							default:
							{
								if (featureClassType == esriCoverageFeatureClassType.esriCFCTAnnotation)
								{
									break;
								}
								else
								{
                                        gxDataset = children.Next() as IGxDataset;
								    break;
								}
							}
						}
						_esriCoverageType = esriCoverageType.esriAnnotationCoverage;
						return _esriCoverageType;
					}
				}
				else if ((children as IGxObjectArray).Count == 3)
				{
					_esriCoverageType = esriCoverageType.esriLineCoverage;
					return _esriCoverageType;
				}
				else if ((children as IGxObjectArray).Count != 4)
				{
					if ((children as IGxObjectArray).Count <= 4)
					{
                        _esriCoverageType = esriCoverageType.esriEmptyCoverage;
                        return _esriCoverageType;
                    }
					_esriCoverageType = esriCoverageType.esriPolygonCoverage;
					return _esriCoverageType;
				}
				else
				{
					children.Reset();
					gxDataset = children.Next() as IGxDataset;
					while (gxDataset != null)
					{
						featureClassType = (gxDataset.DatasetName as ICoverageFeatureClassName).FeatureClassType;
						if (featureClassType == esriCoverageFeatureClassType.esriCFCTPolygon)
						{
							_esriCoverageType = esriCoverageType.esriPolygonCoverage;
							return _esriCoverageType;
						}
						else if (featureClassType == esriCoverageFeatureClassType.esriCFCTRegion)
						{
							_esriCoverageType = esriCoverageType.esriPreliminaryPolygonCoverage;
							return _esriCoverageType;
						}
						else
						{
							gxDataset = children.Next() as IGxDataset;
						}
					}
				}
		
				_esriCoverageType = esriCoverageType.esriEmptyCoverage;
			}
			else
			{
				_esriCoverageType = esriCoverageType.esriEmptyCoverage;
			}
			return _esriCoverageType;
	
		}

		private void method_1()
		{
			try
			{
				IEnumDatasetName featureClassNames = (this.idatasetName_0 as IFeatureDatasetName2).FeatureClassNames;
				featureClassNames.Reset();
				IDatasetName datasetName = featureClassNames.Next();
				IGxObject gxCoverageDataset = null;
				while (datasetName != null)
				{
					gxCoverageDataset = new GxCoverageDataset();
					if (gxCoverageDataset != null)
					{
						(gxCoverageDataset as IGxDataset).DatasetName = datasetName;
						gxCoverageDataset.Attach(this, this.igxCatalog_0);
					}
					datasetName = featureClassNames.Next();
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "错误");
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
				if ((this.idatasetName_0.Type == esriDatasetType.esriDTContainer ? true : this.idatasetName_0.Type == esriDatasetType.esriDTFeatureDataset))
				{
					this.igxObjectArray_0.Empty();
					this.method_1();
				}
				this.igxCatalog_0.ObjectRefreshed(this);
			}
		}

		public void Rename(string string_0)
		{
			try
			{
				if (string_0 != null)
				{
					this.Dataset.Rename(string_0);
					this.idatasetName_0.Name = string_0;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "错误");
			}
		}

		public void SearchChildren(string string_0, IGxObjectArray igxObjectArray_1)
		{
		}

		public void SetProperty(string string_0, object object_0)
		{
		}
	}
}