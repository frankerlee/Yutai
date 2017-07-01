using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Yutai.ArcGIS.Common;

namespace Yutai.Plugins.Printing.Commands.Carto
{
    public class ClipHelper
    {
        private static void InsertFeature(IFeatureCursor ifeatureCursor_0, IFeatureBuffer ifeatureBuffer_0,
            IFields ifields_0, IFeature ifeature_0, IGeometry igeometry_0, IGeometry igeometry_1)
        {
            IFields fields = ifeatureBuffer_0.Fields;
            IFields field = ifeature_0.Fields;
            for (int i = 0; i < field.FieldCount; i++)
            {
                IField field1 = field.Field[i];
                if ((field1.Type == esriFieldType.esriFieldTypeGeometry
                    ? false
                    : field1.Type != esriFieldType.esriFieldTypeOID))
                {
                    int value = fields.FindField(ifields_0.Field[i].Name);
                    if (value != -1)
                    {
                        field1 = ifeatureBuffer_0.Fields.Field[value];
                        if ((field1.Type == esriFieldType.esriFieldTypeGeometry ||
                             field1.Type == esriFieldType.esriFieldTypeOID
                            ? false
                            : field1.Editable))
                        {
                            try
                            {
                                ifeatureBuffer_0.Value[value] = ifeature_0.Value[i];
                            }
                            catch (Exception exception)
                            {
                            }
                        }
                    }
                }
            }
            try
            {
                IGeometry zAware = null;
                if (igeometry_1 != null)
                {
                    bool flag = false;
                    try
                    {
                        flag = (igeometry_1 as IRelationalOperator).Contains(igeometry_0);
                    }
                    catch
                    {
                    }
                    if (!flag)
                    {
                        ITopologicalOperator igeometry1 = (ITopologicalOperator) igeometry_1;
                        if (igeometry_0.GeometryType == esriGeometryType.esriGeometryMultipoint)
                        {
                            zAware = igeometry1.Intersect(igeometry_0, esriGeometryDimension.esriGeometry0Dimension);
                            (zAware as IZAware).ZAware = (igeometry_0 as IZAware).ZAware;
                            (zAware as IMAware).MAware = (igeometry_0 as IMAware).MAware;
                        }
                        else if (igeometry_0.GeometryType == esriGeometryType.esriGeometryPolygon)
                        {
                            zAware = igeometry1.Intersect(igeometry_0, esriGeometryDimension.esriGeometry2Dimension);
                            (zAware as IZAware).ZAware = (igeometry_0 as IZAware).ZAware;
                            if ((zAware as IZAware).ZAware)
                            {
                                (zAware as IZ).SetConstantZ((igeometry_0 as IZ).ZMin);
                            }
                            (zAware as IMAware).MAware = (igeometry_0 as IMAware).MAware;
                        }
                        else if (igeometry_0.GeometryType != esriGeometryType.esriGeometryPolyline)
                        {
                            zAware = igeometry_0;
                        }
                        else
                        {
                            try
                            {
                                zAware = igeometry1.Intersect(igeometry_0, esriGeometryDimension.esriGeometry1Dimension);
                                (zAware as IZAware).ZAware = (igeometry_0 as IZAware).ZAware;
                                (zAware as IMAware).MAware = (igeometry_0 as IMAware).MAware;
                            }
                            catch (Exception exception1)
                            {
                                zAware = igeometry_0;
                            }
                        }
                    }
                    else
                    {
                        zAware = igeometry_0;
                    }
                }
                else
                {
                    zAware = igeometry_0;
                }
                ifeatureBuffer_0.Shape = zAware;
                ifeatureCursor_0.InsertFeature(ifeatureBuffer_0);
            }
            catch (Exception exception2)
            {
            }
        }

        private static void ExtractSpecifyRegFeatures(IWorkspaceName iworkspaceName_0, IMap imap_0,
            IGeometry igeometry_0)
        {
            ISelectionEnvironment selectionEnvironment = new SelectionEnvironment();
            selectionEnvironment.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            try
            {
                imap_0.SelectByShape(igeometry_0, selectionEnvironment, false);
            }
            catch (COMException ex)
            {
                if (ex.ErrorCode == -2147467259)
                {
                    System.Windows.Forms.MessageBox.Show("执行查询时产生错误。空间索引不存在", "选择");
                }
            }
            catch (Exception)
            {
            }
            IWorkspace workspace = (iworkspaceName_0 as IName).Open() as IWorkspace;
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                ILayer layer = imap_0.get_Layer(i);
                if (layer is IFeatureLayer)
                {
                    ClipHelper.ExtractSelectFeatureFormLayer((IFeatureLayer) layer, (IFeatureWorkspace) workspace,
                        igeometry_0);
                }
            }
        }


