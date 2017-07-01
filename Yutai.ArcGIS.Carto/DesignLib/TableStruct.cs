namespace Yutai.ArcGIS.Carto.DesignLib
{
    public abstract class TableStruct
    {
        protected TableStruct()
        {
        }

        public abstract string DescriptionFieldName { get; }

        public abstract string IDFieldName { get; }

        public abstract string NameFieldName { get; }

        public abstract string OrderIDFieldName { get; }

        public abstract string ParentIDFieldName { get; }

        public abstract string TableName { get; }
    }
}