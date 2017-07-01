using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;
using Yutai.ArcGIS.Common.Carto;
using Yutai.Shared;

namespace Yutai.ArcGIS.Carto.UI
{
    public partial class frmLabelToAnnoSetup : Form
    {
        private Container container_0 = null;
        private double double_0 = 0.0;
        private esriLabelWhichFeatures esriLabelWhichFeatures_0 = esriLabelWhichFeatures.esriAllFeatures;
        private IFeatureLayer ifeatureLayer_0 = null;
        private IMap imap_0 = null;
        private IWorkspace iworkspace_0 = null;

        public frmLabelToAnnoSetup()
        {
            if (CartoLicenseProviderCheck.Check())
            {
                this.InitializeComponent();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.method_3(this.imap_0, this.ifeatureLayer_0, this.txtAnnoName.Text, this.double_0, this.iworkspace_0,
                    true, this.chkFeatureLinked.Checked, this.esriLabelWhichFeatures_0);
            }
            catch (Exception exception)
            {
                Logger.Current.Error("", exception, "");
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
        }

        private void frmLabelToAnnoSetup_Load(object sender, EventArgs e)
        {
            this.method_0();
        }

        private void method_0()
        {
            if (this.ifeatureLayer_0 == null)
            {
                this.btnConvert.Enabled = false;
            }
            else
            {
                this.txtFeatLayer.Text = this.ifeatureLayer_0.Name;
                this.iworkspace_0 = (this.ifeatureLayer_0.FeatureClass as IDataset).Workspace;
                string str = this.txtFeatLayer.Text + "_Anno";
                this.txtAnnoName.Text = WorkspaceOperator.GetFinalName(this.iworkspace_0,
                    esriDatasetType.esriDTFeatureClass, str);
                this.txtSavePos.Text = this.iworkspace_0.PathName + @"\" + this.txtAnnoName.Text;
                this.double_0 = this.method_1();
                this.lblRefrencesScale.Text = "1:" + this.double_0.ToString();
            }
        }

        private double method_1()
        {
            if (this.imap_0 != null)
            {
                try
                {
                    if (this.imap_0.ReferenceScale == 0.0)
                    {
                        return this.imap_0.MapScale;
                    }
                    return this.imap_0.ReferenceScale;
                }
                catch (Exception exception)
                {
                    Logger.Current.Error("", exception, null);
                }
            }
            return 0.0;
        }

        private IFeatureClass method_2(IFeatureWorkspaceAnno ifeatureWorkspaceAnno_0, IFeatureDataset ifeatureDataset_0,
            IFeatureClass ifeatureClass_0, double double_1, esriUnits esriUnits_0,
            IAnnotateLayerPropertiesCollection iannotateLayerPropertiesCollection_0,
            ISymbolCollection isymbolCollection_0, string string_0)
        {
            IObjectClassDescription description = new AnnotationFeatureClassDescriptionClass();
            IFields requiredFields = description.RequiredFields;
            int index = requiredFields.FindField((description as IFeatureClassDescription).ShapeFieldName);
            if (index != -1)
            {
                IField field = requiredFields.get_Field(index);
                IGeometryDef geometryDef = field.GeometryDef;
                ISpatialReference spatialReference = geometryDef.SpatialReference;
                SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspaceAnno_0 as IGeodatabaseRelease,
                    spatialReference, false);
                (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                (field as IFieldEdit).GeometryDef_2 = geometryDef;
            }
            IFeatureClassDescription description2 = description as IFeatureClassDescription;
            IGraphicsLayerScale referenceScale = new GraphicsLayerScaleClass
            {
                ReferenceScale = double_1,
                Units = esriUnits_0
            };
            UID instanceCLSID = description.InstanceCLSID;
            UID classExtensionCLSID = description.ClassExtensionCLSID;
            return ifeatureWorkspaceAnno_0.CreateAnnotationClass(string_0, requiredFields, instanceCLSID,
                classExtensionCLSID, description2.ShapeFieldName, "", ifeatureDataset_0, ifeatureClass_0,
                iannotateLayerPropertiesCollection_0, referenceScale, isymbolCollection_0, true);
        }

        private void method_3(IMap imap_1, ILayer ilayer_0, string string_0, double double_1, IWorkspace iworkspace_1,
            bool bool_0, bool bool_1, esriLabelWhichFeatures esriLabelWhichFeatures_1)
        {
            if ((iworkspace_1.Type != esriWorkspaceType.esriFileSystemWorkspace) && (ilayer_0 is IGeoFeatureLayer))
            {
                int num;
                IAnnotateLayerProperties properties;
                IElementCollection elements;
                IElementCollection elements2;
                ILabelEngineLayerProperties2 properties2;
                IAnnotationLayer layer2;
                IGeoFeatureLayer layer = ilayer_0 as IGeoFeatureLayer;
                IFeatureClass featureClass = layer.FeatureClass;
                IAnnotationLayerFactory factory = new FDOGraphicsLayerFactoryClass();
                ISymbolCollection2 lcs = new SymbolCollectionClass();
                IAnnotateLayerPropertiesCollection propertiess = new AnnotateLayerPropertiesCollectionClass();
                IAnnotateLayerPropertiesCollection annotationProperties = layer.AnnotationProperties;
                for (num = 0; num < annotationProperties.Count; num++)
                {
                    annotationProperties.QueryItem(num, out properties, out elements, out elements2);
                    if (properties != null)
                    {
                        ISymbolIdentifier2 identifier;
                        propertiess.Add(properties);
                        properties2 = properties as ILabelEngineLayerProperties2;
                        IClone symbol = properties2.Symbol as IClone;
                        lcs.AddSymbol(symbol.Clone() as ISymbol, properties.Class + " " + num.ToString(), out identifier);
                        properties2.SymbolID = identifier.ID;
                    }
                }
                properties = null;
                properties2 = null;
                IGraphicsLayerScale scale = new GraphicsLayerScaleClass
                {
                    ReferenceScale = double_1,
                    Units = imap_1.MapUnits
                };
                IFeatureClassDescription description = new AnnotationFeatureClassDescriptionClass();
                IObjectClassDescription description2 = description as IObjectClassDescription;
                IFields requiredFields = description2.RequiredFields;
                IGeometryDefEdit geometryDef =
                    requiredFields.get_Field(requiredFields.FindField(description.ShapeFieldName)).GeometryDef as
                        IGeometryDefEdit;
                IGeoDataset dataset = featureClass as IGeoDataset;
                geometryDef.SpatialReference_2 = dataset.SpatialReference;
                IMapOverposter overposter = imap_1 as IMapOverposter;
                IOverposterProperties overposterProperties = overposter.OverposterProperties;
                if (bool_1)
                {
                    this.method_2(iworkspace_1 as IFeatureWorkspaceAnno, featureClass.FeatureDataset, featureClass,
                        scale.ReferenceScale, scale.Units, propertiess, lcs as ISymbolCollection, string_0);
                    layer2 = factory.OpenAnnotationLayer(iworkspace_1 as IFeatureWorkspace, featureClass.FeatureDataset,
                        string_0);
                }
                else
                {
                    this.method_2(iworkspace_1 as IFeatureWorkspaceAnno, featureClass.FeatureDataset, null,
                        scale.ReferenceScale, scale.Units, propertiess, lcs as ISymbolCollection, string_0);
                    layer2 = factory.OpenAnnotationLayer(iworkspace_1 as IFeatureWorkspace, featureClass.FeatureDataset,
                        string_0);
                }
                IActiveView view = imap_1 as IActiveView;
                IScreenDisplay screenDisplay = view.ScreenDisplay;
                (layer2 as IGraphicsLayer).Activate(screenDisplay);
                for (num = 0; num < propertiess.Count; num++)
                {
                    propertiess.QueryItem(num, out properties, out elements, out elements2);
                    if (properties != null)
                    {
                        properties.FeatureLayer = layer;
                        properties.GraphicsContainer = layer2 as IGraphicsContainer;
                        properties.AddUnplacedToGraphicsContainer = bool_0;
                        properties.CreateUnplacedElements = true;
                        properties.DisplayAnnotation = true;
                        properties.FeatureLinked = bool_1;
                        properties.LabelWhichFeatures = esriLabelWhichFeatures_1;
                        properties.UseOutput = true;
                        properties2 = properties as ILabelEngineLayerProperties2;
                        properties2.SymbolID = num;
                        properties2.AnnotationClassID = num;
                        IOverposterLayerProperties2 overposterLayerProperties =
                            properties2.OverposterLayerProperties as IOverposterLayerProperties2;
                        overposterLayerProperties.TagUnplaced = true;
                    }
                }
                propertiess.Sort();
                IAnnotateMapProperties annoMapCmdProps = new AnnotateMapPropertiesClass
                {
                    AnnotateLayerPropertiesCollection = propertiess
                };
                ITrackCancel trackCancel = new CancelTrackerClass();
                (imap_1.AnnotationEngine as IAnnotateMap2).Label(overposterProperties, annoMapCmdProps, imap_1,
                    trackCancel);
                for (num = 0; num < propertiess.Count; num++)
                {
                    propertiess.QueryItem(num, out properties, out elements, out elements2);
                    if (properties != null)
                    {
                        properties.FeatureLayer = null;
                    }
                }
                imap_1.AddLayer(layer2 as ILayer);
                layer.DisplayAnnotation = false;
                view.Refresh();
            }
        }

        private void txtAnnoName_TextChanged(object sender, EventArgs e)
        {
            this.txtSavePos.Text = this.iworkspace_0.PathName + @"\" + this.txtAnnoName.Text;
        }

        public ILayer Layer
        {
            set { this.ifeatureLayer_0 = value as IFeatureLayer; }
        }

        public IMap Map
        {
            set { this.imap_0 = value; }
        }
    }
}