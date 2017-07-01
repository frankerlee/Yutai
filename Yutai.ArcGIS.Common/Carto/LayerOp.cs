using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Geodatabase;

namespace Yutai.ArcGIS.Common.Carto
{
    internal class LayerOp
    {
        public LayerOp()
        {
        }

        private static void ChangeCoordinateSystem(IGeodatabaseRelease igeodatabaseRelease_0,
            ISpatialReference ispatialReference_0, bool bool_0)
        {
            if (ispatialReference_0 != null)
            {
                bool geoDatasetPrecision = GeodatabaseTools.GetGeoDatasetPrecision(igeodatabaseRelease_0);
                IControlPrecision2 ispatialReference0 = ispatialReference_0 as IControlPrecision2;
                if (ispatialReference0.IsHighPrecision != geoDatasetPrecision)
                {
                    ispatialReference0.IsHighPrecision = geoDatasetPrecision;
                    if (bool_0)
                    {
                        ISpatialReferenceResolution spatialReferenceResolution =
                            ispatialReference_0 as ISpatialReferenceResolution;
                        spatialReferenceResolution.ConstructFromHorizon();
                        spatialReferenceResolution.SetDefaultXYResolution();
                        (ispatialReference_0 as ISpatialReferenceTolerance).SetDefaultXYTolerance();
                    }
                }
            }
        }

        public static void ConvertLabels2Anno(IMap imap_0, IGeoFeatureLayer igeoFeatureLayer_0, string string_0,
            double double_0)
        {
            int i;
            IAnnotateLayerProperties igeoFeatureLayer0;
            IElementCollection elementCollection;
            IElementCollection elementCollection1;
            IElementCollection elementCollection2;
            IElement element;
            int num;
            IActiveView imap0 = imap_0 as IActiveView;
            int j = 0;
            int count = 0;
            IAnnotateLayerPropertiesCollection annotateLayerPropertiesCollectionClass =
                new AnnotateLayerPropertiesCollection();
            IAnnotateLayerPropertiesCollection annotationProperties = igeoFeatureLayer_0.AnnotationProperties;
            for (i = 0; i < annotationProperties.Count; i++)
            {
                annotationProperties.QueryItem(i, out igeoFeatureLayer0, out elementCollection, out elementCollection1);
                if (igeoFeatureLayer0 != null)
                {
                    annotateLayerPropertiesCollectionClass.Add(
                        (igeoFeatureLayer0 as IClone).Clone() as IAnnotateLayerProperties);
                }
            }
            igeoFeatureLayer0 = null;
            IOverposterProperties overposterProperties = (imap_0 as IMapOverposter).OverposterProperties;
            IGraphicsLayer double0 = (imap_0.BasicGraphicsLayer as ICompositeGraphicsLayer).AddLayer(string_0,
                igeoFeatureLayer_0);
            (double0 as IGraphicsLayerScale).ReferenceScale = double_0;
            double0.Activate(imap0.ScreenDisplay);
            for (i = 0; i < annotateLayerPropertiesCollectionClass.Count; i++)
            {
                annotateLayerPropertiesCollectionClass.QueryItem(i, out igeoFeatureLayer0, out elementCollection,
                    out elementCollection1);
                if (igeoFeatureLayer0 != null)
                {
                    igeoFeatureLayer0.FeatureLayer = igeoFeatureLayer_0;
                    igeoFeatureLayer0.GraphicsContainer = double0 as IGraphicsContainer;
                    igeoFeatureLayer0.AddUnplacedToGraphicsContainer = false;
                    igeoFeatureLayer0.CreateUnplacedElements = true;
                    igeoFeatureLayer0.DisplayAnnotation = true;
                    igeoFeatureLayer0.FeatureLinked = false;
                    igeoFeatureLayer0.LabelWhichFeatures = esriLabelWhichFeatures.esriAllFeatures;
                    igeoFeatureLayer0.UseOutput = true;
                    ((igeoFeatureLayer0 as ILabelEngineLayerProperties2).OverposterLayerProperties as
                        IOverposterLayerProperties2).TagUnplaced = true;
                }
            }
            annotateLayerPropertiesCollectionClass.Sort();
            IAnnotateMapProperties annotateMapPropertiesClass = new AnnotateMapProperties()
            {
                AnnotateLayerPropertiesCollection = annotateLayerPropertiesCollectionClass
            };
            ITrackCancel cancelTrackerClass = new CancelTracker();
            (imap_0.AnnotationEngine as IAnnotateMap2).Label(overposterProperties, annotateMapPropertiesClass, imap_0,
                cancelTrackerClass);
            for (i = 0; i < annotateLayerPropertiesCollectionClass.Count; i++)
            {
                annotateLayerPropertiesCollectionClass.QueryItem(i, out igeoFeatureLayer0, out elementCollection,
                    out elementCollection2);
                if (igeoFeatureLayer0 != null)
                {
                    count = count + elementCollection2.Count;
                    if (elementCollection2.Count > 0)
                    {
                        IOverflowGraphicsContainer overflowGraphicsContainer = double0 as IOverflowGraphicsContainer;
                        for (j = 0; j < elementCollection2.Count; j++)
                        {
                            elementCollection2.QueryItem(j, out element, out num);
                            overflowGraphicsContainer.AddOverflowElement(element);
                        }
                    }
                    igeoFeatureLayer0.FeatureLayer = null;
                }
            }
            igeoFeatureLayer_0.DisplayAnnotation = false;
            imap0.ContentsChanged();
            imap0.Refresh();
        }

