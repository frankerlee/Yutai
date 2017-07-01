using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace Yutai.ArcGIS.Common.Geodatabase
{
    public class FieldEdit
    {
        public FieldEdit()
        {
        }

        public static void AddDefaultField(FieldEdit.ClassType classType_0, IFieldsEdit ifieldsEdit_0)
        {
            IObjectClassDescription objectClassDescriptionClass = new ObjectClassDescription();
            switch (classType_0)
            {
                case FieldEdit.ClassType.Table:
                {
                    objectClassDescriptionClass = new ObjectClassDescription();
                    break;
                }
                case FieldEdit.ClassType.FeatureClass:
                {
                    objectClassDescriptionClass = new FeatureClassDescription();
                    break;
                }
            }
            IFields requiredFields = objectClassDescriptionClass.RequiredFields;
            for (int i = 0; i < requiredFields.FieldCount; i++)
            {
                ifieldsEdit_0.AddField(requiredFields.Field[i]);
            }
        }

        public static void AddDefaultField(IFieldsEdit ifieldsEdit_0, esriGeometryType esriGeometryType_0,
            ISpatialReference ispatialReference_0, bool bool_0, bool bool_1, int int_0)
        {
            IFieldEdit fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
            fieldClass.Name_2 = "OBJECTID";
            fieldClass.AliasName_2 = "OBJECTID";
            fieldClass.IsNullable_2 = false;
            fieldClass.Type_2 = esriFieldType.esriFieldTypeOID;
            ifieldsEdit_0.AddField(fieldClass);
            fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
            fieldClass.Name_2 = "SHAPE";
            fieldClass.AliasName_2 = "SHAPE";
            fieldClass.IsNullable_2 = true;
            fieldClass.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDefEdit geometryDefClass = new GeometryDef() as IGeometryDefEdit;
            geometryDefClass.SpatialReference_2 = ispatialReference_0;
            geometryDefClass.GridCount_2 = 1;
            geometryDefClass.GridSize_2[0] = (double) int_0;
            geometryDefClass.GeometryType_2 = esriGeometryType_0;
            geometryDefClass.HasZ_2 = bool_1;
            geometryDefClass.HasM_2 = bool_0;
            fieldClass.GeometryDef_2 = geometryDefClass;
            ifieldsEdit_0.AddField(fieldClass);
        }

        private static IFieldEdit CreateField(string string_0, esriFieldType esriFieldType_0)
        {
            IFieldEdit fieldClass = new ESRI.ArcGIS.Geodatabase.Field() as IFieldEdit;
            fieldClass.Name_2 = string_0;
            fieldClass.AliasName_2 = string_0;
            fieldClass.Type_2 = esriFieldType_0;
            return fieldClass;
        }

        public enum ClassType
        {
            Table,
            FeatureClass
        }
    }
}