using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Shared;

namespace Yutai.ArcGIS.Common.Carto
{
    public class AnnoHelper
    {
        public AnnoHelper()
        {
        }

        public static void ConvertLabels2Anno(IMap imap_0, ILayer ilayer_0, string string_0, double double_0)
        {
            int i;
            IAnnotateLayerProperties annotateLayerProperty;
            IElementCollection elementCollection;
            IElementCollection elementCollection1;
            IElementCollection elementCollection2;
            IElement element;
            int num;
            IActiveView imap0 = imap_0 as IActiveView;
            int j = 0;
            int count = 0;
            IGeoFeatureLayer ilayer0 = ilayer_0 as IGeoFeatureLayer;
            if (ilayer0 != null)
            {
                IAnnotateLayerPropertiesCollection annotateLayerPropertiesCollectionClass =
                    new AnnotateLayerPropertiesCollection();
                IAnnotateLayerPropertiesCollection annotationProperties = ilayer0.AnnotationProperties;
                for (i = 0; i < annotationProperties.Count; i++)
                {
                    annotationProperties.QueryItem(i, out annotateLayerProperty, out elementCollection,
                        out elementCollection1);
                    if (annotateLayerProperty != null)
                    {
                        annotateLayerPropertiesCollectionClass.Add(
                            (annotateLayerProperty as IClone).Clone() as IAnnotateLayerProperties);
                    }
                }
                annotateLayerProperty = null;
                IOverposterProperties overposterProperties = (imap_0 as IMapOverposter).OverposterProperties;
                IGraphicsLayer double0 = (imap_0.BasicGraphicsLayer as ICompositeGraphicsLayer).AddLayer(string_0,
                    ilayer0);
                (double0 as IGraphicsLayerScale).ReferenceScale = double_0;
                double0.Activate(imap0.ScreenDisplay);
                for (i = 0; i < annotateLayerPropertiesCollectionClass.Count; i++)
                {
                    annotateLayerPropertiesCollectionClass.QueryItem(i, out annotateLayerProperty, out elementCollection,
                        out elementCollection1);
                    if (annotateLayerProperty != null)
                    {
                        annotateLayerProperty.FeatureLayer = ilayer0;
                        annotateLayerProperty.GraphicsContainer = double0 as IGraphicsContainer;
                        annotateLayerProperty.AddUnplacedToGraphicsContainer = false;
                        annotateLayerProperty.CreateUnplacedElements = true;
                        annotateLayerProperty.DisplayAnnotation = true;
                        annotateLayerProperty.FeatureLinked = false;
                        annotateLayerProperty.LabelWhichFeatures = esriLabelWhichFeatures.esriAllFeatures;
                        annotateLayerProperty.UseOutput = true;
                        ((annotateLayerProperty as ILabelEngineLayerProperties2).OverposterLayerProperties as
                            IOverposterLayerProperties2).TagUnplaced = true;
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
                    annotateLayerPropertiesCollectionClass.QueryItem(i, out annotateLayerProperty, out elementCollection,
                        out elementCollection2);
                    if (annotateLayerProperty != null)
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
                        annotateLayerProperty.FeatureLayer = null;
                    }
                }
                ilayer0.DisplayAnnotation = false;
                imap0.ContentsChanged();
                imap0.Refresh();
            }
        }