        public static void ConvertLabels2StandardAnno(IMap imap_0, ILayer ilayer_0, string string_0, double double_0,
            IWorkspace iworkspace_0, bool bool_0, bool bool_1, esriLabelWhichFeatures esriLabelWhichFeatures_0)
        {
            int i;
            IAnnotateLayerProperties bool0;
            IElementCollection elementCollection;
            IElementCollection elementCollection1;
            ILabelEngineLayerProperties2 d;
            ISymbolIdentifier2 symbolIdentifier2;
            IAnnotationLayer annotationLayer;
            if (iworkspace_0.Type != esriWorkspaceType.esriFileSystemWorkspace && ilayer_0 is IGeoFeatureLayer)
            {
                IGeoFeatureLayer ilayer0 = ilayer_0 as IGeoFeatureLayer;
                IFeatureClass featureClass = ilayer0.FeatureClass;
                IAnnotationLayerFactory fDOGraphicsLayerFactoryClass =
                    new FDOGraphicsLayerFactory() as IAnnotationLayerFactory;
                ISymbolCollection2 symbolCollectionClass = new SymbolCollection() as ISymbolCollection2;
                IAnnotateLayerPropertiesCollection annotateLayerPropertiesCollectionClass =
                    new AnnotateLayerPropertiesCollection();
                IAnnotateLayerPropertiesCollection annotationProperties = ilayer0.AnnotationProperties;
                for (i = 0; i < annotationProperties.Count; i++)
                {
                    annotationProperties.QueryItem(i, out bool0, out elementCollection, out elementCollection1);
                    if (bool0 != null)
                    {
                        annotateLayerPropertiesCollectionClass.Add(bool0);
                        d = bool0 as ILabelEngineLayerProperties2;
                        IClone symbol = d.Symbol as IClone;
                        symbolCollectionClass.AddSymbol(symbol.Clone() as ISymbol,
                            string.Concat(bool0.Class, " ", i.ToString()), out symbolIdentifier2);
                        d.SymbolID = symbolIdentifier2.ID;
                    }
                }
                bool0 = null;
                d = null;
                IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale()
                {
                    ReferenceScale = double_0,
                    Units = imap_0.MapUnits
                };
                IFeatureClassDescription annotationFeatureClassDescriptionClass =
                    new AnnotationFeatureClassDescription() as IFeatureClassDescription;
                IFields requiredFields =
                    (annotationFeatureClassDescriptionClass as IObjectClassDescription).RequiredFields;
                IField field =
                    requiredFields.Field[requiredFields.FindField(annotationFeatureClassDescriptionClass.ShapeFieldName)
                    ];
                (field.GeometryDef as IGeometryDefEdit).SpatialReference_2 =
                    (featureClass as IGeoDataset).SpatialReference;
                IOverposterProperties overposterProperties = (imap_0 as IMapOverposter).OverposterProperties;
                if (!bool_1)
                {
                    LayerOp.CreateAnnoFeatureClass(iworkspace_0 as IFeatureWorkspaceAnno, featureClass.FeatureDataset,
                        null, graphicsLayerScaleClass.ReferenceScale, graphicsLayerScaleClass.Units,
                        annotateLayerPropertiesCollectionClass, symbolCollectionClass as ISymbolCollection, string_0);
                    annotationLayer = fDOGraphicsLayerFactoryClass.OpenAnnotationLayer(
                        iworkspace_0 as IFeatureWorkspace, featureClass.FeatureDataset, string_0);
                }
                else
                {
                    LayerOp.CreateAnnoFeatureClass(iworkspace_0 as IFeatureWorkspaceAnno, featureClass.FeatureDataset,
                        featureClass, graphicsLayerScaleClass.ReferenceScale, graphicsLayerScaleClass.Units,
                        annotateLayerPropertiesCollectionClass, symbolCollectionClass as ISymbolCollection, string_0);
                    annotationLayer = fDOGraphicsLayerFactoryClass.OpenAnnotationLayer(
                        iworkspace_0 as IFeatureWorkspace, featureClass.FeatureDataset, string_0);
                }
                IActiveView imap0 = imap_0 as IActiveView;
                (annotationLayer as IGraphicsLayer).Activate(imap0.ScreenDisplay);
                for (i = 0; i < annotateLayerPropertiesCollectionClass.Count; i++)
                {
                    annotateLayerPropertiesCollectionClass.QueryItem(i, out bool0, out elementCollection,
                        out elementCollection1);
                    if (bool0 != null)
                    {
                        bool0.FeatureLayer = ilayer0;
                        bool0.GraphicsContainer = annotationLayer as IGraphicsContainer;
                        bool0.AddUnplacedToGraphicsContainer = bool_0;
                        bool0.CreateUnplacedElements = true;
                        bool0.DisplayAnnotation = true;
                        bool0.FeatureLinked = bool_1;
                        bool0.LabelWhichFeatures = esriLabelWhichFeatures_0;
                        bool0.UseOutput = true;
                        d = bool0 as ILabelEngineLayerProperties2;
                        d.SymbolID = i;
                        d.AnnotationClassID = i;
                        (d.OverposterLayerProperties as IOverposterLayerProperties2).TagUnplaced = true;
                    }
                }
                annotateLayerPropertiesCollectionClass.Sort();
                IAnnotateMapProperties annotateMapPropertiesClass = new AnnotateMapProperties()
                {
                    AnnotateLayerPropertiesCollection = annotateLayerPropertiesCollectionClass
                };
                ITrackCancel cancelTrackerClass = new CancelTracker();
                (imap_0.AnnotationEngine as IAnnotateMap2).Label(overposterProperties, annotateMapPropertiesClass,
                    imap_0, cancelTrackerClass);
                for (i = 0; i < annotateLayerPropertiesCollectionClass.Count; i++)
                {
                    annotateLayerPropertiesCollectionClass.QueryItem(i, out bool0, out elementCollection,
                        out elementCollection1);
                    if (bool0 != null)
                    {
                        bool0.FeatureLayer = null;
                    }
                }
                imap_0.AddLayer(annotationLayer as ILayer);
                ilayer0.DisplayAnnotation = false;
                imap0.Refresh();
            }
        }

