using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Customization;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Yutai.ArcGIS.Common;

namespace Yutai.Pipeline.Editor.Classes
{
    public sealed class GSCSUtilsGen
    {
        private static readonly GSCSUtilsGen _instance;

        public static GSCSUtilsGen Instance => GSCSUtilsGen._instance;

        static GSCSUtilsGen()
        {
            GSCSUtilsGen._instance = new GSCSUtilsGen();
        }

        public GSCSUtilsGen()
        {
        }

        public IFeatureClass CreateFeatureClass(IWorkspace2 workspace, IFeatureDataset featureDataset, string featureClassName, IFields fields, UID CLSID, UID CLSEXT, string strConfigKeyword, esriGeometryType geometryType)
        {
            IFeatureClass featureClass1;
            if (featureClassName != "")
            {
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                IFeatureClass featureClass;
                if (!workspace.NameExists[esriDatasetType.esriDTFeatureClass, featureClassName])
                {
                    if (CLSID == null)
                    {
                        CLSID = new UIDClass {Value = "esriGeoDatabase.Feature"};
                    }
                    IObjectClassDescription objectClassDescription = new FeatureClassDescriptionClass();
                    if (fields == null)
                    {
                        fields = objectClassDescription.RequiredFields;
                        IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
                        IField field = new FieldClass();
                        IFieldEdit fieldEdit = (IFieldEdit)field;
                        fieldEdit.Name_2 = "SampleField";
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                        fieldEdit.IsNullable_2 = true;
                        fieldEdit.AliasName_2 = "Sample Field Column";
                        fieldEdit.DefaultValue_2 = "text";
                        fieldEdit.Editable_2 = true;
                        fieldEdit.Length_2 = 100;
                        fieldsEdit.AddField(field);
                        fields = fieldsEdit;
                    }
                    string strShapeField = "";
                    for (int j = 0; j < fields.FieldCount; j++)
                    {
                        if (fields.Field[j].Type == esriFieldType.esriFieldTypeGeometry)
                        {
                            strShapeField = fields.Field[j].Name;
                            IGeometryDefEdit geometryDefEdit = fields.Field[j].GeometryDef as IGeometryDefEdit;
                            if (geometryDefEdit != null)
                                geometryDefEdit.GeometryType_2 = geometryType;
                        }
                    }
                    IFieldChecker fieldChecker = new FieldCheckerClass();
                    IEnumFieldError enumFieldError = null;
                    IFields validatedFields = null;
                    fieldChecker.ValidateWorkspace = (IWorkspace) workspace;
                    fieldChecker.Validate(fields, out enumFieldError, out validatedFields);
                    featureClass = (featureDataset != null ? featureDataset.CreateFeatureClass(featureClassName, validatedFields, CLSID, CLSEXT, esriFeatureType.esriFTSimple, strShapeField, strConfigKeyword) : featureWorkspace.CreateFeatureClass(featureClassName, validatedFields, CLSID, CLSEXT, esriFeatureType.esriFTSimple, strShapeField, strConfigKeyword));
                    featureClass1 = featureClass;
                }
                else
                {
                    featureClass = featureWorkspace.OpenFeatureClass(featureClassName);
                    featureClass1 = featureClass;
                }
            }
            else
            {
                featureClass1 = null;
            }
            return featureClass1;
        }

        public ITable CreateTable(IWorkspace2 workspace, string tableName, IFields fields)
        {
            ITable table;
            UID uid = new UIDClass();
            if (workspace != null)
            {
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                if (!workspace.NameExists[ esriDatasetType.esriDTTable, tableName])
                {
                    uid.Value= "esriGeoDatabase.Object";
                    IObjectClassDescription objectClassDescription = new ObjectClassDescriptionClass();
                    if (fields == null)
                    {
                        fields = objectClassDescription.RequiredFields;
                        IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
                        IField field = new FieldClass();
                        IFieldEdit fieldEdit = (IFieldEdit)field;
                        fieldEdit.Name_2 = "SampleField";
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                        fieldEdit.IsNullable_2 = true;
                        fieldEdit.AliasName_2 = "Sample Field Column";
                        fieldEdit.DefaultValue_2 = "test";
                        fieldEdit.Editable_2 = true;
                        fieldEdit.Length_2 = 100;
                        fieldsEdit.AddField(field);
                        fields = fieldsEdit;
                    }
                    IFieldChecker fieldChecker = new FieldCheckerClass();
                    IEnumFieldError enumFieldError = null;
                    IFields validatedFields = null;
                    fieldChecker.ValidateWorkspace= (IWorkspace)workspace;
                    fieldChecker.Validate(fields, out enumFieldError, out validatedFields);
                    table = featureWorkspace.CreateTable(tableName, validatedFields, uid, null, "");
                }
                else
                {
                    table = featureWorkspace.OpenTable(tableName);
                }
            }
            else
            {
                table = null;
            }
            return table;
        }

    }
}