        public static void ConvertLabels2StandardAnno(IMap imap_0, ILayer ilayer_0, string string_0)
        {
            int i;
            IAnnotateLayerProperties annotateLayerProperty;
            IElementCollection elementCollection;
            IElementCollection elementCollection1;
            ILabelEngineLayerProperties2 d;
            ISymbolIdentifier2 symbolIdentifier2;
            IActiveView imap0 = imap_0 as IActiveView;
            if (ilayer_0 is IGeoFeatureLayer)
            {
                IGeoFeatureLayer ilayer0 = ilayer_0 as IGeoFeatureLayer;
                IWorkspace workspace = (ilayer0 as IDataset).Workspace;
                if (workspace.Type != esriWorkspaceType.esriFileSystemWorkspace)
                {
                    IFeatureClass featureClass = ilayer0.FeatureClass;
                    IGeoDataset geoDataset = featureClass as IGeoDataset;
                    IAnnotationLayerFactory fDOGraphicsLayerFactoryClass =
                        new FDOGraphicsLayerFactory() as IAnnotationLayerFactory;
                    ISymbolCollection2 symbolCollectionClass = new SymbolCollection() as ISymbolCollection2;
                    IAnnotateLayerPropertiesCollection annotateLayerPropertiesCollectionClass =
                        new AnnotateLayerPropertiesCollection();
                    IAnnotateLayerPropertiesCollection annotationProperties = ilayer0.AnnotationProperties;
                    for (i = 0; i < annotationProperties.Count; i++)
                    {
                        annotationProperties.QueryItem(i, out annotateLayerProperty, out elementCollection,
                            out elementCollection1);
                        if (annotateLayerProperty != null)
                        {
                            annotateLayerPropertiesCollectionClass.Add(annotateLayerProperty);
                            d = annotateLayerProperty as ILabelEngineLayerProperties2;
                            IClone symbol = d.Symbol as IClone;
                            symbolCollectionClass.AddSymbol(symbol.Clone() as ISymbol,
                                string.Concat(annotateLayerProperty.Class, " ", i.ToString()), out symbolIdentifier2);
                            d.SymbolID = symbolIdentifier2.ID;
                        }
                    }
                    annotateLayerProperty = null;
                    d = null;
                    IGraphicsLayerScale graphicsLayerScaleClass = new GraphicsLayerScale();
                    if (imap_0.ReferenceScale != 0)
                    {
                        graphicsLayerScaleClass.ReferenceScale = imap_0.ReferenceScale;
                    }
                    else
                    {
                        try
                        {
                            graphicsLayerScaleClass.ReferenceScale = imap_0.MapScale;
                        }
                        catch (Exception exception)
                        {
                            Logger.Current.Error("", exception, null);
                        }
                    }
                    graphicsLayerScaleClass.Units = imap_0.MapUnits;
                    IFeatureClassDescription annotationFeatureClassDescriptionClass =
                        new AnnotationFeatureClassDescription() as IFeatureClassDescription;
                    IFields requiredFields =
                        (annotationFeatureClassDescriptionClass as IObjectClassDescription).RequiredFields;
                    IField field =
                        requiredFields.Field[
                            requiredFields.FindField(annotationFeatureClassDescriptionClass.ShapeFieldName)];
                    IGeometryDefEdit geometryDef = field.GeometryDef as IGeometryDefEdit;
                    geometryDef.SpatialReference_2 = geoDataset.SpatialReference;
                    IOverposterProperties overposterProperties = (imap_0 as IMapOverposter).OverposterProperties;
                    IAnnotationLayer annotationLayer =
                        fDOGraphicsLayerFactoryClass.CreateAnnotationLayer(workspace as IFeatureWorkspace,
                            featureClass.FeatureDataset, string_0, geometryDef, null,
                            annotateLayerPropertiesCollectionClass, graphicsLayerScaleClass,
                            symbolCollectionClass as ISymbolCollection, true, true, false, true, overposterProperties,
                            "");
                    (annotationLayer as IGraphicsLayer).Activate(imap0.ScreenDisplay);
                    for (i = 0; i < annotateLayerPropertiesCollectionClass.Count; i++)
                    {
                        annotateLayerPropertiesCollectionClass.QueryItem(i, out annotateLayerProperty,
                            out elementCollection, out elementCollection1);
                        if (annotateLayerProperty != null)
                        {
                            annotateLayerProperty.FeatureLayer = ilayer0;
                            annotateLayerProperty.GraphicsContainer = annotationLayer as IGraphicsContainer;
                            annotateLayerProperty.AddUnplacedToGraphicsContainer = true;
                            annotateLayerProperty.CreateUnplacedElements = true;
                            annotateLayerProperty.DisplayAnnotation = true;
                            annotateLayerProperty.FeatureLinked = true;
                            annotateLayerProperty.LabelWhichFeatures = esriLabelWhichFeatures.esriAllFeatures;
                            annotateLayerProperty.UseOutput = true;
                            d = annotateLayerProperty as ILabelEngineLayerProperties2;
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
                        annotateLayerPropertiesCollectionClass.QueryItem(i, out annotateLayerProperty,
                            out elementCollection, out elementCollection1);
                        if (annotateLayerProperty != null)
                        {
                            annotateLayerProperty.FeatureLayer = null;
                        }
                    }
                    imap_0.AddLayer(annotationLayer as ILayer);
                    ilayer0.DisplayAnnotation = false;
                    imap0.Refresh();
                }
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

        public static ITextElement MakeTextElement(string string_0, double double_0, double double_1, double double_2,
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

        private IGeometryDef method_0(IFeatureClass ifeatureClass_0)
        {
            IGeometryDef geometryDef =
                ifeatureClass_0.Fields.Field[ifeatureClass_0.FindField(ifeatureClass_0.ShapeFieldName)].GeometryDef;
            return geometryDef;
        }
    }
}