using System.IO;
using ESRI.ArcGIS.DataInterop;
using ESRI.ArcGIS.Geodatabase;

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
    }
}
