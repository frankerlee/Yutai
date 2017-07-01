namespace Yutai.ArcGIS.Common.CodeDomainEx
{
    public class CodeDomainTableStruct
    {
        public static string ConnectionFieldName;
        public static string DescriptionFieldName;
        public static string DomainIDFieldName;
        public static string DomainNameFieldName;
        public static string fieldtypeFieldName;
        public static string IDFieldName;
        public static string ParentIDFieldName;
        public static string TableFieldName;
        public static string TableName;
        public static string ValueFieldName;

        static CodeDomainTableStruct()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            TableName = "DomainTable";
            DomainIDFieldName = "DomainID";
            DomainNameFieldName = "DomainName";
            ConnectionFieldName = "ConnectionName";
            TableFieldName = "TableName";
            ValueFieldName = "ValueField";
            DescriptionFieldName = "DescriptionField";
            fieldtypeFieldName = "FieldType";
            IDFieldName = "IDField";
            ParentIDFieldName = "ParentIDField";
        }
    }
}