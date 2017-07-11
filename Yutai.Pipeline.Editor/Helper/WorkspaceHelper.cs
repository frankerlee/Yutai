using System;
using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.Pipeline.Editor.Helper
{
    public class WorkspaceHelper
    {
        public static IWorkspace GetWorkspace(IFeatureClass featureClass)
        {
            IDataset pFeatureDataset = featureClass as IDataset;
            if (pFeatureDataset == null)
                return null;
            return pFeatureDataset.Workspace;
        }

        public static IWorkspace GetWorkspace(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            IWorkspaceFactory pWorkspaceFactory;
            switch (fileInfo.Extension.ToLower())
            {
                case ".mdb":
                    pWorkspaceFactory = new AccessWorkspaceFactoryClass();
                    break;
                case ".gdb":
                    pWorkspaceFactory = new FileGDBWorkspaceFactoryClass();
                    break;
                default:
                    pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                    break;
            }
            IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(fileName, 0) as IWorkspace;
            return pWorkspace;
        }

        public static bool Exist(IWorkspace workspace, string featureClassName)
        {
            IWorkspace2 pWorkspace2 = workspace as IWorkspace2;
            return pWorkspace2 != null && pWorkspace2.NameExists[esriDatasetType.esriDTFeatureClass, featureClassName];
        }

        public static IGeometry ConvertGeometry(IGeometry geometry, esriGeometryType type)
        {
            switch (geometry.GeometryType)
            {
                case esriGeometryType.esriGeometryNull:
                    break;
                case esriGeometryType.esriGeometryPoint:
                    break;
                case esriGeometryType.esriGeometryMultipoint:
                    {
                        IPointCollection pointCollection = geometry as IPointCollection;
                        if (pointCollection != null)
                            switch (type)
                            {
                                case esriGeometryType.esriGeometryPoint:
                                    return pointCollection.Point[0];
                            }
                    }
                    break;
                case esriGeometryType.esriGeometryLine:
                    break;
                case esriGeometryType.esriGeometryCircularArc:
                    break;
                case esriGeometryType.esriGeometryEllipticArc:
                    break;
                case esriGeometryType.esriGeometryBezier3Curve:
                    break;
                case esriGeometryType.esriGeometryPath:
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    break;
                case esriGeometryType.esriGeometryRing:
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    break;
                case esriGeometryType.esriGeometryEnvelope:
                    break;
                case esriGeometryType.esriGeometryAny:
                    break;
                case esriGeometryType.esriGeometryBag:
                    break;
                case esriGeometryType.esriGeometryMultiPatch:
                    break;
                case esriGeometryType.esriGeometryTriangleStrip:
                    break;
                case esriGeometryType.esriGeometryTriangleFan:
                    break;
                case esriGeometryType.esriGeometryRay:
                    break;
                case esriGeometryType.esriGeometrySphere:
                    break;
                case esriGeometryType.esriGeometryTriangles:
                    break;
            }
            return geometry;
        }

        public static void LoadFeatureClass(IMap map, IFeatureClass featureClass)
        {
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = featureClass;
            pFeatureLayer.Name = featureClass.AliasName;
            pFeatureLayer.Visible = true;

            map.AddLayer(pFeatureLayer);
        }

        public static bool ExistFeatureClass(IMap map, IFeatureClass featureClass)
        {
            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer pLayer = map.Layer[i];
                if (pLayer is IGroupLayer)
                {
                    ICompositeLayer pCompositeLayer = pLayer as ICompositeLayer;
                    for (int j = 0; j < pCompositeLayer.Count; j++)
                    {
                        ILayer pgLayer = pCompositeLayer.Layer[j];
                        IFeatureLayer pgFeatureLayer = pgLayer as IFeatureLayer;
                        if (pgFeatureLayer == null)
                            continue;
                        if (pgFeatureLayer.FeatureClass == featureClass)
                            return true;
                    }
                }
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                if (pFeatureLayer == null)
                    continue;
                if (pFeatureLayer.FeatureClass == featureClass)
                    return true;
            }
            return false;
        }

        public static IFeatureClass CreateSurveyFeatureClass(IWorkspace workspace, string featureClassName, bool hasZ, IEnvelope pEnvelope)
        {
            if (workspace == null || featureClassName == null)
                return null;
            IObjectClassDescription pObjectClassDescription = new FeatureClassDescriptionClass();
            IFields pFields = pObjectClassDescription.RequiredFields;
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "点号";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldEdit.Length_2 = 50;
            pFieldsEdit.AddField(pField);

            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "代码";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldEdit.Length_2 = 50;
            pFieldsEdit.AddField(pField);

            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "X坐标";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldsEdit.AddField(pField);

            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Y坐标";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldsEdit.AddField(pField);

            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Z坐标";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldsEdit.AddField(pField);

            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "测量组号";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldEdit.Length_2 = 50;
            pFieldsEdit.AddField(pField);

            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "测量人员";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldEdit.Length_2 = 50;
            pFieldsEdit.AddField(pField);

            pField = new FieldClass();
            pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "测量日期";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldsEdit.AddField(pField);

            string strShapeField = "";
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                if (pFields.Field[i].Type == esriFieldType.esriFieldTypeGeometry)
                {
                    pField = pFields.Field[i];
                    strShapeField = pField.Name;
                    pFieldEdit = (IFieldEdit)pField;
                    IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pFieldEdit.GeometryDef;
                    pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                    pGeometryDefEdit.HasZ_2 = hasZ;
                    ISpatialReference pSpatialReference = new UnknownCoordinateSystemClass();
                    pSpatialReference.SetDomain(pEnvelope.XMin, pEnvelope.XMax, pEnvelope.YMin, pEnvelope.YMax);
                    pSpatialReference.SetZDomain(pEnvelope.ZMin, pEnvelope.ZMax);
                    //ISpatialReference pSpatialReference = new UnknownCoordinateSystemClass();
                    //pSpatialReference.SetDomain(-1000000, 1000000, -1000000, 1000000);
                    //pSpatialReference.SetZDomain(-1000, 10000);
                    //pSpatialReference.SetMDomain(0, 0);
                    pGeometryDefEdit.SpatialReference_2 = pSpatialReference;
                    pFieldEdit.GeometryDef_2 = pGeometryDefEdit;
                }
            }
            pFields = (IFields)pFieldsEdit;
            IFieldChecker pFieldChecker = new FieldCheckerClass();
            IEnumFieldError pEnumFieldError = null;
            IFields validatedFields = null;
            pFieldChecker.ValidateWorkspace = workspace;
            pFieldChecker.Validate(pFields, out pEnumFieldError, out validatedFields);

            IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)workspace;
            return pFeatureWorkspace.CreateFeatureClass(featureClassName, validatedFields, new ESRI.ArcGIS.esriSystem.UID(), new ESRI.ArcGIS.esriSystem.UID(), esriFeatureType.esriFTSimple, strShapeField, "");
        }

        public static IPoint CreatePoint(double x, double y, double z, bool hasZ)
        {
            IPoint point = new PointClass
            {
                X = x,
                Y = y
            };
            IZAware pZAware = point as IZAware;
            if (hasZ)
            {
                pZAware.ZAware = true;
                point.Z = z;
            }
            return point;
        }

        public static bool CheckHasZ(IFeatureClass featureClass)
        {
            IFields pFields = featureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField pField = pFields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    return pField.GeometryDef.HasZ;
                }
            }
            return false;
        }

        public static IFeatureClass CreateFeatureClass(IWorkspace workspace, string featureClassName, esriGeometryType type, ISpatialReference spatialReference)
        {
            IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
            IFeatureClassDescription pFeatureClassDescription = new FeatureClassDescriptionClass();
            IObjectClassDescription pObjectClassDescription = pFeatureClassDescription as IObjectClassDescription;
            IFields pFields = pObjectClassDescription.RequiredFields;
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                if (pFieldsEdit.Field[i].Type == esriFieldType.esriFieldTypeGeometry)
                {
                    IFieldEdit pFieldEdit = pFieldsEdit.Field[i] as IFieldEdit;
                    IGeometryDef pGeometryDef = new GeometryDefClass();
                    IGeometryDefEdit pGeometryDefEdit = pGeometryDef as IGeometryDefEdit;
                    pGeometryDefEdit.GeometryType_2 = type;
                    pGeometryDefEdit.SpatialReference_2 = spatialReference;
                    pFieldEdit.GeometryDef_2 = pGeometryDef;
                }
            }

            IFieldChecker pFieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            pFieldChecker.ValidateWorkspace = workspace;
            pFieldChecker.Validate(pFields, out enumFieldError, out validatedFields);

            return pFeatureWorkspace.CreateFeatureClass(featureClassName, validatedFields,
                pObjectClassDescription.InstanceCLSID, pObjectClassDescription.ClassExtensionCLSID,
                esriFeatureType.esriFTSimple, "SHAPE", "");
        }

        public static IFeatureClass CreateFeatureClass(IWorkspace workspace, string featureClassName,
            esriGeometryType type, ISpatialReference spatialReference, IFields fields)
        {
            IFeatureClass pFeatureClass = CreateFeatureClass(workspace, featureClassName, type, spatialReference);
            for (int i = 0; i < fields.FieldCount; i++)
            {
                try
                {
                    IField pField = fields.Field[i];
                    int index = pFeatureClass.FindField(pField.Name);
                    if (index >= 0)
                        continue;
                    if (pField.Type == esriFieldType.esriFieldTypeOID)
                        continue;
                    IField pNewField = new FieldClass();
                    IFieldEdit pNewFieldEdit = pNewField as IFieldEdit;
                    pNewFieldEdit.Name_2 = pField.Name;
                    pNewFieldEdit.AliasName_2 = pField.AliasName;
                    pNewFieldEdit.Type_2 = pField.Type;
                    pNewFieldEdit.Length_2 = pField.Length;
                    pNewFieldEdit.Precision_2 = pField.Precision;
                    pNewFieldEdit.Scale_2 = pField.Scale;
                    pFeatureClass.AddField(pNewField);
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("{0}:{1}", fields.Field[i].Name, e.Message));
                }
            }
            return pFeatureClass;
        }

        public static bool ExistDomain(IWorkspace workspace, string name)
        {
            IWorkspaceDomains pWorkspaceDomains = workspace as IWorkspaceDomains;
            if (pWorkspaceDomains == null)
                return false;
            IDomain pDomain = pWorkspaceDomains.DomainByName[name];
            return pDomain != null;
        }

        public static IDomain AddDomain(IWorkspace workspace, IDomain domain)
        {
            if (domain == null)
                return null;
            IWorkspaceDomains pWorkspaceDomains = workspace as IWorkspaceDomains;
            if (pWorkspaceDomains == null)
                return null;
            IDomain pDomain = pWorkspaceDomains.DomainByName[domain.Name];
            if (pDomain != null)
                return pDomain;
            if (domain.Type == esriDomainType.esriDTCodedValue)
            {
                ICodedValueDomain pCodedValueDomain = new CodedValueDomainClass();
                ICodedValueDomain pOriginCodedValueDomain = domain as ICodedValueDomain;
                for (int i = 0; i < pOriginCodedValueDomain.CodeCount; i++)
                {
                    pCodedValueDomain.AddCode(pOriginCodedValueDomain.Value[i], pOriginCodedValueDomain.Name[i]);
                }
                pDomain = pCodedValueDomain as IDomain;
            }
            else
            {
                IRangeDomain pRangeDomain = new RangeDomainClass();
                IRangeDomain pOriginRangeDomain = domain as IRangeDomain;
                pRangeDomain.MinValue = pOriginRangeDomain.MinValue;
                pRangeDomain.MaxValue = pOriginRangeDomain.MaxValue;
                pDomain = pRangeDomain as IDomain;
            }
            pDomain.Name = domain.Name;
            pDomain.FieldType = domain.FieldType;
            pDomain.Description = domain.Description;
            pDomain.SplitPolicy = esriSplitPolicyType.esriSPTDuplicate;
            pDomain.MergePolicy = esriMergePolicyType.esriMPTDefaultValue;
            pWorkspaceDomains.AddDomain(pDomain);
            return pDomain;
        }

        public static List<IDataset> GeTables(IWorkspace workspace)
        {
            List<IDataset> list = new List<IDataset>();

            try
            {
                IEnumDataset pEnumDataset = workspace.Datasets[esriDatasetType.esriDTTable];
                pEnumDataset.Reset();
                IDataset pDataset;
                while ((pDataset = pEnumDataset.Next()) != null)
                {
                    list.Add(pDataset);
                }
            }
            catch (Exception e)
            {
            }

            return list;
        }

        public static List<IFeatureClass> GetFeatureClasses(IDataset pDataset)
        {
            List<IFeatureClass> pList = new List<IFeatureClass>();
            IFeatureClassContainer ipFcContain = (IFeatureClassContainer)pDataset;
            IEnumFeatureClass ipFcEnum = ipFcContain.Classes;
            IFeatureClass ipFtClass;
            while ((ipFtClass = ipFcEnum.Next()) != null)
            {
                pList.Add(ipFtClass);
            }
            return pList;
        }

        public static List<IFeatureClass> GetFeatureClasses(IWorkspace workspace)
        {
            List<IFeatureClass> pList = new List<IFeatureClass>();
            IEnumDataset pEnumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureClass];
            IDataset pDataset;
            while ((pDataset = pEnumDataset.Next()) != null)
            {
                IFeatureClass pFeatureClass = pDataset as IFeatureClass;
                pList.Add(pFeatureClass);
            }
            pEnumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureDataset];
            while ((pDataset = pEnumDataset.Next()) != null)
            {
                pList.AddRange(GetFeatureClasses(pDataset));
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumDataset);
            return pList;
        }

        public static IFeatureClass CreateFeatureClassByAxfFeatureLayer(IWorkspace workspace, IFeatureLayer axfFeatureLayer, bool isConvertToPoint = false, string className = "")
        {
            IFeatureWorkspace pFeatureWorkspace = workspace as IFeatureWorkspace;
            if (pFeatureWorkspace == null)
                return null;
            IFields pFields = GetFields(axfFeatureLayer.FeatureClass);
            if (pFields == null)
                return null;
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
            if (pFieldsEdit == null)
                return null;
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Name_2 = "AXF_STATUS";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            pFieldsEdit.AddField(pField);
            if (isConvertToPoint && axfFeatureLayer.FeatureClass.ShapeType == esriGeometryType.esriGeometryMultipoint)
            {
                for (int i = 0; i < pFieldsEdit.FieldCount; i++)
                {
                    if (pFieldsEdit.Field[i].Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        IFieldEdit geoFieldEdit = pFieldsEdit.Field[i] as IFieldEdit;
                        IGeometryDefEdit pGeometryDefEdit = geoFieldEdit.GeometryDef as IGeometryDefEdit;
                        pGeometryDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                        geoFieldEdit.GeometryDef_2 = pGeometryDefEdit;
                    }
                }
            }
            IFieldChecker pFieldChecker = new FieldCheckerClass();
            IEnumFieldError pEnumFieldError = null;
            IFields validateFields = null;
            pFieldChecker.ValidateWorkspace = workspace;
            pFieldChecker.Validate(pFieldsEdit, out pEnumFieldError, out validateFields);
            if (string.IsNullOrEmpty(className))
                return pFeatureWorkspace.CreateFeatureClass(className, validateFields, axfFeatureLayer.FeatureClass.CLSID,
                    axfFeatureLayer.FeatureClass.EXTCLSID, axfFeatureLayer.FeatureClass.FeatureType, "SHAPE", null);
            else
                return pFeatureWorkspace.CreateFeatureClass(axfFeatureLayer.Name, validateFields, axfFeatureLayer.FeatureClass.CLSID,
                    axfFeatureLayer.FeatureClass.EXTCLSID, axfFeatureLayer.FeatureClass.FeatureType, "SHAPE", null);
        }

        public static IFields GetFields(IFeatureClass featureClass)
        {
            IFields pNewFields = new FieldsClass();
            IFields pFields = featureClass.Fields;
            IFieldsEdit pNewFieldsEdit = pNewFields as IFieldsEdit;
            int fieldCount = pFields.FieldCount;
            for (int i = 0; i < fieldCount; i++)
            {
                IField pField = pFields.Field[i];
                if (pField.Name == "OBJECTID_1" || pField.Name == "OBJECTID")
                    continue;
                if (pField.Editable == false)
                    continue;
                IField pNewField = new FieldClass();
                IFieldEdit pNewFieldEdit = pNewField as IFieldEdit;
                pNewFieldEdit.Name_2 = pField.Name;
                pNewFieldEdit.AliasName_2 = pField.AliasName;

                if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    pNewFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    IGeometryDef pGeometryDef = new GeometryDefClass();
                    IGeometryDefEdit pGeometryDefEdit = pGeometryDef as IGeometryDefEdit;
                    pGeometryDefEdit.GeometryType_2 = pField.GeometryDef.GeometryType;
                    if (pField.GeometryDef.SpatialReference.Name == "Unknown")
                    {
                        ISpatialReference pSpatialReference = new UnknownCoordinateSystemClass();
                        pSpatialReference.SetDomain(-1000000, 1000000, -1000000, 1000000);
                        pSpatialReference.SetZDomain(0, 0);
                        pSpatialReference.SetMDomain(0, 0);
                        pGeometryDefEdit.SpatialReference_2 = pSpatialReference;
                    }
                    else
                        pGeometryDefEdit.SpatialReference_2 = pField.GeometryDef.SpatialReference;
                    pNewFieldEdit.GeometryDef_2 = pGeometryDefEdit;
                }
                else
                {
                    pNewFieldEdit.DefaultValue_2 = pField.DefaultValue;
                    pNewFieldEdit.DomainFixed_2 = pField.DomainFixed;
                    pNewFieldEdit.Domain_2 = pField.Domain;
                    pNewFieldEdit.Precision_2 = pField.Precision;
                    pNewFieldEdit.Required_2 = pField.Required;
                    pNewFieldEdit.Scale_2 = pField.Scale;
                    pNewFieldEdit.Type_2 = pField.Type;
                }
                pNewFieldsEdit.AddField(pNewField);
            }
            return pNewFieldsEdit;
        }

        public static void FeatureClassToFeatureClass(IFeatureClass targetFeatureClass, IFeatureClass sourceFeatureClass, int objectid, int axfStatus)
        {
            IQueryFilter pQueryFilter = new QueryFilterClass();
            switch (objectid)
            {
                case -1:
                    pQueryFilter.WhereClause = "OBJECTID IS NULL";
                    break;
                default:
                    pQueryFilter.WhereClause = string.Format("OBJECTID = {0}", objectid);
                    break;
            }
            IFeatureCursor pSourceFeatureCursor = sourceFeatureClass.Search(pQueryFilter, false);
            IFeature pSourceFeature;
            while ((pSourceFeature = pSourceFeatureCursor.NextFeature()) != null)
            {
                IFeature pNewFeature = targetFeatureClass.CreateFeature();
                pNewFeature.Shape = ConvertGeometry(pSourceFeature.ShapeCopy, pNewFeature.Shape.GeometryType);
                IField pField;
                for (int i = 0; i < pNewFeature.Fields.FieldCount; i++)
                {
                    pField = pNewFeature.Fields.Field[i];
                    if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                        continue;
                    int idx = pSourceFeature.Fields.FindField(pField.Name);
                    if (pField.Editable && idx != -1)
                        if ((pSourceFeature.Value[idx] is DBNull) == false)
                            pNewFeature.Value[i] = pSourceFeature.Value[idx];
                }
                int idxAxfStatus = pNewFeature.Fields.FindField("AXF_STATUS");
                pNewFeature.Value[idxAxfStatus] = axfStatus;
                pNewFeature.Store();
            }
        }

        public static ITable CreateObjectClass(IWorkspace workspace, String className, IFields fields)
        {
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            IObjectClassDescription ocDescription = new ObjectClassDescriptionClass();

            // Use IFieldChecker to create a validated fields collection.
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IEnumFieldError enumFieldError = null;
            IFields validatedFields = null;
            fieldChecker.ValidateWorkspace = workspace;
            fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

            // The enumFieldError enumerator can be inspected at this point to determine 
            // which fields were modified during validation.
            ITable table = featureWorkspace.CreateTable(className, validatedFields,
                ocDescription.InstanceCLSID, null, "");
            return table;
        }
    }
}