        public static IFeatureClass CreateAnnoFeatureClass(string string_0, IFeatureDataset ifeatureDataset_0,
            double double_0, ITextSymbol itextSymbol_0)
        {
            IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescription();
            IFeatureClassDescription featureClassDescription =
                annotationFeatureClassDescriptionClass as IFeatureClassDescription;
            IFields field = (annotationFeatureClassDescriptionClass.RequiredFields as IClone).Clone() as IFields;
            IFeatureWorkspaceAnno workspace = ifeatureDataset_0.Workspace as IFeatureWorkspaceAnno;
            IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale()
            {
                ReferenceScale = double_0,
                Units = esriUnits.esriMeters
            };
            UID instanceCLSID = annotationFeatureClassDescriptionClass.InstanceCLSID;
            UID classExtensionCLSID = annotationFeatureClassDescriptionClass.ClassExtensionCLSID;
            ISymbolCollection symbolCollectionClass = new SymbolCollection();
            symbolCollectionClass.Symbol[0] = itextSymbol_0 as ISymbol;
            IFeatureClass featureClass = workspace.CreateAnnotationClass(string_0, field, instanceCLSID,
                classExtensionCLSID, featureClassDescription.ShapeFieldName, "", ifeatureDataset_0, null, null,
                graphicsLayerScaleClass, symbolCollectionClass, true);
            return featureClass;
        }

