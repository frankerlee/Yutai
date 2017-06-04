using ESRI.ArcGIS.Geodatabase;

namespace Yutai.ArcGIS.Common.Editor
{
	public class FieldMaps
	{
		private int int_0 = -1;

		private int int_1 = -1;

		public string DestFlieldAliasName
		{
			get;
			set;
		}

		public string SourceFieldName
		{
			get;
			set;
		}

		public string SourceFlieldAliasName
		{
			get;
			set;
		}

		public string TargetFieldName
		{
			get;
			set;
		}

		public FieldMaps()
		{
		}

		public void CopyValue(IRow irow_0, IRow irow_1)
		{
			if (this.int_0 == -1)
			{
				this.int_0 = irow_0.Fields.FindField(this.SourceFieldName);
				this.int_1 = irow_1.Fields.FindField(this.TargetFieldName);
			}
			if ((this.int_0 == -1 ? false : this.int_1 != -1))
			{
				irow_1.Value[this.int_1] = irow_0.Value[this.int_0];
			}
		}
	}
}