        public static IFeatureCursor searchFeatureFormLayer(ILayer ilayer_0, IGeometry igeometry_0)
        {
            IFeatureCursor featureCursor;
            if (ilayer_0 != null)
            {
                IFeatureCursor featureCursor1 = null;
                IFeatureLayer ilayer0 = ilayer_0 as IFeatureLayer;
                if (ilayer0.FeatureClass != null)
                {
                    ISpatialFilter spatialFilterClass = new SpatialFilter()
                    {
                        Geometry = igeometry_0,
                        GeometryField = ilayer0.FeatureClass.ShapeFieldName
                    };
                    switch (ilayer0.FeatureClass.ShapeType)
                    {
                        case esriGeometryType.esriGeometryPoint:
                        {
                            spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                            break;
                        }
                        case esriGeometryType.esriGeometryMultipoint:
                        {
                            spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            break;
                        }
                        case esriGeometryType.esriGeometryPolyline:
                        {
                            spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            break;
                        }
                        case esriGeometryType.esriGeometryPolygon:
                        {
                            spatialFilterClass.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                            break;
                        }
                        default:
                        {
                            goto case esriGeometryType.esriGeometryMultipoint;
                        }
                    }
                    IQueryFilter queryFilter = spatialFilterClass;
                    try
                    {
                        featureCursor1 = ilayer0.Search(queryFilter, false);
                    }
                    catch (Exception exception)
                    {
                    }
                    featureCursor = featureCursor1;
                }
                else
                {
                    featureCursor = null;
                }
            }
            else
            {
                featureCursor = null;
            }
            return featureCursor;
        }

