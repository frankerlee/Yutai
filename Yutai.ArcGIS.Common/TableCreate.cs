using System.Runtime.CompilerServices;
using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common
{
    public abstract class TableCreate
    {
        [CompilerGenerated]
        private string string_0;

        protected TableCreate()
        {
        }

        public IField CreateDateField(string string_1, string string_2)
        {
            IField field = new Field();
            (field as IFieldEdit).Name_2 = string_1;
            (field as IFieldEdit).AliasName_2 = string_2;
            (field as IFieldEdit).Type_2 = esriFieldType.esriFieldTypeDate;
            return field;
        }

        public IField CreateDoubleField(string string_1, string string_2)
        {
            IField field = new Field();
            (field as IFieldEdit).Name_2 = string_1;
            (field as IFieldEdit).AliasName_2 = string_2;
            (field as IFieldEdit).Type_2 = esriFieldType.esriFieldTypeDouble;
            return field;
        }

        public IField CreateIntField(string string_1, string string_2)
        {
            IField field = new Field();
            (field as IFieldEdit).Name_2 = string_1;
            (field as IFieldEdit).AliasName_2 = string_2;
            (field as IFieldEdit).Type_2 = esriFieldType.esriFieldTypeInteger;
            return field;
        }

        public IField CreateStringField(string string_1, string string_2, int int_0)
        {
            IField field = new Field();
            (field as IFieldEdit).Name_2 = string_1;
            (field as IFieldEdit).AliasName_2 = string_2;
            (field as IFieldEdit).Type_2 = esriFieldType.esriFieldTypeString;
            (field as IFieldEdit).Length_2 = int_0;
            return field;
        }

        public ITable CreateTable(IFeatureWorkspace ifeatureWorkspace_0)
        {
            if (!(ifeatureWorkspace_0 as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, this.TableName))
            {
                return ifeatureWorkspace_0.CreateTable(this.TableName, this.GetFields(), null, null, "");
            }
            ITable table = ifeatureWorkspace_0.OpenTable(this.TableName);
            this.UpdateStruct(table);
            return null;
        }

        public abstract IFields GetFields();
        public virtual void UpdateStruct(ITable itable_0)
        {
        }

        public string TableName
        {
            [CompilerGenerated]
            get
            {
                return this.string_0;
            }
            [CompilerGenerated]
            set
            {
                this.string_0 = value;
            }
        }
    }
}

