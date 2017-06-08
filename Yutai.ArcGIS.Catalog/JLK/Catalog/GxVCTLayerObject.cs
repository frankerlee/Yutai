namespace JLK.Catalog
{
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Display;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using JLK.Catalog.VCT;
    using JLK.Utility;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class GxVCTLayerObject : IGxObject, IGxLayer, IGxObjectUI, IGxVCTLayerObject
    {
        private ICoConvert icoConvert_0 = null;
        private IGxCatalog igxCatalog_0 = null;
        private IGxObject igxObject_0 = null;
        private ILayer ilayer_0 = null;
        [CompilerGenerated]
        private JLK.Catalog.LayerType layerType_0;
        [CompilerGenerated]
        private string string_0;
        [CompilerGenerated]
        private string string_1;
        [CompilerGenerated]
        private string string_2;
        [CompilerGenerated]
        private string string_3;

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
            int featureCount = this.icoConvert_0.FeatureCount;
            GxVCTObject parent = this.Parent as GxVCTObject;
            frmProgressBar1 bar = new frmProgressBar1();
            try
            {
                IFeatureLayer layer;
                IFeatureClass class2 = null;
                CoLayerType layerType = this.icoConvert_0.XpgisLayer.LayerType;
                string str = "Point";
                switch (layerType)
                {
                    case CoLayerType.Point:
                        str = "Point";
                        break;

                    case CoLayerType.Line:
                        str = "line";
                        break;

                    case CoLayerType.Region:
                        str = "Polygon";
                        break;

                    case CoLayerType.Annotation:
                        str = "Text";
                        break;
                }
                ICoConvert convert = null;
                if (str != "Text")
                {
                    convert = new ShapeLayerClass(parent.GetTemplatePath() + @"\" + this.icoConvert_0.XpgisLayer.Name + ".shp", str, this.icoConvert_0.XpgisLayer);
                }
                else
                {
                    IFieldsEdit edit = null;
                    edit = new FieldsClass();
                    foreach (ICoField field in this.icoConvert_0.XpgisLayer.Fields)
                    {
                        IFieldEdit edit2 = new FieldClass {
                            Name = field.Name,
                            AliasName = field.AliasName,
                            DefaultValue = field.DefaultValue,
                            Editable = field.Enable
                        };
                        CoFieldType type = field.Type;
                        switch (type)
                        {
                            case CoFieldType.整型:
                                edit2.Type = esriFieldType.esriFieldTypeInteger;
                                break;

                            case ((CoFieldType) 2):
                            case ((CoFieldType) 6):
                            case ((CoFieldType) 7):
                                break;

                            case CoFieldType.浮点型:
                                edit2.Type = esriFieldType.esriFieldTypeSingle;
                                edit2.Precision = field.Precision;
                                edit2.Scale = field.Length;
                                break;

                            case CoFieldType.字符型:
                                edit2.Type = esriFieldType.esriFieldTypeString;
                                edit2.Length = field.Length;
                                break;

                            case CoFieldType.日期型:
                                edit2.Type = esriFieldType.esriFieldTypeDate;
                                break;

                            case CoFieldType.二进制:
                                edit2.Type = esriFieldType.esriFieldTypeBlob;
                                break;

                            default:
                                if (type == CoFieldType.布尔型)
                                {
                                    edit2.Type = esriFieldType.esriFieldTypeInteger;
                                }
                                break;
                        }
                        edit.AddField(edit2);
                    }
                    class2 = this.method_2(parent.GetTemplateTextWorksoace() as IFeatureWorkspace, this.icoConvert_0.XpgisLayer.Name, esriFeatureType.esriFTAnnotation, esriGeometryType.esriGeometryPolygon, edit);
                    convert = new GeodatabaseLayerClass(class2);
                }
                ConvertHander hander = new ConvertHander();
                bar.Text = "打开数据";
                bar.Show();
                bar.progressBar1.Maximum = featureCount;
                bar.progressBar1.Value = 0;
                bar.Caption1.Text = "正在载入图层:" + this.icoConvert_0.XpgisLayer.AliasName + " [要素总数:" + featureCount.ToString() + "]";
                hander.Convert(this.icoConvert_0, convert, bar.progressBar1);
                string name = this.icoConvert_0.XpgisLayer.Name;
                convert.Dispose();
                if (class2 == null)
                {
                    class2 = (parent.GetTemplateShapefile() as IFeatureWorkspace).OpenFeatureClass(name);
                }
                if (class2.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    FDOGraphicsLayerClass class3 = new FDOGraphicsLayerClass {
                        Cached = true
                    };
                    layer = class3;
                    layer.FeatureClass = class2;
                    layer.Name = class2.AliasName;
                }
                else if (class2.FeatureType == esriFeatureType.esriFTDimension)
                {
                    DimensionLayerClass class4 = new DimensionLayerClass {
                        Cached = true
                    };
                    layer = class4;
                    layer.FeatureClass = class2;
                    layer.Name = class2.AliasName;
                }
                else
                {
                    FeatureLayerClass class5 = new FeatureLayerClass {
                        Cached = true
                    };
                    layer = class5;
                    layer.FeatureClass = class2;
                    layer.Name = class2.AliasName;
                }
                this.ilayer_0 = layer;
                bar.Close();
            }
            catch (Exception)
            {
            }
        }

        private IFeatureClass method_1(IFeatureWorkspace ifeatureWorkspace_0, string string_4, double double_0, ITextSymbol itextSymbol_0, IFields ifields_0)
        {
            IObjectClassDescription description = new AnnotationFeatureClassDescriptionClass();
            IFeatureClassDescription description2 = description as IFeatureClassDescription;
            IClone requiredFields = description.RequiredFields as IClone;
            IFields fields = requiredFields.Clone() as IFields;
            IFieldEdit edit = null;
            int index = fields.FindField(description2.ShapeFieldName);
            edit = fields.get_Field(index) as IFieldEdit;
            IGeometryDefEdit geometryDef = edit.GeometryDef as IGeometryDefEdit;
            IFeatureWorkspaceAnno anno = ifeatureWorkspace_0 as IFeatureWorkspaceAnno;
            IGraphicsLayerScale referenceScale = new GraphicsLayerScaleClass {
                ReferenceScale = double_0,
                Units = esriUnits.esriMeters
            };
            UID instanceCLSID = description.InstanceCLSID;
            UID classExtensionCLSID = description.ClassExtensionCLSID;
            ISymbolCollection symbolCollection = new SymbolCollectionClass();
            symbolCollection.set_Symbol(0, itextSymbol_0 as ISymbol);
            IAnnotateLayerPropertiesCollection2 annoProperties = new AnnotateLayerPropertiesCollectionClass();
            IAnnotateLayerProperties item = new LabelEngineLayerPropertiesClass {
                Class = "要素类 1",
                FeatureLinked = false,
                AddUnplacedToGraphicsContainer = false,
                CreateUnplacedElements = true,
                DisplayAnnotation = true,
                UseOutput = true
            };
            ILabelEngineLayerProperties properties2 = item as ILabelEngineLayerProperties;
            properties2.Offset = 0.0;
            properties2.SymbolID = 0;
            properties2.Symbol = itextSymbol_0;
            annoProperties.Add(item);
            for (int i = 0; i < ifields_0.FieldCount; i++)
            {
                if (fields.FindField(ifields_0.get_Field(i).Name) == -1)
                {
                    (fields as IFieldsEdit).AddField(ifields_0.get_Field(i));
                }
            }
            try
            {
                return anno.CreateAnnotationClass(string_4, fields, instanceCLSID, classExtensionCLSID, description2.ShapeFieldName, "", null, null, annoProperties, referenceScale, symbolCollection, false);
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
            return null;
        }

        private IFeatureClass method_2(IFeatureWorkspace ifeatureWorkspace_0, string string_4, esriFeatureType esriFeatureType_0, esriGeometryType esriGeometryType_0, IFields ifields_0)
        {
            IFeatureClass class2 = null;
            string str;
            IFieldChecker checker = new FieldCheckerClass {
                ValidateWorkspace = ifeatureWorkspace_0 as IWorkspace
            };
            checker.ValidateTableName(string_4, out str);
            IObjectClassDescription description = null;
            if (esriFeatureType_0 == esriFeatureType.esriFTAnnotation)
            {
                return this.method_1(ifeatureWorkspace_0, string_4, 1000.0, new TextSymbolClass(), ifields_0);
            }
            description = new FeatureClassDescriptionClass();
            IFieldsEdit requiredFields = description.RequiredFields as IFieldsEdit;
            IFieldEdit edit2 = null;
            int index = requiredFields.FindField((description as IFeatureClassDescription).ShapeFieldName);
            edit2 = requiredFields.get_Field(index) as IFieldEdit;
            IGeometryDefEdit geometryDef = edit2.GeometryDef as IGeometryDefEdit;
            esriFeatureType esriFTSimple = esriFeatureType.esriFTSimple;
            edit2.GeometryDef = geometryDef;
            for (int i = 0; i < ifields_0.FieldCount; i++)
            {
                requiredFields.AddField(ifields_0.get_Field(i));
            }
            try
            {
                class2 = ifeatureWorkspace_0.CreateFeatureClass(string_4, requiredFields, null, null, esriFTSimple, (description as IFeatureClassDescription).ShapeFieldName, "");
            }
            catch (Exception exception)
            {
                CErrorLog.writeErrorLog(this, exception, "");
            }
            return class2;
        }

        public void Refresh()
        {
            this.igxCatalog_0.ObjectRefreshed(this);
        }

        public string BaseName
        {
            [CompilerGenerated]
            get
            {
                return this.string_2;
            }
            [CompilerGenerated]
            protected set
            {
                this.string_2 = value;
            }
        }

        public string Category
        {
            [CompilerGenerated]
            get
            {
                return this.string_3;
            }
            [CompilerGenerated]
            protected set
            {
                this.string_3 = value;
            }
        }

        public UID ClassID
        {
            get
            {
                return null;
            }
        }

        public string FullName
        {
            [CompilerGenerated]
            get
            {
                return this.string_1;
            }
            [CompilerGenerated]
            protected set
            {
                this.string_1 = value;
            }
        }

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
                string layerTypeName = this.LayerTypeName;
                switch (layerTypeName)
                {
                    case null:
                        break;

                    case "Point":
                        return ImageLib.GetSmallImage(0x20);

                    case "line":
                        return ImageLib.GetSmallImage(0x21);

                    default:
                        if (!(layerTypeName == "Polygon"))
                        {
                            if (layerTypeName == "Text")
                            {
                                return ImageLib.GetSmallImage(0x22);
                            }
                        }
                        else
                        {
                            return ImageLib.GetSmallImage(0x22);
                        }
                        break;
                }
                return ImageLib.GetSmallImage(0x23);
            }
        }

        public Bitmap LargeSelectedImage
        {
            get
            {
                string layerTypeName = this.LayerTypeName;
                switch (layerTypeName)
                {
                    case null:
                        break;

                    case "Point":
                        return ImageLib.GetSmallImage(0x20);

                    case "line":
                        return ImageLib.GetSmallImage(0x21);

                    default:
                        if (!(layerTypeName == "Polygon"))
                        {
                            if (layerTypeName == "Text")
                            {
                                return ImageLib.GetSmallImage(0x22);
                            }
                        }
                        else
                        {
                            return ImageLib.GetSmallImage(0x22);
                        }
                        break;
                }
                return ImageLib.GetSmallImage(0x23);
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

        public JLK.Catalog.LayerType LayerType
        {
            [CompilerGenerated]
            get
            {
                return this.layerType_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.layerType_0 = value;
            }
        }

        public string LayerTypeName
        {
            get
            {
                CoLayerType layerType = this.icoConvert_0.XpgisLayer.LayerType;
                string str = "Point";
                switch (layerType)
                {
                    case CoLayerType.Point:
                        return "Point";

                    case CoLayerType.Line:
                        return "line";

                    case CoLayerType.Region:
                        return "Polygon";

                    case CoLayerType.Annotation:
                        str = "Text";
                        break;
                }
                return str;
            }
        }

        public string Name
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            protected set
            {
                this.string_0 = value;
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
                switch (this.LayerTypeName)
                {
                    case "Point":
                        this.LayerType = JLK.Catalog.LayerType.PointLayer;
                        return ImageLib.GetSmallImage(0x20);

                    case "line":
                        this.LayerType = JLK.Catalog.LayerType.LineLayer;
                        return ImageLib.GetSmallImage(0x21);

                    case "Polygon":
                        this.LayerType = JLK.Catalog.LayerType.PolygonLayer;
                        return ImageLib.GetSmallImage(0x22);

                    case "Text":
                        this.LayerType = JLK.Catalog.LayerType.AnnoLayer;
                        return ImageLib.GetSmallImage(0x22);
                }
                this.LayerType = JLK.Catalog.LayerType.UnknownLayer;
                return ImageLib.GetSmallImage(0x23);
            }
        }

        public Bitmap SmallSelectedImage
        {
            get
            {
                switch (this.LayerTypeName)
                {
                    case "Point":
                        this.LayerType = JLK.Catalog.LayerType.PointLayer;
                        return ImageLib.GetSmallImage(0x20);

                    case "line":
                        this.LayerType = JLK.Catalog.LayerType.LineLayer;
                        return ImageLib.GetSmallImage(0x21);

                    case "Polygon":
                        this.LayerType = JLK.Catalog.LayerType.PolygonLayer;
                        return ImageLib.GetSmallImage(0x22);

                    case "Text":
                        this.LayerType = JLK.Catalog.LayerType.AnnoLayer;
                        return ImageLib.GetSmallImage(0x22);
                }
                this.LayerType = JLK.Catalog.LayerType.UnknownLayer;
                return ImageLib.GetSmallImage(0x23);
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
    }
}

