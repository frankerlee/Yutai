using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.ZD
{
	public abstract class ZDTableCreate
	{
		[System.Runtime.CompilerServices.CompilerGenerated]
		private string string_0;

		public string TableName
		{
			get;
			set;
		}

		public virtual void UpdateStruct(ITable itable_0)
		{
		}

		public abstract IFields GetFields();

		public ITable CreateTable(IFeatureWorkspace ifeatureWorkspace_0)
		{
			ITable table;
			ITable result;
			if (!(ifeatureWorkspace_0 as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, this.TableName))
			{
				try
				{
					table = ifeatureWorkspace_0.CreateTable(this.TableName, this.GetFields(), null, null, "");
					result = table;
					return result;
				}
				catch (System.Exception)
				{
					goto IL_4B;
				}
			}
			table = ifeatureWorkspace_0.OpenTable(this.TableName);
			this.UpdateStruct(table);
			IL_4B:
			result = null;
			return result;
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
	}
}