        public static IFeatureClass CreateAnnoFeatureClass(IFeatureWorkspaceAnno ifeatureWorkspaceAnno_0,
            IFeatureDataset ifeatureDataset_0, IFeatureClass ifeatureClass_0, double double_0, esriUnits esriUnits_0,
            IAnnotateLayerPropertiesCollection iannotateLayerPropertiesCollection_0,
            ISymbolCollection isymbolCollection_0, string string_0)
        {
            IObjectClassDescription annotationFeatureClassDescriptionClass = new AnnotationFeatureClassDescription();
            IFields requiredFields = annotationFeatureClassDescriptionClass.RequiredFields;
            int num =
                requiredFields.FindField(
                    (annotationFeatureClassDescriptionClass as IFeatureClassDescription).ShapeFieldName);
            if (num != -1)
            {
                IField field = requiredFields.Field[num];
                IGeometryDef geometryDef = field.GeometryDef;
                ISpatialReference spatialReference = geometryDef.SpatialReference;
                LayerOp.ChangeCoordinateSystem(ifeatureWorkspaceAnno_0 as IGeodatabaseRelease, spatialReference, false);
                (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                (field as IFieldEdit).GeometryDef_2 = geometryDef;
            }
            IFeatureClassDescription featureClassDescription =
                annotationFeatureClassDescriptionClass as IFeatureClassDescription;
            IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale()
            {
                ReferenceScale = double_0,
                Units = esriUnits_0
            };
            UID instanceCLSID = annotationFeatureClassDescriptionClass.InstanceCLSID;
            UID classExtensionCLSID = annotationFeatureClassDescriptionClass.ClassExtensionCLSID;
            IFeatureClass featureClass = ifeatureWorkspaceAnno_0.CreateAnnotationClass(string_0, requiredFields,
                instanceCLSID, classExtensionCLSID, featureClassDescription.ShapeFieldName, "", ifeatureDataset_0,
                ifeatureClass_0, iannotateLayerPropertiesCollection_0, graphicsLayerScaleClass, isymbolCollection_0,
                true);
            return featureClass;
        }

        public IGeometryDef GetGeometryDef(IFeatureClass ifeatureClass_0)
        {
            IGeometryDef geometryDef =
                ifeatureClass_0.Fields.Field[ifeatureClass_0.FindField(ifeatureClass_0.ShapeFieldName)].GeometryDef;
            return geometryDef;
        }

        public ITextElement MakeTextElement(string string_0, double double_0, double double_1, double double_2,
            int int_0)
        {
            ITextElement textElement = new TextElement() as ITextElement;
            textElement.ScaleText = true;
            textElement.Text = string_0;
            ITextElement textElementClass = textElement as ITextElement;
            (textElementClass as IGroupSymbolElement).SymbolID = int_0;
            IElement element = textElementClass as IElement;
            IPoint pointClass = new Point();
            pointClass.PutCoords(double_0, double_1);
            element.Geometry = pointClass;
            if (double_2 != 0)
            {
                (textElementClass as ITransform2D).Rotate(pointClass, double_2*0.0174532925388889);
            }
            return textElementClass;
        }
    }
}