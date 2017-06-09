using System;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common.Geodatabase;
using Yutai.Shared;

namespace Yutai.ArcGIS.Catalog.Geodatabase.UI
{
    internal class ObjectClassHelper
    {
        public string AliasName = "";
        private IFeatureDataset ifeatureDataset_0 = null;
        public bool IsRelatedFeature = false;
        private IWorkspace iworkspace_0 = null;
        public esriFeatureType m_FeatreType = esriFeatureType.esriFTSimple;
        public bool m_IsFeatureClass = true;
        public IAnnotateLayerPropertiesCollection2 m_pAnnoPropertiesColn = new AnnotateLayerPropertiesCollectionClass();
        public IFields m_pFieds = null;
        public static ObjectClassHelper m_pObjectClassHelper;
        public ISymbolCollection2 m_pSymbolColl = new SymbolCollectionClass();
        public double m_RefScale = 500.0;
        public IFeatureClass m_RelatedFeatureClass = null;
        public esriUnits m_Units = esriUnits.esriUnknownUnits;
        public string Name = "";
        public string ShapeFieldName = "SHAPE";

        static ObjectClassHelper()
        {
            old_acctor_mc();
        }

        public IObjectClass CreateObjectClass()
        {
            IObjectClass class2 = null;
            if (this.m_IsFeatureClass)
            {
                IFeatureClass relatedFeatureClass;
                if (this.ifeatureDataset_0 != null)
                {
                    if ((this.ifeatureDataset_0.Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, this.Name))
                    {
                        MessageBox.Show("要素已存在，请重新给定要素名");
                        return null;
                    }
                    if (this.m_FeatreType == esriFeatureType.esriFTAnnotation)
                    {
                        relatedFeatureClass = null;
                        if (this.IsRelatedFeature)
                        {
                            relatedFeatureClass = this.m_RelatedFeatureClass;
                        }
                        class2 = this.method_1(this.Name, this.m_RefScale, this.m_pFieds, this.ifeatureDataset_0, relatedFeatureClass, this.iworkspace_0 as IFeatureWorkspaceAnno, this.m_Units, this.m_pAnnoPropertiesColn, this.m_pSymbolColl);
                    }
                    else
                    {
                        class2 = this.ifeatureDataset_0.CreateFeatureClass(this.Name, this.m_pFieds, null, null, this.m_FeatreType, this.ShapeFieldName, "");
                    }
                }
                else
                {
                    if (ObjectClassShareData.m_IsShapeFile)
                    {
                        if (File.Exists(this.iworkspace_0.PathName + @"\" + this.Name + ".shp"))
                        {
                            MessageBox.Show("文件已存在，请重新给定要素名");
                            return null;
                        }
                        try
                        {
                            class2 = (this.iworkspace_0 as IFeatureWorkspace).CreateFeatureClass(this.Name, this.m_pFieds, null, null, this.m_FeatreType, this.ShapeFieldName, "");
                            goto Label_0262;
                        }
                        catch (Exception exception)
                        {
                            Logger.Current.Error("",exception, "创建shape文件");
                            return null;
                        }
                    }
                    if ((this.iworkspace_0 as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, this.Name))
                    {
                        MessageBox.Show("要素已存在，请重新给定要素名");
                        return null;
                    }
                    if (this.m_FeatreType == esriFeatureType.esriFTAnnotation)
                    {
                        relatedFeatureClass = null;
                        if (this.IsRelatedFeature)
                        {
                            relatedFeatureClass = this.m_RelatedFeatureClass;
                        }
                        class2 = this.method_1(this.Name, this.m_RefScale, this.m_pFieds, null, relatedFeatureClass, this.iworkspace_0 as IFeatureWorkspaceAnno, this.m_Units, this.m_pAnnoPropertiesColn, this.m_pSymbolColl);
                    }
                    else
                    {
                        class2 = (this.iworkspace_0 as IFeatureWorkspace).CreateFeatureClass(this.Name, this.m_pFieds, null, null, this.m_FeatreType, this.ShapeFieldName, "");
                    }
                }
            }
            else
            {
                class2 = (this.iworkspace_0 as IFeatureWorkspace).CreateTable(this.Name, this.m_pFieds, null, null, "") as IObjectClass;
            }
        Label_0262:
            if (((class2 != null) && !ObjectClassShareData.m_IsShapeFile) && (this.AliasName.Length > 0))
            {
                (class2 as IClassSchemaEdit).AlterAliasName(this.AliasName);
            }
            return class2;
        }

        public static void CreateOCHelper()
        {
            if (m_pObjectClassHelper != null)
            {
                m_pObjectClassHelper = null;
            }
            m_pObjectClassHelper = new ObjectClassHelper();
        }

        public void InitFields()
        {
            if (this.m_IsFeatureClass)
            {
                IFeatureClassDescription description;
                IField field;
                IGeometryDef geometryDef;
                ISpatialReference spatialReference;
                if (this.m_FeatreType == esriFeatureType.esriFTSimple)
                {
                    description = new FeatureClassDescriptionClass();
                    this.ShapeFieldName = description.ShapeFieldName;
                    this.m_pFieds = (description as IObjectClassDescription).RequiredFields;
                    field = this.m_pFieds.get_Field(this.m_pFieds.FindField(description.ShapeFieldName));
                    geometryDef = field.GeometryDef;
                    if (this.ifeatureDataset_0 != null)
                    {
                        spatialReference = (this.ifeatureDataset_0 as IGeoDataset).SpatialReference;
                    }
                    else
                    {
                        spatialReference = geometryDef.SpatialReference;
                        this.method_0(spatialReference);
                    }
                    (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                    (field as IFieldEdit).GeometryDef_2 = geometryDef;
                }
                else if (this.m_FeatreType == esriFeatureType.esriFTDimension)
                {
                    description = new DimensionClassDescriptionClass();
                    this.ShapeFieldName = description.ShapeFieldName;
                    this.m_pFieds = (description as IObjectClassDescription).RequiredFields;
                    field = this.m_pFieds.get_Field(this.m_pFieds.FindField(description.ShapeFieldName));
                    geometryDef = field.GeometryDef;
                    if (this.ifeatureDataset_0 != null)
                    {
                        spatialReference = (this.ifeatureDataset_0 as IGeoDataset).SpatialReference;
                    }
                    else
                    {
                        spatialReference = geometryDef.SpatialReference;
                        this.method_0(spatialReference);
                    }
                    (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                    (field as IFieldEdit).GeometryDef_2 = geometryDef;
                }
                else if (this.m_FeatreType == esriFeatureType.esriFTAnnotation)
                {
                    description = new AnnotationFeatureClassDescriptionClass();
                    this.ShapeFieldName = description.ShapeFieldName;
                    this.m_pFieds = (description as IObjectClassDescription).RequiredFields;
                    field = this.m_pFieds.get_Field(this.m_pFieds.FindField(description.ShapeFieldName));
                    geometryDef = field.GeometryDef;
                    if (this.ifeatureDataset_0 != null)
                    {
                        spatialReference = (this.ifeatureDataset_0 as IGeoDataset).SpatialReference;
                    }
                    else
                    {
                        spatialReference = geometryDef.SpatialReference;
                        this.method_0(spatialReference);
                    }
                    (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                    (field as IFieldEdit).GeometryDef_2 = geometryDef;
                }
            }
            else
            {
                IObjectClassDescription description2 = new ObjectClassDescriptionClass();
                this.m_pFieds = description2.RequiredFields;
            }
        }

        private void method_0(ISpatialReference ispatialReference_0)
        {
            bool geoDatasetPrecision = false;
            IGeodatabaseRelease release = this.iworkspace_0 as IGeodatabaseRelease;
            IControlPrecision2 precision = ispatialReference_0 as IControlPrecision2;
            geoDatasetPrecision = GeodatabaseTools.GetGeoDatasetPrecision(release);
            precision.IsHighPrecision = geoDatasetPrecision;
            ISpatialReferenceResolution resolution = ispatialReference_0 as ISpatialReferenceResolution;
            resolution.ConstructFromHorizon();
            resolution.SetDefaultXYResolution();
            (ispatialReference_0 as ISpatialReferenceTolerance).SetDefaultXYTolerance();
        }

        private IFeatureClass method_1(string string_0, double double_0, IFields ifields_0, IFeatureDataset ifeatureDataset_1, IFeatureClass ifeatureClass_0, IFeatureWorkspaceAnno ifeatureWorkspaceAnno_0, esriUnits esriUnits_0, IAnnotateLayerPropertiesCollection2 iannotateLayerPropertiesCollection2_0, ISymbolCollection2 isymbolCollection2_0)
        {
            IObjectClassDescription description = new AnnotationFeatureClassDescriptionClass();
            IFeatureClassDescription description2 = description as IFeatureClassDescription;
            IGraphicsLayerScale referenceScale = new GraphicsLayerScaleClass {
                ReferenceScale = double_0,
                Units = esriUnits_0
            };
            UID instanceCLSID = description.InstanceCLSID;
            UID classExtensionCLSID = description.ClassExtensionCLSID;
            return ifeatureWorkspaceAnno_0.CreateAnnotationClass(string_0, ifields_0, instanceCLSID, classExtensionCLSID, description2.ShapeFieldName, "", ifeatureDataset_1, ifeatureClass_0, iannotateLayerPropertiesCollection2_0, referenceScale, isymbolCollection2_0, true);
        }

        private static void old_acctor_mc()
        {
            m_pObjectClassHelper = null;
        }

        public IFeatureDataset FeatureDataset
        {
            get
            {
                return this.ifeatureDataset_0;
            }
            set
            {
                this.ifeatureDataset_0 = value;
                this.iworkspace_0 = this.ifeatureDataset_0.Workspace;
                ObjectClassShareData.m_IsShapeFile = false;
            }
        }

        public IWorkspace Workspace
        {
            get
            {
                return this.iworkspace_0;
            }
            set
            {
                this.iworkspace_0 = value;
                this.ifeatureDataset_0 = null;
            }
        }
    }
}

