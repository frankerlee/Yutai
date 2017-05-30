using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Catalog.VCT;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Yutai.Catalog
{
	public class GxVCTLayerObject : IGxObject, IGxLayer, IGxObjectUI, IGxVCTLayerObject
	{
		private IGxObject igxObject_0 = null;

		private IGxCatalog igxCatalog_0 = null;

		private ICoConvert icoConvert_0 = null;

		private ILayer ilayer_0 = null;

		public string BaseName { get; set; }

	

		public string Category { get; set; }

		

		public UID ClassID
		{
			get
			{
				return null;
			}
		}

		public string FullName { get; set; }

		

		public IName InternalObjectName
		{
			get
			{
				return null;
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
				Bitmap smallImage;
				string layerTypeName = this.LayerTypeName;
				if (layerTypeName == null)
				{
					smallImage = ImageLib.GetSmallImage(35);
					return smallImage;
				}
				else if (layerTypeName == "Point")
				{
					smallImage = ImageLib.GetSmallImage(32);
				}
				else if (layerTypeName == "line")
				{
					smallImage = ImageLib.GetSmallImage(33);
				}
				else if (layerTypeName == "Polygon")
				{
					smallImage = ImageLib.GetSmallImage(34);
				}
				else
				{
					if (layerTypeName != "Text")
					{
						smallImage = ImageLib.GetSmallImage(35);
						return smallImage;
					}
					smallImage = ImageLib.GetSmallImage(34);
				}
				return smallImage;
			}
		}

		public Bitmap LargeSelectedImage
		{
			get
			{
				Bitmap smallImage;
				string layerTypeName = this.LayerTypeName;
				if (layerTypeName == null)
				{
					smallImage = ImageLib.GetSmallImage(35);
					return smallImage;
				}
				else if (layerTypeName == "Point")
				{
					smallImage = ImageLib.GetSmallImage(32);
				}
				else if (layerTypeName == "line")
				{
					smallImage = ImageLib.GetSmallImage(33);
				}
				else if (layerTypeName == "Polygon")
				{
					smallImage = ImageLib.GetSmallImage(34);
				}
				else
				{
					if (layerTypeName != "Text")
					{
						smallImage = ImageLib.GetSmallImage(35);
						return smallImage;
					}
					smallImage = ImageLib.GetSmallImage(34);
				}
				return smallImage;
			}
		}

		public ILayer Layer
		{
			get
			{
				if (this.ilayer_0 == null)
				{
					this.method_0();
				}
				return this.ilayer_0;
			}
			set
			{
				this.ilayer_0 = value;
			}
		}

		public Yutai.Catalog.LayerType LayerType { get; set; }

		

		public string LayerTypeName
		{
			get
			{
				CoLayerType layerType = this.icoConvert_0.XpgisLayer.LayerType;
				string str = "Point";
				if (layerType == CoLayerType.Point)
				{
					str = "Point";
				}
				else if (layerType == CoLayerType.Line)
				{
					str = "line";
				}
				else if (layerType == CoLayerType.Region)
				{
					str = "Polygon";
				}
				else if (layerType == CoLayerType.Annotation)
				{
					str = "Text";
				}
				return str;
			}
		}

		public string Name { get; set; }

		

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
				Bitmap smallImage;
				string layerTypeName = this.LayerTypeName;
				if (layerTypeName == null)
				{
					this.LayerType = Yutai.Catalog.LayerType.UnknownLayer;
					smallImage = ImageLib.GetSmallImage(35);
					return smallImage;
				}
				else if (layerTypeName == "Point")
				{
					this.LayerType = Yutai.Catalog.LayerType.PointLayer;
					smallImage = ImageLib.GetSmallImage(32);
				}
				else if (layerTypeName == "line")
				{
					this.LayerType = Yutai.Catalog.LayerType.LineLayer;
					smallImage = ImageLib.GetSmallImage(33);
				}
				else if (layerTypeName == "Polygon")
				{
					this.LayerType = Yutai.Catalog.LayerType.PolygonLayer;
					smallImage = ImageLib.GetSmallImage(34);
				}
				else
				{
					if (layerTypeName != "Text")
					{
						this.LayerType = Yutai.Catalog.LayerType.UnknownLayer;
						smallImage = ImageLib.GetSmallImage(35);
						return smallImage;
					}
					this.LayerType = Yutai.Catalog.LayerType.AnnoLayer;
					smallImage = ImageLib.GetSmallImage(34);
				}
				return smallImage;
			}
		}

		public Bitmap SmallSelectedImage
		{
			get
			{
				Bitmap smallImage;
				string layerTypeName = this.LayerTypeName;
				if (layerTypeName == null)
				{
					this.LayerType = Yutai.Catalog.LayerType.UnknownLayer;
					smallImage = ImageLib.GetSmallImage(35);
					return smallImage;
				}
				else if (layerTypeName == "Point")
				{
					this.LayerType = Yutai.Catalog.LayerType.PointLayer;
					smallImage = ImageLib.GetSmallImage(32);
				}
				else if (layerTypeName == "line")
				{
					this.LayerType = Yutai.Catalog.LayerType.LineLayer;
					smallImage = ImageLib.GetSmallImage(33);
				}
				else if (layerTypeName == "Polygon")
				{
					this.LayerType = Yutai.Catalog.LayerType.PolygonLayer;
					smallImage = ImageLib.GetSmallImage(34);
				}
				else
				{
					if (layerTypeName != "Text")
					{
						this.LayerType = Yutai.Catalog.LayerType.UnknownLayer;
						smallImage = ImageLib.GetSmallImage(35);
						return smallImage;
					}
					this.LayerType = Yutai.Catalog.LayerType.AnnoLayer;
					smallImage = ImageLib.GetSmallImage(34);
				}
				return smallImage;
			}
		}

		public object VCTLayer
		{
			get
			{
				return this.icoConvert_0;
			}
			set
			{
				this.icoConvert_0 = value as ICoConvert;
				this.Name = this.icoConvert_0.XpgisLayer.AliasName;
				this.BaseName = this.icoConvert_0.XpgisLayer.AliasName;
			}
		}

		public GxVCTLayerObject()
		{
			this.Category = "VCT图层";
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

		public void Detach()
		{
			if (this.igxCatalog_0 != null)
			{
				this.igxCatalog_0.ObjectDeleted(this);
			}
			this.igxObject_0 = null;
			this.igxCatalog_0 = null;
		}

		private void method_0()
		{
			IFeatureLayer fDOGraphicsLayerClass;
			int featureCount = this.icoConvert_0.FeatureCount;
			GxVCTObject parent = this.Parent as GxVCTObject;
			frmProgressBar1 _frmProgressBar1 = new frmProgressBar1();
			try
			{
				IFeatureClass featureClass = null;
				CoLayerType layerType = this.icoConvert_0.XpgisLayer.LayerType;
				string str = "Point";
				if (layerType == CoLayerType.Point)
				{
					str = "Point";
				}
				else if (layerType == CoLayerType.Line)
				{
					str = "line";
				}
				else if (layerType == CoLayerType.Region)
				{
					str = "Polygon";
				}
				else if (layerType == CoLayerType.Annotation)
				{
					str = "Text";
				}
				ICoConvert geodatabaseLayerClass = null;
				if (str == "Text")
				{
					IFieldsEdit fieldsClass = null;
				    fieldsClass = new Fields() as IFieldsEdit;
					foreach (ICoField field in this.icoConvert_0.XpgisLayer.Fields)
					{
					    IFieldEdit fieldClass = new Field() as IFieldEdit;
						
                            fieldClass.Name_2 = field.Name;
							fieldClass.AliasName_2 = field.AliasName;
							fieldClass.DefaultValue_2 = field.DefaultValue;
						    fieldClass.Editable_2 = field.Enable;
						
						CoFieldType type = field.Type;
						switch (type)
						{
							case CoFieldType.整型:
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
								goto case CoFieldType.整型 | CoFieldType.浮点型 | CoFieldType.字符型 | CoFieldType.日期型;
							}
							case (CoFieldType)2:
							case (CoFieldType)6:
							case CoFieldType.整型 | CoFieldType.浮点型 | CoFieldType.字符型 | CoFieldType.日期型:
							{
								fieldsClass.AddField(fieldClass);
								continue;
							}
							case CoFieldType.浮点型:
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeSingle;
								fieldClass.Precision_2 = field.Precision;
								fieldClass.Scale_2 = field.Length;
								goto case CoFieldType.整型 | CoFieldType.浮点型 | CoFieldType.字符型 | CoFieldType.日期型;
							}
							case CoFieldType.字符型:
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeString;
								fieldClass.Length_2 = field.Length;
								goto case CoFieldType.整型 | CoFieldType.浮点型 | CoFieldType.字符型 | CoFieldType.日期型;
							}
							case CoFieldType.日期型:
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeDate;
								goto case CoFieldType.整型 | CoFieldType.浮点型 | CoFieldType.字符型 | CoFieldType.日期型;
							}
							case CoFieldType.二进制:
							{
								fieldClass.Type_2 = esriFieldType.esriFieldTypeBlob;
								goto case CoFieldType.整型 | CoFieldType.浮点型 | CoFieldType.字符型 | CoFieldType.日期型;
							}
							default:
							{
								if (type == CoFieldType.布尔型)
								{
									fieldClass.Type_2 = esriFieldType.esriFieldTypeInteger;
									goto case CoFieldType.整型 | CoFieldType.浮点型 | CoFieldType.字符型 | CoFieldType.日期型;
								}
								else
								{
									goto case CoFieldType.整型 | CoFieldType.浮点型 | CoFieldType.字符型 | CoFieldType.日期型;
								}
							}
						}
					}
					featureClass = this.method_2(parent.GetTemplateTextWorksoace() as IFeatureWorkspace, this.icoConvert_0.XpgisLayer.Name, esriFeatureType.esriFTAnnotation, esriGeometryType.esriGeometryPolygon, fieldsClass);
					geodatabaseLayerClass = new GeodatabaseLayerClass(featureClass);
				}
				else
				{
					geodatabaseLayerClass = new ShapeLayerClass(string.Concat(parent.GetTemplatePath(), "\\", this.icoConvert_0.XpgisLayer.Name, ".shp"), str, this.icoConvert_0.XpgisLayer);
				}
				ConvertHander convertHander = new ConvertHander();
				_frmProgressBar1.Text = "打开数据";
				_frmProgressBar1.Show();
				_frmProgressBar1.progressBar1.Maximum = featureCount;
				_frmProgressBar1.progressBar1.Value = 0;
				Label caption1 = _frmProgressBar1.Caption1;
				string[] aliasName = new string[] { "正在载入图层:", this.icoConvert_0.XpgisLayer.AliasName, " [要素总数:", featureCount.ToString(), "]" };
				caption1.Text = string.Concat(aliasName);
				convertHander.Convert(this.icoConvert_0, geodatabaseLayerClass, _frmProgressBar1.progressBar1);
				string name = this.icoConvert_0.XpgisLayer.Name;
				geodatabaseLayerClass.Dispose();
				if (featureClass == null)
				{
					featureClass = (parent.GetTemplateShapefile() as IFeatureWorkspace).OpenFeatureClass(name);
				}
				if (featureClass.FeatureType == esriFeatureType.esriFTAnnotation)
				{
				    fDOGraphicsLayerClass = new FDOGraphicsLayer() as IFeatureLayer;
				   
				        fDOGraphicsLayerClass.Cached = true;
				        fDOGraphicsLayerClass.FeatureClass = featureClass;
				        fDOGraphicsLayerClass.Name = featureClass.AliasName;
				   
				}
				else if (featureClass.FeatureType != esriFeatureType.esriFTDimension)
				{
					fDOGraphicsLayerClass = new FeatureLayer()
					{
						Cached = true,
						FeatureClass = featureClass,
						Name = featureClass.AliasName
					};
				}
				else
				{
					fDOGraphicsLayerClass = new DimensionLayer() as IFeatureLayer;

                    fDOGraphicsLayerClass.Cached = true;
                    fDOGraphicsLayerClass.FeatureClass = featureClass;
                    fDOGraphicsLayerClass.Name = featureClass.AliasName;
                   
				}
				this.ilayer_0 = fDOGraphicsLayerClass;
				_frmProgressBar1.Close();
			}
			catch (Exception exception)
			{
			}
		}

		private IFeatureClass method_1(IFeatureWorkspace ifeatureWorkspace_0, string string_4, double double_0, ITextSymbol itextSymbol_0, IFields ifields_0)
		{
			IFeatureClass featureClass;
			IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescription();
			IFeatureClassDescription featureClassDescription = annotationFeatureClassDescriptionClass as IFeatureClassDescription;
			IFields field = (annotationFeatureClassDescriptionClass.RequiredFields as IClone).Clone() as IFields;
			int num = field.FindField(featureClassDescription.ShapeFieldName);
			IGeometryDefEdit geometryDef = (field.Field[num] as IFieldEdit).GeometryDef as IGeometryDefEdit;
			IFeatureWorkspaceAnno ifeatureWorkspace0 = ifeatureWorkspace_0 as IFeatureWorkspaceAnno;
			IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale()
			{
				ReferenceScale = double_0,
				Units = esriUnits.esriMeters
			};
			UID instanceCLSID = annotationFeatureClassDescriptionClass.InstanceCLSID;
			UID classExtensionCLSID = annotationFeatureClassDescriptionClass.ClassExtensionCLSID;
			ISymbolCollection symbolCollectionClass = new SymbolCollection();
            symbolCollectionClass.Symbol[0] = itextSymbol_0 as ISymbol;
			IAnnotateLayerPropertiesCollection2 annotateLayerPropertiesCollectionClass = new AnnotateLayerPropertiesCollection() as IAnnotateLayerPropertiesCollection2;
		    IAnnotateLayerProperties labelEngineLayerPropertiesClass =
		        new LabelEngineLayerProperties() as IAnnotateLayerProperties;

           
                labelEngineLayerPropertiesClass.Class = "要素类 1";
				labelEngineLayerPropertiesClass.FeatureLinked = false;
				labelEngineLayerPropertiesClass.AddUnplacedToGraphicsContainer = false;
				labelEngineLayerPropertiesClass.CreateUnplacedElements = true;
				labelEngineLayerPropertiesClass.DisplayAnnotation = true;
				labelEngineLayerPropertiesClass.UseOutput = true;
			ILabelEngineLayerProperties itextSymbol0 = labelEngineLayerPropertiesClass as ILabelEngineLayerProperties;
			itextSymbol0.Offset = 0;
			itextSymbol0.SymbolID = 0;
			itextSymbol0.Symbol = itextSymbol_0;
			annotateLayerPropertiesCollectionClass.Add(labelEngineLayerPropertiesClass);
			for (int i = 0; i < ifields_0.FieldCount; i++)
			{
				if (field.FindField(ifields_0.Field[i].Name) == -1)
				{
					(field as IFieldsEdit).AddField(ifields_0.Field[i]);
				}
			}
			try
			{
				IFeatureClass featureClass1 = ifeatureWorkspace0.CreateAnnotationClass(string_4, field, instanceCLSID, classExtensionCLSID, featureClassDescription.ShapeFieldName, "", null, null, annotateLayerPropertiesCollectionClass, graphicsLayerScaleClass, symbolCollectionClass, false);
				featureClass = featureClass1;
				return featureClass;
			}
			catch (Exception exception)
			{
			//	CErrorLog.writeErrorLog(this, exception, "");
			}
			featureClass = null;
			return featureClass;
		}

		private IFeatureClass method_2(IFeatureWorkspace ifeatureWorkspace_0, string string_4, esriFeatureType esriFeatureType_0, esriGeometryType esriGeometryType_0, IFields ifields_0)
		{
			string str;
			IFeatureClass featureClass;
			IFeatureClass featureClass1 = null;
			IFieldChecker fieldCheckerClass = new FieldChecker()
			{
				ValidateWorkspace = ifeatureWorkspace_0 as IWorkspace
			};
			fieldCheckerClass.ValidateTableName(string_4, out str);
			IObjectClassDescription featureClassDescriptionClass = null;
			if (esriFeatureType_0 != esriFeatureType.esriFTAnnotation)
			{
				featureClassDescriptionClass = new FeatureClassDescription();
				IFieldsEdit requiredFields = featureClassDescriptionClass.RequiredFields as IFieldsEdit;
				IFieldEdit field = null;
				int num = requiredFields.FindField((featureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName);
				field = requiredFields.Field[num] as IFieldEdit;
				IGeometryDefEdit geometryDef = field.GeometryDef as IGeometryDefEdit;
				esriFeatureType _esriFeatureType = esriFeatureType.esriFTSimple;
				field.GeometryDef_2 = geometryDef;
				for (int i = 0; i < ifields_0.FieldCount; i++)
				{
					requiredFields.AddField(ifields_0.Field[i]);
				}
				try
				{
					featureClass1 = ifeatureWorkspace_0.CreateFeatureClass(string_4, requiredFields, null, null, _esriFeatureType, (featureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName, "");
				}
				catch (Exception exception)
				{
				//	CErrorLog.writeErrorLog(this, exception, "");
				}
				featureClass = featureClass1;
			}
			else
			{
				featureClass = this.method_1(ifeatureWorkspace_0, string_4, 1000, new TextSymbol(), ifields_0);
			}
			return featureClass;
		}

		public void Refresh()
		{
			this.igxCatalog_0.ObjectRefreshed(this);
		}
	}
}