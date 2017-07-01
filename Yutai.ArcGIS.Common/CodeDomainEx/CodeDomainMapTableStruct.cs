namespace Yutai.ArcGIS.Common.CodeDomainEx
{
    public class CodeDomainMapTableStruct
    {
        public static string _TableName;
        public static string DomainID;
        public static string FeatureClassName;
        public static string FieldName;

        static CodeDomainMapTableStruct()
        {
            old_acctor_mc();
        }

        private static void old_acctor_mc()
        {
            _TableName = "CodeDomainMap";
            FieldName = "FieldName";
            FeatureClassName = "FeatureClassName";
            DomainID = "DomainID";
        }

        public static string TableName
        {
            get { return _TableName; }
            set { }
        }
    }
}