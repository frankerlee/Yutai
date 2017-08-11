using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataInterop;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.Pipeline.Editor.SqlCe;

namespace Yutai.Pipeline.Editor.Helper
{
    public class FeatureClassUtil
    {
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

        public static bool CheckHasM(IFeatureClass featureClass)
        {
            IFields pFields = featureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField pField = pFields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    return pField.GeometryDef.HasM;
                }
            }
            return false;
        }

        public static void AddZ(IFeatureClass featureClass)
        {
            if (CheckHasZ(featureClass))
                return;
            IGeometryDef pGeometryDef = GetGeometryDef(featureClass);
            IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGeometryDefEdit.HasZ_2 = true;
        }

        public static void AddM(IFeatureClass featureClass)
        {
            if (CheckHasM(featureClass))
                return;
            IGeometryDef pGeometryDef = GetGeometryDef(featureClass);
            IGeometryDefEdit pGeometryDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGeometryDefEdit.HasM_2 = true;
        }

        public static void DeleteFeatureClass(IFeatureClass featureClass)
        {
            IDataset pDataset = featureClass as IDataset;
            if (pDataset == null)
                return;
            pDataset.CanDelete();
            pDataset.Delete();
        }

        public static IGeometryDef GetGeometryDef(IFeatureClass featureClass)
        {
            IFields pFields = featureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField pField = pFields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    return pField.GeometryDef;
                }
            }
            return new GeometryDefClass();
        }

        public static void DeleteField(IFeatureClass featureClass, string fieldName)
        {
            int idx = featureClass.FindField(fieldName);
            if (idx < 0)
                return;
            IField field = featureClass.Fields.Field[idx];
            featureClass.DeleteField(field);
        }

        public static void AddField(IFeatureClass featureClass, string fieldName, esriFieldType type, IDomain domain = null, string aliasName = null)
        {
            IField field = new FieldClass();
            IFieldEdit fieldEdit = field as IFieldEdit;
            fieldEdit.Name_2 = fieldName;
            if (string.IsNullOrEmpty(aliasName))
                fieldEdit.AliasName_2 = fieldName;
            else
                fieldEdit.AliasName_2 = aliasName;
            fieldEdit.Type_2 = type;
            fieldEdit.Editable_2 = true;
            if (domain != null)
                fieldEdit.Domain_2 = domain;
            featureClass.AddField(field);
        }

        public static void AddStringField(IFeatureClass featureClass, string fieldName, int length, IDomain domain = null, string aliasName = null)
        {
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Name_2 = fieldName;
            if (string.IsNullOrEmpty(aliasName))
                pFieldEdit.AliasName_2 = fieldName;
            else
                pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldEdit.Length_2 = length;
            if (domain != null)
                pFieldEdit.Domain_2 = domain;
            featureClass.AddField(pField);
        }

        public static void AddDoubleField(IFeatureClass featureClass, string fieldName, int length, int precision, IDomain domain = null, string aliasName = null)
        {
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Name_2 = fieldName;
            if (string.IsNullOrEmpty(aliasName))
                pFieldEdit.AliasName_2 = fieldName;
            else
                pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldEdit.Length_2 = length;
            pFieldEdit.Precision_2 = precision;
            if (domain != null)
                pFieldEdit.Domain_2 = domain;
            featureClass.AddField(pField);
        }

        public static void AddDateField(IFeatureClass featureClass, string fieldName, string aliasName = null)
        {
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Name_2 = fieldName;
            if (string.IsNullOrEmpty(aliasName))
                pFieldEdit.AliasName_2 = fieldName;
            else
                pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDate;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            featureClass.AddField(pField);
        }

        public static void AddIntField(IFeatureClass featureClass, string fieldName, int length, IDomain domain = null, string aliasName = null)
        {
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Name_2 = fieldName;
            if (string.IsNullOrEmpty(aliasName))
                pFieldEdit.AliasName_2 = fieldName;
            else
                pFieldEdit.AliasName_2 = aliasName;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldEdit.Length_2 = length;
            if (domain != null)
                pFieldEdit.Domain_2 = domain;
            featureClass.AddField(pField);
        }

        public static void EditDoubleField(IFeatureClass featureClass, string fieldName, int length, int precision,
            IDomain domain = null)
        {
            int idx = featureClass.FindField(fieldName);
            if (idx == -1)
                return;
            IField pField = featureClass.Fields.Field[idx];
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Name_2 = fieldName;
            pFieldEdit.AliasName_2 = fieldName;
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
            pFieldEdit.IsNullable_2 = true;
            pFieldEdit.Editable_2 = true;
            pFieldEdit.Length_2 = length;
            pFieldEdit.Precision_2 = precision;
            if (domain != null)
                pFieldEdit.Domain_2 = domain;
            pField = pFieldEdit;
        }

        public static IFeatureClass GetFeatureClass(string axfPath, string name)
        {
            FileInfo fileInfo = new FileInfo(axfPath);
            if (!fileInfo.Exists)
                return null;
            if (fileInfo.Directory == null)
                return null;
            IWorkspaceFactory factory = new FMEWorkspaceFactoryClass() as IWorkspaceFactory;
            if (factory == null)
                return null;
            IFeatureWorkspace pFeatureWorkspace = factory.OpenFromFile(fileInfo.Directory.FullName, 0) as IFeatureWorkspace;
            if (pFeatureWorkspace == null)
                return null;
            IFeatureDataset pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(fileInfo.Name);
            IFeatureClassContainer pFeatureClassContainer = pFeatureDataset as IFeatureClassContainer;
            if (pFeatureClassContainer == null)
                return null;
            IEnumFeatureClass pEnumFeatureClass = pFeatureClassContainer.Classes;
            IFeatureClass pFeatureClass;
            while ((pFeatureClass = pEnumFeatureClass.Next()) != null)
            {
                if (pFeatureClass.AliasName == name)
                    return pFeatureClass;
            }
            return null;
        }

        public static string GetMaxValue(IFeatureClass featureClass, string fieldName)
        {
            List<string> list = new List<string>();
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.SubFields = fieldName;
            IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
            int idx = featureCursor.FindField(fieldName);
            if (idx < 0)
                return "";
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
            {
                if (feature.Value[idx] is DBNull || feature.Value[idx] == null)
                    continue;
                list.Add(feature.Value[idx].ToString());
            }
            Marshal.ReleaseComObject(featureCursor);
            return list.Max();
        }

        public static DataTable FeatureClassToDataTable(IFeatureClass featureClass, IDictionary<int, IGeometry> geometrys = null, string whereClause = null, IDictionary<int, IField> fields = null)
        {
            DataTable dataTable;
            if (fields == null || fields.Count <= 0)
                dataTable = CreateDataTable(featureClass.AliasName, featureClass.Fields);
            else
                dataTable = CreateDataTable(featureClass.AliasName, fields.Values.ToList());
            string strGeometry = GetShapeString(featureClass);
            IFeatureCursor featureCursor = null;
            IFeature feature;
            if (geometrys == null || geometrys.Count <= 0)
            {
                IQueryFilter queryFilter = new QueryFilterClass();
                if (string.IsNullOrWhiteSpace(whereClause) == false)
                    queryFilter.WhereClause = whereClause;
                featureCursor = featureClass.Search(queryFilter, false);
                while ((feature = featureCursor.NextFeature()) != null)
                {
                    DataRow dataRow = dataTable.NewRow();
                    if (fields == null || fields.Count <= 0)
                    {
                        for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                        {
                            IField field = featureClass.Fields.Field[i];
                            if (field.Type == esriFieldType.esriFieldTypeGeometry)
                            {
                                dataRow[field.Name] = strGeometry;
                            }
                            else if (field.Type != esriFieldType.esriFieldTypeBlob)
                            {
                                dataRow[field.Name] = feature.Value[i];
                            }
                            else
                            {
                                dataRow[field.Name] = "二进制数据";
                            }
                        }
                    }
                    else
                    {
                        foreach (KeyValuePair<int, IField> field in fields)
                        {
                            if (field.Value.Type == esriFieldType.esriFieldTypeGeometry)
                            {
                                dataRow[field.Value.Name] = strGeometry;
                            }
                            else if (field.Value.Type != esriFieldType.esriFieldTypeBlob)
                            {
                                dataRow[field.Value.Name] = feature.Value[field.Key];
                            }
                            else
                            {
                                dataRow[field.Value.Name] = "二进制数据";
                            }
                        }
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                foreach (KeyValuePair<int, IGeometry> geometry in geometrys)
                {
                    ISpatialFilter spatialFilter = new SpatialFilterClass();
                    if (string.IsNullOrWhiteSpace(whereClause) == false)
                        spatialFilter.WhereClause = whereClause;
                    spatialFilter.Geometry = geometry.Value;
                    spatialFilter.GeometryField = featureClass.ShapeFieldName;
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    featureCursor = featureClass.Search(spatialFilter, false);
                    while ((feature = featureCursor.NextFeature()) != null)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        if (fields == null || fields.Count <= 0)
                        {
                            for (int i = 0; i < featureClass.Fields.FieldCount; i++)
                            {
                                IField field = featureClass.Fields.Field[i];
                                if (field.Type == esriFieldType.esriFieldTypeGeometry)
                                {
                                    dataRow[field.Name] = strGeometry;
                                }
                                else if (field.Type != esriFieldType.esriFieldTypeBlob)
                                {
                                    dataRow[field.Name] = feature.Value[i];
                                }
                                else
                                {
                                    dataRow[field.Name] = "二进制数据";
                                }
                            }
                        }
                        else
                        {
                            foreach (KeyValuePair<int, IField> field in fields)
                            {
                                if (field.Value.Type == esriFieldType.esriFieldTypeGeometry)
                                {
                                    dataRow[field.Value.Name] = strGeometry;
                                }
                                else if (field.Value.Type != esriFieldType.esriFieldTypeBlob)
                                {
                                    dataRow[field.Value.Name] = feature.Value[field.Key];
                                }
                                else
                                {
                                    dataRow[field.Value.Name] = "二进制数据";
                                }
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }

            }
            return dataTable;
        }

        public static string GetShapeString(IFeature feature)
        {
            string str;
            if (feature != null)
            {
                string str1 = "";
                switch (feature.Shape.GeometryType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        {
                            str1 = "点";
                            break;
                        }
                    case esriGeometryType.esriGeometryMultipoint:
                        {
                            str1 = "多点";
                            break;
                        }
                    case esriGeometryType.esriGeometryPolyline:
                        {
                            str1 = "线";
                            break;
                        }
                    case esriGeometryType.esriGeometryPolygon:
                        {
                            str1 = "多边形";
                            break;
                        }
                    case esriGeometryType.esriGeometryEnvelope:
                    case esriGeometryType.esriGeometryPath:
                    case esriGeometryType.esriGeometryAny:
                    case esriGeometryType.esriGeometryMultiPatch:
                        {
                            str1 = "多面";
                            break;
                        }
                    default:
                        break;
                }
                int num = feature.Fields.FindField((feature.Class as IFeatureClass).ShapeFieldName);
                IGeometryDef geometryDef = feature.Fields.Field[num].GeometryDef;
                str1 = string.Concat(str1, " ");
                if (geometryDef.HasZ)
                {
                    str1 = string.Concat(str1, "Z");
                }
                if (geometryDef.HasM)
                {
                    str1 = string.Concat(str1, "M");
                }
                str = str1;
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static string GetShapeString(esriGeometryType type)
        {
            string str = "";
            switch (type)
            {
                case esriGeometryType.esriGeometryPoint:
                    {
                        str = "点";
                        break;
                    }
                case esriGeometryType.esriGeometryMultipoint:
                    {
                        str = "多点";
                        break;
                    }
                case esriGeometryType.esriGeometryPolyline:
                    {
                        str = "线";
                        break;
                    }
                case esriGeometryType.esriGeometryPolygon:
                    {
                        str = "多边形";
                        break;
                    }
                case esriGeometryType.esriGeometryEnvelope:
                case esriGeometryType.esriGeometryPath:
                case esriGeometryType.esriGeometryAny:
                case esriGeometryType.esriGeometryMultiPatch:
                    {
                        str = "多面";
                        break;
                    }
                default:
                    break;
            }

            return str;
        }

        public static string GetShapeString(IFeatureClass pFeatClass)
        {
            string str;
            if (pFeatClass != null)
            {
                string str1 = "";
                switch (pFeatClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        {
                            str1 = "点";
                            break;
                        }
                    case esriGeometryType.esriGeometryMultipoint:
                        {
                            str1 = "多点";
                            break;
                        }
                    case esriGeometryType.esriGeometryPolyline:
                        {
                            str1 = "线";
                            break;
                        }
                    case esriGeometryType.esriGeometryPolygon:
                        {
                            str1 = "多边形";
                            break;
                        }
                    case esriGeometryType.esriGeometryEnvelope:
                    case esriGeometryType.esriGeometryPath:
                    case esriGeometryType.esriGeometryAny:
                    case esriGeometryType.esriGeometryMultiPatch:
                        {
                            str1 = "多面";
                            break;
                        }
                    default:
                        break;
                }
                int num = pFeatClass.Fields.FindField(pFeatClass.ShapeFieldName);
                IGeometryDef geometryDef = pFeatClass.Fields.Field[num].GeometryDef;
                str1 = string.Concat(str1, " ");
                if (geometryDef.HasZ)
                {
                    str1 = string.Concat(str1, "Z");
                }
                if (geometryDef.HasM)
                {
                    str1 = string.Concat(str1, "M");
                }
                str = str1;
            }
            else
            {
                str = "";
            }
            return str;
        }

        public static DataTable CreateDataTable(string tableName, List<IField> fields)
        {
            DataTable dataTable = new DataTable(tableName);
            foreach (IField field in fields)
            {
                DataColumn dataColumn = new DataColumn(field.Name)
                {
                    Caption = field.AliasName
                };
                if (field.Type == esriFieldType.esriFieldTypeDouble)
                {
                    dataColumn.DataType = Type.GetType("System.Double");
                }
                else if (field.Type == esriFieldType.esriFieldTypeInteger)
                {
                    dataColumn.DataType = Type.GetType("System.Int32");
                }
                else if (field.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    dataColumn.DataType = Type.GetType("System.Int16");
                }
                else if (field.Type == esriFieldType.esriFieldTypeSingle)
                {
                    dataColumn.DataType = Type.GetType("System.Double");
                }
                else if (field.Type == esriFieldType.esriFieldTypeDate)
                {
                    dataColumn.DataType = Type.GetType("System.DateTime");
                }

                if (field.Type == esriFieldType.esriFieldTypeBlob)
                {
                    dataColumn.ReadOnly = true;
                }
                else if (field.Type != esriFieldType.esriFieldTypeGeometry)
                {
                    dataColumn.ReadOnly = !field.Editable;
                }
                else
                {
                    dataColumn.ReadOnly = true;
                }

                dataTable.Columns.Add(dataColumn);
            }
            return dataTable;
        }

        public static DataTable CreateDataTable(string tableName, IFields fields)
        {
            DataTable dataTable = new DataTable(tableName);
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.Field[i];
                DataColumn dataColumn = new DataColumn(field.Name)
                {
                    Caption = field.AliasName
                };
                if (field.Type == esriFieldType.esriFieldTypeDouble)
                {
                    dataColumn.DataType = Type.GetType("System.Double");
                }
                else if (field.Type == esriFieldType.esriFieldTypeInteger)
                {
                    dataColumn.DataType = Type.GetType("System.Int32");
                }
                else if (field.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    dataColumn.DataType = Type.GetType("System.Int16");
                }
                else if (field.Type == esriFieldType.esriFieldTypeSingle)
                {
                    dataColumn.DataType = Type.GetType("System.Double");
                }
                else if (field.Type == esriFieldType.esriFieldTypeDate)
                {
                    dataColumn.DataType = Type.GetType("System.DateTime");
                }

                if (field.Type == esriFieldType.esriFieldTypeBlob)
                {
                    dataColumn.ReadOnly = true;
                }
                else if (field.Type != esriFieldType.esriFieldTypeGeometry)
                {
                    dataColumn.ReadOnly = !field.Editable;
                }
                else
                {
                    dataColumn.ReadOnly = true;
                }

                dataTable.Columns.Add(dataColumn);
            }
            return dataTable;
        }
    }
}