        public static void ExtractSelectedFeatures(IWorkspaceName iworkspaceName_0, IMap imap_0)
        {
            IWorkspace workspace = (iworkspaceName_0 as IName).Open() as IWorkspace;
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                ILayer layer = imap_0.get_Layer(i);
                if (layer.Visible)
                {
                    if (layer is IGroupLayer)
                    {
                        ClipHelper.ExtractSelectedFeatures(workspace as IFeatureWorkspace, layer as ICompositeLayer);
                    }
                    if (layer is IFeatureLayer)
                    {
                        ClipHelper.ExtractSelectFeatureFormLayer(layer as IFeatureLayer, workspace as IFeatureWorkspace,
                            null);
                    }
                }
            }
        }

        private static void ExtractSelectedFeatures(IFeatureWorkspace ifeatureWorkspace_0,
            ICompositeLayer icompositeLayer_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.get_Layer(i);
                if (layer.Visible)
                {
                    if (layer is IGroupLayer)
                    {
                        ClipHelper.ExtractSelectedFeatures(ifeatureWorkspace_0, layer as ICompositeLayer);
                    }
                    else if (layer is IFeatureLayer)
                    {
                        ClipHelper.ExtractSelectFeatureFormLayer(layer as IFeatureLayer, ifeatureWorkspace_0, null);
                    }
                }
            }
        }

        private static void ExtractSpecifyHRegFeatures(IFeatureWorkspace ifeatureWorkspace_0,
            ICompositeLayer icompositeLayer_0, IGeometry igeometry_0)
        {
            for (int i = 0; i < icompositeLayer_0.Count; i++)
            {
                ILayer layer = icompositeLayer_0.Layer[i];
                if (layer.Visible)
                {
                    if (layer is IGroupLayer)
                    {
                        ClipHelper.ExtractSpecifyHRegFeatures(ifeatureWorkspace_0, layer as ICompositeLayer, igeometry_0);
                    }
                    else if (layer is IFeatureLayer)
                    {
                        try
                        {
                            IEnvelope areaOfInterest = layer.AreaOfInterest;
                            ITopologicalOperator topologicalOperator =
                                (ITopologicalOperator) ((IClone) igeometry_0).Clone();
                            topologicalOperator.Simplify();
                            if (topologicalOperator.IsSimple)
                            {
                                topologicalOperator.Clip(areaOfInterest);
                            }
                        }
                        catch (Exception exception)
                        {
                        }
                        if (!igeometry_0.IsEmpty)
                        {
                            ClipHelper.ExtractSelectFeatureFormFeatureCursor(layer.Name,
                                ClipHelper.searchFeatureFormLayer(layer, igeometry_0), ifeatureWorkspace_0, igeometry_0);
                        }
                    }
                }
            }
        }

        public static void ExtractSpecifyHRegFeatures(IWorkspaceName iworkspaceName_0, IMap imap_0,
            IGeometry igeometry_0)
        {
            (new SelectionEnvironment()).CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            IWorkspace workspace = (iworkspaceName_0 as IName).Open() as IWorkspace;
            for (int i = 0; i < imap_0.LayerCount; i++)
            {
                ILayer layer = imap_0.Layer[i];
                if (layer.Visible)
                {
                    if (layer is IGroupLayer)
                    {
                        ClipHelper.ExtractSpecifyHRegFeatures(workspace as IFeatureWorkspace, layer as ICompositeLayer,
                            igeometry_0);
                    }
                    if (layer is IFeatureLayer)
                    {
                        try
                        {
                            IEnvelope areaOfInterest = layer.AreaOfInterest;
                            ITopologicalOperator topologicalOperator =
                                (ITopologicalOperator) ((IClone) igeometry_0).Clone();
                            topologicalOperator.Simplify();
                            if (topologicalOperator.IsSimple)
                            {
                                topologicalOperator.Clip(areaOfInterest);
                            }
                        }
                        catch (Exception exception)
                        {
                        }
                        if (!igeometry_0.IsEmpty)
                        {
                            ClipHelper.ExtractSelectFeatureFormFeatureCursor(layer.Name,
                                ClipHelper.searchFeatureFormLayer(layer, igeometry_0), (IFeatureWorkspace) workspace,
                                igeometry_0);
                        }
                    }
                }
            }
            workspace = null;
        }

        public static void ExtractSpecifyHRegFeatures(IWorkspaceName iworkspaceName_0, IMap imap_0,
            IGeometry igeometry_0, IMap imap_1)
        {
            (new SelectionEnvironment()).CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            IWorkspace workspace = (iworkspaceName_0 as IName).Open() as IWorkspace;
            for (int i = imap_0.LayerCount - 1; i >= 0; i--)
            {
                ILayer layer = imap_0.Layer[i];
                if ((!layer.Visible ? false : layer is IFeatureLayer))
                {
                    try
                    {
                        IEnvelope areaOfInterest = layer.AreaOfInterest;
                        ITopologicalOperator topologicalOperator = (ITopologicalOperator) ((IClone) igeometry_0).Clone();
                        topologicalOperator.Simplify();
                        if (topologicalOperator.IsSimple)
                        {
                            topologicalOperator.Clip(areaOfInterest);
                        }
                    }
                    catch (Exception exception)
                    {
                    }
                    if (!igeometry_0.IsEmpty)
                    {
                        ClipHelper.ExtractSelectFeatureFormFeatureCursor(layer,
                            ClipHelper.searchFeatureFormLayer(layer, igeometry_0), (IFeatureWorkspace) workspace,
                            igeometry_0, imap_1);
                    }
                }
            }
            Marshal.ReleaseComObject(workspace);
            workspace = null;
        }


        public static void ClipMapByRegion(IWorkspaceName iworkspaceName_0, IMap imap_0, IGeometry igeometry_0,
            IMap imap_1)
        {
            (new SelectionEnvironment()).CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            IWorkspace workspace = (iworkspaceName_0 as IName).Open() as IWorkspace;
            TempAcceessBD tempAcceessBD = new TempAcceessBD();
            tempAcceessBD.CreateTempDB();
            IFeatureClass featureClass = tempAcceessBD.CreateFeatureClass(igeometry_0);
            for (int i = imap_0.LayerCount - 1; i >= 0; i--)
            {
                ILayer layer = imap_0.Layer[i];
                if ((!layer.Visible ? false : layer is IFeatureLayer))
                {
                    try
                    {
                        IEnvelope areaOfInterest = layer.AreaOfInterest;
                        ITopologicalOperator topologicalOperator = (ITopologicalOperator) ((IClone) igeometry_0).Clone();
                        topologicalOperator.Simplify();
                        if (topologicalOperator.IsSimple)
                        {
                            topologicalOperator.Clip(areaOfInterest);
                        }
                    }
                    catch (Exception exception)
                    {
                    }
                    if (!igeometry_0.IsEmpty)
                    {
                        ClipHelper.ClipLayer(layer as IFeatureLayer, featureClass, (IFeatureWorkspace) workspace, imap_1);
                    }
                }
            }
            Marshal.ReleaseComObject(workspace);
            workspace = null;
        }

        public static void ClipLayer(IFeatureLayer ifeatureLayer_0, IFeatureClass ifeatureClass_0,
            IFeatureWorkspace ifeatureWorkspace_0, IMap imap_0)
        {
            IEnumFieldError enumFieldError;
            IFields field;
            string str;
            try
            {
                string name = ifeatureLayer_0.Name;
                IFields fields = ifeatureLayer_0.FeatureClass.Fields;
                IFieldChecker fieldCheckerClass = new FieldChecker()
                {
                    ValidateWorkspace = ifeatureWorkspace_0 as IWorkspace
                };
                fieldCheckerClass.Validate(fields, out enumFieldError, out field);
                fieldCheckerClass.ValidateTableName(name, out str);
                string name1 = "";
                int num = 0;
                while (true)
                {
                    if (num < field.FieldCount)
                    {
                        IField field1 = field.Field[num];
                        if (field1.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            name1 = field1.Name;
                            IGeometryDef geometryDef = field1.GeometryDef;
                            (geometryDef as IGeometryDefEdit).SpatialReference_2 = geometryDef.SpatialReference;
                            (field1 as IFieldEdit).GeometryDef_2 = geometryDef;
                            break;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                name = str;
                IFeatureClass featureClass = ifeatureWorkspace_0.CreateFeatureClass(name, field, null, null,
                    esriFeatureType.esriFTSimple, name1, "");
                IFeatureClassName fullName = (featureClass as IDataset).FullName as IFeatureClassName;
                IBasicGeoprocessor basicGeoprocessorClass = new BasicGeoprocessor();
                IFeatureClass featureClass1 = basicGeoprocessorClass.Clip(ifeatureLayer_0 as ITable, false,
                    ifeatureClass_0 as ITable, false, 0, fullName);
                if (imap_0 != null)
                {
                    IFeatureLayer featureLayerClass = new FeatureLayer();
                    (featureLayerClass as IGeoFeatureLayer).Renderer = (ifeatureLayer_0 as IGeoFeatureLayer).Renderer;
                    featureLayerClass.Name = (featureClass as IDataset).Name;
                    featureLayerClass.FeatureClass = featureClass1;
                    imap_0.AddLayer(featureLayerClass);
                }
            }
            catch
            {
            }
        }


        public static void ExtractSelectFeatureFormFeatureCursor(ILayer ilayer_0, IFeatureCursor ifeatureCursor_0,
            IFeatureWorkspace ifeatureWorkspace_0, IGeometry igeometry_0, IMap imap_0)
        {
            IEnumFieldError enumFieldError;
            IFields field;
            string str;
            string name = ilayer_0.Name;
            if (ifeatureCursor_0 != null)
            {
                IFields fields = ifeatureCursor_0.Fields;
                IFieldChecker fieldCheckerClass = new FieldChecker()
                {
                    ValidateWorkspace = ifeatureWorkspace_0 as IWorkspace
                };
                fieldCheckerClass.Validate(fields, out enumFieldError, out field);
                fieldCheckerClass.ValidateTableName(name, out str);
                string name1 = "";
                int num = 0;
                while (true)
                {
                    if (num < field.FieldCount)
                    {
                        IField field1 = field.Field[num];
                        if (field1.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            name1 = field1.Name;
                            IGeometryDef geometryDef = field1.GeometryDef;
                            ISpatialReference spatialReference = geometryDef.SpatialReference;
                            SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspace_0 as IGeodatabaseRelease,
                                spatialReference, false);
                            (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                            (field1 as IFieldEdit).GeometryDef_2 = geometryDef;
                            break;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                name = str;
                IFeatureClass featureClass = ifeatureWorkspace_0.CreateFeatureClass(name, field, null, null,
                    esriFeatureType.esriFTSimple, name1, "");
                IFeature feature = ifeatureCursor_0.NextFeature();
                if (feature != null)
                {
                    IFeatureCursor featureCursor = featureClass.Insert(true);
                    IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
                    while (feature != null)
                    {
                        if (feature.Shape != null)
                        {
                            try
                            {
                                ClipHelper.InsertFeature(featureCursor, featureBuffer, field, feature, feature.Shape,
                                    igeometry_0);
                            }
                            catch (Exception exception)
                            {
                            }
                        }
                        feature = ifeatureCursor_0.NextFeature();
                    }
                    featureCursor.Flush();
                    IFeatureLayer featureLayerClass = new FeatureLayer();
                    (featureLayerClass as IGeoFeatureLayer).Renderer = (ilayer_0 as IGeoFeatureLayer).Renderer;
                    featureLayerClass.Name = (featureClass as IDataset).Name;
                    featureLayerClass.FeatureClass = featureClass;
                    imap_0.AddLayer(featureLayerClass);
                    featureBuffer = null;
                    featureCursor = null;
                }
            }
        }

        public static void ExtractSelectFeatureFormFeatureCursor(string string_0, IFeatureCursor ifeatureCursor_0,
            IFeatureWorkspace ifeatureWorkspace_0, IGeometry igeometry_0)
        {
            IEnumFieldError enumFieldError;
            IFields field;
            string str;
            string string0;
            int num;
            if (ifeatureCursor_0 != null)
            {
                IFields fields = ifeatureCursor_0.Fields;
                IFieldChecker fieldCheckerClass = new FieldChecker()
                {
                    ValidateWorkspace = ifeatureWorkspace_0 as IWorkspace
                };
                fieldCheckerClass.Validate(fields, out enumFieldError, out field);
                fieldCheckerClass.ValidateTableName(string_0, out str);
                string name = "";
                int num1 = 0;
                while (true)
                {
                    if (num1 < field.FieldCount)
                    {
                        IField field1 = field.Field[num1];
                        if (field1.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            name = field1.Name;
                            IGeometryDef geometryDef = field1.GeometryDef;
                            ISpatialReference spatialReference = geometryDef.SpatialReference;
                            SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspace_0 as IGeodatabaseRelease,
                                spatialReference, false);
                            (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                            (field1 as IFieldEdit).GeometryDef_2 = geometryDef;
                            break;
                        }
                        else
                        {
                            num1++;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                string_0 = str;
                if (((ifeatureWorkspace_0 as IWorkspace).Type == esriWorkspaceType.esriLocalDatabaseWorkspace
                    ? false
                    : (ifeatureWorkspace_0 as IWorkspace).Type != esriWorkspaceType.esriRemoteDatabaseWorkspace))
                {
                    string str1 = string.Concat((ifeatureWorkspace_0 as IWorkspace).PathName, "\\", string_0);
                    string0 = string.Concat(str1, ".shp");
                    num = 1;
                    while (File.Exists(string0))
                    {
                        string0 = string.Concat(str1, "_", num.ToString(), ".shp");
                        num++;
                    }
                    string_0 = System.IO.Path.GetFileNameWithoutExtension(string0);
                }
                else
                {
                    IWorkspace2 ifeatureWorkspace0 = ifeatureWorkspace_0 as IWorkspace2;
                    string0 = string_0;
                    num = 1;
                    while (ifeatureWorkspace0.NameExists[esriDatasetType.esriDTFeatureClass, string0])
                    {
                        string0 = string.Concat(string_0, "_", num.ToString());
                        num++;
                    }
                    string_0 = string0;
                }
                try
                {
                    IFeatureClass featureClass = ifeatureWorkspace_0.CreateFeatureClass(string_0, field, null, null,
                        esriFeatureType.esriFTSimple, name, "");
                    IFeature feature = ifeatureCursor_0.NextFeature();
                    IFeatureCursor featureCursor = featureClass.Insert(true);
                    IFeatureBuffer featureBuffer = featureClass.CreateFeatureBuffer();
                    while (feature != null)
                    {
                        if (feature.Shape != null)
                        {
                            ClipHelper.InsertFeature(featureCursor, featureBuffer, field, feature, feature.Shape,
                                igeometry_0);
                        }
                        feature = ifeatureCursor_0.NextFeature();
                    }
                    featureCursor.Flush();
                    Marshal.ReleaseComObject(featureClass);
                    featureClass = null;
                    Marshal.ReleaseComObject(featureBuffer);
                    featureBuffer = null;
                    Marshal.ReleaseComObject(featureCursor);
                    featureCursor = null;
                }
                catch
                {
                }
                Marshal.ReleaseComObject(ifeatureCursor_0);
                ifeatureCursor_0 = null;
            }
        }

        public static void ExtractSelectFeatureFormLayer(IFeatureLayer ifeatureLayer_0,
            IFeatureWorkspace ifeatureWorkspace_0, IGeometry igeometry_0)
        {
            try
            {
                IFeatureSelection featureSelection = (IFeatureSelection) ifeatureLayer_0;
                if (featureSelection.SelectionSet.Count != 0)
                {
                    IFeatureClass featureClass = ifeatureLayer_0.FeatureClass;
                    IFields fields = featureClass.Fields;
                    IFieldChecker fieldChecker = new FieldChecker();
                    fieldChecker.InputWorkspace = (featureClass as IDataset).Workspace;
                    fieldChecker.ValidateWorkspace = (ifeatureWorkspace_0 as IWorkspace);
                    IEnumFieldError enumFieldError;
                    IFields fields2;
                    fieldChecker.Validate(fields, out enumFieldError, out fields2);
                    enumFieldError.Reset();
                    IFieldError fieldError = enumFieldError.Next();
                    string text = "";
                    while (fieldError != null)
                    {
                        fieldError.FieldError.ToString();
                        IField field = fields.get_Field(fieldError.FieldIndex);
                        IField field2 = fields2.get_Field(fieldError.FieldIndex);
                        string text2 = text;
                        text = string.Concat(new string[]
                        {
                            text2,
                            field2.Name,
                            " reason: ",
                            field.Name,
                            "  "
                        });
                        fieldError = enumFieldError.Next();
                    }
                    new Hashtable();
                    string text3 = ifeatureLayer_0.Name;
                    string text4;
                    fieldChecker.ValidateTableName(text3, out text4);
                    text3 = text4;
                    string shapeFieldName = "";
                    IFieldsEdit fieldsEdit = new Fields() as IFieldsEdit;
                    for (int i = fields2.FieldCount - 1; i >= 0; i--)
                    {
                        IField field3 = fields2.get_Field(i);
                        if (field3.Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            shapeFieldName = field3.Name;
                            IGeometryDef geometryDef = field3.GeometryDef;
                            ISpatialReference spatialReference =
                                (geometryDef.SpatialReference as IClone).Clone() as ISpatialReference;
                            SpatialReferenctOperator.ChangeCoordinateSystem(ifeatureWorkspace_0 as IGeodatabaseRelease,
                                spatialReference, false);
                            (geometryDef as IGeometryDefEdit).SpatialReference_2 = spatialReference;
                            (field3 as IFieldEdit).GeometryDef_2 = geometryDef;
                        }
                        if ((ifeatureWorkspace_0 as IWorkspace).Type == esriWorkspaceType.esriFileSystemWorkspace)
                        {
                            if (field3.Type != esriFieldType.esriFieldTypeBlob)
                            {
                                fieldsEdit.AddField(field3);
                            }
                        }
                        else
                        {
                            fieldsEdit.AddField(field3);
                        }
                    }
                    IFeatureClass featureClass2 = null;
                    try
                    {
                        featureClass2 = ifeatureWorkspace_0.CreateFeatureClass(text3, fieldsEdit, null, null,
                            esriFeatureType.esriFTSimple, shapeFieldName, "");
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("无法创建输出要素类!");
                        return;
                    }
                    IFeatureCursor featureCursor = featureClass2.Insert(true);
                    IFeatureBuffer featureBuffer = featureClass2.CreateFeatureBuffer();
                    ICursor cursor;
                    featureSelection.SelectionSet.Search(null, false, out cursor);
                    IFeatureCursor featureCursor2 = (IFeatureCursor) cursor;
                    for (IFeature feature = featureCursor2.NextFeature();
                        feature != null;
                        feature = featureCursor2.NextFeature())
                    {
                        if (feature.Shape != null)
                        {
                            ClipHelper.InsertFeature(featureCursor, featureBuffer, fields2, feature, feature.Shape,
                                igeometry_0);
                        }
                    }
                    featureCursor.Flush();
                    Marshal.ReleaseComObject(featureBuffer);
                    Marshal.ReleaseComObject(featureCursor);
                }
            }
            catch
            {
            }
        }
    }
}